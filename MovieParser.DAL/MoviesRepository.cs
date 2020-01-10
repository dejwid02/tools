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

        Movie[] IMoviesRepository.GetAllMovies()
        {
            return _context.Movies.ToArray();          
        }

        Channel[] IMoviesRepository.GetAllChannels()
        {
            return _context.Channels.ToArray();
        }

        TvListingItem[] IMoviesRepository.GetAllTvListingItems()
        {
            return _context.TvListingItems
                .Include(c => c.Movie)
                .Include(c=>c.Channel)
                .ToArray();
        }
    }
}
