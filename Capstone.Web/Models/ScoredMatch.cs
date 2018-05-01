using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ScoredMatch
    {
        public int      Id          { get; set; }
        public int      Holes       { get; set; }
        public int      Par         { get; set; }
        public int      Score       { get; set; }
        public string   CourseName  { get; set; }
        public string   LeagueName  { get; set; }
        public DateTime Date        { get; set; }
    }
}