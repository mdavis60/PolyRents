using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Windows;
using static PolyRents.model.Status;
using System.ComponentModel;

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

        public AddEditResourceView(Resource theResource = null, ResourceType[] types = null)
        {
            if (theResource == null)
            {
                theResource = new Resource();
            }

            this.resource = theResource;
            this.DataContext = theResource;

            if (types == null)
            {
                types = new ResourceType[0];
            }

            Types = types;


            InitializeComponent();

            intializeFields();

        }

        private void intializeFields()
        {
            //setup types combobox item source
            resourceType.ItemsSource = Types;
            resourceType.DisplayMemberPath = "ResourceName";

            //setup status combobox item source
            statuses = resource.Status.getStatusEnumeration();
            status.ItemsSource = Statuses;

            //set the fields
            idResource.Text = resource.IdResource.ToString();

            resourceType.SelectedValue = resource.Type;
            status.SelectedValue = resource.Status.TheStatus;

            statusDescription.Text = resource.StatusDescription;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            bool fieldsChanged = false;
            int newId = int.Parse(idResource.Text);
            Status newStatus = status.Text;
            ResourceType newType = resourceType.SelectedValue as ResourceType;
            string newStatusDescription = statusDescription.Text;
            
            if (resource.IdResource != newId)
            {
                resource.IdResource = newId;
            }
            if (resource.Status != newStatus)
            {
                resource.Status = newStatus;
                fieldsChanged = true;
            }
            if (resource.StatusDescription != newStatusDescription)
            {

            }


            base.OnClosing(e);
        }
    }
}
