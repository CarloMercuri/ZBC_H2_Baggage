using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace BagageSortering.Data.Database.Processing
{
    public class Names
    {
        public string[] boys { get; set; }
        public string[] girls { get; set; }
        public string[] last { get; set; }

        public Names()
        {
            boys = new string[] { };
            girls = new string[] { };
            last = new string[] { };
        }
    }

    public class RandomNameGenerator
    {
        private Random rand;
        List<string> males;
        List<string> females;
        List<string> lasts;

        private Names names;

        public void LoadData()
        {
            rand = new Random();
            using (StreamReader reader = new StreamReader("C:/dev/ZBC/Threads/BagageSortering/BagageSortering/Data/Database/Json/Names.json"))
            {
                names = JsonConvert.DeserializeObject<Names>(reader.ReadToEnd());                
            }       
        }

        public string GenerateRandomFirstName(bool male)
        {
            return male ? names.boys[rand.Next(names.boys.Length)] : names.girls[rand.Next(names.girls.Length)];
        }

        public string GenerateRandomLastName()
        {
            return names.last[rand.Next(names.last.Length)];
        }

        public string GenerateRandomNameFull(bool male)
        {
            string name = male? names.boys[rand.Next(names.boys.Length)] : names.girls[rand.Next(names.girls.Length)];
            string surname = names.last[rand.Next(names.last.Length)];

            return $"{name} {surname}";
        }
    }
}
