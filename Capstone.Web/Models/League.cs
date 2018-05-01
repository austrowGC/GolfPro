using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class League
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int OrganizerId { get; set; }
        public string OrganizerFirstName { get; set; }
        public string OrganizerLastName { get; set; }
        public string OrganizerUsername { get; set; }
        public int CourseId { get; set; }
        public List<Course> courses { get; set; }
        public string UserName { get; set; }
        public string CourseName { get; set; }
    }
}