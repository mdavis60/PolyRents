using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Windows;
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
    public partial class CheckoutWindow : Page
    {
        private Logger logger = Logger.getInstance();

        private Rental rental;
        private Renter renter;
        private Resource resource;

        private string libNumber;
        
        private CardDataReader cardDataReader;

        private RenterTableAdapter renters;
        private ResourcesTableAdapter resources;
        private Rental_HistoryTableAdapter rentals;

        private InformationWindow infoWindow;

        private bool readDataFlag;
        private bool isEdit;

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

        public CheckoutWindow(Rental theRental = null, bool isEdit=false)
        {
            if (theRental == null)
            {
                theRental = new Rental();
            }

            renters = RenterTableAdapter.getInstance();
            resources = ResourcesTableAdapter.getInstance();
            rentals = Rental_HistoryTableAdapter.getInstance();
            
            SetRentalToView(theRental, isEdit);

            InitializeComponent();

            initializeFields();
        }

        private void keyUpHandler(object sender, KeyEventArgs e)
        {
            if (readDataFlag)
            {
                cardDataReader.keyUpHandler(sender, e);
            }
        }

        private void keyDownHandler(object sender, KeyEventArgs e)
        {
            if (readDataFlag)
            {
                cardDataReader.keyDownHandler(sender, e);
            }
        }

        public void resetFlags()
        {
            libNumber = "";
            cardDataReader.resetFlags();
        }

        public void SetRentalToView(Rental aRental = null, bool isEdit=false)
        {
            if (aRental == null)
            {
                aRental = new Rental();
            }
            this.isEdit = isEdit;

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

            cardDataReader = new CardDataReader(okButton);
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            Button sent = sender as Button;

            if (((String)sent.Name).Equals("cancelButton"))
            {
                NavigationService.GoBack();
                return;
            }

            if (FormValid)
            {
                renter = renters.getRenterByLibraryNumber(renterLibNumber.Text);
                Resource resource = resources.getById(int.Parse(resourceId.Text));
                resource.Status = EnumUtil.ParseEnum<Resource.ResourceStatus>("CHECKED_OUT");

                newRental.Renter = renter;
                newRental.Resource = resource;

                newRental.CheckoutTime = DateTime.Now;
                newRental.CheckinTime = DateTime.MinValue;

                RentalChanged = !rental.Equals(newRental);

                if (RentalChanged)
                {
                    if (isEdit)
                    {
                        rentals.updateSingle(newRental);
                    }
                    else
                    {
                        rentals.addSingle(newRental);
                    }
                    resources.updateSingle(newRental.Resource);
                }

                NavigationService.GoBack();
            }
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
                
                infoWindow = new InformationWindow("Invalid Resource Id");

                infoWindow.setInfoText("The resource with  id " + resourceId.Text + " was not found");
                infoWindow.ShowDialog();

                return;
            }

            resourceType.Text = resource.Type == null ?
                "" : resource.Type.ResourceName;
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

            if (cardDataReader.CardInfo == null)
            {
                return;
            }

            libNumber = cardDataReader.CardInfo.LibraryNumber;

            renter = renters.getRenterByLibraryNumber(libNumber);

            if (renter == null)
            {
                infoWindow = new InformationWindow("Unrecognized Library Number");
                
                infoWindow.setInfoText("A renter with the library number:\n" + libNumber + "\nwas not found");
                infoWindow.ShowDialog();

                renterLibNumber.Text = libNumber;

                return;
            }

            renterName.Text = renter.FullName;
            renterLibNumber.Text = renter.LibraryNumber;
            renterEmail.Text = renter.CpEmail;
        }

        private void renterEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox sent = sender as TextBox;

            if (string.IsNullOrEmpty(sent.Text))
            {
                return;
            }

            Renter renter = renters.getRenterByEmail(sent.Text);

            if (renter == null)
            {
                infoWindow = new InformationWindow("Unrecognized Cal Poly Email");

                infoWindow.setInfoText("A renter with the email:\n" + libNumber + "\nwas not found");
                infoWindow.ShowDialog();

                return;
            }

            renterName.Text = renter.FullName;
            renterLibNumber.Text = renter.LibraryNumber;
            renterEmail.Text = renter.CpEmail;
        }
    }
}
