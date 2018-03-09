using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.ComputingResourcesDataSetTableAdapters;
using PolyRents.model;
using System.Data;

namespace PolyRents.repository.concrete
{
    class ResourceDAOImpl : ResourceDAO
    {
        private ComputingResourcesDataSetTableAdapters.ResourcesTableAdapter tableAdapter;

        public ResourcesTableAdapter TableAdapter
        {
            get
            {
                return tableAdapter;
            }
            set
            {
                this.tableAdapter = value;
            }
        }

        public void deleteSingle(int id)
        {
            throw new NotImplementedException();
        }

        public List<Resource> getAll()
        {
            throw new NotImplementedException();
        }

        public List<Resource> getAllResoucesByStatus(Status status)
        {
            throw new NotImplementedException();
        }

        public List<Resource> getAllResourcesByResourceType(ResourceType resourceType)
        {
            throw new NotImplementedException();
        }

        public Resource getById()
        {
            throw new NotImplementedException();
        }

        public Resource updateSingle(Resource toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
