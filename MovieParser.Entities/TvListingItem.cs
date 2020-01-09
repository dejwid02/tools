using System;

namespace MovieParser.Entities
{
    public class TvListingItem
    {
        public int Id { get; set; }
        public long MovieID { get; set; }
        public DateTime StartTime { get; set; }
        public int ChannelId { get; set; }
    }

}
