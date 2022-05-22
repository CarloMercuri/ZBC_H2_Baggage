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
        FlightData flight;
        int selectedSeatIndex = 0;
        string emptySeatFileName = "seat_Empty.png";
        string bookedSeatFileName = "seat_Taken.png";
        AirplaneSeat selectedSeat;

        public FlightInspector(string flightNumber)
        {
            InitializeComponent();

            info_Seat.Visibility = Visibility.Hidden;
            info_Status.Visibility = Visibility.Hidden;
            info_Reservation.Visibility = Visibility.Hidden;
            info_Person.Visibility = Visibility.Hidden;
            info_CheckedIn.Visibility = Visibility.Hidden;

            List<AirplaneSeat> rawSeats =  manager.GetFlightSeats(flightNumber);
            flight = manager.GetFlight(flightNumber);

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

                    Button btn = new Button();

                    string imageName = "";
                    var brush = new ImageBrush();
                    if (flight.Seats[seatIndex].ReservationID == "" || flight.Seats[seatIndex].ReservationID == null)
                    {                        
                        brush.ImageSource = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Empty.png")));
                        btn.Background = brush;
                        btn.Content = $"Empty:{seatIndex}";
                        btn.ContentStringFormat = "\n\r id={0}";
                    }
                    else
                    {
                        brush.ImageSource = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Taken.png")));
                        btn.Background = brush;
                        btn.Content = $"Taken:{seatIndex}";
                        btn.ContentStringFormat = "\n\r id={0}";
                    }
                    
                    
                    btn.MouseEnter += Btn_MouseEnter;
                    btn.MouseLeave += Btn_MouseLeave;
                    btn.Click += Btn_Click;

                    Grid.SetRow(btn, row);
                    Grid.SetColumn(btn, seat);
                    seatsGrid.Children.Add(btn);

                    seatIndex++;
                }
            }
        }

        private void UpdateInfo()
        {
            AirplaneSeat seat = flight.Seats[selectedSeatIndex];
            info_Seat.Text = $"Seat: {seat.SeatName}";
            info_Status.Visibility = Visibility.Visible;            
            info_Start.Visibility = Visibility.Hidden;

            if(seat.ReservationID == "" || seat.ReservationID is null)
            {
                info_Status.Text = "Status: Not Booked";
                info_Person.Visibility = Visibility.Hidden;                
                info_CheckedIn.Visibility = Visibility.Hidden;
                info_Reservation.Visibility = Visibility.Hidden;
            }
            else
            {
                info_Status.Text = "Status: Booked";
                info_Person.Visibility = Visibility.Visible;
                info_CheckedIn.Visibility = Visibility.Visible;
                info_Reservation.Visibility = Visibility.Visible;
                info_Reservation.Text = $"Reservation: {seat.ReservationID}";
                Passenger passenger = manager.GetPassenger(seat.PersonID);
                info_Person.Text = "Person: " +  passenger.FirstName + " " + passenger.LastName;
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            selectedSeatIndex = Convert.ToInt32(btn.Content.ToString().Substring(btn.Content.ToString().IndexOf(':') + 1));
            UpdateInfo();
            //string s = btn.Content.ToString().Substring(btn.Content.ToString().IndexOf(':') + 1);
        }


        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            if(btn.Content.ToString().Contains("Empty"))
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Empty.png")));
                btn.Background = brush;
            }
            else
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Taken.png")));
                btn.Background = brush;
            }            
        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Content.ToString().Contains("Empty"))
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Highlight.png")));
                btn.Background = brush;
            }
            else
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Taken_Highlight.png")));
                btn.Background = brush;
            }
        }
    }
}
