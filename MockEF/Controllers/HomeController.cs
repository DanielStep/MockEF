using MockEF.Data;
using MockEF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockEF.Controllers
{
    public class HomeController : Controller
    {
        IService MockEFService;
        public HomeController(IService service)
        {
            MockEFService = service;
        }

        public ActionResult Index()
        {
            var results = MockEFService.List().ToList();
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
