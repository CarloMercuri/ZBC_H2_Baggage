using BagageSortering.Data.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Data.Database.Processing
{
    public class AirportDataProcessor
    {
        private List<FlightData> flightsList;
        private List<Airport> airports;
        private List<Reservation> reservations;
        private List<PassengerReservation> passengerReservations;
        private List<Passenger> passengers;
        private Dictionary<string, int> terminalGates;

        RandomNameGenerator nameGenerator;



        public AirportDataProcessor()
        {
            
        }

        public void Initialize()
        {
            LoadDataInMemory();
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

        private int GetReservationID(int passengerReservationID)
        {
            return passengerReservations.Find(x => x.ReservationID == passengerReservationID).ReservationID;
        }

        private string GetFlightNumber(int reservationID)
        {
            return reservations.Find(x => x.ReservationID == reservationID).FlightNumber;
        }

        public int GetBaggageTerminalDestination(int passengerReservationID)
        {
            int resID = GetReservationID(passengerReservationID);
            string flightNr = GetFlightNumber(resID);
            string departureGate = flightsList.Find(x => x.FlightNumber == flightNr).DepartureGate;
            return GetTerminalNumber(departureGate);

        }

        public string GetBaggageDestinationCode(int pResID)
        {
            return flightsList.Find(x => x.FlightNumber == GetFlightNumber(GetReservationID(pResID))).ArrivalAirportCode;
        }

        public void GenerateRandomReservations()
        {
            Random rand = new Random();
            List<PassengerReservation> pReservations = new List<PassengerReservation>();
            List<int> usedPassengersIDs = new List<int>();
            List<Reservation> reservationsFinal = new List<Reservation>();
            int passengerIndex = 0;
            int reservationID = 0;

            foreach(FlightData flight in flightsList)
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

            CsvHelper helper = new CsvHelper();
            helper.AddToReservations(reservationsFinal);
            helper.AddToPassengerReservations(pReservations);

        }

        private void CreatePassengerReservation(int reservationID, int passengerID, int checkedLuggage, int maxLuggage)
        {

            PassengerReservation pres = new PassengerReservation();
            pres.ReservationID = reservationID;
            pres.PassengerID = passengerID;
            pres.CheckedLuggage = checkedLuggage;
            pres.MaxLuggage = maxLuggage;
        }

        public void FillPassengers()
        {
            nameGenerator = new RandomNameGenerator();
            nameGenerator.LoadData();

            List<Passenger> passengers = new List<Passenger>();

            for (int i = 0; i < 300; i++)
            {
               
                string name = nameGenerator.GenerateRandomFirstName(i%2 == 0);
                string lastName = nameGenerator.GenerateRandomLastName();
                string tlf = GeneratRandomNumber(8);
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

            CsvHelper helper = new CsvHelper();
            helper.AddToPassengers(passengers);
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
            switch(rand.Next(0, 4))
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

            return $"{name.Substring(0, (name.Length >= 3? 3 : name.Length))}.{lastName.Substring(0, (lastName.Length >= 3 ? 3 : lastName.Length))}@{provider}".ToLower();
        }

        /// <summary>
        /// Create a random number as a string with a maximum length.
        /// </summary>
        /// <param name="length">Length of number</param>
        /// <returns>Generated string</returns>
        private string GeneratRandomNumber(int length)
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

        private void LoadDataInMemory()
        {
            CsvHelper csvHelper = new CsvHelper();

            flightsList = csvHelper.LoadFlightData();
            airports = csvHelper.LoadAirportsData();
            reservations = csvHelper.LoadReservationData();
            passengerReservations = csvHelper.LoadPassengerReservationsData();
            terminalGates = csvHelper.LoadTerminalGatesData();
            passengers = csvHelper.LoadPassengerData();

            Console.WriteLine();
        }
    }
}
