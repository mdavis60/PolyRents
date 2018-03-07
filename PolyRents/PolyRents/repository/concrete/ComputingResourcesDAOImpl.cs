using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.repository.concrete
{
    class ComputingResourcesDAOImpl
    {
        private static ComputingResourcesDataSet myDataSet;

        private static ComputingResourcesDAOImpl myInstance;

        private ComputingResourcesDAOImpl(ComputingResourcesDataSet theDataSet = null)
        {
            myDataSet = theDataSet;
        }

        public static ComputingResourcesDAOImpl getInstance(ComputingResourcesDataSet theDataSet =  null)
        {
            if (myInstance == null)
            {
                myInstance = new ComputingResourcesDAOImpl(theDataSet);
            }

            return myInstance;
        }

        public ComputingResourcesDataSet DataSet
        {
            get {
                return myDataSet;
            }
        }
    }
}
