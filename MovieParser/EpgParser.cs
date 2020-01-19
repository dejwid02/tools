using MovieParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MovieParser
{
    public class EpgParser
    {
        public IList<TvListingItem> ParseTvSchedule(IList<Channel> channels, string content, string providerUrl)
        {

            var movies = new List<TvListingItem>();
            var elemnt = XElement.Parse(content);
            var selectedChannels = channels.Select(c => c.Name).ToList();
            var filteredMovies = elemnt.Descendants("programme").Where(node => selectedChannels.Contains(node.Attributes("channel").Single().Value) && node.Descendants("star-rating").Count() > 0);
            return filteredMovies.Select(movieNode =>
            {
                var channelName = movieNode.Attributes("channel").Single().Value;
                var startDate = ParseDate(movieNode.Attributes("start").Single().Value);
                var stopDate = ParseDate(movieNode.Attributes("stop").Single().Value);
                double rating;
                int ageRating;
                return new TvListingItem()
                {
                    Channel = channels.Single(c=>c.Name == channelName),
                    StartTime = startDate,
                    Movie = new Movie()
                    {
                        Title = movieNode.Descendants("title").Single().Value,
                        Description = movieNode.Descendants("desc").Single().Value,
                        Category = $"{movieNode.Descendants("category").First().Value} {movieNode.Descendants("category").Skip(1).First().Value}",
                        Year = int.Parse(movieNode.Descendants("date").Single().Value),
                        Rating = double.TryParse(movieNode.Descendants("star-rating").FirstOrDefault()?.Descendants()?.FirstOrDefault()?.Value, out rating) ? (double?)rating : null,
                        Duration = (int)(stopDate - startDate).TotalMinutes,
                        AgeRating = int.TryParse(movieNode.Descendants("rating").FirstOrDefault()?.Descendants()?.FirstOrDefault()?.Value, out ageRating) ? ageRating : 0,
                        Country = movieNode.Descendants("country").Single().Value,
                    }
                };
            }
            ).ToList();
        }

        private DateTime ParseDate(string date)
        {
            var year = int.Parse(date.Substring(0, 4));
            var month = int.Parse(date.Substring(4, 2));
            var day = int.Parse(date.Substring(6, 2));
            var hour = int.Parse(date.Substring(8, 2));
            var minute = int.Parse(date.Substring(10, 2));
            return new DateTime(year, month, day, hour, minute, 0);
        }
    }
}
