using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.DALs;
using Capstone.Web.Models;

namespace Capstone.Web.DALs
{
    public interface GolfSqlDal
    {
        bool CreateMatch(Match match);
        bool SaveUser(User user);
        User GetUsername(string username);
        bool AddNewCourse(Course course);

    }
}
