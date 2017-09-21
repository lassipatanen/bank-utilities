using System;

namespace bank_utility
{
    public class BBAN
    {
        private static bool Validate(string userBankNumber)
        {
            string bankNumber = BBAN.FormatBBAN(userBankNumber);
            if (bankNumber.Length <= 14)
                return true;
            else
                return false;
        }

        public static bool VerifyCheckDigit(string userBankNumber)
        {
            string bankNumber = BBAN.FormatBBANToMachine(userBankNumber);

            // käy pankkinumero läpi 13. luvusta alkaen alaspäin
            // onko luvun sijainti parillinen vai ei
            // jos pariton, kerro luku kahdella ja vähennä siitä 9 ja lisää se kokonaissummaan
            // ja parillinen, kerro luku yhdellä ja lisää se kokonaissumaan
        }

        private static string FormatBBAN(string userBankNumber)
        {
            string bankNumber = userBankNumber;
            bankNumber = bankNumber.Trim();
            bankNumber = bankNumber.Replace(" ", "");
            bankNumber = bankNumber.Replace("-", "");
            return bankNumber;
        }

        public static string FormatBBANToMachine(string userBankNumber)
        {
            if (BBAN.Validate(userBankNumber))
            {
                string bankMachineNumber = BBAN.FormatBBAN(userBankNumber);
                while (bankMachineNumber.Length < 14)
                {
                    if (bankMachineNumber.StartsWith("4") || bankMachineNumber.StartsWith("5"))
                        bankMachineNumber = bankMachineNumber.Insert(7, "0");
                    else
                        bankMachineNumber = bankMachineNumber.Insert(6, "0");
                }
                return bankMachineNumber;
            }
            else
                return "error";
        }
    }
}
