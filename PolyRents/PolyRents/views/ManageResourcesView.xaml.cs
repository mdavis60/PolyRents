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

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for ManageResourcesView.xaml
    /// </summary>
    public partial class ManageResourcesView : Window
    {
        private ResourcesTableAdapter resources;
        private ResourceConverter resourceConverter;

        private ResourceTypeTableAdapter types;
        private ResourceTypeConverter typeConverter;

        private AddEditResourceView addEdit;

        public ManageResourcesView()
        {
            resources = ResourcesTableAdapter.getInstance();
            types = ResourceTypeTableAdapter.getInstance();

            resourceConverter = new ResourceConverter();
            typeConverter = new ResourceTypeConverter();

            InitializeComponent();

            initializeData();
        }

        private void initializeData()
        {
            addEdit = new AddEditResourceView(types:types.getAll().ToArray());
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            addEdit.SetResourceToView(new Resource());

            addEdit.ShowDialog();

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
                toEdit = resourceConverter.ConvertSingle((resourcesDataGrid.SelectedItem as DataRowView).Row);
            }

            addEdit.SetResourceToView(toEdit);

            addEdit.ShowDialog();

            if (addEdit.ResourceChanged)
            {
                resources.updateSingle(addEdit.BoundResource);
                updateDataGrid();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (resourcesDataGrid.SelectedItem != null)
            {
                Resource toDelete = resourceConverter.ConvertSingle((resourcesDataGrid.SelectedItem as DataRowView).Row);
                resources.deleteSingle(toDelete);
                updateDataGrid();
            }
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
        }

        private void updateDataGrid()
        {
            resourcesDataGrid.ItemsSource = null;
            resourcesDataGrid.ItemsSource = resources.GetData();
        }
        

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }
    }
}
