using AirportGUI.GUIModels;
using BagageSortering.Data.Database.Models;
using BagageSortering.Data.Database.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AirportGUI
{
    /// <summary>
    /// Interaction logic for FlightsDisplayWindow.xaml
    /// </summary>
    public partial class FlightsDisplayWindow : Window
    {
        public FlightsDisplayWindow()
        {
            InitializeComponent();
            AirportDataProcessor  airportData = new AirportDataProcessor();
            airportData.Initialize();
            List<FlightData> flights = airportData.GetFlightsList();

            for (int i = 0; i < flights.Count; i++)
            {
                FlightDataDisplay disp = new FlightDataDisplay(flights[i], mainGrid, i + 1);
            }

            Console.WriteLine();
        }
    }
}
