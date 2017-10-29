using System.Text.RegularExpressions;

namespace BankUtility
{
    public class Bban
    {
        private string _bankAccountNumber;

        public Bban() { }
        public Bban(string userBankAccountNumber)
        {
            userBankAccountNumber = userBankAccountNumber
                                    .Trim()
                                    .Replace(" ", "")
                                    .Replace("-", "");

            if (Validate(userBankAccountNumber))
                _bankAccountNumber = userBankAccountNumber;
        }

        public static bool Validate(string bankAccountNumber)
        {
            Regex rgx = new Regex(@"^[1-6|8]\d{0,2}\d{3}[-]?\d{2,8}$");
            if (rgx.IsMatch(bankAccountNumber))
                return true;
            else
                return false;
        }

        public int CalculateCheckDigit(string bankAccountNumber)
        {
            bankAccountNumber = ConvertToMachineFormat(bankAccountNumber);
            int checkDigit = 0;
            int checkSum = 0;

            for (int i = bankAccountNumber.Length - 1; i >= 0; i--)
            {
                int.TryParse(bankAccountNumber[i].ToString(), out int digit);

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
            string bankAccountNumber = ConvertToMachineFormat(userBankAccountNumber);

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

        public static string ConvertToMachineFormat(string bankAccountNumber)
        {
            string machineFormatBankAccountNumber = bankAccountNumber;
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