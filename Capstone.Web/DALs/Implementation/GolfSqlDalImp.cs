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
<<<<<<< HEAD
        private readonly string getUserModelSql = @"select id, firstname, lastname, username from users";
=======
        private readonly string getUserModelSql = @"select id, username, firstname, lastname, password, isadmin, salt from users where (username = @username);";
>>>>>>> 371e80bcf124687d387f67978a81740f0620457c

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

<<<<<<< HEAD
<<<<<<< HEAD
        public User VerifyLogin(Login model)
        {
            User user = null;

            string VerifyLoginSql = @"select id, username, firstname, lastname from users where (username = @username) AND (password = @password);";
=======
<<<<<<< HEAD
=======

>>>>>>> 371e80bcf124687d387f67978a81740f0620457c
        public List<Course> GetAllCourses()
        {
            var list = new List<Course>();

            string sql = "SELECT * FROM courses ORDER BY name ASC;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    Course c = new Course()
                    {

                        Name = Convert.ToString(r["name"]),
                        Par = Convert.ToInt32(r["par"]),
                        NumberOfHoles = Convert.ToInt32(r["holeCount"]),
                        LengthInYards = Convert.ToInt32(r["totalLengthYards"]),

                    };

                    list.Add(c);
                }
            }
            return list;
        }

        public User GetUser(string username)
        {
            User user = new User();
<<<<<<< HEAD
>>>>>>> 94b0b94a3a159a0721791065b7c6016f3b647cb2
>>>>>>> 8eaffe562d9e761b9e8a3daa1a484af6cb00c5f2
=======

>>>>>>> 371e80bcf124687d387f67978a81740f0620457c
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
            string getUsernameSql = @"select id, username, firstname, lastname, isadmin from users where username = @username;";
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
            string getUsernameSql = @"select users.firstName, users.lastName, users.userName, users.isadmin 
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
                                         where users.userName = @userName and leagues.name = @leagueName
                                         group by users.firstName, users.lastName, users.userName, courses.holeCount";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getLeaderboardSql, conn);
                cmd.Parameters.AddWithValue("@username", userName);
                cmd.Parameters.AddWithValue("@leagueName", leagueName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    leaderboard = AssembleLeaderboard(reader);
                }
                conn.Close();
            }

            return leaderboard;
        }

        //public Leaderboard GetLeaderboard(string leagueName, string userName)
        //{
        //    Leaderboard leaderboard = new Leaderboard();
        //    string getLeaderboardSql = @"select users.firstName, users.lastName, users.userName, courses.holeCount,
        //                                 count(matches.id) as totalMatches, sum(users_matches.score) as totalStrokes
        //                                 from users
        //                                 join users_leagues on users_leagues.userId = users.id
        //                                 join leagues on leagues.id = users_leagues.leagueId
        //                                 join courses on courses.id = leagues.courseId 
        //                                 join users_matches on users_matches.userId = users.id
        //                                 join matches on matches.id = users_matches.matcheId
        //                                 where users.id = 1
        //                                 group by users.firstName, users.lastName, users.userName, courses.holeCount";
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(getLeaderboardSql, conn);
        //        //cmd.Parameters.AddWithValue("@username", username);
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            leaderboard = AssembleLeaderboard(reader);
        //        }
        //        conn.Close();
        //    }

        //    return leaderboard;
        //}


        private User AssembleUser(SqlDataReader reader)
        {
            User user = new User()
            {
                Id = Convert.ToInt32(reader["id"]),
                Username = Convert.ToString(reader["username"]),
                FirstName = Convert.ToString(reader["firstname"]),
<<<<<<< HEAD
                LastName = Convert.ToString(reader["lastname"])
=======
                LastName = Convert.ToString(reader["lastname"]),

                IsAdministrator = false,

                Password = Convert.ToString(reader["password"]),
                Salt = Convert.ToString(reader["salt"])


>>>>>>> 8eaffe562d9e761b9e8a3daa1a484af6cb00c5f2
            };

            if (Convert.ToInt32(reader["isadmin"]) == 1)
            {
                user.IsAdministrator = true;
            }

            return user;
        }
    }
}