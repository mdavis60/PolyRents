﻿using System;
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
            get
            {
                return idResourceType;
            }
            set
            {
                idResourceType = value;
            }

        }

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
>>>>>>> 114831b9646017edb5433849781949422859e309
        }
    }
}
