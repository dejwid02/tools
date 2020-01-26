using System;
using System.Collections.Generic;
using System.Text;

namespace MovieParser.Entities
{
    public class Recording
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime RecordedAtTime { get; set; }
    }
}
