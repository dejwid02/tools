using System;

namespace MovieParser.Entities
{
    public class LogData
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string ErrorMessage { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime LastSynchronizedDate { get; set; }
        public int NoOfMoviesCreated { get; set; }
        public int NoOfTvListingItemsCreated { get; set; }
    }

}
