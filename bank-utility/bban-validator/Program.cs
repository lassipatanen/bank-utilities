using System;
using bank_utility;

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

            BBAN myBban = new BBAN(userInput);
            Console.WriteLine("BBAN is: {0}", myBban.ToString());

            IBAN myIban = new IBAN(userInput);
            Console.WriteLine("BBAN is: {0}", myIban.ToString());

            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();
        }
    }
}
