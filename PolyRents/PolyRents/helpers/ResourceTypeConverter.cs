using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PolyRents.helpers
{
    public class ResourceTypeConverter : IConverter<ResourceType>
    {
        public ResourceTypeConverter()
        {

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

        public override DataRow toDataRow(DataTable table, ResourceType toConvert)
        {
            DataRow row = table.NewRow();

            row["ID"] = toConvert.IdResourceType;
            row["ResourceName"] = toConvert.ResourceName;
            row["Replacement Cost"] = toConvert.ReplacementCost;
            row["Overdue Cost"] = toConvert.PastDueCost;

            return row;
        }
    }
}
