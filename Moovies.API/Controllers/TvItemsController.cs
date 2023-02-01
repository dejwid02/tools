using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieParser.DAL;
using Movies.Data;

namespace Movies.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TvItemsController : ControllerBase
    {
        private readonly ILogger<TvItemsController> _logger;
        private readonly IMoviesRepository repository;
        private readonly IMapper mapper;

        public TvItemsController(ILogger<TvItemsController> logger, IMoviesRepository repository, IMapper mapper)
        {
            _logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TvListingItem>> Get(bool hidePast = true, bool hideRecorded = false)
        {
            var tvItems = hidePast ? repository.GetAllTvListingItems().Where(t => t.StartTime > DateTime.Now) : repository.GetAllTvListingItems();
            if (hideRecorded)
            {
                var recordedIds = repository.GetAllRecordings().Where(r => r.RecordedAtTime <= DateTime.Now).Select(r => r.Movie.Id);
                tvItems = tvItems.Where(tv => !recordedIds.Contains(tv.Movie.Id));
            }
            return Ok(tvItems.OrderBy(t => t.StartTime).Select(i => mapper.Map<Data.TvListingItem>(i)));
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            var tviItem = repository.GetTvListingItem(id);
            if (tviItem == null)
                BadRequest($"Could not find item with id {id}");
            return Ok(mapper.Map<Data.TvListingItem>(tviItem));
        }
    }
}
