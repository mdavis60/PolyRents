using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    public class ResourceType
    {
        private int idResourceType;
        private string resourceName;
        private decimal replacementCost;
        private decimal pastDueCost;

        public ResourceType()
        {

        }

        public ResourceType(string resourceName, float replacementCost, float pastDueCost)
        {

        }

        public int IdResourceType
        {
            get
            {
                return idResourceType;
            }
            set
            {
                idResourceType = value;
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

        public decimal ReplacementCost
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

        public decimal PastDueCost
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

        public override bool Equals(object obj)
        {
            if (!((obj is ResourceType)))
            {
                return false;
            }
            ResourceType other = obj as ResourceType;
            return this.IdResourceType.Equals(other.IdResourceType);
        }
    }
}
