using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Data.Models
{
    public class AirlineInformation
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string prefix;

        public string Prefix
        {
            get { return prefix; }
            set { prefix = value; }
        }


        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string logoFileName;

        public string LogoFileName
        {
            get { return logoFileName; }
            set { logoFileName = value; }
        }



    }
}
