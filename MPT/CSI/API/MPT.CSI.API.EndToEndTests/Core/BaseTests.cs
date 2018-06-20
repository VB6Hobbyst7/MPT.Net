using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core
{
    [TestFixture]
    public class BaseTests
    {
        protected void delayTestStart(bool until,
            int attempts,
            int wait)
        {
            int numberOfAttempts = 0;
            while (!until &&
                   (numberOfAttempts < attempts))
            {
                // Wait to execute test...
                numberOfAttempts++;
                Thread.Sleep(wait);
            }
        }
    }
}
