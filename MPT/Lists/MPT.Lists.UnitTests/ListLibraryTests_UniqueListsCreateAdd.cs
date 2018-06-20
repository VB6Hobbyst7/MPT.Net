using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace MPT.Lists.UnitTests
{
    [TestFixture]
    public class ListLibraryTests_UniqueListsCreateAdd
    {
        [Test]
        public void CreateUniqueList_of_String_BaseList_Null_Returns_CheckList()
        {
            List<string> checkList = new List<string> { "Foo", "Bar" };
            IList<string> uniqueList = new List<string>(ListLibrary.MergeUnique(checkList, null));

            Assert.AreEqual(uniqueList.Count, checkList.Count);
            Assert.AreEqual(checkList[0], uniqueList[0]);
            Assert.AreEqual(checkList[1], uniqueList[1]);
        }

        [Test]
        public void CreateUniqueList_of_String_CheckList_Null_Returns_BaseList()
        {
            List<string> baseList = new List<string> { "Bar", "Moo", "Nar" };

            IList<string> uniqueList = new List<string>(ListLibrary.MergeUnique(null, baseList));

            Assert.AreEqual(uniqueList.Count, baseList.Count);
            Assert.AreEqual(baseList[0], uniqueList[0]);
            Assert.AreEqual(baseList[1], uniqueList[1]);
            Assert.AreEqual(baseList[2], uniqueList[2]);
        }

        [Test]
        public void CreateUniqueList_of_String_CheckList_Null_And_BaseList_Null_Returns_Empty()
        {
            IList<string> uniqueList = new List<string>(ListLibrary.MergeUnique(null, null));

            Assert.IsEmpty(uniqueList);
        }

        [Test]
        public void CreateUniqueList_of_String_Returns_Unique_List_of_Merged_Lists()
        {
            List<string> checkList = new List<string> { "Foo", "Bar" };
            List<string> baseList = new List<string> { "Bar", "Moo", "Nar" };
            List<string> resultList = new List<string> { "Bar", "Moo", "Nar", "Foo" };

            IList<string> uniqueList = new List<string>(ListLibrary.MergeUnique(checkList, baseList));

            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }

        [Test]
        public void CreateUniqueList_of_String_And_Different_List_Types_Returns_Unique_List_of_Merged_Lists()
        {
            ObservableCollection<string> checkList = new ObservableCollection<string> { "Foo", "Bar" };
            Collection<string> baseList = new Collection<string> { "Bar", "Moo", "Nar" };
            List<string> resultList = new List<string> { "Bar", "Moo", "Nar", "Foo" };

            IList<string> uniqueList = new List<string>(ListLibrary.MergeUnique(checkList, baseList));

            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }

        [Test]
        public void CreateUniqueList_of_List_of_String_Returns_Unique_List()
        {
            ObservableCollection<string> checkList = new ObservableCollection<string> { "Foo", "Bar" };
            Collection<string> baseList = new Collection<string> { "Bar", "Moo", "Nar" };
            List<string> resultList = new List<string> { "Bar", "Moo", "Nar", "Foo" };

            List<string> uniqueList = ListLibrary.CreateUniqueList(checkList, baseList);

            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }

        [Test]
        public void CreateUniqueList_of_List_of_String_Returns_Unique_List_Considering_Case()
        {
            ObservableCollection<string> checkList = new ObservableCollection<string> { "Foo", "bar" };
            Collection<string> baseList = new Collection<string> { "Bar", "Moo", "Nar" };
            List<string> resultList = new List<string> { "Bar", "Moo", "Nar", "Foo", "bar" };

            List<string> uniqueList = ListLibrary.CreateUniqueList(checkList, baseList, caseSensitive: true);

            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
            Assert.AreEqual(resultList[4], uniqueList[4]);
        }

        [Test]
        public void CreateUniqueList_of_ObservableCollection_of_String_Returns_Unique_List()
        {
            ObservableCollection<string> checkList = new ObservableCollection<string> { "Foo", "Bar" };
            Collection<string> baseList = new Collection<string> { "Bar", "Moo", "Nar" };
            List<string> resultList = new List<string> { "Bar", "Moo", "Nar", "Foo" };

            ObservableCollection<string> uniqueList = ListLibrary.CreateUniqueObservableCollection(checkList, baseList);

            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }

        [Test]
        public void CreateUniqueList_of_ObservableCollection_of_String_Returns_Unique_List_Considering_Case()
        {
            ObservableCollection<string> checkList = new ObservableCollection<string> { "Foo", "bar" };
            Collection<string> baseList = new Collection<string> { "Bar", "Moo", "Nar" };
            List<string> resultList = new List<string> { "Bar", "Moo", "Nar", "Foo", "bar" };

            ObservableCollection<string> uniqueList = ListLibrary.CreateUniqueObservableCollection(checkList, baseList, caseSensitive: true);

            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
            Assert.AreEqual(resultList[4], uniqueList[4]);
        }


        [Test]
        public void ConvertToUniqueList_Of_String_Of_Null_Returns_Empty_List()
        {
            IList<string> uniqueList = new List<string>();
            ListLibrary.ConvertToUniqueList(null);

            Assert.IsEmpty(uniqueList);
        }

        [Test]
        public void ConvertToUniqueList_Of_String_Of_Unique_List_Returns_Original_List()
        {
            Collection<string> originalList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar" };

            IList<string> uniqueList = new List<string>(ListLibrary.ConvertToUnique(originalList));

            Assert.IsTrue(uniqueList.Count == originalList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }


        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_List_Returns_Unique_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "Bar", "Foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar" };

            IList<string> uniqueList = new List<string>(ListLibrary.ConvertToUnique(nonUniqueList));

            Assert.IsTrue(uniqueList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }

        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_List_By_Case_Ignoring_Case_Returns_Unique_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "bar", "foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar" };

            IList<string> uniqueList = new List<string>(ListLibrary.ConvertToUnique(nonUniqueList, caseSensitive: false));

            Assert.IsTrue(uniqueList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }


        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_List_By_Case_Considering_Case_Returns_Original_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "bar", "foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar", "bar", "foo" };

            IList<string> uniqueList = new List<string>(ListLibrary.ConvertToUnique(nonUniqueList, caseSensitive: true));

            Assert.IsTrue(uniqueList.Count == nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
            Assert.AreEqual(resultList[4], uniqueList[4]);
            Assert.AreEqual(resultList[5], uniqueList[5]);
        }

        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_List_Not_By_Case_Ignoring_Case_Returns_Unique_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "Bar", "Foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar" };

            IList<string> uniqueList = new List<string>(ListLibrary.ConvertToUnique(nonUniqueList, caseSensitive: false));

            Assert.IsTrue(uniqueList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }

        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_List_Not_By_Case_Considering_Case_Returns_Unique_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "Bar", "Foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar" };

            IList<string> uniqueList = new List<string>(ListLibrary.ConvertToUnique(nonUniqueList, caseSensitive: true));

            Assert.IsTrue(uniqueList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }

        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_List_of_String_By_Case_Ignoring_Case_Returns_Unique_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "bar", "foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar" };

            List<string> uniqueList = ListLibrary.ConvertToUniqueList(nonUniqueList, caseSensitive: false);

            Assert.IsTrue(resultList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }


        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_List_of_String_By_Case_Considering_Case_Returns_Original_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "bar", "foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar", "bar", "foo" };

            List<string> uniqueList = ListLibrary.ConvertToUniqueList(nonUniqueList, caseSensitive: true);

            Assert.IsTrue(resultList.Count == nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
            Assert.AreEqual(resultList[4], uniqueList[4]);
            Assert.AreEqual(resultList[5], uniqueList[5]);
        }

        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_List_of_String_Not_By_Case_Ignoring_Case_Returns_Unique_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "bar", "foo", "Fie", "Foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar" };

            List<string> uniqueList = ListLibrary.ConvertToUniqueList(nonUniqueList, caseSensitive: false);

            Assert.IsTrue(resultList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }

        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_List_of_String_Not_By_Case_Considering_Case_Returns_Unique_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "bar", "foo", "Fie", "Foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar", "bar", "foo", "Fie" };

            List<string> uniqueList = ListLibrary.ConvertToUniqueList(nonUniqueList, caseSensitive: true);

            Assert.IsTrue(resultList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
            Assert.AreEqual(resultList[4], uniqueList[4]);
            Assert.AreEqual(resultList[5], uniqueList[5]);
            Assert.AreEqual(resultList[6], uniqueList[6]);
        }

        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_ObservableCollection_of_String_By_Case_Ignoring_Case_Returns_Original_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "bar", "foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar" };

            ObservableCollection<string> uniqueList = ListLibrary.ConvertToUniqueObservableCollection(nonUniqueList, caseSensitive: false);

            Assert.IsTrue(resultList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }


        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_ObservableCollection_of_String_By_Case_Considering_Case_Returns_Unique_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "bar", "foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar", "bar", "foo" };

            ObservableCollection<string> uniqueList = ListLibrary.ConvertToUniqueObservableCollection(nonUniqueList, caseSensitive: true);

            Assert.IsTrue(resultList.Count == nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
            Assert.AreEqual(resultList[4], uniqueList[4]);
            Assert.AreEqual(resultList[5], uniqueList[5]);
        }

        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_ObservableCollection_of_String_Not_By_Case_Ignoring_Case_Returns_Unique_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "bar", "foo", "Fie", "Foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar" };

            ObservableCollection<string> uniqueList = ListLibrary.ConvertToUniqueObservableCollection(nonUniqueList, caseSensitive: false);

            Assert.IsTrue(resultList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }

        [Test]
        public void ConvertToUniqueList_Of_String_Of_Non_Unique_ObservableCollection_of_String_Not_By_Case_Considering_Case_Returns_Unique_List()
        {
            Collection<string> nonUniqueList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar", "bar", "foo", "Fie", "Foo" };
            List<string> resultList = new List<string> { "Foo", "Bar", "Moo", "Nar", "bar", "foo", "Fie" };

            ObservableCollection<string> uniqueList = ListLibrary.ConvertToUniqueObservableCollection(nonUniqueList, caseSensitive: true);

            Assert.IsTrue(resultList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
            Assert.AreEqual(resultList[4], uniqueList[4]);
            Assert.AreEqual(resultList[5], uniqueList[5]);
            Assert.AreEqual(resultList[6], uniqueList[6]);
        }

        [Test]
        public void ConvertToUniqueList_Of_Integer_Of_Null_Returns_Empty_List()
        {
            IList<int> newList = new List<int>(ListLibrary.ConvertToUniqueList(null));

            Assert.IsEmpty(newList);
        }

        [Test]
        public void ConvertToUniqueList_Of_Integer_Of_Unique_List_Returns_Original_List()
        {
            Collection<int> originalList = new Collection<int>() { 1, 55, 25, 999, -1, 3 };
            IList<int> uniqueList = new List<int>(ListLibrary.ConvertToUniqueList(originalList));
            List<int> resultList = new List<int> { 1, 55, 25, 999, -1, 3 };

            Assert.IsTrue(uniqueList.Count == originalList.Count);
            Assert.AreEqual(resultList[0], uniqueList[0]);
            Assert.AreEqual(resultList[1], uniqueList[1]);
            Assert.AreEqual(resultList[2], uniqueList[2]);
            Assert.AreEqual(resultList[3], uniqueList[3]);
        }


        [Test]
        public void ConvertToUniqueList_Of_Integer_Of_Non_Unique_List_Returns_Unique_List()
        {
            Collection<int> nonUniqueList = new Collection<int>() { 1, 55, 25, 999, -1, 3, 55, -1 };
            List<int> resultList = new List<int> { 1, 55, 25, 999, -1, 3 };

            IList<int> unqueList = new List<int>(ListLibrary.ConvertToUniqueList(nonUniqueList));

            Assert.IsTrue(unqueList.Count < nonUniqueList.Count);
            Assert.AreEqual(resultList[0], unqueList[0]);
            Assert.AreEqual(resultList[1], unqueList[1]);
            Assert.AreEqual(resultList[2], unqueList[2]);
            Assert.AreEqual(resultList[3], unqueList[3]);
        }



        [Test]
        public void ConvertToUniqueList_Of_Integer_Of_Non_Unique_List_of_Integer_Ignoring_Order_Returns_Unique_List_In_Original_Order()
        {
            Collection<int> nonUniqueList = new Collection<int>() { 1, 55, 25, 999, -1, 3, 55, -1 };
            IList<int> newList = ListLibrary.ConvertToUniqueList(nonUniqueList, sortList: false);

            List<int> resultList = new List<int> { 1, 55, 25, 999, -1, 3 };

            Assert.IsTrue(nonUniqueList.Count > newList.Count);
            Assert.AreEqual(resultList[0], newList[0]);
            Assert.AreEqual(resultList[1], newList[1]);
            Assert.AreEqual(resultList[2], newList[2]);
            Assert.AreEqual(resultList[3], newList[3]);
        }


        [Test]
        public void ConvertToUniqueList_Of_Integer_Of_Non_Unique_List_of_Integer_By_Order_Considering_Order_Returns_Unique_List_In_Order()
        {
            Collection<int> nonUniqueList = new Collection<int>() { 1, 55, 25, 999, -1, 3, 55, -1 };
            IList<int> newList = ListLibrary.ConvertToUniqueList(nonUniqueList, sortList: true);

            List<int> resultList = new List<int> { -1, 1, 3, 25, 55, 999 };

            Assert.IsTrue(nonUniqueList.Count > newList.Count);
            Assert.AreEqual(resultList[0], newList[0]);
            Assert.AreEqual(resultList[1], newList[1]);
            Assert.AreEqual(resultList[2], newList[2]);
            Assert.AreEqual(resultList[3], newList[3]);
        }



        [Test]
        public void ConvertToUniqueList_Of_Integer_Of_Non_Unique_ObservableCollection_of_Integer_Ignoring_Order_Returns_Unique_List_In_Original_Order()
        {
            Collection<int> nonUniqueList = new Collection<int>() { 1, 55, 25, 999, -1, 3, 55, -1 };
            IList<int> newList = ListLibrary.ConvertToUniqueObservableCollection(nonUniqueList, sortList: false);

            List<int> resultList = new List<int> { 1, 55, 25, 999, -1, 3 };

            Assert.IsTrue(nonUniqueList.Count > newList.Count);
            Assert.AreEqual(resultList[0], newList[0]);
            Assert.AreEqual(resultList[1], newList[1]);
            Assert.AreEqual(resultList[2], newList[2]);
            Assert.AreEqual(resultList[3], newList[3]);
        }


        [Test]
        public void ConvertToUniqueList_Of_Integer_Of_Non_Unique_ObservableCollection_of_Integer_By_Order_Considering_Order_Returns_Unique_List_In_Order()
        {
            Collection<int> nonUniqueList = new Collection<int>() { 1, 55, 25, 999, -1, 3, 55, -1 };
            IList<int> newList = ListLibrary.ConvertToUniqueObservableCollection(nonUniqueList, sortList: true);


            List<int> resultList = new List<int> { -1, 1, 3, 25, 55, 999 };

            Assert.IsTrue(nonUniqueList.Count > newList.Count);
            Assert.AreEqual(resultList[0], newList[0]);
            Assert.AreEqual(resultList[1], newList[1]);
            Assert.AreEqual(resultList[2], newList[2]);
            Assert.AreEqual(resultList[3], newList[3]);
        }


        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void AddIfNew_Returns_Original_If_Item_Is_Null_Or_Whitespace(string checkItem)
        {
            Collection<string> baseList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };

            IEnumerable<string> updatedList = ListLibrary.AddIfNew(baseList, checkItem);
            List<string> result = new List<string>(updatedList);

            List<string> expectedResult = new List<string>() { "Foo", "Bar", "Moo", "Nar" };
            Assert.AreEqual(expectedResult.Count, result.Count);
            Assert.AreEqual(expectedResult[0], result[0]);
            Assert.AreEqual(expectedResult[1], result[1]);
            Assert.AreEqual(expectedResult[2], result[2]);
            Assert.AreEqual(expectedResult[3], result[3]);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void AddIfNew_Returns_Empty_If_Item_Is_Null_Or_Whitespace_And_Baselist_Is_Null(string checkItem)
        {
            IEnumerable<string> updatedList = ListLibrary.AddIfNew(null, checkItem);
            List<string> result = new List<string>(updatedList);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void AddIfNew_Returns_List_Of_Only_Checked_Item_If_BaseList_Is_Null()
        {
            string checkItem = "Fie";

            IEnumerable<string> updatedList = ListLibrary.AddIfNew(null, checkItem);
            List<string> result = new List<string>(updatedList);

            List<string> expectedResult = new List<string>() { "Fie" };
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(expectedResult[0], result[0]);
        }

        [Test]
        public void AddIfNew_Ignores_New_If_Not_Unique()
        {
            Collection<string> baseList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            string checkItem = "Bar";

            IEnumerable<string> updatedList = ListLibrary.AddIfNew(baseList, checkItem);
            List<string> result = new List<string>(updatedList);

            List<string> expectedResult = new List<string>() { "Foo", "Bar", "Moo", "Nar" };
            Assert.AreEqual(expectedResult.Count, result.Count);
            Assert.AreEqual(expectedResult[0], result[0]);
            Assert.AreEqual(expectedResult[1], result[1]);
            Assert.AreEqual(expectedResult[2], result[2]);
            Assert.AreEqual(expectedResult[3], result[3]);
        }

        [Test]
        public void AddIfNew_Appends_New_If_Unique_By_Default()
        {
            Collection<string> baseList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            string checkItem = "Fie";

            IEnumerable<string> updatedList = ListLibrary.AddIfNew(baseList, checkItem);
            List<string> result = new List<string>(updatedList);

            List<string> expectedResult = new List<string>() { "Foo", "Bar", "Moo", "Nar", "Fie" };
            Assert.AreEqual(expectedResult.Count, result.Count);
            Assert.AreEqual(expectedResult[0], result[0]);
            Assert.AreEqual(expectedResult[1], result[1]);
            Assert.AreEqual(expectedResult[2], result[2]);
            Assert.AreEqual(expectedResult[3], result[3]);
            Assert.AreEqual(expectedResult[4], result[4]);
        }

        [Test]
        public void AddIfNew_Preppends_New_If_Unique_And_Place_First_Is_Specified()
        {
            Collection<string> baseList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            string checkItem = "Fie";

            IEnumerable<string> updatedList = ListLibrary.AddIfNew(baseList, checkItem, placeFirst: true);
            List<string> result = new List<string>(updatedList);

            List<string> expectedResult = new List<string>() { "Fie", "Foo", "Bar", "Moo", "Nar" };
            Assert.AreEqual(expectedResult.Count, result.Count);
            Assert.AreEqual(expectedResult[0], result[0]);
            Assert.AreEqual(expectedResult[1], result[1]);
            Assert.AreEqual(expectedResult[2], result[2]);
            Assert.AreEqual(expectedResult[3], result[3]);
            Assert.AreEqual(expectedResult[4], result[4]);
        }

        [Test]
        public void AddIfNew_Ignores_New_If_Not_Unique_Considering_Case()
        {
            Collection<string> baseList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            string checkItem = "bar";

            IEnumerable<string> updatedList = ListLibrary.AddIfNew(baseList, checkItem, caseSensitive: false);
            List<string> result = new List<string>(updatedList);

            List<string> expectedResult = new List<string>() { "Foo", "Bar", "Moo", "Nar" };
            Assert.AreEqual(expectedResult.Count, result.Count);
            Assert.AreEqual(expectedResult[0], result[0]);
            Assert.AreEqual(expectedResult[1], result[1]);
            Assert.AreEqual(expectedResult[2], result[2]);
            Assert.AreEqual(expectedResult[3], result[3]);
        }

        [Test]
        public void AddIfNew_Appends_New_If_Unique_Considering_Case()
        {
            Collection<string> baseList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            string checkItem = "bar";

            IEnumerable<string> updatedList = ListLibrary.AddIfNew(baseList, checkItem, caseSensitive: true);
            List<string> result = new List<string>(updatedList);

            List<string> expectedResult = new List<string>() { "Foo", "Bar", "Moo", "Nar", "bar" };
            Assert.AreEqual(expectedResult.Count, result.Count);
            Assert.AreEqual(expectedResult[0], result[0]);
            Assert.AreEqual(expectedResult[1], result[1]);
            Assert.AreEqual(expectedResult[2], result[2]);
            Assert.AreEqual(expectedResult[3], result[3]);
            Assert.AreEqual(expectedResult[4], result[4]);
        }
    }
}
