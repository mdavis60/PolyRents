using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    public class RenterNote : INote
    {
        private int idRenterNote;
        private Renter renter;
        private string comment;
        private DateTime timeStamp;

        public string getNote()
        {
            return this.comment;
        }

        public DateTime getTimeStamp()
        {
            return this.timeStamp;
        }

        public void setNote(string note)
        {
            this.comment = note;
        }

        public void setTimeStamp(DateTime timeStamp)
        {
            this.timeStamp = timeStamp;
        }
    }
}
