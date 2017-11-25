using Dentplex.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Dentplex.Web.Controllers
{
    public class HomeController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult TopSlider()
        {
            return PartialView(db.SliderItems.Where(s => s.SlideID == 1).OrderBy(s => s.SlideItemOrder).ToList());
        }
        public PartialViewResult SwipSlider()
        {
            return PartialView(db.SliderItems.Where(s => s.SlideID == 3).OrderBy(s=>s.SlideItemOrder).ToList());
        }

    }
}