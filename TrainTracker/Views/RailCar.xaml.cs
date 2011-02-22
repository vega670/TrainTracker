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
using TrainTracker.Web.Models;


namespace TrainTracker.Views
{
    public partial class RailCar : Page
    {
        public RailCar()
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
        private void railCarTypeDomainDataSource_LoadedData(object sender, System.Windows.Controls.LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void railCarDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
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
        #endregion

        #region Car
        private void carSearch_Click(object sender, RoutedEventArgs e)
        {
            warning.Text = "";
            this.railCarDomainDataSource.Clear();
            this.railCarDomainDataSource.Load();
            carSave.IsEnabled = false;
            carNew.IsEnabled = true;
            carCancel.IsEnabled = false;
        }
        private void carNew_Click(object sender, RoutedEventArgs e)
        {
            if (railCarDomainDataSource.DataView.IsEmpty)
            {
                warning.Text = "Perform Minor Search First";
                return;
            } 
            carNew.IsEnabled = false;
            var car = new TrainTracker.Web.Models.RailCar();          
            railCarDomainDataSource.DataView.Add(car);
          //  ShowChildWindow(0);
            carSave.IsEnabled = true;
            carCancel.IsEnabled = true;
        }
        private void carSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.railCarDomainDataSource.HasChanges)
            {
                try
                {
                    this.railCarDomainDataSource.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            carSave.IsEnabled = false;
            carCancel.IsEnabled = false;
            carNew.IsEnabled = true;
            carCancel.IsEnabled = false;
        }

        private void carCancel_Click(object sender, RoutedEventArgs e)
        {
            railCarDomainDataSource.RejectChanges();
            carSave.IsEnabled = false;
            carCancel.IsEnabled = false;
            carNew.IsEnabled = true;
        }

        private void railCarDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            carSave.IsEnabled = true;
            carNew.IsEnabled = false;
        }
      

        #endregion

        #region Car Type
        private void typeSearch_Click(object sender, RoutedEventArgs e)
        {
            this.railCarTypeDomainDataSource.Clear();
            this.railCarTypeDomainDataSource.Load();
            typeSave.IsEnabled = false;
            typeNew.IsEnabled = true;
            typeCancel.IsEnabled = false;
        }
        private void typeNew_Click(object sender, RoutedEventArgs e)
        {
            if (railCarTypeDomainDataSource.DataView.Count == 0)
            {
                return;
            } 
            typeNew.IsEnabled = false;
            var type = new TrainTracker.Web.Models.RailCarType();
            railCarTypeDomainDataSource.DataView.Add(type);
            //ShowChildWindow(0);
            typeSave.IsEnabled = true;
            typeCancel.IsEnabled = true;
        }
        private void typeSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.railCarTypeDomainDataSource.HasChanges)
            {
                this.railCarTypeDomainDataSource.SubmitChanges();
            }
            typeSave.IsEnabled = false;
            typeCancel.IsEnabled = false;
            typeNew.IsEnabled = true;
            typeCancel.IsEnabled = false;
        }

        private void typeCancel_Click(object sender, RoutedEventArgs e)
        {
            railCarTypeDomainDataSource.RejectChanges();
            typeSave.IsEnabled = false;
            typeCancel.IsEnabled = false;
            typeNew.IsEnabled = true;
        }
     
        private void railCarTypeDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            typeSave.IsEnabled = true;
            typeNew.IsEnabled = false;
        }

        #endregion

        #region Load Status
        private void statusSearch_Click(object sender, RoutedEventArgs e)
        {
            this.carLoadStatuDomainDataSource.Clear();
            this.carLoadStatuDomainDataSource.Load();
            statusSave.IsEnabled = false;
            statusNew.IsEnabled = true;
            statusCancel.IsEnabled = false;
        }
        private void statusNew_Click(object sender, RoutedEventArgs e)
        {
            if (carLoadStatuDomainDataSource.DataView.Count == 0)
            {
                return;
            }    
            statusNew.IsEnabled = false;
            var status = new TrainTracker.Web.Models.CarLoadStatu();
                carLoadStatuDomainDataSource.DataView.Add(status);
            //ShowChildWindow(0);
            statusSave.IsEnabled = true;
            statusCancel.IsEnabled = true;
        }
        private void statusSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.carLoadStatuDomainDataSource.HasChanges)
            {
                this.carLoadStatuDomainDataSource.SubmitChanges();
            }
            statusSave.IsEnabled = false;
            statusCancel.IsEnabled = false;
            statusNew.IsEnabled = true;
            statusCancel.IsEnabled = false;
        }

        private void statusCancel_Click(object sender, RoutedEventArgs e)
        {
            carLoadStatuDomainDataSource.RejectChanges();
             statusSave.IsEnabled = false;
            statusCancel.IsEnabled = false;
            statusNew.IsEnabled = true;
        }
        private void carLoadStatuDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            statusSave.IsEnabled = true;
            statusNew.IsEnabled = false;
        }
        #endregion     

        #region Child Window for new entry
        private void ShowChildWindow(int x)
        {
            if (x == 0)
            {
                var newCar = new TrainTracker.Web.Models.RailCar();
                var item = new NewEntry();
               // item.AddData(newCar);
                item.Show();
                railCarDomainDataSource.DataView.Add(newCar);
            }
        }
        #endregion

    }
}
