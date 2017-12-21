using Dentplex.Data.Model;
using Dentplex.Web.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dentplex.Web.Areas.Admin.Controllers
{
    public class ProductGroupsAjaxController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        //Parent
        public JsonResult ProductGroupsParent_Read([DataSourceRequest] DataSourceRequest request)
        {
            var list = db.ProductGroups.Where(p => p.ProductParentGroupID == null).Select(p => new ProductGroupsViewModel
            {
                ProductGroupID = p.ProductGroupID,
                ProductParentGroupID = p.ProductParentGroupID,
                ProductGroupTitle = p.ProductGroupTitle,
                ProductGroupImage = p.ProductGroupImage,
                ProductGroupBanner = p.ProductGroupBanner
            }).ToList();

            DataSourceResult result = list.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductGroupsParent_Create([DataSourceRequest] DataSourceRequest request, ProductGroupsViewModel productGroupsViewModel)
        {
            if (productGroupsViewModel != null && ModelState.IsValid)
            {
                db.ProductGroups.Add(new ProductGroup()
                {
                    ProductGroupTitle = productGroupsViewModel.ProductGroupTitle,
                });
                db.SaveChanges();
            }

            return Json(new[] { productGroupsViewModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult ProductGroupsParent_Update([DataSourceRequest] DataSourceRequest request, ProductGroupsViewModel productGroupsViewModel)
        {
            if (productGroupsViewModel != null && ModelState.IsValid)
            {
                ProductGroup productGroup = db.ProductGroups.Find(productGroupsViewModel.ProductGroupID);
                if (productGroup != null)
                {
                    productGroupsViewModel.ProductGroupTitle = productGroupsViewModel.ProductGroupTitle;

                    db.Entry(productGroup).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(new[] { productGroupsViewModel }.ToDataSourceResult(request, ModelState));
        }

        //Child
        public ActionResult ProductGroupsChild_Read([DataSourceRequest] DataSourceRequest request, int productGroupID)
        {
            var list = db.ProductGroups.Where(p => p.ProductParentGroupID == productGroupID).Select(p => new ProductGroupsViewModel
            {
                ProductGroupID = p.ProductGroupID,
                ProductParentGroupID = p.ProductParentGroupID,
                ProductGroupTitle = p.ProductGroupTitle,
            }).ToList();
            DataSourceResult result = list.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductGroupsChild_Create([DataSourceRequest] DataSourceRequest request, ProductGroupsViewModel productGroupsViewModel, int productGroupID)
        {
            if (productGroupsViewModel != null && ModelState.IsValid)
            {
                db.ProductGroups.Add(new ProductGroup()
                {
                    ProductParentGroupID = productGroupID,
                    ProductGroupTitle = productGroupsViewModel.ProductGroupTitle,
                });
                db.SaveChanges();
            }

            return Json(new[] { productGroupsViewModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult ProductGroupsChild_Update([DataSourceRequest] DataSourceRequest request, ProductGroupsViewModel productGroupsViewModel)
        {
            if (productGroupsViewModel != null && ModelState.IsValid)
            {
                ProductGroup productGroup = db.ProductGroups.Find(productGroupsViewModel.ProductGroupID);
                if (productGroup != null)
                {
                    productGroup.ProductGroupTitle = productGroupsViewModel.ProductGroupTitle;
                    db.Entry(productGroup).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(new[] { productGroupsViewModel }.ToDataSourceResult(request, ModelState));
        }
    }
}