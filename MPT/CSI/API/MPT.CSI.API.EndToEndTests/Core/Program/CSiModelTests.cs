using System.Collections.Generic;
using MPT.CSI.API.Core.Program;
using NUnit.Framework;

namespace MPT.CSI.API.EndToEndTests.Core.Program 
{
    [TestFixture]
    public class CSiModelTests_Get : CsiGet
    {
        [Test]
        public void GetModelIsLocked()
        {
            bool isLocked = _app.Model.GetModelIsLocked();
            Assert.IsFalse(isLocked);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        [Test]
        public void GetUserComment()
        {
            string userComment = "";
            _app.Model.GetUserComment(ref userComment);

            Assert.IsFalse(string.IsNullOrWhiteSpace(userComment));
        }
#endif
        [Test]
        public void GetDatabaseUnits()
        {
            eUnits units = _app.Model.GetDatabaseUnits();
            Assert.AreEqual(eUnits.kip_in_F, units);
        }

        [Test]
        public void GetPresentUnits()
        {
            eUnits units = _app.Model.GetPresentUnits();
            Assert.AreEqual(eUnits.kip_in_F, units);
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        [Test]
        public void GetDatabaseUnits(ref eForce forceUnits,
            ref eLength lengthUnits,
            ref eTemperature temperatureUnits)
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void GetPresentUnits(ref eForce forceUnits,
            ref eLength lengthUnits,
            ref eTemperature temperatureUnits)
        {
            Assert.IsTrue(false);
        }
#endif
        [Test]
        public void GetMergeTolerance()
        {
            double mergeTolerance = 0;
            _app.Model.GetMergeTolerance(ref mergeTolerance);
            Assert.AreEqual(0.1, mergeTolerance);
        }

        [Test]
        public void GetPresentCoordSystem()
        {
            string coordinateSystem = _app.Model.GetPresentCoordSystem();
            Assert.AreEqual("CSYS1", coordinateSystem);
        }


        [Test]
        public void GetProjectInfo()
        {
            Dictionary<string, string> projectData = new Dictionary<string, string>()
            {
                {"Company Name", "Computers and Structures, Inc."} ,
                {"Client Name", "Foo"} ,
                {"Project Name", "Bar"} ,
                {"Project Number", "12345"} ,
                {"Model Name", "FooBar"} ,
                {"Model Description", "Bars of Foo"} ,
                {"Revision Number", "54321"} ,
                {"Frame Type", "Marzipan"} ,
                {"Engineer", "John"} ,
                {"Checker", "Doe"} ,
                {"Supervisor", "Lumberg"} ,
                {"Issue Code", "9876"} ,
                {"Design Code", "Magic"} ,
            };
            string[] projectInfoItems = new string[0];
            string[] projectInfoData = new string[0];
            _app.Model.GetProjectInfo(ref projectInfoItems, ref projectInfoData);
            
            for (int i = 0; i < projectInfoItems.Length; i++)
            {
                Assert.IsTrue(projectData.ContainsKey(projectInfoItems[i]));
                Assert.AreEqual(projectData[projectInfoItems[i]], projectInfoData[i]);
            }
        }

        [Test]
        public void GetVersion()
        {
            string versionName = string.Empty;
            double versionNumber = 0;

            _app.Model.GetVersion(ref versionName, ref versionNumber);

            Assert.AreEqual(CSiData.versionName, versionName);
            Assert.AreEqual(CSiData.versionNumber, versionNumber);
        }
    }

    
    [TestFixture]
    public class CSiModelTests_Set : CsiSet
    {
        [TestCase("units")]
        public void InitializeNewModel(eUnits units)
        {

        }

        [TestCase("lockModel")]
        public void SetModelIsLocked(bool lockModel)
        {

        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        [TestCase("commentUser", "numberLinesBlank", "replace")]
        public void SetUserComment(string commentUser, 
            int numberLinesBlank = 1, 
            bool replace = false)
        {

        }
#endif

        [TestCase("units")]
        public void SetPresentUnits(eUnits units)
        {

        }
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        [TestCase("forceUnits", "lengthUnits", "temperatureUnits")]
        public void SetPresentUnits(eForce forceUnits,
            eLength lengthUnits,
            eTemperature temperatureUnits)
        {
           
        }
#endif

        [TestCase("mergeTolerance")]
        public void SetMergeTolerance(double mergeTolerance)
        {

        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        [TestCase("nameCoordinateSystem")]
        public void SetPresentCoordSystem(string nameCoordinateSystem)
        {

        }
#endif

        [TestCase("projectInfoItem", "projectInfoData")]
        public void SetProjectInfo(string projectInfoItem,
            string projectInfoData)
        {

        }
    }
}
