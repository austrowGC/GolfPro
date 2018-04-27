using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;

namespace Capstone.Web.DALs.Implementation
{
    public class GolfSqlDalImp : GolfSqlDal
    {
        private readonly string getUserModelSql = @"select id, firstname, lastname, username from users";

        private readonly string connectionString;

        public GolfSqlDalImp(string connectionString)
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

        public User VerifyLogin(Login model)
        {
            User user = null;

            string VerifyLoginSql = @"select id, username, firstname, lastname from users where (username = @username) AND (password = @password);";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(VerifyLoginSql, conn);
                cmd.Parameters.AddWithValue("@username", model.Username);
                cmd.Parameters.AddWithValue("@password", model.Password);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user = AssembleUser(reader);
                }
                conn.Close();
            }
            return user;
        }

        public User GetUsername(string username)
        {
            User user = null;
            string getUsernameSql = @"select id, username, firstname, lastname from users where username = @username;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getUsernameSql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user = AssembleUser(reader);
                }
                conn.Close();
            }
            return user;
        }

        public bool CreateMatch(Match match)
        {
            bool isSuccessful = true;

            string SQL_CreateMatch = @"Insert into matches (date, numOfPlayers) 
            values (@date, @numOfPlayers)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CreateMatch, conn);

                    cmd.Parameters.Add(new SqlParameter("@name", match.Reservation));
                    cmd.Parameters.Add(new SqlParameter("@numOfPlayers", match.NumberOfPlayers));
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

        public bool SaveUser(User user)
        {
            bool registrationSuccess = false;
            string saveUserSql = @"insert into users (firstname, lastname, username, password) values (@firstname, @lastname, @username, @password);";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(saveUserSql, conn);
                cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                cmd.Parameters.AddWithValue("@lastname", user.LastName);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                int affectedRows = cmd.ExecuteNonQuery();
                if(affectedRows == 1)
                {
                    registrationSuccess = true;
                }
                conn.Close();
            }
            return registrationSuccess;
        }

        public List<User> GetLeaderboardUsernames(string leagueName)

        {
            List<User> users = new List<User>();
            string getUsernameSql = @"select users.firstName, users.lastName, users.userName 
                                      from users 
                                      join users_leagues on users_leagues.userId = users.id
                                      join leagues on leagues.id = users_leagues.leagueId
                                      where leagues.id = @leagueID";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getUsernameSql, conn);
                cmd.Parameters.AddWithValue("@leagueID", leagueName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User user = AssembleUser(reader);
                    users.Add(user);
                }
                conn.Close();
            }
            return users;
        }

        public Leaderboard GetLeaderboard(string leagueName, string userName)
        {
            Leaderboard leaderboard = new Leaderboard();
            string getLeaderboardSql = @"select users.firstName, users.lastName, users.userName, courses.holeCount,
                                         count(matches.id) as totalMatches, sum(users_matches.score) as totalStrokes
                                         from users
                                         join users_leagues on users_leagues.userId = users.id
                                         join leagues on leagues.id = users_leagues.leagueId
                                         join courses on courses.id = leagues.courseId 
                                         join users_matches on users_matches.userId = users.id
                                         join matches on matches.id = users_matches.matcheId
                                         where users.id = 1
                                         group by users.firstName, users.lastName, users.userName, courses.holeCount";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getLeaderboardSql, conn);
                //cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    leaderboard = AssembleLeaderboard(reader);
                }
                conn.Close();
            }

            return leaderboard;
        }

        private User AssembleUser(SqlDataReader reader)
        {
            User user = new User()
            {
                Id = Convert.ToInt32(reader["id"]),
                Username = Convert.ToString(reader["username"]),
                FirstName = Convert.ToString(reader["firstname"]),
                LastName = Convert.ToString(reader["lastname"])
            };

            return user;
        }
    }
}