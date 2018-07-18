using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using PolyRents.helpers;
using PolyRents.ComputingResourcesDataSetTableAdapters;

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for ResourceView.xaml
    /// </summary>
    public partial class AddEditResourceView : Page
    {
        private Resource resource;
        private bool isEdit;

        private ResourcesTableAdapter resources;

        private Logger logger = Logger.getInstance();

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
                return EnumUtil.getEnumerable<Resource.ResourceStatus>();
            }
        }

        public AddEditResourceView(Resource theResource = null, ResourceType[] types = null, bool isEdit = false)
        {
            resources = ResourcesTableAdapter.getInstance();
            if (types == null)
            {
                types = new ResourceType[0];
            }

            Types = types;

            if (theResource == null)
            {
                theResource = new Resource();
            }

            SetResourceToView(theResource, isEdit);
            
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
            status.SelectedValue = resource.Status.ToString();

            resourceType.IsEnabled = !isEdit;

            statusDescription.Text = resource.StatusDescription;
        }

        public void SetResourceToView(Resource aResource = null, bool isEdit = false)
        {
            if (aResource == null)
            {
                aResource = new Resource();
            }
            ResourceChanged = false;
            this.isEdit = isEdit;

            resource = aResource;
            newResource = new Resource(aResource);
            

            if (IsInitialized)
            {
                intializeFields();
            }
        }



        private void Submit(object sender, RoutedEventArgs e)
        {
            Button sent = sender as Button;

            if (((String)sent.Name).Equals("cancelButton"))
            {
                ResourceChanged = false;
                NavigationService.GoBack();
                return;
            }

            if (FormValid)
            {
                newResource.IdResource = int.Parse(idResource.Text);
                newResource.Type = resourceType.SelectedValue as ResourceType;
                newResource.Status = EnumUtil.ParseEnum<Resource.ResourceStatus>(status.Text);
                newResource.StatusDescription = statusDescription.Text;

                ResourceChanged = !resource.Equals(newResource);

                if (ResourceChanged)
                {
                    resource = newResource;
                    if (isEdit)
                    {
                        resources.updateSingle(resource);
                    }
                    else
                    {
                        resources.addSingle(resource);
                    }
                }
                NavigationService.GoBack();
            }
            else
            {
                ErrorLabel.Visibility = ErrorVisible;
            }

        }
    }
}
