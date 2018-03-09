using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.helpers
{
    public abstract class IConverter<T>
    {
        public abstract DataTable GetDataTable();
        public abstract T ConvertSingle(DataRow row);
        public List<T> ConvertAll(DataRowCollection rows)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in rows)
            {
                list.Add(ConvertSingle(row));
            }

            return list;
        }

        public DataColumnCollection GetColumns()
        {
            return GetDataTable().Columns;
        }
    }
}
