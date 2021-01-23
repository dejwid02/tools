using Movies.Data;
using MoviesManagement.Dtos;
using MoviesManagement.Models;
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
        MovieViewModel MapMovie(Movie movie);
        MovieDto MapMovieRequest(CreateMovieViewModel movieViewModel);
    }
}
