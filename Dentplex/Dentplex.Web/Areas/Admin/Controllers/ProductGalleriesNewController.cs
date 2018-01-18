using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dentplex.Data.Model;

namespace Dentplex.Web.Areas.Admin.Controllers
{
    public class ProductGalleriesNewController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();

        // GET: Admin/ProductGalleriesNew
        public ActionResult Index()
        {
            var productGalleries = db.ProductGalleries.Include(p => p.Product);
            return View(productGalleries.ToList());
        }

        // GET: Admin/ProductGalleriesNew/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGallery productGallery = db.ProductGalleries.Find(id);
            if (productGallery == null)
            {
                return HttpNotFound();
            }
            return View(productGallery);
        }

        // GET: Admin/ProductGalleriesNew/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductTitle");
            return View();
        }

        // POST: Admin/ProductGalleriesNew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductGalleryID,ProductID,ProductColor,ProductImageName,ProductImageTitle")] ProductGallery productGallery)
        {
            if (ModelState.IsValid)
            {
                db.ProductGalleries.Add(productGallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductTitle", productGallery.ProductID);
            return View(productGallery);
        }

        // GET: Admin/ProductGalleriesNew/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGallery productGallery = db.ProductGalleries.Find(id);
            if (productGallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductTitle", productGallery.ProductID);
            return View(productGallery);
        }

        // POST: Admin/ProductGalleriesNew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductGalleryID,ProductID,ProductColor,ProductImageName,ProductImageTitle")] ProductGallery productGallery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productGallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductTitle", productGallery.ProductID);
            return View(productGallery);
        }

        // GET: Admin/ProductGalleriesNew/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGallery productGallery = db.ProductGalleries.Find(id);
            if (productGallery == null)
            {
                return HttpNotFound();
            }
            return View(productGallery);
        }

        // POST: Admin/ProductGalleriesNew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductGallery productGallery = db.ProductGalleries.Find(id);
            db.ProductGalleries.Remove(productGallery);
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
