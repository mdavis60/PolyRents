using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model.collections
{
    public class Rentals : ObservableCollection<Rental>
    {
        public ObservableCollection<Rental> RentalList { get; set; }

        public Rentals()
        {
            RentalList = new ObservableCollection<Rental>();
        }
    }
}
