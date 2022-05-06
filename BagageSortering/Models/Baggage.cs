using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Models
{
    public class Baggage
    {
        private int passengerReservation;

        public int PassengerReservation
        {
            get { return passengerReservation; }
            set { passengerReservation = value; }
        }

        private string chipCode;

        public string ChipCode
        {
            get { return chipCode; }
            set { chipCode = value; }
        }


    }
}
