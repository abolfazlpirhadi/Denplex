﻿using System;
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
    public class UsersController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,UserPassword,UserEmail,UserIsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserPassword = PasswordHelper.EncodePasswordMd5(user.UserPassword).Replace("-", "");
                user.UserIsActive = true;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.UserPassword = "******";
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,UserPassword,UserEmail,UserIsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                var currentPerson = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                if (currentPerson == null)
                    return HttpNotFound();

                if (user.UserPassword != "******")
                {
                    currentPerson.UserPassword = PasswordHelper.EncodePasswordMd5(user.UserPassword).Replace("-", ""); ;
                }
                currentPerson.UserName = user.UserName;
                currentPerson.UserEmail = user.UserEmail;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            //return RedirectToAction("Index");
        }



        [HttpPost]
        public bool AjaxDelete(int userId)
        {

            User user = db.Users.Find(userId);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
                return true;

            }
            return false;




            //JsonResult
            //return Json(person);
        }



        [HttpPost]
        public bool ChangeStatus(int userId)
        {
            var currentPerson = db.Users.FirstOrDefault(u => u.UserID == userId);
            if (currentPerson == null)
                return false;

            currentPerson.UserIsActive = !currentPerson.UserIsActive;
                db.SaveChanges();
                new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
                return true;
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
