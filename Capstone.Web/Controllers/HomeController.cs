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

<<<<<<< HEAD
=======
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

>>>>>>> 7774bffb8b7b00172fb82e001f311820b88a4707

    }
}