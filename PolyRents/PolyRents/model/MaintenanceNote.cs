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

        public string Reporter { get => reporter; set => reporter = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        public string MaintenanceComment { get => comment; set => comment = value; }
        public int Id { get => idMaintenance; set => idMaintenance = value; }

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
