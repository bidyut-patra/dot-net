using System;
using System.Timers;
using System.Windows;
using System.Reflection;
using System.Windows.Interop;
using SchneiderElectric.ProcessExpert.IPC;
using System.IO;

namespace ContainerizedWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer timer;

        public MainWindow()
        {
            InitializeComponent();

            this.timer = new Timer(1000);
            this.timer.Elapsed += OnTimerElapsed;

            this.Loaded += OnWindowLoaded;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.timer.Stop();

            var assembly = Assembly.GetEntryAssembly();
            var exe_location = assembly.Location;
            var dir = new FileInfo(exe_location).DirectoryName;
            var writer = new StreamWriter(dir + @"\log.txt", true);
            writer.WriteLine(DateTime.Now.ToString());
            writer.Close();

            this.timer.Start();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var receiver = new MessageReceiver();
            receiver.RegisterCommand<string, ResultData>(this.GetWindow);
            receiver.WaitForCommand(Assembly.GetEntryAssembly().GetName().Name);

            this.timer.Start();

            var handle = new WindowInteropHelper(this).Handle;
            SnapshotHandler.Savesnapshot(handle);
        }

        [CommandRoute(Name = "GetWindow")]
        private ResultData GetWindow(string input)
        {
            var handle = new WindowInteropHelper(this).Handle;

            return new ResultData()
            {
                Data = handle,
                IsSuccess = true
            };
        }
    }
}
