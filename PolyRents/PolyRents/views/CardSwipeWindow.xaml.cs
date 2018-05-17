using PolyRents.model;
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
using System.ComponentModel;
using PolyRents.helpers;

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for CardSwipeWindow.xaml
    /// </summary>
    public partial class CardSwipeWindow : Window
    {
        private string libNumber;
        private string role;

        private string rawInput;

        private bool shiftDown;
        private bool readStarted;
        private bool readEnded;
        private bool libNumberRead;

        private CardData cardData;
        private CardDataReader cardDataReader;


        private bool cancelPressed;

        public bool CancelPressed
        {
            get
            {
                return cancelPressed;
            }
            private set
            {
                cancelPressed = value;
            }
        }

        public bool CardDataChanged
        {
            get
            {
                return readEnded;
            }
        }

        public CardData CardInfo
        {
            get
            {
                return cardDataReader == null ? null : cardDataReader.CardInfo;
            }
        }

        public CardSwipeWindow()
        {
            InitializeComponent();

            rawDataTextBox.Focus();

            Keyboard.AddKeyDownHandler(rawDataTextBox, keyDownHandler);
            Keyboard.AddKeyUpHandler(rawDataTextBox, keyUpHandler);
            
            cardDataReader = new CardDataReader(cancelButton);

            DataContext = cardData;

            resetFlags();
        }
        
        public void keyUpHandler(object sender, KeyEventArgs e)
        {
            cardDataReader.keyUpHandler(sender, e);
        }

        public void keyDownHandler(object sender, KeyEventArgs e)
        {
            cardDataReader.keyDownHandler(sender, e);
        }

        public void resetFlags()
        {
            CancelPressed = false;
            shiftDown = false;
            readStarted = false;
            readEnded = false;
            libNumberRead = false;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            resetFlags();
            CancelPressed = true;
            Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            rawDataTextBox.Focus();
            e.Cancel = true;
            base.Hide();
        }
    }
}
