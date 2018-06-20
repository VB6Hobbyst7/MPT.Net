#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.BridgeAdvanced.BridgeType
{
    [TestFixture]
    public class ConcreteBoxGirder_Get : CsiGet
    {
      
        public void CountSuperstructureCutWebStressPoint(string name,
            int cutIndex,
            int webIndex,
            ref int count)
        {
          
        }

       
       
        public void GetSuperstructureCutSectionPropertiesAtY(string name,
            int cutIndex,
            double yCoordinate,
            bool isAboveY,
            ref double yCentroidOfRegion,
            ref double areaOfRegion,
            ref double momentOfInertiaOfRegion)
        {
          
        }

       
       
        public void GetSuperstructureCutSectionValues(string name,
            int cutIndex,
            eSectionCutItem cutSectionItem,
            ref double sectionCutValue)
        {
          
        }

        
        
        public void GetSuperstructureCutSlabCoordinatesAtX(string name,
            int cutIndex,
            double xCoordinate,
            ref eCutStatus status,
            ref double yTopmostSectionCut,
            ref double yBottommostSectionCut,
            ref double yTopmostInteriorCellCut,
            ref double yBottommostInteriorCellCut)
        {
          
        }

        
        
        public void GetSuperstructureCutTendonNames(string name,
            int cutIndex,
            int tendonIndex,
            ref string nameBridgeTendon,
            ref string nameTendonObject)
        {
          
        }

       
       
        public void GetSuperstructureCutTendonValues(string name,
            int cutIndex,
            int tendonIndex,
            eTendonCutItem tendonCutItem,
            ref double tendonCutValue)
        {
          
        }

       
       
        public void GetSuperstructureCutWebCoordinatesAtY(string name,
            int cutIndex,
            double yCoordinate,
            ref bool[] webIsCut,
            ref double[] xCoordinatesWebLeft,
            ref double[] xCoordinatesWebRight)
        {
          
        }


        
        public void GetSuperstructureCutWebStressPoint(string name,
            int cutIndex,
            int webIndex,
            int pointIndex,
            ref Coordinate2DCartesian coordinateOfStressPoint,
            ref string nameMaterial,
            ref string note)
        {
          
        }

        
        
        public void GetSuperstructureCutWebValues(string name,
            int cutIndex,
            int webIndex,
            eWebCutItem webCutItem,
            ref double webCutValue)
        {
          
        }
    }
}
#endif
