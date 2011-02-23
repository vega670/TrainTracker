using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using TrainTracker.Web.Services;

namespace TrainTracker.Views
{
    public partial class TrainTrackerMain : Page
    {
        private int yardID;
        private RailServeDS context;

        public TrainTrackerMain()
        {
            InitializeComponent();
            context = new RailServeDS();

        
            WebContext.Current.Authentication.LoggedOut += (se, ev) =>
            {
                NavigationService.Navigate(new Uri("/Home", UriKind.Relative));
            };
        }

        #region Rail Yard Lookup
        private void railYardDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        } 
        private void yardSelector_Click(object sender, RoutedEventArgs e)
        {
            var selectedYard = comboBox1.SelectedItem as TrainTracker.Web.Models.RailYard;
            yardID = selectedYard.YardID;
            yardBox.Text = yardID.ToString();
            this.railCarCurrentStatuDomainDataSource.Load();
            if (yardID != 0)
            {
                receiveCar.IsEnabled = true;
                yardStatus.IsEnabled = true;             
            }
        }

        private void receiveCar_Click(object sender, RoutedEventArgs e)
        {
            ReceiveCar newCar = new ReceiveCar(yardID);
            newCar.Show();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedYard = comboBox1.SelectedItem as TrainTracker.Web.Models.RailYard;
            yardID = selectedYard.YardID;
            yardBox.Text = yardID.ToString();
            receiveCar.IsEnabled = false;
            yardStatus.IsEnabled = false;

        }
        #endregion

        private void railCarCurrentStatuDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void railCarCurrentStatuDomainDataSource1_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

    }
}
