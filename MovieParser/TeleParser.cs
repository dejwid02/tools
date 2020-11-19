using HtmlAgilityPack;
using MovieParser.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MovieParser
{
    public class TeleParser
    {
        public TeleParser()
        {

        }

        public IList<TvListingItem> ParseTvSchedule(IEnumerable<Channel> channels, string content)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            var allNodes = doc.DocumentNode.Descendants().Where(n => n.Name == "a" && n.Attributes["class"]?.Value == "movie-search-item").ToList();

            var result = allNodes.Select(n =>
                new TvListingItem
                {
                    Channel = channels.First(c => c.Name == getNameFromString(n.Descendants().Single(n2 => n2.Name == "figure").OuterHtml)),
                    StartTime = ParseDate(n.ChildNodes.Last().ChildNodes.Last().PreviousSibling.InnerText, n.ChildNodes.Last().ChildNodes.Last().InnerText),
                    Movie = new Movie()
                    {
                        Title = n.Descendants().Single(n2 => n2.Name == "h3").InnerText,
                        Rating = ParseRating(n.Descendants().SingleOrDefault(n => n.Name == "div" && n.Attributes["class"]?.Value == "imdb")?.InnerText),
                        Category = n.Descendants().SingleOrDefault(n => n.Name == "div" && n.Attributes["class"]?.Value == "info").FirstChild.InnerText.ToLower(),
                        Year = ParseYear(n.Descendants().SingleOrDefault(n => n.Name == "div" && n.Attributes["class"]?.Value == "info").LastChild.InnerText),
                        Country = ParseCountry(n.Descendants().SingleOrDefault(n => n.Name == "div" && n.Attributes["class"]?.Value == "info").LastChild.InnerText),
                        Url = n.Attributes["href"].Value,
                        ImageUrl = n.ChildNodes.First()?.Attributes["src"]?.Value
                    }
                }).ToList();
            return result;
        }

        private string ParseCountry(string innerText)
        {
            return innerText.Split(" ").Last();
        }

        private int? ParseYear(string innerText)
        {
            var year = innerText.Split(" ").First();
            int result = 0;
            if (int.TryParse(year, out result))
                return result;
            return null;
        }

        private double? ParseRating(string innerText)
        {
            if (innerText == null) return null;
            var score = innerText.Replace(",", ".");
            return double.Parse(score, CultureInfo.InvariantCulture);
        }

        private DateTime ParseDate(string daymonth, string hourminute)
        {
            var dayMonth = daymonth.Split(" ").Last();
            int day = int.Parse(dayMonth.Split(".").First());
            int month = int.Parse(dayMonth.Split(".").Last());

            int hour = int.Parse(hourminute.Split(":").First());
            int minute = int.Parse(hourminute.Split(":").Last());
            int year = DateTime.Now.Year;
            if (month == 1 && DateTime.Now.Month == 12)
                year++;
            return new DateTime(year, month, day, hour, minute, 0);
        }

        public void FillMovieDetails(Movie movie, string content)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            var rankNode = doc.DocumentNode.Descendants().FirstOrDefault(n => n.Name == "a" && n.Attributes["class"]?.Value == "movieRank filmwebRank");
            double? rank = ParseRank(rankNode?.FirstChild?.InnerText);
            var rankUrl = rankNode?.Attributes["href"]?.Value;
            movie.Url = rankUrl;
            if (movie.Rating == null)
                movie.Rating = rank;
            else
                movie.Rating = Math.Max(movie.Rating.Value, rank ?? 0);

            var tag = doc.DocumentNode.Descendants().FirstOrDefault(n => n.Name == "a" && n.Attributes["itemprop"]?.Value == "director");
            if (tag != null)
            {
                var names = tag.InnerText.Split(" ");
                movie.Director.FirstName = names.ElementAtOrDefault(0);
                movie.Director.LastName = names.ElementAtOrDefault(1);
            }

            var descriptionTag = FindTagWithItemProp(doc, "p", "description").FirstOrDefault();
            var description = Regex.Replace(descriptionTag.InnerText, @"\s?\(?<[a-z<>\s]*>\)?", "");
            if (!string.IsNullOrEmpty(description))
                movie.Description = description;
            var imageTag = FindTagWithItemProp(doc, "img", "image").FirstOrDefault();
            if (imageTag!=null)
                movie.ImageUrl = imageTag.Attributes["src"]?.Value;
            var tags = doc.DocumentNode.Descendants().Where(n => n.Name == "a" && n.Attributes["itemprop"]?.Value == "actor");
            if (tags.Any() && !movie.Actors.Any())
            {
                movie.Actors = tags.Take(3).Select(t =>
                {
                    var names = t.InnerText.Split(" "); ;
                    return new Actor
                    {
                        FirstName = names.ElementAtOrDefault(0),
                        LastName = names.ElementAtOrDefault(1)
                    };

                }).ToList();

            }

         
        }


        private IEnumerable<HtmlNode>  FindTagWithItemProp(HtmlDocument doc, string tagName, string itemPropValue)
        {
           return doc.DocumentNode.Descendants().Where(n => n.Name == tagName && n.Attributes["itemprop"]?.Value == itemPropValue);
        }
        private double? ParseRank(string innerText)
        {
            if (innerText == null) return null;
            return double.Parse(innerText, CultureInfo.InvariantCulture);
        }

        private string getNameFromString(string v)
        {
            if (v.Contains("16.png"))
                return "CanalPlus.pl";
            return "HBO.pl";
        }
    }
}
