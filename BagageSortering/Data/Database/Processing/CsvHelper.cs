using BagageSortering.Data.Database.Models;
using BagageSortering.Data.Models;
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

        private string FolderPath = "C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Csv/";
        //private string FolderPath = "D:/dev/school/ZBC_H2_Baggage/BagageSortering/Data/Database/Csv/";
        private string airports_FN = "Airports.csv";
        private string TerminalGates_FN = "AirportTerminalGates.csv";
        private string Flights_FN = "Flights.csv";
        private string PassengerReservations_FN = "PassengerReservations.csv";
        private string AirlineInformation_FN = "Companies.csv";
        private string Passengers_FN = "Passengers.csv";
        private string Reservations_FN = "Reservations.csv";
    


        public void AddToReservations(List<Reservation> reservations)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            using (var stream = File.Open(Path.Combine(FolderPath, Reservations_FN), FileMode.Append))
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

            using (var stream = File.Open(Path.Combine(FolderPath, PassengerReservations_FN), FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(preservations);
            }
        }

        public void AddFlights(List<FlightData> flights)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            using (var stream = File.Open(Path.Combine(FolderPath, Flights_FN), FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(flights);
            }
        }

        public void AddToPassengers(List<Passenger> passengers)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            using (var stream = File.Open(Path.Combine(FolderPath, Passengers_FN), FileMode.Append))
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
                using (var reader = new StreamReader(Path.Combine(FolderPath, Reservations_FN)))
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
                using (var reader = new StreamReader(Path.Combine(FolderPath, PassengerReservations_FN)))
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

        public List<AirlineInformation> LoadAirlines()
        {
            try
            {
                using (var reader = new StreamReader(Path.Combine(FolderPath, AirlineInformation_FN)))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<AirlineInformation>();

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
                using (var reader = new StreamReader(Path.Combine(FolderPath, Passengers_FN)))
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
                using (var reader = new StreamReader(Path.Combine(FolderPath, Reservations_FN)))
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
                using (var reader = new StreamReader(Path.Combine(FolderPath, TerminalGates_FN)))
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
                using (var reader = new StreamReader(Path.Combine(FolderPath, airports_FN)))
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
                using (var reader = new StreamReader(Path.Combine(FolderPath, Flights_FN)))
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
