﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Leaderboard
    {
        public string LeagueName { get; set; }
        public List<User> Users { get; set; }
        public Course Course { get; set; }
        
    }
}