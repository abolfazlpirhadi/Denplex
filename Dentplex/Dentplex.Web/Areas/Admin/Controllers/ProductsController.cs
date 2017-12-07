using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dentplex.Data.Model;
using Dentplex.Web.Classes;
using System.IO;

namespace Dentplex.Web.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();

        [Authorize]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductGroup).Include(p => p.ProductGroup1);
            return View(products.ToList());
        }
        public JsonResult GetSubGroups(int id)
        {
            List<SelectListItem> list = db.ProductGroups.Where(g => g.ProductParentGroupID == id)
                    .Select(g => new SelectListItem()
                    {
                        Value = g.ProductGroupID.ToString(),
                        Text = g.ProductGroupTitle
                    }).ToList();

            return Json(new SelectList(list, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ProductGroupID = new SelectList(db.ProductGroups.Where(g => g.ProductParentGroupID == null), "ProductGroupID", "ProductGroupTitle");
            List<ProductGroup> list = new List<ProductGroup>()
            {
                new ProductGroup() { ProductGroupID = 0,ProductGroupTitle = " --- انتخاب کنید ---"}
            };
            ViewBag.ProductSubGroupID = new SelectList(list, "ProductGroupID", "ProductGroupTitle", 0);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductGroupID,ProductSubGroupID,ProductTitle,ProductShortText,ProductText,ProductImage,ProductIsFavourite")] Product product, HttpPostedFileBase imgProduct)
        {
            if (ModelState.IsValid)
            {
                product.ProductImage = "";

                if (imgProduct != null && imgProduct.IsImage())
                {
                    string mainImagePath = "/Images/ProductImages/MainImage/";
                    string thumbImagePath = "/Images/ProductImages/ThumbImage/";

                    if (!Directory.Exists(thumbImagePath))
                        Directory.CreateDirectory(Server.MapPath(thumbImagePath));

                    if (!Directory.Exists(mainImagePath))
                        Directory.CreateDirectory(Server.MapPath(mainImagePath));

                    string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgProduct.FileName);
                    product.ProductImage = imageName;
                    imgProduct.SaveAs(Server.MapPath(mainImagePath + imageName));

                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath(mainImagePath + imageName),
                        Server.MapPath(thumbImagePath + imageName));

                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ProductImage", "تصویر را انتخاب کنید!");
                    return View(imgProduct);
                }
            }

            ViewBag.ProductGroupID = new SelectList(db.ProductGroups, "ProductGroupID", "ProductGroupTitle", product.ProductGroupID);
            ViewBag.ProductSubGroupID = new SelectList(db.ProductGroups, "ProductGroupID", "ProductGroupTitle", product.ProductSubGroupID);
            return View(product);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductGroupID = new SelectList(db.ProductGroups.Where(g => g.ProductParentGroupID == null), "ProductGroupID", "ProductGroupTitle", product.ProductGroupID);
            ViewBag.ProductSubGroupID = new SelectList(db.ProductGroups.Where(g => g.ProductParentGroupID == product.ProductGroupID), "ProductGroupID", "ProductGroupTitle", product.ProductSubGroupID);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductGroupID,ProductSubGroupID,ProductTitle,ProductShortText,ProductText,ProductImage,ProductIsFavourite")] Product product, HttpPostedFileBase imgProduct)
        {
            if (ModelState.IsValid)
            {
                string mainImagePath = "/Images/ProductImages/MainImage/";
                string thumbImagePath = "/Images/ProductImages/ThumbImage/";

                if (imgProduct != null && imgProduct.IsImage())
                {
                    if (System.IO.File.Exists(Server.MapPath(mainImagePath + product.ProductImage)))
                        System.IO.File.Delete(Server.MapPath(mainImagePath + product.ProductImage));

                    if (System.IO.File.Exists(Server.MapPath(thumbImagePath + product.ProductImage)))
                        System.IO.File.Delete(Server.MapPath(thumbImagePath + product.ProductImage));

                    string imageName = Guid.NewGuid() + Path.GetExtension(imgProduct.FileName);
                    product.ProductImage = imageName;
                    imgProduct.SaveAs(Server.MapPath(mainImagePath + imageName));

                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath(mainImagePath + imageName),
                        Server.MapPath(thumbImagePath + imageName));
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductGroupID = new SelectList(db.ProductGroups, "ProductGroupID", "ProductGroupTitle", product.ProductGroupID);
            ViewBag.ProductSubGroupID = new SelectList(db.ProductGroups, "ProductGroupID", "ProductGroupTitle", product.ProductSubGroupID);
            return View(product);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
