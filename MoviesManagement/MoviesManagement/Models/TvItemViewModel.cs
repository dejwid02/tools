using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MoviesManagement.Models
{
    public class TvItemViewModel
    {
        public MovieViewModel Movie { get; set; }
        public SelectList Emissions { get; set; }

    }
}