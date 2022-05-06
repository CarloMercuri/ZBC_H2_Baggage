using BagageSortering.Data.Database.Models;
using BagageSortering.Data.Database.Processing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.BaggageSorting
{
    public class BaggageProcessor
    {
        private AirportDataProcessor mainProcessor;

        public BaggageProcessor(AirportDataProcessor mainDataProcessor)
        {
            mainProcessor = mainDataProcessor;
        }
        public string GenerateBaggageCode(PassengerReservation reservation)
        {
            string destination = 
        }
    }
}
