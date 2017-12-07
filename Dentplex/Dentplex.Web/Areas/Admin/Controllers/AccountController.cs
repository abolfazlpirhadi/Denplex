using Dentplex.Data.Model;
using Dentplex.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Dentplex.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private DentplexDBEntities db = new DentplexDBEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel, string ReturnUrl = "/Admin/Home/Index")
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == loginViewModel.UserName && u.UserPassword == loginViewModel.Password);
                if (user != null)
                {
                    if (user.UserIsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                        Session["Username"] = user.UserName;
                        return Redirect(ReturnUrl);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("UserName", "کاربری با این مشخصات یافت نشد!");
            }
            return View(loginViewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return Redirect("/Admin/Account/Login");
        }
    }
}