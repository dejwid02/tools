using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using MovieParser.DAL;
using Movies.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordingsController : Controller
    {
        private readonly ILogger<TvItemsController> logger;
        private readonly IMoviesRepository repository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public RecordingsController(ILogger<TvItemsController> logger, IMoviesRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<Recording>> Get()
        {
            return Ok(repository.GetAllRecordings().Select(i=>mapper.Map<Data.Recording>(i)));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Data.Recording> Get(int id)
        {
            try
            {
                var recordingEntity = repository.GetRecording(id);
                return Ok(mapper.Map<Data.Recording>(recordingEntity));
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<Data.Recording> Post(Data.Recording recording)
        {
            try
            {
                var mappedRecording = mapper.Map<MovieParser.Entities.Recording>(recording);
                if (recording.Movie == null)
                {
                    return BadRequest("Movie not specified");
                }
                var movie = repository.GetMovie(recording.Movie.Id);
                if (movie == null)
                {
                    return BadRequest("Movie can not be found");
                }

                mappedRecording.Movie = movie;
                repository.Add(mappedRecording);
                repository.SaveChanges();
                var uri = linkGenerator.GetPathByAction("Get", "Recordings", new { id = mappedRecording.Id });
                if (string.IsNullOrEmpty(uri))
                {
                    return BadRequest("Can not use id");
                }
                return Created(uri, mapper.Map<Data.Recording>(mappedRecording));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Recording recording)
        {
            try
            {
                var oldRecording = repository.GetRecording(id);
            if (oldRecording == null) return NotFound($"Could not get recording with id {id}");

            mapper.Map(recording, oldRecording);
            if (recording.Movie!=null)
            {
                    var movie = repository.GetMovie(recording.Movie.Id);
                    if(movie!=null)
                    {
                        oldRecording.Movie = movie;
                    }
                
            }
                repository.SaveChanges();
                return Ok(mapper.Map<Data.Recording>(oldRecording));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var oldRecording = repository.GetRecording(id);
            if (oldRecording == null) return NotFound($"Could not get recording with id {id}");

            try
            {
                repository.Delete(oldRecording);
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
