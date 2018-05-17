using System.Collections.Generic;
using PolyRents.model;
using PolyRents.repository;
using PolyRents.helpers;
using System.Data;
using static PolyRents.ComputingResourcesDataSet;
using PolyRents.converters;
using System;

namespace PolyRents
{


    partial class ComputingResourcesDataSet
    {

    }
}

namespace PolyRents.ComputingResourcesDataSetTableAdapters
{
    partial class Rental_HistoryTableAdapter : RentalDAO
    {
        private RentalConverter converter;
        private static Rental_HistoryTableAdapter myInstance;

        private Logger logger = Logger.getInstance();

        private RentalConverter Converter
        {
            get
            {
                if (converter == null)
                {
                    converter = new RentalConverter();
                }
                return converter;
            }
        }

        public static Rental_HistoryTableAdapter getInstance()
        {
            if (myInstance == null)
            {
                myInstance = new Rental_HistoryTableAdapter();
            }

            return myInstance;
        }

        public int addSingle(Rental toAdd)
        {
            return Insert(toAdd.Renter.IdRenter, toAdd.Resource.IdResource, toAdd.CheckoutTime, toAdd.CheckinTime);
        }

        public void deleteSingle(int id)
        {
            try
            {
                DeleteQuery(id);
            }
            catch(Exception ex)
            {
                logger.Debug(ex.Message);
            }
        }

        public void deleteSingle(Rental toDelete)
        {
            try
            {
                DeleteQuery(toDelete.IdRental);
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
            }
        }

        public List<Rental> getAll()
        {
            return Converter.ConvertAll(GetData().Rows);
        }

        public List<Rental> getAllRentalsByRenter(Renter renter)
        {
            throw new NotImplementedException();
        }

        public List<Rental> getAllRentalsOnDate(DateTime checkout)
        {
            throw new NotImplementedException();
        }

        public Rental getById(int id)
        {
            Rental_HistoryRow row = GetData().FindByID(id);

            return row == null ? null : Converter.ConvertSingle(row);
        }

        public List<Rental> getResourceRentalHistory(Resource resource)
        {
            throw new NotImplementedException();
        }

        public Rental updateSingle(Rental toUpdate)
        {
            int updateResult = UpdateQuery(toUpdate.Renter.IdRenter, toUpdate.Resource.IdResource, toUpdate.CheckoutTime, toUpdate.CheckinTime, toUpdate.IdRental);

            if (updateResult != 1)
            {
                logger.Debug(string.Format("Update result on rental {0} returned {1}", toUpdate.IdRental, updateResult));
            }

            return toUpdate;
        }

        public List<Rental> getRentalByResourceId(int id)
        {
            DataRowCollection rows = GetRentalByResourceId("CHECKED_OUT", id).Rows;

            if (rows.Count <1)
            {
                logger.Info("Get Rental by Resource returned 0 results");
                return null;
            }

            return Converter.ConvertAll(rows);
        }
    }
    partial class RenterTableAdapter : RenterDAO
    {
        private RenterConverter converter;
        private static RenterTableAdapter myInstance;

        private Logger logger = Logger.getInstance();

        private RenterConverter Converter
        {
            get
            {
                if (converter == null)
                {
                    converter = new RenterConverter();
                }
                return converter;
            }
        }

        public static RenterTableAdapter getInstance()
        {
            if (myInstance == null)
            {
                myInstance = new RenterTableAdapter();
            }

            return myInstance;
        }

        public int addSingle(Renter toAdd)
        {
            return Insert(toAdd.LibraryNumber, toAdd.FirstName, toAdd.LastName, toAdd.CpEmail, toAdd.Role, toAdd.CanRent);
        }

        public void deleteSingle(int id)
        {
            DeleteQuery(id);
        }

        public void deleteSingle(Renter toDelete)
        {
            DeleteQuery(toDelete.IdRenter);
        }

        public List<Renter> getAll()
        {
            return Converter.ConvertAll(GetData().Rows);
        }

        public Renter getById(int id)
        {
            RenterRow renterRow = GetData().FindByidRenter(id);

            if (renterRow == null)
            {
                logger.Warning("Get Renter by Id return no results id: " + id);
                return null;
            }
            return Converter.ConvertSingle(renterRow);
        }

        public Renter getRenterByEmail(string email)
        {
            DataRowCollection rows = GetDataByCpEmail(email).Rows;

            if (rows.Count != 1)
            {
                logger.Error(string.Format("get by email query with email {0} returned {1} results", email, rows.Count));
            }
            
            return rows.Count == 0 ? null: Converter.ConvertAll(rows)[0];
        }

        public Renter getRenterByLibraryNumber(string libNumber)
        {
            DataRowCollection rows = GetDataByLibNumber(libNumber).Rows;

            if (rows.Count != 1)
            {
                logger.Error(string.Format("get by libnumber query with libnumber {0} returned {1} results", libNumber, rows.Count));
            }

            return rows.Count == 0 ? null: Converter.ConvertAll(GetDataByLibNumber(libNumber).Rows)[0];
        }

