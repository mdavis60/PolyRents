using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PolyRents.helpers
{
    class ResourceTypeConverter : IConverter<ResourceType>
    {
        private ComputingResourcesDataSetTableAdapters.ResourceTypeTableAdapter tableAdapter;

        public ResourceTypeConverter(ComputingResourcesDataSetTableAdapters.ResourceTypeTableAdapter tableAdapter)
        {
            this.tableAdapter = tableAdapter;
        }

        public override ResourceType ConvertSingle(DataRow row)
        {
            ResourceType resourceType = new ResourceType();

            resourceType.IdResourceType = (int)row["idResourceType"];
            resourceType.ResourceName = (string)row["Resource Name"];
            resourceType.ReplacementCost = (float)row["Replacement Cost"];
            resourceType.PastDueCost = (float)row["Overdue Cost"];

            return resourceType;
        }

        public override DataTable GetDataTable()
        {
            return tableAdapter.GetData();
        }
    }
}
