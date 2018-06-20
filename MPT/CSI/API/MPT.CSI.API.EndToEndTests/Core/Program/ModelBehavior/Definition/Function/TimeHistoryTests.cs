#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function
{
    [TestFixture]
    public class TimeHistory_Get : CsiGet
    {
        public void GetFromFile(string name,
            ref string fileName,
            ref int headerLinesSkip,
            ref int prefixCharactersSkip,
            ref int pointsPerLine,
            ref eFileValueType valueType,
            ref bool freeFormat,
            ref int numberFixed,
            ref double timeInterval)
        {
          
        }

        
        public void GetUser(string name,
            ref double[] times,
            ref double[] values)
        {
          
        }

        
        public void GetUserPeriodic(string name,
            ref double[] frequencies,
            ref double[] values,
            ref int numberOfCycles)
        {
          
        }
        
        
        public void GetRamp(string name,
            ref double timeToInitialValue,
            ref double amplitude,
            ref double maxTime)
        {
          
        }

        
        public void GetCosine(string name,
            ref double period,
            ref int steps,
            ref int cycles,
            ref double amplitude)
        {
          
        }

        
        public void GetSine(string name,
            ref double period,
            ref int steps,
            ref int cycles,
            ref double amplitude)
        {
          
        }


        
        public void GetSawtooth(string name,
            ref double period,
            ref double timeToAmplitude,
            ref int cycles,
            ref double amplitude)
        {
          
        }

        
        public void GetTriangular(string name,
            ref double period,
            ref int cycles,
            ref double amplitude)
        {
          
        }
    }
    
    
    [TestFixture]
    public class TimeHistory_Set : CsiSet
    {
        
        
        public void SetFromFile(string name,
            string fileName,
            int headerLinesSkip,
            int prefixCharactersSkip,
            int pointsPerLine,
            eFileValueType valueType,
            bool freeFormat,
            int numberFixed = 10,
            double timeInterval = 0.02)
        {
          
        }

        
        public void SetUser(string name,
            double[] times,
            double[] values)
        {
          
        }

        
        public void SetUserPeriodic(string name,
            double[] frequencies,
            double[] values,
            int numberOfCycles)
        {
          
        }

        
        public void SetRamp(string name,
            double timeToInitialValue,
            double amplitude,
            double maxTime)
        {
          
        }

        
        public void SetCosine(string name,
            double period,
            int steps,
            int cycles,
            double amplitude)
        {
          
        }

        
        public void SetSine(string name,
            double period,
            int steps,
            int cycles,
            double amplitude)
        {
          
        }

        
        public void SetSawtooth(string name,
            double period,
            double timeToAmplitude,
            int cycles,
            double amplitude)
        {
          
        }

        
        public void SetTriangular(string name,
            double period,
            int cycles,
            double amplitude)
        {
          
        }
    }
}
#endif