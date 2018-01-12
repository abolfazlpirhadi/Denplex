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

        [Route("Product")]
        public ActionResult Product()
        {
            ViewBag.AllProduct = "ProductBannerAll";
            return View();
        }
        public PartialViewResult ProductBannerAll()
        {
            return PartialView();
        }
        public PartialViewResult ProductGroups()
        {
            return PartialView(db.ProductGroups.Where(p => p.ProductParentGroupID == null));
        }
        public PartialViewResult ProductAll()
        {
            return PartialView(db.ProductGroups.ToList());
        }
        //public ActionResult AjGroupItemShow()
        //{
        //    var fildes = db.Products;
        //    return PartialView("_ProductListSubGroups", fildes);
        //}

        //***************************************




        [Route("Product/{id}")]
        public ActionResult Product(int? id)
        {
            return View();
        }
        public ActionResult ProductBanner(int? id)
        {
            var productgroup = new ProductGroup();
            if (id != null)
                 productgroup = db.ProductGroups.Find(id);
            
            return PartialView(productgroup);
        }

        public ActionResult ProductShowList(int? id)
        {
            var productList = new List<Product>();
            if (id != null)
            {
                productList = db.Products.Where(v => v.ProductGroupID == id).OrderByDescending(d => d.ProductDateCreate).ToList();
            }
            else
            {
                //Limit 200 products for quick load page
                productList = db.Products.Take(200).OrderByDescending(d => d.ProductDateCreate).ToList();
            }
            

            return PartialView("ProductList", productList);

        }

        //[Route("Product/{id}")]
        //[HttpPost]
        //public ActionResult AjGroupItemShow(int? id, List<string> gIds)
        //{
        //    string combindedString = string.Join(",", gIds.ToArray());
        //    int fid;
        //    //for try parse using from "Select"

        //    if (combindedString == "")
        //    {
        //        var fildes = db.Products.Where(p => p.ProductGroupID == id);

        //        return PartialView("_ProductListSubGroups", fildes);
        //    }
        //    else
        //    {
        //        var fIdLists = combindedString.Split(',').Select(p => Int32.TryParse(p, out fid) ? Int32.Parse(p) : -1).ToList();

        //        //if (fIdLists.Count < 30)
        //        //{
        //        var fildes = (from u in db.Products
        //                      join up in fIdLists on u.ProductSubGroupID equals up
        //                      select u).ToList();

        //        return PartialView("_ProductListSubGroups", fildes);
        //    }



        //    //JsonResult
        //    //return Json(person);
        //}

        //[Route("Product/{id}")]
        ////[HttpPost]
        //public ActionResult AjGroupItemShow(int? id)
        //{
        //        var fildes = db.Products.Where(p => p.ProductGroupID == id);
        //        return PartialView("_ProductListSubGroups", fildes);
        //}

        public ActionResult _ProductsubGroups(int? id)
        {
            var productSubgroup = new List<ProductGroup>();
            if (id != null)
              productSubgroup = db.ProductGroups.Where(v => v.ProductParentGroupID == id).ToList();

            return PartialView("_ProductsubGroups",productSubgroup);
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
            var listProductRelated = db.Products.Where(p => p.ProductSubGroupID == product.ProductSubGroupID && p.ProductID != id);

            return PartialView(listProductRelated);
        }
    }
}