using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MPT.Verification.UnitTests
{
    [TestFixture]
    public class ObjectVerifyTests
    {
        [Test]
        public void IsValidObject_Null_Is_Invalid()
        {
            object testObject = null;
            Assert.IsFalse(ObjectVerify.IsValidObject(testObject));
        }

        [Test]
        public void IsValidObject_Not_Null_Is_Valid()
        {
            List<string> testObject = new List<string>();
            Assert.IsTrue(ObjectVerify.IsValidObject(testObject));
        }

        [Test]
        public void IsValidObjectDB_Null_Is_Invalid()
        {
            object testObject = null;
            Assert.IsFalse(ObjectVerify.IsValidObjectDB(testObject));
        }

        [Test]
        public void IsValidObjectDB_DBNull_Is_Invalid()
        {
            Assert.IsFalse(ObjectVerify.IsValidObjectDB(DBNull.Value));
        }

        [Test]
        public void IsValidObjectDB_Not_Null_And_DBNull_Is_Valid()
        {
            List<string> testObject = new List<string>();
            Assert.IsTrue(ObjectVerify.IsValidObjectDB(testObject));
        }

        [Test]
        public void IsValidObjectDBStringFilled_Null_Is_Invalid()
        {
            object testObject = null;
            Assert.IsFalse(ObjectVerify.IsValidObjectDBStringFilled(testObject));
        }

        [Test]
        public void IsValidObjectDBStringFilled_DBNull_Is_Invalid()
        {
            Assert.IsFalse(ObjectVerify.IsValidObjectDBStringFilled(DBNull.Value));
        }

        [Test]
        public void IsValidObjectDBStringFilled_String_Empty_Is_Invalid()
        {
            string stringEmpty = "";
            Assert.IsFalse(ObjectVerify.IsValidObjectDBStringFilled(stringEmpty));
        }

        [Test]
        public void IsValidObjectDBStringFilled_String_Filled_Is_Valid()
        {
            string stringFilled = "Foobar";
            Assert.IsTrue(ObjectVerify.IsValidObjectDBStringFilled(stringFilled));
        }

        [Test]
        public void IsValidObjectDBStringFilled_Not_Null_And_DBNull_Is_Valid()
        {
            List<string> testObject = new List<string>();
            Assert.IsTrue(ObjectVerify.IsValidObjectDBStringFilled(testObject));
        }

        [Test]
        public void IsValidObjectDBStringEmpty_Null_Is_Invalid()
        {
            object testObject = null;
            Assert.IsFalse(ObjectVerify.IsValidObjectDBStringEmpty(testObject));
        }

        [Test]
        public void IsValidObjectDBStringEmpty_DBNull_Is_Invalid()
        {
            Assert.IsFalse(ObjectVerify.IsValidObjectDBStringEmpty(DBNull.Value));
        }

        [Test]
        public void IsValidObjectDBStringEmpty_String_Empty_Is_Invalid()
        {
            string stringEmpty = "";
            Assert.IsTrue(ObjectVerify.IsValidObjectDBStringEmpty(stringEmpty));
        }

        [Test]
        public void IsValidObjectDBStringEmpty_String_Filled_Is_Valid()
        {
            string stringFilled = "Foobar";
            Assert.IsFalse(ObjectVerify.IsValidObjectDBStringEmpty(stringFilled));
        }

        [Test]
        public void IsValidObjectDBStringEmpty_Not_Null_And_DBNull_Is_Valid()
        {
            List<string> testObject = new List<string>();
            Assert.IsFalse(ObjectVerify.IsValidObjectDBStringEmpty(testObject));
        }
    }
}
