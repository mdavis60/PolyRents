using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.model;

namespace PolyRents.repository
{
    interface ResourceDAO:AbstractDAO<Resource>
    {
        List<Resource> getAllResoucesByStatus(string status);
        List<Resource> getAllResourcesByResourceType(ResourceType resourceType);
    }
}
