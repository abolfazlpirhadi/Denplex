using Dentplex.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dentplex.Web.Controllers
{
    public class PagesController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();

        public ActionResult ContactUs()
        {
            var page = db.Pages.Where(p => p.PageName == "ContactUs").SingleOrDefault();
            return View(page);
        }
        public ActionResult AboutUs()
        {
            var page = db.Pages.Where(p => p.PageName == "AboutUs").SingleOrDefault();
            return View(page);
        }
    }
}