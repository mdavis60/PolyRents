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

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for ConfirmationDialog.xaml
    /// </summary>
    public partial class ConfirmationDialog : Window
    {
        private bool yesClicked;

        public bool YesClicked
        {
            get
            {
                return yesClicked;
            }
            private set
            {
                yesClicked = value;
            }
        }

        public ConfirmationDialog(String title, String confirmationMessage)
        {
            InitializeComponent();

            YesClicked = false;
            
            this.Title = title;
            confirmText.Text = confirmationMessage;
        }

        public ConfirmationDialog(String title)
        {
            InitializeComponent();

            this.Title = title;

            YesClicked = false;
        }

        public void setTitle(String title)
        {
            this.Title = title;
        }

        public void setMessage(String confirmationMessage)
        {
            confirmText.Text = confirmationMessage;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            YesClicked = b.Name.Equals("yesButton");
            softClose();
        }

        private void softClose()
        {
            Hide();
        }
    }
}
