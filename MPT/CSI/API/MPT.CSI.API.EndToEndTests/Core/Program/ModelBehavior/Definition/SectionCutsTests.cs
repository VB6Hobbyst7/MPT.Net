#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class SectionCuts_Get : CsiGet
    {

        
        public void GetCutInfo(string nameSectionCut, 
            ref string groupName,
            ref eSectionResultType sectionCutType,
            ref int numberQuadrilaterals)
        {
          
        }

        
        public void GetNameList(ref string[] namesSectionCut)
        {
          
        }

        
        public void GetQuadrilateral(string nameQuad,
            int quadrilateralNumber,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] zCoordinates)
        {
          
        }

        
        public void GetLocalAxesAdvancedAnalysis(string nameSectionCut,
            ref bool isActive,
            ref eReferenceVector axisVectorOption,
            ref string axisCoordinateSystem,
            ref eReferenceVectorDirection[] axisVectorDirection,
            ref string[] axisPoint,
            ref double[] axisReferenceVector,
            ref int localPlaneByReferenceVector,
            ref eReferenceVector planeVectorOption,
            ref string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector)
        {
          
        }

        
        public void GetLocalAxesAnalysis(string nameSectionCut,
            ref double zRotation,
            ref double yRotation,
            ref double xRotation,
            ref bool isAdvanced)
        {
          
        }

        
        public void GetLocalAxesAngleDesign(string nameSectionCut, 
            ref double angle)
        {
          
        }

        
        public void GetResultLocation(string nameSectionCut, 
            ref bool isDefault,
            ref double xCoordinate,
            ref double yCoordinate,
            ref double zCoordinate)
        {
          
        }

        
        public void GetResultsSide(string nameSectionCut,
            ref int side)
        {
          
        }
    }
    
    [TestFixture]
    public class SectionCuts_Set : CsiSet
    {

    
        public void AddQuadrilateral(string nameQuad,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] zCoordinates)
        {
          
        }

        
        public void SetByGroup(string nameSectionCut,
            string groupName,
            eSectionResultType sectionCutType)
        {
          
        }

        
        public void SetByQuadrilateral(string nameSectionCut,
            string groupName,
            eSectionResultType sectionCutType,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] zCoordinates)
        {
          
        }

        
        public void SetLocalAxesAdvancedAnalysis(string nameSectionCut, 
            bool isActive,
            eReferenceVector axisVectorOption,
            string axisCoordinateSystem,
            ref eReferenceVectorDirection[] axisVectorDirection,
            ref string[] axisPoint,
            ref double[] axisReferenceVector,
            int localPlaneByReferenceVector,
            eReferenceVector planeVectorOption,
            string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector)
        {
          
        }

        
        public void SetLocalAxesAnalysis(string nameSectionCut,
            double zRotation,
            double yRotation,
            double xRotation)
        {
          
        }

        
        public void SetLocalAxesAngleDesign(string nameSectionCut,
            double angle)
        {
          
        }

        
        public void SetResultLocation(string nameSectionCut, 
            bool isDefault,
            double xCoordinate = 0,
            double yCoordinate = 0,
            double zCoordinate = 0)
        {
          
        }
        
        
        public void SetResultsSide(string nameSectionCut,
            int side)
        {   
        }
    }
}
#endif
