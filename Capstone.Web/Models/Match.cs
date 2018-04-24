using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Match
    {
        public int ID { get; set; }
        public List<Player> Players { get; set; }
        public DateTime ReservationDateAndTime { get; set; }
        public Course Course { get; set; }
        public League League { get; set; }
    }
}