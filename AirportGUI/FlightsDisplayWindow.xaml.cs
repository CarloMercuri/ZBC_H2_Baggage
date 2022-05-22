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
        List<Button> selectionButtons = new List<Button>();

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

            for (int i = 0; i < 14; i++)
            {
                Button btn = new Button();
                btn.Click += RowButtonClick;
                btn.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                Style style = this.FindResource("NoMouseOverButton") as Style;
                btn.Style = style;
                Grid.SetRow(btn, i + 1);
                Grid.SetColumn(btn, 0);
                Grid.SetColumnSpan(btn, 9);
                selectionButtons.Add(btn);
                mainGrid.Children.Add(btn);
            }

            Console.WriteLine();
        }

        private void RowButtonClick(object sender, RoutedEventArgs e)
        {
            Button _btn = sender as Button;

            foreach(Button btn in selectionButtons)
            {
                btn.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }

            _btn.Background = new SolidColorBrush(Color.FromArgb(100, 200, 200, 0));
            int _row = (int)_btn.GetValue(Grid.RowProperty);
            int _column = (int)_btn.GetValue(Grid.ColumnProperty);
            //MessageBox.Show(string.Format("Button clicked at column {0}, row {1}", _column, _row));
        }
    }
}
