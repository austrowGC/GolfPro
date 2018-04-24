using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
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


    }
}