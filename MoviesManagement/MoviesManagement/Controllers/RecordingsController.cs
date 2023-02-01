using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using MoviesManagement.Dtos;
using MoviesManagement.Mappers;
using MoviesManagement.Models;

namespace MoviesManagement.Controllers
{
    public class RecordingsController : Controller
    {
        private readonly IApiClient apiClient;
        private readonly ITvItemsMapper mapper;

        public RecordingsController(IApiClient apiClient, ITvItemsMapper mapper)
        {
            this.apiClient = apiClient;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Index(int item)
        {
            var tvItem = await apiClient.GetAsync<TvListingItem>($"api/tvitems/{item}");
            var vm = mapper.MapRecording(tvItem);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Record(int id)
        {  
            var tvItem = await apiClient.GetAsync<TvListingItem>($"api/tvitems/{id}");
            var request = mapper.MapRecordingRequest(tvItem);
            RecordingDto result = await apiClient.PostAsync<RecordingDto, RecordingDto>("api/recordings", request);
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await apiClient.Delete($"api/Recordings/{id}");
            return RedirectToAction("List");
        }

        public async Task<IActionResult> List()
        {
            var recordings = await apiClient.GetAsync<IEnumerable<Recording>>("api/recordings");

            return View("List", mapper.MapRecordingList(recordings));
        }
    }
}