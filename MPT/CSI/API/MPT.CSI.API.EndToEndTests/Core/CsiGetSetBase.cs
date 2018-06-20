using System;
using MPT.CSI.API.Core.Program;
using NUnit.Framework;

namespace MPT.CSI.API.EndToEndTests.Core
{
    [TestFixture]
    public abstract class CsiGetSetBase
    {

        protected CSiApplication _app;


        protected void setup(string pathModel)
        {
            _app = new CSiApplication(CSiData.pathApp,
                modelPath: CSiData.pathResources + @"\" + pathModel + CSiData.extension,
                numberOfExitAttempts: 60,
                intervalBetweenExitAttempts: 1000);
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            // Needed to force ETABS results to be consisted with SAP2000 for easier testing.
            _app.Model.SetPresentUnits(eUnits.kip_in_F);
#endif
        }


        protected void tearDown()
        {
            _app.Dispose();
        }

        /// <summary>
        /// Checks two values of type double for equality.
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="tolerance">The zero-tolerance for equality.</param>
        /// <returns><c>true</c> if the values are equal within the specified or default tolerance, <c>false</c> otherwise.</returns>
        protected bool areEqual(double value1, 
            double value2, 
            double tolerance = 0.001)
        {
            return (Math.Abs(value1 - value2) < tolerance);
        }
    }
}
