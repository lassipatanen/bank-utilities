using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace bank_utility
{
    public class IBAN : BankAccountNumber
    {
        public override string Convert(string userBankNumber)
        {
            if (VerifyCheckDigit(userBankNumber))
                return ConvertBBANToIBAN(userBankNumber);
            else
                return "bank account number is invalid";
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
            if (VerifyIBANAccountNumber(userBankNumber))
            {
                Console.WriteLine("IBAN is valid");
                Console.WriteLine(GetBicCode(userBankNumber));
            }
            else
                Console.WriteLine("IBAN is invalid");
        }
        private string ConvertBBANToIBAN(string userBankNumber)
        {
            string bankNumberMachineFormat = ConvertBBANToMachineFormat(userBankNumber);
            string bankNumber = bankNumberMachineFormat;
            bankNumber = bankNumber + "FI00";
            bankNumber = bankNumber.Replace("FI", "1518");

            string countryIdAndDigit;

            BigInteger evaluatedNumber;
            bool isit = BigInteger.TryParse(bankNumber, out evaluatedNumber);

            BigInteger remainder = evaluatedNumber % 97;
            BigInteger checkDigit = 98 - remainder;

            if (checkDigit < 10)
                countryIdAndDigit = "FI0" + checkDigit.ToString();
            else
                countryIdAndDigit = "FI" + checkDigit.ToString();

            bankNumber = countryIdAndDigit + bankNumberMachineFormat;

            return bankNumber;
        }
        private bool VerifyIBANAccountNumber(string userBankNumber)
        {
            string bankNumber = userBankNumber;
            string countryIdAndDigit = bankNumber.Substring(0, 4);
            bankNumber = bankNumber.Replace(countryIdAndDigit, "");
            bankNumber = bankNumber + countryIdAndDigit;
            bankNumber = bankNumber.Replace("FI", "1518");

            BigInteger eval;
            BigInteger.TryParse(bankNumber, out eval);

            BigInteger checkSum = eval % 97;

            if (checkSum == 1)
                return true;
            else
                return false;
        }

        private string GetBicCode(string userBankNumber)
        {
            List<Bic> bicCodes = new List<Bic>();
            using (StreamReader r = new StreamReader("bic-codes.json"))
            {
                string json = r.ReadToEnd();
                bicCodes = JsonConvert.DeserializeObject<List<Bic>>(json);
                if( userBankNumber[0].Equals("3") )
                {
                    string bankID = userBankNumber.Substring(0,2);
                }
                else if( userBankNumber[0].Equals("4") || userBankNumber[0].Equals("7"))
                {
                    string bankID = userBankNumber.Substring(0,3);
                }
                else
                {
                    string bankID = userBankNumber.Substring(0,1);
                }
                return bankID;
            }
        }
    }
}