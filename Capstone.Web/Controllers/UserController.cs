using Capstone.Web.DALs;
using Capstone.Web.Models;
using Capstone.Web.DALs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class UserController
    {
        private readonly UserSqlDal userDal;

        public UserController(UserSqlDal userDal)
        {
            this.userDal = userDal;
        }


    }
}