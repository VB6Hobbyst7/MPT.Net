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
    public class UBC_97_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref double Ct,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_UBC_97 soilType,
            ref double R,
            ref double I,
            ref double Z,
            ref eSource seismicCoefficientSource,
            ref double Ca,
            ref double Cv,
            ref eSource nearSourceSource,
            ref eSourceType_UBC_97 seismicSourceType,
            ref double distanceToSeismicSource,
            ref double Na,
            ref double Nv)
        {
          
        }
    }
    
    [TestFixture]
    public class UBC_97_Set : CsiSet
    {
        
        
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            double Ct,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_UBC_97 soilType,
            double R,
            double I,
            double Z,
            eSource seismicCoefficientSource,
            double Ca,
            double Cv,
            eSource nearSourceSource,
            eSourceType_UBC_97 seismicSourceType,
            double distanceToSeismicSource,
            double Na,
            double Nv)
        {
          
        }
    }
}
#endif
