using MojaBanka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojaBanka.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext db = new MyDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Baza = db.Klijenti.ToList();
            return View();
        }
    }
}