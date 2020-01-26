using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Data
{
    public class Recording
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime RecordedAtTime { get; set; }
    }
}
