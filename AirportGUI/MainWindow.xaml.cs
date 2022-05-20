using AirportGUI.Data;
using AirportGUI.Windows;
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

        private void btn_Check_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Checkin ciw = new Checkin();
            ciw.Show();
            Image btn = (Image)sender;
            btn.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/btn_CheckIn_Pressed.png"));
            //btn.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            
        }

        private void btn_Check_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image btn = (Image)sender;
            btn.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/btn_CheckIn_Normal.png"));
        }
    }
}
