﻿using MVCRoleBasedAuthentication.EntityModel;
using MVCRoleBasedAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCRoleBasedAuthentication.Controllers
{
    public class AccountsController : Controller
    {
        public ActionResult Login()
        {
            string localbranch2 = "";
            string localbranch1 = "";
            string testvariable = "";
            string test2 = "";
            return View();
        }
        [HttpPost]  
        public ActionResult Login(UserModel model)
        {
            using (MVCDBEntity context = new MVCDBEntity())
            {
                bool IsValidUser = context.Users.Any(user => user.UserName.ToLower() ==
                     model.UserName.ToLower() && user.UserPassword == model.UserPassword);
                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "invalid Username or Password");
                return View();
            }
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (MVCDBEntity context = new MVCDBEntity())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}