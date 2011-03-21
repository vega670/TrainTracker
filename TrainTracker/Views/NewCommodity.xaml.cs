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

namespace TrainTracker.Views
{
    public partial class NewCommodity : ChildWindow
    {
        private int thisLocation;
        private bool isSaveable = false;
        public NewCommodity(int location)
        {
            InitializeComponent();
            locFilter.Text = location.ToString();
            thisLocation = location;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void commodityDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        #region Commodity
       
        private void newCom_Click(object sender, RoutedEventArgs e)
        {
            var temp = new TrainTracker.Web.Models.Commodity();
            temp.LocationID = thisLocation; ;
            commodityDomainDataSource.DataView.Add(temp);
            comNew.IsEnabled = false;
            comCancel.IsEnabled = true;
            comFilter.IsEnabled = false;
        }
        private void comSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.commodityDomainDataSource.HasChanges)
            {
                this.commodityDomainDataSource.SubmitChanges();
            }
            comSave.IsEnabled = false;
            comCancel.IsEnabled = false;
            comNew.IsEnabled = true;
            comCancel.IsEnabled = false;
            comFilter.IsEnabled = true;
        }

        private void comCancel_Click(object sender, RoutedEventArgs e)
        {
            commodityDomainDataSource.RejectChanges();
            comSave.IsEnabled = false;
            comCancel.IsEnabled = false;
            comNew.IsEnabled = true;
            comFilter.IsEnabled = true;
        }

        private void commodityDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            if (this.commodityDomainDataSource.HasChanges)
            {                
                comNew.IsEnabled = false;
                comCancel.IsEnabled = true;
                if(isSaveable)
                    comSave.IsEnabled = true;
            }
        }
        private void commodityDataGrid_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            isSaveable = false;
            comSave.IsEnabled = true;
        }
        #endregion

        private void commodityDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isSaveable = true;
        }
        private void DomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show("Submit Failed: Data Input Errors Appear In RED");
                e.MarkErrorAsHandled();
                commodityDomainDataSource.RejectChanges();
            }
        }
       
    }
}

