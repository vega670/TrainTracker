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
    public partial class Commodity : Page
    {
        public Commodity()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void DomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show(string.Format("Submit Failed: {0}", e.Error.Message));
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
        #region Commodity
        private void comSearch_Click(object sender, RoutedEventArgs e)
        {
            this.commodityDomainDataSource.Clear();
            this.commodityDomainDataSource.Load();
            comSave.IsEnabled = false;
            comNew.IsEnabled = true;
              comCancel.IsEnabled = false;
        }
        private void newCom_Click(object sender, RoutedEventArgs e)
        {
            var temp = new TrainTracker.Web.Models.Commodity();
            commodityDomainDataSource.DataView.Add(temp);
            comNew.IsEnabled = false;
            //ShowChildWindow(0);
            comSave.IsEnabled = true;
            comCancel.IsEnabled = true;
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
        }
       
        private void comCancel_Click(object sender, RoutedEventArgs e)
        {
            commodityDomainDataSource.RejectChanges();
            comSave.IsEnabled = false;
            comCancel.IsEnabled = false;
            comNew.IsEnabled = true;
        }        

        private void commodityDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            comSave.IsEnabled = true;
            comNew.IsEnabled = false;
        }
        #endregion
    }
}
