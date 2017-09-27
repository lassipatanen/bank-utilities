using System;
using bank_utility;
using System.Text.RegularExpressions;

namespace bank_barcode
{
    class Program
    {
        static void Main(string[] args)
        {
            // init variables with defaults
            string defaultBankAccountNumber = "101710-122";
            string defaultSumOfBill = "100";
            string defaultReferenceNumber = "123456";
            string defaultDueDate = "1.7.2017";
            string versionNumber = "5";

            string bankAccountNumber;
            string sumOfBill;
            string referenceNumber;
            string dueDate;

            string userInput;

            // Main program start
            Console.WriteLine("Welcome to virtual bank barcode generator 9000.");

            // Ask user for details
            QueryUser();

            // create virtual barcode
            VirtualBarcode userBarCode = new VirtualBarcode(versionNumber, bankAccountNumber, sumOfBill, referenceNumber, dueDate);
            userBarCode.Print();


            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();

            // Functions

            void QueryUser()
            {
                Console.WriteLine("What is your bank account number?");
                userInput = Console.ReadLine();
                bankAccountNumber = (String.IsNullOrEmpty(userInput)) ? defaultBankAccountNumber : userInput;

                Console.WriteLine("How big is the bill? (e)");
                userInput = Console.ReadLine();
                sumOfBill = (String.IsNullOrEmpty(userInput)) ? defaultSumOfBill : userInput;

                Console.WriteLine("Reference base part?");
                userInput = Console.ReadLine();
                referenceNumber = (String.IsNullOrEmpty(userInput)) ? defaultReferenceNumber : userInput;

                Console.WriteLine("Due date?");
                userInput = Console.ReadLine();
                dueDate = (String.IsNullOrEmpty(userInput)) ? defaultDueDate : userInput;
            }
        }
    }
}
