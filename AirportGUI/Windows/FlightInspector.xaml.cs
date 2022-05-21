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

        public FlightInspector(string flightNumber)
        {
            InitializeComponent();

            manager.GetFlightSeats("AS4732");

            int seatIndex = 0;

            for (int row = 0; row < 24; row++)
            {
                for (int seat = 0; seat < 7; seat++)
                {
                    if (seat == 3) continue;

                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Empty.png")));
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
            img.Source = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Empty.png")));
        }

        private void Img_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = (Image)sender;
            img.Source = new BitmapImage(new Uri(Path.Combine(constants.IconsFolderPath, "seat_Highlight.png")));
        }
    }
}
