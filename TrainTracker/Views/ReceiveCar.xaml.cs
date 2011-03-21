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
using System.ServiceModel.DomainServices.Client;

namespace TrainTracker.Views
{
    public partial class ReceiveCar : ChildWindow
    {
        private int myLocation;
        private TrainTracker.Web.Models.RailCarType theType;
        private TrainTracker.Web.Models.RailCar theCar;
        private bool isSaveable = false;
      
        public ReceiveCar(int location)
        {
            
            InitializeComponent();
            locationBox.Text = location.ToString();
            this.myLocation = location;
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
            var selectedCar = autoCompleteBox1.SelectedItem as TrainTracker.Web.Models.RailCar;
            if (selectedCar == null)
            {
                MessageBox.Show("Car is not in System.\nPlease Enter a 'New Car'");
                return;
            }
            var context = new RailServeDS();
            InvokeOperation<int> InvokeOp = context.GetRailCarsByNumber(selectedCar.Number);
            InvokeOp.Completed += (ss, ee) =>
            {
                int carInSystem = InvokeOp.Value;
                if(carInSystem < 1)
                {
                    MessageBox.Show("Car is not in System. Please enter a 'New Car from next tab'");
                        return;
                }
                else
                {
                Random key = new Random();
            
                var currentStatus = new RailCarCurrentStatu();
                currentStatus.CurrentStatusID = key.Next(0, 99999999);
                var selectedStatus = loadComboBox.SelectedItem as TrainTracker.Web.Models.CarLoadStatu;
                var selectedComm = commComboBox.SelectedItem as TrainTracker.Web.Models.Commodity;
                var selectedDepartment = departmentComboBox.SelectedItem as TrainTracker.Web.Models.Department;
                var selectedTrack = trackComboBox.SelectedItem as TrainTracker.Web.Models.Track;
                    
                currentStatus.LocationID = myLocation;
                currentStatus.YardID = (int)selectedTrack.YardId;
                currentStatus.StatusId = selectedStatus.StatusID;
                currentStatus.CommodityId = selectedComm.CommodityID;
                currentStatus.CarID = selectedCar.CarID;                    
                currentStatus.TrackId = selectedTrack.TrackID;

                currentStatus.PrimaryUser = WebContext.Current.User.Name;
                currentStatus.Demurrage = checkBox1.IsChecked;
                currentStatus.Company = company.Text;
                currentStatus.DepartmentId = selectedDepartment.DepartmentID;
                currentStatus.ReceiptDate = date.DisplayDate;
                currentStatus.ReceiptTime = (DateTime)time.Value;
                currentStatus.MovementDate = DateTime.Today;
            
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
                currentStatus.HistoryTypeId = 1; //HistoryType is stored in db to distinguish movement types
                if (railCarCurrentStatuDomainDataSource.DataView.CanAdd == false)
                    railCarCurrentStatuDomainDataSource.Load();
                railCarCurrentStatuDomainDataSource.DataView.Add(currentStatus);
                if (railCarCurrentStatuDomainDataSource.HasChanges)
                {
                    railCarCurrentStatuDomainDataSource.SubmitChanges();
                    MessageBox.Show("Car: " +selectedCar.Number + " is now is your yard!");
                    save.IsEnabled = false;
                 }
               }
           };
        }
        #endregion


        private void car_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = new RailServeDS();
            var selectedCar = autoCompleteBox1.SelectedItem as TrainTracker.Web.Models.RailCar;
            if (selectedCar != null)
            {
                InvokeOperation<int> InvokeOp = context.GetRailCarCurrentStatusByLocationandCarCount(myLocation, selectedCar.CarID);
                InvokeOp.Completed += (ss, ee) =>
                {
                    int carInSystem = InvokeOp.Value;
                    if (carInSystem >= 1)
                    {
                        MessageBox.Show("Car is already in your location");
                        save.IsEnabled = false;
                        return;
                    }
                    else
                        save.IsEnabled = true;

                };
            }
        }

        #region Tab 2 Car Search
        private void carSearch_Click(object sender, RoutedEventArgs e)
        {
            this.railCarDomainDataSource.Clear();
            this.railCarDomainDataSource.Load();
        }
        #endregion

        #region Data Load Error Handling


        private void data_LoadedData(object sender, LoadedDataEventArgs e)
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
                railCarDomainDataSource2.RejectChanges();
            }
        }        
        #endregion

        
        #region New Car
        private void carNew_Click(object sender, RoutedEventArgs e)
        {
            if (carTypeComboBox.Items.Count < 1)
            {
                MessageBox.Show("Please Create Rail Car Types");
                return;
            }
            carTypeComboBox.IsEnabled = true;
            carNew.IsEnabled = false;
            theCar = new TrainTracker.Web.Models.RailCar();
            var type = carTypeComboBox.SelectedItem as RailCarType;
            theCar.Type = type.TypeID;
            railCarDomainDataSource2.DataView.Add(theCar);
            carFilter.IsEnabled = false;
            carCancel.IsEnabled = true;
        }
        private void carSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.railCarDomainDataSource2.HasChanges)
            {
                this.railCarDomainDataSource2.SubmitChanges();
            }
            carSave.IsEnabled = false;
            carCancel.IsEnabled = false;
            carNew.IsEnabled = true;
            carCancel.IsEnabled = false;
            carTypeComboBox.IsEnabled = false;
            carFilter.IsEnabled = true;
        }

        private void carCancel_Click(object sender, RoutedEventArgs e)
        {
            railCarDomainDataSource2.RejectChanges();
            carSave.IsEnabled = false;
            carCancel.IsEnabled = false;
            carNew.IsEnabled = true;
            carTypeComboBox.IsEnabled = false;
            carFilter.IsEnabled = true;
        }

        private void railCarDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            if (this.railCarDomainDataSource2.HasChanges)
            {
                carCancel.IsEnabled = true;
                carNew.IsEnabled = false;  
                if(isSaveable)
                    carSave.IsEnabled = true;
            }            
        }
        #endregion       

        #region Tab 1 Receice Car Stuff
        private void railCarTypeDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void carTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!carNew.IsEnabled)
            {
                theType = carTypeComboBox.SelectedItem as TrainTracker.Web.Models.RailCarType;
                theCar.Type = theType.TypeID;
            }
        }

        private void railCarDataGrid_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            carSave.IsEnabled = false;
            isSaveable = false;
        }

        private void railCarDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isSaveable = true;
        }
        private void departmentDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
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

       
    }
}

