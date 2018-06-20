using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace MPT.Lists.UnitTests
{
    [TestFixture]
    public class ListLibraryTests_RemoveSort
    {

        [Test]
        public void TrimListOfEmptyItems_Given_Null_Returns_Empty()
        {
            List<string> trimmedList = new List<string>(ListLibrary.TrimListOfEmptyItems(null));
            Assert.IsEmpty(trimmedList);
        }

        [Test]
        public void TrimListOfEmptyItems_WIth_All_Empty_Items_Returns_Empty()
        {
            Collection<string> empty = new Collection<string>() { "", "", " ", " ", "" };
            List<string> trimmedList = new List<string>(ListLibrary.TrimListOfEmptyItems(empty));
            Assert.IsEmpty(trimmedList);
        }

        [Test]
        public void TrimListOfEmptyItems_With_No_Empty_Items_Returns_Original()
        {
            Collection<string> original = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            List<string> trimmedList = new List<string>(ListLibrary.TrimListOfEmptyItems(original));
            Assert.IsTrue(original.Count == trimmedList.Count);
            Assert.AreEqual(original[0], trimmedList[0]);
            Assert.AreEqual(original[1], trimmedList[1]);
            Assert.AreEqual(original[2], trimmedList[2]);
            Assert.AreEqual(original[3], trimmedList[3]);
        }


        [Test]
        public void TrimListOfEmptyItems_Trims_Empty_Items()
        {
            Collection<string> original = new Collection<string>() { "Foo", "", "Bar", "Moo", " ", "Nar" };
            List<string> expectedResult = new List<string>() { "Foo", "Bar", "Moo", "Nar" };
            List<string> trimmedList = new List<string>(ListLibrary.TrimListOfEmptyItems(original));

            Assert.IsTrue(original.Count > trimmedList.Count);
            Assert.AreEqual(expectedResult[0], trimmedList[0]);
            Assert.AreEqual(expectedResult[1], trimmedList[1]);
            Assert.AreEqual(expectedResult[2], trimmedList[2]);
            Assert.AreEqual(expectedResult[3], trimmedList[3]);
        }




        [Test]
        public void RemoveFromList_Where_Both_Lists_Are_Null_Returns_Empty()
        {
            List<string> filteredList = new List<string>(ListLibrary.RemoveFromList(null, null));

            Assert.IsEmpty(filteredList);
        }

        [Test]
        public void RemoveFromList_Where_OriginalList_Is_Null_Returns_Empty()
        {
            Collection<string> listToRemove = new Collection<string>() { "Bar", "Nar" };
            List<string> filteredList = new List<string>(ListLibrary.RemoveFromList(listToRemove, null));

            Assert.IsEmpty(filteredList);
        }

        [Test]
        public void RemoveFromList_Where_ListToRemove_Is_Null_Returns_OriginalList()
        {
            Collection<string> originalList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            List<string> filteredList = new List<string>(ListLibrary.RemoveFromList(null, originalList));

            Assert.IsTrue(originalList.Count == filteredList.Count);
            Assert.AreEqual(originalList[0], filteredList[0]);
            Assert.AreEqual(originalList[1], filteredList[1]);
            Assert.AreEqual(originalList[2], filteredList[2]);
            Assert.AreEqual(originalList[3], filteredList[3]);
        }
        

        [Test]
        public void RemoveFromList_Where_No_Items_To_Remove_Found_Returns_OriginalList()
        {
            Collection<string> listToRemove = new Collection<string>() { "Fie", "Fei" };
            Collection<string> originalList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            List<string> filteredList = new List<string>(ListLibrary.RemoveFromList(listToRemove, originalList));

            Assert.IsTrue(originalList.Count == filteredList.Count);
            Assert.AreEqual(originalList[0], filteredList[0]);
            Assert.AreEqual(originalList[1], filteredList[1]);
            Assert.AreEqual(originalList[2], filteredList[2]);
            Assert.AreEqual(originalList[3], filteredList[3]);
        }

        [Test]
        public void RemoveFromList_Where_All_Original_Items_Removed_Returns_Empty()
        {
            Collection<string> listToRemove = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            Collection<string> originalList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            List<string> filteredList = new List<string>(ListLibrary.RemoveFromList(listToRemove, originalList));

            Assert.IsEmpty(filteredList);
        }

        [Test]
        public void RemoveFromList_Removes_Matching_Items_From_List()
        {
            Collection<string> listToRemove = new Collection<string>() { "Bar", "Nar", "Fei", "Fie" };
            Collection<string> originalList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            List<string> expectedResults = new List<string>() {"Foo", "Moo"};
            List<string> filteredList = new List<string>(ListLibrary.RemoveFromList(listToRemove, originalList));

            Assert.IsTrue(originalList.Count > filteredList.Count);
            Assert.AreEqual(expectedResults[0], filteredList[0]);
            Assert.AreEqual(expectedResults[1], filteredList[1]);
        }

        [Test]
        public void RemoveFromList_Where_Case_Ignored_Removes_Differing_Cases_Of_Same_Items()
        {
            Collection<string> listToRemove = new Collection<string>() { "bar", "Nar", "Fei", "Fie" };
            Collection<string> originalList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            List<string> expectedResults = new List<string>() { "Foo", "Moo" };
            List<string> filteredList = new List<string>(ListLibrary.RemoveFromList(listToRemove, originalList, caseSensitive: false));

            Assert.IsTrue(originalList.Count > filteredList.Count);
            Assert.AreEqual(expectedResults[0], filteredList[0]);
            Assert.AreEqual(expectedResults[1], filteredList[1]);
        }

        [Test]
        public void RemoveFromList_Where_Case_Considered_Removes_Only_Matching_Case_Items()
        {
            Collection<string> listToRemove = new Collection<string>() { "bar", "Nar", "Fei", "Fie" };
            Collection<string> originalList = new Collection<string>() { "Foo", "Bar", "Moo", "Nar" };
            List<string> expectedResults = new List<string>() { "Foo", "Bar", "Moo" };
            List<string> filteredList = new List<string>(ListLibrary.RemoveFromList(listToRemove, originalList, caseSensitive: true));

            Assert.IsTrue(originalList.Count > filteredList.Count);
            Assert.AreEqual(expectedResults[0], filteredList[0]);
            Assert.AreEqual(expectedResults[1], filteredList[1]);
            Assert.AreEqual(expectedResults[2], filteredList[2]);
        }




        [Test]
        public void SortCorrelatedLists_With_All_Lists_Null_Returns_All_Null()
        {
            IList<string> listToSort = null;
            IList<IList<string>> correlatedLists = null;

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists);

            Assert.IsNull(listToSort);
            Assert.IsNull(correlatedLists);
        }

        [Test]
        public void SortCorrelatedLists_With_All_Lists_Empty_Returns_All_Empty()
        {
            IList<string> listToSort = new List<string>();
            IList<IList<string>> correlatedLists = new List<IList<string>>();

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists);

            Assert.IsEmpty(listToSort);
            Assert.IsEmpty(correlatedLists);
        }

        [Test]
        public void SortCorrelatedLists_With_ListToSort_Null_Returns_Lists_As_Is()
        {
            IList<string> listToSort = null;

            IList<IList<string>> correlatedLists = new List<IList<string>>();
            List<string> list1 = new List<string>() { "One", "Two", "Three", "Four" };
            List<string> list2 = new List<string>() { "Four", "One", "Two", "Three" };
            List<string> list3 = new List<string>() { "Three", "Four", "One", "Two" };
            correlatedLists.Add(list1);
            correlatedLists.Add(list2);
            correlatedLists.Add(list3);

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists);

            Assert.IsNull(listToSort);

            Assert.AreEqual(list1[0], correlatedLists[0][0]);
            Assert.AreEqual(list1[1], correlatedLists[0][1]);
            Assert.AreEqual(list1[2], correlatedLists[0][2]);
            Assert.AreEqual(list1[3], correlatedLists[0][3]);

            Assert.AreEqual(list2[0], correlatedLists[1][0]);
            Assert.AreEqual(list2[1], correlatedLists[1][1]);
            Assert.AreEqual(list2[2], correlatedLists[1][2]);
            Assert.AreEqual(list2[3], correlatedLists[1][3]);

            Assert.AreEqual(list3[0], correlatedLists[2][0]);
            Assert.AreEqual(list3[1], correlatedLists[2][1]);
            Assert.AreEqual(list3[2], correlatedLists[2][2]);
            Assert.AreEqual(list3[3], correlatedLists[2][3]);
        }

        [Test]
        public void SortCorrelatedLists_With_ListToSort_Empty_Returns_Lists_As_Is()
        {
            IList<string> listToSort = new List<string>();

            IList<IList<string>> correlatedLists = new List<IList<string>>();
            List<string> list1 = new List<string>() { "One", "Two", "Three", "Four" };
            List<string> list2 = new List<string>() { "Four", "One", "Two", "Three" };
            List<string> list3 = new List<string>() { "Three", "Four", "One", "Two" };
            correlatedLists.Add(list1);
            correlatedLists.Add(list2);
            correlatedLists.Add(list3);

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists);

            Assert.IsEmpty(listToSort);

            Assert.AreEqual(list1[0], correlatedLists[0][0]);
            Assert.AreEqual(list1[1], correlatedLists[0][1]);
            Assert.AreEqual(list1[2], correlatedLists[0][2]);
            Assert.AreEqual(list1[3], correlatedLists[0][3]);

            Assert.AreEqual(list2[0], correlatedLists[1][0]);
            Assert.AreEqual(list2[1], correlatedLists[1][1]);
            Assert.AreEqual(list2[2], correlatedLists[1][2]);
            Assert.AreEqual(list2[3], correlatedLists[1][3]);

            Assert.AreEqual(list3[0], correlatedLists[2][0]);
            Assert.AreEqual(list3[1], correlatedLists[2][1]);
            Assert.AreEqual(list3[2], correlatedLists[2][2]);
            Assert.AreEqual(list3[3], correlatedLists[2][3]);
        }

        [Test]
        public void SortCorrelatedLists_With_CorrelatedLists_Null_Sorts_List_Returns_Null_CorrelatedLists()
        {
            IList<string> listToSort = new List<string>() { "2", "4", "1", "3" };
            IList<IList<string>> correlatedLists = null;

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists);

            Assert.IsNull(correlatedLists);
            Assert.AreEqual("1", listToSort[0]);
            Assert.AreEqual("2", listToSort[1]);
            Assert.AreEqual("3", listToSort[2]);
            Assert.AreEqual("4", listToSort[3]);
        }


        [Test]
        public void SortCorrelatedLists_With_Correlated_Lists_Containing_Null_Sorts_Lists_Setting_Null_Lists_To_Empty()
        {
            IList<string> listToSort = new List<string>() { "2", "4", "1", "3" };

            IList<IList<string>> correlatedLists = new List<IList<string>>();
            List<string> list1 = new List<string>() { "One", "Two", "Three", "Four" };
            List<string> list2 = null;
            List<string> list3 = new List<string>() { "Three", "Four", "One", "Two" };
            correlatedLists.Add(list1);
            correlatedLists.Add(list2);
            correlatedLists.Add(list3);

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists);
            
            Assert.AreEqual("1", listToSort[0]);
            Assert.AreEqual("2", listToSort[1]);
            Assert.AreEqual("3", listToSort[2]);
            Assert.AreEqual("4", listToSort[3]);

            Assert.AreEqual(list1[2], correlatedLists[0][0]);
            Assert.AreEqual(list1[0], correlatedLists[0][1]);
            Assert.AreEqual(list1[3], correlatedLists[0][2]);
            Assert.AreEqual(list1[1], correlatedLists[0][3]);

            Assert.IsEmpty(correlatedLists[1]);

            Assert.AreEqual(list3[2], correlatedLists[2][0]);
            Assert.AreEqual(list3[0], correlatedLists[2][1]);
            Assert.AreEqual(list3[3], correlatedLists[2][2]);
            Assert.AreEqual(list3[1], correlatedLists[2][3]);
        }

        [Test]
        public void SortCorrelatedLists_With_Correlated_Lists_Containing_Empty_Sorts_Lists_Setting_Null_Lists_To_Empty()
        {
            IList<string> listToSort = new List<string>() { "2", "4", "1", "3" };

            IList<IList<string>> correlatedLists = new List<IList<string>>();
            List<string> list1 = new List<string>() { "One", "Two", "Three", "Four" };
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>() { "Three", "Four", "One", "Two" };
            correlatedLists.Add(list1);
            correlatedLists.Add(list2);
            correlatedLists.Add(list3);

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists);

            Assert.AreEqual("1", listToSort[0]);
            Assert.AreEqual("2", listToSort[1]);
            Assert.AreEqual("3", listToSort[2]);
            Assert.AreEqual("4", listToSort[3]);

            Assert.AreEqual(list1[2], correlatedLists[0][0]);
            Assert.AreEqual(list1[0], correlatedLists[0][1]);
            Assert.AreEqual(list1[3], correlatedLists[0][2]);
            Assert.AreEqual(list1[1], correlatedLists[0][3]);

            Assert.IsEmpty(correlatedLists[1]);

            Assert.AreEqual(list3[2], correlatedLists[2][0]);
            Assert.AreEqual(list3[0], correlatedLists[2][1]);
            Assert.AreEqual(list3[3], correlatedLists[2][2]);
            Assert.AreEqual(list3[1], correlatedLists[2][3]);
        }

        [Test]
        public void SortCorrelatedLists_Sorts_List_And_Sorts_Other_Lists_Correlating_Indices()
        {
            IList<string> listToSort = new List<string>() { "2", "4", "1", "3" };

            IList<IList<string>> correlatedLists = new List<IList<string>>();
            List<string> list1 = new List<string>() { "One", "Two", "Three", "Four" };
            List<string> list2 = new List<string>() { "Four", "One", "Two", "Three" };
            List<string> list3 = new List<string>() { "Three", "Four", "One", "Two" };
            correlatedLists.Add(list1);
            correlatedLists.Add(list2);
            correlatedLists.Add(list3);

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists);

            Assert.AreEqual("1", listToSort[0]);
            Assert.AreEqual("2", listToSort[1]);
            Assert.AreEqual("3", listToSort[2]);
            Assert.AreEqual("4", listToSort[3]);

            Assert.AreEqual(list1[2], correlatedLists[0][0]);
            Assert.AreEqual(list1[0], correlatedLists[0][1]);
            Assert.AreEqual(list1[3], correlatedLists[0][2]);
            Assert.AreEqual(list1[1], correlatedLists[0][3]);

            Assert.AreEqual(list2[2], correlatedLists[1][0]);
            Assert.AreEqual(list2[0], correlatedLists[1][1]);
            Assert.AreEqual(list2[3], correlatedLists[1][2]);
            Assert.AreEqual(list2[1], correlatedLists[1][3]);

            Assert.AreEqual(list3[2], correlatedLists[2][0]);
            Assert.AreEqual(list3[0], correlatedLists[2][1]);
            Assert.AreEqual(list3[3], correlatedLists[2][2]);
            Assert.AreEqual(list3[1], correlatedLists[2][3]);
        }

        [Test]
        public void SortCorrelatedLists_Sorts_Ascending()
        {
            IList<string> listToSort = new List<string>() { "2", "4", "1", "3" };

            IList<IList<string>> correlatedLists = new List<IList<string>>();
            List<string> list1 = new List<string>() { "One", "Two", "Three", "Four" };
            List<string> list2 = new List<string>() { "Four", "One", "Two", "Three" };
            List<string> list3 = new List<string>() { "Three", "Four", "One", "Two" };
            correlatedLists.Add(list1);
            correlatedLists.Add(list2);
            correlatedLists.Add(list3);

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists, sortAscending: true);

            Assert.AreEqual("1", listToSort[0]);
            Assert.AreEqual("2", listToSort[1]);
            Assert.AreEqual("3", listToSort[2]);
            Assert.AreEqual("4", listToSort[3]);

            Assert.AreEqual(list1[2], correlatedLists[0][0]);
            Assert.AreEqual(list1[0], correlatedLists[0][1]);
            Assert.AreEqual(list1[3], correlatedLists[0][2]);
            Assert.AreEqual(list1[1], correlatedLists[0][3]);

            Assert.AreEqual(list2[2], correlatedLists[1][0]);
            Assert.AreEqual(list2[0], correlatedLists[1][1]);
            Assert.AreEqual(list2[3], correlatedLists[1][2]);
            Assert.AreEqual(list2[1], correlatedLists[1][3]);

            Assert.AreEqual(list3[2], correlatedLists[2][0]);
            Assert.AreEqual(list3[0], correlatedLists[2][1]);
            Assert.AreEqual(list3[3], correlatedLists[2][2]);
            Assert.AreEqual(list3[1], correlatedLists[2][3]);
        }

        [Test]
        public void SortCorrelatedLists_Sorts_Descending()
        {
            IList<string> listToSort = new List<string>() { "2", "4", "1", "3" };

            IList<IList<string>> correlatedLists = new List<IList<string>>();
            List<string> list1 = new List<string>() { "One", "Two", "Three", "Four" };
            List<string> list2 = new List<string>() { "Four", "One", "Two", "Three" };
            List<string> list3 = new List<string>() { "Three", "Four", "One", "Two" };
            correlatedLists.Add(list1);
            correlatedLists.Add(list2);
            correlatedLists.Add(list3);

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists, sortAscending: false);

            Assert.AreEqual("4", listToSort[0]);
            Assert.AreEqual("3", listToSort[1]);
            Assert.AreEqual("2", listToSort[2]);
            Assert.AreEqual("1", listToSort[3]);

            Assert.AreEqual(list1[1], correlatedLists[0][0]);
            Assert.AreEqual(list1[3], correlatedLists[0][1]);
            Assert.AreEqual(list1[0], correlatedLists[0][2]);
            Assert.AreEqual(list1[2], correlatedLists[0][3]);

            Assert.AreEqual(list2[1], correlatedLists[1][0]);
            Assert.AreEqual(list2[3], correlatedLists[1][1]);
            Assert.AreEqual(list2[0], correlatedLists[1][2]);
            Assert.AreEqual(list2[2], correlatedLists[1][3]);

            Assert.AreEqual(list3[1], correlatedLists[2][0]);
            Assert.AreEqual(list3[3], correlatedLists[2][1]);
            Assert.AreEqual(list3[0], correlatedLists[2][2]);
            Assert.AreEqual(list3[2], correlatedLists[2][3]);
        }

        [Test]
        public void SortCorrelatedLists_With_Correlated_Lists_Containing_Different_List_Types()
        {
            IList<string> listToSort = new List<string>() { "2", "4", "1", "3" };

            IList<IList<string>> correlatedLists = new List<IList<string>>();
            List<string> list1 = new List<string>() { "One", "Two", "Three", "Four" };
            Collection<string> list2 = new Collection<string>() { "Four", "One", "Two", "Three" };
            ObservableCollection<string> list3 = new ObservableCollection<string>() { "Three", "Four", "One", "Two" };
            correlatedLists.Add(list1);
            correlatedLists.Add(list2);
            correlatedLists.Add(list3);

            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists);

            Assert.AreEqual("1", listToSort[0]);
            Assert.AreEqual("2", listToSort[1]);
            Assert.AreEqual("3", listToSort[2]);
            Assert.AreEqual("4", listToSort[3]);

            Assert.AreEqual(list1[2], correlatedLists[0][0]);
            Assert.AreEqual(list1[0], correlatedLists[0][1]);
            Assert.AreEqual(list1[3], correlatedLists[0][2]);
            Assert.AreEqual(list1[1], correlatedLists[0][3]);

            Assert.AreEqual(list2[2], correlatedLists[1][0]);
            Assert.AreEqual(list2[0], correlatedLists[1][1]);
            Assert.AreEqual(list2[3], correlatedLists[1][2]);
            Assert.AreEqual(list2[1], correlatedLists[1][3]);

            Assert.AreEqual(list3[2], correlatedLists[2][0]);
            Assert.AreEqual(list3[0], correlatedLists[2][1]);
            Assert.AreEqual(list3[3], correlatedLists[2][2]);
            Assert.AreEqual(list3[1], correlatedLists[2][3]);
        }

        [Test]
        public void SortCorrelatedLists_With_Jagged_Correlated_Lists_Throws_Exception()
        {
            var ex = Assert.Throws<IndexOutOfRangeException>(() =>
                        {
                            IList<string> listToSort = new List<string>() {"2", "4", "1", "3"};

                            IList<IList<string>> correlatedLists = new List<IList<string>>();
                            List<string> list1 = new List<string>() {"One", "Two"};
                            List<string> list2 = new List<string>() {"Four", "One", "Two", "Three", "Foo"};
                            List<string> list3 = new List<string>() {"Three", "Four", "One", "Two"};
                            correlatedLists.Add(list1);
                            correlatedLists.Add(list2);
                            correlatedLists.Add(list3);

                            ListLibrary.SortCorrelatedLists(ref listToSort, ref correlatedLists, sortAscending: true);
                        });
            Assert.That(ex.Message, Is.EqualTo("Method cannot correlate sorting of jagged lists! \r\nMake sure all lists provided are of equal length to each other and the primary list being sorted."));
        }
    }
}
