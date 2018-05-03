using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class UserMatch
    {
        public UserFace User        { get; set; }
        public int      Score       { get; set; }   
        public int      MatchId     { get; set; }
        public DateTime MatchDate   { get; set; }
    }
}