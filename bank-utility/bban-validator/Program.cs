using System;
using bank_utility;

namespace bban_validator
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = "562009-2134961";
            string userInput2 = "562009-213496"; // 159030-776
            string userInput3 = "159030-776"; // 159030-776


            Console.WriteLine("My bank number is: {0}", userInput);

            // process user input
            BankAccountNumber myBankNumber = new BBAN();
            myBankNumber.ProcessBankAccountNumber(userInput);

            // Digit Check
            Console.Write("Verifying bank number input...\t");
            if (myBankNumber.VerifyCheckDigit(userInput))
           	    Console.Write("Bank number is valid\n");
            else
            	Console.WriteLine("Bank number is not valid\n");

            Console.Write("Calculate checkDigit...\t");
            BankAccountNumber mySecondAccount = new BBAN();
            int cd = mySecondAccount.CalculateCheckDigit(userInput2);
            Console.Write("{0}", cd);


            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();
        }
    }
}
