using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenvilleFarmsProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string url = "https://maps.googleapis.com/maps/api/js?key=" + System.Web.Configuration.WebConfigurationManager.AppSettings["googleAPIkey"];
            url += "&callback=initMap";
            ViewBag.googleAPI = url;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}