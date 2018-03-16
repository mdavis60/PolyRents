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
        private static ResourceTypeTableAdapter myInstance;

        public static ResourceTypeTableAdapter getInstance()
        {
            if (myInstance == null)
            {
                myInstance = new ResourceTypeTableAdapter();
            }
            return myInstance;
        }

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

        public ResourceType updatePastDueCost(ResourceType type, decimal newPastDueCost)
        {
            type.PastDueCost = newPastDueCost;

            ResourceTypeRow row = GetData().FindByID(type.IdResourceType);

            row["Overdue Cost"] = newPastDueCost;

            ResourceTypeRow[] rows = new ResourceTypeRow[1];
            rows[0] = row;

            GetData().LoadDataRow(rows, true);

            return type;
        }

        public ResourceType updateReplacementCost(ResourceType type, decimal newReplacementCost)
        {
            throw new System.NotImplementedException();
        }

        public ResourceType updateSingle(ResourceType toUpdate)
        {
            throw new System.NotImplementedException();
        }
    }

    public partial class ResourcesTableAdapter : ResourceDAO
    {
        private ResourceConverter converter = new ResourceConverter();
        private static ResourcesTableAdapter myInstance;

        public static ResourcesTableAdapter getInstance()
        {
            if (myInstance == null)
            {
                myInstance = new ResourcesTableAdapter();
            }

            return myInstance;
        }

        public void deleteSingle(int id)
        {
            GetData().RemoveResourcesRow(GetData().FindByidResource(id));
        }

        public List<Resource> getAll()
        {
            return converter.ConvertAll(GetData().Rows);
        }

        public List<Resource> getAllResoucesByStatus(Status status)
        {
            throw new System.NotImplementedException();
        }

        public List<Resource> getAllResourcesByResourceType(ResourceType resourceType)
        {
            throw new System.NotImplementedException();
        }

        public Resource getById(int id)
        {
            return converter.ConvertSingle(GetData().FindByidResource(id));
        }

        public Resource updateSingle(Resource toUpdate)
        {
            ResourcesRow[] rows = new ResourcesRow[1];
            rows[0] =(ResourcesRow) converter.toDataRow(GetData(), toUpdate);
            GetData().LoadDataRow(rows, true);

            return toUpdate;
        }
    }
}
