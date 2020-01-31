using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MovieParser.DAL;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieUserDatasController : ControllerBase
    {
        private readonly IMoviesRepository repository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public MovieUserDatasController(IMoviesRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }
        // GET: api/MovieUSerDatas
        [HttpGet]
        public ActionResult<IEnumerable<Data.MovieUserData>> Get()
        {
            try
            {
                var userData = repository.GetAllMovieUserData();
                return Ok(mapper.Map<Data.MovieUserData>(userData));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error while getting from db");
            }
        }

        // GET: api/MovieUSerDatas/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Data.MovieUserData> Get(int id)
        {
            try
            {
                var userData = repository.GetMovieUserData(id);
                if (userData == null)
                {
                    return BadRequest("Can not find movie user data");
                }
                return Ok(mapper.Map<Data.MovieUserData>(userData));
                
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error while getting from db");
            }
        }

        // POST: api/MovieUSerDatas
        [HttpPost]
        public ActionResult<Data.MovieUserData> Post([FromBody] Data.MovieUserData movieUserData)
        {
            try
            {
                var mapped = mapper.Map<MovieParser.Entities.MovieUserData>(movieUserData);
                if(movieUserData.Movie==null)
                {
                    return BadRequest("Movie not specified");
                }
                var movie = repository.GetMovie(movieUserData.Movie.Id);
                if (movie == null)
                {
                    return BadRequest("Movie can not be found");
                }
                mapped.Movie = movie;
                repository.Add(mapped);
                repository.SaveChanges();
                var link = linkGenerator.GetPathByAction("Get", "MovieUserDatas", new { id = mapped.Id });
                return Created(link, mapper.Map<Data.MovieUserData>(mapped));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error while saving data to db");
            }
        }

        // PUT: api/MovieUSerDatas/5
        [HttpPut("{id}")]
        public ActionResult<Data.MovieUserData> Put(int id, [FromBody] Data.MovieUserData value)
        {
            try
            {
                var existing = repository.GetMovieUserData(id);

                if (existing == null)
                {
                    return BadRequest("Can not find movie user data");
                }
                mapper.Map(value, existing);
                if (value.Movie == null)
                {
                    return BadRequest("Movie not specified");
                }
                var movie = repository.GetMovie(value.Movie.Id);
                if (movie == null)
                {
                    return BadRequest("Movie not found");
                }
                existing.Movie = movie;
                repository.SaveChanges();
                return Ok(mapper.Map<Data.MovieUserData>(existing));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error while saving data to db");
            }
           
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var existing = repository.GetMovieUserData(id);

                if (existing == null)
                {
                    return BadRequest("Can not find movie user data");
                }
                repository.Delete(existing);
                repository.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error while saving data to db");
            }
        }
    }
}