        public Renter updateRenterCanRent(int idRenter, bool newCanRent)
        {
            throw new System.NotImplementedException();
        }

        public Renter updateRenterLibraryNumber(int idRenter, int newlibNumber)
        {
            throw new System.NotImplementedException();
        }

        public Renter updateSingle(Renter toUpdate)
        {
            int updateCount = UpdateQuery(toUpdate.LibraryNumber, toUpdate.FirstName, toUpdate.LastName, toUpdate.CpEmail, toUpdate.Role, toUpdate.CanRent, toUpdate.IdRenter);

            if (updateCount == 0)
            {
                logger.Debug(string.Format("update Renter failed on renter with id {0}", toUpdate.IdRenter));
            }
            if (updateCount > 1)
            {
                logger.Error(string.Format("update Renter updated {1} renters with renter with id {0}", toUpdate.IdRenter, updateCount));
            }

            return toUpdate;
        }
    }
    partial class ResourceTypeTableAdapter : ResourceTypeDAO
    {
        private ResourceTypeConverter converter;
        private static ResourceTypeTableAdapter myInstance;
        private Logger logger = Logger.getInstance();

        public static ResourceTypeTableAdapter getInstance()
        {
            if (myInstance == null)
            {
                myInstance = new ResourceTypeTableAdapter();
            }
            return myInstance;
        }

        private ResourceTypeConverter Converter
        {
            get
            {
                if (converter == null)
                {
                    converter = new ResourceTypeConverter();
                }
                return converter;
            }
        }

        public void deleteSingle(int id)
        {
            GetData().Rows.Remove(GetData().FindByID(id));

            Update(GetData());
        }

        public List<ResourceType> getAll()
        {
            return Converter.ConvertAll(GetData().Rows);
        }

        public ResourceType getById(int id)
        {
            ResourceTypeRow resourceTypeRow = GetData().FindByID(id);

            if (resourceTypeRow == null)
            {
                logger.Warning("Get Resource type by id returned no results. id: " + id);
                return null;
            }

            return Converter.ConvertSingle(resourceTypeRow);
        }

        public ResourceType updatePastDueCost(ResourceType type, decimal newPastDueCost)
        {
            type.PastDueCost = newPastDueCost;

            ResourceTypeRow row = GetData().FindByID(type.IdResourceType);

            row["Overdue Cost"] = newPastDueCost;

            ResourceTypeRow[] rows = new ResourceTypeRow[1];
            rows[0] = row;

            GetData().LoadDataRow(rows, true);

            Update(GetData());

            return type;
        }

        public ResourceType updateReplacementCost(ResourceType type, decimal newReplacementCost)
        {
            throw new System.NotImplementedException();
        }

        public ResourceType updateSingle(ResourceType toUpdate)
        {
            throw new System.NotImplementedException();
        }

        public int addSingle(ResourceType toAdd)
        {
            return Insert(toAdd.ResourceName, toAdd.ReplacementCost, toAdd.PastDueCost);
            
        }

        public void deleteSingle(ResourceType toDelete)
        {
            throw new System.NotImplementedException();
        }
    }

    public partial class ResourcesTableAdapter : ResourceDAO
    {
        private ResourceConverter converter;
        private static ResourcesTableAdapter myInstance;

        private Logger logger = Logger.getInstance();

        public static ResourcesTableAdapter getInstance()
        {
            if (myInstance == null)
            {
                myInstance = new ResourcesTableAdapter();
            }

            return myInstance;
        }

        public ResourceConverter Converter
        {
            get
            {
                if (converter == null)
                {
                    converter = new ResourceConverter();
                }
                return converter;
            }
        }

        public void deleteSingle(int id)
        {
            DeleteQuery(id);
        }

        public List<Resource> getAll()
        {
            return Converter.ConvertAll(GetData().Rows);
        }

        public List<Resource> getAllResoucesByStatus(string status)
        {
            return Converter.ConvertAll(GetByStatus(status).Rows);

        }

        public List<Resource> getAllResourcesByResourceType(ResourceType resourceType)
        {
            throw new System.NotImplementedException();
        }

        public Resource getById(int id)
        {
            ResourcesRow row = GetData().FindByidResource(id);

            if (row == null)
            {
                logger.Warning("get resource by id returned no results");
                return null;
            }

            return Converter.ConvertSingle(row);
        }

        public Resource updateSingle(Resource toUpdate)
        {
            UpdateQuery(toUpdate.Status.ToString(), toUpdate.StatusDescription, toUpdate.Type.IdResourceType, toUpdate.IdResource);
            return toUpdate;
        }

        public int addSingle(Resource toAdd)
        {
            return Insert(Status.StatusToString(toAdd.Status), toAdd.StatusDescription, toAdd.Type.IdResourceType);
        }

        public void deleteSingle(Resource toDelete)
        {
            DeleteQuery(toDelete.IdResource);
        }
    }
}
