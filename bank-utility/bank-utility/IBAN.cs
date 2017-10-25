using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;

namespace BankUtility
{
    public class Iban
    {
        private string _bankAccountNumber;

        public Iban() { }
        public Iban(string userBankAccountNumber)
        {
            if (new Bban(userBankAccountNumber).Validate())
                _bankAccountNumber = ConvertBBANToIBAN(userBankAccountNumber);
        }

        public bool Validate()
        {
            Regex rgx = new Regex(@"^FI\d{2}[1-6|8]\d{13}$");
            if (
                rgx.IsMatch(_bankAccountNumber) &&
                VerifyIBANAccountNumber(_bankAccountNumber)
                )
                return true;
            else
                return false;
        }
        public bool Validate(string bankAccountNumber)
        {
            Regex rgx = new Regex(@"^FI\d{2}[1-6|8]\d{13}$");
            if (
                rgx.IsMatch(bankAccountNumber.Trim().Replace(" ", "")) &&
                VerifyIBANAccountNumber(bankAccountNumber)
                )
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
            string bankNumber = new Bban(userBankAccountNumber).ConvertBBANToMachineFormat();

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

        private string ConvertBBANToIBAN(string userBankAccountNumber)
        {
            string bankNumberMachineFormat = new Bban(userBankAccountNumber).ConvertBBANToMachineFormat();
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

            BigInteger.TryParse(bankNumber, out BigInteger eval);
            BigInteger checkSum = eval % 97;
            if (checkSum == 1)
                return true;
            else
                return false;
        }
        public static string GetBicCode(string userBankAccountNumber)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("BankUtility.bic-codes.json")))
            {
                string json = textStreamReader.ReadToEnd();
                List<Bic> bicCodes = JsonConvert.DeserializeObject<List<Bic>>(json);

                string bankId;

                if (userBankAccountNumber[0].Equals("3"))
                    bankId = userBankAccountNumber.Substring(4, 2);
                else if (userBankAccountNumber[0].Equals("4") || userBankAccountNumber[0].Equals("7"))
                    bankId = userBankAccountNumber.Substring(4, 3);
                else
                    bankId = userBankAccountNumber.Substring(4, 1);

                Bic bicCode = bicCodes
                              .Where(b => b.Id == bankId)
                              .FirstOrDefault();

                return bicCode.Name;
            }
        }

        public override string ToString()
        {
            return _bankAccountNumber;
        }
    }
}