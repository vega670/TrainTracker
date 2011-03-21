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
using System.Collections.ObjectModel;
using TrainTracker.Models;
using TrainTracker.Web;

namespace TrainTracker.Views
{
    public partial class TrackModule : UserControl
    {
        private int trackID;
        private TrainTracker.Web.Models.Track myTrack;
        private ObservableCollection<TrainTracker.Web.Models.RailCarCurrentStatu> trackCars;
        public TrackModule()
        {
            InitializeComponent();
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        public int GetTrackID()
        {
            return trackID;
        }
        public void SetTrackFliter(int track)
        {
            trackFilter.Text = track.ToString();
        }
        public void SetTrack(TrainTracker.Web.Models.Track track)
        {
            myTrack = track;
            trackFilter.Text = track.TrackID.ToString();
            this.Visibility = System.Windows.Visibility.Visible;
            railCarCurrentStatuDomainDataSource.Load();
        }
        public void SetTrackData()
        {
            trackFilter.Text = myTrack.TrackID.ToString();
            double l = 0;
            int ml = (int)myTrack.Length;
            int mc = (int)myTrack.MaxCars;

            TrackData toolTipData = new TrackData();
            toolTipData.Length = (int)myTrack.Length;
            toolTipData.MaxCars = (int)myTrack.MaxCars;
            toolTipData.Track = myTrack.Track1;
            toolTipData.Yard = myTrack.RailYard.YardName;
            toolTipData.TrackType = myTrack.TrackType.TypeName;
            trackID = myTrack.TrackID;
            trackCars = new ObservableCollection<TrainTracker.Web.Models.RailCarCurrentStatu>();

            foreach (TrainTracker.Web.Models.RailCarCurrentStatu car in railCarCurrentStatuDomainDataSource.DataView)
            {
                TimeSpan interval = (TimeSpan)(DateTime.Today - car.ReceiptDate);
                car.DemurrageDays = (int)interval.TotalDays;
                trackCars.Add(car);
                if (car.RailCar.RailCarType.Length != null)
                    l += (double)car.RailCar.RailCarType.Length;

            }

            carList.ItemsSource = trackCars;
            toolTipData.CarCount = trackCars.Count;
            toolTipData.ActualLength = l;
            trackdata.DataContext = toolTipData;
            if (l > ml || trackCars.Count > mc)
                border3.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        }
        public bool IsAvailable()
        {
            if (this.Visibility == System.Windows.Visibility.Collapsed)
                return true;
            else
                return false;
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            trackID = 0;
            trackFilter.Text = trackID.ToString();
            this.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void DomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show(string.Format("Submit Failed: {0}", e.Error.Message));
                e.MarkErrorAsHandled();
            }
        }

        private void railCarCurrentStatuDomainDataSource_LoadedData_1(object sender, LoadedDataEventArgs e)
        {
            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
            else
                SetTrackData();
        }

       
        private void ListBoxDragDropTarget_Drop(object sender, Microsoft.Windows.DragEventArgs e)
        {
           
            MessageBox.Show(sender.ToString());
        }
    }
}
