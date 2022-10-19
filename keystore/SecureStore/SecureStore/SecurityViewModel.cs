using System;
using System.ComponentModel;

namespace SecureStore
{
    [Serializable]
    public class SecurityViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyRaised("Title");
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyRaised("Description");
            }
        }

        private string _user;
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyRaised("User");
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyRaised("Password");
            }
        }

        private string UserKey
        {
            get
            {
                return SecureMemory.USER_KEY + "_" + this.GetHashCode().ToString();
            }
        }

        private string PasswordKey
        {
            get
            {
                return SecureMemory.PASSWORD_KEY + "_" + this.GetHashCode().ToString();
            }
        }

        private const char Separator = '|';

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public override string ToString()
        {
            return this.Title + Separator + this.User + Separator + this.Password + Separator + this.Description;
        }

        internal SecurityViewModel Clone(bool encrypted = false)
        {
            if(encrypted)
            {
                return new SecurityViewModel()
                {
                    Title = this.Title,
                    Description = this.Description,
                    User = this.User,
                    Password = this.Password
                };
            }
            else
            {
                return new SecurityViewModel()
                {
                    Title = this.Title,
                    Description = this.Description,
                    User = SecureMemory.GetDecryptedValue(this.UserKey),
                    Password = SecureMemory.GetDecryptedValue(this.PasswordKey)
                };
            }
        }

        internal void Secure()
        {
            this.EncryptUser();
            this.EncryptPassword();
        }

        internal void EncryptUser()
        {
            this.User = SecureMemory.Add(this.UserKey, this.User, SecretForm.Encrypted);
        }

        internal void EncryptPassword()
        {
            this.Password = SecureMemory.Add(this.PasswordKey, this.Password, SecretForm.Encrypted);
        }

        internal void DecryptUser()
        {
            this.User = SecureMemory.GetDecryptedValue(this.UserKey);
        }

        internal void DecryptPassword()
        {
            this.Password = SecureMemory.GetDecryptedValue(this.PasswordKey);
        }

        internal void CopyFrom(SecurityViewModel viewModel)
        {
            this.Title = viewModel.Title;
            this.Description = viewModel.Description;
            this.User = SecureMemory.Add(this.UserKey, viewModel.User, SecretForm.Encrypted);
            this.Password = SecureMemory.Add(this.PasswordKey, viewModel.Password, SecretForm.Encrypted);
        }
    }
}
