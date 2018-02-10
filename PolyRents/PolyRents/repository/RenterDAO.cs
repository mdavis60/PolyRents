using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.model;

namespace PolyRents.repository
{
    interface RenterDAO: AbstractDAO<model.Renter>
    {
        Renter getRenterByEmail(string email);
        Renter getRenterByLibraryNumber(int libNumber);
        Renter updateRenterLibraryNumber(int idRenter, int newlibNumber);
        Renter updateRenterCanRent(int idRenter, bool newCanRent);
    }
}
