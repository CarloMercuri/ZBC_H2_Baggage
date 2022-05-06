using BagageSortering.Data.Database.Models;
using BagageSortering.Errors;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BagageSortering.Data.Database.Processing
{
    public class CsvHelper
    {
        public void Test()
        {
            using (var reader = new StreamReader("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Database/Csv/Flights.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<FlightData>();

                foreach (FlightData flight in records)
                {
                    Console.WriteLine();
                }

                Console.WriteLine(records);
            }
        }

        public void AddToReservations(List<Reservation> reservations)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            using (var stream = File.Open("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/Reservations.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(reservations);
            }
        }

        public void AddToPassengerReservations(List<PassengerReservation> preservations)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            using (var stream = File.Open("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/PassengerReservations.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(preservations);
            }
        }

        public void AddToPassengers(List<Passenger> passengers)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            using (var stream = File.Open("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/Passengers.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(passengers);
            }
        }

        public List<Reservation> LoadReservationsData()
        {
            try
            {
                using (var reader = new StreamReader("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/Reservations.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Reservation>();

                    return Enumerable.ToList(records);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger log = new ErrorLogger();
                log.LogError(ex);
                return null;
            }

        }

        public List<PassengerReservation> LoadPassengerReservationsData()
        {
            try
            {
                using (var reader = new StreamReader("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/PassengerReservations.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<PassengerReservation>();

                    return Enumerable.ToList(records);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger log = new ErrorLogger();
                log.LogError(ex);
                return null;
            }

        }

        public List<Passenger> LoadPassengerData()
        {
            try
            {
                using (var reader = new StreamReader("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/Passengers.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Passenger>();

                    return Enumerable.ToList(records);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger log = new ErrorLogger();
                log.LogError(ex);
                return null;
            }

        }

        public List<Reservation> LoadReservationData()
        {
            try
            {
                using (var reader = new StreamReader("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/Reservations.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Reservation>();

                    return Enumerable.ToList(records);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger log = new ErrorLogger();
                log.LogError(ex);
                return null;
            }

        }

        public Dictionary<string, int> LoadTerminalGatesData()
        {
            try
            {
                using (var reader = new StreamReader("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/AirportTerminalGates.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<AirportTerminalGates>();
                    Dictionary<string, int> returnDict = new Dictionary<string, int>();

                    foreach(AirportTerminalGates couple in records)
                    {
                        returnDict.Add(couple.GateNr, couple.Terminal);
                    }

                    return returnDict;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger log = new ErrorLogger();
                log.LogError(ex);
                return null;
            }

        }

        public List<Airport> LoadAirportsData()
        {
            try
            {
                using (var reader = new StreamReader("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/Airports.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Airport>();

                    return Enumerable.ToList(records);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger log = new ErrorLogger();
                log.LogError(ex);
                return null;
            }

        }

        public List<FlightData> LoadFlightData()
        {
            try
            {
                using (var reader = new StreamReader("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/Flights.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<FlightData>();

                    return Enumerable.ToList(records);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger log = new ErrorLogger();
                log.LogError(ex);
                return null;
            }
           
        }
    }
}
