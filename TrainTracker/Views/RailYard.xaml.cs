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
using TrainTracker.Web.Models;

namespace TrainTracker.Views
{
    public partial class RailYard : Page
    {
        public RailYard()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        #region Data Load

      
        private void railYardDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
    
        #endregion

        #region Location
        private void locationSearch_Click(object sender, RoutedEventArgs e)
        {
            this.locationDomainDataSource.Clear();
            this.locationDomainDataSource.Load();
            locationSave.IsEnabled = false;
            locationNew.IsEnabled = true;
            locationCancel.IsEnabled = false;
        }
        private void locationNew_Click(object sender, RoutedEventArgs e)
        {
            locationNew.IsEnabled = false;
            var temp = new TrainTracker.Web.Models.Location();
            locationDomainDataSource.DataView.Add(temp);
            //ShowChildWindow(0);
            locationSave.IsEnabled = true;
            locationCancel.IsEnabled = true;
            locationFilter.IsEnabled = false;
        }
        private void locationSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.locationDomainDataSource.HasChanges)
            {
                this.locationDomainDataSource.SubmitChanges();
            }
            locationSave.IsEnabled = false;
            locationCancel.IsEnabled = false;
            locationNew.IsEnabled = true;
            locationCancel.IsEnabled = false;
            locationFilter.IsEnabled = true;
        }

        private void locationDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            locationSave.IsEnabled = true;
            locationNew.IsEnabled = false;
        }

        private void locationCancel_Click(object sender, RoutedEventArgs e)
        {
            locationDomainDataSource.RejectChanges();
            locationSave.IsEnabled = false;
            locationCancel.IsEnabled = false;
            locationNew.IsEnabled = true;
            locationFilter.IsEnabled = true;
        }
        private void LocationDomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show("Submit Failed: Data Input Errors");
                e.MarkErrorAsHandled();
                locationDomainDataSource.RejectChanges();
            }
        }
        #endregion

        #region Department
        
        
        private void locationComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filter = new TrainTracker.Web.Models.Location();
            filter = locationComboBox1.SelectedItem as Location;
            locFilter.Text = filter.LocationID.ToString();
        }       
      
        #endregion


        #region Yard     
      

        private void locationComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filter = new TrainTracker.Web.Models.Location();
            filter = locationComboBox2.SelectedItem as Location;
            locFilter2.Text = filter.LocationID.ToString();
        }
       

        #endregion         

       
        private void dataDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
      
    }
}
