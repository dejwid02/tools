using Microsoft.AspNetCore.Mvc;
using MoviesManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManagement.ViewComponents
{
    public class TvItemViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(TvItemViewModel model)
        {
            return await Task.FromResult(View(model));
        }
    }
}
