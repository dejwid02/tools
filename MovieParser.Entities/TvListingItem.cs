using System;

namespace MovieParser.Entities
{
    public class TvListingItem
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime StartTime { get; set; }
        public Channel Channel { get; set; }
    }

}
