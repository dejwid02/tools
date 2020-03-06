using System.Collections.Generic;

namespace MovieParser.Entities
{
    public class Movie
    {
        public long Id { get; set; }
        public Director Director { get; set; } = new Director();
        public IList<Actor> Actors { get; set; } = new List<Actor>();
        public string Title { get; set; }
        public double? Rating { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public string ImageUrl { get; set; }
        public double Duration { get; set; }
        public int AgeRating { get; set; }
        public string Country { get; set; }
    }
}
