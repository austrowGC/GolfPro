
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        [ChildActionOnly]
        public ActionResult Navigation()
        {
            if (Session[SessionKeys.Username] == null)
            {
                return PartialView("_NavAnon");
            }
            else
            {
                return PartialView("_NavAuth");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostUserRegistration()
        {
            return View("Index");
        }
    }
}