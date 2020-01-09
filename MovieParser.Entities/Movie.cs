namespace MovieParser.Entities
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int ImageUrl { get; set; }
    }

}
