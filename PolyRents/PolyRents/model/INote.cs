using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    interface INote
    {
        DateTime getTimeStamp();
        void setTimeStamp(DateTime timeStamp);
        String getNote();
        void setNote(String note);
    }
}
