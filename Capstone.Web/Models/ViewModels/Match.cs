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
        public DateTime Reservation
        {
            get
            {
                string one = ReservationDate.ToString();
                string two = ReservationTime.ToString();

                DateTime dt = Convert.ToDateTime(one + " " + two);
                DateTime Reservation = DateTime.ParseExact(one + " " + two, "dd/MM/yy h:mm:ss tt", CultureInfo.InvariantCulture);

                return Reservation;
            }
        }
    }
}