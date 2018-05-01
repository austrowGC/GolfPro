using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsOrganizer { get; set; }
    }
}