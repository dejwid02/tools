using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieParser.DAL;
using ParserRunner;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ParserRunner
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
            var configuration= builder.GetContext().Configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<MoviesDbContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
        }
    }
}
