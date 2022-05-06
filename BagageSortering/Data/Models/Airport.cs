using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Data.Database.Models
{
    public class Airport
    {
        //AirportName,Country,AirportCode

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
