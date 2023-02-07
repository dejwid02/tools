using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieParser.DAL;
using Serilog;

namespace Movies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logs/Stations.Api.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();

            builder.Services.AddControllers();
                builder.Services.AddAutoMapper(typeof(Program));
                builder.Services.AddDbContext<MoviesDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
                builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
                builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
                builder.Services.AddCors();
                builder.Services.AddHttpContextAccessor();
                builder.Services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.Authority = "https://login.microsoftonline.com/bc77db4f-ea2f-4910-b7af-f36a5a83b9e6/v2.0";
                        options.Audience = "api://7fceaf3e-f4b9-451c-bcd7-59053c3cea57";
                        options.TokenValidationParameters.ValidateIssuer = false;
                    });

                var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
                app.Run();
        }

    }
}
