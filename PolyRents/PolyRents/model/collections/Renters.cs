using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model.collections
{
    public class Renters : ObservableCollection<Renter>
    {
        public ObservableCollection<Renter> RentalList { get; set; }

        public Renters()
        {
            RentalList = new ObservableCollection<Renter>();
        }
    }
}
