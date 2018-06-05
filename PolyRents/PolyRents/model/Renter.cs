using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PolyRents.model
{
    public class Renter : IValueConverter
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
            FirstName = "";
            LastName = "";
            CpEmail = "";
            LibraryNumber = "";
            canRent = true;
        }

        public Renter(Renter other)
        {
            this.idRenter = other.idRenter;
            this.firstName = other.firstName;
            this.lastName = other.lastName;
            this.libraryNumber = other.libraryNumber;
            this.cpEmail = other.cpEmail;
            this.role = other.role;
            this.canRent = other.canRent;

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

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
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

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Renter val = value as Renter;
            return val.FullName;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Renter))
            {
                return false;
            }

            Renter other = obj as Renter;

            return IdRenter.Equals(other.IdRenter) && 
                LibraryNumber.Equals(other.libraryNumber) &&
                cpEmail.Equals(other.cpEmail);
        }
    }
}
