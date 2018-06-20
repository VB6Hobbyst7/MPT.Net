using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace MPT.Reporting.UnitTests
{
    [TestFixture]
    public class LoggerEventArgsTests
    {
        eMessageActions defaultAction = eMessageActions.None;
        eMessageActions defaultDefaultAction = eMessageActions.None;
        eMessageActionSets defaultActionSet = eMessageActionSets.OkOnly;
        eMessageType defaultMessageType = eMessageType.None;

        private bool defaultHandled = false;


        [Test]
        public void Initialize_With_Message_Contains_Message_And_Defaults()
        {
            string expectedMessage = "A new message";

            LoggerEventArgs loggerEventArg = new LoggerEventArgs(expectedMessage);

            Assert.AreEqual(expectedMessage, loggerEventArg.Message);

            Assert.AreEqual("", loggerEventArg.Title);
            Assert.AreEqual("", loggerEventArg.Footer);
            Assert.AreEqual("", loggerEventArg.PromptList);
            Assert.AreEqual(defaultAction, loggerEventArg.Action);
            Assert.AreEqual(defaultDefaultAction, loggerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, loggerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, loggerEventArg.MessageType);
            Assert.AreEqual(defaultHandled, loggerEventArg.Handled);
            Assert.AreEqual(null, loggerEventArg.Exception);
            Assert.AreEqual("", loggerEventArg.Parameters);
        }

        [Test]
        public void Initialize_With_Message_And_Parameters_Contains_Message_Parameters_And_Defaults()
        {
            string expectedMessage = "A new message";

            string key1 = "Key1:";
            string value1 = "Value1";
            string key2 = "Key2:";
            string value2 = "Value2";

            LoggerEventArgs loggerEventArg = new LoggerEventArgs(expectedMessage,
                                                                  key1, value1,
                                                                  key2, value2);

            Assert.AreEqual(expectedMessage, loggerEventArg.Message);
            Assert.IsTrue(loggerEventArg.Parameters.ContainsKey(key1));
            Assert.AreEqual(value1, loggerEventArg.Parameters[key1]);
            Assert.IsTrue(loggerEventArg.Parameters.ContainsKey(key2));
            Assert.AreEqual(value2, loggerEventArg.Parameters[key2]);

            Assert.AreEqual("", loggerEventArg.Title);
            Assert.AreEqual("", loggerEventArg.Footer);
            Assert.AreEqual("", loggerEventArg.PromptList);
            Assert.AreEqual(defaultAction, loggerEventArg.Action);
            Assert.AreEqual(defaultDefaultAction, loggerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, loggerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, loggerEventArg.MessageType);
            Assert.AreEqual(defaultHandled, loggerEventArg.Handled);
            Assert.AreEqual(null, loggerEventArg.Exception);
        }

        [Test]
        public void Initialize_With_Exception_Contains_Message_And_Exception_And_Defaults()
        {
            Exception expectedException = new Exception();

            LoggerEventArgs loggerEventArg = new LoggerEventArgs(expectedException);

            Assert.AreEqual(expectedException.Message, loggerEventArg.Message);
            Assert.AreEqual(expectedException, loggerEventArg.Exception);

            Assert.AreEqual("", loggerEventArg.Title);
            Assert.AreEqual("", loggerEventArg.Footer);
            Assert.AreEqual("", loggerEventArg.PromptList);
            Assert.AreEqual(defaultAction, loggerEventArg.Action);
            Assert.AreEqual(defaultDefaultAction, loggerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, loggerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, loggerEventArg.MessageType);
            Assert.AreEqual(defaultHandled, loggerEventArg.Handled);
            Assert.AreEqual("", loggerEventArg.Parameters);
        }

        [Test]
        public void Initialize_With_Exception_And_Parameters_Contain_Message_And_Exception_And_Parameters_And_Defaults()
        {
            string key1 = "Key1:";
            string value1 = "Value1";
            string key2 = "Key2:";
            string value2 = "Value2";

            Exception expectedException = new Exception();

            LoggerEventArgs loggerEventArg = new LoggerEventArgs(expectedException,
                                                                  key1, value1,
                                                                  key2, value2);

            Assert.AreEqual(expectedException.Message, loggerEventArg.Message);
            Assert.AreEqual(expectedException, loggerEventArg.Exception);
            Assert.IsTrue(loggerEventArg.Parameters.ContainsKey(key1));
            Assert.AreEqual(value1, loggerEventArg.Parameters[key1]);
            Assert.IsTrue(loggerEventArg.Parameters.ContainsKey(key2));
            Assert.AreEqual(value2, loggerEventArg.Parameters[key2]);

            Assert.AreEqual("", loggerEventArg.Title);
            Assert.AreEqual("", loggerEventArg.Footer);
            Assert.AreEqual("", loggerEventArg.PromptList);
            Assert.AreEqual(defaultAction, loggerEventArg.Action);
            Assert.AreEqual(defaultDefaultAction, loggerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, loggerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, loggerEventArg.MessageType);
            Assert.AreEqual(defaultHandled, loggerEventArg.Handled);
        }

        [Test]
        public void Initialize_With_Odd_Number_Of_Parameters_Does_Not_Store_Parameters()
        {
            string key1 = "Key1:";
            string value1 = "Value1";
            string key2 = "Key2:";
            string value2 = "Value2";

            Exception expectedException = new Exception();

            LoggerEventArgs loggerEventArg = new LoggerEventArgs(expectedException,
                                                                  key1, value1,
                                                                  key2);

            Assert.AreEqual(expectedException.Message, loggerEventArg.Message);
            Assert.AreEqual(expectedException, loggerEventArg.Exception);
            Assert.IsTrue(loggerEventArg.Parameters.ContainsKey(key1));
            Assert.AreEqual(value1, loggerEventArg.Parameters[key1]);
            Assert.IsFalse(loggerEventArg.Parameters.ContainsKey(key2));

            Assert.AreEqual("", loggerEventArg.Title);
            Assert.AreEqual("", loggerEventArg.Footer);
            Assert.AreEqual("", loggerEventArg.PromptList);
            Assert.AreEqual(defaultAction, loggerEventArg.Action);
            Assert.AreEqual(defaultDefaultAction, loggerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, loggerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, loggerEventArg.MessageType);
            Assert.AreEqual(defaultHandled, loggerEventArg.Handled);
        }

        [Test]
        public void Initialize_With_Parameters_Containing_Lists_Stores_Lists()
        {
            string key1 = "Key1:";
            string value1 = "Value1";
            string key2 = "Key2:";
            string item1 = "Item 1";
            string key2Item1 = key2 + "(0)";
            string item2 = "Item 2";
            string key2Item2 = key2 + "(1)";
            List<string> value2 = new List<string> { item1, item2 };

            Exception expectedException = new Exception();

            LoggerEventArgs loggerEventArg = new LoggerEventArgs(expectedException,
                                                                  key1, value1,
                                                                  key2, value2);

            Assert.AreEqual(expectedException.Message, loggerEventArg.Message);
            Assert.AreEqual(expectedException, loggerEventArg.Exception);
            Assert.IsTrue(loggerEventArg.Parameters.ContainsKey(key1));
            Assert.AreEqual(value1, loggerEventArg.Parameters[key1]);
            Assert.IsTrue(loggerEventArg.Parameters.ContainsKey(key2Item1));
            Assert.AreEqual(item1, loggerEventArg.Parameters[key2Item1]);
            Assert.IsTrue(loggerEventArg.Parameters.ContainsKey(key2Item2));
            Assert.AreEqual(item2, loggerEventArg.Parameters[key2Item2]);

            Assert.AreEqual("", loggerEventArg.Title);
            Assert.AreEqual("", loggerEventArg.Footer);
            Assert.AreEqual("", loggerEventArg.PromptList);
            Assert.AreEqual(defaultAction, loggerEventArg.Action);
            Assert.AreEqual(defaultDefaultAction, loggerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, loggerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, loggerEventArg.MessageType);
            Assert.AreEqual(defaultHandled, loggerEventArg.Handled);
        }
    }
}
