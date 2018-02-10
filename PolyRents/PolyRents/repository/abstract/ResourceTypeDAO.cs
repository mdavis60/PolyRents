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
        ResourceType updateReplacementCost(ResourceType type, float newReplacementCost);
        ResourceType updatePastDueCost(ResourceType type, float newPastDueCost);
    }
}
