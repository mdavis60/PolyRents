using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    class ResourceType
    {
        private string type;
        private string resourceName;
        private float replacementCost;
        private float pastDueCost;

        public string Type
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

        public string ResourceName
        {
            get
            {
                return resourceName;
            }

            set
            {
                resourceName = value;
            }
        }

        public float ReplacementCost
        {
            get
            {
                return replacementCost;
            }

            set
            {
                replacementCost = value;
            }
        }

        public float PastDueCost
        {
            get
            {
                return pastDueCost;
            }

            set
            {
                pastDueCost = value;
            }
        }
    }
}
