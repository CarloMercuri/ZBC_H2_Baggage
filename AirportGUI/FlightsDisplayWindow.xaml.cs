using AirportGUI.Data;
using AirportGUI.GUIModels;
using BagageSortering.Airportcontrol;
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
            AirportManager processor = AirportManager.Instance;

            Constants constants = new Constants();
            List<FlightData> flights = processor.GetUpcomingFlights(constants.CurrentTime);
            Console.WriteLine();

            for (int i = 0; i < mainGrid.RowDefinitions.Count - 1; i++)
            {
                if (i >= flights.Count) break;
                
                FlightDataDisplay disp = new FlightDataDisplay(flights[i], mainGrid, i + 1);
            }

            Console.WriteLine();
        }
    }
}
