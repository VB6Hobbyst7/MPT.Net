using System;
using NUnit.Framework;

namespace MPT.Reporting.UnitTests
{
    [TestFixture]
    public class MessengerEventArgsTests
    {
        eMessageActions defaultAction = eMessageActions.None;
        eMessageActions defaultActionDefault = eMessageActions.None;
        eMessageActionSets defaultActionSet = eMessageActionSets.OkOnly;
        eMessageType defaultMessageType = eMessageType.None;

        private bool defaultHandled = false;

        [Test]
        public void Initialize_With_Message_Contains_Message_And_Defaults()
        {
            string expectedMessage = "A new message";

            MessengerEventArgs messengerEventArg = new MessengerEventArgs(expectedMessage);

            Assert.AreEqual(expectedMessage, messengerEventArg.Message);

            Assert.AreEqual("", messengerEventArg.Title);
            Assert.AreEqual("", messengerEventArg.Footer);
            Assert.AreEqual("", messengerEventArg.PromptList);
            Assert.AreEqual(defaultAction, messengerEventArg.Action);
            Assert.AreEqual(defaultActionDefault, messengerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, messengerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, messengerEventArg.MessageType);
            Assert.AreEqual(defaultHandled, messengerEventArg.Handled);
        }

        [Test]
        public void Initialize_With_Message_And_ParamArray_Contains_Message_Formatted_By_ParamArray()
        {
            string baseMessage = "A new message{0}with a new value of {1}";
            object[] argArray = { @"/n" , 42};
            string expectedMessage = string.Format(baseMessage, argArray);

            MessengerEventArgs messengerEventArg = new MessengerEventArgs(baseMessage, argArray);

            Assert.AreEqual(expectedMessage, messengerEventArg.Message);

            Assert.AreEqual("", messengerEventArg.Title);
            Assert.AreEqual("", messengerEventArg.Footer);
            Assert.AreEqual("", messengerEventArg.PromptList);
            Assert.AreEqual(defaultAction, messengerEventArg.Action);
            Assert.AreEqual(defaultActionDefault, messengerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, messengerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, messengerEventArg.MessageType);
            Assert.AreEqual(defaultHandled, messengerEventArg.Handled);
        }

        [Test]
        public void Initialize_With_Title_And_Message_And_ParamArray_Contains_Title_And_Message_Formatted_By_ParamArray()
        {
            string expectedTitle = "A new title";
            string baseMessage = "A new message{0}with a new value of {1}";
            object[] argArray = { @"/n", 42 };
            string expectedMessage = string.Format(baseMessage, argArray);

            MessengerEventArgs messengerEventArg = new MessengerEventArgs(expectedTitle, baseMessage, argArray);

            Assert.AreEqual(expectedTitle, messengerEventArg.Title);
            Assert.AreEqual(expectedMessage, messengerEventArg.Message);

            Assert.AreEqual("", messengerEventArg.Footer);
            Assert.AreEqual("", messengerEventArg.PromptList);
            Assert.AreEqual(defaultAction, messengerEventArg.Action);
            Assert.AreEqual(defaultActionDefault, messengerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, messengerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, messengerEventArg.MessageType);
            Assert.AreEqual(defaultHandled, messengerEventArg.Handled);
        }

        [Test]
        public void Initialize_With_Message_Details_And_Message_And_ParamArray_Contains_Message_Details_Properties_And_Message_Formatted_By_ParamArray()
        {
            eMessageActionSets expectedActionSet = eMessageActionSets.YesNoCancel;
            eMessageType expectedMessageType = eMessageType.Warning;
            eMessageActions expectedActionDefault = eMessageActions.Cancel;
            
            MessageDetails messageDetails = new MessageDetails(expectedActionSet, expectedMessageType, expectedActionDefault);

            string baseMessage = "A new message{0}with a new value of {1}";
            object[] argArray = { @"/n", 42 };
            string expectedMessage = string.Format(baseMessage, argArray);

            MessengerEventArgs messengerEventArg = new MessengerEventArgs(messageDetails, baseMessage, argArray);

            Assert.AreEqual(expectedMessage, messengerEventArg.Message);
            Assert.AreEqual(expectedActionDefault, messengerEventArg.ActionDefault);
            Assert.AreEqual(expectedActionSet, messengerEventArg.ActionSet);
            Assert.AreEqual(expectedMessageType, messengerEventArg.MessageType);

            Assert.AreEqual("", messengerEventArg.Title);
            Assert.AreEqual("", messengerEventArg.Footer);
            Assert.AreEqual("", messengerEventArg.PromptList);
            Assert.AreEqual(defaultAction, messengerEventArg.Action);
            Assert.AreEqual(defaultHandled, messengerEventArg.Handled);
        }

