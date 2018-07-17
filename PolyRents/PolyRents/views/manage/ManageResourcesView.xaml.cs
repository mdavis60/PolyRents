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
using System.Collections;
using System.Timers;
using PolyRents.converters;
using PolyRents.model.collections;

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for ManageResourcesView.xaml
    /// </summary>
    public partial class ManageResourcesView : Page
    {
        private ResourcesTableAdapter resources;
        private ResourceConverter resourceConverter;

        public Resources Resources { get; set; }

        private ResourceTypeTableAdapter types;
        private ResourceTypeConverter typeConverter;

        private AddEditResourceView addEdit;
        private ConfirmationDialog deleteConfirmation;

        private Logger logger = Logger.getInstance();

        public ManageResourcesView()
        {
            resources = ResourcesTableAdapter.getInstance();
            types = ResourceTypeTableAdapter.getInstance();

            resourceConverter = new ResourceConverter();
            typeConverter = new ResourceTypeConverter();

            InitializeComponent();
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            addEdit = new AddEditResourceView(types: types.getAll().ToArray());

            NavigationService.Navigate(addEdit);

            if (addEdit.ResourceChanged)
            {
                resources.addSingle(addEdit.BoundResource);
                updateDataGrid();
            }

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            Resource toEdit = null;
            if (resourcesDataGrid.SelectedItem != null)
            {
                toEdit = resourcesDataGrid.SelectedItem as Resource;
            }
            addEdit = new AddEditResourceView(toEdit, types.getAll().ToArray(), true);

            NavigationService.Navigate(addEdit);

            if (addEdit.ResourceChanged)
            {
                resources.updateSingle(addEdit.BoundResource);
                updateDataGrid();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (resourcesDataGrid.SelectedItem == null)
            {
                return;
            }

            String confirmMessage = "Are you sure you want to delete this rental entry?";

            if (resourcesDataGrid.SelectedItems != null && resourcesDataGrid.SelectedItems.Count > 1)
            {
                confirmMessage = "Are you sure you want to delete these " + resourcesDataGrid.SelectedItems.Count + " rental entries?";
            }

            deleteConfirmation = new ConfirmationDialog("Delete Rental Confirmation", confirmMessage);

            deleteConfirmation.ShowDialog();

            if (!deleteConfirmation.YesClicked)
            {
                return;
            }

            deleteResourcesHelper(resourcesDataGrid.SelectedItems);
            updateDataGrid();
        }

        private void detailsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComputingResourcesDataSet computingResourcesDataSet = ((ComputingResourcesDataSet)(this.FindResource("computingResourcesDataSet")));

            resources.Fill(computingResourcesDataSet.Resources);
            CollectionViewSource resourcesViewSource = ((CollectionViewSource)(this.FindResource("resourcesViewSource")));
            resourcesViewSource.View.MoveCurrentToFirst();
            buildResources();
        }

        private void buildResources()
        {
            Resources = new Resources();
            resources.getAll().ForEach(Resources.Add);
            resourcesDataGrid.ItemsSource = Resources;
        }

        private void updateDataGrid()
        {
            int index = resourcesDataGrid.SelectedIndex;

            resourcesDataGrid.ItemsSource = null;
            buildResources();

            resourcesDataGrid.SelectedIndex = index;
        }

        private void deleteResourcesHelper(IList rowsToDelete)
        {
            foreach (Resource row in rowsToDelete)
            {
                resources.deleteSingle(row);
            }
        }
    }
}
