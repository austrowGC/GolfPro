using System;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
using System.Security.Cryptography;


namespace Capstone.Web.DALs.Implementation
{
    public class GolfSqlDalImp : GolfSqlDal
    {
        private readonly string getUserModelSql = @"select id, username, firstname, lastname, password, isadmin, salt from users where (username = @username);";

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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getUserModelSql, conn);
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

        public bool ValidLogin(Login model)
        {
            bool validPass = false;
            if (usernameExists(model.Username))
            {
                UserCredentials cred = GetUserCredentials(model.Username);
                validPass = new Authenticator(cred.Salt, cred.Hash).AssertValidPassword(model.Password);
            }
            return validPass;
            
        }

        public User GetUsername(string username)
        {
            User user = null;
            string getUsernameSql = @"select id, username, firstname, lastname, password, isadmin, salt from users where username = @username;";
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

        public bool CreateLeague(League model, User user)
        {
            bool isSuccessful = true;

            string SQL_CreateLeague = @"Insert into leagues (name, organizerId, courseId) values (@name, @organizerId, courseId)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CreateLeague, conn);

                    cmd.Parameters.Add(new SqlParameter("@name", model.Name));
                    cmd.Parameters.Add(new SqlParameter("@organizerId", user.Id));
                    cmd.Parameters.Add(new SqlParameter("@courseId", model.CourseId));
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

        public bool SaveUser(Registration model)
        {
            bool registrationSuccess = false;
            string saveUserSql = @"insert into users (firstname, lastname, username, password, salt) values (@firstname, @lastname, @username, @password, @salt);";

            Authenticator auth = new Authenticator(model.Password);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(saveUserSql, conn);

                cmd.Parameters.AddWithValue("@firstname", model.FirstName);
                cmd.Parameters.AddWithValue("@lastname", model.LastName);
                cmd.Parameters.AddWithValue("@username", model.UserName);
                cmd.Parameters.AddWithValue("@password", auth.Hash);
                cmd.Parameters.AddWithValue("@salt", auth.Salt);

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows == 1)
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
            string getUsernameSql = @"select users.firstName, users.lastName, users.userName, users.password, users.isadmin 
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
                LastName = Convert.ToString(reader["lastname"]),

                IsAdministrator = false,

                Password = Convert.ToString(reader["password"]),
                Salt = Convert.ToString(reader["salt"])


            };

            if (Convert.ToInt32(reader["isadmin"]) == 1)
            {
                user.IsAdministrator = true;
            }

            return user;
        }

        private UserCredentials AssembleCredentials(SqlDataReader reader)
        {
            return new UserCredentials()
            {
                Username = Convert.ToString(reader["username"]),
                Hash = Convert.ToString(reader["password"]),
                Salt = Convert.ToString(reader["salt"])
            };
        }

        private bool usernameExists(string username)
        {
            bool hasRows = false;
            string userExistsSql = @"select id from users where username = @username;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(userExistsSql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                hasRows = cmd.ExecuteReader().HasRows;

                conn.Close();
            }
            return hasRows;

        }

        private UserCredentials GetUserCredentials(string username)
        {
            string userCredSql = @"select username, password, salt from users where username = @username;";

            UserCredentials cred = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(userCredSql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cred = AssembleCredentials(sdr);
                }

                conn.Close();
            }

            return cred;
        }

        private UserRole AssembleUserRole(SqlDataReader reader)
        {
            return new UserRole()
            {
                Username = Convert.ToString(reader["username"]),
                IsAdministrator = (Convert.ToInt32(reader["isadmin"]) == 1)
            };
        }

        public UserRole GetUserRole(string username)
        {
            string getUserRoleSql = @"select username, isadmin from users where username = @username;";

            UserRole userRole = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(getUserRoleSql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    userRole = AssembleUserRole(sdr);
                }

                conn.Close();
            }

            return userRole;
        }

        public UserProfile GetUserProfile(string username)
        {
            string userDetailSql = @"select id, username, firstname, lastname from users where (username = @username);";
            string userLeaguesSql = @"select l.id, l.name'leagueName', u.username'orgUsername', u.firstname'orgFirstName', u.lastname'orgLastName', c.name'courseName' from users_leagues ul inner join leagues l on (l.id = ul.leagueId) inner join users u on (u.id = ul.userId) inner join courses c on (l.courseId = c.id) where ul.userId = @userId;";
            string userMatchesSql = @"select m.id, m.date, um.score, l.name'leagueName', c.name'courseName', c.par, c.holeCount from users_matches as um inner join matches m on (m.id = um.matchId) inner join leagues_matches lm on (lm.matchId = um.matchId) inner join leagues l on (l.id = lm.leagueId) inner join courses c on (c.id = l.courseId) where userId = @userId;";

            UserProfile profile = new UserProfile();

            using(SqlConnection sqlC = new SqlConnection(connectionString))
            {
                sqlC.Open();
                SqlCommand cmd = new SqlCommand(userDetailSql, sqlC);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    profile.Id = Convert.ToInt32(sdr["id"]);
                    profile.Username = Convert.ToString(sdr["username"]);
                    profile.FirstName = Convert.ToString(sdr["firstname"]);
                    profile.LastName = Convert.ToString(sdr["lastname"]);
                }

                sdr.Close();
                sdr.Dispose();
                cmd.Dispose();

                cmd = new SqlCommand(userLeaguesSql, sqlC);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    profile.Leagues.Add(ReadLeague(sdr));
                }

                sdr.Close();
                sdr.Dispose();
                cmd.Dispose();

                cmd = new SqlCommand(userMatchesSql, sqlC);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    profile.Scores.Add(ReadScoredMatch(sdr));
                }

                sqlC.Close();
            }

            return profile;
        }

        private ScoredMatch ReadScoredMatch(SqlDataReader reader)
        {
            return new ScoredMatch()
            {
                ID = Convert.ToInt32(reader["id"])
            };
        }
        private League ReadLeague(SqlDataReader reader)
        {
            return new League()
            {
                ID = Convert.ToInt32(reader["id"]),
                
            };
        }

        private class Authenticator
        {
            private static int length = 24;
            private static int saltSize = 24;
            private static int iterations = 100;

            public string Hash { get; }
            public string Salt { get; }

            public Authenticator(string password)
            {
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, saltSize, iterations);
                this.Hash = Convert.ToBase64String(rfc.GetBytes(length));
                this.Salt = Convert.ToBase64String(rfc.Salt);
            }

            public Authenticator(string dbSalt, string dbHash)
            {
                this.Salt = dbSalt;
                this.Hash = dbHash;
            }

            public bool AssertValidPassword(string password)
            {
                byte[] saltBytes = Convert.FromBase64String(this.Salt);

                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, saltBytes, iterations);

                string hash = Convert.ToBase64String(rfc.GetBytes(length));
                return string.Equals(this.Hash, hash);
            }

            public bool AreTheseEqual()
            {
                bool verdict = false;

                string pass = "qwerty1234";

                Rfc2898DeriveBytes rfc01 = new Rfc2898DeriveBytes(pass, saltSize, iterations);

                Rfc2898DeriveBytes rfc02 = new Rfc2898DeriveBytes(pass, rfc01.Salt, iterations);

                string hash01 = Convert.ToBase64String(rfc01.GetBytes(length));
                string hash02 = Convert.ToBase64String(rfc02.GetBytes(length));
                verdict = string.Equals(hash01, hash02);

                return verdict;
            }
        }

    }
}