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

        private static Regex reg = new Regex(@"\%(\d{13})\^(.{7})\?");

        public string LibraryNumber
        {
            get
            {
                return libNumber;
            }
        }

        public string Role
        {
            get
            {
                return role;
            }
        }

        public CardData(string rawInput)
        {
            libNumber = getLibNumberFromRawData(rawInput);
            role = getRoleFromRawData(rawInput);
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

        public static bool rawDataIsValid(String rawData)
        {
            return reg.IsMatch(rawData);
        }
        
        public static string getRoleFromRawData(string rawData)
        {
            if (rawDataIsValid(rawData))
            {
                return reg.Match(rawData).Groups[2].Value;
            }

            return "";
        }

        public static string getLibNumberFromRawData(string rawData)
        {
            if (rawDataIsValid(rawData))
            {
                string encodedNumber = reg.Match(rawData).Groups[1].Value;
                return encodedNumber + calculateCheckDigit(encodedNumber);
            }

            return "";
        }

        public static string completeLibNumber(string libNumber)
        {
            return libNumber + calculateCheckDigit(libNumber);
        }
    }
}
