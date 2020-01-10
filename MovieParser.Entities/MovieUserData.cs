namespace MovieParser.Entities
{
    public class MovieUserData
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public bool IsRecorded { get; set; }
        public bool DontShow { get; set; }
        public TvListingItem TvListingItem { get; set; }
    }

}
