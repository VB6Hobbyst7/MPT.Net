#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    [TestFixture]
    public class User_Get : CsiGet
    {
      
        public void GetLoad(string name,
                    ref string[] diaphragms,
                    ref double[] Fx,
                    ref double[] Fy,
                    ref double[] Mz,
                    ref Coordinate2DCartesian[] applicationCoordinate)
        {
          
        }
    }
    
    [TestFixture]
    public class User_Set : CsiSet
    {

        
        public void SetLoad(string name,
                    string diaphragm,
                    double Fx,
                    double Fy,
                    double Mz,
                    Coordinate2DCartesian applicationCoordinate)
        {
          
        }
    }
}
#endif
