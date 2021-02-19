using Microsoft.AspNetCore.Mvc;
using MoviesManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManagement.ViewComponents
{
    public class MovieViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(MovieViewModel model)
        {
            return await Task.FromResult(View(model));
        }
        public async Task<IViewComponentResult> Kupa(MovieViewModel model)
        {
            return await Task.FromResult(View((model)));
        }
    }
}
