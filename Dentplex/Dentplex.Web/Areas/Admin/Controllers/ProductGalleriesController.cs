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
    public class ProductGalleriesController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();

        // GET: Admin/ProductGalleries
        public ActionResult Index()
        {
            var productGalleries = db.ProductGalleries.Include(p => p.Product);
            return View(productGalleries.ToList());
        }

        // GET: Admin/ProductGalleries/Details/5
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

        // GET: Admin/ProductGalleries/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductTitle");
            return View();
        }

        // POST: Admin/ProductGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductGalleryID,ProductID,ProductColor,ProductImageName,ProductImageTitle")] ProductGallery productGallery,int id,HttpPostedFileBase imgProductGallery)
        {
            productGallery.ProductID = id;
            if (ModelState.IsValid)
            {
                if (imgProductGallery != null && imgProductGallery.IsImage())
                {
                    string mainImagePath = "/Images/ProductGallery/MainImage/";
                    string thumbImagePath = "/Images/ProductGallery/ThumbImage/";

                    if (!Directory.Exists(thumbImagePath))
                        Directory.CreateDirectory(Server.MapPath(thumbImagePath));

                    if (!Directory.Exists(mainImagePath))
                        Directory.CreateDirectory(Server.MapPath(mainImagePath));

                    string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgProductGallery.FileName);
                    productGallery.ProductImageName = imageName;
                    imgProductGallery.SaveAs(Server.MapPath(mainImagePath + imageName));

                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath(mainImagePath + imageName),
                        Server.MapPath(thumbImagePath + imageName));

                    db.ProductGalleries.Add(productGallery);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
                else
                {
                    ModelState.AddModelError("ProductImageName", "تصویر را انتخاب کنید!");
                    return View(productGallery);
                }

                
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductTitle", productGallery.ProductID);
            return View(productGallery);
        }

        // GET: Admin/ProductGalleries/Edit/5
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

        // POST: Admin/ProductGalleries/Edit/5
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

        // GET: Admin/ProductGalleries/Delete/5
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

        // POST: Admin/ProductGalleries/Delete/5
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
