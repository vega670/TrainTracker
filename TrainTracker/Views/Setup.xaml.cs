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

namespace TrainTracker.Views.Login
{
    public partial class Setup : Page
    {
        public Setup()
        {
            InitializeComponent();

            WebContext.Current.Authentication.LoggedOut += (se, ev) =>
            {
                NavigationService.Navigate(new Uri("/Home", UriKind.Relative));
            };
            BuildTree();
        }

        private void BuildTree()
        {
            //Create the menu items with links to actual page to fill content frame
            List<Models.NavMenu> menu = new List<Models.NavMenu>();

            menu.Add(new Models.NavMenu { Name = "Rail Cars", PageUrl = "/RailCar" });
            menu.Add(new Models.NavMenu { Name = "Rail Yards", PageUrl = "/RailYard" });
            menu.Add(new Models.NavMenu { Name = "Commodities ", PageUrl = "/Commodity" });
   
            menuSelection.ItemsSource = menu;          
         }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void menuSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox selectedBox = sender as ListBox;
            Models.NavMenu selected = selectedBox.SelectedItem as Models.NavMenu;            

            if (selected != null)
            {                
                contentFrame.Navigate(new Uri(selected.PageUrl , UriKind.Relative));
                label1.Text = selected.Name;
            }
        }
      
    }
}
