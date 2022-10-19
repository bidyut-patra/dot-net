using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace SecureStore
{
    /// <summary>
    /// Interaction logic for MasterPassword.xaml
    /// </summary>
    public partial class MasterPassword : Window
    {
        public bool ChangePassword { get; private set; }

        public MasterPassword(bool hideCloseButton, bool changePassword, string title)
        {
            InitializeComponent();

            this.Loaded += OnLoaded;

            this.Title = title;
            this.ChangePassword = changePassword;
            WindowBehavior.SetHideCloseButton(this, hideCloseButton);

            if(changePassword)
            {
                this.Height = 220;
                this.newPasswordPanel.Visibility = Visibility.Visible;
                this.confirmPasswordPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.Height = 180;
                this.newPasswordPanel.Visibility = Visibility.Collapsed;
                this.confirmPasswordPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.masterPassword.Focus();
        }        

        private void OnOk(object sender, RoutedEventArgs e)
        {
            OpenMainWindow();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                OpenMainWindow();
            }
        }

        private void OpenMainWindow()
        {
            var mainViewModel = this.GetMainViewModel();
            if (mainViewModel == null)
            {
                mainViewModel = this.CreateMainViewModel();
                if (mainViewModel == null)
                {
                    this.masterPassword.Clear();
                    this.masterPassword.Focus();
                }
                else
                {
                    var MainWindow = new MainWindow();
                    MainWindow.DataContext = mainViewModel;
                    MainWindow.Show();
                    this.Close();
                }
            }
            else
            {
                var masterKeyValid = mainViewModel.IsMasterKeyValid(this.masterPassword.Password);
                if (masterKeyValid)
                {
                    if(this.ChangePassword)
                    {
                        var passwordsAreNotEqual = !this.newPassword.Password.Equals(this.confirmPassword.Password);
                        var passwordSameAsOld = this.newPassword.Password.Equals(this.masterPassword.Password);
                        if (passwordsAreNotEqual)
                        {
                            this.errorMsg.Visibility = Visibility.Visible;
                            this.errorMsg.Text = Resource.PasswordPairNotSame;
                        }
                        else if(passwordSameAsOld)
                        {
                            this.errorMsg.Visibility = Visibility.Visible;
                            this.errorMsg.Text = Resource.NewAndOldPasswordsSame;
                        }
                        else if(!this.IsPasswordComplex(this.newPassword.Password))
                        {
                            this.errorMsg.Visibility = Visibility.Visible;
                            this.errorMsg.Text = Resource.PasswordComplexityError;
                        }
                        else
                        {
                            mainViewModel.ChangeMasterKey(this.newPassword.Password);
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.errorMsg.Visibility = Visibility.Visible;
                    this.errorMsg.Text = "Master password is not correct!";
                }
            }
        }

        private bool IsPasswordComplex(string password)
        {
            if(password.Length >= 8)
            {
                var numberFound = false;
                var capLetterFound = false;
                var smallLetterFound = false;
                var specialCharFound = false;
                var otherCharacterFound = false;
                var specialChars = new List<string>() { "@", "#", "$", "%", "&", "*", "!" };
                foreach(var ch in password)
                {
                    if(specialChars.IndexOf(ch.ToString()) >= 0)
                    {
                        specialCharFound = true;
                    }
                    else if((ch >= 97) && (ch <= 122)) // small letter
                    {
                        smallLetterFound = true;
                    }
                    else if((ch >= 65) && (ch <= 90))
                    {
                        capLetterFound = true;
                    }
                    else if ((ch >= 48) && (ch <= 57))
                    {
                        numberFound = true;
                    }
                    else
                    {
                        otherCharacterFound = true;
                    }
                }
                if (otherCharacterFound)
                {
                    return false;
                }
                else
                {
                    return numberFound && smallLetterFound && capLetterFound && specialCharFound;
                }
            }
            else
            {
                return false;
            }
        }

        private MainViewModel GetMainViewModel()
        {
            var mainViewModel = ViewModelRegistry.Instance.Get(typeof(MainViewModel));
            return mainViewModel as MainViewModel;
        }

        private MainViewModel CreateMainViewModel()
        {
            MainViewModel mainViewModel = null;
            try
            {
                mainViewModel = new MainViewModel(this.masterPassword.Password);
                mainViewModel.LoadData();
                ViewModelRegistry.Instance.Register(mainViewModel);
            }
            catch(InvalidMasterKey imk)
            {
                mainViewModel = null;
                MessageBox.Show(imk.Message);
            }
            catch (DecryptionError de)
            {
                mainViewModel = null;
                MessageBox.Show(de.Message);
            }
            catch(Exception ex)
            {
                mainViewModel = null;
                MessageBox.Show(ex.Message);
            }
            return mainViewModel;
        }
    }
}
