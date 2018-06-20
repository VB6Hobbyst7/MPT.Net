using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MPT.String.Character
{
    /// <summary>
    /// Extension methods to strings and classes relating to strings manipulating characters.
    /// </summary>
    public static class CharacterExtensions
    {
        /// <summary>
        /// Determines whether the specified word contains vowels.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns><c>true</c> if the specified word contains vowels; otherwise, <c>false</c>.</returns>
        public static bool ContainsVowels(this string word)
        {
            return !string.IsNullOrWhiteSpace(word) && word.Any(StringLibrary.IsVowel);
        }

        /// <summary>
        /// Changes the first character in the string to being lower case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToLowerFirst(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            char[] values = value.ToCharArray();
            values[0] = char.ToLower(values[0]);

            return new string(values);
        }

        /// <summary>
        /// Changes the first character in the string to being upper case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToUpperFirst(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            char[] values = value.ToCharArray();
            values[0] = char.ToUpper(values[0]);

            return new string(values);
        }

        /// <summary>
        /// Merges the specified array separated by the specified demarcator.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="demarcator">The demarcator to separate the joined entries by.</param>
        /// <returns>string.</returns>
        public static string Merge(this string[] values, string demarcator = "")
        {
            if (values == null) return string.Empty;

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                stringBuilder.Append(values[i]);
                if (values.Length > 1 && i != values.Length - 1)
                {
                    stringBuilder.Append(demarcator);
                }
            }
            return stringBuilder.ToString().Trim();
        }

        /// <summary>
        /// Splits the specified string by the specified string demarcator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="demarcator">The string demarcator that splits the string.</param>
        /// <returns>string[].</returns>
        public static string[] Split(this string value, string demarcator)
        {
            List<string> values = new List<string>();
            if (string.IsNullOrWhiteSpace(value)) return values.ToArray();
            if (string.IsNullOrWhiteSpace(demarcator) ||
                value.Length <= demarcator.Length)
            {
                values.Add(value);
                return values.ToArray();
            }
            
            StringBuilder stringBuilder = new StringBuilder();
            int indexSkip = -1;
            for (int i = 0; i < value.Length; i++)
            {
                if (i <= indexSkip) continue;

                if (demarcator.Length <= value.Length - i &&
                    value[i] == demarcator[0] &&
                    value.Substring(i, demarcator.Length) == demarcator)
                {
                    if (stringBuilder.Length > 0)
                    {
                        values.Add(stringBuilder.ToString());
                        stringBuilder.Clear();
                    }
                    indexSkip = i + demarcator.Length - 1;
                }
                else
                {
                    stringBuilder.Append(value[i]);
                }
            }
            if (stringBuilder.Length > 0) values.Add(stringBuilder.ToString());

            return values.ToArray();
        }

        /// <summary>
        /// Converts a string to the specified capitalization pattern.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pattern">The pattern to capitalize by.</param>
        /// <returns>string.</returns>
        public static string Capitalize(this string value,
            eCapitalization pattern)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            switch (pattern)
            {
                case eCapitalization.alllower:
                    return value.ToLower();
                case eCapitalization.ALLCAPS:
                    return value.ToUpper();
                case eCapitalization.Firstupper:
                    return value[0].ToString().ToUpper() + value.Substring(1, value.Length - 1).ToLower();
                default:
                    return value.ToUpper();
            }
        }
        
        /// <summary>
        /// Joins the specified characters before and after the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pre">The character to prepend to the beginning.</param>
        /// <param name="post">The character to append to the end.</param>
        /// <returns>string.</returns>
        public static string JoinPrePost(this string value, 
            string pre = "", 
            string post = "")
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            if (!string.IsNullOrEmpty(pre) &&
                value.Substring(0, pre.Length) != pre)
            {
                value = pre + value;
            }
            if (!string.IsNullOrEmpty(post) &&
                value.Substring(value.Length - post.Length, post.Length) != post)
            {
                value += post;
            }
            return value;
        }

        /// <summary>
        /// Trim the specified characters before and after the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pre">The character to trim at the beginning.</param>
        /// <param name="post">The character to trim at the end.</param>
        /// <returns>string.</returns>
        public static string TrimPrePost(this string value,
            string pre = "",
            string post = "")
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            if (!string.IsNullOrEmpty(pre) &&
                value.Substring(0, pre.Length) == pre)
            {
                value = value.Substring(pre.Length, value.Length - pre.Length);
            }
            if (!string.IsNullOrEmpty(post) &&
                value.Substring(value.Length - post.Length, post.Length) == post)
            {
                value = value.Substring(0, value.Length - post.Length);
            }
            return value;
        }


        /// <summary>
        /// Generically sets the word to its likely singular equivalent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="requireVowels">if set to <c>true</c> then the string must have vowels to be made singular.</param>
        /// <returns>string.</returns>
        public static string ToSingular(this string value,
            bool requireVowels = true)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            // Only letters are supported
            if (!Regex.IsMatch(value, @"^[\p{L}]+$")) return value;

            // Check for ending in 's' for words long enough to have a plural
            if (value.Length < 4 || 
                value[value.Length - 1] != 's') return value;

            // Check if word contains vowels. All consonants implies an abbreviation.
            if (requireVowels && !value.ContainsVowels()) return value;

            int charactersToRemove = 1;

            // Check for 'es' vs. 'ch'-'es' case
            if (value.Length > 5 &&
                value[value.Length - 2] == 'e' &&
                value[value.Length - 3] == 'h' &&
                value[value.Length - 4] == 'c')
            {
                charactersToRemove = 2;
            }
            
            return value.Remove(value.Length - charactersToRemove);
        }

        /// <summary>
        /// Generically sets the word to its likely plural equivalent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToPlural(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            // Only letters are supported
            if (!Regex.IsMatch(value, @"^[\p{L}]+$")) return value;

            // Check for ending in 's' for words long enough to have a plural
            if (value.Length < 3 ||
                value[value.Length - 1] == 's') return value;
            
            // Check for 'es' vs. 'ch'-'es' case
            if (value[value.Length - 1] == 'h' &&
                value[value.Length - 2] == 'c')
            {
                return value + "es";
            }
            return value + 's';
        }

        /// <summary>
        /// Makes a string singular or plural based on the number provided, which will be prepended. e.g. 2 items, 1 item, etc.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="number">The number.</param>
        /// <returns>string.</returns>
        public static string ToPlural(this string value, 
            int number)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            // Only letters are supported
            if (!Regex.IsMatch(value, @"^[\p{L}]+$")) return value;

            if (number == 1) return number.ToString() + ' ' + value;
            return number.ToString() + ' ' + value.ToPlural();
        }

        /// <summary>
        /// Makes a string singular or plural based on the number provided, which will be prepended. e.g. 2 items, 1 item, etc.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="number">The number.</param>
        /// <param name="tolerance">The tolerance by which a number is considered to be treated as '1'.</param>
        /// <returns>string.</returns>
        public static string ToPlural(this string value, 
            double number, 
            double tolerance = 1E-3)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            // Only letters are supported
            if (!Regex.IsMatch(value, @"^[\p{L}]+$")) return value;
            if (tolerance < 0) tolerance = 1E-3;
            if (Math.Abs(number - 1) < tolerance) 
            {
                return "1 " + value;
            }
            return number.ToString(CultureInfo.InvariantCulture) + ' ' + value.ToPlural();
        }

        /// <summary>
        /// Makes a string singular or plural based on the number provided, which will be prepended. e.g. 2 items, 1 item, etc.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="number">The number.</param>
        /// <returns>string.</returns>
        public static string ToPlural(this string value, 
            decimal number)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            // Numbers not supported
            if (!Regex.IsMatch(value, @"^[\p{L}]+$")) return value;

            if (number == 1) return number.ToString(CultureInfo.InvariantCulture) + ' ' + value;
            return number.ToString(CultureInfo.InvariantCulture) + ' ' + value.ToPlural();
        }

        /// <summary>
        /// Generically sets the word to its likely possessive equivalent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToPossessive(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            if (value.ToUpper() == "IT") return value + 's';

            if (value.Length < 3) return value;

            // Only letters are supported
            if (!Regex.IsMatch(value, @"^[\p{L}]+$")) return value;
            
            // Check for ending in 's' for words long enough to have a plural
            if (value[value.Length - 1] == 's') return value + '\'';

            return value + "\'s";
        }


        /// <summary>
        /// Generically sets the word from its likely possessive form to a non-possessive equivalent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string FromPossessive(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            if (value.ToUpper() == "ITS") return value.Remove(value.Length - 1);

            if (value.Length < 4) return value;

            // Only letters are supported
            if (!Regex.IsMatch(value, @"^[\p{L}']+$")) return value;

            int numberCharsToRemove = 0;

            // Check 's case
            if (value[value.Length - 2] == '\'' &&
                value[value.Length - 1] == 's')
            {
                numberCharsToRemove = 2;
            }

            // Check s' case
            if (value[value.Length - 2] == 's' &&
               value[value.Length - 1] == '\'')
            {
                numberCharsToRemove = 2;
            }

            return numberCharsToRemove == 0 ? value : value.Remove(value.Length - numberCharsToRemove);
        }
    }
}
