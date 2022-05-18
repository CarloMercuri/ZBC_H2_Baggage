using BagageSortering.Data.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AirportGUI.GUIModels
{
    public class FlightDataDisplay
    {
        public FlightDataDisplay(FlightData flight, Grid parentGrid, int row)
        {
            TextBlock tBox_flightNumber = new TextBlock();
            tBox_flightNumber.Text = flight.FlightNumber;
            tBox_flightNumber.Style = Application.Current.TryFindResource("style_FlightDisplayYellow") as Style;

            parentGrid.Children.Add(tBox_flightNumber);
            Grid.SetRow(tBox_flightNumber, row);
            Grid.SetColumn(tBox_flightNumber, 1);

            //
            // Logo
            //

            Image logo = new Image();
            //logo.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/logo_Alitalia.jpg"));
            //Image img = Properties.Resources.logo_Alitalia;
            //logo.Source = Properties.Resources.logo_Alitalia as ImageSource; 
            parentGrid.Children.Add(logo);
            Grid.SetRow(logo, row);
            Grid.SetColumn(logo, 0);

            //
            // Destination Airport Name
            //

            TextBlock tBox_Destination = new TextBlock();
            tBox_Destination.Text = flight.ArrivalAirportCode;
            tBox_Destination.Style = Application.Current.TryFindResource("style_FlightDisplayWhite") as Style;

            parentGrid.Children.Add(tBox_Destination);
            Grid.SetRow(tBox_Destination, row);
            Grid.SetColumn(tBox_Destination, 2);

            //
            // Arrival Time
            //

            TextBlock tBox_ArrivalTime = new TextBlock();
            tBox_ArrivalTime.Text = flight.ArrivalTime.ToString("hh:mm");
            tBox_ArrivalTime.Style = Application.Current.TryFindResource("style_FlightDisplayYellow") as Style;

            parentGrid.Children.Add(tBox_ArrivalTime);
            Grid.SetRow(tBox_ArrivalTime, row);
            Grid.SetColumn(tBox_ArrivalTime, 3);

            // 
            // Gate
            //

            TextBlock tBox_DepartureGate = new TextBlock();
            tBox_DepartureGate.Text = flight.DepartureGate;
            tBox_DepartureGate.Style = Application.Current.TryFindResource("style_FlightDisplayWhite") as Style;

            parentGrid.Children.Add(tBox_DepartureGate);
            Grid.SetRow(tBox_DepartureGate, row);
            Grid.SetColumn(tBox_DepartureGate, 4);

            //
            // Status
            //

            TextBlock tbox_Status = new TextBlock();
            tbox_Status.Text = flight.Status;            
            tbox_Status.Style = Application.Current.TryFindResource("style_FlightDisplayYellow") as Style;

            parentGrid.Children.Add(tbox_Status);
            Grid.SetRow(tbox_Status, row);
            Grid.SetColumn(tbox_Status, 5);

        }
    }
}
