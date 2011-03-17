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
            carNew.IsEnabled = true;
            carCancel.IsEnabled = false;
            carFilter.IsEnabled = true;
            this.railCarDomainDataSource.Clear();
            this.railCarDomainDataSource.Load();           
        }
        private void carNew_Click(object sender, RoutedEventArgs e)
        {
            carNew.IsEnabled = false;
            var car = new TrainTracker.Web.Models.RailCar();
            railCarTypeDomainDataSource.DataView.Add(car);
            carSave.IsEnabled = true;
            carCancel.IsEnabled = true;
            carFilter.IsEnabled = false;
        }
        private void carSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.railCarDomainDataSource.HasChanges)
            {
                this.railCarDomainDataSource.SubmitChanges();
            }
            carSave.IsEnabled = false;
            carCancel.IsEnabled = false;
            carNew.IsEnabled = true;
            carCancel.IsEnabled = false;
            carFilter.IsEnabled = true;
        }

        private void carCancel_Click(object sender, RoutedEventArgs e)
        {
            railCarDomainDataSource.RejectChanges();
            carSave.IsEnabled = false;
            carCancel.IsEnabled = false;
            carNew.IsEnabled = true;
            carFilter.IsEnabled = true;
        }

        private void railCarDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            carSave.IsEnabled = true;
            carNew.IsEnabled = false;
        }
        private void CarDomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show("Submit Failed: Data Input Errors Appear In RED");
                e.MarkErrorAsHandled();
                railCarDomainDataSource.RejectChanges();
            }
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
            typeNew.IsEnabled = false;
            var type = new TrainTracker.Web.Models.RailCarType();
            railCarTypeDomainDataSource.DataView.Add(type);
            typeSave.IsEnabled = true;
            typeCancel.IsEnabled = true;
            typeFilter.IsEnabled = false;
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
            typeFilter.IsEnabled = true;
        }

        private void typeCancel_Click(object sender, RoutedEventArgs e)
        {
            railCarTypeDomainDataSource.RejectChanges();
            typeSave.IsEnabled = false;
            typeCancel.IsEnabled = false;
            typeNew.IsEnabled = true;
            typeFilter.IsEnabled = true;
        }
     
        private void railCarTypeDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            typeSave.IsEnabled = true;
            typeNew.IsEnabled = false;
        }
        private void TypeDomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show("Submit Failed: Data Input Errors Appear In RED");
                e.MarkErrorAsHandled();
                railCarTypeDomainDataSource.RejectChanges();
            }
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
            statusNew.IsEnabled = false;
            var status = new TrainTracker.Web.Models.CarLoadStatu();
                carLoadStatuDomainDataSource.DataView.Add(status);
            statusSave.IsEnabled = true;
            statusCancel.IsEnabled = true;
            statusFilter.IsEnabled = false;

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
            statusFilter.IsEnabled = true;
        }

        private void statusCancel_Click(object sender, RoutedEventArgs e)
        {
            carLoadStatuDomainDataSource.RejectChanges();
             statusSave.IsEnabled = false;
            statusCancel.IsEnabled = false;
            statusNew.IsEnabled = true;
            statusFilter.IsEnabled = true;

        }
        private void carLoadStatuDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            statusSave.IsEnabled = true;
            statusNew.IsEnabled = false;
        }

        private void LoadDomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show("Submit Failed: Data Input Errors Appear In RED");
                e.MarkErrorAsHandled();
                carLoadStatuDomainDataSource.RejectChanges();
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

    }
}
