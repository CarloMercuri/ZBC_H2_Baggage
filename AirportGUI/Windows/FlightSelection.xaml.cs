using AirportGUI.Data;
using AirportGUI.Models;
using BagageSortering.Airportcontrol;
using BagageSortering.Data.Database.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace AirportGUI.Windows
{
    /// <summary>
    /// Interaction logic for FlightSelection.xaml
    /// </summary>
    public partial class FlightSelection : Window
    {
        AirportManager manager = AirportManager.Instance;
        Constants constants = new Constants();

        public FlightSelection()
        {
            InitializeComponent();

            List<FlightData> Flights = manager.GetFlightsList();

            List<FlightGuiData> FlightGuiDatas = new List<FlightGuiData>();

            foreach (FlightData flight in Flights)
            {
                // Only allowed to check in flights that are within 3 hours
                if (DateTime.Parse(flight.DepartureTime) > constants.CurrentTime.AddHours(8)) continue;

                FlightGuiData gui = new FlightGuiData();
                gui.ArrivalGate = flight.ArrivalGate;
                gui.DepartureGate = flight.DepartureGate;
                gui.ArrivalTime = flight.ArrivalTime;
                gui.DepartureTime = flight.DepartureTime;
                gui.FlightNumber = flight.FlightNumber;
                gui.ArrivalAirportCode = flight.ArrivalAirportCode;
                gui.DepartureAirportCode = flight.DepartureAirportCode;
                gui.MaxPassengers = flight.MaxPassengers;
                gui.Status = flight.Status;
                gui.CompanyLogo = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, manager.GetAirlineLogo(flight.FlightNumber))));
                FlightGuiDatas.Add(gui);
            }

            flightsListView.ItemsSource = FlightGuiDatas;
        }
    }
}
