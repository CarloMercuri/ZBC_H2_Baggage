using BagageSortering.Data.Database.Models;
using BagageSortering.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Data.Database.Processing
{
    public sealed class AirportDataProcessor
    {
        private List<FlightData> flightsList;
        private List<Airport> airports;
        private List<Reservation> reservations;
        private List<PassengerReservation> passengerReservations;
        private List<Passenger> passengers;
        private List<AirlineInformation> airlines;
        private Dictionary<string, int> terminalGates;

        RandomNameGenerator nameGenerator;
        CsvHelper csvHelper;

        public AirportDataProcessor()
        {
            Initialize();
        }

        private void Initialize()
        {
            csvHelper = new CsvHelper();
            LoadDataInMemory();

        }

        public FlightData GetFlight(string flightNumber)
        {
            return flightsList.Find(x => x.FlightNumber == flightNumber);
        }

        private void AddFlightSeats()
        {
            foreach (FlightData flight in flightsList)
            {
                flight.Seats = new List<AirplaneSeat>();

                for (int row = 0; row < 24; row++)
                {
                    flight.Seats.Add(new AirplaneSeat($"{row + 1}A"));
                    flight.Seats.Add(new AirplaneSeat($"{row + 1}B"));
                    flight.Seats.Add(new AirplaneSeat($"{row + 1}C"));
                    flight.Seats.Add(new AirplaneSeat($"{row + 1}D"));
                    flight.Seats.Add(new AirplaneSeat($"{row + 1}E"));
                    flight.Seats.Add(new AirplaneSeat($"{row + 1}F"));
                }
            }
        }

        public List<FlightData> GetFlightsList()
        {
             return flightsList;
        }

        public string GetAirportNameFromCode(string airportCode)
        {
            Airport found = airports.Find(x => x.AirportCode == airportCode);

            if(found is null)
            {
                return "";
            }
            else
            {
                return found.AirportName;
            }
        }

        public int GetTerminalNumber(string gate)
        {
            return terminalGates[gate];
        }

        public List<AirlineInformation> GetAirlines()
        {
            return airlines;
        }

        public List<Airport> GetAirports()
        {
            return airports;
        }

        public List<Reservation> GetReservations()
        {
            return reservations;
        }

        public List<PassengerReservation> GetPassengerReservations()
        {
            return passengerReservations;
        }

        public Dictionary<string, int> GetTerminalGates()
        {
            return terminalGates;
        }

        public void AddFlights(List<FlightData> flights)
        {
            csvHelper.AddFlights(flights);
        }



        public string GetAirlineLogo(string flightNumber)
        {
            string prefix = flightNumber.Substring(0, 2);

            AirlineInformation airline = airlines.Find(x => x.Prefix == prefix);

            if(airline is null)
            {
                return "LogoNotFound.png";
            }
            else
            {
                return airline.LogoFileName;
            }
        }

        /// <summary>
        /// Retreives the airline company from a flight number, and returns it. 
        /// Returns null if not found.
        /// </summary>
        /// <param name="flightNumber"></param>
        /// <returns></returns>
        public AirlineInformation GetAirline(string flightNumber)
        {
            string prefix = flightNumber.Substring(0, 2);

            return airlines.Find(x => x.Prefix == prefix);
        }

        private string GetReservationID(string passengerReservationID)
        {
            return passengerReservations.Find(x => x.ReservationID == passengerReservationID).ReservationID;
        }

        private string GetFlightNumber(string reservationID)
        {
            return reservations.Find(x => x.ReservationID == reservationID).FlightNumber;
        }

        public int GetBaggageTerminalDestination(string passengerReservationID)
        {
            string resID = GetReservationID(passengerReservationID);
            string flightNr = GetFlightNumber(resID);
            string departureGate = flightsList.Find(x => x.FlightNumber == flightNr).DepartureGate;
            return GetTerminalNumber(departureGate);

        }

        public string GetBaggageDestinationCode(string pResID)
        {
            return flightsList.Find(x => x.FlightNumber == GetFlightNumber(GetReservationID(pResID))).ArrivalAirportCode;
        }


        public void AddToReservations(List<Reservation> reservations)
        {
            csvHelper.AddToReservations(reservations);
        }

        public void AddToPassengerReservations(List<PassengerReservation> pReservations)
        {

            csvHelper.AddToPassengerReservations(pReservations);
        }

        public void AddToPassengers(List<Passenger> passengers)
        {

            csvHelper.AddToPassengers(passengers);
        }

        private void LoadDataInMemory()
        {
            airlines = csvHelper.LoadAirlines();
            flightsList = csvHelper.LoadFlightData();
            AddFlightSeats();
            airports = csvHelper.LoadAirportsData();
            reservations = csvHelper.LoadReservationData();
            passengerReservations = csvHelper.LoadPassengerReservationsData();
            terminalGates = csvHelper.LoadTerminalGatesData();
            passengers = csvHelper.LoadPassengerData();

            Console.WriteLine();
        }
    }
}
