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
using System.ServiceModel.DomainServices.Client;
using TrainTracker.Web.Services;
using TrainTracker.Web.Models;

namespace TrainTracker.Views
{
    public partial class NewTrack : ChildWindow
    {
        private int thisLocation;
        private TrainTracker.Web.Models.RailYard theYard;
        private TrainTracker.Web.Models.TrackType theTrackType;
        private TrainTracker.Web.Models.Track theTrack;
        private bool isSaveable = false;

        public NewTrack(int location)
        {
            InitializeComponent();    
            thisLocation = location;
            locFilter.Text = location.ToString();                  
        }
        private void DomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show("Submit Failed: Data Input Errors Appear In RED");
                e.MarkErrorAsHandled();
                trackDomainDataSource.RejectChanges();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        #region Data Laod
        private void trackTypeDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        #region Track
       
        private void trackNew_Click(object sender, RoutedEventArgs e)
        {            
            if (yardComboBox.Items.Count < 1)
            {
                MessageBox.Show("Please Create Yards");
                return;
            }
            trackTypeComboBox.IsEnabled = true;
            yardComboBox.IsEnabled = true;
            trackNew.IsEnabled = false;
            theTrack = new Track();
            theYard = yardComboBox.SelectedItem as TrainTracker.Web.Models.RailYard;
            theTrackType = trackTypeComboBox.SelectedItem as TrainTracker.Web.Models.TrackType;
            theTrack.YardId = theYard.YardID;
            theTrack.LocationID = thisLocation;
            theTrack.TypeID = theTrackType.TypeID;
            trackDomainDataSource.DataView.Add(theTrack);
            trackCancel.IsEnabled = true;
            trackFilter.IsEnabled = false;
            typeFilter.IsEnabled = false;
        }
        private void trackSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.trackDomainDataSource.HasChanges)
            {
                this.trackDomainDataSource.SubmitChanges();
            }
            trackSave.IsEnabled = false;
            trackCancel.IsEnabled = false;
            trackNew.IsEnabled = true;
            trackCancel.IsEnabled = false;
            trackTypeComboBox.IsEnabled = false;
            yardComboBox.IsEnabled = false;
            trackFilter.IsEnabled = true;
            typeFilter.IsEnabled = true;
        }

        private void trackCancel_Click(object sender, RoutedEventArgs e)
        {
            trackDomainDataSource.RejectChanges();
            trackSave.IsEnabled = false;
            trackCancel.IsEnabled = false;
            trackNew.IsEnabled = true;
            trackTypeComboBox.IsEnabled = false;
            yardComboBox.IsEnabled = false;
            trackFilter.IsEnabled = true;
            typeFilter.IsEnabled = true;
        }

        private void trackDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            if (this.trackDomainDataSource.HasChanges)
            {
                trackSave.IsEnabled = true;
                trackNew.IsEnabled = false;
                if(isSaveable)
                    trackCancel.IsEnabled = true;
            }              
        }

        #endregion
        private void railYardDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        #endregion

        private void trackDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void trackTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!trackNew.IsEnabled)
            {
                theTrackType = trackTypeComboBox.SelectedItem as TrainTracker.Web.Models.TrackType;
                theTrack.TypeID = theTrackType.TypeID;
            }
        }

        private void yardComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!trackNew.IsEnabled)
            {
                theYard = yardComboBox.SelectedItem as TrainTracker.Web.Models.RailYard;
                theTrack.YardId = theYard.YardID;
            }
        }
        private void trackDataGrid_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            trackSave.IsEnabled = false;
            isSaveable = false;
        }

        private void trackDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isSaveable = true;
        }
    }
}

