using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace bank_utility
{
    public class IBAN
    {
        // Variables
        private string _bankAccountNumber;

        // Const
        public IBAN(string userBankAccountNumber)
        {
            _bankAccountNumber = userBankAccountNumber;
        }

        // Public functions

        public string Convert()
        {
            string bbmf = ConvertBBANToMachineFormat(_bankAccountNumber);
            if (VerifyCheckDigit(_bankAccountNumber))
                return ConvertBBANToIBAN(_bankAccountNumber);
            else
                return "bank account number is invalid";
        }

        public bool IsValidInput()
        {
            _bankAccountNumber = _bankAccountNumber.Replace("-", "");
            Regex rgx = new Regex(@"^(?=.{0,14}$)[1-6|8][0-9]{0,2}\d{3}[-]?\d{2,8}$");
            if (rgx.IsMatch(_bankAccountNumber))
                return true;
            else
                return false;
        }

        public int CalculateCheckDigit(string userBankAccountNumber)
        {
            return 0;
        }

        public bool VerifyCheckDigit(string userBankAccountNumber)
        {
            string bankNumber = this.ConvertBBANToMachineFormat(userBankAccountNumber);

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

        public void PrintBankAccountInfo(string userBankAccountNumber)
        {
            Console.Write("IBAN number is {0}" + Environment.NewLine, userBankAccountNumber);
            if (VerifyIBANAccountNumber(userBankAccountNumber))
            {
                Console.WriteLine("IBAN is valid");
                Console.WriteLine("BIC code is: {0}", GetBicCode(userBankAccountNumber));
            }
            else
                Console.WriteLine("IBAN is invalid");
        }

        public string GetBankAccountInfo(string userBankAccountNumber)
        {
            if (VerifyIBANAccountNumber(userBankAccountNumber))
                return userBankAccountNumber;
            else
                return "error";
        }

        public bool Validate()
        {
            if (IsValidInput())
                return true;
            else
                return false;
        }

        // Private functions
        public string ConvertBBANToMachineFormat(string userBankAccountNumber)
        {
            string machineFormatBankAccountNumber = _bankAccountNumber.Replace("-", "");
            while (machineFormatBankAccountNumber.Length < 14)
            {
                if (machineFormatBankAccountNumber.StartsWith("4") || machineFormatBankAccountNumber.StartsWith("5"))
                    machineFormatBankAccountNumber = machineFormatBankAccountNumber.Insert(7, "0");
                else
                    machineFormatBankAccountNumber = machineFormatBankAccountNumber.Insert(6, "0");
            }
            return machineFormatBankAccountNumber;
        }

        private string ConvertBBANToIBAN(string userBankAccountNumber)
        {
            string bankNumberMachineFormat = ConvertBBANToMachineFormat(userBankAccountNumber);
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

        private bool VerifyIBANAccountNumber(string userBankAccountNumber)
        {
            string bankNumber = userBankAccountNumber;
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

        private string GetBicCode(string userBankAccountNumber)
        {
            List<Bic> bicCodes = new List<Bic>();

            string path = @"C:\dev\bank-utilities\bank-utility\bank-utility\bic-codes.json";

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var sr = new System.IO.StreamReader(fs))
            {
                //Read file via sr.Read(), sr.ReadLine, ...
                string json = sr.ReadToEnd();
                bicCodes = JsonConvert.DeserializeObject<List<Bic>>(json);

                string bankID;

                if (userBankAccountNumber[0].Equals("3"))
                    bankID = userBankAccountNumber.Substring(4, 2);
                else if (userBankAccountNumber[0].Equals("4") || userBankAccountNumber[0].Equals("7"))
                    bankID = userBankAccountNumber.Substring(4, 3);
                else
                    bankID = userBankAccountNumber.Substring(4, 1);

                string bicCode = "";
                foreach (Bic bicObj in bicCodes)
                {
                    if (bicObj.Id == bankID)
                    {
                        bicCode = bicObj.Name;
                        break;
                    }
                }
                return bicCode;
            }
        }

        private string StripWhiteSpace(string userBankAccountNumber)
        {
            string bankNumber = userBankAccountNumber;
            bankNumber = bankNumber.Trim();
            bankNumber = bankNumber.Replace(" ", "");
            return bankNumber;
        }
    }
}