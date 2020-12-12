using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OpdrachtScenius.Controllers
{
    public class AngularController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}