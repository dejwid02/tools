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
        public IList<TvListingItem> ParseTvSchedule(IEnumerable<Channel> channels, string content)
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
                var creditsNodes = movieNode.Descendants("credits").Single();
                double rating;
                int ageRating;
                int year;
                return new TvListingItem()
                {
                    Channel = channels.Single(c=>c.Name == channelName),
                    StartTime = startDate,
                    Movie = new Movie()
                    {
                        Title = movieNode.Descendants("title").Single().Value,
                        Actors = creditsNodes.Descendants("actor").Take(4).Select(node=>CreateActor(node.Value)).ToList(),
                        Director = CreateDirector(creditsNodes.Descendants("director").First().Value),
                        Description = movieNode.Descendants("desc").Single().Value,
                        Category = $"{movieNode.Descendants("category").First().Value} {movieNode.Descendants("category").Skip(1)?.FirstOrDefault()?.Value ?? ""}",
                        Year = int.TryParse(movieNode.Descendants("date").FirstOrDefault()?.Value, out year) ? (int?)year : null,
                        Rating = double.TryParse(movieNode.Descendants("star-rating").FirstOrDefault()?.Descendants()?.FirstOrDefault()?.Value, out rating) ? (double?)rating : null,
                        Duration = (int)(stopDate - startDate).TotalMinutes,
                        AgeRating = int.TryParse(movieNode.Descendants("rating").FirstOrDefault()?.Descendants()?.FirstOrDefault()?.Value, out ageRating) ? ageRating : 0,
                        Country = movieNode.Descendants("country").SingleOrDefault()?.Value ?? "",
                    }
                };
            }
            ).ToList();
        }

        private Actor CreateActor(string fullName)
        {
            var array = fullName.Split(" ");
            return new Actor
            {
                FirstName = array[0],
                LastName = array.ElementAtOrDefault(1) ?? ""
            };
        }

        private Director CreateDirector(string fullName)
        {
            var array = fullName.Split(" ");
            return new Director
            {
                FirstName = array[0],
                LastName = array[1]
            };
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
