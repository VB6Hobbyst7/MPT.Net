using MPT.String.Character;

namespace MPT.String.Boolean
{
    /// <summary>
    /// Extension methods to strings and classes relating to strings and booleans.
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// Converts the true/false/{unknown} string to the equivalent boolean, or null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if value is some form of 'true', <c>false</c> if value is some form of 'false', <c>null</c> otherwise.</returns>
        public static bool? BoolerizeNullable(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            string valueCheck = value.ToUpper();
            switch (valueCheck)
            {
                case "TRUE":
                    return true;
                case "FALSE":
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Converts the true/false string to the equivalent boolean.
        /// Other values are also returned as <c>false</c>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if value is some form of 'true', <c>false</c> if value is some form of 'false' or any other string.</returns>
        public static bool Boolerize(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            string valueCheck = value.ToUpper();
            switch (valueCheck)
            {
                case "TRUE":
                    return true;
                case "FALSE":
                    return false;
                default:
                    return false;
            }
        }




        /// <summary>
        /// Converts the yes/no/{unknown} string to the equivalent boolean, or null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if value is some form of 'true', <c>false</c> if value is some form of 'false', <c>null</c> otherwise.</returns>
        public static bool? BoolerizeYesNoNullable(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            string valueCheck = value.ToUpper();
            switch (valueCheck)
            {
                case "YES":
                    return BoolerizeYesNo(valueCheck);
                case "NO":
                    return BoolerizeYesNo(valueCheck);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Converts the yes/no string to the equivalent boolean.
        /// Other values are also returned as <c>false</c>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if value is some form of 'true', <c>false</c> if value is some form of 'false' or any other string.</returns>
        public static bool BoolerizeYesNo(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            string valueCheck = value.ToUpper();
            switch (valueCheck)
            {
                case "YES":
                    return Boolerize("TRUE");
                case "NO":
                    return Boolerize("FALSE");
                default:
                    return false;
            }
        }




        /// <summary>
        /// Converts the boolean to an equivalent string expression.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="forNull">Value to return if null.</param>
        /// <param name="pattern">The pattern of capitalization to use.</param>
        /// <returns>string.</returns>
        public static string Stringify(this bool? value,
            string forNull = "null",
            eCapitalization pattern = eCapitalization.alllower)
        {
            return value == null ? forNull.Capitalize(pattern) : StringifyYesNo((bool)value);
        }

        /// <summary>
        /// Converts the boolean to an equivalent string expression.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="pattern">The pattern of capitalization to use.</param>
        /// <returns>string.</returns>
        public static string Stringify(this bool value,
            eCapitalization pattern = eCapitalization.alllower)
        {
            string stringified = value ? "true" : "false";
            return stringified.Capitalize(pattern);
        }


        /// <summary>
        /// Converts the nullable boolean to an equivalent yes/no/{unknown} string expression.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="forNull">Value to return if null.</param>
        /// <param name="pattern">The pattern of capitalization to use.</param>
        /// <returns>string.</returns>
        public static string StringifyYesNo(this bool? value,
            string forNull = "null",
            eCapitalization pattern = eCapitalization.alllower)
        {
            return value == null ? forNull.Capitalize(pattern) : StringifyYesNo((bool)value);
        }

        /// <summary>
        /// Converts the boolean to an equivalent yes/no string expression.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="pattern">The pattern of capitalization to use.</param>
        /// <returns>string.</returns>
        public static string StringifyYesNo(this bool value,
                eCapitalization pattern = eCapitalization.alllower)
        {
            string stringified = value ? "yes" : "no";
            return stringified.Capitalize(pattern);
        }
    }
}
