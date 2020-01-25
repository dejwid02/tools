using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieParser.DAL;
using Movies.Data;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MoviesController> _logger;
        private readonly IMoviesRepository repository;
        private readonly IMapper mapper;

        public MoviesController(ILogger<MoviesController> logger, IMoviesRepository repository, IMapper mapper)
        {
            _logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetAll()
        {
            var tvItems = repository.GetAllTvListingItems().Where(t => t.StartTime > DateTime.Now) ;
            return Ok(tvItems.Select(i=>mapper.Map<Data.TvListingItem>(i)));
        }
    }
}
