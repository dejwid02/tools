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
        Movie GetMovieByYearAndTitle(int? year, string title);
        Actor GetActorsByName(string firstName, string lastName);
        Director GetDirectorsByName(string firstName, string lastName);
        LogData GetLastLog();
        Channel[] GetAllChannels();
        Recording[] GetAllRecordings();
        Recording GetRecording(int id);
        TvListingItem[] GetAllTvListingItems();
        TvListingItem GetTvListingItem(int id);
    }
}
