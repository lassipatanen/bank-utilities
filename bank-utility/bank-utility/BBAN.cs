using System;
using System.Text.RegularExpressions;

namespace bank_utility
{
    public class BBAN
    {
        // Variables
        private string _bankAccountNumber;

        // Const
        public BBAN(string userBankAccountNumber)
        {
            _bankAccountNumber = StripWhiteSpace(userBankAccountNumber);
            if (Validate())
                _bankAccountNumber = userBankAccountNumber.Replace("-", "");
        }

        // Public functions

        public bool Validate()
        {
            Regex rgx = new Regex(@"^[1-6|8]\d{0,2}\d{3}[-]?\d{2,8}$");
            if (rgx.IsMatch(_bankAccountNumber))
                return true;
            else
                return false;
        }

        public int CalculateCheckDigit(string userBankAccountNumber)
        {
            string bankAccountNumber = ConvertBBANToMachineFormat();
            int checkDigit = 0;
            int checkSum = 0;

            for (int i = bankAccountNumber.Length - 1; i >= 0; i--)
            {
                int digit;
                int.TryParse(bankAccountNumber[i].ToString(), out digit);

                int isOdd = i % 2;
                if (isOdd != 0)
                {
                    checkSum += digit;
                }
                else
                {
                    digit = digit * 2;
                    if (digit >= 10)
                        digit -= 9;
                    checkSum += digit;
                }
            }

            while (checkSum % 10 != 0)
            {
                checkSum++;
                checkDigit++;
            }
            return checkDigit;
        }

        public bool VerifyCheckDigit(string userBankAccountNumber)
        {
            string bankAccountNumber = ConvertBBANToMachineFormat(userBankAccountNumber);

            int checkDigit;
            int.TryParse(bankAccountNumber[bankAccountNumber.Length - 1].ToString(), out checkDigit);

            int checkSum = checkDigit;
            bool isOdd = false;

            for (int i = bankAccountNumber.Length - 2; i >= 0; i--)
            {
                int digit;
                int.TryParse(bankAccountNumber[i].ToString(), out digit);

                if (isOdd)
                {
                    checkSum += digit;
                    isOdd = false;
                }
                else
                {
                    digit = digit * 2;
                    if (digit >= 10)
                        digit -= 9;
                    checkSum += digit;
                    isOdd = true;
                }
            }
            return checkSum % 10 == 0;
        }

        public void PrintBankAccountInfo(string userBankAccountNumber)
        {
            Console.Write("Machine format BBAN number is {0}" + Environment.NewLine, userBankAccountNumber);
            if (VerifyCheckDigit(userBankAccountNumber))
                Console.Write("BBAN is valid." + Environment.NewLine);
            else
                Console.Write("BBAN is invalid." + Environment.NewLine);
        }
        public string GetBankAccountInfo(string userBankAccountNumber)
        {
            if (VerifyCheckDigit(userBankAccountNumber))
                return userBankAccountNumber;
            else
                return "error";
        }

        public string ConvertBBANToMachineFormat()
        {
            string machineFormatBankAccountNumber = _bankAccountNumber;
            while (machineFormatBankAccountNumber.Length < 14)
            {
                if (machineFormatBankAccountNumber.StartsWith("4") || machineFormatBankAccountNumber.StartsWith("5"))
                    machineFormatBankAccountNumber = machineFormatBankAccountNumber.Insert(7, "0");
                else
                    machineFormatBankAccountNumber = machineFormatBankAccountNumber.Insert(6, "0");
            }
            return machineFormatBankAccountNumber;
        }
        public string ConvertBBANToMachineFormat(string userBankAccountNumber)
        {
            string machineFormatBankAccountNumber = userBankAccountNumber;
            while (machineFormatBankAccountNumber.Length < 14)
            {
                if (machineFormatBankAccountNumber.StartsWith("4") || machineFormatBankAccountNumber.StartsWith("5"))
                    machineFormatBankAccountNumber = machineFormatBankAccountNumber.Insert(7, "0");
                else
                    machineFormatBankAccountNumber = machineFormatBankAccountNumber.Insert(6, "0");
            }
            return machineFormatBankAccountNumber;
        }

        private string StripWhiteSpace(string userBankAccountNumber)
        {
            string bankAccountNumber = userBankAccountNumber;
            bankAccountNumber = bankAccountNumber.Trim();
            bankAccountNumber = bankAccountNumber.Replace(" ", "");
            return bankAccountNumber;
        }

        // Override

        public override string ToString()
        {
            return _bankAccountNumber;
        }
    }
}
