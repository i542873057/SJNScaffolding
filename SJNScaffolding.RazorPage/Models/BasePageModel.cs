using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace SJNScaffolding.RazorPage.Models
{
    public class BasePageModel: PageModel
    {
        public IActionResult Json(object o)
        {
            return Content(JsonConvert.SerializeObject(o));
        }

        public IActionResult Success(string msg)
        {
            return Json(new LayuiResultDto(msg));
        }
    }
}
