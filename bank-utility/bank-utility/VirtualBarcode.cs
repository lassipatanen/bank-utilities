using System;
using System.Numerics;

namespace BankUtility
{
    public class VirtualBarcode
    {
        private DateTime _dueDate;
        private decimal _sumOfBill;
        private string _bankAccountNumber;
        private string _mode;
        private string _referenceNumber;
        private string _startcodeC = "105";
        private string _virtualBarcode;

        public VirtualBarcode(
            string userBankAccountNumber, 
            string sumOfBill, 
            string referenceNumber, 
            string dueDate
            )
        {
            if (new Bban().Validate(userBankAccountNumber))
                _bankAccountNumber = new Iban(userBankAccountNumber).ToString().Trim().Replace(" ", "");
            else if (new Iban().Validate(userBankAccountNumber))
                _bankAccountNumber = userBankAccountNumber.Trim().Replace(" ", "");
            else
                throw new Exception("Invalid bank account number!");

            if (!Decimal.TryParse(sumOfBill, out _sumOfBill))
                throw new Exception("Invalid sum of bill!");

            if (new FinnishReferenceNumber().Validate(referenceNumber))
            {
                _mode = "4";
                if (new FinnishReferenceNumber().ValidateCheckDigit(referenceNumber))
                    _referenceNumber = referenceNumber.Trim().Replace(" ", "");
                else
                    throw new Exception("Reference number check digit is invalid!");
            }
            else if (new InternationalReferenceNumber().Validate(referenceNumber))
            {
                _mode = "5";
                if (new InternationalReferenceNumber().ValidateCheckDigit(referenceNumber))
                    _referenceNumber = referenceNumber.Trim().Replace(" ", "");
                else
                    throw new Exception("Int. reference numbers is invalid!");
            }
            else
                throw new Exception("Invalid reference number dummy!");

            if (!DateTime.TryParse(dueDate, out _dueDate))
                throw new Exception("Invalid date!");

            _virtualBarcode = BuildVirtualBarcode();
        }

        private string BuildVirtualBarcode()
        {
            string myref = _referenceNumber;
            if (_mode.Equals("4"))
            {
                while (myref.Length < 20)
                {
                    myref = myref.Insert(0, "0");
                }
                myref = myref.Insert(0, "000");
            }
            if (_mode.Equals("5"))
            {
                myref = myref.Replace("RF", "");
                while (myref.Length < 23)
                {
                    myref = myref.Insert(2, "0");
                }
            }

            string sumOfBill = _sumOfBill.ToString().Replace(",", "");
            while (sumOfBill.Length < 8)
            {
                sumOfBill = sumOfBill.Insert(0, "0");
            }

            string outputVirtualBarcode = String.Format(
                "{0}{1}{2}{3}{4}",
                _mode,
                _bankAccountNumber.Replace("FI", ""),
                sumOfBill,
                myref,
                _dueDate.ToString("yyMMdd")
            );

            BigInteger checkSum = BigInteger.Parse(_startcodeC);

            string[] valuePairs = new string[27];
            var valuePairStartIndex = 0;
            for (int i = 0; i < 27; i++)
            {
                valuePairs[i] = outputVirtualBarcode.Substring(valuePairStartIndex, 2);
                valuePairStartIndex += 2;
            }

            for (int i = 1; i <= 27; i++)
            {
                int valuePair = int.Parse(valuePairs[i - 1]);
                BigInteger sum = valuePair * i;
                checkSum += sum;
            }

            BigInteger checkDigit2 = checkSum % 103;

            outputVirtualBarcode = String.Format(
                "[{0}]{1}{2}{3}{4}{5}{6}{7}",
                _startcodeC.ToString(),
                _mode,
                _bankAccountNumber.Replace("FI", ""),
                sumOfBill,
                myref,
                _dueDate.ToString("yyMMdd"),
                checkDigit2,
                "[STOP]"
            );

            return outputVirtualBarcode;
        }

        public override string ToString()
        {
            return _virtualBarcode;
        }
    }
}
