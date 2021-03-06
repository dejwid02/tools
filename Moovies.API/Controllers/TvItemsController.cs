﻿using System;
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
        public ActionResult<IEnumerable<TvListingItem>> Get(bool hidePast = true)
        {
            var tvItems = hidePast ? repository.GetAllTvListingItems().Where(t => t.StartTime > DateTime.Now) : repository.GetAllTvListingItems() ;
            return Ok(tvItems.Select(i=>mapper.Map<Data.TvListingItem>(i)));
        }
    }
}
