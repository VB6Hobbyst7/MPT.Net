using System;
using System.Windows;
using NMath = System.Math;

using NUnit.Framework;

namespace MPT.Math.UnitTests
{
    [TestFixture]
    public class AlgebraTests
    {
        // TODO: Quadratic formula from object
        // TODO: Summation from object & strategy pattern

        [TestCase(0, 0, 0)]
        [TestCase(0, 0, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(3, 5.5, 12)]
        [TestCase(3, -5.5, 12)]
        public void SRSS(double value1, double value2, double value3)
        {
            double expectedResult = NMath.Sqrt(value1.Squared() + value2.Squared() + value3.Squared());
            Assert.AreEqual(expectedResult, Algebra.SRSS(value1, value2, value3));
        }
    }

    
}
