
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Capstone.Web.DALs;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private GolfSqlDal dal;
        public HomeController(GolfSqlDal dal)
        {
            this.dal = dal;
        }
        public HomeController()
        {

        }

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

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
        [ChildActionOnly]
        public ActionResult Content()
        {
            if (Session[SessionKeys.Username] == null)
            {
                return PartialView("_Splash");
            }
            else
            {
                return PartialView("_Dashboard");
            }
        }


        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult PostUserLogin(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            User user = dal.VerifyLogin(model);
            if(user == null)
            {
                return View("Login", model);
            }
            Session[SessionKeys.Username] = model.Username;

            return RedirectToAction("Index");
        }


        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostUserRegistration(Registration model)
        {
            if (!ModelState.IsValid)
            {
                return View("Registration", model);
            }

            User user = dal.GetUsername(model.UserName);
            if (user != null)
            {
                ModelState.AddModelError("username-exists", "Username unavailable");
                return View("Registration", model);
            }
            else
            {
                user = new User()
                {
                    Username = model.UserName,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                dal.SaveUser(user);
                Session[SessionKeys.Username] = user.Username;
                Session[SessionKeys.UserId] = user.Id;
            }

            return RedirectToAction("Index");
        }

        public ActionResult LeagueLeaderBoard()
        {
            return View("LeagueLeaderBoard");
        }

        public ActionResult CreateMatch()
        {
            return View("CreateMatch");
        }

        public ActionResult CreateLeague()
        {
            return View("CreateLeague");
        }

        public ActionResult AddNewCourse()
        {
            return View("AddNewCourse");
        }

        [HttpPost]
        public ActionResult AddNewCourse(Course course)
        {
            dal.AddNewCourse(course);

            return RedirectToAction("Index", "Home");
        }

    }
}