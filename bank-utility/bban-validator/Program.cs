using System;
using bank_utility;

namespace bban_validator
{
    class Program
    {
        static void Main(string[] args)
        {
            //string myBankNumber = "562009-2134961";
            string myBankNumber = "159030-776";

            Console.WriteLine("My bank number is: {0}", myBankNumber);
            Console.WriteLine("My machine format bank number is: {0}", BBAN.FormatBBANToMachine(myBankNumber));

            // Digit Check
            Console.Write("Verifying bank number input...\t");
            if (BBAN.Validate(myBankNumber))
                Console.Write("Bank number is valid\n");
            else
                Console.WriteLine("Bank number is not valid\n");



            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();
        }
    }
}
