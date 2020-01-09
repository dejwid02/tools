namespace MovieParser.Entities
{
    public class MovieUserData
    {
        public int Id { get; set; }
        public long MovieId { get; set; }
        public bool IsRecorded { get; set; }
        public bool DontShow { get; set; }
        public int TvListingItemId { get; set; }
    }

}
