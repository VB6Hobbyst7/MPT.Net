#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wave;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wave
{
    [TestFixture]
    public class WaveLoadGeneric_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eLoadMethod loadMethod,
            ref string coordinateSystem,
            ref bool adjustGravityLateral,
            ref double adjustGravityLateralFactor,
            ref bool adjustGravityVertical,
            ref double adjustGravityVerticalFactor,
            ref Coordinate3DCartesian centerRotation,
            ref double[] parameters,
            ref bool ignorePhase)
        {
          
        }
    }
    
    [TestFixture]
    public class WaveLoadGeneric_Set : CsiSet
    {

        public void SetLoad(string name,
            eLoadMethod loadMethod,
            string coordinateSystem,
            bool adjustGravityLateral,
            double adjustGravityLateralFactor,
            bool adjustGravityVertical,
            double adjustGravityVerticalFactor,
            Coordinate3DCartesian centerRotation,
            double[] parameters,
            bool ignorePhase)
        {
          
        }
    }
}
#endif
