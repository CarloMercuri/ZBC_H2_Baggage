using BagageSortering.Data.Database.Processing;
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
            AirportDataProcessor processor = new AirportDataProcessor();
            processor.Initialize();
            processor.GenerateRandomReservations();

            //processor.GenerateRandomReservations();






            Console.ReadKey();
        }
    }
}
