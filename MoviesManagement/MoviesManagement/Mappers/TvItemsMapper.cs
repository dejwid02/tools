using Movies.Data;
using MoviesManagement.Dtos;
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

           var groups= tvItems.GroupBy(t => t.Movie.Id).ToList();
            return groups.Select(t => new TvItemViewModel
            {
                Movie = MapMovie(tvItems.FirstOrDefault(m=>m.Movie.Id==t.Key).Movie),
                Emissions = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(t.Select(t2 => new { key= $"{t2.Channel.Name} | {t2.StartTime.ToString("MMM-dd hh:mm")}", value=$"{t2.Id}" }).ToDictionary(i=>i.key, i=>i.value), "Value", "Key")
            }).ToList();
        }

        public RecordingItemViewModel MapRecording(TvListingItem tvItem)
        {

            return new RecordingItemViewModel
            {
                Movie = MapMovie(tvItem.Movie),
                RecordingTime = tvItem.StartTime,
                TvItemId = tvItem.Id
            };
        }

        public RecordingDto MapRecordingRequest(TvListingItem tvItem)
        {

            return new RecordingDto
            {
                Movie = new MovieDto { Id = tvItem.Movie.Id },
                RecordedAtTime = tvItem.StartTime
            };
        }

        public MovieViewModel MapMovie(Movie movie)
        {
            return new MovieViewModel
            {
                Id=movie.Id,
                Category = movie.Category,
                Country = movie.Country,
                Year = movie.Year,
                Description = movie.Description,
                Title = movie.Title,
                ImageUrl = movie.ImageUrl,
                Rating = movie.Rating
            };
        }

        public IList<RecordingItemViewModel> MapRecordingList(IEnumerable<Recording> recordings)
        {
           return recordings.Select(r => new RecordingItemViewModel
            {
                Movie = MapMovie(r.Movie),
                RecordingTime = r.RecordedAtTime,
                TvItemId = r.Id
            }).ToList();
        }
    }
}
