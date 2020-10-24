using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Services;

namespace MoviesManagement.Controllers
{
    public class RecordingsController : Controller
    {
        private readonly IApiClient apiClient;

        public RecordingsController(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }
        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            
            var form = HttpContext.Request.Form;
            var itemId = form.Where(f => f.Key == "Item").Select(i => i.Value).SelectMany(i2 => i2.ToArray()).Select(i => i.Split("|")).FirstOrDefault(i => i[0] == id.ToString())[1];
            var tvItem = await apiClient.Get<TvListingItem>($"api/tvitems/{itemId}");
            return View();
        }
    }
}