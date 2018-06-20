using System.Globalization;
using System.Text;
using MPT.String.Character;

namespace MPT.String.Code
{
    /// <summary>
    /// Extension methods to strings and classes relating to strings and conversion to different coding formats.
    /// </summary>
    public static class CodeExtensions
    {

        /// <summary>
        /// Converts the string to pascal case, e.g. 'to pascal case' = 'ToPascalCase'.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToPascalCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            StringBuilder resultBuilder = new StringBuilder();

            foreach (char c in value)
            {
                // Replace anything, but letters and digits, with space
                if (!char.IsLetterOrDigit(c))
                {
                    resultBuilder.Append(" ");
                }
                else
                {
                    resultBuilder.Append(c);
                }
            }

            string result = resultBuilder.ToString();

            // Make result string all lowercase, because ToTitleCase does not change all uppercase correctly
            result = result.ToLower();

            // Creates a TextInfo based on the "en-US" culture.
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            result = textInfo.ToTitleCase(result).Replace(" ", string.Empty);
            return result;
        }

        /// <summary>
        /// Converts the string from pascal case to a multi-worded string, e.g. 'FromPascalCase' = 'From Pascal Case'.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string FromPascalCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            StringBuilder resultBuilder = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                // Insert a space anywhere that capitalization changes or a number is encountered after a letter
                if ((char.IsUpper(value[i]) || 
                    (char.IsNumber(value[i]) && !char.IsUpper(value[i - 1]))) &&
                     resultBuilder.Length > 0)
                {
                    resultBuilder.Append(" ");
                    resultBuilder.Append(value[i]);
                }
                else
                {
                    resultBuilder.Append(value[i]);
                }
            }
            return resultBuilder.ToString(); 
        }


        /// <summary>
        /// Converts the string to camel case, e.g. 'to camel case' = 'toCamelCase'.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToCamelCase(this string value)
        {
            value = value.ToPascalCase();
            value = value.ToLowerFirst();
            return value;
        }


        /// <summary>
        /// Converts the string from camel case to a multi-worded string, e.g. 'fromCamelCase' = 'from camel case'.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string FromCamelCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            value = value.FromPascalCase();
            value = value.ToLower();

            return value;
        }


        /// <summary>
        /// Converts the string to snake case, e.g. 'to snake case' = 'to_snake_case'.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToSnakeCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            StringBuilder resultBuilder = new StringBuilder();
            string lastJoiner = "";
            value = value.Trim();

            foreach (char t in value)
            {
                // Replace anything, but letters and digits, with a single underscore
                if (!char.IsLetterOrDigit(t) && string.IsNullOrEmpty(lastJoiner))
                {
                    lastJoiner = "_";
                    resultBuilder.Append(lastJoiner);
                }
                else if (char.IsLetterOrDigit(t))
                {
                    lastJoiner = "";
                    resultBuilder.Append(t);
                }
            }
            string result = resultBuilder.ToString();
            return result;
        }

        /// <summary>
        /// Converts the string from snake case to a multi-worded string, e.g. 'from_Snake_case' = 'from Snake case'.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string FromSnakeCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            StringBuilder resultBuilder = new StringBuilder();
            value = value.Trim();

            foreach (char t in value)
            {
            // Insert a space anywhere that an underscore is encountered (except the first character)
                if (resultBuilder.Length > 0 &&
                    t == '_')
                {
                    resultBuilder.Append(" ");
                }
                else if (t != '_')
                {
                    resultBuilder.Append(t);
                }
            }
            return resultBuilder.ToString();
        }
        

        /// <summary>
        /// Converts the string to train case, e.g. 'to train case' = 'To-Train-Case'.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToTrainCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            StringBuilder resultBuilder = new StringBuilder();
            string lastJoiner = "";
            value = value.Trim();

            foreach (char t in value)
            {
                // Replace anything, but letters and digits, with a single underscore
                if (!char.IsLetterOrDigit(t) && string.IsNullOrEmpty(lastJoiner))
                {
                    lastJoiner = "-";
                    resultBuilder.Append(lastJoiner);
                }
                else if (char.IsLetterOrDigit(t))
                {
                    lastJoiner = "";
                    resultBuilder.Append(t);
                }
            }
            string result = resultBuilder.ToString();

            // Make result string all lowercase, because ToTitleCase does not change all uppercase correctly
            result = result.ToLower();

            // Creates a TextInfo based on the "en-US" culture.
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            result = textInfo.ToTitleCase(result).Replace(" ", string.Empty);
            return result;
        }

        /// <summary>
        /// Converts the string from train case to a multi-worded string, e.g. 'From-Train-Case' = 'From Train Case'.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string FromTrainCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            StringBuilder resultBuilder = new StringBuilder();
            value = value.Trim();

            foreach (char t in value)
            {
                // Insert a space anywhere that a dash is encountered (except the first character)
                if (resultBuilder.Length > 0 &&
                    t == '-')
                {
                    resultBuilder.Append(" ");
                }
                else if (t != '-')
                {
                    resultBuilder.Append(t);
                }
            }
            return resultBuilder.ToString();
        }


        /// <summary>
        /// Converts the string to kebab case, e.g. 'to kebab case' = 'to-kebab-case'.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToKebabCase(this string value)
        {
            value = value.ToTrainCase();
            value = value.ToLower();
            return value;
        }

        /// <summary>
        /// Converts the string from kebab case to a multi-worded string, e.g. 'from-kebab-case' = 'from kebab case'.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string FromKebabCase(this string value)
        {
            value = value.FromTrainCase();
            value = value.ToLower();
            return value;
        }
    }
}
