using System.Text;
using MPT.String.Character;

namespace MPT.String.Database
{
    /// <summary>
    /// Extension methods to strings and classes relating to strings for common database-oriented adjustments.
    /// </summary>
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Adjusts value to only contain valid characters for MySQl.
        /// For example, single quotes are doubled.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToMySqlValue(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            if (!value.Contains("'")) return value;
            
            StringBuilder resultBuilder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if ((value[i] == '\'') &&
                    (((i < value.Length - 1) && value[i + 1] != '\'') ||
                      (i == value.Length - 1)))
                {
                    resultBuilder.Append("''");
                }
                else
                {
                    resultBuilder.Append(value[i]);
                }
            }
            return resultBuilder.ToString();
        }

        /// <summary>
        /// Adjusts value to remove adjustments for invalid characters to be made valid for MySQL.
        /// For example, double single quotes '' are returned back to single quotes '.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string FromMySqlValue(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            if (!value.Contains("'")) return value;

            StringBuilder resultBuilder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if ((value[i] == '\'') &&
                    ((i < value.Length - 1) && value[i + 1] == '\''))
                {
                    continue;
                }
                resultBuilder.Append(value[i]);
            }
            return resultBuilder.ToString(); 
        }



        /// <summary>
        /// Adjusts table or header name to be compatible for MySQL, such as using ` `.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToMySqlTableOrHeaderName(this string value)
        {
            return value.JoinPrePost("`", "`");
        }

        /// <summary>
        /// Adjusts table or header name to remove elements added to ensure compatibility for MySQL, such as using ` `.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string FromMySqlTableOrHeaderName(this string value)
        {
            return value.TrimPrePost("`", "`");
        }



        /// <summary>
        /// Adjusts table name to be compatible with an SQL-based database system, such as using [ ].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToSqlTableName(this string value)
        {
            return value.JoinPrePost("[", "]");
        }

        /// <summary>
        /// Adjusts table name to remove elements compatible with an SQL-based database system, such as using [ ].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string FromSqlTableName(this string value)
        {
            return value.TrimPrePost("[", "]");
        }
    }
}
