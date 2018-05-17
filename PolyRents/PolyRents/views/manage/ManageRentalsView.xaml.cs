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
using static PolyRents.ComputingResourcesDataSet;
using PolyRents.helpers;
using PolyRents.model;
using PolyRents.views;
using PolyRents.ComputingResourcesDataSetTableAdapters;
using System.Data;
using System.ComponentModel;
using PolyRents.converters;
using System.Collections;

namespace PolyRents.views.manage
{
    /// <summary>
    /// Interaction logic for ManageRentalsView.xaml
    /// </summary>
    public partial class ManageRentalsView : Page
    {
        private Rental_HistoryTableAdapter rentals;
        private RentalConverter rentalConverter;

        private CheckoutWindow checkout;
        private ConfirmationDialog deleteConfirmation;
        private InformationWindow infoWindow;

        public ManageRentalsView()
        {
            rentals = Rental_HistoryTableAdapter.getInstance();
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
            rentals.Fill(computingResourcesDataSet.Rental_History);
            CollectionViewSource rentalViewSource = ((CollectionViewSource)(this.FindResource("rental_HistoryViewSource")));
            rentalViewSource.View.MoveCurrentToFirst();
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            checkout = new CheckoutWindow();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            Rental toEdit = null;

            if (rental_HistoryDataGrid.SelectedItem != null)
            {
                toEdit = rentalConverter.ConvertSingle((rental_HistoryDataGrid.SelectedItem as DataRowView).Row);
            }

            checkout = new CheckoutWindow(toEdit, true);
            

            if (checkout.RentalChanged)
            {
                rentals.updateSingle(checkout.BoundRental);
                updateDataGrid();
            }
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

            if (deleteConfirmation == null)
            {
                deleteConfirmation = new ConfirmationDialog("Delete Rental Confirmation");
            }

            deleteConfirmation.setMessage(confirmMessage);

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
            rental_HistoryDataGrid.ItemsSource = rentals.GetData();
        }

        private void deleteRentalsHelper(IList rowsToDelete)
        {
            Rental toDelete;

            foreach (DataRowView row in rowsToDelete)
            {
                toDelete = rentalConverter.ConvertSingle(row.Row);
                rentals.deleteSingle(toDelete);
            }
        }
    }
}
