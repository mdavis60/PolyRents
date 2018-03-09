using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model
{
    public class Renter
    {
        private int idRenter;
        private string firstName;
        private string lastName;
        private string libraryNumber;
        private string cpEmail;
        private string role;
        private bool canRent;

        public Renter()
        {

        }

        public Renter(int id, string firstName, string lastName, string libraryNumber, string cpEmail, string role, bool canRent = true)
        {
            this.idRenter = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.libraryNumber = libraryNumber;
            this.cpEmail = cpEmail;
            this.role = role;
        }

        public int IdRenter
        {
            get
            {
                return idRenter;
            }

            set
            {
                idRenter = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        public string LibraryNumber
        {
            get
            {
                return libraryNumber;
            }

            set
            {
                libraryNumber = value;
            }
        }

        public string CpEmail
        {
            get
            {
                return cpEmail;
            }

            set
            {
                cpEmail = value;
            }
        }

        public string Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
            }
        }

        public bool CanRent
        {
            get
            {
                return canRent;
            }

            set
            {
                canRent = value;
            }
        }
    }
}
