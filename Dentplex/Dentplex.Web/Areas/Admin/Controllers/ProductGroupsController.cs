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
    public class ProductGroupsController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();

        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult List()
        {
            var productGroups = db.ProductGroups.Where(p => p.ProductParentGroupID == null);
            return PartialView(productGroups.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }
        public ActionResult Create(int? parentId)
        {
            //ViewBag.ProductParentGroupID = new SelectList(db.ProductGroups, "ProductGroupID", "ProductGroupTitle");
            return PartialView(new ProductGroup()
            {
                ProductParentGroupID = parentId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductGroupID,ProductParentGroupID,ProductGroupTitle")] ProductGroup productGroup, HttpPostedFileBase imgBannerProductGroup, HttpPostedFileBase imgProductGroup)
        {
            if (ModelState.IsValid)
            {
                string mainImagePath = "/Images/ProuctGroups/MainImage/";
                string bannerImagePath = "/Images/ProuctGroups/BannerImage/";

                if (imgProductGroup != null && imgProductGroup.IsImage())
                {
                    string imageName = Guid.NewGuid() + Path.GetExtension(imgProductGroup.FileName);
                    productGroup.ProductGroupImage = imageName;
                    imgProductGroup.SaveAs(Server.MapPath(mainImagePath + imageName));
                }

                if (imgBannerProductGroup != null && imgBannerProductGroup.IsImage())
                {
                    string imageName = Guid.NewGuid() + Path.GetExtension(imgBannerProductGroup.FileName);
                    productGroup.ProductGroupBanner = imageName;
                    imgBannerProductGroup.SaveAs(Server.MapPath(bannerImagePath + imageName));
                }

                db.ProductGroups.Add(productGroup);
                db.SaveChanges();

                return PartialView("List", db.ProductGroups.Where(p => p.ProductParentGroupID == null));
            }

            ViewBag.ProductParentGroupID = new SelectList(db.ProductGroups, "ProductGroupID", "ProductGroupTitle", productGroup.ProductParentGroupID);
            return PartialView(productGroup);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductParentGroupID = new SelectList(db.ProductGroups, "ProductGroupID", "ProductGroupTitle", productGroup.ProductParentGroupID);
            return PartialView(productGroup);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductGroupID,ProductParentGroupID,ProductGroupTitle")] ProductGroup productGroup, HttpPostedFileBase imgBannerProductGroup, HttpPostedFileBase imgProductGroup)
        {
            if (ModelState.IsValid)
            {
                if (imgProductGroup != null && imgProductGroup.IsImage())
                {
                    string mainImagePath = "/Images/ProuctGroups/MainImage/";
                    if (System.IO.File.Exists(Server.MapPath(mainImagePath + productGroup.ProductGroupImage)))
                        System.IO.File.Delete(Server.MapPath(mainImagePath + productGroup.ProductGroupImage));

                    string imageName = Guid.NewGuid() + Path.GetExtension(imgProductGroup.FileName);
                    productGroup.ProductGroupImage = imageName;
                    imgProductGroup.SaveAs(Server.MapPath(mainImagePath + imageName));
                }

                if (imgBannerProductGroup != null && imgBannerProductGroup.IsImage())
                {
                    string bannerImagePath = "/Images/ProuctGroups/BannerImage/";
                    if (System.IO.File.Exists(Server.MapPath(bannerImagePath + productGroup.ProductGroupBanner)))
                        System.IO.File.Delete(Server.MapPath(bannerImagePath + productGroup.ProductGroupBanner));

                    string imageName = Guid.NewGuid() + Path.GetExtension(imgBannerProductGroup.FileName);
                    productGroup.ProductGroupBanner = imageName;
                    imgBannerProductGroup.SaveAs(Server.MapPath(bannerImagePath + imageName));
                }


                db.Entry(productGroup).State = EntityState.Modified;
                db.SaveChanges();

                return PartialView("List", db.ProductGroups.Where(p => p.ProductParentGroupID == null));
            }
            ViewBag.ProductParentGroupID = new SelectList(db.ProductGroups, "ProductGroupID", "ProductGroupTitle", productGroup.ProductParentGroupID);
            return PartialView(productGroup);
        }
        public ActionResult Delete(int? id)
        {
            var group = db.ProductGroups.Find(id);
            if (group.ProductGroups1.Any())
            {
                foreach (var group2 in db.ProductGroups.Where(p => p.ProductParentGroupID == id))
                {
                    db.ProductGroups.Remove(group2);
                }
            }

            db.ProductGroups.Remove(group);
            db.SaveChanges();

            return PartialView("List", db.ProductGroups.Where(g => g.ProductParentGroupID == null));

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductGroup productGroup = db.ProductGroups.Find(id);
            db.ProductGroups.Remove(productGroup);
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
