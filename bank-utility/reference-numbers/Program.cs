using System;
using bank_utility;

namespace reference_numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string validRef = "12345672";
            string invalidRef = "12345673";

            FinnishReferenceNumber myValidRef = new FinnishReferenceNumber();
            if(myValidRef.ValidateCheckDigit(validRef))
				Console.WriteLine("{0} is valid", myValidRef.FormatReferenceNumber(validRef));
			else
				Console.WriteLine("{0} is invalid", myValidRef.FormatReferenceNumber(validRef));

            FinnishReferenceNumber myInvalidRef = new FinnishReferenceNumber();
            if(myInvalidRef.ValidateCheckDigit(invalidRef))
				Console.WriteLine("{0} is valid", myValidRef.FormatReferenceNumber(invalidRef));
            else
				Console.WriteLine("{0} is invalid", myValidRef.FormatReferenceNumber(invalidRef));

            Console.WriteLine("\n-----------------------------------------");
            Console.WriteLine("Generating a new set of reference numbers");
            Console.WriteLine("-----------------------------------------\n");

            FinnishReferenceNumber myNewRef = new FinnishReferenceNumber();
            string[] referenceNumbers = myNewRef.GenerateReferenceNumber("1232534567", 5);

            foreach (string myref in referenceNumbers)
            {
                if (myNewRef.Validate(myref))
                    Console.WriteLine(myNewRef.FormatReferenceNumber(myref));
                else
                    Console.WriteLine("Error");
            }

            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();
        }
    }
}
