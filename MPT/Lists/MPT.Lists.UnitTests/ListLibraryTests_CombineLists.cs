using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace MPT.Lists.UnitTests
{
    [TestFixture]
    public class ListLibraryTests_CombineLists
    {
        [Test]
        public void AppendToList_Returns_EmptyIf_All_Lists_Are_Null()
        {
            List<string> appendedList = new List<string>(ListLibrary.AppendToList(null, null));

            Assert.IsEmpty(appendedList);
        }

        [Test]
        public void AppendToList_Returns_List_To_Append_If_Original_List_Is_Null()
        {
            Collection<string> listToAppend = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };

            List<string> appendedList = new List<string>(ListLibrary.AppendToList(null, listToAppend));

            Assert.IsTrue(listToAppend.Count == appendedList.Count);
            Assert.AreEqual(listToAppend[0], appendedList[0]);
            Assert.AreEqual(listToAppend[1], appendedList[1]);
            Assert.AreEqual(listToAppend[2], appendedList[2]);
            Assert.AreEqual(listToAppend[3], appendedList[3]);
        }

        [Test]
        public void AppendToList_Returns_Original_List_If_List_To_Append_Is_Null()
        {
            Collection<string> originalList = new Collection<string>() { "Roo", "Rar", "Soo", "Car" };

            List<string> appendedList = new List<string>(ListLibrary.AppendToList(originalList, null));

            Assert.IsTrue(originalList.Count == appendedList.Count);
            Assert.AreEqual(originalList[0], appendedList[0]);
            Assert.AreEqual(originalList[1], appendedList[1]);
            Assert.AreEqual(originalList[2], appendedList[2]);
            Assert.AreEqual(originalList[3], appendedList[3]);
        }

        [Test]
        public void AppendToList_Returns_Appended_List()
        {
            Collection<string> originalList = new Collection<string>() { "Roo", "Rar", "Soo", "Car" };
            Collection<string> listToAppend = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            Collection<string> expectedResult = new Collection<string>() { "Roo", "Rar", "Soo", "Car", "Foo", "Bar", "Moo", "Nar" };

            List<string> appendedList = new List<string>(ListLibrary.AppendToList(originalList, listToAppend));

            Assert.IsTrue(appendedList.Count == listToAppend.Count + originalList.Count);
            Assert.AreEqual(expectedResult[0], appendedList[0]);
            Assert.AreEqual(expectedResult[1], appendedList[1]);
            Assert.AreEqual(expectedResult[2], appendedList[2]);
            Assert.AreEqual(expectedResult[3], appendedList[3]);
            Assert.AreEqual(expectedResult[4], appendedList[4]);
            Assert.AreEqual(expectedResult[5], appendedList[5]);
            Assert.AreEqual(expectedResult[6], appendedList[6]);
            Assert.AreEqual(expectedResult[7], appendedList[7]);
        }



        [Test]
        public void CombineListsUnique_Returns_EmptyIf_All_Lists_Are_Null()
        {
            List<string> newList = new List<string>(ListLibrary.CombineListsUnique(null, null));

            Assert.IsEmpty(newList);
        }

        [Test]
        public void CombineListsUnique_Returns_List_To_Append_If_Original_List_Is_Null()
        {
            Collection<string> newList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };

            List<string> expectedResult = new List<string>(ListLibrary.CombineListsUnique(null, newList));

            Assert.IsTrue(newList.Count == expectedResult.Count);
            Assert.AreEqual(newList[0], expectedResult[0]);
            Assert.AreEqual(newList[1], expectedResult[1]);
            Assert.AreEqual(newList[2], expectedResult[2]);
            Assert.AreEqual(newList[3], expectedResult[3]);
        }

        [Test]
        public void CombineListsUnique_Returns_Original_List_If_List_To_Append_Is_Null()
        {
            Collection<string> originalList = new Collection<string>() { "Roo", "Rar", "Soo", "Car" };

            List<string> expectedResult = new List<string>(ListLibrary.CombineListsUnique(originalList, null));

            Assert.IsTrue(originalList.Count == expectedResult.Count);
            Assert.AreEqual(originalList[0], expectedResult[0]);
            Assert.AreEqual(originalList[1], expectedResult[1]);
            Assert.AreEqual(originalList[2], expectedResult[2]);
            Assert.AreEqual(originalList[3], expectedResult[3]);
        }

        [Test]
        public void CombineListsUnique_Returns_Appended_List_If_Both_Lists_Are_Unique()
        {
            Collection<string> originalList = new Collection<string>() { "Roo", "Rar", "Soo", "Car" };
            Collection<string> newList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            Collection<string> expectedResult = new Collection<string>() { "Roo", "Rar", "Soo", "Car", "Foo", "Bar", "Moo", "Nar" };

            List<string> appendedList = new List<string>(ListLibrary.CombineListsUnique(originalList, newList));

            Assert.IsTrue(appendedList.Count == newList.Count + originalList.Count);
            Assert.AreEqual(expectedResult[0], appendedList[0]);
            Assert.AreEqual(expectedResult[1], appendedList[1]);
            Assert.AreEqual(expectedResult[2], appendedList[2]);
            Assert.AreEqual(expectedResult[3], appendedList[3]);
            Assert.AreEqual(expectedResult[4], appendedList[4]);
            Assert.AreEqual(expectedResult[5], appendedList[5]);
            Assert.AreEqual(expectedResult[6], appendedList[6]);
            Assert.AreEqual(expectedResult[7], appendedList[7]);
        }

        [Test]
        public void CombineListsUnique_Returns_Unique_Combined_List_If_Both_Lists_Have_Unique_And_Matching_Items()
        {
            Collection<string> originalList = new Collection<string>() { "Roo", "Foo", "Soo", "Bar" };
            Collection<string> newList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            Collection<string> expectedResult = new Collection<string>() { "Roo", "Foo", "Soo", "Bar", "Moo", "Nar" };

            List<string> combinedList = new List<string>(ListLibrary.CombineListsUnique(originalList, newList));

            Assert.IsTrue(combinedList.Count < newList.Count + originalList.Count);
            Assert.AreEqual(expectedResult[0], combinedList[0]);
            Assert.AreEqual(expectedResult[1], combinedList[1]);
            Assert.AreEqual(expectedResult[2], combinedList[2]);
            Assert.AreEqual(expectedResult[3], combinedList[3]);
            Assert.AreEqual(expectedResult[4], combinedList[4]);
            Assert.AreEqual(expectedResult[5], combinedList[5]);
        }

        [Test]
        public void CombineListsUnique_Returns_Original_List_If_Both_Lists_Are_Identical()
        {
            Collection<string> originalList = new Collection<string>() { "Roo", "Rar", "Soo", "Car" };
            Collection<string> newList = new Collection<string>() { "Roo", "Rar", "Soo", "Car" };
            Collection<string> expectedResult = new Collection<string>() { "Roo", "Rar", "Soo", "Car" };

            List<string> appendedList = new List<string>(ListLibrary.CombineListsUnique(originalList, newList));

            Assert.IsTrue(appendedList.Count == originalList.Count);
            Assert.AreEqual(expectedResult[0], appendedList[0]);
            Assert.AreEqual(expectedResult[1], appendedList[1]);
            Assert.AreEqual(expectedResult[2], appendedList[2]);
            Assert.AreEqual(expectedResult[3], appendedList[3]);
        }
    }
}
