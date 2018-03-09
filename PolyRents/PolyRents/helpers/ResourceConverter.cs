using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PolyRents.helpers
{
    public class ResourceConverter:IConverter<Resource>
    {
        private ComputingResourcesDataSetTableAdapters.ResourcesTableAdapter tableAdapter;

        public ResourceConverter(ComputingResourcesDataSetTableAdapters.ResourcesTableAdapter tableAdapter)
        {
            this.tableAdapter = tableAdapter;

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

        public override DataTable GetDataTable()
        {
            return tableAdapter.GetData();
        }
    }
}
