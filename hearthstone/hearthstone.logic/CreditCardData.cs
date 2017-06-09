using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hearthstone.logic
{
    public class CreditCardData
    {
        public static bool IsValidNumber(string data)
        {
            int sum = 0;
            int len = data.Length;
            for (int i = 0; i < len; i++)
            {
                int add = (data[i] - '0') * (2 - (i + len) % 2);
                add -= add > 9 ? 9 : 0;
                sum += add;
            }
            return sum % 10 == 0;
        }

        public static bool IsValidExpiration(int month, int year)
        {
            bool isValidExpiration = true;

            if (year < DateTime.Now.Year)
                isValidExpiration = false;
            else if (year == DateTime.Now.Year && month < DateTime.Now.Month)
                isValidExpiration = false;

            return isValidExpiration;
        }

        public string CreditCardNumber { get; private set; }
        public string CardHolder { get; private set; }
        public int ExpireMonth { get; private set; }
        public int ExpireYear { get; private set; }
        public int SecurityCode { get; private set; }

        private CreditCardData(string creditCardNumber, string cardHolder, int expireMonth, int expireYear, int securityCode)
        {
            CreditCardNumber = creditCardNumber;
            CardHolder = cardHolder;
            ExpireMonth = expireMonth;
            ExpireYear = expireYear;
            SecurityCode = securityCode;
        }

        // Factory!!
        public static CreditCardData Create(string creditCardNumber, string cardHolder, int expireMonth, int expireYear, int securityCode)
        {
            CreditCardData cc = null;

            if (IsValidNumber(creditCardNumber) && IsValidExpiration(expireMonth, expireYear))
                cc = new CreditCardData(creditCardNumber, cardHolder, expireMonth, expireYear, securityCode)´;

            return cc;
        }

    }
}
