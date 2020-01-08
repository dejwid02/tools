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
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<MovieEntity> Movies { get; set; }
    }
}
