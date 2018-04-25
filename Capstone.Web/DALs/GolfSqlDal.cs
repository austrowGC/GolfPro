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
        bool RegisterUser(User user);
        bool CheckUsername(User user);
        bool AddNewCourse(Course course);
    }
}
