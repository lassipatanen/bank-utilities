using System;
using System.Text.RegularExpressions;

namespace bank_utility
{
    public abstract class BankAccountNumber
    {
        private string _bankNumber;
        public void ProcessBankAccountNumber(string userBankNumber)
        {
            _bankNumber = this.StripWhiteSpace(userBankNumber);
            if (IsValidInput())
            {
                FormatBBAN();
                _bankNumber = Convert(_bankNumber);
                PrintBankAccountInfo(_bankNumber);
            }
            else
            {
                Console.Write("Error" + Environment.NewLine);
            }

        }

        public string ConvertBBANToMachineFormat(string userBankNumber)
        {
            string bankMachineNumber = _bankNumber;
            while (bankMachineNumber.Length < 14)
            {
                if (bankMachineNumber.StartsWith("4") || bankMachineNumber.StartsWith("5"))
                    bankMachineNumber = bankMachineNumber.Insert(7, "0");
                else
                    bankMachineNumber = bankMachineNumber.Insert(6, "0");
            }
            return bankMachineNumber;
        }

        // Private functions

        private string StripWhiteSpace(string userBankNumber)
        {
            string bankNumber = userBankNumber;
            bankNumber = bankNumber.Trim();
            bankNumber = bankNumber.Replace(" ", "");
            return bankNumber;
        }
        private bool IsValidInput()
        {
            Regex rgx = new Regex(@"^(?=.{0,14}$)[1-6|8][0-9]{0,2}\d{3}[-]?\d{2,8}$");
            if (rgx.IsMatch(_bankNumber))
                return true;
            else
                return false;
        }
        private void FormatBBAN()
        {
            _bankNumber = _bankNumber.Replace("-", "");
        }

        // Abstract functions
        public abstract void PrintBankAccountInfo(string userBankNumber);
        public abstract string Convert(string userBankNumber);
        public abstract int CalculateCheckDigit(string userBankNumber);
        public abstract bool VerifyCheckDigit(string userBankNumber);
    }
}