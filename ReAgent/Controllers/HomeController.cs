using ReAgent.Services.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReAgent.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IAccountServ accountServ) : base(accountServ){ }

        public ActionResult Index()
        {
            if (TempData.ContainsKey("Message"))
                ViewBag.Message = TempData["Message"];

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Service(string id = null)
        {
            if (id != null)
                return View("Services/" + id);
            else
                return View();
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Requisites()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}
