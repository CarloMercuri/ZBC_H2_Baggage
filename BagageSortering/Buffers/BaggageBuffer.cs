using BagageSortering.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering
{
    public class BaggageBuffer
    {
        private Queue<Baggage> entryQueue;

        private bool itemsPresent;

        public bool ItemsPresent
        {
            get { return itemsPresent; }
            set { itemsPresent = value; }
        }
        
        public BaggageBuffer()
        {           
            entryQueue = new Queue<Baggage>();
        }

        /// <summary>
        /// Check if it is full. Unused for now.
        /// </summary>
        /// <returns></returns>
        private bool IsFull()
        {
            return false;
        }

        /// <summary>
        /// Gets the number of bottles contained in the buffer
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
          return entryQueue.Count;
        }

        /// <summary>
        /// Attempts to insert a bottle in the buffer
        /// </summary>
        /// <param name="bottle"></param>
        /// <returns></returns>
        public bool TryInsertProduct(Baggage baggage)
        {
           // For now it always succeeds.

            entryQueue.Enqueue(baggage);

            return true;
        }

        public Baggage[] GetContents()
        {
            return entryQueue.ToArray();
        }

        /// <summary>
        /// Attempts to return the first occurrence of a baggage.
        /// </summary>
        /// <param name="foundProduct"></param>
        /// <returns></returns>
        public bool TryGetBaggage(out Baggage foundProduct)
        {
            if(entryQueue.TryDequeue(out foundProduct))
            {
                return true;
            }

            return false;
        }

        public Baggage GetFirstInQueue()
        {
            return entryQueue.Peek();
        }
        
    }
}
