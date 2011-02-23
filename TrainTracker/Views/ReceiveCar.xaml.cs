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
using TrainTracker.Web.Services;
using TrainTracker.Web.Models;

namespace TrainTracker.Views
{
    public partial class ReceiveCar : ChildWindow
    {
        private int yardID;
      
        public ReceiveCar(int yardID)
        {
            
            InitializeComponent();
            yardBox.Text = yardID.ToString();
            this.yardID = yardID;
            date.Text = DateTime.Today.ToShortDateString();
            time.Value = DateTime.Now;
            weight.Text = "0";

        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #region Package and Validate Current Status
        private void save_Click(object sender, RoutedEventArgs e)
        {
            Random key = new Random();
            var context = new RailServeDS();
            var currentStatus = new RailCarCurrentStatu();
            currentStatus.CurrentStatusID = key.Next(0, 99999999);
            var selectedAct = act.SelectedItem as TrainTracker.Web.Models.Activity;
            var selectedTrack = track.SelectedItem as TrainTracker.Web.Models.Track;
            var selectedStatus = load.SelectedItem as TrainTracker.Web.Models.CarLoadStatu;
            var selectedComm = comm.SelectedItem as TrainTracker.Web.Models.Commodity;
            var selectedCar = car.SelectedItem as TrainTracker.Web.Models.RailCar;

            currentStatus.YardID = yardID;
            currentStatus.ActivityId = selectedAct.ActivityId;
            if (track.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid Track");
                return;
            }
            currentStatus.TrackId = selectedTrack.TrackID;
            currentStatus.StatusId = selectedStatus.StatusID;
            currentStatus.CommodityId = selectedComm.CommodityID;
            currentStatus.CarID = selectedCar.CarID;
            currentStatus.PrimaryUser = WebContext.Current.User.ToString();
           
            int sp;
            if (Int32.TryParse(spot.Text, out sp))
                  currentStatus.Spot = sp; 
            else
            {
                MessageBox.Show("Spot is a required Numeric field");
                return;
             }
            currentStatus.ReceiptDate = date.DisplayDate;
            currentStatus.ReceiptTime = (DateTime)time.Value;
            
            if(weight.Text != null || weight.Text !="")
            {
                int we;
                if (Int32.TryParse(weight.Text, out we))
                  currentStatus.Weight = we; 
                else
                {
                    MessageBox.Show("Weight is not a valid Numeric field");
                    return;
                 }
             }
            currentStatus.Supplier = supplier.Text;
            currentStatus.Comments = comments.Text;
            currentStatus.HistoryTypeId = 1;
           // context.currentstatus_set(currentStatus);
           railCarCurrentStatuDomainDataSource.DataView.Add(currentStatus);
            if (railCarCurrentStatuDomainDataSource.HasChanges)
            {
                railCarCurrentStatuDomainDataSource.SubmitChanges();
                MessageBox.Show("Car: " +selectedCar.Number + " is now is your yard!");
             }
        }
        #endregion

        #region Data Load Error Handling


        private void railCarDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
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

        private void carLoadStatuDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void commodityDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
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

        private void DomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show(string.Format("Submit Failed: {0}", e.Error.Message));
                e.MarkErrorAsHandled();
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

        private void railCarCurrentStatuDomainDataSource_LoadedData_1(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void railCarCurrentStatuDomainDataSource_LoadedData_2(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void trackDomainDataSource1_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
    }
}

