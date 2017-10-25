using System;
using System.Text.RegularExpressions;

namespace BankUtility
{
    public class FinnishReferenceNumber
    {
        public string ReferenceNumber { get; set; }

        public FinnishReferenceNumber(string referenceNumber = "")
        {
            if (String.IsNullOrEmpty(referenceNumber))
                ReferenceNumber = "";
            else
                ReferenceNumber = referenceNumber;
        }

        public string GenerateReferenceNumber(string basePart)
        {
            int checkSum = 0;
            int weight = 7;
            for (int i = basePart.Length - 1; i > 0; i--)
            {
                int digit;
                int.TryParse(basePart[i].ToString(), out digit);

                if (weight == 7)
                {
                    checkSum += digit * weight;
                    weight = 3;
                }
                else if (weight == 3)
                {
                    checkSum += digit * weight;
                    weight = 1;
                }
                else
                {
                    checkSum += digit * weight;
                    weight = 7;
                }
            }
            int checkDigit = 0;
            if (checkSum % 10 != 0)
                checkDigit = (10 - checkSum % 10);
            else
                checkDigit = 0;

            return basePart + checkDigit.ToString();
        }
        public string[] GenerateReferenceNumber(string basePart, int count)
        {
            string[] referenceNumberSet = new string[count];
            for (int i = 0; i < count; i++)
            {
                string mybase = basePart + i;
                referenceNumberSet[i] = GenerateReferenceNumber(mybase);
            }
            return referenceNumberSet;
        }

        public bool Validate()
        {
            Regex rgx = new Regex(@"^\d{3,18}$");
            if (rgx.IsMatch(ReferenceNumber.Trim().Replace(" ", "")))
                return true;
            else
                return false;
        }
        public bool Validate(string referenceNumber)
        {
            referenceNumber = referenceNumber.Trim().Replace(" ", "");
            Regex rgx = new Regex(@"^\d{3,18}$");
            if (rgx.IsMatch(referenceNumber))
                return true;
            else
                return false;
        }

        public bool ValidateCheckDigit(string referenceNumber)
        {
            referenceNumber = referenceNumber.Trim().Replace(" ", "");
            if (Validate(referenceNumber))
            {
                int checkDigitToCompare = int.Parse(referenceNumber[referenceNumber.Length - 1].ToString());

                int checkSum = 0;
                int weight = 7;
                for (int i = referenceNumber.Length - 2; i >= 0; i--)
                {
                    int digit;
                    int.TryParse(referenceNumber[i].ToString(), out digit);

                    if (weight == 7)
                    {
                        checkSum += digit * weight;
                        weight = 3;
                    }
                    else if (weight == 3)
                    {
                        checkSum += digit * weight;
                        weight = 1;
                    }
                    else
                    {
                        checkSum += digit * weight;
                        weight = 7;
                    }
                }

                int checkDigit = 0;

                if (checkSum % 10 != 0)
                    checkDigit = (10 - checkSum % 10);
                else
                    checkDigit = 0;

                if (checkDigitToCompare.Equals(checkDigit))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public string Beautify(string referenceNumber)
        {
            string formattedReferenceNumber = "";
            int counterInsertSpace = 1;
            for (int i = referenceNumber.Length - 1; i >= 0; i--)
            {
                if (counterInsertSpace <= 5)
                    formattedReferenceNumber = referenceNumber[i].ToString() + formattedReferenceNumber;
                else
                {
                    formattedReferenceNumber = referenceNumber[i].ToString() + " " + formattedReferenceNumber;
                    counterInsertSpace = 1;
                }
                counterInsertSpace++;
            }
            return formattedReferenceNumber;
        }

        public override string ToString()
        {
            return ReferenceNumber;
        }
    }
}
