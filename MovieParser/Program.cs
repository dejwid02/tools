using HtmlAgilityPack;
using System;
using System.Web;
using System.Linq;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using MovieParser.DAL;
using MovieParser.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieParser
{
    class Program
    {
        static void Main(string[] args)
        {
            int status = 0;
            int noOfMoviesCreated = 0;
            string error = "";
            var startDate = DateTime.Now;
            IEnumerable<TvListingItem> contents = null;


            var configurationBuilder = new ConfigurationBuilder()
                                           .SetBasePath(Directory.GetCurrentDirectory())
                                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configurationBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<MoviesDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            IMoviesRepository repository = new MoviesRepository(new MoviesDbContext(optionsBuilder.Options));

            try
            {
                var providerUrl = Environment.GetEnvironmentVariable("MoviesProviderUrl");

                var channels = repository.GetAllChannels();
                var filterDate = repository.GetLastLog()?.LastSynchronizedDate ?? DateTime.Now;
                List<TvListingItem> tvItems = GetTeleTvItems(providerUrl, channels);

                contents = tvItems.Where(item => (item.Movie.Rating > 6.0 || item.Movie.Rating == null) && item.StartTime > filterDate).OrderBy(i => i.StartTime);
                var existingMovies = new List<Movie>();
                var existingActors = new List<Actor>();
                var existingDirectors = new List<Director>();
                foreach (var tvListingItem in contents)
                {
                    var existingMovie = existingMovies.FirstOrDefault(m => m.Title == tvListingItem.Movie.Title && m.Year == tvListingItem.Movie.Year)
                        ?? repository.GetMovieByYearAndTitle(tvListingItem.Movie.Year, tvListingItem.Movie.Title);
                    if (existingMovie != null)
                    {
                        existingMovie.Rating = tvListingItem.Movie.Rating;
                        existingMovie.Category = existingMovie.Category?.ToLower();
                        tvListingItem.Movie = existingMovie;
                    }
                    else
                    {
                        var client = new WebClient();
                        var content = client.DownloadString(providerUrl + tvListingItem.Movie.Url);
                        client.Dispose();
                        new TeleParser().FillMovieDetails(tvListingItem.Movie, content);
                        System.Threading.Thread.Sleep(1000);

                        if (tvListingItem.Movie.ImageUrl != null)
                        {
                            SaveImage(tvListingItem.Movie.ImageUrl, @"C:\inetpub\wwwroot\Movies\images");
                            tvListingItem.Movie.ImageUrl = "/images/" + GetFileName(tvListingItem.Movie.ImageUrl);
                            System.Threading.Thread.Sleep(1000);
                        }

                        existingMovies.Add(tvListingItem.Movie);
                        for (int i = 0; i < tvListingItem.Movie.Actors.Count; i++)
                        {
                            var actor = tvListingItem.Movie.Actors[i];
                            var existingActor = existingActors.FirstOrDefault(a => a.FirstName == actor.FirstName && a.LastName == actor.LastName)
                                ?? repository.GetActorsByName(actor.FirstName, actor.LastName);
                            if (existingActor != null)
                            {
                                tvListingItem.Movie.Actors[i] = existingActor;
                            }
                            else
                            {
                                existingActors.Add(actor);
                            }
                        }
                        var director = tvListingItem.Movie.Director;
                        var existingDirector = existingDirectors.FirstOrDefault(a => a.FirstName == director.FirstName && a.LastName == director.LastName)
                               ?? repository.GetDirectorsByName(director.FirstName, director.LastName);
                        if (existingDirector != null)
                        {
                            tvListingItem.Movie.Director = existingDirector;
                        }
                        else
                        {
                            existingDirectors.Add(director);
                        }
                        noOfMoviesCreated++;
                    }

                    repository.Add(tvListingItem);
                }
                Console.WriteLine("Saving data to database...");
                repository.SaveChanges();
                Console.WriteLine($"Created {noOfMoviesCreated} movies...");
            }
            catch (Exception ex)
            {
                status = 1;
                error = ex.ToString();
            }
            var finishDate = DateTime.Now;
            var configurationItem = new LogData()
            {
                StartDate = startDate,
                FinishDate = finishDate,
                Duration = finishDate - startDate,
                LastSynchronizedDate = contents?.LastOrDefault()?.StartTime ?? finishDate,
                ErrorMessage = error,
                Status = status,
                NoOfMoviesCreated = noOfMoviesCreated,
                NoOfTvListingItemsCreated = contents?.Count() ?? 0
            };
            repository.Add(configurationItem);
            repository.SaveChanges();
        }

        private static List<TvListingItem> GetTvItems(string providerUrl, Channel[] channels)
        {
            EpgParser scheduleParser = new EpgParser();
            string content = GetContent(providerUrl);
            Console.WriteLine("Parsing...");
            var tvItems = scheduleParser.ParseTvSchedule(channels, content).ToList();
            return tvItems;
        }

        private static List<TvListingItem> GetTeleTvItems(string providerUrl, Channel[] channels)
        {
            var scheduleParser = new TeleParser();
            string[] contents = GetTeleContent(providerUrl).Result.ToArray();
            Console.WriteLine("Parsing...");
            var tvItems = contents.SelectMany(content => scheduleParser.ParseTvSchedule(channels, content)).ToList();
            return tvItems;
        }

        private async static Task<string> GetTeleContentPage(string url, int pageNr)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url + $"filmy?page={pageNr}"))
                {
                    request.Headers.TryAddWithoutValidation("authority", url);
                    request.Headers.TryAddWithoutValidation("cache-control", "max-age=0");
                    request.Headers.TryAddWithoutValidation("upgrade-insecure-requests", "1");
                    request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.122 Safari/537.36");
                    request.Headers.TryAddWithoutValidation("sec-fetch-dest", "document");
                    request.Headers.TryAddWithoutValidation("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                    request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
                    request.Headers.TryAddWithoutValidation("sec-fetch-mode", "navigate");
                    request.Headers.TryAddWithoutValidation("sec-fetch-user", "?1");
                    request.Headers.TryAddWithoutValidation("referer", url + "moje-stacje");
                    request.Headers.TryAddWithoutValidation("accept-language", "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                    request.Headers.TryAddWithoutValidation("cookie", "_ga=GA1.2.550524766.1583165680; _gid=GA1.2.514516976.1583165680; __gads=ID=aa00795e9f054f80:T=1583165683:S=ALNI_MbB_FabtWs_bc1YKqUtPpiSgC5sww; SID=ge2nbc6lm4j9mbw4bva7ud01uztfp8ur");

                    var response = await httpClient.SendAsync(request);
                    var result = await response.Content.ReadAsStringAsync();


                    return result;
                }
            }
        }

        private async static Task<IEnumerable<string>> GetTeleContent(string url)
        {
            int page = 1;
            int totalPages = 1;
            var result = new List<string>();
            do
            {
                var pageContent = await GetTeleContentPage(url, page);
                var index = pageContent.IndexOf("<div>Strona");
                var pageString = new string(pageContent.Substring(index + 11).TakeWhile(c => c != '<').ToArray());
                totalPages = int.Parse(pageString.Split("/")[1]);
                result.Add(pageContent);
                System.Threading.Thread.Sleep(1000);
            }
            while (page++ < totalPages);

            return result;

        }
        private static string GetContent(string providerUrl)
        {
            Console.WriteLine("Downloading data...");
            var webClient = new WebClient();
            var content = webClient.DownloadString(providerUrl);
            webClient.Dispose();
            return content;
        }

        public static void SaveImage(string url, string directoryPath)
        {

            var filePath = System.IO.Path.Combine(directoryPath, GetFileName(url));
            if (!System.IO.File.Exists(filePath))
            {
                Console.WriteLine($"Downloading image {url}");
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri($"https:{url}"), filePath);
                }
            }

        }

        private static string GetFileName(string path)
        {
            var index = path.LastIndexOf('/');
            return path.Substring(index + 1);
        }
    }
}
