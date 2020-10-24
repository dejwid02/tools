using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManagement.Dtos
{
    public class RecordingDto
    {
        public int Id { get; set; }
        public MovieDto Movie { get; set; }
        public DateTime RecordedAtTime { get; set; }
    }
}
