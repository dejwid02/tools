using MovieParser.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieParser.DAL
{
    public interface IMoviesRepository
    {
        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();
        Movie[] GetAllMovies();
        LogData GetLastLog();
        Channel[] GetAllChannels();
        TvListingItem[] GetAllTvListingItems();
        Movie GetMovieById(long id);
    }
}
