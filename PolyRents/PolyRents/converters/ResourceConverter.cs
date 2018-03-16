using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PolyRents.ComputingResourcesDataSetTableAdapters;

namespace PolyRents.helpers
{
    class ResourceConverter:IConverter<Resource>
    {
        private ResourceTypeTableAdapter typeTable;
        public ResourceConverter()
        {
            this.typeTable = ResourceTypeTableAdapter.getInstance();
        }

        public override Resource ConvertSingle(DataRow row)
        {
            Resource resource = new Resource();
            resource.IdResource = (int)(row["idResource"]);
            resource.Status = new Status((string)(row["status"]));
            resource.StatusDescription = (row["status description"]).ToString();
            resource.Type = typeTable.getById((int)(row["idResourceType"]));
            return resource;
        }

        public override DataRow toDataRow(DataTable table, Resource toConvert)
        {
            DataRow row = table.NewRow();

            row["idResource"] = toConvert.IdResource;
            row["status"] = toConvert.Status.ToString();
            row["statusDescription"] = toConvert.StatusDescription;
            row["idResourceType"] = toConvert.Type.IdResourceType;

            return row;
        }
    }
}
