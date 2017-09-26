using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace bank_utility
{
    public class InternationalReferenceNumber
    {
        // Properties
        public string ReferenceNumber { get; set; }

        // Const
        public InternationalReferenceNumber()
        {
            //
        }

        public bool Validate(string internationalReferenceNumber)
        {
            string rf = internationalReferenceNumber.Substring(0, 4);
            string myInternationalRefNro = internationalReferenceNumber.Remove(0, 4) + rf;
            myInternationalRefNro = myInternationalRefNro.Replace("RF", "2715");

            BigInteger evaluatedNumber;
            BigInteger.TryParse(myInternationalRefNro, out evaluatedNumber);

            BigInteger remainder = evaluatedNumber % 97;

            if (remainder == 1)
                return true;
            else
                return false;
        }

        public string GenerateReferenceNumber(string finnishReferenceNumber)
        {
            string internationalReferenceNumber = finnishReferenceNumber + "271500";
            string countryIdAndDigit;

            BigInteger evaluatedNumber;
            BigInteger.TryParse(internationalReferenceNumber, out evaluatedNumber);

            BigInteger remainder = evaluatedNumber % 97;
            BigInteger checkDigit = 98 - remainder;

            if (checkDigit < 10)
                countryIdAndDigit = "RF0" + checkDigit.ToString();
            else
                countryIdAndDigit = "RF" + checkDigit.ToString();

            internationalReferenceNumber = countryIdAndDigit + finnishReferenceNumber;

            return internationalReferenceNumber;
        }
    }
}
