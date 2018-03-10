using System.Collections.Generic;
using PolyRents.model;
using PolyRents.repository;
using PolyRents.helpers;
using System.Data;
using static PolyRents.ComputingResourcesDataSet;

namespace PolyRents
{


    partial class ComputingResourcesDataSet
    {
    }
}

namespace PolyRents.ComputingResourcesDataSetTableAdapters
{
    partial class ResourceTypeTableAdapter : ResourceTypeDAO
    {
        private ResourceTypeConverter converter = new ResourceTypeConverter();

        public void deleteSingle(int id)
        {
            GetData().Rows.Remove(GetData().FindByID(id));
        }

        public List<ResourceType> getAll()
        {
            return converter.ConvertAll(GetData().Rows);
        }

        public ResourceType getById(int id)
        {
            return converter.ConvertSingle(GetData().FindByID(id));
        }

        public ResourceType updatePastDueCost(ResourceType type, float newPastDueCost)
        {
            type.PastDueCost = newPastDueCost;

            ResourceTypeRow row = GetData().FindByID(type.IdResourceType);

            row["Overdue Cost"] = newPastDueCost;

            ResourceTypeRow[] rows = new ResourceTypeRow[1];
            rows[0] = row;

            GetData().LoadDataRow(rows, true);

            return type;
        }

        public ResourceType updateReplacementCost(ResourceType type, float newReplacementCost)
        {
            throw new System.NotImplementedException();
        }

        public ResourceType updateSingle(ResourceType toUpdate)
        {
            throw new System.NotImplementedException();
        }
    }

    public partial class ResourcesTableAdapter:ResourceDAO, IConverter<Resource> {
    }
}
