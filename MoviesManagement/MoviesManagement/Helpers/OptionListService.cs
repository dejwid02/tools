using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace MoviesManagement.Helpers
{
    public class OptionListService : IOptionListService
    {
        public IList<SelectListItem> GetYears()
        {
            var values =  Enumerable.Range(1950, 72).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() }).ToList();
            values.Last().Selected = true;
            return values;
        }
    }
}
