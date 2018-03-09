using PolyRents.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    public class Resource
    {
        private int idResource;
        private ResourceType type;
        private Status status;
        private string statusDescription;

        public Resource()
        {

        }

        public Resource(int idResource, ResourceType type, Status status, string statusDescription)
        {
            this.idResource = idResource;
            this.type = type;
            this.status = status;
            this.statusDescription = statusDescription;
        }

        public string StatusDescription
        {
            get
            {
                return statusDescription;
            }

            set
            {
                statusDescription = value;
            }
        }

        internal Status Status
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

        internal ResourceType Type
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
    }
}
