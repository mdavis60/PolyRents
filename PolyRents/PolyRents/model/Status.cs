using System;

namespace PolyRents.model
{
    public class Status
    {
        public enum ResourceStatus
        {
            AVAILABLE = 0,
            CHECKED_OUT = 1,
            NEEDS_MAINTENANCE = 2,
            UNDER_MAINTENANCE = 3,
            RETIRED = 4
        }

        private ResourceStatus status;
        private String myStatus;

        public Status(string status)
        {
            SetStatus(status);

        }

        public void SetStatus(string newStatus)
        {
            if (!Enum.TryParse<ResourceStatus>(newStatus.ToUpper(), out status))
            {
                status = ResourceStatus.AVAILABLE;
            }

            myStatus = status.ToString();
        }

        public override string ToString()
        {
            return myStatus;
        }
    }
}
