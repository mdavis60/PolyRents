using PolyRents.model;
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
using System.Globalization;
using System.Windows.Markup;
using static PolyRents.model.Status;

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for ResourceView.xaml
    /// </summary>
    public partial class AddEditResourceView : Window
    {
        private IEnumerable<ResourceType> types;
        private IEnumerable<ResourceStatus> statuses;
        private Resource resource;

        public IEnumerable<ResourceType> Types
        {
            get
            {
                return types;
            }
            set
            {
                types = value;
            }
        }
        public IEnumerable<ResourceStatus> Statuses
        {
            get
            {
                return statuses;
            }
        }
        public String Status
        {
            get { return resource.Status.ToString(); }
            set
            {
                resource.Status.SetStatus(value);
            }
        }

        public AddEditResourceView(Resource context = null, ResourceType[] types = null)
        {
            if (context == null)
            {
                context = new Resource();
            }

            this.resource = context;
            this.DataContext = context;

            if (types == null)
            {
                types = new ResourceType[0];
            }

            Types = types;


            InitializeComponent();

            resourceType.ItemsSource = Types;
            resourceType.DisplayMemberPath = "ResourceName";
            
            resourceType.SelectedValue = resource.Type;

            statuses = resource.Status.getStatusEnumeration();
            status.ItemsSource = Statuses;

            status.SelectedValue = resource.Status.TheStatus;
        }

        private void resourceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            resource.Type = comboBox.SelectedItem as ResourceType;
        }

        private void status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
