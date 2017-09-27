using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace bank_utility
{
    public class VirtualBarcode
    {
        // Variables
        private string _bankNumber;
        private string _virtualBarcode;

        // Const
        public VirtualBarcode(string mode, string userBankNumber, string sumOfBill, string referenceNumber, string dueDate)
        {
            if (IsValidBbanOrIban(userBankNumber))
            {
                _bankNumber = userBankNumber;
                BBAN userBban = new BBAN(_bankNumber);
                IBAN userIban = new IBAN(userBban.Convert());
                FinnishReferenceNumber userFinRef = new FinnishReferenceNumber();
                InternationalReferenceNumber userIntRef = new InternationalReferenceNumber();

                string myref = userIntRef.GenerateReferenceNumber(userFinRef.GenerateReferenceNumber(referenceNumber));
                myref = myref.Replace("RF", "");
                while (myref.Length < 23)
                {
                    myref = myref.Insert(2, "0");
                }

                string mysum = sumOfBill.Replace(",", "");
                while (mysum.Length < 8)
                {
                    mysum = mysum.Insert(0, "0");
                }

                // Format due date
                //  1.7.2017
                string[] separators = { ".", "/" };
                string[] dates = dueDate.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string formattedDueDate = new DateTime(
                    int.Parse(dates[2]),
                    int.Parse(dates[1]),
                    int.Parse(dates[0])
                ).ToString("yyMMdd");
                string checkDigit = "1";

                string outputVirtualBarcode = String.Format(
                    "{0} {1} {2} {3} {4} {5} {6} {7}",
                    "X",
                    mode,
                    userIban.Convert().Replace("FI", ""),
                    mysum,
                    myref,
                    formattedDueDate,
                    checkDigit,
                    "Y"
                );

                Console.WriteLine(outputVirtualBarcode.Replace(" ", " "));
            }
            else
                Console.WriteLine("Bank account number is not valid BBAN or IBAN.");
        }

        // Public functions
        public void Print()
        {
            FormatBarcode();
            Console.WriteLine(_virtualBarcode);
        }

        private bool IsValidBbanOrIban(string userBankNumber)
        {
            // detect what format userinput is in
            Regex rgxBBAN = new Regex(@"^(?=.{0,14}$)[1-6|8][0-9]{0,2}\d{3}[-]?\d{2,8}$");
            Regex rgxIBAN = new Regex(@"^(?=.{0,14}$)[1-6|8][0-9]{0,2}\d{3}[-]?\d{2,8}$");

            if (rgxBBAN.IsMatch(userBankNumber) || rgxIBAN.IsMatch(userBankNumber))
                return true;
            else
                return false;
        }

        // Private functions
        private void FormatBarcode()
        {
            // Is user input bban or iban
            // detect input format
            // create virtual barcode based in input
        }
    }
}
