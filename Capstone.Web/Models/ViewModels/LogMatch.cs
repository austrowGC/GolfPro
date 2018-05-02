using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class LogMatch
    {
        public List<UserFace> leagueUsers { get; set; }
        public Match match { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Score must be a natural number")]
        public int score { get; set; }
    }
}