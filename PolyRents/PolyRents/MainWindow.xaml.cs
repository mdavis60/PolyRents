using PolyRents.helpers;
using PolyRents.views;
using System;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;

namespace PolyRents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window    {
        private String myStatus;

        private ManageResourcesView manageResources;

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

            InformationStatus = "ready";
        }

        private void checkoutButton_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
