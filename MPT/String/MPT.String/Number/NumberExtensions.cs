using System;
using System.Linq;

namespace MPT.String.Number
{
    /// <summary>
    /// Extension methods to strings and classes relating to strings and numbers.
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// Returns only the numeric digits in the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="keepSign">True: Sign of the number will be kept in the string.
        /// False: The number will not include any negative sign when the number is negative.</param>
        /// <param name="keepDecimal">True: The number will retain any decimal, which is to be assumed to be the first occurrence of a period leading or surrounded by digits.
        /// False: Decimals will be stripped.</param>
        /// <returns>string.</returns>
        public static string GetNumbers(this string value, 
            bool keepSign = true, 
            bool keepDecimal = true)
        {
            int indexFirstDigit = value.IndexOfAny("0123456789".ToCharArray());
            int indexDecimal = value.IndexOf('.');

            string output = new string(value.Where(char.IsDigit).ToArray());
            
            if (keepDecimal)
            {

                if (indexDecimal == 0) // .345
                {
                    output = "." + output;
                }
                if (indexDecimal > 0 &&
                     (value[indexDecimal - 1] == ' ' || char.IsDigit(value[indexDecimal - 1])) &&     // Preceded by a number
                     ((indexDecimal + 1) < (value.Length) && char.IsDigit(value[indexDecimal + 1])))  // Proceeded by a number
                {
                    output = output.Insert(indexDecimal - indexFirstDigit, ".");
                }
            }

            if (!keepSign) return output;
            if (indexFirstDigit > 0 && value[indexFirstDigit - 1] == '-')
            {
                output = "-" + output;
            }

            return output;
        }

        /// <summary>
        /// Converts the string representation of a number to an integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="default">Value returned if the conversion fails.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt(this string value,
            int @default = 0)
        {
            int convertedValue;
            bool isNumeric = int.TryParse(value, out convertedValue);

            return isNumeric ? convertedValue : @default;
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="default">Value returned if the conversion fails.</param>
        /// <returns>System.Double.</returns>
        public static double ToDouble(this string value,
            double @default = 0)
        {
            double convertedValue;
            bool isNumeric = double.TryParse(value, out convertedValue);

            return isNumeric ? convertedValue : @default;
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="default">Value returned if the conversion fails.</param>
        /// <returns>System.Decimal.</returns>
        public static decimal ToDecimal(this string value,
            decimal @default = 0)
        {
            decimal convertedValue;
            bool isNumeric = decimal.TryParse(value, out convertedValue);

            return isNumeric ? convertedValue : @default;
        }

        // TODO: ToNumericExpression  (See C++ exercise for int)

        // TODO: FromNumericExpression  (See C++ exercise for int)
    }
}