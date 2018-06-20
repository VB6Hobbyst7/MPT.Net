#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    [TestFixture]
    public class UBC_97_ISO_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_UBC_97 soilType,
            ref double Z,
            ref eSource seismicCoefficientSource,
            ref double Cv,
            ref eSource nearSourceSource,
            ref eSourceType_UBC_97 seismicSourceType,
            ref double distanceToSeismicSource,
            ref double Nv,
            ref double Ri,
            ref double Bd,
            ref double KDmax,
            ref double KDmin)
        {
          
        }
    }
    
    [TestFixture]
    public class UBC_97_ISO_Set : CsiSet
    {
        
        
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_UBC_97 soilType,
            double Z,
            eSource seismicCoefficientSource,
            double Cv,
            eSource nearSourceSource,
            eSourceType_UBC_97 seismicSourceType,
            double distanceToSeismicSource,
            double Nv,
            double Ri,
            double Bd,
            double KDmax,
            double KDmin)
        {
          
        }
    }
}
#endif
