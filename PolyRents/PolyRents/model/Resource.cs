using PolyRents.helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    public class Resource
    {
        private int idResource;
        private ResourceType type;
        private ResourceStatus status;
        private string statusDescription;

        public enum ResourceStatus
        {
            RETURNED = 0,
            CHECKED_OUT = 1,
            NEEDS_MAINTENANCE = 2,
            UNDER_MAINTENANCE = 3,
            RETIRED = 4
        }

        public int IdResource
        {
            get
            {
                return idResource;
            }

            set
            {
                idResource = value;
            }
        }

        public ResourceType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public ResourceStatus Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public string StatusDescription
        {
            get
            {
                return statusDescription;
            }

            set
            {
                statusDescription = value == null ? "" : value;
            }
        }

        public Resource()
        {
            Status = ResourceStatus.RETURNED;
            StatusDescription = "";

        }

        public Resource(Resource other)
        {
            this.idResource = other.idResource;
            this.type = other.type;
            this.status = other.status;
            this.statusDescription = other.statusDescription;
        }

        public Resource(int idResource, ResourceType type, ResourceStatus status, string statusDescription)
        {
            this.idResource = idResource;
            this.type = type;
            this.status = status;
            this.statusDescription = statusDescription;
        }

        public override bool Equals(Object obj)
        {
            if (!(obj is Resource))
            {
                return false;
            }

            return Equals((obj as Resource));
        }

        private bool Equals(Resource other)
        {
            return IdResource.Equals(other.IdResource) && Status.Equals(other.Status) &&
                other.Type != null && Type != null && Type.Equals(other.Type) &&
                other.StatusDescription != null && StatusDescription != null && 
                StatusDescription.Equals(other.StatusDescription);
        }
    }
}
