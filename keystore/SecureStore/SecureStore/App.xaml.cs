using System;
using System.Windows;
using System.Windows.Threading;

namespace SecureStore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {            
            base.OnStartup(e);

            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            var mPasswordDialog = new MasterPassword(false, false, Resource.AppLoginDlgTitle);
            mPasswordDialog.ShowDialog();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            this.HandleException(exception);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            this.HandleException(e.Exception);
            e.Handled = true;
        }

        private void HandleException(Exception e)
        {
            string error = string.Format(Resource.UnhandledException, e.Message, e.StackTrace);
            MessageBox.Show(error, Resource.ProductTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
