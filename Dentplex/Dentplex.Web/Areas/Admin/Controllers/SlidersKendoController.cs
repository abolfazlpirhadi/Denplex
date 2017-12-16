using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dentplex.Data.Model;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Dentplex.Web.Areas.Admin.Controllers
{
    public class SlidersKendoController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();




        public class SliderResualt
        {
            public int Total { get; set; }

            //This property creates a circular reference because of the Customer class.
            //Refers to the Order class through the Orders property.
            public List<Sliderclass> Data { get; set; }
        }

        public class Sliderclass
        {
            public int SlideID { get; set; }
            public string SlideTitle { get; set; }
            public string SlideImage { get; set; }
            public bool SlideIsActive { get; set; }
            public string SlideDesc { get; set; }

        }


        //Action.
        public ActionResult GetAllSlider([DataSourceRequest] DataSourceRequest request)
        {
            //var northwind = new DentplexDBEntities();
            //var orders = northwind.SliderResualt;

            //Avoid the circular reference by creating a View Model object and skipping the Customer property.
            //var result = orders.ToDataSourceResult(request, o => new {
            //    OrderID = o.OrderID,
            //    CustomerName = o.Customer.ContactName
            //});


            try
            {
                var response = (from u in db.Sliders
                                orderby u.SlideID descending 
                    select new Sliderclass
                    {
                        SlideID = u.SlideID,
                        SlideTitle = u.SlideTitle,
                        SlideImage = u.SlideImage,
                        SlideDesc = u.SlideDesc,
                        SlideIsActive = u.SlideIsActive
                    }).ToList();
                //Apply paging.
                int count = response.Count;








                if (request.Page > 0)
                {
                    response = response.Skip((request.Page - 1)*request.PageSize).ToList();
                }
                response = response.Take(request.PageSize).ToList();

                //return Json(response.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);


                var result = new SliderResualt()
                {
                    Data = response,
                    Total = count
                };

                return Json(result, JsonRequestBehavior.AllowGet);


                //return Json(GetOrders().ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            //return new SliderResualt() {Data = response };
        }









        // UPDATE: Slider  
        
        public ActionResult UpdateSlider([DataSourceRequest] DataSourceRequest request, Sliderclass sdr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Slider slider = db.Sliders.Find(sdr.SlideID);
                    if (slider != null)
                    {
                        slider.SlideTitle = sdr.SlideTitle;
                        slider.SlideDesc = sdr.SlideDesc;
                        slider.SlideImage = sdr.SlideImage;
                        slider.SlideIsActive = sdr.SlideIsActive;
                        db.Entry(slider).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                        
                    return Json(new[] { sdr }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);

                }
                else
                {
                    var response = (from u in db.Sliders
                                    orderby u.SlideID descending
                                    select new Sliderclass
                        {
                            SlideID = u.SlideID,
                            SlideTitle = u.SlideTitle,
                            SlideImage = u.SlideImage,
                            SlideDesc = u.SlideDesc,
                            SlideIsActive = u.SlideIsActive
                        }).ToList();
                    //Apply paging.
                    if (request.Page > 0)
                    {
                        response = response.Skip((request.Page - 1)*request.PageSize).ToList();
                    }
                    response = response.Take(request.PageSize).ToList();

                    return Json(response.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // ADD: Slider  

        public ActionResult AddSlider([DataSourceRequest] DataSourceRequest request, Sliderclass sdr)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Sliders.Add(new Slider()
                    {
                        SlideTitle = sdr.SlideTitle,
                        SlideImage = sdr.SlideImage,
                        SlideDesc = sdr.SlideDesc,
                        SlideIsActive = sdr.SlideIsActive
                    });
                    db.SaveChanges();
                    //return Json(sdr.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                    return Json(new[] { sdr }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    //return Json(ModelState.IsValid ? new object() : ModelState.ToDataSourceResult());
                }

                else
                {
                    var response = (from u in db.Sliders
                                    orderby u.SlideID descending
                                    select new Sliderclass
                        {
                            SlideID = u.SlideID,
                            SlideTitle = u.SlideTitle,
                        }).ToList();
                    //Apply paging.
                    if (request.Page > 0)
                    {
                        response = response.Skip((request.Page - 1)*request.PageSize).ToList();
                    }
                    response = response.Take(request.PageSize).ToList();

                    return Json(response.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // DELETE: Slider  

        public ActionResult DeleteSlider([DataSourceRequest] DataSourceRequest request, Sliderclass sdr)
        {
            try
            {
                var employee = db.Sliders.Find(sdr.SlideID);
                if (employee == null)
                {
                    return Json("اسلایدر پیدا نشد");
                }

                db.Sliders.Remove(employee);
                db.SaveChanges();

                //return all data
                var response = (from u in db.Sliders
                                orderby u.SlideID descending
                                select new Sliderclass
                    {
                        SlideID = u.SlideID,
                        SlideTitle = u.SlideTitle,
                    }).ToList();
                //Apply paging.
                if (request.Page > 0)
                {
                    response = response.Skip((request.Page - 1)*request.PageSize).ToList();
                }
                response = response.Take(request.PageSize).ToList();

                return Json(response.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }



        public ActionResult UldSave(HttpPostedFileBase file)
        {
            //var fileName = Path.GetFileName(file.FileName);
            //var physicalPath = Path.Combine(Server.MapPath("~/CategoryImage"), fileName);

            //file.SaveAs(physicalPath);

            var fileName = "MohsenFileNameReturned";

            return Json(new { ImageUrl = fileName }, "text/plain");
        }


























        //// GET: AllSlider  
        //public ActionResult GetAllSlider([DataSourceRequest]DataSourceRequest request)
        //{
        //    try
        //    {

        //        var slider = db.Sliders.ToList();

        //        return Json(slider.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }

        //}





        // GET: Admin/SlidersKendo
        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }

        //// GET: Admin/SlidersKendo/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Slider slider = db.Sliders.Find(id);
        //    if (slider == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(slider);
        //}

        //// GET: Admin/SlidersKendo/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/SlidersKendo/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "SlideID,SlideTitle,SlideImage,SlideIsActive,SlideDesc")] Slider slider)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Sliders.Add(slider);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(slider);
        //}

        //// GET: Admin/SlidersKendo/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Slider slider = db.Sliders.Find(id);
        //    if (slider == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(slider);
        //}

        //// POST: Admin/SlidersKendo/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SlideID,SlideTitle,SlideImage,SlideIsActive,SlideDesc")] Slider slider)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(slider).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(slider);
        //}

        //// GET: Admin/SlidersKendo/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Slider slider = db.Sliders.Find(id);
        //    if (slider == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(slider);
        //}

        //// POST: Admin/SlidersKendo/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Slider slider = db.Sliders.Find(id);
        //    db.Sliders.Remove(slider);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
