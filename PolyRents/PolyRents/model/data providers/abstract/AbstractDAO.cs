using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.repository
{
    interface AbstractDAO<T>
    {
        int addSingle(T toAdd);
        List<T> getAll();
        T getById(int id);
        T updateSingle(T toUpdate);
        void deleteSingle(int id);
        void deleteSingle(T toDelete);
    }
}
