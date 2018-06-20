#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class GeneralReferenceLines_Get : CsiGet
    {

        public void Count()
        {
          
        }

        
        public void GetNameList(ref string[] names)
        {
          
        }

        
        public void GetLine(string name,
            ref double discretizationLength,
            ref double discretizationAngle,
            ref int color,
            ref bool isVisible)
        {
          
        }

        
        public void GetLineElevationPoints(string name,
            ref int numberPoints,
            ref eLayoutCurveType[] curveTypes,
            ref double[] curveTypeValues1,
            ref double[] curveTypeValues2,
            ref double[] curveTypeValues3,
            ref double[] stationCoordinates,
            ref double[] zCoordinates,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }

        
        public void GetLinePlanPoints(string name,
            ref int numberPoints,
            ref eLayoutCurveType[] curveTypes,
            ref double[] curveTypeValues1,
            ref double[] curveTypeValues2,
            ref double[] curveTypeValues3,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }
    }
    
    [TestFixture]
    public class GeneralReferenceLines_Set : CsiSet
    {

        
        public void Delete(string name)
        {
          
        }
        
        
        public void ConvertLineToBridgeLayoutLine(string name,
            double firstStation = 0,
            string coordinateSystem = CoordinateSystems.Global,
            double offsetX = 0,
            double offsetY = 0,
            double offsetZ = 0)
        {
          
        }

        
        public void SetLine(string name,
            double discretizationLength,
            double discretizationAngle,
            string coordinateSystem = CoordinateSystems.Global,
            int color = -1,
            bool isVisible = true)
        {
          
        }

        
        public void SetLineElevationPoints(string name,
            eLayoutCurveType[] curveTypes,
            double[] curveTypeValues1,
            double[] curveTypeValues2,
            double[] curveTypeValues3,
            double[] stationCoordinates,
            double[] zCoordinates)
        {
          
        }

        
        public void SetLinePlanPoints(string name,
            eLayoutCurveType[] curveTypes,
            double[] curveTypeValues1,
            double[] curveTypeValues2,
            double[] curveTypeValues3,
            double[] xCoordinates,
            double[] yCoordinates)
        {
          
        }
    }
}
#endif