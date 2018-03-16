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

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for ResourceView.xaml
    /// </summary>
    public partial class AddEditResourceView : Window
    {
        private IEnumerable<ResourceType> types;
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

        public String Status
        {
            get { return resource.Status.ToString(); }
            set
            {
                resource.Status = (Status)Enum.Parse(typeof(Status), value);
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

            resourceType.SelectedItem = resource.Type.IdResourceType;
            resourceType.SelectedValue = resource.Type;
        }

        private void resourceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            resource.Type = comboBox.SelectedItem as ResourceType;
        }
    }
}
