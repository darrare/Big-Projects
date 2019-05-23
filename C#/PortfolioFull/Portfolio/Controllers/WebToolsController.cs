using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    public class WebToolsController : Controller
    {
        [Route("WebTools")]
        [Route("WebTools/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("WebTools/Calculator")]
        public IActionResult Calculator()
        {
            return View();
        }

        [Route("WebTools/ReadFast")]
        public IActionResult ReadFast()
        {
            return View();
        }

        [Route("WebTools/RecursiveTree")]
        public IActionResult RecursiveTree()
        {
            return View();
        }

        [Route("WebTools/FlockingSimulation")]
        public IActionResult FlockingSimulation()
        {
            return View();
        }

        [Route("WebTools/SecurityCams")]
        public IActionResult SecurityCams()
        {
            return View();
        }
    }
}