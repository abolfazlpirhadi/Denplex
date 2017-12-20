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
            try
            {
                var response = (from p in db.ProductGroups
                                where p.ProductParentGroupID == null
                                select new ProductGroupsViewModel
                                {
                                    ProductGroupID = p.ProductGroupID,
                                    ProductGroupTitle = p.ProductGroupTitle,
                                }).ToList();
                //Apply paging.
                int count = response.Count;
                if (request.Page > 0)
                {
                    response = response.Skip((request.Page - 1) * request.PageSize).ToList();
                }
                response = response.Take(request.PageSize).ToList();

                //return Json(response.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                //var result = new SliderResualt()
                //{
                //    Data = response,
                //    Total = count
                //};

                return Json(response, JsonRequestBehavior.AllowGet);


                //return Json(GetOrders().ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }


            //var list = db.ProductGroups.Where(p => p.ProductParentGroupID == null).Select(p=> new ProductGroupsViewModel
            //{
            //    ProductGroupTitle = p.ProductGroupTitle,
            //});
            //DataSourceResult result = list.ToDataSourceResult(request);
            //return Json(result, JsonRequestBehavior.AllowGet);
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
        public ActionResult ProductGroupsChild_Read([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var list = db.ProductGroups.Where(p => p.ProductGroupID == parentID);
            DataSourceResult result = list.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductGroupsChild_Create([DataSourceRequest] DataSourceRequest request, ProductGroupsViewModel productGroupsViewModel, int parentID)
        {
            if (productGroupsViewModel != null && ModelState.IsValid)
            {

                db.ProductGroups.Add(new ProductGroup()
                {
                    ProductParentGroupID = parentID,
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
                    productGroupsViewModel.ProductGroupTitle = productGroupsViewModel.ProductGroupTitle;

                    db.Entry(productGroup).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(new[] { productGroupsViewModel }.ToDataSourceResult(request, ModelState));
        }
    }
}