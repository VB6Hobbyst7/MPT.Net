
namespace MPT.CSI.API.EndToEndTests.Core
{
    public static class CSiDataLink
    {
        public const int NumberOfObjectsExpected = 2;

        public const string NameSectionMultiLinearElastic = "MultiLinearElastic";

        public const string NameObjectSinglePoint = "Link1Point1";
        public const string NameElementSinglePoint = NameObjectSinglePoint;
        public static string[] SinglePointJoints = { "10~Link", "10" };

        public const string NameObjectTwoPoints = "Link2Points1";
        public const string NameElementTwoPoints = NameObjectTwoPoints;
        public static string[] TwoPointsJoints = {"Restraint1", "11"};
    }
}
