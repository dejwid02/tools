namespace MoviesManagement.Models
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public string Description { get; internal set; }
        public double? Rating { get; internal set; }
        public long Id { get;  set; }
        public string Country { get; internal set; }
        public int? Year { get; internal set; }
    }
}