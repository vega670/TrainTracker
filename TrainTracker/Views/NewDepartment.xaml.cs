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
    public partial class NewDepartment : ChildWindow
    {
        private int thisLocation;
        private bool isSaveable = false;
        public NewDepartment(int location)
        {
            InitializeComponent();
            thisLocation = location;
            locFilter.Text = location.ToString(); 
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void departmentDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {
            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        #region Department


        private void DomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show("Submit Failed: Data Input Errors Appear In RED");
                e.MarkErrorAsHandled();
                departmentDomainDataSource.RejectChanges();
            }
        }
        private void departmentNew_Click(object sender, RoutedEventArgs e)
        {
            departmentNew.IsEnabled = false;
            var temp = new TrainTracker.Web.Models.Department();
            temp.LocationID = thisLocation;
            departmentDomainDataSource.DataView.Add(temp);
            departmentCancel.IsEnabled = true;
            departmentFilter.IsEnabled = false;
        }
        private void departmentSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.departmentDomainDataSource.HasChanges)
            {
                this.departmentDomainDataSource.SubmitChanges();
            }
            departmentSave.IsEnabled = false;
            departmentCancel.IsEnabled = false;
            departmentNew.IsEnabled = true;
            departmentCancel.IsEnabled = false;
            departmentFilter.IsEnabled = true;
        }

        private void departmentDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {

            if (this.departmentDomainDataSource.HasChanges)
            {
                departmentSave.IsEnabled = true;
                departmentNew.IsEnabled = false;
                if(isSaveable)
                    departmentCancel.IsEnabled = true;
            }
        }
        private void departmentCancel_Click(object sender, RoutedEventArgs e)
        {
            departmentDomainDataSource.RejectChanges();
            departmentSave.IsEnabled = false;
            departmentCancel.IsEnabled = false;
            departmentNew.IsEnabled = true;
            departmentFilter.IsEnabled = true;
        }
        private void departmentDataGrid_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            departmentSave.IsEnabled = false;
            isSaveable = false;
        }
      #endregion

        private void departmentDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isSaveable = true;
        }

    }
}

