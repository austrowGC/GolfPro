using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.DALs;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;

namespace Capstone.Web.DALs
{
    public interface GolfSqlDal
    {
        bool CreateMatch(Match match);
        User GetUser(string username);
        bool SaveUser(Registration model);
        //bool CreateLeague(League league);
        User GetUsername(string username);
        bool AddNewCourse(Course course);
        User VerifyLogin(Login model);

    }
}
