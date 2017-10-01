 using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace bank_utility
{
    public class InternationalReferenceNumber
    {
        // Properties
        public string ReferenceNumber { get; set; }

        // Const
        public InternationalReferenceNumber(string referenceNumber)
        {
            // If input is null or empty do nothing
            if (String.IsNullOrEmpty(referenceNumber))
                ReferenceNumber = "not set";
            else
                ReferenceNumber = referenceNumber;
                // Is input correctly formatted finnish reference number?
                if(new finnishReferenceNumber(ReferenceNumber).Validate())
                    // If so convert it to international format
                    ReferenceNumber = GenerateReferenceNumber(referenceNumber);
        }
        // Public funtions
        public bool Validate()
        {
            Regex rgx = new Regex(@"^RF\d{2}\d{4,20}$");
            if (rgx.IsMatch(ReferenceNumber.Trim().Replace(" ", "")))
                return true;
            else
                return false;
        }
        public bool ValidateCheckDigit()
        {
            string rf = ReferenceNumber.Substring(0, 4);
            string myInternationalRefNro = ReferenceNumber.Remove(0, 4) + rf;
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
