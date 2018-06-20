using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace MPT.Lists.UnitTests
{
    [TestFixture]
    public class ListLibraryTests_ListConversions
    {
        [Test]
        public void ConvertListToObservableCollection_Given_Null_Returns_Empty()
        {
            ObservableCollection<string> convertedList = ListLibrary.ConvertListToObservableCollection<string>(null);
            Assert.IsEmpty(convertedList);
        }

        [Test]
        public void ConvertListToObservableCollection_Of_Collection_Of_String_Returns_Expected()
        {
            Collection<string> listToConvert = new Collection<string>() {"Foo", "bar", "Moo", "nar"};
            ObservableCollection<string> convertedList = ListLibrary.ConvertListToObservableCollection<string>(listToConvert);

            Assert.IsTrue(convertedList.Count == listToConvert.Count);
            Assert.AreEqual(listToConvert[0], convertedList[0]);
            Assert.AreEqual(listToConvert[1], convertedList[1]);
            Assert.AreEqual(listToConvert[2], convertedList[2]);
            Assert.AreEqual(listToConvert[3], convertedList[3]);
        }

        [Test]
        public void ConvertListToObservableCollection_Of_List_Of_String_Returns_Expected()
        {
            List<string> listToConvert = new List<string>() { "Foo", "bar", "Moo", "nar" };
            ObservableCollection<string> convertedList = ListLibrary.ConvertListToObservableCollection<string>(listToConvert);

            Assert.IsTrue(convertedList.Count == listToConvert.Count);
            Assert.AreEqual(listToConvert[0], convertedList[0]);
            Assert.AreEqual(listToConvert[1], convertedList[1]);
            Assert.AreEqual(listToConvert[2], convertedList[2]);
            Assert.AreEqual(listToConvert[3], convertedList[3]);
        }

        [Test]
        public void ConvertListToObservableCollection_Of_Collection_Of_Double_Returns_Expected()
        {
            List<double> listToConvert = new List<double>() { 1, 2.2, 999, -4 };
            ObservableCollection<double> convertedList = ListLibrary.ConvertListToObservableCollection<double>(listToConvert);

            Assert.IsTrue(convertedList.Count == listToConvert.Count);
            Assert.AreEqual(listToConvert[0], convertedList[0]);
            Assert.AreEqual(listToConvert[1], convertedList[1]);
            Assert.AreEqual(listToConvert[2], convertedList[2]);
            Assert.AreEqual(listToConvert[3], convertedList[3]);
        }


        [Test]
        public void Convert_Given_Null_Returns_Empty()
        {
            List<char> convertedList = new List<char>(ListLibrary.Convert<string, char>(null));
            Assert.IsEmpty(convertedList);
        }

        [Test]
        public void Convert_Incompatible_Returns_Empty()
        {
            List<int> originalList = new List<int>() {1, 2, 3, 4, 5};
            List<char> convertedList = new List<char>(ListLibrary.Convert<int, char>(originalList));
            Assert.IsEmpty(convertedList);
        }
        
        [Test]
        public void Convert_Compatible_Returns_Converted_List()
        {
            List<ClassForConversion> listToConvert = new List<ClassForConversion>()
            {
                new ClassForConversion(),
                new ClassForConversion(),
                new ClassForConversion()
            };
            List<BaseForConversion> convertedList = new List<BaseForConversion>(ListLibrary.Convert<ClassForConversion, BaseForConversion>(listToConvert));

            Assert.IsTrue(listToConvert.Count == convertedList.Count);
        }

        [Test]
        public void Convert_Compatible_Of_List_And_Collection_Returns_Converted_List()
        {
            Collection<ClassForConversion> listToConvert = new Collection<ClassForConversion>()
            {
                new ClassForConversion(),
                new ClassForConversion(),
                new ClassForConversion()
            };
            List<BaseForConversion> convertedList = new List<BaseForConversion>(ListLibrary.Convert<ClassForConversion, BaseForConversion>(listToConvert));

            Assert.IsTrue(listToConvert.Count == convertedList.Count);
        }


        private class BaseForConversion
        {

        }

        private class ClassForConversion : BaseForConversion
        {
            
        }
    }
   
}
