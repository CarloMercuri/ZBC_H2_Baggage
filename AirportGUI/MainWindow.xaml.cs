﻿using AirportGUI.Data;
using BagageSortering.Airportcontrol;
using BagageSortering.Data.Database.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirportGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AirportManager processor = AirportManager.Instance;
            //Constants constants = new Constants();
            //processor.GenerateFlights(constants.CurrentTime);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FlightsDisplayWindow fdw = new FlightsDisplayWindow();
            fdw.Show();
        }
    }
}
