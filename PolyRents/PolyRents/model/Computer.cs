using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    class Computer
    {
        private string serialNumber;
        private int stateId;
        private string vendor;
        private string model;
        private string lastUsername;

        public Computer()
        {

        }

        public Computer(string serialNumber, int stateId, string vendor, string model, string lastUsername)
        {
            this.serialNumber = serialNumber;
            this.stateId = stateId;
            this.vendor = vendor;
            this.model = model;
            this.lastUsername = lastUsername;
        }

        public string SerialNumber
        {
            get
            {
                return serialNumber;
            }

            set
            {
                serialNumber = value;
            }
        }

        public int StateId
        {
            get
            {
                return stateId;
            }

            set
            {
                stateId = value;
            }
        }

        public string Vendor
        {
            get
            {
                return vendor;
            }

            set
            {
                vendor = value;
            }
        }

        public string Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }

        public string LastUsername
        {
            get
            {
                return lastUsername;
            }

            set
            {
                lastUsername = value;
            }
        }
    }
}
