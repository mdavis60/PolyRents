using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    class MaintenanceNote : INote
    {
        private int idMaintenance;
        private string comment;
        private DateTime timestamp;
        private String reporter;
        private Resource resource;

        public MaintenanceNote()
        {

        }

        public MaintenanceNote(int idMaintenance, Resource resource, string comment, DateTime timestamp, String reporter)
        {
            this.idMaintenance = idMaintenance;
            this.resource = resource;
            this.comment = comment;
            this.timestamp = timestamp;
        }

        public int IdMaintenance
        {
            get
            {
                return idMaintenance;
            }

            set
            {
                idMaintenance = value;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }

            set
            {
                timestamp = value;
            }
        }

        public string Reporter
        {
            get
            {
                return reporter;
            }

            set
            {
                reporter = value;
            }
        }

        internal Resource Resource
        {
            get
            {
                return resource;
            }

            set
            {
                resource = value;
            }
        }

        public string getNote()
        {
            return comment;
        }

        public DateTime getTimeStamp()
        {
            return timestamp;
        }

        public void setNote(string note)
        {
            this.comment = note;
        }

        public void setTimeStamp(DateTime timestamp)
        {
            this.timestamp = timestamp;
        }
    }
}
