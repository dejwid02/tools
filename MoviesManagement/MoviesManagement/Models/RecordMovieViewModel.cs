using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManagement.Models
{
    public class RecordMovieViewModel
    {
        public string MovieTitle { get; set; }
        public DateTime RecordedAt { get; set; }
        public long Id { get; set; }
    }
}
