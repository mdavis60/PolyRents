using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.model;

namespace PolyRents.repository
{
    interface RentalDAO:AbstractDAO<Rental>
    {
        List<Rental> getAllRentalsOnDate(DateTime checkout);
        List<Rental> getAllRentalsByRenter(Renter renter);
        List<Rental> getResourceRentalHistory(Resource resource);
        List<Rental> getRentalByResourceId(int id);
    }
}
