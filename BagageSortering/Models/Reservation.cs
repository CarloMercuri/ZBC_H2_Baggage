using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Models
{
    internal class Reservation
    {
        private int reservationID;

        public int ReservationID
        {
            get { return reservationID; }
            set { reservationID = value; }
        }

        private List<Passenger> passengersList;

        public List<Passenger> PassengersList
        {
            get { return passengersList; }
            set { passengersList = value; }
        }

        private string flightNumber;

        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }

        private int checkedBaggagesAllowed;

        public int CheckedBaggagesAllowed
        {
            get { return checkedBaggagesAllowed; }
            set { checkedBaggagesAllowed = value; }
        }


    }
}
