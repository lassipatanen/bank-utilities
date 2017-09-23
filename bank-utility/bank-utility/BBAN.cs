using System;

namespace bank_utility
{
    public class BBAN : BankAccountNumber
    {
        public override string Convert(string userBankNumber)
        {
            string bankNumber = ConvertBBANToMachineFormat(userBankNumber);
            return bankNumber;
        }
        public override int CalculateCheckDigit(string userBankNumber)
        {
            return 0;
        }
        public override bool VerifyCheckDigit(string userBankNumber)
        {
            string bankNumber = ConvertBBANToMachineFormat(userBankNumber);

            int checkDigit;
            int.TryParse(bankNumber[13].ToString(), out checkDigit);

            int checkSum = checkDigit;

            for (int i = 12; i >= 0; i--)
            {
                int digit;
                int.TryParse(bankNumber[i].ToString(), out digit);

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

            if (checkSum % 10 == 0)
                return true;
            else
                return false;
        }
        public override void PrintBankAccountInfo(string userBankNumber)
        {
            Console.Write("Machine format BBAN number is {0}" + Environment.NewLine, userBankNumber);
        }
    }
}
