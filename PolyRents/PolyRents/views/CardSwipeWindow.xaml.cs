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
                return cardData;
            }
        }

        public CardSwipeWindow()
        {
            cardData = new CardData();

            InitializeComponent();

            rawDataTextBox.Focus();

            Keyboard.AddKeyDownHandler(rawDataTextBox, keyDownHandler);
            Keyboard.AddKeyUpHandler(rawDataTextBox, keyUpHandler);

            cardData = new CardData();

            DataContext = cardData;

            resetFlags();
        }
        
        private void keyUpHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                shiftDown = false;
            }
        }

        private void keyDownHandler(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            int keyVal = (int) e.Key;

            if (keyVal >= 34 && keyVal <= 43)
            {
                handleNumber(key);
            }
            else if (key == Key.LeftShift)
            {
                shiftDown = true;
            }
            else if (keyVal >= 44 && keyVal <=69)
            {
                handleLetters(key);
            }
            else if (key == Key.OemQuestion)
            {
                //End of read
                readEnded = true;
                cardData = new CardData(libNumber, role);
            }
            else if (key == Key.Enter)
            {
                Close();
            }
            
        }

        private void handleLetters(Key key)
        {
            role += key.ToString();
        }

        private void handleNumber(Key key)
        {
            if (libNumberRead)
            {
                throw new Exception("Library number finished reading and read a new number");
            }

            if (shiftDown)
            {
                if (key == Key.D5)
                {
                    readStarted = true;
                }
                else if (key == Key.D6)
                {
                    libNumberRead = true;
                }
                else
                {
                    throw new Exception("Unexpected key pressed while shift pressed. Key: " + key.ToString());
                }
            }
            else
            {
                libNumber += key.ToString().Substring(1, 1);
            }
        }

        public void resetFlags()
        {
            libNumber = "";
            role = "";

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
            e.Cancel = true;
            base.Hide();
        }
    }
}
