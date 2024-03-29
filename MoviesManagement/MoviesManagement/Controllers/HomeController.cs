﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Movies.Data;
using MoviesManagement.Mappers;
using MoviesManagement.Models;

namespace MoviesManagement.Controllers
{
    [Authorize]
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

           var items = await apiClient.GetAsync<IList<TvListingItem>>("api/tvitems?hidepast=true");
            var vm = mapper.Map(items);
            return View(new TvItemsViewModel { TvItems = vm });
        }
        [HttpPost]
        public async Task<IActionResult> Record(TvItemsViewModel vm)
        {
            return View();
        }

     
        public async Task LogOff()
        {
            await HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext?.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        
        }

        public async Task<ActionResult> Movie(int id)
        {
            var movie = await apiClient.GetAsync<Movie>($"api/movies/{id}");

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
