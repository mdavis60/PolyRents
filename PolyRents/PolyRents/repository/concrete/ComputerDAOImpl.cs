using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.model;

namespace PolyRents.repository.concrete
{
    class ComputerDAOImpl : ComputerDAO
    {
        private static ComputerDAOImpl instance;
        
        public static ComputerDAOImpl getInstance()
        {
            if (instance == null)
            {
                instance = new ComputerDAOImpl();
            }

            return instance;
        }

        private ComputerDAOImpl()
        {

        }

        public void deleteSingle(int id)
        {
            throw new NotImplementedException();
        }

        public List<Computer> getAll()
        {
            throw new NotImplementedException();
        }

        public Computer getById()
        {
            throw new NotImplementedException();
        }

        public Computer getComputerBySerialNumber(int serialNumber)
        {
            throw new NotImplementedException();
        }

        public Computer getComputerByStateId(int stateId)
        {
            throw new NotImplementedException();
        }

        public Computer updateSingle(Computer toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
