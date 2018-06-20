#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class NamedSets_Get : CsiGet
    {
      
        public void GetJointRespSpec(string nameSet,
            ref string loadCase,
            ref string[] nameJoints,
            ref string coordinateSystem,
            ref int direction,
            ref eVibrationUnit abscissa,
            ref eSpectralResponse ordinate,
            ref bool useDefaultFrequencies,
            ref bool useStructuralFrequencies,
            ref double[] userFrequencies,
            ref double[] dampingValues,
            ref ePlotType abscissaPlotType,
            ref double spectrumWidening,
            ref ePlotType ordinatePlotType,
            ref double ordinateScaleFactor)
        {
          
        }
    }
    
    [TestFixture]
    public class NamedSets_Set : CsiSet
    {
        
        
        public void SetJointRespSpec(string nameSet,
            string loadCase,
            ref string[] nameJoints,
            string coordinateSystem,
            int direction,
            eVibrationUnit abscissa,
            eSpectralResponse ordinate,
            bool useDefaultFrequencies,
            bool useStructuralFrequencies,
            ref double[] userFrequencies,
            ref double[] dampingValues,
            ePlotType abscissaPlotType,
            double spectrumWidening,
            ePlotType ordinatePlotType,
            double ordinateScaleFactor)
        {
          
        }
    }
}
#endif