using System;
using BankUtility;
using System.Text.RegularExpressions;

namespace bank_barcode
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultBankAccountNumber = "FI58 1017 1000 0001 22"; // 101710-122, FI2056200920134961, 562009-2134961, FI58 1017 1000 0001 22
            string defaultSumOfBill = "482,99";
            string defaultReferenceNumber = "55958 22432 94671"; // RF712348231 , 123456, 55958 22432 94671
            string defaultDueDate = "31.1.2012";

            string bankAccountNumber;
            string sumOfBill;
            string referenceNumber;
            string dueDate;

            string userInput;

            // Main program start
            Console.WriteLine("Welcome to virtual barcode generator 9000.");

            // Ask user for details
            QueryUserDetails();

            // create virtual barcode
            VirtualBarcode userVirtualBarcode = new VirtualBarcode(
                bankAccountNumber,
                sumOfBill,
                referenceNumber,
                dueDate
            );
            Console.WriteLine(userVirtualBarcode.ToString());

            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();

            // Functions
            void QueryUserDetails()
            {
                Console.Write("What is your bank account number? ");
                userInput = Console.ReadLine();
                bankAccountNumber = (String.IsNullOrEmpty(userInput.Trim())) ? defaultBankAccountNumber : userInput;
                Console.Write("{0}\n\n", bankAccountNumber);

                Console.Write("How big is the bill? ");
                userInput = Console.ReadLine();
                sumOfBill = (String.IsNullOrEmpty(userInput.Trim())) ? defaultSumOfBill : userInput;
                Console.Write("{0}\n\n", sumOfBill);

                Console.Write("Reference number? ");
                userInput = Console.ReadLine();
                referenceNumber = (String.IsNullOrEmpty(userInput.Trim())) ? defaultReferenceNumber : userInput;
                Console.Write("{0}\n\n", referenceNumber);

                Console.Write("Due date? ");
                userInput = Console.ReadLine();
                dueDate = (String.IsNullOrEmpty(userInput.Trim())) ? defaultDueDate : userInput;
                Console.Write("{0}\n\n", dueDate);
            }
        }
    }
}
