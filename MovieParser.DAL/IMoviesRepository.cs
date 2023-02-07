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
        Movie GetMovie(long id);
        Movie[] GetAllMovies();
        Actor GetActorsByName(string firstName, string lastName);
        Director GetDirectorsByName(string firstName, string lastName);
        LogData GetLastLog();
        Channel[] GetAllChannels();
        Recording[] GetAllRecordings();
        Recording GetRecording(int id);
        Recording GetRecordingForMovie(long movieId);
        TvListingItem[] GetAllTvListingItems();
        TvListingItem GetTvListingItem(int id);
        MovieUserData[] GetAllMovieUserData();
        MovieUserData GetMovieUserData(int id);
    }
}
