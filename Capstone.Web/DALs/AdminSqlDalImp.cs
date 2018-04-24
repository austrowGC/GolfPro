using System;
using System.Collections.Generic;
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

        public bool AddNewCourse(Course course)
        {
            string SQL_AddNewCourse = @"Insert into forum_post (username, subject, message) 
            values (@username, @subject, @message); Select Cast(Scope_identity() as int);";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SaveNewPost, conn);

                    cmd.Parameters.Add(new SqlParameter("@username", post.Username));
                    cmd.Parameters.Add(new SqlParameter("@subject", post.Subject));
                    cmd.Parameters.Add(new SqlParameter("@message", post.Message));
                    int newId = (int)cmd.ExecuteScalar();
                }
            }

            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                isSuccessful = false;
            }
            return isSuccessful;
        }

    }
}