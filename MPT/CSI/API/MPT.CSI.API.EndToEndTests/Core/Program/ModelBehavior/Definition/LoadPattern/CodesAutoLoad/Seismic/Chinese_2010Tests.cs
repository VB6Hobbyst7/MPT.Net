#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    [TestFixture]
    public class Chinese_2010_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double alphaMax,
            ref eSeismicIntensity_Chinese_2010 seismicIntensity,
            ref double damping,
            ref double Tg,
            ref double periodTimeDiscount,
            ref double enhancementFactor)
        {
          
        }
    }
    
    [TestFixture]
    public class Chinese_2010_Set : CsiSet
    {
      
        
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double alphaMax,
            eSeismicIntensity_Chinese_2010 seismicIntensity,
            double damping,
            double Tg,
            double periodTimeDiscount,
            double enhancementFactor)
        {
          
        }
    }
}
#endif
