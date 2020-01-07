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

            var channels = new List<Channel> {
            CreateChannel(baseUrl, "Canal%2B", "Canal+"),
            CreateChannel(baseUrl, "Canal%2B+Film", "Canal+ Film"),
            CreateChannel(baseUrl, "Canal%2B+Family", "Canal+ Family"),
            CreateChannel(baseUrl, "HBO", "HBO"),
            CreateChannel(baseUrl, "HBO2", "HBO2"),
            CreateChannel(baseUrl, "HBO+3", "HBO3")

            };

            var scheduleParser = new ScheduleParser();
            var tvItems = channels.SelectMany(channel=>scheduleParser.ParseTvSchedule(channel));
            tvItems.Where(item => item.Movie.Rating > 6.5 && item.StartTime > DateTime.Now).OrderBy(item=>item.StartTime).ToList()
                .ForEach(item => Console.WriteLine(
                    $"{item.StartTime.ToString("dd MMM hh:mm")} {item.Channel.Name} {item.Movie.Title} {item.Movie.MovieType} {item.Movie.Rating?.ToString("F2") ?? "NA"}"));
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