        [Test]
        public void Initialize_With_Message_Details_And_Data_And_ParamArray_Contains_Message_Details_And_Data_Properties_And_Message_Formatted_By_ParamArray()
        {
            eMessageActionSets expectedActionSet = eMessageActionSets.YesNoCancel;
            eMessageType expectedMessageType = eMessageType.Warning;
            eMessageActions expectedActionDefault = eMessageActions.Cancel;

            MessageDetails messageDetails = new MessageDetails(expectedActionSet, expectedMessageType, expectedActionDefault);

            string baseMessage = "A new message{0}with a new value of {1}";
            object[] argArray = { @"/n", 42 };
            string expectedMessage = string.Format(baseMessage, argArray);

            string expectedTitle = "A new title";
            string expectedFooter = "A new footer";
            string expectedPromptList = "A new prompt list";

            MessageData messageData = new MessageData(expectedTitle, baseMessage, expectedFooter, expectedPromptList);

            MessengerEventArgs messengerEventArg = new MessengerEventArgs(messageDetails, messageData, argArray);

            Assert.AreEqual(expectedTitle, messengerEventArg.Title);
            Assert.AreEqual(expectedMessage, messengerEventArg.Message);
            Assert.AreEqual(expectedFooter, messengerEventArg.Footer);
            Assert.AreEqual(expectedPromptList, messengerEventArg.PromptList);
            Assert.AreEqual(expectedActionDefault, messengerEventArg.ActionDefault);
            Assert.AreEqual(expectedActionSet, messengerEventArg.ActionSet);
            Assert.AreEqual(expectedMessageType, messengerEventArg.MessageType);

            Assert.AreEqual(defaultAction, messengerEventArg.Action);
            Assert.AreEqual(defaultHandled, messengerEventArg.Handled);
        }

        [Test]
        public void Set_Property_Expected_Action_Sets_Property()
        {
            MessengerEventArgs messengerEventArg = new MessengerEventArgs("");

            eMessageActions expectedAction = eMessageActions.Retry;
            messengerEventArg.Action = expectedAction;

            Assert.AreEqual(expectedAction, messengerEventArg.Action);

            Assert.AreEqual("", messengerEventArg.Title);
            Assert.AreEqual("", messengerEventArg.Message);
            Assert.AreEqual("", messengerEventArg.Footer);
            Assert.AreEqual("", messengerEventArg.PromptList);
            Assert.AreEqual(defaultActionDefault, messengerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, messengerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, messengerEventArg.MessageType);
            Assert.AreEqual(defaultHandled, messengerEventArg.Handled);
        }

        [Test]
        public void Set_Property_Handled_Allows_Later_Listeners_To_Ignore_Event()
        {
            MessengerEventArgs messengerEventArg = new MessengerEventArgs("");

            // Throw event arg

            // Catch event arg , assert Handled state, and set Handled

            Assert.AreEqual(defaultHandled, messengerEventArg.Handled);

            // Catch event again and assert new Handled state
            bool expectedIsHandled = true;
            Assert.AreEqual(expectedIsHandled, messengerEventArg.Handled);
            
            Assert.AreEqual("", messengerEventArg.Title);
            Assert.AreEqual("", messengerEventArg.Message);
            Assert.AreEqual("", messengerEventArg.Footer);
            Assert.AreEqual("", messengerEventArg.PromptList);
            Assert.AreEqual(defaultAction, messengerEventArg.Action);
            Assert.AreEqual(defaultActionDefault, messengerEventArg.ActionDefault);
            Assert.AreEqual(defaultActionSet, messengerEventArg.ActionSet);
            Assert.AreEqual(defaultMessageType, messengerEventArg.MessageType);
        }
    }
}
