using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MoviesManagement.Controllers
{
    public class RecordingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}