using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingAuth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpClientCertificate cs = Request.ClientCertificate;
            var result = cs.Subject;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}