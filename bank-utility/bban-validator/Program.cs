using System;
using bank_utility;

namespace bban_validator
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = "562009-2134961";
            //string userInput = "159030-776";

            Console.WriteLine("My bank number is: {0}", userInput);

            // process user input
            BankAccountNumber myBankNumber = new BBAN();
            myBankNumber.ProcessBankAccountNumber(userInput);
            if (myBankNumber.VerifyCheckDigit(userInput))
                Console.WriteLine("My bank number is valid");
            else
                Console.WriteLine("My bank number is invalid");
            //Console.WriteLine("My machine format bank number is: {0}", userInput);

            // Digit Check
            //Console.Write("Verifying bank number input...\t");
            //if (BBAN.Validate(userInput))
            //	Console.Write("Bank number is valid\n");
            //else
            //	Console.WriteLine("Bank number is not valid\n");



            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();
        }
    }
}
