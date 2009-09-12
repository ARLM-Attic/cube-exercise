using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CubeExercise
{
    /// <summary>
    /// Interaction logic for StopWatch.xaml
    /// </summary>
    public partial class StopWatch : UserControl
    {
        private int _startTick;
        private long _elapsed;
        private DispatcherTimer timer;

        /// <summary>
        /// Creates an instance of the Stopwatch class.
        /// </summary>
        public StopWatch()
        {
            InitializeComponent();
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(10);
            this.timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = this.Ellapsed;
            this.txtMinutes.Text = ((int)timeSpan.TotalMinutes).ToString("000");
            this.txtSedonds.Text = timeSpan.Seconds.ToString("00");
            this.txtMilliseconds.Text = (timeSpan.Milliseconds / 10).ToString("00");
        }

        /// <summary>
        /// Completely resets and deactivates the timer.
        /// </summary>
        public void Reset()
        {
            _elapsed = 0;
            this.timer.Stop();
            _startTick = 0;
            this.txtMinutes.Text = "000";
            this.txtSedonds.Text = "00";
            this.txtMilliseconds.Text = "00";
        }

        /// <summary>
        /// Begins the timer.
        /// </summary>
        public void Start()
        {
            if (!this.IsRunning)
            {
                _startTick = Environment.TickCount;
                this.timer.Start();
            }
        }

        /// <summary>
        /// Stops the current timer.
        /// </summary>
        public void Stop()
        {
            if (this.IsRunning)
            {
                _elapsed += Environment.TickCount - _startTick;
                this.timer.Stop();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the instance is currently recording.
        /// </summary>
        public bool IsRunning
        {
            get { return this.timer.IsEnabled; }
        }

        /// <summary>
        /// Gets the Ellapsed time as a Timespan.
        /// </summary>
        public TimeSpan Ellapsed
        {
            get { return TimeSpan.FromMilliseconds(GetCurrentElapsedTicks()); }
        }

        /// <summary>
        /// Gets the Ellapsed time as the total number of milliseconds.
        /// </summary>
        public long EllapsedMilliseconds
        {
            get { return GetCurrentElapsedTicks() / TimeSpan.TicksPerMillisecond; }
        }

        /// <summary>
        /// Gets the Ellapsed time as the total number of ticks
        /// </summary>
        public long EllapsedTicks
        {
            get { return GetCurrentElapsedTicks(); }
        }

        private long GetCurrentElapsedTicks()
        {
            return this._elapsed + (this.IsRunning ? (Environment.TickCount - _startTick) : 0);
        }
    }
}
