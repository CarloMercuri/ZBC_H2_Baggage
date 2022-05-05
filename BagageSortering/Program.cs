using BagageSortering.Models;
using CsvHelper;
using System;
using System.Globalization;
using System.IO;

namespace BagageSortering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var reader = new StreamReader("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Database/Csv/Flights.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<FlightData>();

                foreach(FlightData flight in records)
                {
                    Console.WriteLine();
                }

                Console.WriteLine(records);
            }

            

            Console.ReadKey();
        }
    }
}
