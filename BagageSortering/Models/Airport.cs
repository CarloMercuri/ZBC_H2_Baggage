using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Models
{
    public class Airport
    {
        private string airportName;

        public string AirportName
        {
            get { return airportName; }
            set { airportName = value; }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        private string airportCode;

        public string AirportCode
        {
            get { return airportCode; }
            set { airportCode = value; }
        }



    }
}
