using BagageSortering.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BagageSortering.Locations
{
    public class BaggageSortingHub
    {
        private BaggageBuffer entryBuffer;

        private bool working;

        public BaggageSortingHub()
        {
            working = true;
            entryBuffer = new BaggageBuffer();
        }

        public bool IntakeBaggage(Baggage bag)
        {
            if (entryBuffer.TryInsertProduct(bag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void ProcessingMain()
        {
            while (working)
            {
                Thread.Sleep(100);
            }
        }
    }
}
