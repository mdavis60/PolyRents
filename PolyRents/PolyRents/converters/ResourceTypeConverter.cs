using PolyRents.model;
using System.Data;
using PolyRents.ComputingResourcesDataSetTableAdapters;

namespace PolyRents.helpers
{
    public class ResourceTypeConverter : IConverter<ResourceType>
    {
        private ResourcesTableAdapter resourceTable;

        public ResourceTypeConverter()
        {
            resourceTable = ResourcesTableAdapter.getInstance();
        }

        public override ResourceType ConvertSingle(DataRow row)
        {
            ResourceType resourceType = new ResourceType();

            resourceType.IdResourceType = (int)row["ID"];
            resourceType.ResourceName = (string)row["Resource Name"];
            resourceType.ReplacementCost = (decimal)row["Replacement Cost"];
            resourceType.PastDueCost = (decimal)row["Overdue Cost"];

            return resourceType;
        }

        public override DataRow toDataRow(ResourceType toConvert)
        {
            DataRow row = resourceTable.GetData().NewRow();

            row["ID"] = toConvert.IdResourceType;
            row["ResourceName"] = toConvert.ResourceName;
            row["Replacement Cost"] = toConvert.ReplacementCost;
            row["Overdue Cost"] = toConvert.PastDueCost;

            return row;
        }
    }
}
