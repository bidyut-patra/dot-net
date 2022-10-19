using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SecureStore
{
    /// <summary>
    /// Interaction logic for SecureEntryDialog.xaml
    /// </summary>
    public partial class SecureEntryDialog : Window
    {
        public SecureEntryDialog(string title)
        {
            this.Title = title;
            InitializeComponent();
        }

        private void OnOk(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
