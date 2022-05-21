using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Data.Database.Models
{
    public class Reservation
    {
        private string reservationID;

        public string ReservationID
        {
            get { return reservationID; }
            set { reservationID = value; }
        }

        private string flightNumber;

        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }


    }
}
