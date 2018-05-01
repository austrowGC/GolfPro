using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class DashboardStats
    {
        public string Best18    { get; set; }
        public string Best9     { get; set; }
        public string Average18 { get; set; }
        public UserProfile User { get; set; }
    }
}