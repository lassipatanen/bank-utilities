using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace bank_utility
{
    public class VirtualBarcode
    {
        // Variables
        private int _startcodeC = 105;
        private string _mode;
        private string _bankAccountNumber;
        private decimal _sumOfBill;
        private string _referenceNumber;
        private DateTime _dueDate;
        private string _virtualBarcode;

        // Const
        public VirtualBarcode(string mode, string userBankAccountNumber, string sumOfBill, string referenceNumber, string dueDate)
        {
            // check that mode is correct
            if( mode.Equals("4") || mode.Equals("5") )
                _mode = mode;

            if(IsValidBbanOrIban(userBankAccountNumber))
                _bankAccountNumber = userBankAccountNumber;

            if(new FinnishReferenceNumber(referenceNumber).Validate() || new InternationalReferenceNumber(referenceNumber).Validate())
                _referenceNumber = referenceNumber;

            Decimal.TryParse(sumOfBill, out _sumOfBill);

            _dueDate = DateTime.Parse(dueDate);

            _virtualBarcode = BuildVirtualBarcode();
        }

        // Private functions

        private bool IsValidBbanOrIban(string userBankAccountNumber)
        {
            BBAN isBBAN = new BBAN(userBankAccountNumber);
            IBAN isIBAN = new IBAN(userBankAccountNumber);

            if(new BBAN(userBankAccountNumber).Validate() || new IBAN(userBankAccountNumber).Validate())
                return true;
            else
                return false;
        }

        private string BuildVirtualBarcode()
        {
            BBAN userBban = new BBAN(_bankAccountNumber);
            IBAN userIban = new IBAN(_bankAccountNumber);

            FinnishReferenceNumber userFinRef = new FinnishReferenceNumber(_referenceNumber);
            InternationalReferenceNumber userIntRef = new InternationalReferenceNumber(userFinRef.ToString());

            if(new FinnishReferenceNumber(_referenceNumber).Validate())
                Console.WriteLine("Finnish reference number!");
            else
                Console.WriteLine("International reference number!");

            string myref = userIntRef.GenerateReferenceNumber(userFinRef.GenerateReferenceNumber(_referenceNumber));
            myref = myref.Replace("RF", "");
            while (myref.Length < 23)
            {
                myref = myref.Insert(2, "0");
            }

            string sumOfBill = _sumOfBill.ToString();
            while (sumOfBill.Length < 8)
            {
                sumOfBill = sumOfBill.Insert(0, "0");
            }

            string myiban = userIban.ToString();

            string checkDigit = CalculateCheckDigit2(myiban, myref, sumOfBill);

            string outputVirtualBarcode = String.Format(
                "[{0}] {1} {2} {3} {4} {5} {6} {7}",
                _startcodeC.ToString(),
                _mode,
                userIban.ToString().Replace("FI", ""),
                sumOfBill,
                myref,
                _dueDate.ToString("ddMMyy"),
                checkDigit,
                "[STOP]"
            );

            return outputVirtualBarcode.Replace(" ", " ");
        }

        private string CalculateCheckDigit2(string userIban, string myref, string sumOfBill)
        {
            string bareBarcode = String.Format(
                "{0}{1}{2}{3}{4}",
                _mode,
                userIban.Replace("FI", ""),
                sumOfBill,
                myref,
                _dueDate.ToString("ddMMyy")
            );

            BigInteger checkSum = BigInteger.Parse(_startcodeC.ToString());

            for (int i = 0; i < 27; i++) 
            {
                int x = int.Parse(bareBarcode[i].ToString());
                BigInteger sum = x * i;
                checkSum += sum;
            }

            BigInteger remainder = checkSum % 103;
            checkSum = remainder;

            return checkSum.ToString();   
        }

        // Overrides

        public override string ToString()
        {
            return _virtualBarcode;
        }
    }
}
