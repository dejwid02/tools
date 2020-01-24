using Microsoft.EntityFrameworkCore;
using MovieParser.Entities;
using System;

namespace MovieParser.DAL
{
    public class MoviesDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = Movies; Trusted_Connection = True; "); //ToDo load me from configuration
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<TvListingItem> TvListingItems { get; set; }
        public DbSet<MovieUserData> MoviesUserData { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<LogData> LogsData { get; set; }
    }
}
