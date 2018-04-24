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

        public bool AddNewCourse(Course course)
        {
            bool isSuccessful = true;

            string SQL_AddNewCourse = @"Insert into courses (name, par, holeCount, totalLengthYards) 
            values (@name, @par, @holeCount, @totalLengthYards)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddNewCourse, conn);

                    cmd.Parameters.Add(new SqlParameter("@name", course.Name));
                    cmd.Parameters.Add(new SqlParameter("@par", course.Par));
                    cmd.Parameters.Add(new SqlParameter("@holeCount", course.NumberOfHoles));
                    cmd.Parameters.Add(new SqlParameter("@totalLengthYards", course.LengthInYards));
                    cmd.ExecuteNonQuery();
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