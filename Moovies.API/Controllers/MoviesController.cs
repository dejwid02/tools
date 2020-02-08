using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieParser.DAL;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository repository;
        private readonly IMapper mapper;

        public MoviesController(IMoviesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        // GET: api/Movies
        [HttpGet]
        public ActionResult<IEnumerable<Data.Movie>> Get()
        {
            try
            {
                var movies = repository.GetAllMovies();
                var mapped = movies.Select(m => mapper.Map<Data.Movie>(m)).ToArray();
                return Ok(mapped);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error while getting data from db");
            }
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public ActionResult<Data.Movie> Get(long id)
        {
            try
            {
                var movie = repository.GetMovie(id);
                if (movie==null)
                {
                    return BadRequest("Can not find movie");
                }

                return Ok(mapper.Map<Data.Movie>(movie));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error while getting data from db");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Data.Movie> Update(long id, Data.Movie movie)
        {
            try
            {
                var existing = repository.GetMovie(id);
                if(existing==null)
                {
                    return BadRequest($"Can not find movie with id {id}");
                }
                mapper.Map(movie, existing);

                return Ok(mapper.Map<Data.Movie>(existing));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating data in db");
            }
           
        }


    }
}
