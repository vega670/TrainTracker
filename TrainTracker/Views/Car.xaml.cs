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

namespace TrainTracker.Models
{
    public partial class Car : UserControl
    {
        private Point _startingDragPoint;
        private int _highestIndex;
        private string carNumber;
        private string loadStatus;
        private string track;
        private int zIndex = 0;

        public Car()
        {
            InitializeComponent();
            MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
        }
        protected void BringToFront()
        {
            if (zIndex == 0)
            {
                var oldIndex = this.zIndex;
                var mainCanvas = this.Parent as Canvas;
                foreach (FrameworkElement fElement in mainCanvas.Children)
                {
                    Canvas.SetZIndex(fElement, 0);
                }
                Canvas.SetZIndex(this, 2);
                zIndex = 1;
            }
        }
       
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement DragObject = (FrameworkElement)sender;
            _startingDragPoint = e.GetPosition(DragObject);
            BringToFront();
            DragObject.CaptureMouse();
            DragObject.Cursor = Cursors.Hand;
          
            DragObject.MouseMove += new MouseEventHandler(Rectangle_MouseMove);
            DragObject.MouseLeftButtonUp += new MouseButtonEventHandler(Rectangle_MouseLeftButtonUp);
        }

        public void SetTitle(string carName)
        {
            textBlock1.Text = "[ "+carName +" ]";
            carNumber = "[ " + carName + " ]";
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement DragObject = (FrameworkElement)sender;
            DragObject.ReleaseMouseCapture();
            zIndex = 0;
            DragObject.MouseMove -= Rectangle_MouseMove;
            DragObject.MouseLeftButtonUp -= Rectangle_MouseLeftButtonUp;
            DragObject.Cursor = null;
           
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            Canvas Canvas = (Canvas)this.Parent;
            Point Point = e.GetPosition(Canvas);
            FrameworkElement DragObject = (FrameworkElement)sender;
            Canvas.SetLeft(DragObject, Point.X - _startingDragPoint.X);
            Canvas.SetTop(DragObject, Point.Y - _startingDragPoint.Y);
        }
    }
}
