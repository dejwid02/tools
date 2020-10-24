using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManagement.Models
{
    public class RecordingItemViewModel
    {
        public int TvItemId { get; set; }
        public MovieViewModel Movie { get; set; }
        public DateTime RecordingTime { get; set; }

    }
}
