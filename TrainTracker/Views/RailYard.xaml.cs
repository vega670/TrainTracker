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

        private void DomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show(string.Format("Submit Failed: {0}", e.Error.Message));
                e.MarkErrorAsHandled();
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

        private void activityDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
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

        #region Yard
        private void yardSearch_Click(object sender, RoutedEventArgs e)
        {
            this.railYardDomainDataSource.Clear();
            this.railYardDomainDataSource.Load();
            yardSave.IsEnabled = false;
            yardNew.IsEnabled = true;
            yardCancel.IsEnabled = false;
        }
        private void yardNew_Click(object sender, RoutedEventArgs e)
        {
            yardNew.IsEnabled = false;
            var temp = new TrainTracker.Web.Models.RailYard();
            railYardDomainDataSource.DataView.Add(temp);
            //ShowChildWindow(0);
            yardSave.IsEnabled = true;
            yardCancel.IsEnabled = true;
        }
        private void yardSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.railYardDomainDataSource.HasChanges)
            {
                this.railYardDomainDataSource.SubmitChanges();
            }
            yardSave.IsEnabled = false;
            yardCancel.IsEnabled = false;
            yardNew.IsEnabled = true;
            yardCancel.IsEnabled = false;
        }

        private void railYardDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            yardSave.IsEnabled = true;
            yardNew.IsEnabled = false;
        }

        private void yardCancel_Click(object sender, RoutedEventArgs e)
        {
            railYardDomainDataSource.RejectChanges();
            yardSave.IsEnabled = false;
            yardCancel.IsEnabled = false;
            yardNew.IsEnabled = true;
        }
        #endregion

        #region Tracks
        private void trackSearch_Click(object sender, RoutedEventArgs e)
        {
            this.trackDomainDataSource.Clear();
            this.trackDomainDataSource.Load();
            trackSave.IsEnabled = false;
            trackNew.IsEnabled = true;
            trackCancel.IsEnabled = false;
        }
        private void trackNew_Click(object sender, RoutedEventArgs e)
        {
            var temp = new TrainTracker.Web.Models.Track();
            trackDomainDataSource.DataView.Add(temp);
            trackNew.IsEnabled = false;
            //ShowChildWindow(0);
            trackSave.IsEnabled = true;
            trackCancel.IsEnabled = true;
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
        }

        private void trackCancel_Click(object sender, RoutedEventArgs e)
        {
            trackDomainDataSource.RejectChanges();
            trackSave.IsEnabled = false;
            trackCancel.IsEnabled = false;
            trackNew.IsEnabled = true;
        }
        private void trackDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            trackSave.IsEnabled = true;
            trackNew.IsEnabled = false;
        }
        #endregion

        #region Activity
        private void actSearch_Click(object sender, RoutedEventArgs e)
        {
            this.activityDomainDataSource.Clear();
            this.activityDomainDataSource.Load();
            actSave.IsEnabled = false;
            actNew.IsEnabled = true;
            actCancel.IsEnabled = false;
        }
        private void actNew_Click(object sender, RoutedEventArgs e)
        {
            var temp = new TrainTracker.Web.Models.Activity();
            activityDomainDataSource.DataView.Add(temp);
            actNew.IsEnabled = false;
            //ShowChildWindow(0);
            actSave.IsEnabled = true;
            actCancel.IsEnabled = true;
        }
        private void actSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.activityDomainDataSource.HasChanges)
            {
                this.activityDomainDataSource.SubmitChanges();
            }
            actSave.IsEnabled = false;
            actCancel.IsEnabled = false;
            actNew.IsEnabled = true;
            actCancel.IsEnabled = false;
        }

        private void actCancel_Click(object sender, RoutedEventArgs e)
        {
            activityDomainDataSource.RejectChanges();
            actSave.IsEnabled = false;
            actCancel.IsEnabled = false;
            actNew.IsEnabled = true;
        }

        private void activityDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            actSave.IsEnabled = true;
            actNew.IsEnabled = false;
        }

        #endregion

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // var yard = comboBox1.SelectedItem as TrainTracker.Web.Models.RailYard;
          //  yardBox.Text = yard.YardID.ToString();
        }

              
    }
}
