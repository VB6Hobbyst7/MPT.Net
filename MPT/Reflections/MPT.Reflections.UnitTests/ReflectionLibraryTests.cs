using System;

using NUnit.Framework;

namespace MPT.Reflections.UnitTests
{
    [TestFixture]
    public class ReflectionLibraryTests
    {
        // ncrunch: no coverage start
        private string FooBar { get; set; }
        // ncrunch: no coverage end

        [Test]
        public void NameOfProp_Of_Null_Is_Null()
        {
            Assert.IsNull(ReflectionLibrary.NameOfProp<object>(() => null));
        }

        [Test]
        public void NameOfProp_Of_Property_Is_Property_Name()
        {
            Assert.AreEqual("FooBar", ReflectionLibrary.NameOfProp(() => FooBar));
        }

        [Test]
        public void NameOfParam_Of_Null_Is_Null()
        {
            Assert.IsNull(ReflectionLibrary.NameOfParam<object>(() => null));
        }

        [Test]
        public void NameOfParam_Of_Property_Is_Property_Name()
        {
            string foobarParam = "";
            Assert.AreEqual("foobarParam", ReflectionLibrary.NameOfParam(() => foobarParam));
        }
    }
}
