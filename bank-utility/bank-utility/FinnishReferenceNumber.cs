using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace bank_utility
{
    public class FinnishReferenceNumber
    {
        // Properties
        public string ReferenceNumber { get; set; }

        // Const
        public FinnishReferenceNumber(string referenceNumber = "")
        {
            if (String.IsNullOrEmpty(referenceNumber))
                ReferenceNumber = "not set";
            else
                ReferenceNumber = referenceNumber;
        }

        // Public methods
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
            ReferenceNumber.Trim();
            ReferenceNumber.Replace(" ", "");

            Regex rgx = new Regex(@"^(?=.{0,14}$)[1-6|8][0-9]{0,2}\d{3}[-]?\d{2,8}$");
            if (rgx.IsMatch(ReferenceNumber))
                return true;
            else
                return false;
        }


        public bool Validate(string referenceNumber)
        {
            referenceNumber.Trim();
            referenceNumber.Replace(" ", "");

            Regex rgx = new Regex(@"^(?=.{0,14}$)[1-6|8][0-9]{0,2}\d{3}[-]?\d{2,8}$");
            if (rgx.IsMatch(referenceNumber))
                return true;
            else
                return false;
        }
        
        public bool ValidateCheckDigit(string referenceNumber)
        {
        	if( Validate(referenceNumber) )
        	{
        		int checkDigitToCompare = int.Parse( referenceNumber[referenceNumber.Length-1].ToString() );

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
	     	  	 
	     	  	 if( checkDigitToCompare.Equals(checkDigit))
	     	  	 	return true;
	     	  	 else
	     	  	 	return false;
        	}
        	else 
        		return false; 
        }
        
        public string FormatReferenceNumber(string referenceNumber)
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
    }
}
