using System;
using System.Collections.Generic;
using System.Text;

namespace MovieParser
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public double Rating { get; set; }
        public List<DateTime> EmissionDates { get; set; } = new List<DateTime>();
    }
}
