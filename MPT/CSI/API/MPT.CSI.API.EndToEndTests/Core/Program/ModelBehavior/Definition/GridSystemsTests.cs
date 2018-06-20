#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class GridSystems_Get : CsiGet
    {

        
        public void Count()
        {
          
        }

        
        public void GetNameList(ref string[] coordinateSystemNames)
        {
          
        }

        
        public void GetTransformationMatrix(string nameCoordinateSystem,
            ref double[] directionCosines)
        {
          
        }


        public void GetGridSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin)
        {
          
        }

        
        public void GetGridSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin,
            ref string gridSystemType,
            ref int numberOfXLines,
            ref string[] gridLineIdX,
            ref double[] ordinateX,
            ref bool[] visibleX,
            ref string[] bubbleLocationX,
            ref int numberOfYLines,
            ref string[] gridLineIdY,
            ref double[] ordinateY,
            ref bool[] visibleY,
            ref string[] bubbleLocationY)
        {
          
        }

#if !BUILD_ETABS2015

        public void GetGridSystemType(string name,
            ref string gridSystemType)
        {
          
        }
        
        public void GetGridSystemCartesian(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin,
            ref bool storyRangeIsDefault,
            ref string topStory,
            ref string bottomStory,
            ref double bubbleSize,
            ref int gridColor,
            ref int numberOfXLines,
            ref string[] gridLineIdX,
            ref double[] ordinateX,
            ref bool[] visibleX,
            ref string[] bubbleLocationX,
            ref int numberOfYLines,
            ref string[] gridLineIdY,
            ref double[] ordinateY,
            ref bool[] visibleY,
            ref string[] bubbleLocationY,
            ref int numberOfGeneralLines,
            ref string[] gridLineIdGeneral,
            ref double[] ordinateX1General,
            ref double[] ordinateY1General,
            ref double[] ordinateX2General,
            ref double[] ordinateY2General,
            ref bool[] visibleGeneral,
            ref string[] bubbleLocationGeneral)
        {
          
        }

        
        public void GetGridSystemCylindrical(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin,
            ref bool storyRangeIsDefault,
            ref string topStory,
            ref string bottomStory,
            ref double bubbleSize,
            ref int gridColor,
            ref int numberOfRadialLines,
            ref string[] gridLineIdRadial,
            ref double[] ordinateRadial,
            ref bool[] visibleRadial,
            ref string[] bubbleLocationRadial,
            ref int numberOfThetaLines,
            ref string[] gridLineIdTheta,
            ref double[] ordinateTheta,
            ref bool[] visibleTheta,
            ref string[] bubbleLocationTheta)
        {
          
        }

        
        public void GetNameTypeList(ref string[] gridSystemNames,
            ref string[] gridSystemTypes
            )
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class GridSystems_Set : CsiSet
    {
      
        public void ChangeName(string name,
            string newName)
        {
          
        }

        
        public void Delete(string nameCoordinateSystem)
        {
          
        }

        
        public void SetGridSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin)
        {
          
        }
    }
}
#endif