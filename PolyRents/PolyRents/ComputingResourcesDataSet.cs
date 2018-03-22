﻿using System.Collections.Generic;
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
    partial class RenterTableAdapter : RenterDAO
    {
        private RenterConverter converter;
        private static RenterTableAdapter myInstance;

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
            return Insert(toAdd.LibraryNumber, toAdd.FirstName, toAdd.LastName, toAdd.Role, toAdd.CpEmail,toAdd.CanRent);
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
            return Converter.ConvertSingle(GetData().FindByidRenter(id));
        }

        public Renter getRenterByEmail(string email)
        {
            return Converter.ConvertAll(GetDataByCpEmail(email).Rows)[0];
        }

        public Renter getRenterByLibraryNumber(string libNumber)
        {
            return Converter.ConvertAll(GetDataByLibNumber(libNumber).Rows)[0];
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
            UpdateQuery(toUpdate.LibraryNumber, toUpdate.FirstName, toUpdate.LastName, toUpdate.CpEmail, toUpdate.Role, toUpdate.CanRent, toUpdate.IdRenter);

            return toUpdate;
        }
    }
    partial class ResourceTypeTableAdapter : ResourceTypeDAO
    {
        private ResourceTypeConverter converter = new ResourceTypeConverter();
        private static ResourceTypeTableAdapter myInstance;

        public static ResourceTypeTableAdapter getInstance()
        {
            if (myInstance == null)
            {
                myInstance = new ResourceTypeTableAdapter();
            }
            return myInstance;
        }

        public void deleteSingle(int id)
        {
            GetData().Rows.Remove(GetData().FindByID(id));

            Update(GetData());
        }

        public List<ResourceType> getAll()
        {
            return converter.ConvertAll(GetData().Rows);
        }

        public ResourceType getById(int id)
        {
            return converter.ConvertSingle(GetData().FindByID(id));
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
            throw new System.NotImplementedException();
        }

        public List<Resource> getAllResourcesByResourceType(ResourceType resourceType)
        {
            throw new System.NotImplementedException();
        }

        public Resource getById(int id)
        {
            return Converter.ConvertSingle(GetData().FindByidResource(id));
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
