using System;
using System.Runtime.InteropServices;
using System.Windows;

using NUnit.Framework;

namespace MPT.Verification.UnitTests
{
    [TestFixture]
    public class StructVerifyTests
    {
        [Test]
        public void PointIsEmpty_Is_Empty()
        {
            Point pointChecked = new Point();

            Assert.IsTrue(StructVerify.PointIsEmpty(pointChecked));
        }

        [Test]
        public void PointIsEmpty_Is_Partially_Empty_X()
        {
            Point pointChecked = new Point();
            pointChecked.X = 5;

            Assert.IsFalse(StructVerify.PointIsEmpty(pointChecked));
        }

        [Test]
        public void PointIsEmpty_Is_Partially_Empty_Y()
        {
            Point pointChecked = new Point();
            pointChecked.Y = 5;

            Assert.IsFalse(StructVerify.PointIsEmpty(pointChecked));
        }

        [Test]
        public void PointIsEmpty_Is_Not_Empty()
        {
            Point pointChecked = new Point();
            pointChecked.X = 5;
            pointChecked.Y = -4;

            Assert.IsFalse(StructVerify.PointIsEmpty(pointChecked));
        }

        [Test]
        public void IntPtrIsEmpty_Is_Empty()
        {
            IntPtr pointChecked = new IntPtr();

            Assert.IsTrue(StructVerify.IntPtrIsEmpty(pointChecked));
        }

        [Test]
        public void IntPtrIsEmpty_Is_Not_Empty()
        {
            IntPtr pointChecked = Marshal.StringToHGlobalAnsi("Foobar"); ;

            Assert.IsFalse(StructVerify.IntPtrIsEmpty(pointChecked));
        }
    }
}
