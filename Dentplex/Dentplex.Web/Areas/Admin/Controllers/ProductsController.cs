using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dentplex.Data.Model;
using System.IO;
using Dentplex.Web.Classes;

namespace Dentplex.Web.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {

        private DentplexDBEntities db = new DentplexDBEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }


        // GET: Admin/Products/Details/5
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

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductGroupID,ProductSubGroupID,ProductTitle,ProductShortText,ProductText,ProductImage")] Product product, HttpPostedFileBase imgProduct)
        {
            if (ModelState.IsValid)
            {
                if (imgProduct != null && imgProduct.IsImage())
                {
                    string imagePath = "/Images/SliderImages/";

                    product.ProductImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgProduct.FileName);
                    imgProduct.SaveAs(Server.MapPath(imagePath + product.ProductImage));
                    product.ProductGroupID = -1;
                    product.ProductSubGroupID = -1;
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("SlideImage", "تصویر را انتخاب کنید!");
                    return View(product);
                }
            }

            return View(product);
        }

        // GET: Admin/Products/Edit/5
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
            ViewBag.ProductGroupID = new SelectList(db.ProductGroups, "ProductGroupID", "ProductGroupTitle", product.ProductGroupID);
            ViewBag.ProductSubGroupID = new SelectList(db.ProductGroups, "ProductGroupID", "ProductGroupTitle", product.ProductSubGroupID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductGroupID,ProductSubGroupID,ProductTitle,ProductShortText,ProductText,ProductImage")] Product product, HttpPostedFileBase imgProduct)
        {
            if (ModelState.IsValid)
            {
                if (imgProduct != null && imgProduct.IsImage())
                {

                        string imagePath = "/Images/SliderImages/";
                    if (product.ProductImage != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(imagePath + product.ProductImage)))
                            System.IO.File.Delete(Server.MapPath(imagePath + product.ProductImage));
                    }

                    product.ProductImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgProduct.FileName);
                    imgProduct.SaveAs(Server.MapPath(imagePath + product.ProductImage));
                }

                product.ProductGroupID = -1;
                product.ProductSubGroupID = -1;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
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

        // POST: Admin/Products/Delete/5
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
