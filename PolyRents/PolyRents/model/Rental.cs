using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    class Rental
    {
        private int idRental;
        private Renter renter;
        private Resource resource;
        private DateTime checkoutTime;
        private DateTime checkinTime;

        public Rental()
        {

        }

        public Rental(int idRental, Renter renter, Resource resource, DateTime checkoutTime, DateTime checkinTime)
        {
            this.idRental = idRental;
            this.renter = renter;
            this.resource = resource;
            this.checkoutTime = checkoutTime;
            this.checkinTime = checkinTime;
        }

        public int IdRental
        {
            get
            {
                return idRental;
            }

            set
            {
                idRental = value;
            }
        }

        internal Renter Renter
        {
            get
            {
                return renter;
            }

            set
            {
                renter = value;
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

        public DateTime CheckoutTime
        {
            get
            {
                return checkoutTime;
            }

            set
            {
                checkoutTime = value;
            }
        }

        public DateTime CheckinTime
        {
            get
            {
                return checkinTime;
            }

            set
            {
                checkinTime = value;
            }
        }
    }
}
