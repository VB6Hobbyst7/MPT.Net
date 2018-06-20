using System;
using System.Collections.Generic;
using NUnit.Framework;
using MPT.String.Word;

namespace MPT.String.Tests.Word
{
    [TestFixture]
    public class WordExtensionTests
    {
        [Test]
        public void ApplyToIndividualWords_Delegate_With_Single_Parameter()
        {
            string originalPhrase = "Foo sells bar down by the bar foo";
            string modifiedPhrase = originalPhrase.ApplyToIndividualWords(ToUpper);
            Assert.That(modifiedPhrase, Is.EqualTo("FOO SELLS BAR DOWN BY THE BAR FOO"));
        }

        private static string ToUpper(string word)
        {
            return word.ToUpper();
        }

        [Test]
        public void ApplyToIndividualWords_Delegate_With_Single_Parameter_Clears_With_No_Additional_Spaces()
        {
            string originalPhrase = "Foo sells bar down by the bar foo";
            string modifiedPhrase = originalPhrase.ApplyToIndividualWords(Remove_If_3_Long);
            Assert.That(modifiedPhrase, Is.EqualTo("sells down by"));
        }

        private static string Remove_If_3_Long(string word)
        {
            return word.Length == 3 ? string.Empty : word;
        }

        [Test]
        public void ApplyToIndividualWords_Delegate_With_Two_Parameters()
        {
            string originalPhrase = "Foo sells bar down by the bar foo";
            string modifiedPhrase = originalPhrase.ApplyToIndividualWords(ReplaceWithBar, "down");
            Assert.That(modifiedPhrase, Is.EqualTo("Foo sells bar Bar by the bar foo"));
        }

        private static string ReplaceWithBar(string word, string withWord)
        {
            return word == withWord ? "Bar" : word;
        }


        [Test]
        public void ApplyToIndividualWords_Delegate_With_Two_Parameters_Clears_With_No_Additional_Spaces()
        {
            string originalPhrase = "Foo sells bar down by the bar foo";
            string modifiedPhrase = originalPhrase.ApplyToIndividualWords(ReplaceWithEmpty, "down");
            Assert.That(modifiedPhrase, Is.EqualTo("Foo sells bar by the bar foo"));
        }

        private static string ReplaceWithEmpty(string word, string withWord)
        {
            return word == withWord ? string.Empty : word;
        }


        [Test]
        public void ApplyToIndividualWords_Delegate_With_Two_Parameters_And_List()
        {
            string originalPhrase = "Foo sells bar down by the bar foo";
            List<string> replacementWords = 
                new List<string>()
                    { "bar", "nar", "the"};
            string modifiedPhrase = originalPhrase.ApplyToIndividualWords(ReplaceIfInList, replacementWords, "Bar");

            Assert.That(modifiedPhrase, Is.EqualTo("Foo sells Bar down by Bar Bar foo"));
        }

        [Test]
        public void ApplyToIndividualWords_Delegate_With_Two_Parameters_And_List_Clears_With_No_Additional_Spaces()
        {
            string originalPhrase = "Foo sells bar down by the bar foo";
            List<string> replacementWords =
                new List<string>()
                    { "bar", "nar", "the"};
            string modifiedPhrase = originalPhrase.ApplyToIndividualWords(ReplaceIfInList, replacementWords, string.Empty);

            Assert.That(modifiedPhrase, Is.EqualTo("Foo sells down by foo"));
        }

        private static string ReplaceIfInList(string word, List<string> words, string withWord)
        {
            foreach (string replacementWord in words)
            {
                if (word == replacementWord) return withWord;
            }
            return word;
        }

        [Test]
        public void ApplyToIndividualWords_Delegate_With_Two_Parameters_And_Dictionary()
        {
            string originalPhrase = "Foo sells bar down by the bar foo";
            Dictionary<string, string> replacementWords =
                new Dictionary<string, string>()
                    { {"bar", "foo"},
                      {"nar", "mar"},
                      {"the", "Bar"} };

            string modifiedPhrase = originalPhrase.ApplyToIndividualWords(ReplaceIfKeyInMap, replacementWords);
            Assert.That(modifiedPhrase, Is.EqualTo("Foo sells foo down by Bar foo foo"));
        }

        [Test]
        public void ApplyToIndividualWords_Delegate_With_Two_Parameters_And_Dictionary_Clears_With_No_Additional_Spaces()
        {
            string originalPhrase = "Foo sells bar down by the bar foo";
            Dictionary<string, string> replacementWords =
                new Dictionary<string, string>()
                    { {"bar", "foo"},
                      {"nar", "mar"},
                      {"the", ""} };

            string modifiedPhrase = originalPhrase.ApplyToIndividualWords(ReplaceIfKeyInMap, replacementWords);
            Assert.That(modifiedPhrase, Is.EqualTo("Foo sells foo down by foo foo"));
        }

        private static string ReplaceIfKeyInMap(string word, Dictionary<string, string> words)
        {
            foreach (string replacementWordKey in words.Keys)
            {
                if (word == replacementWordKey) return words[replacementWordKey];
            }
            return word;
        }
    }
}
