using System;
using System.Numerics;

namespace bank_utility
{
    public class IBAN : BankAccountNumber
    {
        public override string Convert(string userBankNumber)
        {
            if (VerifyCheckDigit(userBankNumber))
            {
                string userIBAN = ConvertBBANToIBAN(userBankNumber);
                return userIBAN;
            }
            else
            {
                return "false";
            }
        }
        public override int CalculateCheckDigit(string userBankNumber)
        {
            return 0;
        }
        public override bool VerifyCheckDigit(string userBankNumber)
        {
            string bankNumber = ConvertBBANToMachineFormat(userBankNumber);

            int checkDigit;
            int.TryParse(bankNumber[bankNumber.Length - 1].ToString(), out checkDigit);

            int checkSum = checkDigit;
            bool isOdd = false;

            for (int i = bankNumber.Length - 2; i >= 0; i--)
            {
                int digit;
                int.TryParse(bankNumber[i].ToString(), out digit);

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
        public override void PrintBankAccountInfo(string userBankNumber)
        {
            Console.Write("IBAN number is {0}" + Environment.NewLine, userBankNumber);
        }
        private string ConvertBBANToIBAN(string userBankNumber)
        {
            string bankNumberMachineFormat = ConvertBBANToMachineFormat(userBankNumber);
            string bankNumber = bankNumberMachineFormat;
            bankNumber = bankNumber + "FI00";
            bankNumber = bankNumber.Replace("FI", "1518");

            string countryIDandDigit;

            BigInteger evaluatedNumber;
            bool isit = BigInteger.TryParse(bankNumber, out evaluatedNumber);

            BigInteger remainder = evaluatedNumber % 97;
            BigInteger checkDigit = 98 - remainder;

            if (checkDigit < 10)
                countryIDandDigit = "FI0" + checkDigit.ToString();
            else
                countryIDandDigit = "FI" + checkDigit.ToString();

            bankNumber = countryIDandDigit + bankNumberMachineFormat;

            return bankNumber;
        }
    }
}