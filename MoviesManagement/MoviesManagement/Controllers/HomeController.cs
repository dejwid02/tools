using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Movies.Data;
using MoviesManagement.Mappers;
using MoviesManagement.Models;
using Services;

namespace MoviesManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiClient apiClient;
        private readonly ITvItemsMapper mapper;

        public HomeController(ILogger<HomeController> logger, IApiClient apiClient, ITvItemsMapper mapper)
        {
            _logger = logger;
            this.apiClient = apiClient;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {

           var items = await apiClient.Get<IList<TvListingItem>>("api/tvitems?hidepast=false");
            var vm = mapper.Map(items);
            return View(new TvItemsViewModel { TvItems = vm });
        }
        [HttpPost]
        public async Task<IActionResult> Record(TvItemsViewModel vm)
        {
            return View();
        }

        public async Task<ActionResult> Movie(int id)
        {
            var movie = await apiClient.Get<Movie>($"api/movies/{id}");

            return View(mapper.MapMovie(movie));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
