using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Match
    {
        public int ID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumberOfPlayers { get; set; }

        public List<UserFace> leagueUsers { get; set; }
        public DateTime Reservation
        {
            get
            {
                string date = ReservationDate.ToShortDateString();
                string time = ReservationTime.ToShortTimeString();

                DateTime Reservation = DateTime.ParseExact((date + " " + time), "g", new CultureInfo("en-US"));

                return Reservation;
            }
        }
    }
}