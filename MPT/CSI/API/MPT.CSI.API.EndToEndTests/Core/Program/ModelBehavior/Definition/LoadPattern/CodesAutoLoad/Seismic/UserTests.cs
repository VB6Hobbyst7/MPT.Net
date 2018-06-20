#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    [TestFixture]
    public class User_Get : CsiGet
    {
      
        public void GetLoad(string name,
                    ref eApplicationPointType applicationPtType,
                    ref double eccentricity,
                    ref string[] diaphragms,
                    ref double[] Fx,
                    ref double[] Fy,
                    ref double[] Mz,
                    ref Coordinate2DCartesian[] applicationCoordinate)
        {
          
        }
        
        public void GetLoadCoefficient(string name,
                            ref eSeismicLoadDirection directionFlag,
                            ref double eccentricity,
                            ref bool userSpecifiedHeights,
                            ref double topZ,
                            ref double bottomZ,
                            ref double c,
                            ref double k)
        {
          
        }
    }
    
    [TestFixture]
    public class User_Set : CsiSet
    {

        
        public void SetLoad(string name,
                    eApplicationPointType applicationPtType,
                    double eccentricity)
        {
          
        }

        
        public void SetLoadValue(string name,
                    string diaphragm,
                    double Fx,
                    double Fy,
                    double Mz,
                    Coordinate2DCartesian applicationCoordinate)
        {
          
        }
        
        
        public void SetLoadCoefficient(string name,
                            eSeismicLoadDirection directionFlag,
                            double eccentricity,
                            bool userSpecifiedHeights,
                            double topZ,
                            double bottomZ,
                            double c,
                            double k)
        {
          
        }
    }
}

#endif