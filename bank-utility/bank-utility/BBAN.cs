using System;

namespace bank_utility
{
    public class BBAN : BankAccountNumber
    {
        public override string Convert(string userBankNumber)
        {
            return ConvertBBANToMachineFormat(userBankNumber);
        }

        public override int CalculateCheckDigit(string userBankNumber)
        {
            string bankNumber = ConvertBBANToMachineFormat(userBankNumber);
            int checkDigit = 0;
            int checkSum = 0;

            for (int i = bankNumber.Length - 1; i >= 0; i--)
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

            while (checkSum % 10 != 0)
            {
                checkSum++;
                checkDigit++;
            }
            return checkDigit;
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
            Console.Write("Machine format BBAN number is {0}" + Environment.NewLine, userBankNumber);
            if (VerifyCheckDigit(userBankNumber))
                Console.Write("BBAN is valid." + Environment.NewLine);
            else
                Console.Write("BBAN is invalid." + Environment.NewLine);
        }
    }
}
