using PolyRents.ComputingResourcesDataSetTableAdapters;
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

namespace PolyRents.views.addEdit
{
    /// <summary>
    /// Interaction logic for ReturnView.xaml
    /// </summary>
    public partial class ReturnView : Page
    {
        private Rental rental;
        private bool rentalFound;

        private Rental_HistoryTableAdapter rentals;
        private ResourcesTableAdapter resources;

        private InformationWindow infoWindow;
    

        public ReturnView()
        {
            rentals = Rental_HistoryTableAdapter.getInstance();
            resources = ResourcesTableAdapter.getInstance();

            rental = null;
            rentalFound = false;
            InitializeComponent();
        }

        private void resourceId_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox sent = sender as TextBox;
            int id = -1;
            List<Rental> checkedOut;

            if (sent.Text == "")
            {
                return;
            }

            if (!int.TryParse(sent.Text, out id))
            {
                //display error message
                return;
            }

            checkedOut = rentals.getRentalByResourceId(id);
                
            if (checkedOut == null || checkedOut.Count < 1)
            {
                infoWindow = new InformationWindow("Rental not found");

                infoWindow.setInfoText("A rental associated to the resource with id " +
                    id + " was not found");

                infoWindow.ShowDialog();
                return;
            }

            rentalFound = true;

            rental = checkedOut[0];
            renterName.Text = rental.Renter.FullName;
            renterEmail.Text = rental.Renter.CpEmail;

            returnButton.Focus();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            Button sent = sender as Button;

            if (((String)sent.Name).Equals("cancelButton"))
            {
                NavigationService.GoBack();
                return;
            }
            
            // only update a rental if it was found in the lookup
            if (rentalFound)
            {
                rental.CheckinTime = DateTime.Now;
                rental.Resource.Status = Status.ResourceStatus.AVAILABLE;

                rentals.updateSingle(rental);
                resources.updateSingle(rental.Resource);

                infoWindow = new InformationWindow("Item Checkin");
                infoWindow.setInfoText("Rental was successfully checked in.");

                infoWindow.ShowDialog();
            }

            NavigationService.GoBack();
        }
    }
}
