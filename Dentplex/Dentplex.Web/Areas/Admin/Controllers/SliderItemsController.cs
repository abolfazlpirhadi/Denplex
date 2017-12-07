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
    public class SliderItemsController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();

        [Authorize]
        public ActionResult Index()
        {
            var sliderItems = db.SliderItems.Include(s => s.Slider);
            return View(sliderItems.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderItem sliderItem = db.SliderItems.Find(id);
            if (sliderItem == null)
            {
                return HttpNotFound();
            }
            return View(sliderItem);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.SlideID = new SelectList(db.Sliders, "SlideID", "SlideTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlideItemID,SlideID,SlideItemTitle,SlideItemImage,SlideItemLink,SlideItemOrder")] SliderItem sliderItem, HttpPostedFileBase imgSliderItems)
        {
            if (ModelState.IsValid)
            {
                if (imgSliderItems != null && imgSliderItems.IsImage())
                {
                    string imagePath = "/Images/SliderItemImages/";

                    sliderItem.SlideItemImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgSliderItems.FileName);
                    imgSliderItems.SaveAs(Server.MapPath(imagePath + sliderItem.SlideItemImage));

                    db.SliderItems.Add(sliderItem);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("SlideItemImage", "تصویر را انتخاب کنید!");
                    return View(sliderItem);
                }
            }

            ViewBag.SlideID = new SelectList(db.Sliders, "SlideID", "SlideTitle", sliderItem.SlideID);
            return View(sliderItem);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderItem sliderItem = db.SliderItems.Find(id);
            if (sliderItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.SlideID = new SelectList(db.Sliders, "SlideID", "SlideTitle", sliderItem.SlideID);
            return View(sliderItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlideItemID,SlideID,SlideItemTitle,SlideItemImage,SlideItemLink,SlideItemOrder")] SliderItem sliderItem, HttpPostedFileBase imgSliderItems)
        {
            if (ModelState.IsValid)
            {
                if (imgSliderItems != null && imgSliderItems.IsImage())
                {
                    string imagePath = "/Images/SliderItemImages/";
                    if (sliderItem.SlideItemImage != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(imagePath + sliderItem.SlideItemImage)))
                            System.IO.File.Delete(Server.MapPath(imagePath + sliderItem.SlideItemImage));

                        sliderItem.SlideItemImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgSliderItems.FileName);
                        imgSliderItems.SaveAs(Server.MapPath(imagePath + sliderItem.SlideItemImage));
                    }
                }

                db.Entry(sliderItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SlideID = new SelectList(db.Sliders, "SlideID", "SlideTitle", sliderItem.SlideID);
            return View(sliderItem);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderItem sliderItem = db.SliderItems.Find(id);
            if (sliderItem == null)
            {
                return HttpNotFound();
            }
            return View(sliderItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SliderItem sliderItem = db.SliderItems.Find(id);
            db.SliderItems.Remove(sliderItem);
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
