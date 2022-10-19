using System;
using System.Timers;
using System.Windows;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SecureStore
{
    public delegate void AutoLockDelegate();
    public class AutoLogoffHandler
    {
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

            [MarshalAs(UnmanagedType.U4)]
            public Int32 cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public Int32 dwTime;
        }

        public Timer Timer { get; private set; }
        public uint Interval { get; private set; }
        public bool Activated { get; private set; }
        public int DeactivatedAt { get; private set; }

        public event AutoLockDelegate OnLock;

        public AutoLogoffHandler(uint interval)
        {
            this.Activated = true;
            this.Interval = interval / 1000;
        }

        public void Initialize()
        {
            Application.Current.Activated += OnAppActivated;
            Application.Current.Deactivated += OnAppDeactivated;

            this.Timer = new Timer(this.Interval * 1000);
            this.Timer.Elapsed += OnTimerElapsed;
            this.Timer.Start();
        }

        private void OnAppActivated(object sender, EventArgs e)
        {
            this.Activated = true;
        }

        private void OnAppDeactivated(object sender, EventArgs e)
        {
            this.Activated = false;
            var timeSpan = this.GetLastInputTime();
            this.DeactivatedAt += timeSpan.Seconds;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if(this.Activated)
            {
                var duration = this.GetLastInputTime().Seconds;
                var totalTime = this.DeactivatedAt + duration;
                if (totalTime >= this.Interval)
                {
                    this.TriggerLockEvent();
                    this.DeactivatedAt = 0;
                }
            }
            else
            {
                this.TriggerLockEvent();
            }
        }

        private void TriggerLockEvent()
        {
            this.Timer.Stop();
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.OnLock?.Invoke();
                this.Timer.Start();
            });
        }

        private TimeSpan GetLastInputTime()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = Marshal.SizeOf(lastInputInfo);

            if (GetLastInputInfo(ref lastInputInfo))
            {
                return TimeSpan.FromMilliseconds(Environment.TickCount - lastInputInfo.dwTime);
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }       
    }
}
