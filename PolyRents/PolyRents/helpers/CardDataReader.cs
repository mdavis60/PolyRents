using PolyRents.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.model;
using System.Windows.Input;
using System.Windows.Controls;

namespace PolyRents.helpers
{
    class CardDataReader
    {
        //Data on card
        private string rawInput;

        //helper fields for reading data
        private bool shiftDown;
        private bool readStarted;
        private bool readEnded;
        private bool libNumberRead;

        private bool readDataFlag;
        
        private CardData cardData;
        private Control nextField;

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

        public CardDataReader(Control nextField)
        {
            this.nextField = nextField;
        }

        public void handleLetters(Key key)
        {
            rawInput += key.ToString();
        }

        public void handleNumber(Key key)
        {
            if (libNumberRead)
            {
                throw new Exception("Library number finished reading and read a new number");
            }

            if (shiftDown)
            {
                if (key == Key.D5)
                {
                    rawInput = "%";
                    readStarted = true;
                }
                else if (key == Key.D6)
                {
                    rawInput += "^";
                    libNumberRead = true;
                }
                else
                {
                    throw new Exception("Unexpected key pressed while shift pressed. Key: " + key.ToString());
                }
            }
            else
            {
                rawInput += key.ToString().Substring(1, 1);
            }
        }

        public void keyDownHandler(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            int keyVal = (int)e.Key;

            if (keyVal >= 34 && keyVal <= 43)
            {
                handleNumber(key);
            }
            else if (key == Key.LeftShift)
            {
                shiftDown = true;
            }
            else if (keyVal >= 44 && keyVal <= 69)
            {
                handleLetters(key);
            }
            else if (key == Key.OemQuestion)
            {
                //End of read
                readEnded = true;
                rawInput += "?";
                cardData = new CardData(rawInput);

            }
            else if (key == Key.Enter)
            {
                nextField.Focus();
            }
        }

        public void keyUpHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                shiftDown = false;
            }
        }

        public void resetFlags()
        {
            shiftDown = false;
            readStarted = false;
            readEnded = false;
            libNumberRead = false;
        }
    }
}
