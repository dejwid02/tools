using System;
using System.Collections.Generic;
using System.Text;

namespace MovieParser
{
    public class TvScheduleItem
    {
        public Movie Movie { get; set; }
        public DateTime StartTime { get; set; }
        public Channel Channel { get; internal set; }
    }
}
