
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
        private GolfSqlDal _dal;
        public HomeController(GolfSqlDal dal)
        {
            _dal = dal;
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

        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult PostUserLogin(Login model)
        {

            return View("Index");
        }


        public ActionResult Registration(Registration model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult PostUserRegistration()
        {
            return View("Index");
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
            _dal.AddNewCourse(course);

            return RedirectToAction("Index", "Home");
        }

    }
}