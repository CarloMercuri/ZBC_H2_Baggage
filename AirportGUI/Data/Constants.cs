using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AirportGUI.Data
{
    public class Constants
    {
        public string IconsFolderPath { get; private set; }
        public DateTime CurrentTime = DateTime.Parse("2022-05-19 11:30");

        private static Constants instance = null;
        private static readonly object padlock = new object();

        public static Constants Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Constants();
                    }
                    return instance;
                }
            }
        }

        public Constants()
        {
            string baseDir = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\"));
            IconsFolderPath = Path.Combine(baseDir, "AirportGUI/Resources/Icons");
        }
    }
}
