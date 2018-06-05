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
    class RentalConverter : IConverter<Rental>
    {
        private Rental_HistoryTableAdapter rentalTable;
        private RenterTableAdapter renterTable;
        private ResourcesTableAdapter resourcesTable;

        public RentalConverter()
        {
            rentalTable = Rental_HistoryTableAdapter.getInstance();
            renterTable = RenterTableAdapter.getInstance();
            resourcesTable = ResourcesTableAdapter.getInstance();
        }

        public override Rental ConvertSingle(DataRow row)
        {
            Rental rental = new Rental();

            rental.IdRental = (int)(row["ID"]);
            rental.Renter = renterTable.getById((int)(row["idRenter"]));
            rental.Resource = resourcesTable.getById((int)(row["idResource"]));
            rental.CheckoutTime = (DateTime)(row["checkout"]);
            rental.CheckinTime = (DateTime)(row["checkin"]);

            return rental;
        }

        public override DataRow toDataRow(Rental toConvert)
        {
            DataRow row = rentalTable.GetData().NewRow();

            row["ID"] = toConvert.IdRental;
            row["idRenter"] = toConvert.Renter.IdRenter;
            row["idResource"] = toConvert.Resource.IdResource;
            row["checkout"] = toConvert.CheckoutTime;
            row["checkin"] = toConvert.CheckinTime;

            return row;
        }
    }
}
