using HtmlAgilityPack;
using MovieParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace MovieParser
{
    public class ScheduleParser
    {
        public IList<TvListingItem> ParseTvSchedule(Channel channel, string providerUrl)
        {
            var client = new WebClient();           
            var content = client.DownloadString(channel.Url);
            client.Dispose();
            var movies = new List<TvListingItem>();
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            var seances = doc.DocumentNode.Descendants().Where(node => node.Name == "div" && node.Attributes.Select(a => a.Value).Any(v => v.StartsWith("seanceInfo"))).ToList();
            var allSeances = seances.SelectMany(s => (IEnumerable<dynamic>)Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(s.InnerText).seances).ToList();
            var allSeancesTyped = allSeances.Select(s => new {
                IdData = long.Parse(s.Name),
                Rating = s.First.film_rating?.Value,
                Type = s.First.type?.Value,
                AgeRating = s.First.age_rating?.Value,
                Duration = ExtractDurationFromString(s.First.duration?.Value),
                MovieType = s.First.type_descr?.Value,
                Description = s.First.synopsis?.Value
            }).ToList();

            var scheduleItems = doc.DocumentNode.Descendants().Where(node => node.Name == "div" && node.Attributes.Select(a => a.Value).Any(v => v.StartsWith("seance film"))).ToList();

            return scheduleItems.Select(node => {
                var movieData = allSeancesTyped.FirstOrDefault(m => m.IdData == long.Parse(node.Attributes["data-sid"].Value));
                var movieLink = node.Descendants().Where(n2 => n2.Name == "a").FirstOrDefault();
                var url = movieLink != null ? new Uri(providerUrl).Append(movieLink?.Attributes["href"]?.Value).ToString() : "";
                return new TvListingItem()
                {
                    Movie = new Movie()
                    {
                        Id = long.Parse(node.Attributes["data-film"]?.Value ?? "-1"),
                        Url = url,
                        Year = ExtractYearFromString(url),
                        Title = movieLink?.InnerText,
                        Rating = movieData?.Rating,
                        AgeRating = (int)movieData?.AgeRating,
                        Duration = movieData?.Duration,
                        Category = movieData?.MovieType,
                        Description = movieData?.Description
                    },
                    StartTime = ParseDate(node.Attributes["data-start"].Value),
                    Channel = channel
                };
            }).ToList();
        }

        private int ExtractDurationFromString(string value)
        {
            var numberStrings = Regex.Split(value, @"\D+");
            var numbers = numberStrings.Where(s => !string.IsNullOrEmpty(s)).Select(s => int.Parse(s)).ToList();
            return numbers.LastOrDefault() + numbers.Count() > 1 ? numbers[0] * 60 : 0;
        }

        private int? ExtractYearFromString(string value)
        {
            if (value == null) return null;
            var numberStrings = Regex.Split(value, @"\D+");
            var numbers = numberStrings.Where(s => !string.IsNullOrEmpty(s)).Select(s => int.Parse(s));
            return numbers.Reverse().Skip(1).FirstOrDefault();
        }

        private static DateTime ParseDate(string date)
        {
            var components = date.Split(',').Select(c => int.Parse(c)).ToList();
            return new DateTime(components[0], components[1], components[2], components[3], components[4], 0);
        }
    }
}
