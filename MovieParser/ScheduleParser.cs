using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MovieParser
{
    public class ScheduleParser
    {
        public IList<TvScheduleItem> ParseTvSchedule(Channel channel)
        {
            var client = new WebClient();           
            var content = client.DownloadString(channel.Url);
            client.Dispose();
            var movies = new List<TvScheduleItem>();
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            var seances = doc.DocumentNode.Descendants().Where(node => node.Name == "div" && node.Attributes.Select(a => a.Value).Any(v => v.StartsWith("seanceInfo"))).ToList();
            var allSeances = seances.SelectMany(s => (IEnumerable<dynamic>)Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(s.InnerText).seances).ToList();
            var allSeancesTyped = allSeances.Select(s => new {
                IdData = long.Parse(s.Name),
                Rating = s.First.film_rating?.Value,
                Type = s.First.type?.Value,
                MovieType = s.First.type_descr?.Value,
                Description = s.First.synopsis?.Value
            }).ToList();

            var scheduleItems = doc.DocumentNode.Descendants().Where(node => node.Name == "div" && node.Attributes.Select(a => a.Value).Any(v => v.StartsWith("seance film"))).ToList();

            return scheduleItems.Select(node => {
                var movieData = allSeancesTyped.FirstOrDefault(m => m.IdData == long.Parse(node.Attributes["data-sid"].Value));
                return new TvScheduleItem()
                {
                    Movie = new Movie()
                    {
                        Id = long.Parse(node.Attributes["data-film"]?.Value),
                        Title = node.Descendants().Where(n2 => n2.Name == "a").FirstOrDefault()?.InnerText,
                        Rating = movieData?.Rating,
                        MovieType = movieData?.MovieType,
                        Description = movieData?.Description
                    },
                    StartTime = ParseDate(node.Attributes["data-start"].Value),
                    Channel = channel
                };
            }).ToList();
        }

        private static DateTime ParseDate(string date)
        {
            var components = date.Split(',').Select(c => int.Parse(c)).ToList();
            return new DateTime(components[0], components[1], components[2], components[3], components[4], 0);
        }
    }
}
