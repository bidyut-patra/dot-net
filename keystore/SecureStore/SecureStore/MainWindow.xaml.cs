using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;

namespace SecureStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel
        {
            get
            {
                return this.DataContext as MainViewModel;
            }
        }

        public AutoLogoffHandler AutoLogoff { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            this.AutoLogoff = new AutoLogoffHandler(60000);
            this.AutoLogoff.OnLock += OnAutoLock;
            this.AutoLogoff.Initialize();

            this.Loaded += OnLoaded;
        }

        private void OnAutoLock()
        {
            this.HideAllWindows();
            var mPasswordDlg = new MasterPassword(true, false, Resource.AppUnlockDlgTitle);
            mPasswordDlg.ShowDialog();
            this.ShowAllWindows();
        }

        private void ShowAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Show();
            }
        }
        private void HideAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Hide();
            }
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SecureFunction.Test();
        }

        private void OnSeeUser(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            var viewModel = button.DataContext as SecurityViewModel;
            viewModel.DecryptUser();
        }

        private void OnHideUser(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            var viewModel = button.DataContext as SecurityViewModel;
            viewModel.EncryptUser();
        }

        private void OnCopyUser(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var viewModel = button.DataContext as SecurityViewModel;
            this.MainViewModel.CopyItem(viewModel);
        }

        private void OnSeePassword(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            var viewModel = button.DataContext as SecurityViewModel;
            viewModel.DecryptPassword();
        }

        private void OnHidePassword(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            var viewModel = button.DataContext as SecurityViewModel;
            viewModel.EncryptPassword();
        }

        private void OnCopyPassword(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var viewModel = button.DataContext as SecurityViewModel;
            this.MainViewModel.CopyItem(viewModel);
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(Resource.DeleteConfirmation, Resource.ProductTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var securityViewModel = button.DataContext as SecurityViewModel;
                this.MainViewModel.DeleteItem(securityViewModel);
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var viewModel = btn.DataContext as SecurityViewModel;
            var clonedViewModel = viewModel.Clone();
            var secureEntryDlg = new SecureEntryDialog(Resource.EditSecureDataDlgTitle);
            secureEntryDlg.DataContext = clonedViewModel;
            var result = secureEntryDlg.ShowDialog();
            if((bool)result)
            {
                viewModel.CopyFrom(clonedViewModel);
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            var viewModel = new SecurityViewModel();
            var secureEntryDlg = new SecureEntryDialog(Resource.NewSecureDataDlgTitle);
            secureEntryDlg.DataContext = viewModel;
            var result = secureEntryDlg.ShowDialog();
            if((bool)result)
            {
                this.MainViewModel.InsertItem(viewModel);
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            this.MainViewModel.SaveData();
            MessageBox.Show(Resource.SaveSuccessful, Resource.ProductTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnChangeMasterKey(object sender, RoutedEventArgs e)
        {
            var mPasswordDlg = new MasterPassword(false, true, Resource.AppPasswordChangeDlgTitle);
            mPasswordDlg.ShowDialog();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if(this.MainViewModel.MasterKeyChanged)
            {
                var result = MessageBox.Show(Resource.MasterKeyChange, Resource.ProductTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    this.MainViewModel.SaveData();
                }
            }
        }

        private void OnBackupDatabase(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.ValidateNames = true;
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.FileName = SecureMemory.DbFileName;
            saveFileDialog.ShowDialog();
            string folderPath = Path.GetDirectoryName(saveFileDialog.FileName);
            var destinationFilePath = folderPath + @"\" + SecureMemory.DbFileName;
            File.Copy(SecureMemory.DbPath, destinationFilePath, true);
        }
    }
}
