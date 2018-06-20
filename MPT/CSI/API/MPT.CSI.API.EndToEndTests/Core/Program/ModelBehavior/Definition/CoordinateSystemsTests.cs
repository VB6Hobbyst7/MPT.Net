#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class CoordinateSystems_Get : CsiGet
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
        
        
        public void GetCoordinateSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double zCoordinateOrigin,
            ref double rzCoordinateOrigin,
            ref double ryCoordinateOrigin,
            ref double rxCoordinateOrigin)
        {
          
        }
    }
    
    [TestFixture]
    public class CoordinateSystems_Set : CsiSet
    {
        
        
        public void ChangeName(string name,
            string newName)
        {
          
        }

       
       
        public void Delete(string nameCoordinateSystem)
        {
          
        }

        
        public void SetCoordinateSystem(string nameCoordinateSystem,
            double xCoordinateOrigin,
            double yCoordinateOrigin,
            double zCoordinateOrigin,
            double rzCoordinateOrigin,
            double ryCoordinateOrigin,
            double rxCoordinateOrigin)
        {
          
        }
    }
}
#endif