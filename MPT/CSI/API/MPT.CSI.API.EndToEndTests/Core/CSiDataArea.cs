
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.EndToEndTests.Core
{
    public static class CSiDataArea
    {
        public const int NumberOfNamedAssignExpected = 1;
        public const string NameNamedAssign = "NamedAssign-Area";

        // Shell
        public const int NumberOfSectionsShellExpected = 6;
        public const int NumberOfObjectsShellExpected = 7;
        public const int NumberOfElementsShellExpected = NumberOfObjectsShellExpected + 3; // As one area is meshed 2x2

        public const string NameSectionShellThin = "Shell-Thin";
        public const string NameObjectShellThin = "Area1-" + NameSectionShellThin;
        public const string NameElementShellThin = NameObjectShellThin;
        public static string[] JointsShellThin = {"6", "Point1", "12", "15"};
        public static AreaModifier OldModifiers = new AreaModifier
        {
            MembraneF11 = 1.1,
            MembraneF22 = 1.2,
            MembraneF12 = 1.3,
            BendingM11 = 1.4,
            BendingM22 = 1.5,
            BendingM12 = 1.6,
            ShearV13 = 1.7,
            ShearV23 = 1.8,
            MassModifier = 1.9,
            WeightModifier = 2.0
        };

        public const string NameSectionShellThick = "Shell-Thick";
        public const string NameObjectShellThick = "Area2-" + NameSectionShellThick;
        public const string NameElementShellThick = NameObjectShellThick + "-3"; // Meshed 2x2
        public static string[] JointsShellThick = { "~28", "9", "~32", "~29" };

        public const string NameSectionShellLayered = "Shell-Layered";
        public const string NameObjectShellLayered = "Area9-" + NameSectionShellLayered;

        public const string NameSectionPlateThin = "Plate-Thin";
        public const string NameObjectPlateThin = "Area3-" + NameSectionPlateThin;

        public const string NameSectionPlateThick = "Plate-Thick";
        public const string NameObjectPlateThick = "Area4-" + NameSectionPlateThick;

        public const string NameSectionMembrane = "Membrane";
        public const string NameObjectMembrane = "Area5-" + NameSectionMembrane;

        // Plane
        public const int NumberOfSectionsPlaneExpected = 3;
        public const int NumberOfObjectsPlaneExpected = 3;
        public const int NumberOfElementsPlaneExpected = 3;

        // Plane Strain
        public const string NameSectionPlaneStrain = "Plane-Strain";
        public const string NameObjectPlaneStrain = "Area6-" + NameSectionPlaneStrain;
        public const string NameElementPlaneStrain = NameObjectPlaneStrain;

        // Plane Stress
        public const string NameSectionPlaneStress = "Plane-Stress";
        public const string NameObjectPlaneStress = "Area7-" + NameSectionPlaneStress;
        public const string NameElementPlaneStress = NameObjectPlaneStress;
        public static string[] JointsPlaneStress = { "20", "19", "28", "TransformedAxisJoint" };

        // ASolid
        public const int NumberOfSectionsASolidExpected = 1;
        public const int NumberOfObjectsASolidExpected = 1;
        //public const int NumberOfElementsASolidExpected = 1;

        public const string NameSectionASolid = "ASolid";
        public const string NameObjectASolid = "Area8-" + NameSectionASolid;


        // Totals
        public const int NumberOfSectionsExpected = NumberOfSectionsShellExpected + NumberOfSectionsPlaneExpected; // + NumberOfSectionsASolidExpected;
        public const int NumberOfObjectsExpected = NumberOfObjectsShellExpected + NumberOfObjectsPlaneExpected; // + NumberOfObjectsASolidExpected;
        public const int NumberOfElementsExpected = NumberOfElementsShellExpected + NumberOfElementsPlaneExpected; // + NumberOfElementsASolidExpected;
    }
}
