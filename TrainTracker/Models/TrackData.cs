using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Data;


namespace TrainTracker.Models
{
    public class TrackData : INotifyPropertyChanged
    {
        private int carCount;
        private double actualLength;

        public int CarCount
        {
            get { return carCount; }
            set
            {
                carCount = (int)value;
                NotifyPropertyChanged("CarCount");
            }
        }
        public string Yard { get; set; }
        public string Track { get; set; }
        public int MaxCars { get; set; }
        public double Length { get; set; }
        public string TrackType { get; set; }

        public double ActualLength
        {
            get { return actualLength; }
            set
            {
                actualLength = (int)value;
                NotifyPropertyChanged("ActualLength");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }  

    }
}
