using NUnit.Framework;

namespace MPT.Reporting.UnitTests
{
    [TestFixture]
    public class MessageDataTests
    {
        string title = "A new title";
        string message = "A new message";
        string footer = "A new footer";
        string promptList = "A new prompt list";

        [Test]
        public void InitializeWithOptionalProperties_None_Contains_None()
        {
            MessageData messageData = new MessageData();

            Assert.AreEqual("", messageData.Title);
            Assert.AreEqual("", messageData.Message);
            Assert.AreEqual("", messageData.Footer);
            Assert.AreEqual("", messageData.PromptList);
        }

        [Test]
        public void InitializeWithOptionalProperties_Title_Contains_Title()
        {
            MessageData messageData = new MessageData(title);

            Assert.AreEqual(title, messageData.Title);
            Assert.AreEqual("", messageData.Message);
            Assert.AreEqual("", messageData.Footer);
            Assert.AreEqual("", messageData.PromptList);
        }

        [Test]
        public void InitializeWithOptionalProperties_Message_Contains_Message()
        {
            MessageData messageData = new MessageData(message: message);

            Assert.AreEqual("", messageData.Title);
            Assert.AreEqual(message, messageData.Message);
            Assert.AreEqual("", messageData.Footer);
            Assert.AreEqual("", messageData.PromptList);
        }

        [Test]
        public void InitializeWithOptionalProperties_Footer_Contains_Footer()
        {
            MessageData messageData = new MessageData(footer: footer);

            Assert.AreEqual("", messageData.Title);
            Assert.AreEqual("", messageData.Message);
            Assert.AreEqual(footer, messageData.Footer);
            Assert.AreEqual("", messageData.PromptList);
        }

        [Test]
        public void InitializeWithOptionalProperties_PromptList_Contains_PromptList()
        {
            MessageData messageData = new MessageData(promptList: promptList);

            Assert.AreEqual("", messageData.Title);
            Assert.AreEqual("", messageData.Message);
            Assert.AreEqual("", messageData.Footer);
            Assert.AreEqual(promptList, messageData.PromptList);
        }

        [Test]
        public void InitializeWithOptionalProperties_All_Contains_All()
        {
            MessageData messageData = new MessageData(title, message, footer, promptList);

            Assert.AreEqual(title, messageData.Title);
            Assert.AreEqual(message, messageData.Message);
            Assert.AreEqual(footer, messageData.Footer);
            Assert.AreEqual(promptList, messageData.PromptList);
        }
    }
}
