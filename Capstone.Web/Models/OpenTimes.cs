using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class OpenTimes
    {
        public int CourseId { get; set; }
        public List<DateTime> AvailableSlots { get; set; }
    }
}