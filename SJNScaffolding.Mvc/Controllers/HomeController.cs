using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SJNScaffolding.Core;
using SJNScaffolding.Helper;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.Models.TemplateModels;
using SJNScaffolding.Mvc.Models;

namespace SJNScaffolding.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AddNewBussinessHelper _bussinessHelper;
        public HomeController(AddNewBussinessHelper bussinessHelper)
        {
            _bussinessHelper = bussinessHelper;

        }

        public IActionResult Index()
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
