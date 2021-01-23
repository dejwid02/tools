using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using MoviesManagement.Mappers;
using MoviesManagement.Models;
using Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesManagement.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IApiClient apiClient;
        private readonly ITvItemsMapper mapper;

        public MoviesController(IApiClient apiClient, ITvItemsMapper mapper)
        {
            this.apiClient = apiClient;
            this.mapper = mapper;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var movies = await apiClient.Get<IList<Movie>>(@"/api/movies");
            var model = movies.Select(mapper.MapMovie).ToList();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View("Create"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieViewModel model)
        {
            var path = $"{@"/images/"}{model.ImageFile}";
            
            return await Task.FromResult(RedirectToAction("Index"));
        }
    }
}
