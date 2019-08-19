using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NETCore_Selenium.Domain.Classes
{
    public static class Currency
    {
        public static decimal Parse(string input)
        {
            return decimal.Parse(Regex.Match(input, @"-?\d{1,3}(,\d{3})*(\.\d+)?").Value);
        }
        public static string FormatWithComma(string input)
        {
            return String.Format("{0:n}", Double.Parse(input));
        }
        public static decimal ParseAndCheckForNegative(string input)
        {
            decimal value;
            if (input.Contains("(") && input.Contains(")"))
            {
                value = Decimal.Negate(Parse(input));
            }
            else
            {
                value = Parse(input);
            }
            return value;
        }
        public static string FormatNegativeToReportingFormat(decimal input)
        {
            return input.ToString("c");
        }
        private static readonly NumberFormatInfo CurrencyFormat = CreateCurrencyFormat();

        private static NumberFormatInfo CreateCurrencyFormat()
        {
            var usCulture = CultureInfo.CreateSpecificCulture("en-US");
            var clonedNumbers = (NumberFormatInfo)usCulture.NumberFormat.Clone();
            clonedNumbers.CurrencyNegativePattern = 2;
            return clonedNumbers;
        }

        public static string FormatCurrency(decimal value)
        {
            return value.ToString("c");
        }
    }
}