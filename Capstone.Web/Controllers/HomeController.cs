
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

        [HttpPost]
        public ActionResult CreateMatch(Match match)
        {
            dal.CreateMatch(match);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateLeague()
        {
            return View("CreateLeague");
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