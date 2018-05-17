using PolyRents.ComputingResourcesDataSetTableAdapters;
using PolyRents.views.addEdit;
using PolyRents.views.manage;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private ManageResourcesView manageResources;
        private ManageRentersView manageRenters;
        private ManageRentalsView manageRentals;
        private CheckoutWindow checkout;
        private ReturnView returns;

        private Rental_HistoryTableAdapter rentals;
        private ResourcesTableAdapter resources;

        private List<Window> myWindows;

        public ReturnView Return
        {
            get
            {
                return returns;
            }
            set
            {
                returns = value;
            }
        }

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

        public ManageRentalsView ManageRentals
        {
            get
            {
                return manageRentals;
            }
            private set
            {
                manageRentals = value;
            }
        }

        public HomePage()
        {
            myWindows = new List<Window>();

            InitializeComponent();
            
            rentals = Rental_HistoryTableAdapter.getInstance();
            resources = ResourcesTableAdapter.getInstance();
        }

        private void checkoutButton_Click(object sender, RoutedEventArgs e)
        {
            Checkout = new CheckoutWindow();
           
            Checkout.SetRentalToView();

            this.NavigationService.Navigate(Checkout);
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            Return = new ReturnView();

            NavigationService.Navigate(Return);
        }

        private void manageResources_Click(object sender, RoutedEventArgs e)
        {
            ManageResources = new ManageResourcesView();

            this.NavigationService.Navigate(ManageResources);
        }

        private void manageRenters_Click(object sender, RoutedEventArgs e)
        {
            ManageRenters = new ManageRentersView();

            NavigationService.Navigate(ManageRenters);
        }

        private void manageRentals_Click(object sender, RoutedEventArgs e)
        {
            ManageRentals = new ManageRentalsView();

            NavigationService.Navigate(ManageRentals);
        }
    }
}
