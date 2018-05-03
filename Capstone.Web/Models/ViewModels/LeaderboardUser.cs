using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class LeaderboardUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalMatches { get; set; }
        public int NumberOfHoles { get; set; }
        public int TotalStrokes { get; set; }
        public double AverageScore
        {
            get
            {
                double average = 0;

                average = ((TotalStrokes/(NumberOfHoles*TotalMatches)) * 18);

                return average;
            }
        }
    }
}