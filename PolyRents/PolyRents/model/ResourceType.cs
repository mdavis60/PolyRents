using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    class ResourceType
    {
        private int idResourceType;
        private string type;
        private string resourceName;
        private float replacementCost;
        private float pastDueCost;

        public ResourceType()
        {

        }

        public ResourceType(string type, string resourceName, float replacementCost, float pastDueCost)
        {

        }

        public int IdResourceType
        {
            get { return this.idResourceType; }
            set { this.idResourceType = value;  }
        }
    }
}
