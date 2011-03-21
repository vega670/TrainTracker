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
using System.ServiceModel.DomainServices.Client;

namespace TrainTracker.Views
{
    public partial class CurrentState : Page
    {
        RailServeDS _Context = new RailServeDS();

        public CurrentState()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentStateDataGrid.ItemsSource = _Context.RailCars;
            _Context.Load(_Context.GetRailCarsQuery());
        }

        private void stateGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
