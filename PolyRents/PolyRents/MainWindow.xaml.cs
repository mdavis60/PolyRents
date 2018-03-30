using PolyRents.helpers;
using PolyRents.views;
using System;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using PolyRents.ComputingResourcesDataSetTableAdapters;
using PolyRents.views.manage;
using System.Timers;
using System.Threading.Tasks;

namespace PolyRents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window    {

        private String myStatus;
        private Timer statusTimer;

        public String Status
        {
            get
            {
                return myStatus;
            }
            set
            {
                myStatus = value;
                status.Text = value;
                status.ToolTip = value == "" ? null : value;
            }
        }

        public void OnLoggerMessageChanged(object sender, MessageEventArgs e)
        {
            statusTimer.Stop();
            Status = e.Message;

            if (e.Level == "FATAL")
            {
                return;
            }

            statusTimer.Start();
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Status = "";
            });
        }

        private ManageResourcesView manageResources;
        private ManageRentersView manageRenters;
        private ManageRentalsView manageRentals;
        private CheckoutWindow checkout;

        private Logger logger = Logger.getInstance();

        public delegate void OnMessageChanged(string message);

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

            statusTimer = new Timer(15000);
            statusTimer.Elapsed += OnTimedEvent;

            logger.MessageChanged += new MessageChangedHandler(OnLoggerMessageChanged);

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
            ManageRentals = new ManageRentalsView();

            myWindows.Add(ManageResources);
            myWindows.Add(ManageRenters);
            myWindows.Add(Checkout);
            myWindows.Add(ManageRentals);
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

        private void manageRentals_Click(object sender, RoutedEventArgs e)
        {
            if (manageRentals == null)
            {
                ManageRentals = new ManageRentalsView();
                myWindows.Add(ManageRentals);
            }

            ManageRentals.Show();
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
