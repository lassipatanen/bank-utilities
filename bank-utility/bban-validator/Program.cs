using System;
using BankUtility;

namespace bban_validator
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultBankAccount = "5620092134961";
            //string userInput2 = "159030-776"; // 159030-776, 101710-122

            Console.WriteLine("Enter your bank number, ty. (e.g. 562009-2134961, 159030-776)");

            string userInput = Console.ReadLine();

            if (String.IsNullOrEmpty(userInput))
                userInput = defaultBankAccount;

            Bban bban = new Bban(userInput);
            Console.WriteLine("BBAN is: {0}", bban.ToString());

            Iban iban = new Iban(userInput);
            Console.WriteLine("IBAN is: {0}", iban.ToString());

            string newIban = "FI2056200920134961";
            if (new Iban().Validate(newIban))
                Console.WriteLine($"{newIban} is valid.");
            else
                Console.WriteLine($"{newIban} is invalid.");

            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();
        }
    }
}
