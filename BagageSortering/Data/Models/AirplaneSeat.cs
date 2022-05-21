using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Data.Database.Models
{
    public class AirplaneSeat
    {
        public AirplaneSeat(string SeatName)
        {
            this.SeatName = SeatName;
        }

        public string SeatName { get; set; }
        public int PersonID { get; set; }
        public string ReservationID { get; set; }
    }
}
