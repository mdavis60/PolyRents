using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Windows;
using static PolyRents.model.Status;
using System.ComponentModel;
using System.Windows.Controls;

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for ResourceView.xaml
    /// </summary>
    public partial class AddEditResourceView : Window
    {
        private Resource resource;

        public Resource BoundResource
        {
            get
            {
                return resource;
            }
        }

        private Resource newResource;

        private IEnumerable<ResourceType> types;

        private bool resourceChanged;

        public bool ResourceChanged
        {
            get
            {
                return resourceChanged;
            }
            private set
            {
                resourceChanged = value;
            }
        }

        private bool formValid;

        public bool FormValid
        {
            get
            {
                return resourceType.SelectedIndex != -1 && status.SelectedIndex != -1;
            }
        }

        public Visibility ErrorVisible
        {
            get
            {
                return FormValid ? Visibility.Hidden : Visibility.Visible;
            }
        }


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

        public IEnumerable<string> Statuses
        {
            get
            {
                return Status.getStatusEnumeration();
            }
        }

        public AddEditResourceView(Resource theResource = null, ResourceType[] types = null)
        {

            if (types == null)
            {
                types = new ResourceType[0];
            }

            Types = types;

            if (theResource == null)
            {
                theResource = new Resource();
            }

            SetResourceToView(theResource);
            
            InitializeComponent();

            intializeFields();

        }

        private void intializeFields()
        {
            //setup types combobox item source
            resourceType.ItemsSource = Types;
            resourceType.DisplayMemberPath = "ResourceName";

            //setup status combobox item source
            status.ItemsSource = Statuses;

            //set the fields
            idResource.Text = resource.IdResource.ToString();

            resourceType.SelectedValue = resource.Type;
            status.SelectedValue = Status.StatusToString(resource.Status);

            statusDescription.Text = resource.StatusDescription;
        }

        public void SetResourceToView(Resource aResource = null)
        {
            if (aResource == null)
            {
                aResource = new Resource();
            }
            ResourceChanged = false;

            resource = aResource;
            newResource = new Resource(aResource);
            

            if (IsInitialized)
            {
                intializeFields();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            Button sent = sender as Button;

            if (((String)sent.Name).Equals("cancelButton"))
            {
                Close();
            }

            if (FormValid)
            {
                newResource.IdResource = int.Parse(idResource.Text);
                newResource.Type = resourceType.SelectedValue as ResourceType;
                newResource.Status = stringToStatus(status.Text);
                newResource.StatusDescription = statusDescription.Text;

                ResourceChanged = !resource.Equals(newResource);

                if (ResourceChanged)
                {
                    resource = newResource;
                }
                Close();
            }
            else
            {
                ErrorLabel.Visibility = ErrorVisible;
            }
            
        }
    }
}
