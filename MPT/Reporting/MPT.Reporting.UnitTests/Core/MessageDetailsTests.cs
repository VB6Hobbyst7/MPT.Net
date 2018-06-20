using NUnit.Framework;

namespace MPT.Reporting.UnitTests
{
    [TestFixture]
    public class MessageDetailsTests
    {
        eMessageActions defaultAction = eMessageActions.None;
        eMessageActions defaultActionDefault = eMessageActions.None;
        eMessageActionSets defaultActionSet = eMessageActionSets.OkOnly;
        eMessageType defaultMessageType = eMessageType.None;

        [Test]
        public void InitializeWithOptionalProperties_None_Contains_Defaults()
        {
            MessageDetails messageDetails = new MessageDetails();

            Assert.AreEqual(defaultAction, messageDetails.Action);
            Assert.AreEqual(defaultActionDefault, messageDetails.ActionDefault);
            Assert.AreEqual(defaultActionSet, messageDetails.ActionSet);
            Assert.AreEqual(defaultMessageType, messageDetails.MessageType);
        }

        [Test]
        public void InitializeWithOptionalProperties_All_Contains_All()
        {
            eMessageActionSets expectedActionSet = eMessageActionSets.YesNoCancel;
            eMessageType expectedMessageType = eMessageType.Warning;
            eMessageActions expectedDefault = eMessageActions.Cancel;
            
            MessageDetails messageDetails = new MessageDetails(expectedActionSet, expectedMessageType, expectedDefault);
            
            Assert.AreEqual(expectedDefault, messageDetails.ActionDefault);
            Assert.AreEqual(expectedActionSet, messageDetails.ActionSet);
            Assert.AreEqual(expectedMessageType, messageDetails.MessageType);

            Assert.AreEqual(defaultAction, messageDetails.Action);
        }

        [Test]
        public void Set_Property_Expected_Action_Sets_Property()
        {
            MessageDetails messageDetails = new MessageDetails();

            eMessageActions expectedAction = eMessageActions.No;
            messageDetails.Action = expectedAction;

            Assert.AreEqual(expectedAction, messageDetails.Action);

            Assert.AreEqual(defaultActionDefault, messageDetails.ActionDefault);
            Assert.AreEqual(defaultActionSet, messageDetails.ActionSet);
            Assert.AreEqual(defaultMessageType, messageDetails.MessageType);
        }
    }
}
