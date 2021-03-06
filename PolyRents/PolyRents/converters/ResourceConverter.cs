﻿using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PolyRents.ComputingResourcesDataSetTableAdapters;
using PolyRents.helpers;

namespace PolyRents.converters
{
    public class ResourceConverter:IConverter<Resource>
    {
        private ResourceTypeTableAdapter typeTable;
        private ResourcesTableAdapter table;

        public ResourceConverter()
        {
            table = ResourcesTableAdapter.getInstance();
            typeTable = ResourceTypeTableAdapter.getInstance();
        }

        public override Resource ConvertSingle(DataRow row)
        {
            Resource resource = new Resource();
            resource.IdResource = (int)(row["idResource"]);
            resource.Status = EnumUtil.ParseEnum<Resource.ResourceStatus>((string)(row["status"]));
            resource.StatusDescription = (row["status description"]).ToString();
            resource.Type = typeTable.getById((int)(row["idResourceType"]));
            return resource;
        }

        public override DataRow toDataRow(Resource toConvert)
        {
            DataRow row = table.GetData().NewRow();

            row["idResource"] = (int) toConvert.IdResource;
            row["status"] = toConvert.Status.ToString();
            row["status description"] = toConvert.StatusDescription;
            row["idResourceType"] = toConvert.Type.IdResourceType;

            return row;
        }
    }
}
