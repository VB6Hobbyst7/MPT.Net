

using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.EndToEndTests.Core
{
    public static class CSiDataSolid
    {
        public const int NumberOfSectionsExpected = 2;
        public const int NumberOfObjectsExpected = 3;
        public const int NumberOfElementsExpected = NumberOfObjectsExpected + 7; // 1 object is meshed 2x2x2 = 8

        public const string NameSection = "SOLID1";
        public const string NameObject = "Solid1";
        public const string NameElement = NameObject;
        public static string[] ElementJoints =
        {
            "5", "2", "14", "11", "37", "MeshAtTopOfSolid", "39", "40"
        };

        public const string NameSectionNonDefault = "SOLID-NonDefault";
        public const string NameObjectMeshed = "Solid-Meshed";
        public const string NameElementMeshed = NameObjectMeshed + "-1";
        public static string[] ElementJointsMeshed =
        {
            "14", "~10", "38", "~11", "~12", "~13", "~14", "~15"
        };


        public static AngleLocalAxes OldMaterialAngle = new AngleLocalAxes()
        {
            AngleA = 1,
            AngleB = 2,
            AngleC = 3
        };
    }
}
