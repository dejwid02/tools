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
            var movie = new ScheduleParser().ParseMovie("");
            IEnumerable<TvListingItem> contents = null;
            IMoviesRepository repository = new MoviesRepository(new MoviesDbContext());

            try
            {
                var providerUrl = Environment.GetEnvironmentVariable("MoviesProviderUrl");

                var channels = repository.GetAllChannels().Take(1);
                var filterDate = repository.GetLastLog()?.LastSynchronizedDate ?? DateTime.Now;
                var scheduleParser = new ScheduleParser();
                var tvItems = channels.SelectMany(channel => scheduleParser.ParseTvSchedule(channel, providerUrl)).ToList();

                contents = tvItems.Where(item => item.Movie.Rating > 4.0 && item.StartTime > filterDate).OrderBy(i => i.StartTime);
                var existingMovies = repository.GetAllMovies().ToList();
                foreach (var tvListingItem in contents)
                {
                    var existingMovie = existingMovies.FirstOrDefault(m => m.Id == tvListingItem.Movie.Id);
                    if (existingMovie != null)
                        tvListingItem.Movie = existingMovie;
                    else
                    {
                        existingMovies.Add(tvListingItem.Movie);
                        tvListingItem.Movie.Title = tvListingItem.Movie.Title.Replace("&oacute;", "ó");
                        tvListingItem.Movie.Description = tvListingItem.Movie.Description.Replace("&oacute;", "ó");
                        noOfMoviesCreated++;
                    }

                    repository.Add(tvListingItem);
                }

                repository.SaveChanges();
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
