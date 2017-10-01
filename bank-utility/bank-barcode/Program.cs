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
            /*var defaults = new[]{ new{
                    accountNumber = "101710-122",
                    sumOfBill = "100"
            }};
            */

            string versionNumber = "5";
            string defaultBankAccountNumber = "562009-2134961"; // 101710-122
            string defaultSumOfBill = "19,99";
            string defaultReferenceNumber = "RF712348231"; // RF712348231 , 123456
            string defaultDueDate = "01.07.2017";

            string bankAccountNumber;
            string sumOfBill;
            string referenceNumber;
            string dueDate;

            string userInput;

            // Main program start
            Console.WriteLine("Welcome to virtual bank barcode generator 9000.");

            // Ask user for details
            QueryUserDetails();
    
            // create virtual barcode
            VirtualBarcode userBarCode = new VirtualBarcode(
                versionNumber, 
                bankAccountNumber, 
                sumOfBill, 
                referenceNumber, 
                dueDate
            );
           Console.WriteLine(userBarCode.ToString());


            // END
            Console.WriteLine("\nPress any key to continue....");
            Console.ReadKey();

            // Functions

            void QueryUserDetails()
            {
                Console.Write("What is your bank account number? ");
                userInput = Console.ReadLine();
                bankAccountNumber = (String.IsNullOrEmpty(userInput)) ? defaultBankAccountNumber : userInput;
                Console.Write("{0}\n\n", bankAccountNumber);

                Console.Write("How big is the bill? ");
                userInput = Console.ReadLine();
                sumOfBill = (String.IsNullOrEmpty(userInput)) ? defaultSumOfBill : userInput;
                Console.Write("{0}\n\n", sumOfBill);

                Console.Write("Reference base part? ");
                userInput = Console.ReadLine();
                referenceNumber = (String.IsNullOrEmpty(userInput)) ? defaultReferenceNumber : userInput;
                Console.Write("{0}\n\n", referenceNumber);

                Console.Write("Due date? ");
                userInput = Console.ReadLine();
                dueDate = (String.IsNullOrEmpty(userInput)) ? defaultDueDate : userInput;
                Console.Write("{0}\n\n", dueDate);
            }
        }
    }
}
