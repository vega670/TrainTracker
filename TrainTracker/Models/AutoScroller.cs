//Copyright (c) 2009, Peter Dungan
//All rights reserved.
//Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//* Neither the name of Peter Dungan nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.  


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
using System.Collections.ObjectModel;

    /// <summary>
    /// Enables automatic scrolling on a targetted ScrollViewer. 
    /// </summary>
    public class AutoScroller
    {
        private System.Windows.Threading.DispatcherTimer myDispatcherTimer;
        private ScrollViewer TargetScrollViewer;
        private ObservableCollection<FrameworkElement> DraggableObjects;
        private double testoffset;
        private double testvoffset;

        private Canvas _targetCanvas;
        /// <summary>
        /// Sets the canvas where objects are dragged/dropped. If null, it will default to the child of TargetScrollViewer when AutoScroll is set to Mode.Drag.
        /// </summary>
        public Canvas TargetCanvas
        {
            get
            {
                return _targetCanvas;
            }
            set
            {
                _targetCanvas = value;
                DraggableObjects = new ObservableCollection<FrameworkElement>();
                foreach (FrameworkElement F in _targetCanvas.Children)
                {
                    DraggableObjects.Add(F);
                };
            }
        }

        /// <summary>
        /// Sets the mode of automatic scrolling for the targetted ScrollViewer. 
        /// </summary>
        public enum Mode
        {
            /// <summary>
            ///  Off: No automatic scrolling.
            /// </summary>
            Off,
            /// <summary>
            /// Auto: Scrolls when the cursor is at the edge of the ScrollViewer. 
            /// </summary>
            Auto,
            /// <summary>
            /// Drag: Scrolls when the the mouse is dragged at the edge of the ScrollViewer.
            /// </summary>
            Drag
        };

        #region properties

        /// <summary>
        /// Defines the width (in pixels) of the zone at the edge of the ScrollViewer that will trigger automatic scrolling. Default is 40.
        /// </summary>
        public double ScrollArea { get; set; }

        /// <summary>
        /// The number of pixels that will be scrolled per 100 milliseconds when scrolling is activated. Default is 5.
        /// </summary>
        public double ScrollPixelsPerTick { get; set; }

        private Mode _autoscroll;
        /// <summary>
        /// Sets the mode of automatic scrolling for the targetted ScrollViewer. Auto: Scrolls when the cursor is at the edge of the ScrollViewer. Drag: Scrolls when the the mouse is dragged at the edge of the ScrollViewer.
        /// </summary>
        public Mode AutoScroll
        {
            get
            {
                return _autoscroll;
            }
            set
            {
                if (_autoscroll == Mode.Auto)
                {
                    TargetScrollViewer.MouseMove -= AutoScrollViewer_MouseMove;
                    TargetScrollViewer.MouseLeave -= AutoScrollViewer_MouseLeave;
                }
                if (_autoscroll == Mode.Drag)
                {
                                       
                    foreach (FrameworkElement F in TargetCanvas.Children)
                    {
                        F.MouseLeftButtonDown -= new MouseButtonEventHandler(F_MouseLeftButtonDown);
                    }
                    TargetCanvas.LayoutUpdated -= new EventHandler(TargetCanvas_LayoutUpdated);
                }
                if (value == Mode.Auto)
                {
                    TargetScrollViewer.MouseMove += AutoScrollViewer_MouseMove;
                    TargetScrollViewer.MouseLeave += AutoScrollViewer_MouseLeave;
                }
                if (value == Mode.Drag)
                {
                    if (TargetCanvas == null)
                    {
                        if (TargetScrollViewer.Content.GetType().ToString() == "System.Windows.Controls.Canvas")
                        {
                            TargetCanvas = ((Canvas)(TargetScrollViewer.Content));
                        }
                    }
                    foreach (FrameworkElement F in TargetCanvas.Children)
                    {
                        F.MouseLeftButtonDown += new MouseButtonEventHandler(F_MouseLeftButtonDown);
                    }
                    TargetCanvas.LayoutUpdated += new EventHandler(TargetCanvas_LayoutUpdated);

                }
                _autoscroll = value;

            }
        }

        void TargetCanvas_LayoutUpdated(object sender, EventArgs e)
        {
            if (DraggableObjects.Count != TargetCanvas.Children.Count)
            {
                foreach (FrameworkElement F in TargetCanvas.Children)
                {
                    if (DraggableObjects.Contains(F) == false)
                    {
                        F.MouseLeftButtonDown += new MouseButtonEventHandler(F_MouseLeftButtonDown);
                        DraggableObjects.Add(F);
                    }
                }
            }
        }

        private FrameworkElement DraggedObject
        { get; set; }

        private Boolean _scrollLeft;

        /// <summary>
        /// Causes the targetted ScrollViewer to scroll left, at a rate defined by the ScrollPixelsPerTick property.
        /// </summary>
        public Boolean ScrollLeft
        {
            get
            {
                return _scrollLeft;
            }
            set
            {
                if (value == true)
                {
                    if (ScrollUp == ScrollDown == ScrollRight == _scrollLeft == false)
                        StartTimer();
                }
                else
                {
                    if (ScrollUp == ScrollDown == ScrollRight == false)
                        StopTimer();
                }
                _scrollLeft = value;

            }
        }

        private Boolean _scrollRight;
        /// <summary>
        /// Causes the targetted ScrollViewer to scroll right, at a rate defined by the ScrollPixelsPerTick property.
        /// </summary>
        public Boolean ScrollRight
        {
            get
            {
                return _scrollRight;
            }
            set
            {
                if (value == true)
                {
                    if (ScrollUp == ScrollDown == ScrollRight == _scrollLeft == false)
                        StartTimer();
                }
                else
                {
                    if (ScrollUp == ScrollDown == ScrollLeft == false)
                        StopTimer();
                }
                _scrollRight = value;

            }
        }

        private Boolean _scrollUp;
        /// <summary>
        /// Causes the targetted ScrollViewer to scroll up, at a rate defined by the ScrollPixelsPerTick property.
        /// </summary>
        public Boolean ScrollUp
        {
            get
            {
                return _scrollUp;
            }
            set
            {
                if (value == true)
                {
                    if (ScrollUp == ScrollDown == ScrollRight == _scrollLeft == false)
                        StartTimer();
                }
                else
                {
                    if (ScrollRight == ScrollDown == ScrollLeft == false)
                        StopTimer();
                }
                _scrollUp = value;

            }
        }

        private Boolean _scrollDown;
        /// <summary>
        /// Causes the targetted ScrollViewer to scroll down, at a rate defined by the ScrollPixelsPerTick property.
        /// </summary>
        public Boolean ScrollDown
        {
            get
            {
                return _scrollDown;
            }
            set
            {
                if (value == true)
                {
                    if (ScrollUp == ScrollDown == ScrollRight == _scrollLeft == false)
                        StartTimer();
                }
                else
                {
                    if (ScrollUp == ScrollRight == ScrollLeft == false)
                        StopTimer();
                }
                _scrollDown = value;

            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Enables automatic scrolling on a targetted ScrollViewer. 
        /// </summary>
        /// <param name="TargetScrollViewer">
        /// The ScrollViewer to use automatic scrolling.
        /// </param>
        public AutoScroller(ScrollViewer TargetScrollViewer)
        {
            this.TargetScrollViewer = TargetScrollViewer;
            ScrollPixelsPerTick = 5;
            ScrollArea = 40;
        }

        /// <summary>
        /// Enables automatic scrolling on a targetted ScrollViewer. 
        /// </summary>
        /// <param name="TargetScrollViewer">The ScrollViewer to use automatic scrolling.</param>
        /// <param name="AutoScroll">The AutoScroll mode. Auto: Scrolls when the cursor is at the edge of the ScrollViewer. Drag: Scrolls when the the mouse is dragged at the edge of the ScrollViewer.</param>
        public AutoScroller(ScrollViewer TargetScrollViewer, Mode AutoScroll)
        {
            this.TargetScrollViewer = TargetScrollViewer;
            ScrollPixelsPerTick = 5;
            ScrollArea = 40;
            this.AutoScroll = AutoScroll;
        }

        /// <summary>
        /// Enables automatic scrolling on a targetted ScrollViewer.
        /// </summary>
        /// <param name="TargetScrollViewer">The ScrollViewer to use automatic scrolling.</param>
        /// <param name="ScrollPixelsPerTick">The number of pixels per 100 milliseconds that the ScrollViewer moves while autoscrolling.</param>
        /// <param name="ScrollArea">/// Defines the width (in pixels) of the zone at the edge of the ScrollViewer that will trigger automatic scrolling.</param>
        public AutoScroller(ScrollViewer TargetScrollViewer, double ScrollPixelsPerTick, double ScrollArea)
        {
            this.TargetScrollViewer = TargetScrollViewer;
            this.ScrollPixelsPerTick = ScrollPixelsPerTick;
            this.ScrollArea = ScrollArea;
        }
        /// <summary>
        /// Enables automatic scrolling on a targetted ScrollViewer.
        /// </summary>
        /// <param name="TargetScrollViewer">The ScrollViewer to use automatic scrolling.</param>
        /// <param name="ScrollPixelsPerTick">The number of pixels per 100 milliseconds that the ScrollViewer moves while autoscrolling.</param>
        /// <param name="ScrollArea">/// Defines the width (in pixels) of the zone at the edge of the ScrollViewer that will trigger automatic scrolling.</param>
        /// <param name="AutoScroll">The AutoScroll mode. Auto: Scrolls when the cursor is at the edge of the ScrollViewer. Drag: Scrolls when the the mouse is dragged at the edge of the ScrollViewer.</param>
        public AutoScroller(ScrollViewer TargetScrollViewer, double ScrollPixelsPerTick, double ScrollArea, Mode AutoScroll)
        {
            this.TargetScrollViewer = TargetScrollViewer;
            this.ScrollPixelsPerTick = ScrollPixelsPerTick;
            this.ScrollArea = ScrollArea;
            this.AutoScroll = AutoScroll;
        }

        #endregion

        #region events

        private void Each_Tick(object o, EventArgs sender)
        {
            if (ScrollRight == true)
            {
                if (TargetScrollViewer.HorizontalOffset == TargetScrollViewer.ScrollableWidth)
                    ScrollRight = false;
                else
                {
                    TargetScrollViewer.ScrollToHorizontalOffset(TargetScrollViewer.HorizontalOffset + ScrollPixelsPerTick);

                    if (DraggedObject != null && testoffset != TargetScrollViewer.HorizontalOffset)
                    {
                        DraggedObject.SetValue(Canvas.LeftProperty, (double)(DraggedObject.GetValue(Canvas.LeftProperty)) + (ScrollPixelsPerTick));
                    }
                    testoffset = TargetScrollViewer.HorizontalOffset;
                }
            }
            if (ScrollLeft == true)
            {
                if (TargetScrollViewer.HorizontalOffset == 0)
                {
                    ScrollLeft = false;
                }
                else
                {
                    TargetScrollViewer.ScrollToHorizontalOffset(TargetScrollViewer.HorizontalOffset - ScrollPixelsPerTick);
                    if (DraggedObject != null && testoffset != TargetScrollViewer.HorizontalOffset)
                    {
                        DraggedObject.SetValue(Canvas.LeftProperty, (double)(DraggedObject.GetValue(Canvas.LeftProperty)) - (ScrollPixelsPerTick));
                    }
                    testoffset = TargetScrollViewer.HorizontalOffset;
                }
            }
            if (ScrollDown == true)
            {
                TargetScrollViewer.ScrollToVerticalOffset(TargetScrollViewer.VerticalOffset + ScrollPixelsPerTick);
                if (DraggedObject != null && testvoffset != TargetScrollViewer.VerticalOffset)
                {
                    DraggedObject.SetValue(Canvas.TopProperty, (double)(DraggedObject.GetValue(Canvas.TopProperty)) + (ScrollPixelsPerTick));
                }
                testvoffset = TargetScrollViewer.VerticalOffset;
            }
            if (ScrollUp == true)
            {
                if (TargetScrollViewer.VerticalOffset == 0)
                {
                    ScrollUp = false;
                }
                else
                {
                    TargetScrollViewer.ScrollToVerticalOffset(TargetScrollViewer.VerticalOffset - ScrollPixelsPerTick);
                    if (DraggedObject != null && testvoffset != TargetScrollViewer.VerticalOffset)
                    {
                        DraggedObject.SetValue(Canvas.TopProperty, (double)(DraggedObject.GetValue(Canvas.TopProperty)) - (ScrollPixelsPerTick));
                    }
                    testvoffset = TargetScrollViewer.VerticalOffset;
                }
            }
        }

        private void AutoScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousepos = e.GetPosition(TargetScrollViewer);

            if (mousepos.X < ScrollArea && TargetScrollViewer.HorizontalOffset > 0)
                ScrollLeft = true;
            else if (ScrollLeft == true)
                ScrollLeft = false;

            if (mousepos.Y < ScrollArea && TargetScrollViewer.VerticalOffset > 0)
                ScrollUp = true;
            else if (ScrollUp == true)
                ScrollUp = false;

            if (mousepos.X > ((double)TargetScrollViewer.ActualWidth - ScrollArea))
                ScrollRight = true;
            else if (ScrollRight == true)
                ScrollRight = false;

            if (mousepos.Y > ((double)TargetScrollViewer.ActualHeight - ScrollArea))
                ScrollDown = true;
            else if (ScrollDown == true)
                ScrollDown = false;

        }

        private void AutoScrollViewer_MouseLeave(object sender, MouseEventArgs e)
        {
            ScrollUp = ScrollDown = ScrollLeft = ScrollRight = false;
        }

        private void F_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            testoffset = TargetScrollViewer.HorizontalOffset;
            testvoffset = TargetScrollViewer.VerticalOffset;
            DraggedObject = (FrameworkElement)sender;
            TargetScrollViewer.MouseMove += AutoScrollViewer_MouseMove;
            TargetScrollViewer.MouseLeave += AutoScrollViewer_MouseLeave;
            DraggedObject.MouseLeftButtonUp += new MouseButtonEventHandler(AutoScrollViewer_MouseLeftButtonUp);
        }

        private void AutoScrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TargetScrollViewer.MouseMove -= AutoScrollViewer_MouseMove;
            TargetScrollViewer.MouseLeave -= AutoScrollViewer_MouseLeave;
            ScrollLeft = ScrollDown = ScrollUp = ScrollRight = false;
            DraggedObject = null;
        }

        #endregion

        #region Methods

        private void StartTimer()
        {
            myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100); // 100 Milliseconds 
            myDispatcherTimer.Tick += new EventHandler(Each_Tick);
            myDispatcherTimer.Start();
        }

        private void StopTimer()
        {
            if (myDispatcherTimer != null)
            {
                myDispatcherTimer.Stop();
                myDispatcherTimer.Tick -= Each_Tick;
            }
        }

        /// <summary>
        /// Clears all references so the object can be picked up by garbage collection.
        /// </summary>
        public void ClearControl()
        {
            this.AutoScroll = Mode.Off;
            ScrollUp = false;
            ScrollRight = false;
            ScrollLeft = false;
            ScrollDown = false;
            TargetScrollViewer = null;
        }

        #endregion
    }
