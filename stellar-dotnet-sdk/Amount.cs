using System;
using System.Globalization;

namespace stellar_dotnet_sdk
{
    public static class Amount
    {
        private static readonly decimal ONE = new decimal(10000000);


        public static string DecimalToString(decimal d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static string FromXdr(long value)
        {
            var amount = decimal.Divide(new decimal(value), ONE);
            return DecimalToString(amount);
        }

        public static long ToXdr(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value), "value cannot be null");

            //This basically takes a decimal value and turns it into a large integer.
            var amount = decimal.Parse(value, CultureInfo.InvariantCulture) * ONE;

            //MJM: Added to satisfy the OperationTest unit test of making sure a failure
            //happens when casting a decimal with fractional places into a long.
            if (amount % 1 > 0)
                throw new ArithmeticException("Unable to cast decimal with fractional places into long.");

            return (long)amount;
        }
    }
}