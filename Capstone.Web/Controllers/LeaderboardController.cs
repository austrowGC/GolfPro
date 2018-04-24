using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class LeaderboardController : Controller
    {
        // GET: Leaderboarf
        public ActionResult Index()
        {
            return View();
        }
    }
}