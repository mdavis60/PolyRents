using PolyRents.helpers;
using PolyRents.views;
using System;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using PolyRents.ComputingResourcesDataSetTableAdapters;

namespace PolyRents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window    {
        private String myStatus;

        private ManageResourcesView manageResources;
        private ManageRentersView manageRenters;
        private CheckoutWindow checkout;

        private Rental_HistoryTableAdapter rentals;

        private List<Window> myWindows;

        public ManageResourcesView ManageResources
        {
            get
            {
                return manageResources;
            }
            private set
            {
                manageResources = value;
            }
        }

        public ManageRentersView ManageRenters
        {
            get
            {
                return manageRenters;
            }
            private set
            {
                manageRenters = value;
            }
        }

        public CheckoutWindow Checkout
        {
            get
            {
                return checkout;
            }
            set
            {
                checkout = value;
            }
        }
        

        public String InformationStatus
        {
            get { return myStatus; }
            set {
                myStatus = value;
                status.Text = value;
            }
        }

        public MainWindow()
        {
            myWindows = new List<Window>();

            InitializeComponent();

            makeWindows();

            rentals = Rental_HistoryTableAdapter.getInstance();

            InformationStatus = "ready";
        }

        private void makeWindows()
        {
            ManageRenters = new ManageRentersView();
            ManageResources = new ManageResourcesView();
            Checkout = new CheckoutWindow();

            myWindows.Add(ManageResources);
            myWindows.Add(ManageRenters);
            myWindows.Add(Checkout);
        }

        private void checkoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkout == null)
            {
                Checkout = new CheckoutWindow();
                myWindows.Add(Checkout);
            }

            Checkout.ShowDialog();

            if (Checkout.RentalChanged)
            {
                rentals.addSingle(Checkout.BoundRental);
            }
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void manageResources_Click(object sender, RoutedEventArgs e)
        {
            if (manageResources == null)
            {
                ManageResources = new ManageResourcesView();
                myWindows.Add(ManageResources);
            }

            ManageResources.Show();
        }

        private void manageRenters_Click(object sender, RoutedEventArgs e)
        {
            if (manageRenters == null)
            {
                ManageRenters = new ManageRentersView();
                myWindows.Add(ManageRenters);
            }

            ManageRenters.Show();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            foreach (Window window in myWindows)
            {
                if (window != null)
                {
                    window.Close();
                }
            }

            base.OnClosing(e);
        }

        private void cardSwipWindow_Click(object sender, RoutedEventArgs e)
        {
            CardSwipeWindow cardSwipeWindow = new CardSwipeWindow();
            cardSwipeWindow.Show();
        }
    }
}
