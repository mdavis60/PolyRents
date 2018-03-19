using PolyRents.helpers;
using PolyRents.model;
using PolyRents.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using static PolyRents.ComputingResourcesDataSet;

namespace PolyRents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window    {
        private String myStatus;
        private int clickCount = 0;
        private ResourceConverter resourceConverter;
        private ResourceTypeConverter resourceTypeConverter;
        public ComputingResourcesDataSetTableAdapters.ResourcesTableAdapter resourcesTableAdapter;
        public ComputingResourcesDataSetTableAdapters.ResourceTypeTableAdapter resourceTypeTableAdapter;
        public ComputingResourcesDataSet computingResourcesDataSet;

        public String Status
        {
            get { return myStatus; }
            set {
                myStatus = value;
                status.Text = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            computingResourcesDataSet = ((ComputingResourcesDataSet)(this.FindResource("computingResourcesDataSet")));
            resourcesTableAdapter = ComputingResourcesDataSetTableAdapters.ResourcesTableAdapter.getInstance();
            resourceTypeTableAdapter = ComputingResourcesDataSetTableAdapters.ResourceTypeTableAdapter.getInstance();

            resourceConverter = new ResourceConverter();
            resourceTypeConverter = new ResourceTypeConverter();

            Status = "ready";
            
        }

        public void setStatusMessage(String value)
        {
            Status = value;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            clickCount++;
            setStatusMessage("" + clickCount);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PolyRents.ComputingResourcesDataSet computingResourcesDataSet = ((PolyRents.ComputingResourcesDataSet)(this.FindResource("computingResourcesDataSet")));
            // Load data into the table Resources. You can modify this code as needed.
 
            resourcesTableAdapter.Fill(computingResourcesDataSet.Resources);
            CollectionViewSource resourcesViewSource = ((CollectionViewSource)(this.FindResource("resourcesViewSource")));
            resourcesViewSource.View.MoveCurrentToFirst();
        }

        private void windowButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditRenterView renterView = new AddEditRenterView();
            renterView.Show();
        }

        private void resourcesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            setStatusMessage(sender.ToString() + "double clicked");
        }

        private void resourceButton_Click(object sender, RoutedEventArgs e)
        {
            
            Resource resource = resourceConverter.ConvertSingle((resourcesDataGrid.SelectedItem as DataRowView).Row);

            AddEditResourceView resourceView = new AddEditResourceView(resource, resourceTypeTableAdapter.getAll().ToArray(), resource.Status.getStatusEnumeration());
            resourceView.Show();
        }
    }
}
