using System.Collections.Generic;
using System.Data;

namespace PolyRents.converters
{
    public abstract class IConverter<T>
    {
        public abstract T ConvertSingle(DataRow row);
        public abstract DataRow toDataRow(T toConvert);
        public List<T> ConvertAll(DataRowCollection rows)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in rows)
            {
                list.Add(ConvertSingle(row));
            }

            return list;
        }
    }
}
