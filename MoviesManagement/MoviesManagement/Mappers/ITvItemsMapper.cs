using Movies.Data;
using MoviesManagement.Dtos;
using MoviesManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManagement.Mappers
{
    public interface ITvItemsMapper
    {
        IList<TvItemViewModel> Map(IList<TvListingItem> tvItems);
        RecordingItemViewModel MapRecording(TvListingItem tvItem);
        IList<RecordingItemViewModel> MapRecordingList(IEnumerable<Recording> recordings);
        RecordingDto MapRecordingRequest(TvListingItem tvItem);
        RecordingDto MapRecordingRequest(long movieId, DateTime recordingDate);
        MovieViewModel MapMovie(Movie movie);
        MovieDto MapMovieRequest(CreateMovieViewModel movieViewModel);
        CreateMovieViewModel MapMovieRequest(Movie movie);
        EditMovieViewModel MapEditMovieRequest(Movie movie);
        RecordMovieViewModel MapRecordMovieRequest(Movie movie);
    }
}
