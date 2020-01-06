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
            var client = new WebClient();
            var providerUrl = Environment.GetEnvironmentVariable("MoviesProviderUrl");
            var baseUrl = new Uri(providerUrl);
            
            var tvScheduleUrl = "program-tv";
            var channel = "HBO";
            var url = baseUrl.Append(tvScheduleUrl, channel);
            var content = client.DownloadString(url);
            client.Dispose();
            var movies = new List<Movie>();
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            var seances = doc.DocumentNode.Descendants().Where(node => node.Name == "div" && node.Attributes.Select(a => a.Value).Any(v => v.StartsWith("seanceInfo"))).ToList();
            var allSeances = seances.SelectMany(s=> (IEnumerable<dynamic>)Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(s.InnerText).seances).ToList();
            var allSeancesTyped = allSeances.Select(s => new {
                Id = long.Parse(s.Name), 
                Rating = s.First.film_rating?.Value, 
                Type = s.First.type?.Value, MovieType = s.First.type_descr?.Value,
                Description = s.First.synopsis?.Value }).ToList();
            //foreach(var s in descr.seancallSeanceses)
            //{
            //    var m = s;
            //}
            var moviesNodes = doc.DocumentNode.Descendants().Where(node => node.Name == "div" && node.Attributes.Select(a => a.Value).Any(v => v.StartsWith("seance film"))).ToList();

            movies = moviesNodes.Select(node=> {
                var movieData = allSeancesTyped.FirstOrDefault(m => m.Id == node.Attributes["data-sid"].Value);
                return new Movie()
                {
                    Id = int.Parse(node.Attributes["data-sid"].Value),
                    Title = node.Descendants().Where(n2 => n2.Name == "a").First().InnerText,
                    EmissionDates = { ParseDate(node.Attributes["data-start"].Value) },
                    Rating = movieData?.Rating,
                    MovieType = movieData.MovieType,

                    
                }}
            ).ToList();

            Console.WriteLine("Hello World!");
        }
        private static DateTime ParseDate(string date)
        {
            var components = date.Split(',').Select(c=>int.Parse(c)).ToList();
            return new DateTime(components[0], components[1], components[2], components[3], components[4], 0);
        }

        //parse seancesinfo nodes
    }
}
