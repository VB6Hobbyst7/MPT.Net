#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.BridgeAdvanced.BridgeType;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.BridgeAdvanced
{

    [TestFixture]
    public class Superstructure_Get : CsiGet
    {
      
        public void GetSuperstructureCutStressPoint(string name,
                    int cutIndex,
                    int pointIndex,
                    ref Coordinate2DCartesian stressPointCoordinate,
                    ref string nameMaterial,
                    ref string note)
        {
          
        }

        
        public void CountSuperstructureCutStressPoint(string name,
            int cutIndex,
            ref int numberOfStressPoints)
        {
          
        }


        
        public void GetSuperstructureCutLocation(string name,
            int cutIndex,
            ref eStation location,
            ref double station,
            ref Coordinate2DCartesian referencePointCoordinate,
            ref double skew,
            ref double grade,
            ref double superstructureElevation)
        {
          
        }

        
        public void CountSuperstructureCut(string name,
            ref int count)
        {
          
        }
    }
}
#endif
