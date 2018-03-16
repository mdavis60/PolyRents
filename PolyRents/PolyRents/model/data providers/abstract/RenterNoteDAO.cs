using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.model;

namespace PolyRents.repository
{
    interface RenterNoteDAO:AbstractDAO<RenterNote>
    {
        List<RenterNote> getAllNotesForRenter(Renter renter);
        void deleteAllNotesForRenter(Renter renter);
    }
}
