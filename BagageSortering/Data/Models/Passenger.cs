using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Data.Database.Models
{
    public class Passenger
    {
        private int passengerID;

        public int PassengerID
        {
            get { return passengerID; }
            set { passengerID = value; }
        }


        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string telephone;

        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string birthday;

        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }





    }
}
