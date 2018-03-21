using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyRents.model
{
    public static class Status
    {
        public enum ResourceStatus
        {
            AVAILABLE = 0,
            CHECKED_OUT = 1,
            NEEDS_MAINTENANCE = 2,
            UNDER_MAINTENANCE = 3,
            RETIRED = 4
        }

        public static ResourceStatus stringToStatus(string inStatus)
        {
            return (ResourceStatus)Enum.Parse(typeof(ResourceStatus), inStatus);
        }

        public static string StatusToString(ResourceStatus inStatus)
        {
            return inStatus.ToString();
        }

        public static IEnumerable<String> getStatusEnumeration()
        {
            return Enum.GetNames(typeof(ResourceStatus));
        }
        
    }
}
