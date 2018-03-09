using PolyRents.repository.concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PolyRents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window    {
        private String myStatus;
        private int clickCount = 0;

        private ResourceDAOImpl myResources;
        private ResourceTypeDAOImpl myResourceTypes;

        public ResourceDAOImpl Resources
        {
            get
            {
                return myResources;
            }
            private set
            {
                this.myResources = value;
            }
        }

        public ResourceTypeDAOImpl ResourceTypes
        {
            get
            {
                return myResourceTypes;
            }

            set
            {
                this.myResourceTypes = value;
            }
        }

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

            PolyRents.ComputingResourcesDataSetTableAdapters.ResourcesTableAdapter computingResourcesDataSetResourcesTableAdapter = new PolyRents.ComputingResourcesDataSetTableAdapters.ResourcesTableAdapter();
            computingResourcesDataSetResourcesTableAdapter.Fill(computingResourcesDataSet.Resources);
            System.Windows.Data.CollectionViewSource resourcesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("resourcesViewSource")));
            resourcesViewSource.View.MoveCurrentToFirst();
        }
    }
}
