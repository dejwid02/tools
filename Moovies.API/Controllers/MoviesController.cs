using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MovieParser.DAL;

namespace Movies.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository repository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly IAuthorizationService _authorizationService;

        public MoviesController(IMoviesRepository repository, IMapper mapper, LinkGenerator linkGenerator, IAuthorizationService authorizationService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            _authorizationService = authorizationService;
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
            if (!_authorizationService.IsAdmin())
                return Unauthorized();
            try
            {
                var existing = repository.GetMovie(id);
                if(existing==null)
                {
                    return BadRequest($"Can not find movie with id {id}");
                }
                mapper.Map(movie, existing);
                repository.SaveChanges();
                return Ok(mapper.Map<Data.Movie>(existing));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating data in db");
            }
           
        }
        [HttpPost]
        public ActionResult<Data.Movie> Create(Data.Movie movie)
        {
            if (!_authorizationService.IsAdmin())
                return Unauthorized();
            if (repository.GetMovie(movie.Id)!= null)
            {
                return BadRequest("Movie with this id already exists");
            }
            var movieEntity = mapper.Map<MovieParser.Entities.Movie>(movie);
            repository.Add(movieEntity);
            repository.SaveChanges();
            var url = linkGenerator.GetPathByAction("Get", "Movies", new { id = movieEntity.Id });
            if(url==null)
            {
                return BadRequest("Can not use this id");
            }
            return Created(url, mapper.Map<Data.Movie>(movieEntity));
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            if (!_authorizationService.IsAdmin())
                return Unauthorized();

            var recordings = repository.GetAllRecordings().Where(r => r.Movie.Id == id);
            foreach (var item in recordings)
            {
                repository.Delete(item);
            }

            repository.SaveChanges();
            return Ok();
        }
    }
}
