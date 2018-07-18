using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PolyRents.ComputingResourcesDataSetTableAdapters;

namespace PolyRents.converters
{
    class RenterConverter : IConverter<Renter>
    {
        private RenterTableAdapter renterTable;

        public RenterConverter()
        {
            renterTable = RenterTableAdapter.getInstance();
        }

        public override Renter ConvertSingle(DataRow row)
        {
            Renter renter = new Renter();
            renter.IdRenter = (int)(row["idRenter"]);
            renter.LibraryNumber = (row["libNumber"]).ToString();
            renter.FirstName = (row["FirstName"]).ToString();
            renter.LastName = (row["LastName"]).ToString();
            renter.CpEmail = (row["calpolyEmail"]).ToString();
            renter.Role = (row["role"]).ToString();
            renter.CanRent = (bool)(row["canRent"]);

            return renter;
        }

        public override DataRow toDataRow(Renter toConvert)
        {
            DataRow row = renterTable.GetData().NewRow();

            row["idRenter"] = toConvert.IdRenter;
            row["libNumber"] = toConvert.LibraryNumber;
            row["FirstName"] = toConvert.FirstName;
            row["LastName"] = toConvert.LastName;
            row["calpolyEmail"] = toConvert.CpEmail;
            row["role"] = toConvert.Role;
            row["canRent"] = toConvert.CanRent;

            return row;
        }
    }
}
