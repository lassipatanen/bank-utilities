using System;
using System.Text.RegularExpressions;

namespace bank_utility
{
    public class BBAN
    {
        private string _bankAccountNumber;

        public BBAN() { }
        public BBAN(string userBankAccountNumber = "")
        {
            _bankAccountNumber = userBankAccountNumber.Trim().Replace(" ", "");
            if (Validate())
                _bankAccountNumber = userBankAccountNumber.Replace("-", "");
        }

        public bool Validate()
        {
            Regex rgx = new Regex(@"^[1-6|8]\d{0,2}\d{3}[-]?\d{2,8}$");
            if (rgx.IsMatch(_bankAccountNumber))
                return true;
            else
                return false;
        }
        public bool Validate(string bankAccountNumber)
        {
            Regex rgx = new Regex(@"^[1-6|8]\d{0,2}\d{3}[-]?\d{2,8}$");
            if (rgx.IsMatch(bankAccountNumber))
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

        public override string ToString()
        {
            return _bankAccountNumber;
        }
    }
}
