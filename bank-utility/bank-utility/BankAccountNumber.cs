using System;
using System.Text.RegularExpressions;

namespace bank_utility
{
    public abstract class BankAccountNumber
    {
        public string _bankNumber;

        public void ProcessBankAccountNumber(string userBankNumber)
        {
            _bankNumber = StripWhiteSpace(userBankNumber);
            if (IsValidInput())
            {
                _bankNumber = Convert(_bankNumber);
                //GetBankAccountInfo(_bankNumber);
            }
            else
                Console.Write("Error. Check your bank account number." + Environment.NewLine);
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

        private string StripWhiteSpace(string userBankNumber)
        {
            string bankNumber = userBankNumber;
            bankNumber = bankNumber.Trim();
            bankNumber = bankNumber.Replace(" ", "");
            return bankNumber;
        }

        public bool IsValidInput()
        {
            _bankNumber = _bankNumber.Replace("-", "");
            Regex rgx = new Regex(@"^(?=.{0,14}$)[1-6|8][0-9]{0,2}\d{3}[-]?\d{2,8}$");
            if (rgx.IsMatch(_bankNumber))
                return true;
            else
                return false;
        }

        // Abstract functions
        public abstract string Convert(string userBankNumber);
        public abstract int CalculateCheckDigit(string userBankNumber);
        public abstract bool VerifyCheckDigit(string userBankNumber);
        public abstract void PrintBankAccountInfo(string userBankNumber);
        public abstract string GetBankAccountInfo(string userBankNumber);
        public abstract bool Validate();
    }
}