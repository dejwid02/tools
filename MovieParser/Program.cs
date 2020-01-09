using HtmlAgilityPack;
using System;
using System.Web;
using System.Linq;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace MovieParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var providerUrl = Environment.GetEnvironmentVariable("MoviesProviderUrl");
            var baseUrl = new Uri(providerUrl);
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Movies_{DateTime.Now.ToString("MM_dd_hh_mm")}.txt");

            var channels = new List<Channel> {
            CreateChannel(baseUrl, "Canal%2B", "Canal+"),
            CreateChannel(baseUrl, "Canal%2B+Film", "Canal+ Film"),
            CreateChannel(baseUrl, "Canal%2B+Family", "Canal+ Family"),
            CreateChannel(baseUrl, "HBO", "HBO"),
            CreateChannel(baseUrl, "HBO2", "HBO2"),
            CreateChannel(baseUrl, "HBO+3", "HBO3")

            };
            ;
            var scheduleParser = new ScheduleParser();
            var tvItems = channels.SelectMany(channel=>scheduleParser.ParseTvSchedule(channel));
            var contents = tvItems.Where(item => item.Movie.Rating > 6.5 && item.StartTime > DateTime.Now).OrderBy(item=>item.StartTime).GroupBy(f=>f.Movie.Id).ToList()
                .Select(item=>
                    $"{item.First().StartTime.ToString("dd MMM hh:mm")} {item.First().Channel.Name} {item.First().Movie.Title} {item.First().Movie.MovieType} {item.First().Movie.Rating?.ToString("F2") ?? "NA"}");
            File.WriteAllLines(filePath, contents);
        }

        private static Channel CreateChannel(Uri baseUrl, string channelUrl, string channelName)
        {
            var tvScheduleUrl = "program-tv";
            var url = baseUrl.Append(tvScheduleUrl, channelUrl);
            var channel = new Channel()
            {
                Name = channelName,
                Url = url
            };
            return channel;
        }


        //parse seancesinfo nodes
    }
}
