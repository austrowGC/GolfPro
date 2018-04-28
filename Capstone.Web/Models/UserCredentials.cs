using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}