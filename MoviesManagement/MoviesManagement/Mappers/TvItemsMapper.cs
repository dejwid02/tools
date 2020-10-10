using Movies.Data;
using MoviesManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesManagement.Mappers
{
    public class TvItemsMapper : ITvItemsMapper
    {
        public IList<TvItemViewModel> Map(IList<TvListingItem> tvItems)
        {
           var groups= tvItems.GroupBy(t => t.Movie);
            return groups.Select(t => new TvItemViewModel
            {
                Movie = MapMovie(t.Key),
                Emissions = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(t.Select(t2 => $"{t2.Channel.Name} | {t2.StartTime.ToString("mmm-dd")}"))
            }).ToList();
        }

        private static MovieViewModel MapMovie(Movie movie)
        {
            return new MovieViewModel
            {
                Category = movie.Category,
                Description = movie.Description,
                Title = movie.Title,
                ImageUrl = movie.ImageUrl
            };
        }
    }
}
