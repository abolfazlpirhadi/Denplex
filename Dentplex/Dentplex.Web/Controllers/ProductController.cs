using Dentplex.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dentplex.Web.Controllers
{
    public class ProductController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();

        [Route("Product/{id}")]
        public ActionResult Product(int? id)
        {
            if (id != -1)
            {
                var product = db.Products.Where(p => p.ProductGroupID == id);
                return View(product);
            }
            else
            {
                var product = db.Products.ToList();
                return View(product);
            }
        }
        public ActionResult ProductBanner(int? id)
        {
            
            if (id != null)
            {
                var productgroup = db.ProductGroups.Find(id);
                return PartialView(productgroup);
            }
            else
            {
                var productgroup = db.ProductGroups;
                return PartialView(productgroup);
            }
        }

        [Route("Product/{id}")]
        [HttpPost]
        public ActionResult AjGroupItemShow(int? id , List<string> gIds)
        {
            string combindedString = string.Join(",", gIds.ToArray());
            int fid;
            //for try parse using from "Select"

            if (combindedString == "")
            {
                var fildes = db.Products.Where(p => p.ProductGroupID == id);

                return PartialView("_ProductListSubGroups", fildes);
            }
            else
            {
                var fIdLists = combindedString.Split(',').Select(p => Int32.TryParse(p, out fid) ? Int32.Parse(p) : -1).ToList();

                //if (fIdLists.Count < 30)
                //{
                var fildes = (from u in db.Products
                              join up in fIdLists on u.ProductSubGroupID equals up
                              select u).ToList();

                return PartialView("_ProductListSubGroups", fildes);
            }
            

            
            //JsonResult
            //return Json(person);
        }

        public ActionResult _ProductsubGroups(int id)
        {
            var productSubgroup = db.ProductGroups.Where(v => v.ProductParentGroupID == id);
            return PartialView(productSubgroup);
        }

        
        public PartialViewResult ProductGroups()
        {
            return PartialView(db.ProductGroups.Where(p => p.ProductParentGroupID == null));
        }
        [Route("ShowProduct/{id}")]
        public ActionResult ShowProduct(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }
        public PartialViewResult RelatedProducts(int id)
        {
            var product = db.Products.Find(id);
            var listProductRelated = db.Products.Where(p => p.ProductSubGroupID == product.ProductSubGroupID && p.ProductID != id).ToList();

            return PartialView(listProductRelated);
        }
    }
}