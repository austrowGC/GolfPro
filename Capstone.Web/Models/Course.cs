using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumberOfHoles { get; set; }
        public int LengthInYards { get; set; }
        public int Par { get; set; }
    }
}