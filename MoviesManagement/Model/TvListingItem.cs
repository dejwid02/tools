using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Data
{
    public class TvListingItem
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public Channel Channel { get; set; }
        public DateTime StartTime { get; set; }
    }
}
