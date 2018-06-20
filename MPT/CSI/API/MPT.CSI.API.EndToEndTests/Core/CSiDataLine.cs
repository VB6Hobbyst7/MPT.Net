
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.EndToEndTests.Core
{
    public static class CSiDataLine
    {
        public const int NumberOfNamedAssignExpected = 2;

        public const int NumberOfElementsExpected = 116;
        public const int NumberOfObjectsExpected = 58;

        // Frames
        public const string NameObjectFrameCurved = "Frame-Curved";
        public const string NameElementFrameCurved = NameObjectFrameCurved + "-1";
        public const double RelativeDistanceFrameCurved = 0.1;

        public const string NameObjectFrameOffsets = "Frame-Offsets";
        public const string NameElementFrameOffsets = NameObjectFrameOffsets;
        public const double RelativeDistanceFrameOffsets = 1;
        public const double OldOffsetI = 6;
        public const double OldOffsetJ = 12;
        public const double OldRigidZoneFactor = 0.5;
        public static string[] NameElementFrameOffsetJoints = { "5", "8" };


        public const string NameObjectFrameReleases = "Frame-Releases";
        public const string NameElementFrameReleases = NameObjectFrameReleases;

        public static DegreesOfFreedomLocal OldIEndRelease = new DegreesOfFreedomLocal()
        {
            U1 = false,
            U2 = true,
            U3 = false,
            R1 = false,
            R2 = false,
            R3 = true,
        };

        public static DegreesOfFreedomLocal OldJEndRelease = new DegreesOfFreedomLocal()
        {
            U1 = true,
            U2 = false,
            U3 = false,
            R1 = false,
            R2 = true,
            R3 = false,
        };

        public static Fixity OldIEndFixity = new Fixity()
        {
            U1 = 0,
            U2 = 0.4,
            U3 = 0,
            R1 = 0,
            R2 = 0,
            R3 = 0,
        };

        public static Fixity OldJEndFixity = new Fixity()
        {
            U1 = 0.3,
            U2 = 0,
            U3 = 0,
            R1 = 0,
            R2 = 0,
            R3 = 0,
        };
        // ISection
        public const int NumberOfSectionFramesIExpected = 2;

        public const string NameSectionFrameCol = "FSECol";

        public const string NameSectionFrame = "ISection";
        public const string NameObjectFrame = "Frame-" + NameSectionFrame;
        public const string NameElementFrame = NameObjectFrame + "-1";
        public const string NameElementFrame2 = NameObjectFrame + "-2";
        public static string[] NameElementFrameMeshedJoints = {"2", "MeshAtTopOfSolid" };
        public const double RelativeDistanceFrameMeshed = 0.042;
        public static FrameModifier OldModifiers = new FrameModifier
        {
            CrossSectionalArea = 1.2,
            ShearV2 = 1.3,
            ShearV3 = 1.4,
            Torsion = 1.5,
            BendingM2 = 1.6,
            BendingM3 = 1.7,
            MassModifier = 1.8,
            WeightModifier = 1.9,
        };

        // Totals
        public const int NumberOfSectionFramesExpected = NumberOfSectionFramesIExpected;
        public const int NumberOfObjectFramesExpected = 58;



        // Cables
        public const int NumberOfNamedAssignCableExpected = 1;
        public const string NameNamedAssignCable = "NamedAssign-Cable";

        public const int NumberOfSectionCablesExpected = 2;
        public const int NumberOfObjectCablesExpected = 19;

        public const string NameSectionCable = "CAB1";
        public const string NameObjectCable = "Cable1";
        public const string NameElementCable = NameObjectCable;
        public static string[] NameElementCableJoints = {"4", "2"};
        public const double RelativeDistanceCable = 1;
        public const double OldCableArea = 1;
        public static CableModifier OldModifiersCable = new CableModifier
        {
            CrossSectionalArea = 1.4,
            MassModifier = 1.5,
            WeightModifier = 1.6,
        };

        public const string NameSectionCableMultiSegment = "CAB2";
        public const string NameObjectCableMultiSegment = "Cable-Multisegment";
        public const string NameElementCableMultiSegment = NameObjectCableMultiSegment;

        // Tendons
        public const int NumberOfSectionTendonsExpected = 2;
        public const int NumberOfObjectTendonsExpected = 2;
        
        public const string NameSectionTendonAsLoads = "TEN1";
        public const string NameObjectTendonAsLoads = "59";
        public const string NameElementTendonAsLoads = NameObjectTendonAsLoads + "-1";

        public const string NameSectionTendonAsElements = "TEN2-AsElements";
        public const string NameObjectTendonAsElements = "60";
        public const string NameElementTendonAsElements = NameObjectTendonAsElements + "-1";
        public const string NameElementTendonAsElements2 = NameObjectTendonAsElements + "-2";
        public const string NameElementTendonAsElements3 = NameObjectTendonAsElements + "-3";
        public const string NameElementTendonAsElements4 = NameObjectTendonAsElements + "-4";
        public const string NameElementTendonAsElements5 = NameObjectTendonAsElements + "-5";
        public const string NameElementTendonAsElements6 = NameObjectTendonAsElements + "-6";
        public const string NameElementTendonAsElements7 = NameObjectTendonAsElements + "-7";
        public const double RelativeDistanceTendonAsElements = 0.143;


        // NamedAssigns
        public const int NumberOfNamedAssignFrameExpected = 1;
        public const string NameNamedAssignFrame = "NamedAssign-Frame";

        public const int NumberOfNamedAssignFrameReleaseExpected = 1;
        public const string NameNamedAssignRelease = "NamedAssign-Release";

        public static DegreesOfFreedomLocal OldIEndReleaseNamedAssign = new DegreesOfFreedomLocal()
        {
            U1 = true,
            U2 = false,
            U3 = false,
            R1 = false,
            R2 = true,
            R3 = false,
        };

        public static DegreesOfFreedomLocal OldJEndReleaseNamedAssign = new DegreesOfFreedomLocal()
        {
            U1 = false,
            U2 = true,
            U3 = false,
            R1 = false,
            R2 = false,
            R3 = true,
        };

        public static Fixity OldIEndFixityNamedAssign = new Fixity()
        {
            U1 = 0,
            U2 = 0,
            U3 = 0,
            R1 = 0,
            R2 = 0.5,
            R3 = 0,
        };

        public static Fixity OldJEndFixityNamedAssign = new Fixity()
        {
            U1 = 0,
            U2 = 0,
            U3 = 0,
            R1 = 0,
            R2 = 0,
            R3 = 0.6,
        };


    }
}
