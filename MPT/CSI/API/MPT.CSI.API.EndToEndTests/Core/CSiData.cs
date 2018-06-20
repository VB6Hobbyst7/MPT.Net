
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.EndToEndTests.Core
{
    public static class CSiData
    {
        public const string pathResources = @"C:\TFS\Regression Tester\Main\DLLs\MPT.Tools\MPT.CSI\API\MPT.CSI.API.EndToEndTests" + @"\Resources"; //Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources";
        public const string pathModelDemo = "Demo";
        public const string pathModelQuery = "Query";
        public const string pathModelSet = "Set";
        public const string pathModelSave = "Save";
        public const string pathModelSaveReadOnly = "SaveReadOnly";
#if BUILD_SAP2000v16
        public const string pathApp = @"";
        public const string extension = ".sdb";
#elif BUILD_SAP2000v17
        public const string pathApp = @"C:\Program Files (x86)\Computers and Structures\SAP2000 17\SAP2000.exe";
        public const string extension = ".sdb";
        public const string versionName = "17.0.0";
        public const double versionNumber = 17.00;
        public const string processName = "SAP2000";
#elif BUILD_SAP2000v18
        public const string pathApp = @"";
        public const string extension = ".sdb";
#elif BUILD_SAP2000v19
        public const string pathApp = @"C:\Program Files (x86)\Computers and Structures\SAP2000 19\SAP2000.exe";
        public const string extension = ".sdb";
        public const string versionName = "19.2.1";
        public const double versionNumber = 19.21;
        public const string processName = "SAP2000";
#elif BUILD_SAP2000v20
        public const string pathApp = @"C:\Users\Mark\Documents\Projects\Work\CSI\Testing\SAP2000\SAP2000 v20.0.0 Build 1370 32-bit\SAP2000.exe";
        public const string extension = ".sdb";
        public const string versionName = "20.0.0";
        public const double versionNumber = 20.00;
        public const string processName = "SAP2000";
#elif BUILD_CSiBridgev16
        public const string pathApp = @"";
        public const string extension = ".bdb";
#elif BUILD_CSiBridgev17
        public const string pathApp = @"";
        public const string extension = ".bdb";
#elif BUILD_CSiBridgev18
        public const string pathApp = @"";
        public const string extension = ".bdb";
#elif BUILD_CSiBridgev19
        public const string pathApp = @"";
        public const string extension = ".bdb";
#elif BUILD_CSiBridgev20
        public const string pathApp = @"";
        public const string extension = ".bdb";
#elif BUILD_ETABS2013
        public const string pathApp = @"";
        public const string extension = ".edb";
#elif BUILD_ETABS2015
        public const string pathApp = @"C:\Program Files (x86)\Computers and Structures\ETABS 2015\ETABS.exe";
        public const string extension = ".edb";
        public const string versionName = "15.2.0";
        public const double versionNumber = 15.20;
        public const string processName = "ETABS";
#elif BUILD_ETABS2016
        public const string pathApp = @"C:\Program Files (x86)\Computers and Structures\ETABS 2016\ETABS.exe";
        public const string extension = ".edb";
        public const string versionName = "16.2.0";
        public const double versionNumber = 16.20;
        public const string processName = "ETABS";
#elif BUILD_ETABS2017
        public const string pathApp = @"C:\Users\Mark\Documents\Projects\Work\CSI\Testing\ETABS\ETABS 2017 v17.0.0 Build 1736 64-bit\ETABS.exe";
        public const string extension = ".edb";
        public const string versionName = "17.0.0";
        public const double versionNumber = 17.00;
        public const string processName = "ETABS";
#endif
        public const string CoordinateSystemGlobal = "GLOBAL";
        public const string CoordinateSystemCustom = "CSYS1";

        public const string JointPattern = "NewPattern";

        public const string OldNotes = "Foo";  // Typical pattern should be to have the oldNotes = name of the item. Only use this where the pattern is not valid.
        public const string NewNotes = "Bar";

        public const string OldGUID = "";
        public const string NewGUID = OldGUID + "1";

        public static string[] OldGroupNames = { "ALL", "GROUP1", "Group" };    // Pattern for 3rd item is to append object type, e.g. 'Group Areas'


        public static DegreesOfFreedomLocal oldDegreesOfFreedom = new DegreesOfFreedomLocal
        {
            U1 = true,
            U2 = true,
            U3 = true,
            R1 = true,
            R2 = false,
            R3 = true
        };
        public static DegreesOfFreedomLocal newDegreesOfFreedom = new DegreesOfFreedomLocal
        {
            U1 = true,
            U2 = false,
            U3 = true,
            R1 = true,
            R2 = true,
            R3 = true
        };

        public static DegreesOfFreedomLocal oldFixity = new DegreesOfFreedomLocal
        {
            U1 = false,
            U2 = false,
            U3 = false,
            R1 = true,
            R2 = false,
            R3 = false
        };
        public static DegreesOfFreedomLocal newFixitySet = new DegreesOfFreedomLocal
        {
            U1 = false,
            U2 = true, 
            U3 = false,
            R1 = false,
            R2 = false,
            R3 = true
        };

        public static DegreesOfFreedomLocal newFixityGet = new DegreesOfFreedomLocal
        {
            U1 = false,
            U2 = false, // Inactive, so this is always false, even though set to true.
            U3 = false,
            R1 = false,
            R2 = false,
            R3 = true
        };

        public static DegreesOfFreedomLocal oldNonlinear = new DegreesOfFreedomLocal
        {
            U1 = false,
            U2 = true,
            U3 = true,
            R1 = false,
            R2 = false,
            R3 = true
        };
        public static DegreesOfFreedomLocal newNonlinearSet = new DegreesOfFreedomLocal
        {
            U1 = true,
            U2 = true,
            U3 = false,
            R1 = true,
            R2 = true,
            R3 = true
        };
        public static DegreesOfFreedomLocal newNonlinearGet = new DegreesOfFreedomLocal
        {
            U1 = true,
            U2 = false, // Inactive, so this is always false, even though set to true.
            U3 = false,
            R1 = true,
            R2 = true,
            R3 = false  // Fixed, so this is always false, even though set to true.
        };

        public static Stiffness oldEffectiveStiffness = new Stiffness
        {
            U1 = 0.5,
            U2 = 0.4,
            U3 = 0.3,
            R1 = 0,     // Fixed
            R2 = 0,     // Inactive
            R3 = 0.2
        };
        public static Stiffness newEffectiveStiffness = new Stiffness
        {
            U1 = oldEffectiveStiffness.U1 + 0.1,
            U2 = 0,     // Inactive
            U3 = oldEffectiveStiffness.U3 + 0.1,
            R1 = oldEffectiveStiffness.R1 + 0.1,
            R2 = oldEffectiveStiffness.R2 + 0.1,
            R3 = 0      // Fixed
        };

        public static Stiffness oldEffectiveDamping = new Stiffness
        {
            U1 = 0.05,
            U2 = 0.04,
            U3 = 0.03,
            R1 = 0,     // Fixed
            R2 = 0,     // Inactive
            R3 = 0.02
        };
        public static Stiffness newEffectiveDamping = new Stiffness
        {
            U1 = oldEffectiveDamping.U1 + 0.01,
            U2 = 0,     // Inactive
            U3 = oldEffectiveDamping.U3 + 0.01,
            R1 = oldEffectiveDamping.R1 + 0.01,
            R2 = oldEffectiveDamping.R2 + 0.01,
            R3 = 0      // Fixed
        };

        public static double oldDistanceFromJEndToU2Spring = 0.1;
        public static double newDistanceFromJEndToU2Spring = oldDistanceFromJEndToU2Spring + 0.2;

        public static double oldDistanceFromJEndToU3Spring = 0.2;
        public static double newDistanceFromJEndToU3Spring = oldDistanceFromJEndToU3Spring + 0.2;


        public static double[] oldForces = { -1, -1, 0, 1, 1 };
        public static double[] oldDisplacements = { -10, -1, 0, 1, 10 };

        public static double[] newForces = {-5, -2, -1, 0, 6 };
        public static double[] newDisplacements = {-11, -5, -3, 0, 4 };

        public static double oldPivotAlpha1 = 10;
        public static double newPivotAlpha1 = oldPivotAlpha1 + 2;

        public static double oldPivotAlpha2 = 11;
        public static double newPivotAlpha2 = oldPivotAlpha2 + 2;

        public static double oldPivotBeta1 = 0.7;
        public static double newPivotBeta1 = newPivotBeta1 + 0.2;

        public static double oldPivotBeta2 = 0.8;
        public static double newPivotBeta2 = oldPivotBeta2 + 0.2;

        public static double oldPivotEta = 0.1;
        public static double newPivotEta = oldPivotEta + 0.2;
    }
}
