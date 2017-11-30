﻿using Dentplex.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dentplex.Web.Controllers
{
    public class ProductController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();

        [Route("Product/{id}")]
        public ActionResult Product(int? id)
        {
            var product = db.Products.Where(p => p.ProductGroupID == id);
            return View(product);
        }
        public ActionResult ProductBanner(int id)
        {
            var productgroup = db.ProductGroups.Find(id);
            return PartialView(productgroup);
        }
        public PartialViewResult ProductGroups()
        {
            return PartialView(db.ProductGroups.Where(p => p.ProductParentGroupID == null));
        }
        public ActionResult ShowProducts(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }
    }
}