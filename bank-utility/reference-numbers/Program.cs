using System;
using bank_utility;

namespace reference_numbers
{
    class Program
    {
        static void Main(string[] args)
        {

            var option = 0;
            while (option == 0)
            {
                Console.Clear();
                Console.WriteLine("Good morning, sir! How can we help you today?");
                // Generate referecene numbers
                Console.WriteLine("1. Generate reference numbers.");
                // Validate reference number
                Console.WriteLine("2. Validate reference number.");
                // Convert finnish reference number to international reference number
                Console.WriteLine("3. Convert finish reference number to international reference number");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("How many reference numbers do ou want to generate?");
                        // end
                        option = 1;
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Enter the reference number to validate.");
                        // end
                        option = 2;
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine("Enter finnish reference number to convert it in to international format.");
                        FinnishReferenceNumber userRefNro = new FinnishReferenceNumber(Console.ReadLine());
                        if (userRefNro.Validate())
                        {
                            InternationalReferenceNumber irf = new InternationalReferenceNumber(userRefNro.ToString());
                            Console.WriteLine(irf.ReferenceNumber);
                        }
                        // end
                        option = 3;
                        break;
                }
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
