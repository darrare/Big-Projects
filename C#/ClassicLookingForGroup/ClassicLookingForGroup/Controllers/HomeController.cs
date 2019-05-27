using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ClassicLookingForGroup.Hubs;
using System.Dynamic;
using ClassicLookingForGroup.Models;
using ClassicLookingForGroup.RealTimeBackend;

namespace ClassicLookingForGroup.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Magtheridon()
        {
            return View();
        }

        public IActionResult Sargeras()
        {
            return View();
        }

        public IActionResult Area52()
        {
            return View();
        }

        public IActionResult LFD()
        {
            return View();
        }
    }
}