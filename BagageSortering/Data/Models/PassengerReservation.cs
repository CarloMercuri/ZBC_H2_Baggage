﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Data.Database.Models
{
    public class PassengerReservation
    {
        //ReservationID,Name,Surname,MaxLuggage,CheckedLuggage

        private int reservationID;

        public int ReservationID
        {
            get { return reservationID; }
            set { reservationID = value; }
        }

        private int passengerID;

        public int PassengerID
        {
            get { return passengerID; }
            set { passengerID = value; }
        }


        private int maxLuggage;

        public int MaxLuggage
        {
            get { return maxLuggage; }
            set { maxLuggage = value; }
        }

        private int checkedLuggage;

        public int CheckedLuggage
        {
            get { return checkedLuggage; }
            set { checkedLuggage = value; }
        }







    }
}