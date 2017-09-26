using System;
using bank_utility;

namespace reference_numbers
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Good morning, sir! How can we help you today?");

			int option = 0;

			while (option == 0) 
			{
				// Generate referecene numbers
				Console.WriteLine("1. Generate reference numbers.");
				// Validate reference number
				Console.WriteLine("2. Validate reference number.");
				// Convert finnish reference number to international reference number
				Console.WriteLine("3. Convert finish reference number to international reference number");

                string selected = Console.Read().ToString();

                if (selected.Equals("1"))
					Console.WriteLine("You selected 1");
				if(selected.Equals("2"))
					Console.WriteLine("You selected 2");
                if (selected.Equals("3"))
                    Console.WriteLine("You selected 3");
			}	



        	/*
            string validRef = "12345672";
            string invalidRef = "12345673";

            FinnishReferenceNumber myValidRef = new FinnishReferenceNumber();
            if (myValidRef.ValidateCheckDigit(validRef))
                Console.WriteLine("{0} is valid", myValidRef.FormatReferenceNumber(validRef));
            else
                Console.WriteLine("{0} is invalid", myValidRef.FormatReferenceNumber(validRef));

            FinnishReferenceNumber myInvalidRef = new FinnishReferenceNumber();
            if (myInvalidRef.ValidateCheckDigit(invalidRef))
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

            Console.WriteLine("\nInternational reference number");
            InternationalReferenceNumber myIntRef = new InternationalReferenceNumber();
            string r = myIntRef.GenerateReferenceNumber("12345672"); // 12345672, 2348236
            Console.WriteLine(r);

            if (myIntRef.Validate(r))
                Console.WriteLine("\nInternational reference number {0} is valid", r);
            else
                Console.WriteLine("\nInternational reference number {0} invalid", r);
            */

            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();
        }
    }
}
