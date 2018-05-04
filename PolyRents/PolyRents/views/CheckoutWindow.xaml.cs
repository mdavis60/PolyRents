using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Windows;
using static PolyRents.model.Status;
using System.ComponentModel;
using System.Windows.Controls;
using PolyRents.ComputingResourcesDataSetTableAdapters;
using PolyRents.helpers;
using System.Windows.Input;

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for CheckoutWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        private Logger logger = Logger.getInstance();

        private Rental rental;
        private Renter renter;
        private Resource resource;

        private string libNumber;
        private string role;

        private string rawInput;

        private bool shiftDown;
        private bool readStarted;
        private bool readEnded;
        private bool libNumberRead;

        private CardSwipeWindow cardSwipe;

        private RenterTableAdapter renters;
        private ResourcesTableAdapter resources;

        private InformationWindow infoWindow;

        private bool readDataFlag;

        public Rental BoundRental
        {
            get
            {
                return rental;
            }
        }

        private Rental newRental;

        private bool rentalChanged;

        public bool RentalChanged
        {
            get
            {
                return rentalChanged;
            }
            private set
            {
                rentalChanged = value;
            }
        }

        private bool formValid;

        public bool FormValid
        {
            get
            {
                return renterLibNumber.Text != "" && resourceId.Text != ""  
                    && resourceType.Text != "";
            }
        }

        public Visibility ErrorVisible
        {
            get
            {
                return FormValid ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public CheckoutWindow(Rental theRental = null)
        {
            if (theRental == null)
            {
                theRental = new Rental();
            }

            renters = RenterTableAdapter.getInstance();
            resources = ResourcesTableAdapter.getInstance();

            SetRentalToView(theRental);

            InitializeComponent();

            initializeFields();
        }

        private void keyUpHandler(object sender, KeyEventArgs e)
        {
            if (!readDataFlag)
            {
                return;
            }

            if (e.Key == Key.LeftShift)
            {
                shiftDown = false;
            }
        }

        private void keyDownHandler(object sender, KeyEventArgs e)
        {
            if (!readDataFlag)
            {
                return;
            }

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
                libNumber = CardData.completeLibNumber(libNumber);
            }
            else if (key == Key.Enter)
            {
                okButton.Focus();
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
            
            shiftDown = false;
            readStarted = false;
            readEnded = false;
            libNumberRead = false;
        }

        public void SetRentalToView(Rental aRental = null)
        {
            if (aRental == null)
            {
                aRental = new Rental();
            }
            rentalChanged = false;

            rental = aRental;
            renter = rental.Renter;
            resource = rental.Resource;

            newRental = new Rental(rental);

            if (IsInitialized)
            {
                initializeFields();
            }
        }

        private void initializeFields()
        {
            if (rental.Renter != null)
            {
                renterName.Text = rental.Renter.FullName;
                renterEmail.Text = rental.Renter.CpEmail;
                renterLibNumber.Text = rental.Renter.LibraryNumber;
            }

            if (rental.Resource != null)
            {
                resourceId.Text = rental.Resource.IdResource.ToString();
                resourceType.Text = rental.Resource.Type.ResourceName;
            }
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            Button sent = sender as Button;

            if (((String)sent.Name).Equals("cancelButton"))
            {
                Close();
                return;
            }

            if (FormValid)
            {
                renter = renters.getRenterByLibraryNumber(renterLibNumber.Text);
                Resource resource = resources.getById(int.Parse(resourceId.Text));
                resource.Status = stringToStatus("CHECKED_OUT");

                newRental.Renter = renter;
                newRental.Resource = resource;

                newRental.CheckoutTime = DateTime.Now;
                newRental.CheckinTime = DateTime.MinValue;

                RentalChanged = !rental.Equals(newRental);

                if (RentalChanged)
                {
                    rental = newRental;
                }
                Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private void swipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (cardSwipe == null)
            {
                cardSwipe = new CardSwipeWindow();
            }

            cardSwipe.ShowDialog();

            if (!cardSwipe.CancelPressed)
            {
                CardData cardData = cardSwipe.CardInfo;
                
                renter = renters.getRenterByLibraryNumber(cardData.LibraryNumber);

                renterName.Text = renter.FullName;
                renterLibNumber.Text = renter.LibraryNumber;
                renterEmail.Text = renter.CpEmail;
            }

            cardSwipe.resetFlags();
        }

        private void resourceId_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox sent = sender as TextBox;

            if (sent.Text == "")
            {
                return;
            }

            int id = 0;

            if (int.TryParse(sent.Text, out id))
            {
                resource = resources.getById(id);
            }

            if (resource == null)
            {
                resourceType.Text = "";
                
                if (infoWindow == null)
                {
                    infoWindow = new InformationWindow("Invalid Resource Id");
                }

                infoWindow.setInfoText("The resource with  id " + resourceId.Text + " was not found");
                infoWindow.ShowDialog();

                return;
            }

            resourceType.Text = resource.Type == null ?
                "" : resource.Type.ResourceName;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            logger.Debug(DateTime.Now.ToShortTimeString() + ": Checkout window content rendered called");

            Boolean visible = (Boolean)e.NewValue;

            if (visible)
            {
                rental.Renter = null;
                rental.Resource = null;

                initializeFields();
            }
            resetFlags();

            resourceId.Focus();
        }

        private void renterLibNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            readDataFlag = true;

            Keyboard.AddKeyDownHandler(renterLibNumber, keyDownHandler);
            Keyboard.AddKeyUpHandler(renterLibNumber, keyUpHandler);
        }

        private void renterLibNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            readDataFlag = false;

            Keyboard.RemoveKeyDownHandler(renterLibNumber, keyDownHandler);
            Keyboard.RemoveKeyUpHandler(renterLibNumber, keyUpHandler);

            renter = renters.getRenterByLibraryNumber(libNumber);

            if (renter == null)
            {
                if (infoWindow == null)
                {
                    infoWindow = new InformationWindow("Unrecognized Library Number");
                }

                infoWindow.setInfoText("A renter with the library number:\n" + libNumber + "\nwas not found");
                infoWindow.ShowDialog();

                renterLibNumber.Text = libNumber;

                return;
            }

            renterName.Text = renter.FullName;
            renterLibNumber.Text = renter.LibraryNumber;
            renterEmail.Text = renter.CpEmail;
        }
        
    }
}
