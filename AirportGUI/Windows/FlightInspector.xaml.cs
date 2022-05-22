using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AirportGUI.Data;
using BagageSortering.Airportcontrol;
using BagageSortering.Data.Database.Models;

namespace AirportGUI.Windows
{
    /// <summary>
    /// Interaction logic for FlightInspector.xaml
    /// </summary>
    public partial class FlightInspector : Window
    {
        Constants constants = new Constants();
        AirportManager manager = AirportManager.Instance;
        string selectedSeatIndex = "";
        string emptySeatFileName = "seat_Empty.png";
        string bookedSeatFileName = "seat_Taken.png";

        public FlightInspector(string flightNumber)
        {
            InitializeComponent();

            List<AirplaneSeat> rawSeats =  manager.GetFlightSeats("AS4732");
            FlightData flight = manager.GetFlight(flightNumber);

            foreach (AirplaneSeat seat in rawSeats)
            {
                flight.Seats[Convert.ToInt32(seat.SeatName)].ReservationID = seat.ReservationID;
                flight.Seats[Convert.ToInt32(seat.SeatName)].PersonID = seat.PersonID;
            }

            if(flight == null)
            {
                throw new Exception("No flight found");
            }

            int seatIndex = 0;

            for (int row = 0; row < 24; row++)
            {
                for (int seat = 0; seat < 7; seat++)
                {
                    if (seat == 3) continue;

                    Image img = new Image();

                    string imageName = "";
                    if(flight.Seats[seatIndex].ReservationID == "" || flight.Seats[seatIndex].ReservationID == null)
                    {
                        img.Source = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Empty.png")));
                        img.Name = "EmptySeat";
                    }
                    else
                    {
                        img.Source = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Taken.png")));
                        img.Name = "TakenSeat";
                    }
                    
                    
                    img.MouseEnter += Img_MouseEnter;
                    img.MouseLeave += Img_MouseLeave;
                    Grid.SetRow(img, row);
                    Grid.SetColumn(img, seat);
                    seatsGrid.Children.Add(img);

                    seatIndex++;
                }
            }
        }

        private void Img_MouseLeave(object sender, MouseEventArgs e)
        {
            Image img = (Image)sender;
            if(img.Name == "EmptySeat")
            {
                img.Source = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Empty.png")));
            }
            else
            {
                img.Source = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Taken.png")));
            }            
        }

        private void Img_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = (Image)sender;
            if (img.Name == "EmptySeat")
            {
                img.Source = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Highlight.png")));
            }
            else
            {
                img.Source = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Taken_Highlight.png")));
            }
        }
    }
}
