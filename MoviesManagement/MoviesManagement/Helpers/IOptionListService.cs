using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesManagement.Helpers
{
    public interface IOptionListService
    {
        IList<SelectListItem> GetYears();
        IList<SelectListItem> GetCategories();
        IList<SelectListItem> GetCountries();
    }
}
