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
            var list = db.SliderItems.Where(s => s.SlideID == 1).OrderBy(s => s.SlideItemOrder).ToList();
            return PartialView(list);
        }
        public PartialViewResult SwipSlider()
        {
            return PartialView(db.Products.Where(p=>p.ProductIsFavourite==true).ToList());
        }
        public ActionResult ProductGroups()
        {
            var list = db.ProductGroups.Where(g => g.ProductParentGroupID == null);
            return PartialView(list);
        }

    }
}