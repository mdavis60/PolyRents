using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PolyRents.helpers
{
    class ResourceConverter:AbstractConverter<Resource>
    {
        public ResourceConverter()
        {

        }

        public List<Resource> convertAll(DataRowCollection rows)
        {
            List<Resource> resources = new List<Resource>();
            foreach (DataRow row in rows)
            {
                resources.Add(convertSingle(row));
            }

            return resources;
        }

        public Resource convertSingle(DataRow row)
        {
            Resource resource = new Resource();
            resource.IdResource = (int)(row["idResource"]);
            resource.Status = (Status)(row["status"]);
            resource.StatusDescription = (String)(row["statusDescription"]);
            resource.Type = null;
            return resource;
        }
    }
}
