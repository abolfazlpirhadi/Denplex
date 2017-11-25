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
    public class SlidersController : Controller
    {

        private DentplexDBEntities db = new DentplexDBEntities();

        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlideID,SlideTitle,SlideImage,SlideIsActive,SlideDesc")] Slider slider, HttpPostedFileBase imgSlider)
        {
            if (ModelState.IsValid)
            {
                if (imgSlider != null && imgSlider.IsImage())
                {
                    string imagePath = "/Images/SliderImages/";

                    slider.SlideImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgSlider.FileName);
                    imgSlider.SaveAs(Server.MapPath(imagePath + slider.SlideImage));

                    db.Sliders.Add(slider);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("SlideImage", "تصویر را انتخاب کنید!");
                    return View(slider);
                }
            }

            return View(slider);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlideID,SlideTitle,SlideImage,SlideIsActive,SlideDesc")] Slider slider, HttpPostedFileBase imgSlider)
        {
            if (ModelState.IsValid)
            {
                if (imgSlider != null && imgSlider.IsImage())
                {
                    string imagePath = "/Images/SliderImages/";
                    if (slider.SlideImage != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(imagePath + slider.SlideImage)))
                            System.IO.File.Delete(Server.MapPath(imagePath + slider.SlideImage));
                    }

                    slider.SlideImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgSlider.FileName);
                    imgSlider.SaveAs(Server.MapPath(imagePath + slider.SlideImage));
                }
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);
            db.Sliders.Remove(slider);
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
