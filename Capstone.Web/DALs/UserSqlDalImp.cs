using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.DALs.Interfaces;
using Capstone.Web.Models;

namespace Capstone.Web.DALs
{
    public class UserSqlDalImp : UserSqlDal
    {
        private readonly string connectionString;

        public UserSqlDalImp(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreateMatch(Match match)
        {
            bool isSuccessful = true;

            string SQL_CreateMatch = @"Insert into matches (name, par, holeCount, totalLengthYards) 
            values (@name, @par, @holeCount, @totalLengthYards)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CreateMatch, conn);

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