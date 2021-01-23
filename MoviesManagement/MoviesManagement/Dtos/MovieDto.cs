using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManagement.Dtos
{
    public class MovieDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public double? Rating { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public string ImageUrl { get; set; }
        public string Country { get; set; }
    }
}
