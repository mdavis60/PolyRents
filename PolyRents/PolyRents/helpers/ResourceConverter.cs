using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PolyRents.helpers
{
    class ResourceConverter:IConverter<Resource>
    {
        public ResourceConverter()
        {
            
        }

        public override Resource ConvertSingle(DataRow row)
        {
            Resource resource = new Resource();
            resource.IdResource = (int)(row["idResource"]);
            resource.Status = (Status)(row["status"]);
            resource.StatusDescription = (String)(row["statusDescription"]);
            resource.Type = null;
            return resource;
        }

        public override DataRow toDataRow(DataTable table, Resource toConvert)
        {
            DataRow row = table.NewRow();

            row["idResource"] = toConvert.IdResource;
            row["status"] = toConvert.Status;
            row["statusDescription"] = toConvert.StatusDescription;
            row["idResourceType"] = toConvert.Type.IdResourceType;

            return row;
        }
    }
}
