using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class Dashboard
    {
        public List<Course> courses { get; set; }

        public UserProfile Profile { get; set; }
        public List<League> leagues { get; set; }

    }
}