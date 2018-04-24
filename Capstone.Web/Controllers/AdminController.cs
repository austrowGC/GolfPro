using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DALs;
using Capstone.Web.DALs.Interfaces;

namespace Capstone.Web.Controllers
{
    public class AdminController : Controller
    {
        private AdminSqlDal _dal;
        public AdminController(AdminSqlDal dal)
        {
            _dal = dal;
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