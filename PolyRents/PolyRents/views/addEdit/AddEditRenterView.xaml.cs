using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Windows;
using static PolyRents.model.Status;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using PolyRents.helpers;
using PolyRents.ComputingResourcesDataSetTableAdapters;

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for AddEditRenterView.xaml
    /// </summary>
    public partial class AddEditRenterView : Page
    {
        private Renter renter;
        private CardDataReader cardDataReader;

        private string libNumber;
        private string role;

        private bool readDataFlag;

        private bool isEdit;

        private RenterTableAdapter renters;

        public Renter BoundRenter
        {
            get
            {
                return renter;
            }
        }

        private Renter newRenter;

        private IEnumerable<String> roles;

        private bool renterChanged;

        public IEnumerable<String> Roles
        {
            get
            {
                return roles;
            }
            set
            {
                roles = value;
            }
        }

        public bool RenterChanged
        {
            get
            {
                return renterChanged;
            }
            private set
            {
                renterChanged = value;
            }
        }

        private bool formValid;

        public bool FormValid
        {
            get
            {
                return roleSelector.SelectedIndex != -1;
            }
        }

        public Visibility ErrorVisible
        {
            get
            {
                return FormValid ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public AddEditRenterView(Renter theRenter = null, bool isEdit=false)
        {
            if (theRenter == null)
            {
                theRenter = new Renter();
            }

            Roles = new String[] {"STUDENT", "FACULTY"};

            renters = RenterTableAdapter.getInstance();

            SetRenterToView(theRenter, isEdit);

            InitializeComponent();

            intializeFields();
        }

        private void intializeFields()
        {
            //setup roles combobox item source
            roleSelector.ItemsSource = Roles;

            //set the fields
            idRenter.Text = renter.IdRenter.ToString();

            libNumberText.Text = renter.LibraryNumber;

            firstName.Text = renter.FirstName;
            lastName.Text = renter.LastName;

            cpEmail.Text = renter.CpEmail;
            roleSelector.SelectedValue = renter.Role;
            roleSelector.IsEnabled = !isEdit;

            canRent.IsChecked = renter.CanRent;

            cardDataReader = new CardDataReader(applyButton);
        }

        public void SetRenterToView(Renter aRenter = null, bool isEdit = false)
        {
            if (aRenter == null)
            {
                aRenter = new Renter();
            }
            RenterChanged = false;

            this.isEdit = isEdit;

            renter = aRenter;
            newRenter = new Renter(aRenter);


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
                NavigationService.GoBack();
                return;
            }

            if (FormValid)
            {
                newRenter.IdRenter = int.Parse(idRenter.Text);

                newRenter.LibraryNumber = libNumberText.Text;

                newRenter.FirstName = firstName.Text;
                newRenter.LastName = lastName.Text; 

                newRenter.CpEmail = cpEmail.Text;
                newRenter.Role = roleSelector.SelectedValue as String;

                newRenter.CanRent = canRent.IsChecked.Value;

                RenterChanged = !renter.Equals(newRenter);

                if (RenterChanged)
                {
                    renter = newRenter;
                    if (isEdit)
                    {
                        renters.updateSingle(renter);
                    }
                    else
                    {
                        renters.addSingle(renter);
                    }
                }
                NavigationService.GoBack();
            }
            else
            {
                ErrorLabel.Visibility = ErrorVisible;
            }

        }

        private void keyUpHandler(object sender, KeyEventArgs e)
        {
            if (readDataFlag)
            {
                cardDataReader.keyUpHandler(sender, e);
            }
        }

        private void keyDownHandler(object sender, KeyEventArgs e)
        {
            if (readDataFlag)
            {
                cardDataReader.keyDownHandler(sender, e);
            }
        }

        public void resetFlags()
        {
            libNumber = "";
            role = "";
            
            cardDataReader.resetFlags();
        }

        private void libNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            readDataFlag = true;

            Keyboard.AddKeyDownHandler(libNumberText, keyDownHandler);
            Keyboard.AddKeyUpHandler(libNumberText, keyUpHandler);

        }

        private void libNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            readDataFlag = false;

            if (cardDataReader.CardInfo != null)
            {
                libNumberText.Text = cardDataReader.CardInfo.LibraryNumber;
                roleSelector.SelectedValue = cardDataReader.CardInfo.Role;
            }

            Keyboard.RemoveKeyDownHandler(libNumberText, keyDownHandler);
            Keyboard.RemoveKeyUpHandler(libNumberText, keyUpHandler);

            resetFlags();
        }
    }
}
