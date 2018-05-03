using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.DALs;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;

namespace Capstone.Web.DALs
{
    public interface GolfSqlDal
    {
        int CreateMatch(Match match);

        bool CreateLeague(League league);

        bool SaveUser(Registration model);

        User GetUsername(string username);

        bool AddNewCourse(Course course);

        bool ValidLogin(Login model);

        List<Course> GetAllCourses();

        UserRole GetUserRole(string username);

        List<UserFace> GetLeaderboardUsernames(int leagueId);

        bool LogMatchScore(LogMatch logmatch);

        UserProfile GetUserProfile(string username);

        Course GetCourseAssociatedWithLeague(int leagueId);

        List<LeaderboardUser> GetLeagueUsers(int leagueId);

        int GetLeagueId(string name);

        bool JoinLeague(UserAndLeague model);

        bool InitLeagueMatch(Match match);
    }
}
