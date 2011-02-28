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
    public partial class TrackSelector : ChildWindow
    {
        Color trackColor; 

        public TrackSelector(int yardID)
        {
            InitializeComponent();
            yardBox.Text = yardID.ToString();
            trackColor = colorPicker1.Color;
        }
        public TrainTracker.Web.Models.Track GetSelectedTrack()
        {
            var selected  = comboBox1.SelectedItem as TrainTracker.Web.Models.Track;
            return selected;
        }
        public Color GetColor()
        {
            return trackColor;
        }
                
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            trackColor = colorPicker1.Color;
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            trackColor = colorPicker1.Color;
            this.DialogResult = false;
        }

        private void trackDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }
        private void colorPicker1_ColorChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (colorPicker1 != null)
            {
                trackColor = colorPicker1.Color;
            }
        }

    }
}

