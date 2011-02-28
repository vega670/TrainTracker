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
using TrainTracker.Models;

namespace TrainTracker.Views
{
    public partial class TrainTrackerMain : Page
    {
        private int yardID;
        private RailServeDS context;
        private double offsetX = 22;
        private double offsetY = 50;
        private AutoScroller AS;
        private PointCollection trackPoints;
        private Polyline trackLine;
        SolidColorBrush brush;
        private Color trackColor;
        private string trackName;
        private TrainTracker.Web.Models.Track thetrack;
        private TrackMarker newTrackmarker;


        private List<int> carsOnYard;


        public TrainTrackerMain()
        {
            InitializeComponent();
            context = new RailServeDS();
            AS = new AutoScroller(scrollViewer1, AutoScroller.Mode.Auto);
            carsOnYard = new List<int>();
            WebContext.Current.Authentication.LoggedOut += (se, ev) =>
            {
                NavigationService.Navigate(new Uri("/Home", UriKind.Relative));
            };
        }

        #region Rail Yard Lookup
        private void railYardDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        private void yardSelector_Click(object sender, RoutedEventArgs e)
        {
            BuildCar();

        }
        private void receiveCar_Click(object sender, RoutedEventArgs e)
        {
            ReceiveCar newCar = new ReceiveCar(yardID);
            newCar.Closed += new EventHandler(newCar_Closed);
            newCar.Show();
        }
        void newCar_Closed(object sender, EventArgs e)
        {

            this.railCarCurrentStatuDomainDataSource1.Load();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedYard = comboBox1.SelectedItem as TrainTracker.Web.Models.RailYard;
            yardID = selectedYard.YardID;
            newTrack.IsChecked = false;
            yardBox.Text = yardID.ToString();

        }
        #endregion

        #region Data Load

        private void railCarCurrentStatuDomainDataSource2_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        private void railCarCurrentStatuDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        private void railCarCurrentStatuDomainDataSource1_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        #endregion

        #region Build Car Image
        private void BuildCar()
        {
            foreach (RailCarCurrentStatu car in railCarCurrentStatuDomainDataSource1.DataView)
            {
                bool inYard = false;

                if (carsOnYard.Count == 0)
                {
                    Car newCar = new Car();
                    newCar.SetTitle(car.RailCar.Number);
                    yard.Children.Add(newCar);
                    if ((car.X == 0 || car.X == null) || (car.Y == 0 || car.Y == null))
                    {
                        newCar.SetValue(Canvas.LeftProperty, scrollViewer1.HorizontalOffset + offsetX);
                        newCar.SetValue(Canvas.TopProperty, scrollViewer1.VerticalOffset + offsetY);

                        offsetY += 45;
                    }
                    else
                    {
                        newCar.SetValue(Canvas.LeftProperty, scrollViewer1.HorizontalOffset + car.X);
                        newCar.SetValue(Canvas.TopProperty, scrollViewer1.VerticalOffset + car.Y);
                    }
                    carsOnYard.Add(car.CarID);
                }
                else
                {
                    foreach (int x in carsOnYard)
                    {
                        if (x == car.CarID)
                            inYard = true;
                    }
                    if (!inYard)
                    {
                        Car newCar = new Car();
                        newCar.SetTitle(car.RailCar.Number);
                        yard.Children.Add(newCar);
                        if ((car.X == 0 || car.X == null) || (car.Y == 0 || car.Y == null))
                        {
                            newCar.SetValue(Canvas.LeftProperty, scrollViewer1.HorizontalOffset + offsetX);
                            newCar.SetValue(Canvas.TopProperty, scrollViewer1.VerticalOffset + offsetY);
                            offsetY += 45;
                        }
                        else
                        {
                            newCar.SetValue(Canvas.LeftProperty, scrollViewer1.HorizontalOffset + car.X);
                            newCar.SetValue(Canvas.TopProperty, scrollViewer1.VerticalOffset + car.Y);
                        }
                        carsOnYard.Add(car.CarID);
                    }
                }
            }
        }
        #endregion

