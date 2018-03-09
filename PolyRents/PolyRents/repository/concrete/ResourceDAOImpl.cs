using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.ComputingResourcesDataSetTableAdapters;
using PolyRents.model;
using System.Data;
using PolyRents.helpers;

namespace PolyRents.repository.concrete
{
    public class ResourceDAOImpl : ResourceDAO
    {
        private ResourcesTableAdapter tableAdapter;

        private ResourceConverter converter;

        public ResourceDAOImpl()
        {

        }

        public ResourceDAOImpl(ResourcesTableAdapter tableAdapter)
        {
            TableAdapter = tableAdapter;
            Converter = new ResourceConverter(TableAdapter);
        }

        public ResourceConverter Converter
        {
            get
            {
                return converter;
            }
            private set
            {
                this.converter = value;
            }
        }

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
