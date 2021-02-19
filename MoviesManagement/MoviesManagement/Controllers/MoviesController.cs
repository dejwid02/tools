using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using MoviesManagement.Dtos;
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
            var movies = await apiClient.GetAsync<IList<Movie>>(@"/api/movies");
            var model = movies.Select(mapper.MapMovie).ToList();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View("Create"));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await apiClient.GetAsync<Movie>(@$"/api/movies/{id}");
            var vm = mapper.MapMovieRequest(movie);
            vm.Id = movie.Id;
            return await Task.FromResult(View("Create", vm));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieViewModel model)
        {
            if (ModelState.IsValid)
            {

                var createdMovie = await apiClient.PostAsync<MovieDto, MovieDto>("api/movies", mapper.MapMovieRequest(model));
                if (createdMovie != null && model.IsRecorded)
                {
                    var createdRecording = apiClient.PostAsync<RecordingDto, RecordingDto>("api/recordings", new RecordingDto()
                    {
                        Movie = createdMovie,
                        RecordedAtTime = model.RecordingDate
                    });
                }
                return await Task.FromResult(RedirectToAction("Index"));
            }
            return await Task.FromResult(View(model));

        }

        public async Task<IActionResult> Save(CreateMovieViewModel vm)
        {
            var movie = await apiClient.GetAsync<Movie>($"api/Movies/{vm.Id}");
            movie.Title = vm.Title;
            movie.Rating = vm.Rating;
            movie.Category = vm.Category;
            movie.Description = vm.Description;
            movie.ImageUrl = vm.ImageFile;
            movie.Year = vm.Year;
            movie.Country = vm.Country;
            await apiClient.PutAsync($"api/Movies/{vm.Id}", movie);
            return await Task.FromResult(View("Index"));
        }
        public IActionResult VerifyImageFile(string imageFile)
        {
            if (imageFile.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || imageFile.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                return Json(true);
            return Json("Please select a valid jpeg file");
        }
    }
}
