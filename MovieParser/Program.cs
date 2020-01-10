using HtmlAgilityPack;
using System;
using System.Web;
using System.Linq;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MovieParser.DAL;
using MovieParser.Entities;

namespace MovieParser
{
    class Program
    {
        static void Main(string[] args)
        {
            int status = 0;
            string error = "";
            var startDate = DateTime.Now;
            IEnumerable<TvListingItem> contents = null;
            IMoviesRepository repository = new MoviesRepository(new MoviesDbContext());
            try
            {
                var providerUrl = Environment.GetEnvironmentVariable("MoviesProviderUrl");
                var baseUrl = new Uri(providerUrl);
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Movies_{DateTime.Now.ToString("MM_dd_hh_mm")}.txt");

                
                var channels = repository.GetAllChannels();


                var scheduleParser = new ScheduleParser();
                var tvItems = channels.SelectMany(channel => scheduleParser.ParseTvSchedule(channel));
                contents = tvItems.Where(item => item.Movie.Rating > 6.5 && item.StartTime > DateTime.Now).OrderBy(i => i.StartTime);
                contents.ToList().ForEach(i => repository.Add(i));
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
                LastSynchronizedDate = contents?.Last()?.StartTime ?? finishDate,
                ErrorMessage = error,
                Status = status
            };
            repository.Add(configurationItem);
            repository.SaveChanges();
            
            //.OrderBy(item => item.StartTime).GroupBy(f => f.Movie.Id).ToList()
            //    .Select(item =>
            //        $"{item.First().StartTime.ToString("dd MMM hh:mm")} {item.First().Channel.Name} {item.First().Movie.Title} {item.First().Movie.MovieType} {item.First().Movie.Rating?.ToString("F2") ?? "NA"}");
            
            //File.WriteAllLines(filePath, contents);
        }

        //private static Entities.Channel CreateChannel(Uri baseUrl, string channelUrl, string channelName)
        //{
        //    var tvScheduleUrl = "program-tv";
        //    var url = baseUrl.Append(tvScheduleUrl, channelUrl);
        //    var channel = new Entities.Channel()
        //    {
        //        Name = channelName,
        //        Url = url.ToString()
        //    };
        //    return channel;
        //}


        //parse seancesinfo nodes
    }
}
