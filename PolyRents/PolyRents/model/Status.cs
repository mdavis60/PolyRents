using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    public enum Status
    {
        AVAILABLE = 0,
        CHECKED_OUT = 1,
        NEEDS_MAINTENANCE = 2,
        UNDER_MAINTENANCE = 3,
        RETIRED = 4
    }
}
