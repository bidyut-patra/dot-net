using System;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace SecureStore
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private string _ClipboardExpiryMessage;
        public string ClipboardExpiryMessage
        {
            get
            {
                return _ClipboardExpiryMessage;
            }
            set
            {
                _ClipboardExpiryMessage = value;
                OnPropertyRaised("ClipboardExpiryMessage");
            }
        }

        public ClipboardTimer Timer { get; private set; }
        public bool MasterKeyChanged { get; private set; }
        public ObservableCollection<SecurityViewModel> Items { get; set; }

        public MainViewModel(string masterPassword)
        {
            SecureMemory.Add(SecureMemory.MASTER_KEY, masterPassword);
        }

        public bool IsMasterKeyValid(string masterPassword)
        {
            var masterPasswordHash = SecureMemory.GetHashString(masterPassword);
            var storedMasterPasswordHash = SecureMemory.GetValue(SecureMemory.MASTER_KEY);
            return masterPasswordHash.Equals(storedMasterPasswordHash);
        }

        internal static MainViewModel Get()
        {
            return ViewModelRegistry.Instance.Get(typeof(MainViewModel)) as MainViewModel;
        }

        internal static MainViewModel Create(string password)
        {
            MainViewModel mainViewModel = null;
            try
            {
                mainViewModel = new MainViewModel(password);
                mainViewModel.LoadData();
                ViewModelRegistry.Instance.Register(mainViewModel);
            }
            catch (InvalidMasterKey imk)
            {
                mainViewModel = null;
                MessageBox.Show(imk.Message);
            }
            catch (DecryptionError de)
            {
                mainViewModel = null;
                MessageBox.Show(de.Message);
            }
            catch (Exception ex)
            {
                mainViewModel = null;
                MessageBox.Show(ex.Message);
            }
            return mainViewModel;
        }

        internal void ChangeMasterKey(string masterPassword)
        {
            var decryptedModels = new ObservableCollection<SecurityViewModel>();
            foreach (var item in this.Items)
            {
                decryptedModels.Add(item.Clone());
            }
            SecureMemory.Add(SecureMemory.MASTER_KEY, masterPassword);
            for(var i = 0; i < decryptedModels.Count; i++)
            {
                var decryptedModel = decryptedModels[i];
                decryptedModel.Secure();
                this.Items[i] = decryptedModel;
            }
            this.MasterKeyChanged = true;
        }

        public void CopyItem(SecurityViewModel viewModel)
        {
            this.Timer = new ClipboardTimer(10);
            this.Timer.IntervalUpdate += OnIntervalUpdate;

            var decryptedUser = viewModel.Clone();
            Clipboard.SetText(decryptedUser.Password);

            this.OnIntervalUpdate(10);
            this.Timer.Start();
        }

        private void OnIntervalUpdate(int remainingInterval)
        {
            if (remainingInterval > 0)
            {
                this.ClipboardExpiryMessage = string.Format(Resource.AddClipboardMessage, remainingInterval);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() => Clipboard.Clear());
                this.ClipboardExpiryMessage = string.Format(Resource.ClearClipboardMessage);
            }
        }

        public void InsertItem(SecurityViewModel viewModel)
        {
            viewModel.Secure();
            this.Items.Add(viewModel);
        }

        public void DeleteItem(SecurityViewModel viewModel)
        {
            this.Items.Remove(viewModel);
        }

        public void LoadData()
        {
            try
            {
                var omodels = new ObservableCollection<SecurityViewModel>();
                var content = SecureMemory.ReadDatabase();
                if (content.Length > 0)
                {
                    var stream = new MemoryStream(content);
                    var serializer = new BinaryFormatter();
                    var data = serializer.Deserialize(stream);
                    var models = data as List<SecurityViewModel>;
                    omodels = new ObservableCollection<SecurityViewModel>(models);
                }
                foreach(var item in omodels)
                {
                    item.Secure();
                }
                this.Items = omodels;
            }
            catch(Exception ex)
            {
                SecureMemory.Remove(SecureMemory.MASTER_KEY);
                throw ex;
            }
        }

        public void SaveData()
        {
            var insecureItems = new List<SecurityViewModel>();
            foreach(var item in this.Items)
            {
                insecureItems.Add(item.Clone());
            }
            var memoryStream = new MemoryStream();
            var serializer = new BinaryFormatter();
            serializer.Serialize(memoryStream, insecureItems);
            SecureMemory.SaveDatabase(memoryStream.ToArray());
            this.MasterKeyChanged = false;
        }

        private void AddSampleViewModel()
        {
            // Add a test model when there is no data
            this.Items.Add(new SecurityViewModel()
            {
                Title = "Secure",
                Description = "Secure Data",
                User = "Test",
                Password = "Test"
            });
        }
    }
}
