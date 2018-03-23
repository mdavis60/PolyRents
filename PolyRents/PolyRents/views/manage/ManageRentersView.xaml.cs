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

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for ManageRentersView.xaml
    /// </summary>
    public partial class ManageRentersView : Window
    {
        private RenterTableAdapter renters;
        private RenterConverter renterConverter;

        private AddEditRenterView addEdit;

        public ManageRentersView()
        {
            renters = RenterTableAdapter.getInstance();
            renterConverter = new RenterConverter();

            InitializeComponent();

            initializeData();
        }
        private void initializeData()
        {
            addEdit = new AddEditRenterView();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComputingResourcesDataSet computingResourcesDataSet = ((ComputingResourcesDataSet)(this.FindResource("computingResourcesDataSet")));

            // Load data into the table Renter. You can modify this code as needed.
            renters.Fill(computingResourcesDataSet.Renter);
            CollectionViewSource renterViewSource = ((CollectionViewSource)(this.FindResource("renterViewSource")));
            renterViewSource.View.MoveCurrentToFirst();
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            addEdit.SetRenterToView();

            addEdit.ShowDialog();

            if (addEdit.RenterChanged)
            {
                renters.addSingle(addEdit.BoundRenter);
                updateDataGrid();
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            Renter toEdit = null;

            if (renterDataGrid.SelectedItem != null)
            {
                toEdit = renterConverter.ConvertSingle((renterDataGrid.SelectedItem as DataRowView).Row);
            }

            addEdit.SetRenterToView(toEdit);

            addEdit.ShowDialog();

            if (addEdit.RenterChanged)
            {
                renters.updateSingle(addEdit.BoundRenter);
                updateDataGrid();
            }
        }

        private void detailsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (renterDataGrid.SelectedItem != null)
            {
                Renter toDelete = renterConverter.ConvertSingle((renterDataGrid.SelectedItem as DataRowView).Row);
                renters.deleteSingle(toDelete);
                updateDataGrid();
            }
        }

        private void updateDataGrid()
        {
            renterDataGrid.ItemsSource = null;
            renterDataGrid.ItemsSource = renters.GetData();
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }
    }
}
