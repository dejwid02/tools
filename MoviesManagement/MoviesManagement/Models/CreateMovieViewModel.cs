using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManagement.Models
{
    public class CreateMovieViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public bool IsRecorded { get; set; }
        public DateTime RecordingDate { get; set; }
        public string ImageFile { get; set; }

    }
}
