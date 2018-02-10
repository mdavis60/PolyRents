using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.model;

namespace PolyRents.repository
{
    interface MaintenanceNoteDAO:AbstractDAO<MaintenanceNote>
    {
        List<MaintenanceNote> getNotesForResource(Resource resource);
        void deleteNotesForResource(Resource resource);
    }
}
