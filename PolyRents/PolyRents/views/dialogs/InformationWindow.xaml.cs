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
    /// Interaction logic for InformationWindow.xaml
    /// </summary>
    public partial class InformationWindow : Window
    {
        private bool okClicked;

        public bool OkClicked
        {
            get
            {
                return okClicked;
            }
            private set
            {
                okClicked = value;
            }
        }

        public InformationWindow(String title)
        {
            InitializeComponent();

            OkClicked = false;

            this.Title = title;
        }

        public InformationWindow(String title, String infoMessage)
        {
            InitializeComponent();

            OkClicked = false;

            this.Title = title;
            infoText.Text = infoMessage;
        }

        public void setInfoText(String infoMessage)
        {
            infoText.Text = infoMessage;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            OkClicked = b.Name.Equals("okButton");
            Close();
        }
    }
}
