using System;

namespace bank_utility
{
    public class IBAN : BankAccountNumber
    {
        public override string Convert(string userBankNumber)
        {
            return "nope";
        }
        public override int CalculateCheckDigit(string userBankNumber)
        {
            return 0;
        }
        public override bool VerifyCheckDigit(string userBankNumber)
        {
            //string bankNumber = BBAN.FormatBBANToMachine(userBankNumber);

            // käy pankkinumero läpi 13. luvusta alkaen alaspäin
            // onko luvun sijainti parillinen vai ei
            // jos pariton, kerro luku kahdella ja vähennä siitä 9 ja lisää se kokonaissummaan
            // ja parillinen, kerro luku yhdellä ja lisää se kokonaissumaan
            return true;
        }
        public override void PrintBankAccountInfo(string userBankNumber)
        {
            Console.Write("Machine format IBAN number is {0}" + Environment.NewLine, userBankNumber);
        }
    }
}