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
    /// Interaction logic for CardSwipeWindow.xaml
    /// </summary>
    public partial class CardSwipeWindow : Window
    {
        long libNumber;
        string role;
        bool shiftDown;
        public CardSwipeWindow()
        {
            InitializeComponent();

            Keyboard.AddKeyDownHandler(stuff, keyDownHandler);
            Keyboard.AddKeyUpHandler(stuff, keyUpHandler);
            libNumber = 0;
            role = "";


            
        }
        // %2015010312644^STUDENT?
        private void keyUpHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                shiftDown = false;
            }
        }

        public void keyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.LeftShift:
                    shiftDown = true;
                    break;
                case Key.D0:
                    libNumber += 0;
                    libNumber *= 10;
                    keyboardInfo.Text = libNumber.ToString();
                    break;
                case Key.D1:
                    libNumber += 1;
                    libNumber *= 10;
                    keyboardInfo.Text = libNumber.ToString();
                    break;
                case Key.D2:
                    libNumber += 2;
                    libNumber *= 10;
                    keyboardInfo.Text = libNumber.ToString();
                    break;
                case Key.D3:
                    libNumber += 3;
                    libNumber *= 10;
                    keyboardInfo.Text = libNumber.ToString();
                    break;
                case Key.D4:
                    libNumber += 4;
                    libNumber *= 10;
                    keyboardInfo.Text = libNumber.ToString();
                    break;
                case Key.D5:
                    if (!shiftDown)
                    {
                        libNumber += 5;
                        libNumber *= 10;
                        keyboardInfo.Text = libNumber.ToString();
                    }
                    break;
                case Key.D6:
                    if (!shiftDown)
                    {
                        libNumber += 6;
                        libNumber *= 10;
                        keyboardInfo.Text = libNumber.ToString();
                    }
                    break;
                case Key.D7:
                    libNumber += 7;
                    libNumber *= 10;
                    keyboardInfo.Text = libNumber.ToString();
                    break;
                case Key.D8:
                    libNumber += 8;
                    libNumber *= 10;
                    keyboardInfo.Text = libNumber.ToString();
                    break;
                case Key.D9:
                    libNumber += 9;
                    libNumber *= 10;
                    keyboardInfo.Text = libNumber.ToString();
                    break;
                case Key.OemQuestion:
                    libNumber /= 10;
                    keyboardInfo.Text = libNumber.ToString();
                    libNumber = 0;
                    break;

            }
        }



    }
}
