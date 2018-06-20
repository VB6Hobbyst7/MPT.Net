using System;
using System.Windows.Input;

using NUnit.Framework;

using MPT.Cursors;

namespace MPT.Cursors.UnitTests
{
    [TestFixture]
    public class WaitCursorTests
    {
        [SetUp]
        public void ResetCursorStart()
        {
            //Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        [TearDown]
        public void ResetCursorEnd()
        {
            //Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        [Test]
        public void New_With_Start_Cursor_Begins_Cursor()
        {
            bool waitCursorActive = true;
            cCursorWait waitCursor = new cCursorWait();
            Assert.AreEqual(waitCursorActive, waitCursor.Active);
        }

        [Test]
        public void New_With_Explicit_Start_Cursor_Begins_Cursor()
        {
            bool waitCursorActive = true;
            cCursorWait waitCursor = new cCursorWait(true);
            Assert.AreEqual(waitCursorActive, waitCursor.Active);
        }

        [Test]
        public void New_Without_Start_Cursor_Does_Not_Begin_Cursor()
        {
            bool waitCursorActive = false;
            cCursorWait waitCursor = new cCursorWait(false);
            Assert.AreEqual(waitCursorActive, waitCursor.Active);
        }

        [Test]
        public void Start_Cursor_When_Active_Does_Nothing()
        {
            bool waitCursorActive = true;
            cCursorWait waitCursor = new cCursorWait();
            waitCursor.StartCursor();
            Assert.AreEqual(waitCursorActive, waitCursor.Active);
        }

        [Test]
        public void Start_Cursor_When_Inactive_Begins_Cursor()
        {
            bool waitCursorActive = true;
            cCursorWait waitCursor = new cCursorWait(false);
            waitCursor.StartCursor();
            Assert.AreEqual(waitCursorActive, waitCursor.Active);
        }

        [Test]
        public void End_Cursor_When_Active_Stops_Cursor()
        {
            bool waitCursorActive = true;
            cCursorWait waitCursor = new cCursorWait();
            waitCursor.StartCursor();
            waitCursor.EndCursor();
            Assert.AreEqual(waitCursorActive, waitCursor.Active);
        }

        [Test]
        public void End_Cursor_When_Inactive_Does_Nothing()
        {
            bool waitCursorActive = true;
            cCursorWait waitCursor = new cCursorWait(false);
            waitCursor.EndCursor();
            Assert.AreEqual(waitCursorActive, waitCursor.Active);
        }
    }
}