        #region Make Track
        private void newTrack_Checked(object sender, RoutedEventArgs e)
        {
            GridLines();
            TrackSelector newTrackSelector = new TrackSelector(yardID);
            newTrackSelector.Show();
            newTrackSelector.Closed += new EventHandler(newTrack_Closed);
            newTrackSelector.Closing += newTrack_Closing;
            yard.MouseLeftButtonDown += yard_MouseLeftButtonDown;
            comboBox1.IsEnabled = false;
            yardSelector.IsEnabled = false;
            receiveCar.IsEnabled = false;
            undo.Visibility = System.Windows.Visibility.Visible;
            trackLine = new Polyline();
            trackPoints = new PointCollection();
            infoText.Text = "Infomation as to how to build a track......";

        }
        private void newTrack_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TrackSelector newTrackSelected = (TrackSelector)sender;
            brush = new SolidColorBrush();
            trackColor = newTrackSelected.GetColor();
        }
        private void newTrack_Closed(object sender, EventArgs e)
        {
            TrackSelector newTrackSelected = (TrackSelector)sender;
            if ((newTrackSelected.DialogResult == true))
            {
                if (newTrackSelected.GetSelectedTrack() != null)
                {
                    thetrack = newTrackSelected.GetSelectedTrack();
                    BuildTrack();
                }
            }
        }
        private void BuildTrack()
        {
            trackName = thetrack.Track1;
            brush.Color = trackColor;
            trackLine.Stroke = brush;
            trackLine.StrokeThickness = 2;
            trackLine.Points = trackPoints;
            yard.Children.Add(trackLine);

        }
        private void newTrack_Unchecked(object sender, RoutedEventArgs e)
        {
            infoText.Text = null;
            comboBox1.IsEnabled = true;
            yardSelector.IsEnabled = true;
            receiveCar.IsEnabled = true;
            yard.MouseLeftButtonDown -= yard_MouseLeftButtonDown;
            undo.Visibility = System.Windows.Visibility.Collapsed;
            RemoveGridLines();
        }

        private void yard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(yard);

            if (newTrack.IsChecked == true)
            {

               /* if (trackPoints.Count > 1)
                {
                    MessageBox.Show(trackPoints[trackPoints.Count - 1].X + ", " + trackPoints[trackPoints.Count - 1].Y);
                    MessageBox.Show(position.X + ", " + position.Y);
                }*/
                newTrackmarker = new TrackMarker();
                newTrackmarker.SetValue(Canvas.LeftProperty, position.X - (newTrackmarker.Width / 2));
                newTrackmarker.SetValue(Canvas.TopProperty, position.Y);
                newTrackmarker.SetTitle(trackName);
                yard.Children.Add(newTrackmarker);
                trackPoints.Add(position);
            }
        }

        private void undo_Click(object sender, RoutedEventArgs e)
        {
            if (trackPoints.Count > 0)
            {
                trackPoints.RemoveAt(trackPoints.Count - 1);
                trackPoints.RemoveAt(trackPoints.Count - 1);
                yard.Children.Remove(trackLine);
                yard.Children.Remove(newTrackmarker);               
                BuildTrack();
            }
        }
        #endregion

        private void GridLines()
        {
            double row = yard.ActualHeight;
            double col = yard.ActualWidth;

            for (int i = 0; i <= Convert.ToInt32(row); i += 30)
            {
                Line lineRow = new Line();
                lineRow.X1 = 0;
                lineRow.Y1 = i;
                lineRow.X2 = row;
                lineRow.Y2 = i;
                SolidColorBrush grayBrush = new SolidColorBrush();
                grayBrush.Color = Colors.LightGray;
                lineRow.StrokeThickness = .2;
                lineRow.Stroke = grayBrush;
                lineRow.Tag = "gridLine";
                yard.Children.Add(lineRow);
            }
            for (int i = 0; i <= Convert.ToInt32(col); i += 30)
            {
                Line lineCol = new Line();
                lineCol.X1 = i;
                lineCol.Y1 = 0;
                lineCol.X2 = i;
                lineCol.Y2 = col;
                SolidColorBrush grayBrush = new SolidColorBrush();
                grayBrush.Color = Colors.LightGray;
                lineCol.StrokeThickness = .2;
                lineCol.Stroke = grayBrush;
                lineCol.Tag = "gridLine";
                yard.Children.Add(lineCol);
            }
        }
        private void RemoveGridLines()
        {
            UIElement[] tmp = new UIElement[yard.Children.Count]; 
            yard.Children.CopyTo(tmp, 0);  
            foreach (UIElement uielement in tmp) 
           {  
                Shape myShape = uielement as Shape;  
             
               if (myShape != null && myShape.Tag != null)  
               {  
                   if (myShape.Tag.ToString().Contains("gridLine"))  
                   {  
                        yard.Children.Remove(uielement);  
                    } 
               }  

            }   

        }
    }
}
