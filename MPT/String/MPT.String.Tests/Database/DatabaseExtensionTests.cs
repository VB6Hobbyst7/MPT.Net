using NUnit.Framework;
using MPT.String.Database;

namespace MPT.String.Tests.Database
{
    [TestFixture]
    public class DatabaseExtensionTests
    {
        [TestCase("Foobar's", ExpectedResult = "Foobar''s")]
        [TestCase("Foobars'", ExpectedResult = "Foobars''")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ToMySqlValue(string value)
        {
            return value.ToMySqlValue();
        }

        [TestCase("Foobar''s", ExpectedResult = "Foobar's")]
        [TestCase("Foobars''", ExpectedResult = "Foobars'")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string FromMySqlValue(string value)
        {
            return value.FromMySqlValue();
        }

        [TestCase("Table Name", ExpectedResult = "`Table Name`")]
        [TestCase("`Table Name`", ExpectedResult = "`Table Name`")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ToMySqlTableOrHeaderName(string tableName)
        {
            return tableName.ToMySqlTableOrHeaderName();
        }

        [TestCase("Table Name", ExpectedResult = "Table Name")]
        [TestCase("`Table Name`", ExpectedResult = "Table Name")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string FromMySqlTableOrHeaderName(string tableName)
        {
            return tableName.FromMySqlTableOrHeaderName();
        }


        [TestCase("Table Name", ExpectedResult = "[Table Name]")]
        [TestCase("[Table Name]", ExpectedResult = "[Table Name]")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ToSqlTableName(string tableName)
        {
            return tableName.ToSqlTableName();
        }

        [TestCase("Table Name", ExpectedResult = "Table Name")]
        [TestCase("[Table Name]", ExpectedResult = "Table Name")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string FromSqlTableName(string tableName)
        {
            return tableName.FromSqlTableName();
        }
    }
}
