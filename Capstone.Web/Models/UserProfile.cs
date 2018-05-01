﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ScoredMatch> Scores { get; set; }
        public List<League> Leagues { get; set; }

        public UserProfile()
        {
            Scores = new List<ScoredMatch>();
            Leagues = new List<League>();
        }
    }
}