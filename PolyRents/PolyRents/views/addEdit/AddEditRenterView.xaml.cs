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
    /// Interaction logic for AddEditRenterView.xaml
    /// </summary>
    public partial class AddEditRenterView : Window
    {
        private Renter renter;
        CardSwipeWindow cardSwipe;

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
                return role.SelectedIndex != -1;
            }
        }

        public Visibility ErrorVisible
        {
            get
            {
                return FormValid ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public AddEditRenterView(Renter theRenter = null)
        {
            if (theRenter == null)
            {
                theRenter = new Renter();
            }

            Roles = new String[] {"STUDENT", "FACULTY"};

            SetRenterToView(theRenter);

            InitializeComponent();

            intializeFields();
        }

        private void intializeFields()
        {
            //setup roles combobox item source
            role.ItemsSource = Roles;

            //set the fields
            idRenter.Text = renter.IdRenter.ToString();

            libNumber.Text = renter.LibraryNumber;

            firstName.Text = renter.FirstName;
            lastName.Text = renter.LastName;

            cpEmail.Text = renter.CpEmail;
            role.SelectedValue = renter.Role;

            canRent.IsChecked = renter.CanRent;

        }

        public void SetRenterToView(Renter aRenter = null)
        {
            if (aRenter == null)
            {
                aRenter = new Renter();
            }
            RenterChanged = false;

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
                Close();
            }

            if (FormValid)
            {
                newRenter.IdRenter = int.Parse(idRenter.Text);

                newRenter.LibraryNumber = libNumber.Text;

                newRenter.FirstName = firstName.Text;
                newRenter.LastName = lastName.Text; 

                newRenter.CpEmail = cpEmail.Text;
                newRenter.Role = role.SelectedValue as String;

                newRenter.CanRent = canRent.IsChecked.Value;

                RenterChanged = !renter.Equals(newRenter);

                if (RenterChanged)
                {
                    renter = newRenter;
                }
                Close();
            }
            else
            {
                ErrorLabel.Visibility = ErrorVisible;
            }

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private void swipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (cardSwipe == null)
            {
                cardSwipe = new CardSwipeWindow();
            }
            
            cardSwipe.ShowDialog();

            if(!cardSwipe.CancelPressed)
            {
                CardData cardData = cardSwipe.CardInfo;

                libNumber.Text = cardData.LibraryNumber;
                role.SelectedValue = cardData.Role;
            }

            cardSwipe.resetFlags();
        }
    }
}
