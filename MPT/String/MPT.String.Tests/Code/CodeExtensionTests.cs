using NUnit.Framework;
using MPT.String.Code;

namespace MPT.String.Tests.Code
{
    [TestFixture]
    public static class CodeExtensions
    {

        [TestCase("to pascal case", ExpectedResult = "ToPascalCase")]
        [TestCase("To Pascal Case", ExpectedResult = "ToPascalCase")]
        [TestCase("To Pascal 2 Case", ExpectedResult = "ToPascal2Case")]
        [TestCase("To Pascal T2 Case", ExpectedResult = "ToPascalT2Case")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public static string ToPascalCase(string value)
        {
            return value.ToPascalCase();
        }

        [TestCase("ToPascalCase", ExpectedResult = "To Pascal Case")]
        [TestCase("ToPascal2Case", ExpectedResult = "To Pascal 2 Case")]
        [TestCase("ToPascalT2Case", ExpectedResult = "To Pascal T2 Case")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public static string FromPascalCase(string value)
        {
            return value.FromPascalCase();
        }

        [TestCase("to camel case", ExpectedResult = "toCamelCase")]
        [TestCase("To Camel Case", ExpectedResult = "toCamelCase")]
        [TestCase("To Camel 2 Case", ExpectedResult = "toCamel2Case")]
        [TestCase("To Camel T2 Case", ExpectedResult = "toCamelT2Case")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public static string ToCamelCase(string value)
        {
            return value.ToCamelCase();
        }
        
        [TestCase("toCamelCase", ExpectedResult = "to camel case")]
        [TestCase("toCamel2Case", ExpectedResult = "to camel 2 case")]
        [TestCase("toCamelT2Case", ExpectedResult = "to camel t2 case")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public static string FromCamelCase(string value)
        {
            return value.FromCamelCase();
        }

        [TestCase("to snake case", ExpectedResult = "to_snake_case")]
        [TestCase("To Snake Case", ExpectedResult = "To_Snake_Case")]
        [TestCase(" To Snake Case", ExpectedResult = "To_Snake_Case")]
        [TestCase("To  Snake  Case", ExpectedResult = "To_Snake_Case")]
        [TestCase("To Snake 2 Case", ExpectedResult = "To_Snake_2_Case")]
        [TestCase("To Snake T2 Case", ExpectedResult = "To_Snake_T2_Case")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public static string ToSnakeCase(string value)
        {
            return value.ToSnakeCase();
        }

        [TestCase("to_Snake_Case", ExpectedResult = "to Snake Case")]
        [TestCase("to_snake_case", ExpectedResult = "to snake case")]
        [TestCase("To_Snake_Case", ExpectedResult = "To Snake Case")]
        [TestCase(" To_Snake_Case", ExpectedResult = "To Snake Case")]
        [TestCase("To_Snake_2_Case", ExpectedResult = "To Snake 2 Case")]
        [TestCase("to_Snake_T2_Case", ExpectedResult = "to Snake T2 Case")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public static string FromSnakeCase(string value)
        {
            return value.FromSnakeCase();
        }



        [TestCase("to kebab case", ExpectedResult = "to-kebab-case")]
        [TestCase("To Kebab Case", ExpectedResult = "to-kebab-case")]
        [TestCase(" To Kebab Case", ExpectedResult = "to-kebab-case")]
        [TestCase("To  Kebab  Case", ExpectedResult = "to-kebab-case")]
        [TestCase("To Kebab 2 Case", ExpectedResult = "to-kebab-2-case")]
        [TestCase("To Kebab T2 Case", ExpectedResult = "to-kebab-t2-case")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public static string ToKebabCase(string value)
        {
            return value.ToKebabCase();
        }

        [TestCase("to-kebab-case", ExpectedResult = "to kebab case")]
        [TestCase("to-kebab-2-case", ExpectedResult = "to kebab 2 case")]
        [TestCase("to-kebab-T2-case", ExpectedResult = "to kebab t2 case")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public static string FromKebabCase(string value)
        {
            return value.FromKebabCase();
        }


        [TestCase("to train case", ExpectedResult = "To-Train-Case")]
        [TestCase("To Train Case", ExpectedResult = "To-Train-Case")]
        [TestCase(" To Train Case", ExpectedResult = "To-Train-Case")]
        [TestCase("To  Train  Case", ExpectedResult = "To-Train-Case")]
        [TestCase("To Train 2 Case", ExpectedResult = "To-Train-2-Case")]
        [TestCase("To Train T2 Case", ExpectedResult = "To-Train-T2-Case")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public static string ToTrainCase(string value)
        {
            return value.ToTrainCase();
        }

        [TestCase("To-Train-Case", ExpectedResult = "To Train Case")]
        [TestCase("To-Train-2-Case", ExpectedResult = "To Train 2 Case")]
        [TestCase("To-Train-T2-Case", ExpectedResult = "To Train T2 Case")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public static string FromTrainCase(string value)
        {
            return value.FromTrainCase();
        }
    }
}
