using System;
using bank_utility;

namespace bban_validator
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultBankAccount = "562009-2134961";
            //string userInput2 = "159030-776"; // 159030-776

            Console.WriteLine("Enter your bank number, ty. (e.g. 562009-2134961, 159030-776)");

            string userInput = Console.ReadLine();

            if (String.IsNullOrEmpty(userInput))
                userInput = defaultBankAccount;

            BankAccountNumber myBankNumber = new BBAN();
            myBankNumber.ProcessBankAccountNumber(userInput);

            BankAccountNumber myIBAN = new IBAN();
            myIBAN.ProcessBankAccountNumber(userInput);

            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();
        }
    }
}
