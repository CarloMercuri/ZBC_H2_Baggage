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
                flight.MaxPassengers = 144;
                flight.Status = "";

                flights.Add(flight);

                startDT = startDT.AddMinutes(rand.Next(3, 30));
            }

            dataProcessor.AddFlights(flights);
        }

        public List<FlightData> GetUpcomingFlights(DateTime nowtime)
        {
            List<FlightData> allFlights = dataProcessor.GetFlightsList();

            allFlights.RemoveAll(x => DateTime.Parse(x.DepartureTime) < nowtime);
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

        public int GetBaggageTerminalDestination(string passengerReservationID)
        {
            return dataProcessor.GetBaggageTerminalDestination(passengerReservationID);

        }

        public string GetBaggageDestinationCode(string pResID)
        {
            return dataProcessor.GetBaggageDestinationCode(pResID);
        }

        public List<Reservation> GetReservationsForFlight(string flightNumber)
        {
            List<Reservation> allReservations = dataProcessor.GetReservations();
            List<Reservation> returnList = new List<Reservation>();

            foreach(Reservation res in allReservations)
            {
                if(res.FlightNumber == flightNumber)
                {
                    returnList.Add(res);
                }
            }

            return returnList;
        }

        public List<PassengerReservation> GetPassengersForReservation(string reservationID)
        {
            List<PassengerReservation> passengers = dataProcessor.GetPassengerReservations();
            List<PassengerReservation> returnList = new List<PassengerReservation>();

            foreach(PassengerReservation res in passengers)
            {
                if(res.ReservationID == reservationID)
                {
                    returnList.Add(res);
                }
            }

            return returnList;
        }

        public FlightData GetFlight(string flightNumber)
        {
            return dataProcessor.GetFlight(flightNumber);
        }

        public List<AirplaneSeat> GetFlightSeats(string flightNumber)
        {
            List<Reservation> reservations = GetReservationsForFlight(flightNumber);
            List<PassengerReservation> passengerReservations = new List<PassengerReservation>();

            foreach(Reservation r in reservations)
            {
                passengerReservations.AddRange(GetPassengersForReservation(r.ReservationID));
            }

            List<AirplaneSeat> seats = new List<AirplaneSeat>();

            foreach(PassengerReservation r in passengerReservations)
            {
                AirplaneSeat seat = new AirplaneSeat(r.SeatName);
                seat.ReservationID = r.ReservationID;
                seat.PersonID = r.PassengerID;
                seats.Add(seat);
            }

            Console.WriteLine();

            return seats;
        }

        /// <summary>
        /// Totally not secure 
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomReservationNumber()
        {
            Random rand = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        public void GenerateRandomReservations()
        {
            Random rand = new Random();
            List<PassengerReservation> pReservations = new List<PassengerReservation>();
            List<int> usedPassengersIDs = new List<int>();
            List<Reservation> reservationsFinal = new List<Reservation>();
            List<Reservation> flightReservations = new List<Reservation>();
            int passengerIndex = 0;
            List<FlightData> flightsList = dataProcessor.GetFlightsList();

            foreach (FlightData flight in flightsList)
            {
                int seatIndex = 0;
                flightReservations.Clear();

                // Generate a Reservations count, to allow families to group up under the same reservation
                int reservationsCount = rand.Next(flight.MaxPassengers - flight.MaxPassengers / 2, flight.MaxPassengers);

                // Create the reservations and add one passenger per
                for (int i = 0; i < reservationsCount; i++)
                {
                    Reservation r = new Reservation();

                    string resID = GenerateRandomReservationNumber();

                    // Totally unnecessary I'm sure
                    while(reservationsFinal.Find(x => x.ReservationID == resID) != null)
                    {
                        resID = GenerateRandomReservationNumber();
                    }

                    r.ReservationID = resID;

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
                    pres.ReservationID = resID;
                    pres.PassengerID = passengerIndex;
                    pres.CheckedLuggage = 0;
                    pres.MaxLuggage = rand.Next(1, 4);
                    flight.Seats[seatIndex].ReservationID = resID;
                    flight.Seats[seatIndex].PersonID = passengerIndex;
                    pres.SeatName = seatIndex.ToString();
                    pReservations.Add(pres);

                    flightReservations.Add(r);

                    seatIndex++;

                }

                int spotsToFill = flight.MaxPassengers - reservationsCount;

                foreach (Reservation reservation in flightReservations)
                {
                    if (spotsToFill <= 0 || seatIndex > 143)
                    {
                        break;
                    }

                    if(rand.Next(0, 11) < 6)
                    {
                        PassengerReservation pres = new PassengerReservation();
                        pres.ReservationID = reservation.ReservationID;
                        pres.PassengerID = passengerIndex;
                        pres.CheckedLuggage = 0;
                        pres.MaxLuggage = rand.Next(1, 4);
                        flight.Seats[seatIndex].ReservationID = reservation.ReservationID;
                        flight.Seats[seatIndex].PersonID = passengerIndex;
                        pres.SeatName = seatIndex.ToString();
                        passengerIndex++;
                        seatIndex++;
                        pReservations.Add(pres);
                    }
                    
                    spotsToFill--;
                    
                }

                reservationsFinal.AddRange(flightReservations);
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
