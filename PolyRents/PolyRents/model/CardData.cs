using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolyRents.model
{
    public class CardData
    {
        private String rawInput;

        private long encodedNumber;
        private int checkDigit;

        private String libNumber;
        private String role;

        private static Regex reg = new Regex(@"\%(\d{ 13})\^(.{7})\?");

        public String RawInput
        {
            get
            {
                return rawInput;
            }

            private set
            {
                rawInput = value;
            }
        }

        public long EncodedNumber
        {
            get
            {
                return encodedNumber;
            }
            private set
            {
                encodedNumber = value;
            }
        }

        public int CheckDigit
        {
            get
            {
                return checkDigit;
            }
            private set
            {
                checkDigit = value;
            }
        }

        public String LibraryNumber
        {
            get
            {
                return libNumber;
            }
            set
            {
                libNumber = value;
            }
        }

        public String Role
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


        public CardData()
        {
            libNumber = "";
            role = "";
        }

        public CardData(String libNumber, String role)
        {
            setData(libNumber, role);
        }

        public CardData(String rawInput)
        {
            RawInput = rawInput;

            LibraryNumber = parseLibraryNumber();
            Role = parseRole();

            long.TryParse(libNumber, out encodedNumber);

            CheckDigit = calculateCheckDigit();

            LibraryNumber += CheckDigit;
        }

        public void setData(String libNumber, String role)
        {
            LibraryNumber = libNumber;
            Role = role;
            long.TryParse(libNumber, out encodedNumber);

            RawInput = makeMockRawData(libNumber, role);

            CheckDigit = calculateCheckDigit();

            LibraryNumber += CheckDigit;
        }

        public int calculateCheckDigit()
        {
            return calculateCheckDigit(libNumber);
        }

        private static int calculateCheckDigit(string libNumber)
        {
            int sum = 0;

            int stepNum = 0;

            for (int i = 0; i < libNumber.Length; i++)
            {
                stepNum = int.Parse(libNumber.Substring(i, 1));

                //Odd position work
                if (i % 2 == 0)
                {
                    stepNum = stepNum * 2;

                    // add the digits together if it is greater than 9
                    stepNum = stepNum > 9 ? stepNum / 10 + stepNum % 10 : stepNum;
                }

                sum += stepNum;

            }

            return 10 - sum % 10;
        }

        private String parseLibraryNumber()
        {
            return rawInput.Substring(1, 13);
        }

        private String parseRole()
        {
            int roleIndex = rawInput.IndexOf("^") + 1;
            int lastIndex = rawInput.IndexOf("?");
            int roleLength = lastIndex - roleIndex;

            return rawInput.Substring(roleIndex, roleLength);
        }

        public static bool rawDataIsValid(String rawData)
        {
            return reg.IsMatch(rawData);
        }
        
        public static string getRoleFromRawData(string rawData)
        {
            if (rawDataIsValid(rawData))
            {
                return reg.Match(rawData).NextMatch().Value;
            }

            return "";
        }

        public static string getLibNumberFromRawData(string rawData)
        {
            if (rawDataIsValid(rawData))
            {
                string encodedNumber = reg.Match(rawData).Value;
                return encodedNumber + calculateCheckDigit(encodedNumber);
            }

            return "";
        }

        private String makeMockRawData(String libNumber, String role)
        {
            return "%" + libNumber + "^" + role + "?";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CardData))
            {
                return false;
            }

            CardData other = obj as CardData;

            return LibraryNumber.Equals(other.LibraryNumber) &&
                Role.Equals(other.Role);
        }

    }
}
