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
            IMoviesRepository repository = new MoviesRepository(new MoviesDbContext(SQLS.UseSqlServer(""));

            try
            {
                var providerUrl = Environment.GetEnvironmentVariable("MoviesProviderUrl");

                var channels = repository.GetAllChannels();
                var filterDate = repository.GetLastLog()?.LastSynchronizedDate ?? DateTime.Now;
                var scheduleParser = new EpgParser();
                Console.WriteLine("Downloading data...");
                var webClient = new WebClient();
                var content = webClient.DownloadString(providerUrl);
                webClient.Dispose();
                Console.WriteLine("Parsing...");
                var tvItems = scheduleParser.ParseTvSchedule(channels, content).ToList();

                contents = tvItems.Where(item => item.Movie.Rating > 2.0 && item.StartTime > filterDate).OrderBy(i => i.StartTime);
                var existingMovies = new List<Movie>();
                var existingActors = new List<Actor>();
                var existingDirectors = new List<Director>();
                foreach (var tvListingItem in contents)
                {
                    var existingMovie = existingMovies.FirstOrDefault(m => m.Title == tvListingItem.Movie.Title && m.Year == tvListingItem.Movie.Year)
                        ?? repository.GetMovieByYearAndTitle(tvListingItem.Movie.Year, tvListingItem.Movie.Title);
                    if (existingMovie != null)
                        tvListingItem.Movie = existingMovie;
                    else
                    {
                        existingMovies.Add(tvListingItem.Movie);
                        for (int i =0; i< tvListingItem.Movie.Actors.Count; i++)
                        {
                            var actor = tvListingItem.Movie.Actors[i];
                            var existingActor = existingActors.FirstOrDefault(a => a.FirstName == actor.FirstName && a.LastName == actor.LastName)
                                ?? repository.GetActorsByName(actor.FirstName, actor.LastName);
                            if (existingActor!= null)
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
    }
}
