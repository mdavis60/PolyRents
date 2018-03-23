using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    public class Rental
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

        public Rental(Rental rental)
        {
            idRental = rental.idRental;
            renter = rental.renter;
            resource = rental.resource;
            checkoutTime = rental.checkoutTime;
            checkinTime = rental.checkinTime;
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

        public Renter Renter
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

        public Resource Resource
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

        public override bool Equals(object obj)
        {
            if (!(obj is Rental))
            {
                return false;
            }

            Rental other = obj as Rental;


            if (!((Renter == null && other.Renter == null) && (Renter != null && other.Renter != null)))
            {
                return false;
            }

            if (!((Resource == null && other.Resource == null) && (Resource != null && other.Resource != null)))
            {
                return false;
            }

            return idRental.Equals(other.idRental) && Renter.Equals(other.Renter) &&
                Resource.Equals(other.Resource) && CheckinTime.Equals(other.checkinTime) &&
                CheckoutTime.Equals(other.CheckoutTime);
        }
    }
}
