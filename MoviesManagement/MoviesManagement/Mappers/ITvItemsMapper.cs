using Movies.Data;
using MoviesManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManagement.Mappers
{
    public interface ITvItemsMapper
    {
        IList<TvItemViewModel> Map(IList<TvListingItem> tvItems);
    }
}
