
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
using Capstone.Web.DALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;


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
        public ActionResult NavAdmin()
        {
            if ((bool)Session[SessionKeys.IsAdmin])
            {
                return PartialView("_NavAdmin");
            }
            else
            {
                return null;
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
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
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
                ModelState.AddModelError("invalid-credentials", "Invalid login credentials");
                return View("Login", model);
            }
            Session[SessionKeys.Username] = user.Username;
            Session[SessionKeys.IsAdmin] = user.IsAdministrator;

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

            if (dal.GetUsername(model.UserName) != null)
            {
                ModelState.AddModelError("username-exists", "Username unavailable");
                return View("Registration", model);
            }
            else
            {
                dal.SaveUser(model);
                User user = dal.GetUser(model.UserName);
                Session[SessionKeys.Username] = user.Username;
            }

            return RedirectToAction("Index");
        }

        public ActionResult LeagueLeaderBoard()
        {
            User user = dal.GetUsername("trogdor");

            return View("LeagueLeaderBoard");
        }

        [HttpPost]
        public ActionResult CreateMatch(Match match)
        {
            dal.CreateMatch(match);

            return RedirectToAction("Index", "Home");
        }


        public ActionResult AddNewCourse()
        {
            return View("AddNewCourse");
        }

        public enum MessageType
        {
            Success,
            Warning,
            Error
        }

        public const string SUCCESS_MESSAGE_KEY = "postSuccessMessage.Text";
        public const string SUCCESS_MESSAGE_TYPE_KEY = "postSuccessMessage.Type";


        protected void SetMessage(string text, MessageType type = MessageType.Success)
        {
            TempData[SUCCESS_MESSAGE_KEY] = text;
            TempData[SUCCESS_MESSAGE_TYPE_KEY] = type;
        }

        [HttpPost]
        public ActionResult AddNewCourse(Course course)
        {
            dal.AddNewCourse(course);

            //Check that it was successfully added
            bool isSuccessful = true;

            //If successful:

            if (isSuccessful)
            {
                SetMessage("Course has been successfully added!", MessageType.Success);
            }
            else
            {
                SetMessage("There was an error adding your course!", MessageType.Error);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}