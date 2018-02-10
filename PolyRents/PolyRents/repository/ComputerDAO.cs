using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyRents.model;

namespace PolyRents.repository
{
    interface ComputerDAO:AbstractDAO<Computer>
    {
        Computer getComputerByStateId(int stateId);
        Computer getComputerBySerialNumber(int serialNumber);
    }
}
