using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TrainTracker.Web.Models;
using TrainTracker.Models;

namespace TrainTracker.Views
{
    public partial class TrainTrackerMain : Page
    {
         private int selectedLocationID;

        public TrainTrackerMain()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PageLoaded);
          
            WebContext.Current.Authentication.LoggedOut += (se, ev) =>
            {
                NavigationService.Navigate(new Uri("/Home", UriKind.Relative));
            };
        }     


        #region Location Lookup

        private void locationDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                receiveCar.IsEnabled = false;
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
            else
                receiveCar.IsEnabled = true;
        }

        private void locationComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            var location = locationComboBox1.SelectedItem as Location;
            selectedLocationID = location.LocationID;
        }
        
        #endregion       
      
        #region Edit Track 
        private void newTrack_Click(object sender, RoutedEventArgs e)
        {
           if (selectedLocationID != 0)
            {
                NewTrack newTrack = new NewTrack(selectedLocationID);
                newTrack.Show();
            }
        }
        private void trackDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        #endregion

        #region Commodity Stuff
        private void commodities_Click(object sender, RoutedEventArgs e)
        {
            if (selectedLocationID != 0)
            {
                NewCommodity newComm = new NewCommodity(selectedLocationID); // TrackSelector(yardID);
                newComm.Show();
            }
        }
        #endregion

        #region Data Load

        private void LoadTrackData(object sender, SelectionChangedEventArgs e)
        {
            Accordion acControl = sender as Accordion;
            TrainTracker.Web.Models.RailYard selectedYard = acControl.SelectedItem as TrainTracker.Web.Models.RailYard;
            if (selectedYard != null)
            {                
                yard.Text = selectedYard.YardID.ToString();
                trackDomainDataSource.Load();
            }
        }
    
        private void railYardDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            receiveCar.IsEnabled = true;
        }
        #endregion

        #region Edit Commodities
        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            commodities.IsEnabled = true;
            newTrack.IsEnabled = true;
            yards.IsEnabled = true;
            departments.IsEnabled = true;
        }
        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            commodities.IsEnabled = false;
            newTrack.IsEnabled = false;
            yards.IsEnabled = false;
            departments.IsEnabled = false;
        }
        #endregion

       
        #region Receive car
        private void receiveCar_Click(object sender, RoutedEventArgs e)
        {
            ReceiveCar newCar = new ReceiveCar(selectedLocationID);
            newCar.Show();
        }
        #endregion

        #region Edit Yard
        private void yards_Click(object sender, RoutedEventArgs e)
        {
           if (selectedLocationID != 0)
            {
                NewYard newYards = new NewYard(selectedLocationID);                 
                newYards.Show();
            }
        }
        #endregion

        #region Edit Department
        private void departments_Click(object sender, RoutedEventArgs e)
        {
            if (selectedLocationID != 0)
            {
                NewDepartment newDepartments = new NewDepartment(selectedLocationID);               
                newDepartments.Show();
            }
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

        #region Display Tracks
        private void DuplicateTrackMessage()
        {
            MessageBox.Show("Track is already visible");
        }
        private void yardList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tracks = sender as ListBox;
            var selectedTrack = tracks.SelectedItem as Track;
            int id = selectedTrack.TrackID;
            if (selectedTrack != null)
            {
                if (selectedTrack.TrackID == trackSpace1.GetTrackID())
                {
                    DuplicateTrackMessage();
                    return;
                }
                if (selectedTrack.TrackID == trackSpace2.GetTrackID())
                {
                    DuplicateTrackMessage();
                    return;
                }
                if (selectedTrack.TrackID == trackSpace3.GetTrackID())
                {
                    DuplicateTrackMessage();
                    return;
                }
                if (selectedTrack.TrackID == trackSpace4.GetTrackID())
                {
                    DuplicateTrackMessage();
                    return;
                }
                if (selectedTrack.TrackID == trackSpace5.GetTrackID())
                {
                    DuplicateTrackMessage();
                    return;
                }
                if (selectedTrack.TrackID == trackSpace6.GetTrackID())
                {
                    DuplicateTrackMessage();
                    return;
                }
                if (selectedTrack.TrackID == trackSpace7.GetTrackID())
                {
                    DuplicateTrackMessage();
                    return;
                }
                if (selectedTrack.TrackID == trackSpace8.GetTrackID())
                {
                    DuplicateTrackMessage();
                    return;
                }

                if (trackSpace1.IsAvailable())
                    trackSpace1.SetTrack(selectedTrack);
                else if (trackSpace2.IsAvailable())
                    trackSpace2.SetTrack(selectedTrack);
                else if (trackSpace3.IsAvailable())
                    trackSpace3.SetTrack(selectedTrack);
                else if (trackSpace4.IsAvailable())
                    trackSpace4.SetTrack(selectedTrack);
                else if (trackSpace5.IsAvailable())
                    trackSpace5.SetTrack(selectedTrack);
                else if (trackSpace6.IsAvailable())
                    trackSpace6.SetTrack(selectedTrack);
                else if (trackSpace7.IsAvailable())
                    trackSpace7.SetTrack(selectedTrack);
                else if (trackSpace8.IsAvailable())
                    trackSpace8.SetTrack(selectedTrack);
                else
                    MessageBox.Show("All track spaces are filled. Please close a track to view desired track");
            }
        }
        #endregion


    }
}
