using PolyRents.model;
using System.Data;
using PolyRents.ComputingResourcesDataSetTableAdapters;

namespace PolyRents.helpers
{
    public class ResourceTypeConverter : IConverter<ResourceType>
    {
        private ResourceTypeTableAdapter types;

        public ResourceTypeConverter()
        {
            types = ResourceTypeTableAdapter.getInstance();
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
            DataRow row = types.GetData().NewRow();

            row["ID"] = toConvert.IdResourceType;
            row["ResourceName"] = toConvert.ResourceName;
            row["Replacement Cost"] = toConvert.ReplacementCost;
            row["Overdue Cost"] = toConvert.PastDueCost;

            return row;
        }
    }
}
