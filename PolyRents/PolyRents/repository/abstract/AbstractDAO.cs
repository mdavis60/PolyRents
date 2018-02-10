using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.repository
{
    interface AbstractDAO<T>
    {
        List<T> getAll();
        T getById();
        T updateSingle(T toUpdate);
        void deleteSingle(int id);
    }
}
