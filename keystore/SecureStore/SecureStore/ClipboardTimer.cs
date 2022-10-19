using System;
using System.Timers;

namespace SecureStore
{
    public delegate void IntervalUpdateDelegate(int interval);

    public class ClipboardTimer
    {
        private int Counter { get; set; }
        private int Interval { get; set; }
        private Timer Timer { get; set; }

        public event IntervalUpdateDelegate IntervalUpdate;

        /// <summary>
        /// Clipboard timer for clearing copied data
        /// </summary>
        /// <param name="interval">in seconds</param>
        public ClipboardTimer(int interval)
        {
            this.Counter = 0;
            this.Interval = interval;
            this.Timer = new Timer(1000);
            this.Timer.Elapsed += OnElapsed;
        }

        public void Start()
        {
            this.Timer.Enabled = true;
            this.Timer.Start();
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            this.Counter++;
            var remainingInterval = this.Interval - this.Counter;

            if (remainingInterval == 0)
            {
                this.Counter = 0;
                this.Timer.Stop();
            }

            this.IntervalUpdate?.Invoke(remainingInterval);
        }
    }
}
