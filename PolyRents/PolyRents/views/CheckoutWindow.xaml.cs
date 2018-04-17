using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Windows;
using static PolyRents.model.Status;
using System.ComponentModel;
using System.Windows.Controls;
using PolyRents.ComputingResourcesDataSetTableAdapters;

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for CheckoutWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        private Rental rental;
        private Renter renter;
        private Resource resource;

        private CardSwipeWindow cardSwipe;

        private RenterTableAdapter renters;
        private ResourcesTableAdapter resources;

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
                return renterLibNumber.Text != "" && resourceId.Text != "";
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

            int id = 0;

            if(int.TryParse(sent.Text, out id))
            {
                resource = resources.getById(id);
            }

            resourceType.Text = resource == null || resource.Type == null ?
                "" : resource.Type.ResourceName;
        }
    }
}
