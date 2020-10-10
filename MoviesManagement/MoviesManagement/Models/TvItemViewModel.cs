using System.Collections.Generic;

namespace MoviesManagement.Models
{
    public class TvItemViewModel
    {
        public MovieViewModel Movie { get; set; }
        public IList<EmissionViewModel> Emissions { get; set; }

    }
}