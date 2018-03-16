using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.model;

namespace PolyRents.repository
{
    interface ResourceTypeDAO:AbstractDAO<ResourceType>
    {
        ResourceType updateReplacementCost(ResourceType type, decimal newReplacementCost);
        ResourceType updatePastDueCost(ResourceType type, decimal newPastDueCost);
    }
}
