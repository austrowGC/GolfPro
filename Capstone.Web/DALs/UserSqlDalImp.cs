using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.DALs.Interfaces;

namespace Capstone.Web.DALs
{
    public class UserSqlDalImp : UserSqlDal
    {
        private readonly string connectionString;

        public UserSqlDalImp(string connectionString)
        {
            this.connectionString = connectionString;
        }

    }
}