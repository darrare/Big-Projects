using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Portfolio.Hubs;

namespace Portfolio.Controllers
{
    public class GroupFinderController : Controller
    {
        private readonly IHubContext<CharacterRegistrationHub> HubContext;
        public GroupFinderController(IHubContext<CharacterRegistrationHub> hubContext)
        {
            HubContext = hubContext;
        }

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

        //https://docs.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-2.2
        public ActionResult Register(Portfolio.Models.CharacterRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Register user in backend
                
            }
            return View("Area52", model);
        }
    }
}