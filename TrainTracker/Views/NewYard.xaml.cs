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
    
    public partial class NewYard : ChildWindow
    {
        private int location;
        private bool isSaveable = false;

        public NewYard(int loc)
        {
            InitializeComponent();
            location = loc;
            locFilter.Text = loc.ToString();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void railYardDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        #region Yard

        private void yardNew_Click(object sender, RoutedEventArgs e)
        {
            yardNew.IsEnabled = false;
            var temp = new TrainTracker.Web.Models.RailYard();
            temp.LocationID = location;
            railYardDomainDataSource.DataView.Add(temp);
            yardFilter.IsEnabled = false;
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
            yardFilter.IsEnabled = true;
        }

        private void railYardDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            if (this.railYardDomainDataSource.HasChanges)
            {
                yardSave.IsEnabled = true;
                yardNew.IsEnabled = false;
                if(isSaveable)
                    yardCancel.IsEnabled = true;
            }
        }

        private void yardCancel_Click(object sender, RoutedEventArgs e)
        {
            railYardDomainDataSource.RejectChanges();
            yardSave.IsEnabled = false;
            yardCancel.IsEnabled = false;
            yardNew.IsEnabled = true;
            yardFilter.IsEnabled = true;
        }
        private void DomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show("Submit Failed: Data Input Errors Appear In RED");
                e.MarkErrorAsHandled();
                railYardDomainDataSource.RejectChanges();
            }
        }       
       
      #endregion

        private void railYardDataGrid_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            isSaveable = false;
            yardSave.IsEnabled = false;
        }

        private void railYardDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isSaveable = true;
        }

    }
}

