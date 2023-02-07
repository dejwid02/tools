using Microsoft.EntityFrameworkCore;
using MovieParser.Entities;
using System.Linq;

namespace MovieParser.DAL
{
    public class MoviesRepository : IMoviesRepository
    {
        MoviesDbContext _context;

        public MoviesRepository(MoviesDbContext dbContext)
        {
            _context = dbContext;
        }
        void IMoviesRepository.Add<T>(T entity)
        {
            _context.Add(entity);
        }

        void IMoviesRepository.Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        bool IMoviesRepository.SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        LogData IMoviesRepository.GetLastLog()
        {
            return _context.LogsData.OrderByDescending(l => l.LastSynchronizedDate).FirstOrDefault();
        }

        Director IMoviesRepository.GetDirectorsByName(string firstName, string lastName)
        {
            return _context.Directors.FirstOrDefault(d => d.FirstName == firstName && d.LastName == lastName);
        }

        Actor IMoviesRepository.GetActorsByName(string firstName, string lastName)
        {
            return _context.Actors.FirstOrDefault(d => d.FirstName == firstName && d.LastName == lastName);
        }

        Movie IMoviesRepository.GetMovieByYearAndTitle(int? year, string title)
        {
                return _context.Movies.FirstOrDefault(m => m.Year == year && m.Title==title);

        }

        Channel[] IMoviesRepository.GetAllChannels()
        {
            return _context.Channels.ToArray();
        }

        public Recording GetRecordingForMovie(long movieId)
        {
            return _context.Recordings.FirstOrDefault(r => r.Movie.Id == movieId);
        }

        TvListingItem[] IMoviesRepository.GetAllTvListingItems()
        {
            return _context.TvListingItems
                .Include(c => c.Movie)
                .Include(c => c.Channel)
                .ToArray();
        }

        public Recording[] GetAllRecordings()
        {
            return _context.Recordings
                .Include(c=>c.Movie).ToArray();
        }

        public Recording GetRecording(int id)
        {
            return _context.Recordings.SingleOrDefault(r => r.Id == id);
        }

        public TvListingItem GetTvListingItem(int id)
        {
            return _context.TvListingItems.Where(tv => tv.Id == id)
                .Include(c=>c.Movie)
                .Include(c=>c.Channel).SingleOrDefault();
        }

        public Movie GetMovie(long id)
        {
            return _context.Movies.SingleOrDefault(m => m.Id == id);
        }

        public MovieUserData[] GetAllMovieUserData()
        {
            return _context.MoviesUserData.ToArray();
        }

        public MovieUserData GetMovieUserData(int id)
        {
            return _context.MoviesUserData.SingleOrDefault(m => m.Id == id);
        }

        public Movie[] GetAllMovies()
        {
            return _context.Movies.ToArray();
        }
    }
}
