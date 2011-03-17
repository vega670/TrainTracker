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

namespace TrainTracker.Views
{
    public partial class TrackType : Page
    {
        public TrackType()
        {
            InitializeComponent();
        }

        #region Data Load

        private void dataDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
      
        private void DomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show("Submit Failed: Data Input Errors");
                e.MarkErrorAsHandled();
                trackTypeDomainDataSource.RejectChanges();
            }
        }
        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        #endregion

        #region Tracks
        private void trackSearch_Click(object sender, RoutedEventArgs e)
        {
            this.trackTypeDomainDataSource.Clear();
            this.trackTypeDomainDataSource.Load();
            trackSave.IsEnabled = false;
            trackNew.IsEnabled = true;
            trackCancel.IsEnabled = false;
        }
        private void trackNew_Click(object sender, RoutedEventArgs e)
        {
            var temp = new TrainTracker.Web.Models.TrackType();
            trackTypeDomainDataSource.DataView.Add(temp);
            trackNew.IsEnabled = false;
            trackSave.IsEnabled = true;
            trackCancel.IsEnabled = true;
            trackFilter.IsEnabled = false;
        }
        private void trackSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.trackTypeDomainDataSource.HasChanges)
            {
                this.trackTypeDomainDataSource.SubmitChanges();
            }
            trackSave.IsEnabled = false;
            trackCancel.IsEnabled = false;
            trackNew.IsEnabled = true;
            trackCancel.IsEnabled = false;
            trackFilter.IsEnabled = true;
        }

        private void trackCancel_Click(object sender, RoutedEventArgs e)
        {
            trackTypeDomainDataSource.RejectChanges();
            trackSave.IsEnabled = false;
            trackCancel.IsEnabled = false;
            trackNew.IsEnabled = true;
            trackFilter.IsEnabled = true;
        }
        private void trackTypeDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            trackSave.IsEnabled = true;
            trackNew.IsEnabled = false;
            trackCancel.IsEnabled = true;

        }
        #endregion



    }
}
