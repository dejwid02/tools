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
                Emissions = t.Select(t2 => new EmissionViewModel
                {
                    ChannelName = t2.Channel.Name,
                    EmissionTime = t2.StartTime
                }).ToList()
            }).ToList();
        }

        private static MovieViewModel MapMovie(Movie movie)
        {
            return new MovieViewModel
            {
                Category = movie.Category,
                Title = movie.Title,
                ImageUrl = movie.ImageUrl
            };
        }
    }
}
