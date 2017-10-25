using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace BankUtility
{
    public class InternationalReferenceNumber
    {
        public string ReferenceNumber { get; set; }

        public InternationalReferenceNumber(string referenceNumber = "")
        {
            if (String.IsNullOrEmpty(referenceNumber))
                ReferenceNumber = "not set";
            else
                ReferenceNumber = referenceNumber;

            if (new FinnishReferenceNumber(ReferenceNumber).Validate())
                ReferenceNumber = GenerateReferenceNumber(referenceNumber);
        }

        public bool Validate()
        {
            Regex rgx = new Regex(@"^RF\d{2}\d{4,20}$");
            if (rgx.IsMatch(ReferenceNumber.Trim().Replace(" ", "")))
                return true;
            else
                return false;
        }
        public bool Validate(string referenceNumber)
        {
            referenceNumber = referenceNumber.Trim().Replace(" ", "");
            Regex rgx = new Regex(@"^RF\d{2}\d{4,20}$");
            if (rgx.IsMatch(referenceNumber))
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
        public bool ValidateCheckDigit(string referenceNumber)
        {
            referenceNumber = referenceNumber.Trim().Replace(" ", "");
            string rf = referenceNumber.Substring(0, 4);
            string myInternationalRefNro = referenceNumber.Remove(0, 4) + rf;
            myInternationalRefNro = myInternationalRefNro.Replace("RF", "2715");

            BigInteger.TryParse(myInternationalRefNro, out BigInteger evaluatedNumber);
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

            BigInteger.TryParse(internationalReferenceNumber, out BigInteger evaluatedNumber);
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
