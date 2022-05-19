using BagageSortering.Data.Database.Models;
using BagageSortering.Data.Database.Processing;
using BagageSortering.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BagageSortering.Airportcontrol
{
    public sealed class AirportManager
    {
        private static AirportManager instance = null;
        private static readonly object padlock = new object();
        private AirportDataProcessor dataProcessor;

        public static AirportManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AirportManager();
                    }
                    return instance;
                }
            }
        }

        private AirportManager()
        {
            dataProcessor = new AirportDataProcessor();
        }

        public List<FlightData> GetFlightsList()
        {
            return dataProcessor.GetFlightsList();
        }

        public string GetAirportNameFromCode(string airportCode)
        {
            return dataProcessor.GetAirportNameFromCode(airportCode);
        }

        public int GetTerminalNumber(string gate)
        {
            return dataProcessor.GetTerminalNumber(gate);
        }

        public void GenerateFlights(DateTime startDT)
        {
            Random rand = new Random();
            List<Airport> airports = dataProcessor.GetAirports();
            Dictionary<string, int> terminalGates = dataProcessor.GetTerminalGates();
            List<FlightData> flights = new List<FlightData>();

            for (int i = 0; i < 30; i++)
            {
                FlightData flight = new FlightData();
                flight.FlightNumber = GenerateRandomFlightNumber();
                flight.DepartureAirportCode = airports[rand.Next(0, airports.Count)].AirportCode;

                bool arrivalFound = false;
                string arrivalCode = "";

                while (!arrivalFound)
                {
                    arrivalCode = airports[rand.Next(0, airports.Count)].AirportCode;
                    if(arrivalCode != flight.DepartureAirportCode)
                    {
                        arrivalFound = true;
                    }
                }

                flight.ArrivalAirportCode = arrivalCode;

                string terminalGate = rand.Next(0, 2) == 0 ? "D" : "B";

                flight.DepartureGate = terminalGate + rand.Next(1, 9).ToString();
                flight.ArrivalGate = "n/a";
                flight.DepartureTime = startDT.ToString("yyyy-MM-dd HH:mm");
                flight.ArrivalTime = startDT.AddMinutes(rand.Next(45, 240)).ToString("yyy-MM-dd HH:mm");
                flight.MaxPassengers = rand.Next(130, 256);
                flight.Status = "";

                flights.Add(flight);

                startDT = startDT.AddMinutes(rand.Next(3, 30));
            }

            dataProcessor.AddFlights(flights);
        }

        public List<FlightData> GetUpcomingFlights()
        {
            List<FlightData> allFlights = dataProcessor.GetFlightsList();

            allFlights.RemoveAll(x => DateTime.Parse(x.DepartureTime) < DateTime.Now);
            List<FlightData> orderedList = allFlights.OrderBy(x => DateTime.Parse(x.DepartureTime)).ToList();

            return orderedList;
        }

        private string GenerateRandomFlightNumber()
        {
            Random rand = new Random();
            List<AirlineInformation> airlines = dataProcessor.GetAirlines();

            AirlineInformation airline = airlines[rand.Next(0, airlines.Count)];

            var sb = new StringBuilder();

            sb.Append(airline.Prefix);

            for (int i = 0; i < 4; i++)
            {
                sb.Append(rand.Next(1, 10).ToString());
            }

            return sb.ToString();
        }

        public int GetBaggageTerminalDestination(int passengerReservationID)
        {
            return dataProcessor.GetBaggageTerminalDestination(passengerReservationID);

        }

        public string GetBaggageDestinationCode(int pResID)
        {
            return dataProcessor.GetBaggageDestinationCode(pResID);
        }

        public void GenerateRandomReservations()
        {
            Random rand = new Random();
            List<PassengerReservation> pReservations = new List<PassengerReservation>();
            List<int> usedPassengersIDs = new List<int>();
            List<Reservation> reservationsFinal = new List<Reservation>();
            int passengerIndex = 0;
            int reservationID = 0;
            List<FlightData> flightsList = dataProcessor.GetFlightsList();

            foreach (FlightData flight in flightsList)
            {
                // Generate a Reservations count, to allow families to group up under the same reservation
                int reservationsCount = rand.Next(flight.MaxPassengers - flight.MaxPassengers / 2, flight.MaxPassengers);

                // Create the reservations and add one passenger per
                for (int i = 0; i < reservationsCount; i++)
                {
                    Reservation r = new Reservation();
                    r.ReservationID = reservationID;
                    r.FlightNumber = flight.FlightNumber;

                    bool done = false;


                    while (!done)
                    {
                        if (!usedPassengersIDs.Contains(passengerIndex))
                        {
                            usedPassengersIDs.Add(passengerIndex);
                            done = true;
                        }

                        passengerIndex++;
                    }

                    PassengerReservation pres = new PassengerReservation();
                    pres.ReservationID = reservationID;
                    pres.PassengerID = passengerIndex;
                    pres.CheckedLuggage = 0;
                    pres.MaxLuggage = rand.Next(1, 4);

                    pReservations.Add(pres);

                    reservationsFinal.Add(r);

                    reservationID++;

                }

                int spotsToFill = flight.MaxPassengers - pReservations.Count;

                foreach (Reservation reservation in reservationsFinal)
                {
                    if (spotsToFill <= 0)
                    {
                        break;
                    }

                    PassengerReservation pres = new PassengerReservation();
                    pres.ReservationID = reservation.ReservationID;
                    pres.PassengerID = passengerIndex;
                    pres.CheckedLuggage = 0;
                    pres.MaxLuggage = rand.Next(1, 4);
                    passengerIndex++;
                    pReservations.Add(pres);
                    spotsToFill--;
                }
            }

            
            dataProcessor.AddToReservations(reservationsFinal);
            dataProcessor.AddToPassengerReservations(pReservations);

        }

        public AirlineInformation GetAirline(string flightNumber)
        {
            return dataProcessor.GetAirline(flightNumber);
        }

        public string GetAirlineLogo(string flightNumber)
        {
            return dataProcessor.GetAirlineLogo(flightNumber);
        }

        public void FillPassengers()
        {
            RandomNameGenerator nameGenerator = new RandomNameGenerator();
            nameGenerator.LoadData();

            List<Passenger> passengers = new List<Passenger>();

            for (int i = 0; i < 300; i++)
            {

                string name = nameGenerator.GenerateRandomFirstName(i % 2 == 0);
                string lastName = nameGenerator.GenerateRandomLastName();
                string tlf = GenerateRandomNumber(8);
                string email = GenerateRandomEmail(name, lastName);
                DateTime birthday = GenerateRandomBirthday();

                Passenger p = new Passenger();
                p.FirstName = name;
                p.LastName = lastName;
                p.Telephone = tlf;
                p.Email = email;
                p.Birthday = birthday.ToString("yyyy-MM-dd");
                p.PassengerID = i;

                passengers.Add(p);
            }
                        
            dataProcessor.AddToPassengers(passengers);
        }

        private DateTime GenerateRandomBirthday()
        {
            Random rnd = new Random();
            DateTime start = new DateTime(1945, 1, 1);
            int range = (new DateTime(2021, 05, 05) - start).Days;
            return start.AddDays(rnd.Next(1, range));
        }

        private string GenerateRandomEmail(string name, string lastName)
        {
            Random rand = new Random();
            string provider = "";
            switch (rand.Next(0, 4))
            {
                case 0:
                    provider = "gmail.com";
                    break;
                case 1:
                    provider = "hotmail.com";
                    break;
                case 2:
                    provider = "yahoo.dk";
                    break;
                case 3:
                    provider = "hotmail.dk";
                    break;

            }

            return $"{name.Substring(0, (name.Length >= 3 ? 3 : name.Length))}.{lastName.Substring(0, (lastName.Length >= 3 ? 3 : lastName.Length))}@{provider}".ToLower();
        }

        /// <summary>
        /// Create a random number as a string with a maximum length.
        /// </summary>
        /// <param name="length">Length of number</param>
        /// <returns>Generated string</returns>
        private string GenerateRandomNumber(int length)
        {
            if (length > 0)
            {
                var sb = new StringBuilder();

                Random rnd = new Random();

                // First number is at least a 4
                sb.Append(rnd.Next(4, 9).ToString());


                for (int i = 0; i < length - 1; i++)
                {
                    sb.Append(rnd.Next(0, 9).ToString());
                }

                return sb.ToString();
            }

            return string.Empty;
        }
    }
}
