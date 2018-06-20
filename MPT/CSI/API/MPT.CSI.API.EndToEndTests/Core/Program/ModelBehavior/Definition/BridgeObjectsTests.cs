#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class BridgeObjects_Get : CsiGet
    {
      
        public void GetBridgeUpdateData(string name,
            ref bool linkedModelExists,
            ref eBridgeLinkModelType modelType,
            ref double maxLengthDeck,
            ref double maxLengthCapBeam,
            ref double maxLengthColumn,
            ref double subMeshSize)
        {
          
        }

        
        public void GetBridgeUpdateForAnalysisFlag(ref bool updateBridgeObjects)
        {
          
        }
    }
    
     [TestFixture]
    public class BridgeObjects_Set : CsiSet
    {

        
        public void SetBridgeUpdateData(string name,
            eBridgeLinkAction action,
            eBridgeLinkModelType modelType,
            double maxLengthDeck,
            double maxLengthCapBeam,
            double maxLengthColumn,
            double subMeshSize)
        {
          
        }
      

        
        public void SetBridgeUpdateForAnalysisFlag(bool updateBridgeObjects)
        {
          
        }
    }
}
#endif
