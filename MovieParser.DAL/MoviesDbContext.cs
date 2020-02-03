using Microsoft.EntityFrameworkCore;
using MovieParser.Entities;
using System;

namespace MovieParser.DAL
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions options) : base(options)
        {
                
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>().HasKey(x => new { x.ActorId, x.MovieId });
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<TvListingItem> TvListingItems { get; set; }
        public DbSet<MovieUserData> MoviesUserData { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<LogData> LogsData { get; set; }
        public DbSet<Recording> Recordings { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
    }
}
