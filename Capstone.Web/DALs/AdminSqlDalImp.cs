using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.DALs.Interfaces;
using Capstone.Web.Models;

namespace Capstone.Web.DALs
{
    public class AdminSqlDalImp : AdminSqlDal
    {
        private readonly string connectionString;

        public AdminSqlDalImp(string connectionString)
        {
            this.connectionString = connectionString;
        }


    }
}