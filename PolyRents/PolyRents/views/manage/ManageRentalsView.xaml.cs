﻿using System;
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
using static PolyRents.ComputingResourcesDataSet;
using PolyRents.helpers;
using PolyRents.model;
using PolyRents.views;
using PolyRents.ComputingResourcesDataSetTableAdapters;
using System.Data;
using System.ComponentModel;
using PolyRents.converters;
using System.Collections;
using PolyRents.model.collections;

namespace PolyRents.views.manage
{
    /// <summary>
    /// Interaction logic for ManageRentalsView.xaml
    /// </summary>
    public partial class ManageRentalsView : Page
    {
        private Rental_HistoryTableAdapter rentalAdapter;
        private RentalConverter rentalConverter;

        public Rentals Rentals { get; set; }

        private CheckoutWindow checkout;
        private ConfirmationDialog deleteConfirmation;

        public ManageRentalsView()
        {
            rentalAdapter = Rental_HistoryTableAdapter.getInstance();
            rentalConverter = new RentalConverter();

            InitializeComponent();

            initializeData();
        }
        private void initializeData()
        {
            checkout = new CheckoutWindow();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComputingResourcesDataSet computingResourcesDataSet = ((ComputingResourcesDataSet)(this.FindResource("computingResourcesDataSet")));

            // Load data into the table Rental. You can modify this code as needed.
            rentalAdapter.Fill(computingResourcesDataSet.Rental_History);
            CollectionViewSource rentalViewSource = ((CollectionViewSource)(this.FindResource("rental_HistoryViewSource")));
            rentalViewSource.View.MoveCurrentToFirst();
            buildRentals();
        }

        private void buildRentals()
        {
            Rentals = new Rentals();
            rentalAdapter.getAll().ForEach(Rentals.Add);
            rental_HistoryDataGrid.ItemsSource = Rentals;
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            checkout = new CheckoutWindow();

            NavigationService.Navigate(checkout);
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            Rental toEdit = null;

            if (rental_HistoryDataGrid.SelectedItem != null)
            {
                toEdit = rental_HistoryDataGrid.SelectedItem as Rental;
            }

            checkout = new CheckoutWindow(toEdit, true);

            NavigationService.Navigate(checkout);
        }

        private void detailsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (rental_HistoryDataGrid.SelectedItem == null)
            {
                return;
            }

            String confirmMessage = "Are you sure you want to delete this rental entry?";

            if (rental_HistoryDataGrid.SelectedItems != null && rental_HistoryDataGrid.SelectedItems.Count > 1)
            {
                confirmMessage = "Are you sure you want to delete these " + rental_HistoryDataGrid.SelectedItems.Count + " rental entries?";
            }

            deleteConfirmation = new ConfirmationDialog("Delete Rental Confirmation", confirmMessage);

            deleteConfirmation.ShowDialog();

            if (!deleteConfirmation.YesClicked)
            {
                return;
            }

            deleteRentalsHelper(rental_HistoryDataGrid.SelectedItems);
            updateDataGrid();
        }

        private void updateDataGrid()
        {
            rental_HistoryDataGrid.ItemsSource = null;
            buildRentals();
            rental_HistoryDataGrid.ItemsSource = Rentals;
        }

        private void deleteRentalsHelper(IList rowsToDelete)
        {
            foreach (Rental row in rowsToDelete)
            {
                rentalAdapter.deleteSingle(row);
            }
        }
    }
}