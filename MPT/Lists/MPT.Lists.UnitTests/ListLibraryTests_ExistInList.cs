using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace MPT.Lists.UnitTests
{
    [TestFixture]
    public class ListLibraryTests_ExistInList
    {

        private readonly List<string> _searchListString = new List<string> { "Foo", "Bar", "Moo", "Nar" };
        private readonly List<string> _searchListStringOtherCase = new List<string> { "foo", "bar", "moo", "nar" };
        private readonly List<string> _searchListStringDifferentNumber = new List<string> { "Foo", "Bar", "Moo", "Nar", "Car", "Far" };
        private readonly List<string> _searchListStringDifferentItems = new List<string> { "Foo", "Fie", "Foe", "Nar" };
        private readonly List<string> _searchListStringDifferentOrder = new List<string> { "Moo", "Foo", "Nar", "Bar" };
        private readonly Collection<string> _searchCollectionString = new Collection<string> { "Foo", "Bar", "Moo", "Nar" };
        private readonly Collection<string> _searchCollectionStringDifferentItems = new Collection<string> { "Foo", "Fie", "Foe", "Nar" };
        private readonly ObservableCollection<string> _searchObservableCollectionString = new ObservableCollection<string> { "Foo", "Bar", "Moo", "Nar" };
        private readonly ObservableCollection<string> _searchObservableCollectionStringDifferentItems = new ObservableCollection<string> { "Foo", "Fie", "Foe", "Nar" };

        private readonly List<int> _searchListInteger = new List<int> { 5, 99, 321, -1 };
        private readonly List<int> _searchListIntegerDifferentOrder = new List<int> { -1, 99, 5, 321 };
        private readonly List<int> _searchListIntegerDifferentNumber = new List<int> { -1, 99, 5, 321, 666, 999 };
        private readonly List<int> _searchListIntegerDifferentItems = new List<int> { 12, 99, 5, 321 };
        private readonly Collection<int> _searchCollectionInteger = new Collection<int> { 5, 99, 321, -1 };
        private readonly ObservableCollection<int> _searchObservableCollectionInteger = new ObservableCollection<int> { 5, 99, 321, -1 };

        private readonly List<double> _searchListDouble = new List<double> { 5.5, 99.00042, 321.2, -1.99 };
        private readonly List<double> _searchListDoubleTolerance = new List<double> { 5.5, 99.00021, 321.2, -1.99 };
        private readonly List<double> _searchListDoubleDifferentOrder = new List<double> { 321.2, 5.5, -1.99, 99.00042 };
        private readonly List<double> _searchListDoubleDifferentNumber = new List<double> { 321.2, 5.5, -1.99, 99.00042, 42.5 };
        private readonly List<double> _searchListDoubleDifferentItems = new List<double> { 321.0, 5.5, -1.99, 99.00042 };

        [TestCase("Foo", ExpectedResult = true)]
        [TestCase("Moo", ExpectedResult = true)]
        [TestCase("moo", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ExistsInList_of_String_List_Case_Sensitive(string entry)
        {
            return ListLibrary.ExistsInList(entry, _searchListString, caseSensitive: true);
        }

        [TestCase("Foo", ExpectedResult = true)]
        [TestCase("Moo", ExpectedResult = true)]
        [TestCase("moo", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ExistsInList_of_String_Collection_Case_Sensitive(string entry)
        {
            return ListLibrary.ExistsInList(entry, _searchCollectionString, caseSensitive: true);
        }

        [TestCase("Foo", ExpectedResult = true)]
        [TestCase("Moo", ExpectedResult = true)]
        [TestCase("moo", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ExistsInList_of_String_ObservableCollection_Case_Sensitive(string entry)
        {
            return ListLibrary.ExistsInList(entry, _searchObservableCollectionString, caseSensitive: true);
        }

        [TestCase("Foo", ExpectedResult = false)]
        [TestCase("Moo", ExpectedResult = false)]
        [TestCase("moo", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ExistsInList_of_String_Null_List_Case_Sensitive(string entry)
        {
            return ListLibrary.ExistsInList(entry, null, caseSensitive: true);
        }

        [TestCase("Foo", ExpectedResult = true)]
        [TestCase("Moo", ExpectedResult = true)]
        [TestCase("moo", ExpectedResult = true)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ExistsInList_of_String_List_Case_Insensitive(string entry)
        {
            return ListLibrary.ExistsInList(entry, _searchListString);
        }

        [TestCase(5, ExpectedResult = true)]
        [TestCase(321, ExpectedResult = true)]
        [TestCase(-1, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ExistsInList_of_Integer_List(int value)
        {
            return ListLibrary.ExistsInList(value, _searchListInteger);
        }

        [TestCase(5, ExpectedResult = true)]
        [TestCase(321, ExpectedResult = true)]
        [TestCase(-1, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ExistsInList_of_Integer_Collection(int value)
        {
            return ListLibrary.ExistsInList(value, _searchCollectionInteger);
        }

        [TestCase(5, ExpectedResult = true)]
        [TestCase(321, ExpectedResult = true)]
        [TestCase(-1, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ExistsInList_of_Integer_ObservableCollection(int value)
        {
            return ListLibrary.ExistsInList(value, _searchObservableCollectionInteger);
        }

        [TestCase(5, ExpectedResult = false)]
        [TestCase(321, ExpectedResult = false)]
        [TestCase(-1, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ExistsInList_of_Integer_Null_List(int value)
        {
            return ListLibrary.ExistsInList(value, null);
        }


        [Test]
        public void ListsAreDifferent_Defaults_of_String_Of_Same_Lists_Returns_False()
        {
            Assert.IsFalse(ListLibrary.ListsAreDifferent(_searchListString, _searchListString));
        }

        [Test]
        public void ListsAreDifferent_Defaults_of_String_Of_Different_Items_Lists_Returns_True()
        {
            Assert.IsTrue(ListLibrary.ListsAreDifferent(_searchListString, _searchListStringDifferentItems));
        }

        [Test]
        public void ListsAreDifferent_Defaults_of_String_Of_Different_Capitalization_Lists_Returns_False()
        {
            Assert.IsFalse(ListLibrary.ListsAreDifferent(_searchListString, _searchListStringOtherCase));
        }

        [Test]
        public void ListsAreDifferent_Defaults_of_String_Of_Different_Order_Lists_Returns_False()
        {
            Assert.IsFalse(ListLibrary.ListsAreDifferent(_searchListString, _searchListStringDifferentOrder));
        }

        [TestCase(true, ExpectedResult = false)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_String_Of_Same_Lists_Different_Capitalization(bool caseSensitive)
        {
            return ListLibrary.ListsAreDifferent(_searchListString, _searchListString, caseSensitive: caseSensitive);
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_String_Of_Different_Capitalized_Lists_Case_Sensitive(bool caseSensitive)
        {
            return ListLibrary.ListsAreDifferent(_searchListString, _searchListStringOtherCase, caseSensitive: caseSensitive);
        }

        [TestCase(true, ExpectedResult = false)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_String_Of_Same_Lists_Same_Order(bool considerOrder)
        {
            return ListLibrary.ListsAreDifferent(_searchListString, _searchListString, considerOrder: considerOrder);
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_String_Of_Same_Lists_Different_Order(bool considerOrder)
        {
            return ListLibrary.ListsAreDifferent(_searchListString, _searchListStringDifferentOrder, considerOrder: considerOrder);
        }

        [TestCase(true, true, ExpectedResult = true)]
        [TestCase(true, false, ExpectedResult = true)]
        [TestCase(false, false, ExpectedResult = true)]
        [TestCase(false, true, ExpectedResult = true)]
        public bool ListsAreDifferent_of_String_Of_Different_Count_Lists_Returns_True(bool considerOrder, bool considerCapitalization)
        {
            return (ListLibrary.ListsAreDifferent(_searchListString, _searchListStringDifferentNumber));
        }

        [TestCase(true, true, ExpectedResult = true)]
        [TestCase(true, false, ExpectedResult = true)]
        [TestCase(false, false, ExpectedResult = true)]
        [TestCase(false, true, ExpectedResult = true)]
        public bool ListsAreDifferent_of_String_Of_Different_Items_Lists_Returns_True(bool considerOrder, bool considerCapitalization)
        {
            return (ListLibrary.ListsAreDifferent(_searchListString, _searchListStringDifferentItems));
        }

        [TestCase(true, true, ExpectedResult = true)]
        [TestCase(true, false, ExpectedResult = true)]
        [TestCase(false, false, ExpectedResult = true)]
        [TestCase(false, true, ExpectedResult = true)]
        public bool ListsAreDifferent_of_String_Of_Inner_Null_List_Returns_True(bool considerOrder, bool considerCapitalization)
        {
            return (ListLibrary.ListsAreDifferent(null, _searchListString));
        }

        [TestCase(true, true, ExpectedResult = true)]
        [TestCase(true, false, ExpectedResult = true)]
        [TestCase(false, false, ExpectedResult = true)]
        [TestCase(false, true, ExpectedResult = true)]
        public bool ListsAreDifferent_of_String_Of_Outer_Null_List_Returns_True(bool considerOrder, bool considerCapitalization)
        {
            return (ListLibrary.ListsAreDifferent(_searchListString, null));
        }

        [Test]
        public void ListsAreDifferent_Defaults_of_String_Of_Same_Lists_And_Collections_Returns_False()
        {
            Assert.IsFalse(ListLibrary.ListsAreDifferent(_searchListString, _searchCollectionString));
        }

        [Test]
        public void ListsAreDifferent_Defaults_of_String_Of_Different_Items_Lists_And_Collections_Returns_True()
        {
            Assert.IsTrue(ListLibrary.ListsAreDifferent(_searchListString, _searchCollectionStringDifferentItems));
        }

        [Test]
        public void ListsAreDifferent_Defaults_of_String_Of_Same_Lists_And_Observable_Collections_Returns_False()
        {
            Assert.IsFalse(ListLibrary.ListsAreDifferent(_searchListString, _searchObservableCollectionString));
        }

        [Test]
        public void ListsAreDifferent_Defaults_of_String_Of_Different_Items_Lists_And_Observable_Collections_Returns_True()
        {
            Assert.IsTrue(ListLibrary.ListsAreDifferent(_searchListString, _searchObservableCollectionStringDifferentItems));
        }


        [Test]
        public void ListsAreDifferent_Defaults_of_Double_Of_Same_Lists_Returns_False()
        {
            Assert.IsFalse(ListLibrary.ListsAreDifferent(_searchListDouble, _searchListDouble));
        }

        [Test]
        public void ListsAreDifferent_Defaults_of_Double_Of_Different_Items_Lists_Returns_True()
        {
            Assert.IsTrue(ListLibrary.ListsAreDifferent(_searchListDouble, _searchListDoubleDifferentItems));
        }

        [TestCase(true, ExpectedResult = false)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_Double_Of_Same_Lists_Same_Order(bool considerOrder)
        {
            return ListLibrary.ListsAreDifferent(_searchListDouble, _searchListDouble, considerOrder: considerOrder);
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_Double_Of_Same_Lists_Different_Order(bool considerOrder)
        {
            return ListLibrary.ListsAreDifferent(_searchListDouble, _searchListDoubleDifferentOrder, considerOrder: considerOrder);
        }


        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = true)]
        public bool ListsAreDifferent_of_Double_Of_Different_Count_Lists_Returns_True(bool considerOrder)
        {
            return (ListLibrary.ListsAreDifferent(_searchListDouble, _searchListDoubleDifferentNumber, considerOrder: considerOrder));
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = true)]
        public bool ListsAreDifferent_of_Double_Of_Different_Items_Lists_Returns_True(bool considerOrder)
        {
            return (ListLibrary.ListsAreDifferent(_searchListDouble, _searchListDoubleDifferentItems, considerOrder: considerOrder));
        }


        [TestCase(true, ExpectedResult = false)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_Double_Of_Same_Items_Lists_In_Tolerance_Returns_True(bool considerOrder)
        {
            double tolerance = 1E-3;
            return (ListLibrary.ListsAreDifferent(_searchListDouble, _searchListDouble, considerOrder: considerOrder, tolerance: tolerance));
        }

        [TestCase(true, ExpectedResult = false)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_Double_Of_Different_Items_Lists_In_Tolerance_Returns_True(bool considerOrder)
        {
            double tolerance = 1E-3;
            return (ListLibrary.ListsAreDifferent(_searchListDouble, _searchListDoubleTolerance, considerOrder: considerOrder, tolerance: tolerance));
        }

        [TestCase(true, ExpectedResult = false)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_Double_Of_Same_Items_Lists_Outside_Tolerance_Returns_True(bool considerOrder)
        {
            double tolerance = 1E-4;
            return (ListLibrary.ListsAreDifferent(_searchListDouble, _searchListDouble, considerOrder: considerOrder, tolerance: tolerance));
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = true)]
        public bool ListsAreDifferent_of_Double_Of_Different_Items_Outside_In_Tolerance_Returns_True(bool considerOrder)
        {
            double tolerance = 1E-4;
            return (ListLibrary.ListsAreDifferent(_searchListDouble, _searchListDoubleTolerance, considerOrder: considerOrder, tolerance: tolerance));
        }


        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = true)]
        public bool ListsAreDifferent_of_Double_Of_Inner_Null_List_Returns_True(bool considerOrder)
        {
            return (ListLibrary.ListsAreDifferent(null, _searchListDouble, considerOrder: considerOrder));
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = true)]
        public bool ListsAreDifferent_of_Double_Of_Outer_Null_List_Returns_True(bool considerOrder)
        {
            return (ListLibrary.ListsAreDifferent(_searchListDouble, null, considerOrder: considerOrder));
        }


        [Test]
        public void ListsAreDifferent_Defaults_of_Integer_Of_Same_Lists_Returns_False()
        {
            Assert.IsFalse(ListLibrary.ListsAreDifferent(_searchListInteger, _searchListInteger));
        }

        [Test]
        public void ListsAreDifferent_Defaults_of_Integer_Of_Different_Items_Lists_Returns_True()
        {
            Assert.IsTrue(ListLibrary.ListsAreDifferent(_searchListInteger, _searchListIntegerDifferentItems));
        }

        [TestCase(true, ExpectedResult = false)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_Integer_Of_Same_Lists_Same_Order(bool considerOrder)
        {
            return ListLibrary.ListsAreDifferent(_searchListInteger, _searchListInteger, considerOrder: considerOrder);
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = false)]
        public bool ListsAreDifferent_of_Integer_Of_Same_Lists_Different_Order(bool considerOrder)
        {
            return ListLibrary.ListsAreDifferent(_searchListInteger, _searchListIntegerDifferentOrder, considerOrder: considerOrder);
        }


        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = true)]
        public bool ListsAreDifferent_of_Integer_Of_Different_Count_Lists_Returns_True(bool considerOrder)
        {
            return (ListLibrary.ListsAreDifferent(_searchListInteger, _searchListIntegerDifferentNumber, considerOrder: considerOrder));
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = true)]
        public bool ListsAreDifferent_of_Integer_Of_Different_Items_Lists_Returns_True(bool considerOrder)
        {
            return (ListLibrary.ListsAreDifferent(_searchListInteger, _searchListIntegerDifferentItems, considerOrder: considerOrder));
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = true)]
        public bool ListsAreDifferent_of_Integer_Of_Inner_Null_List_Returns_True(bool considerOrder)
        {
            return (ListLibrary.ListsAreDifferent(null, _searchListInteger, considerOrder: considerOrder));
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = true)]
        public bool ListsAreDifferent_of_Integer_Of_Outer_Null_List_Returns_True(bool considerOrder)
        {
            return (ListLibrary.ListsAreDifferent(_searchListInteger, null, considerOrder: considerOrder));
        }
    }
}
