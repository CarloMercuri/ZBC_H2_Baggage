using System;
using System.Collections.Generic;
using System.Text;

namespace BagageSortering.Data.Database.Models
{
    public class AirportTerminalGates
    {
        private string gateNr;

        public string GateNr
        {
            get { return gateNr; }
            set { gateNr = value; }
        }

        private int terminal;

        public int Terminal
        {
            get { return terminal; }
            set { terminal = value; }
        }


    }
}
