using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieParser.DAL;
using Movies.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    public class RecordingsController : Controller
    {
        private readonly ILogger<MoviesController> logger;
        private readonly IMoviesRepository repository;
        private readonly IMapper mapper;

        public RecordingsController(ILogger<MoviesController> logger, IMoviesRepository repository, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<Recording>> Get()
        {
            return Ok(repository.GetAllRecordings().Select(i=>mapper.Map<Data.Recording>(i)));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
