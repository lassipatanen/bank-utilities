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
        public Iban(string bankAccountNumber)
        {
            if (Bban.Validate(bankAccountNumber))
                _bankAccountNumber = ConvertBBANToIBAN(bankAccountNumber);
        }

        public static bool Validate(string bankAccountNumber)
        {
            Regex rgx = new Regex(@"^FI\d{2}[1-6|8]\d{13}$");
            if (
                rgx.IsMatch(bankAccountNumber.Trim().Replace(" ", "")) &&
                new Iban().VerifyIBANAccountNumber(bankAccountNumber)
                )
                return true;
            else
                return false;
        }

        public int CalculateCheckDigit(string bankAccountNumber)
        {
            return 0;
        }
        public bool VerifyCheckDigit(string bankAccountNumber)
        {
            bankAccountNumber = Bban.ConvertToMachineFormat(bankAccountNumber);

            int.TryParse(bankAccountNumber[bankAccountNumber.Length - 1].ToString(), out int checkDigit);

            int checkSum = checkDigit;
            bool isOdd = false;

            for (int i = bankAccountNumber.Length - 2; i >= 0; i--)
            {
                int.TryParse(bankAccountNumber[i].ToString(), out int digit);

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

        private string ConvertBBANToIBAN(string bankAccountNumber)
        {
            string bankNumberMachineFormat = Bban.ConvertToMachineFormat(bankAccountNumber);
            string bankNumber = bankNumberMachineFormat;
            bankNumber = bankNumber + "FI00";
            bankNumber = bankNumber.Replace("FI", "1518");

            string countryIdAndDigit;

            BigInteger.TryParse(bankNumber, out BigInteger evaluatedNumber);

            BigInteger remainder = evaluatedNumber % 97;
            BigInteger checkDigit = 98 - remainder;

            if (checkDigit < 10)
                countryIdAndDigit = "FI0" + checkDigit.ToString();
            else
                countryIdAndDigit = "FI" + checkDigit.ToString();

            bankNumber = countryIdAndDigit + bankNumberMachineFormat;

            return bankNumber;
        }
        private bool VerifyIBANAccountNumber(string bankAccountNumber)
        {
            string countryIdAndDigit = bankAccountNumber.Substring(0, 4);

            bankAccountNumber = bankAccountNumber.Replace(countryIdAndDigit, "");
            bankAccountNumber = bankAccountNumber + countryIdAndDigit;
            bankAccountNumber = bankAccountNumber.Replace("FI", "1518");

            BigInteger.TryParse(bankAccountNumber, out BigInteger eval);
            BigInteger checkSum = eval % 97;
            if (checkSum == 1)
                return true;
            else
                return false;
        }
        public static string GetBicCode(string bankAccountNumber)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("BankUtility.bic-codes.json")))
            {
                string json = textStreamReader.ReadToEnd();
                List<Bic> bicCodes = JsonConvert.DeserializeObject<List<Bic>>(json);

                string bankId;

                if (bankAccountNumber[0].Equals("3"))
                    bankId = bankAccountNumber.Substring(4, 2);
                else if (bankAccountNumber[0].Equals("4") || bankAccountNumber[0].Equals("7"))
                    bankId = bankAccountNumber.Substring(4, 3);
                else
                    bankId = bankAccountNumber.Substring(4, 1);

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