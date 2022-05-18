using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Data.Database.Models
{
    public class FlightData
    {
        private string flightNumber;

        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }

        private string departureAirportCode;

        public string DepartureAirportCode
        {
            get { return departureAirportCode; }
            set { departureAirportCode = value; }
        }

        private string arrivalAirportCode;

        public string ArrivalAirportCode
        {
            get { return arrivalAirportCode; }
            set { arrivalAirportCode = value; }
        }

        private string departureGate;

        public string DepartureGate
        {
            get { return departureGate; }
            set { departureGate = value; }
        }

        private string arrivalGate;

        public string ArrivalGate
        {
            get { return arrivalGate; }
            set { arrivalGate = value; }
        }


        private DateTime departureTime;

        public DateTime DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }

        private DateTime arrivalTime;

        public DateTime ArrivalTime
        {
            get { return arrivalTime; }
            set { arrivalTime = value; }
        }

        private int maxPassengers;

        public int MaxPassengers
        {
            get { return maxPassengers; }
            set { maxPassengers = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }


    }
}
