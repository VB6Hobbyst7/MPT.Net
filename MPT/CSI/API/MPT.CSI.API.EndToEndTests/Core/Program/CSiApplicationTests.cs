
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program
{
    [TestFixture]
    public class CSiApplicationTests : BaseTests
    {
        [Test]
        public void CSiApplication_Initialize_New_Instance_Defaults()
        {
            bool programWasOpened;
            using (CSiApplication app = new CSiApplication(CSiData.pathApp))
            {
                Assert.That(app.IsInitialized);
                programWasOpened = app.IsInitialized;
            }
            Assert.IsTrue(programWasOpened);
        }

        [Test]
        public void CSiApplication_Initialize_New_Instance_with_Invalid_Application_Path_Throws_IOException()
        {
            string badPath = "FooBar.Exe";
            var ex = Assert.Throws<IOException>(() =>
            {
                using (CSiApplication app = new CSiApplication("FooBar.Exe"))
                {
                }
            });
            Assert.That(ex.Message, Is.EqualTo("The following CSi program path is invalid: " + badPath));
        }

#if !BUILD_SAP2000v18 && !BUILD_SAP2000v17 && !BUILD_SAP2000v16 && !BUILD_CSiBridgev18 && !BUILD_CSiBridgev17 && !BUILD_CSiBridgev16 && !BUILD_ETABS2015
        [Test]
        public void CSiApplication_Initialize_New_Instance_By_Object_With_Defaults()
        {
            bool programWasOpened;
            using (CSiApplication app = new CSiApplication())
            {
                programWasOpened = app.IsInitialized;
            }
            Assert.IsTrue(programWasOpened);
        }


        [Test]
        public void CSiApplication_Initialize_AttachToProcess()
        {
            // This test should wait until all processes are closed.
            Process[] pname = Process.GetProcessesByName(CSiData.processName);
            delayTestStart(until: (pname.Length == 0), attempts: 20, wait: 1000);
            
            ProcessStartInfo processInfo = new ProcessStartInfo(CSiData.pathApp)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(processInfo);
            
            bool programWasAttachedTo;
            using (CSiApplication app = new CSiApplication(numberOfAttempts: 60, intervalBetweenAttempts: 1000,
                                                           numberOfExitAttempts: 60, intervalBetweenExitAttempts: 1000))
            {
                Assert.IsTrue(app.IsInitialized);
                programWasAttachedTo = app.IsInitialized;
            }
            Assert.IsTrue(programWasAttachedTo);
        }

        [Test]
        public void CSiApplication_Initialize_AttachToProcess_of_Nonexisting_Process_Throws_CSiException()
        {
            // This test should wait until all processes are closed.
            Process[] pname = Process.GetProcessesByName(CSiData.processName);
            delayTestStart(until: (pname.Length == 0), attempts: 20, wait: 1000);

            var ex = Assert.Throws<CSiException>(() =>
            {
                using (CSiApplication app = new CSiApplication(numberOfAttempts: 60, intervalBetweenAttempts: 1000))
                {

                }
            });
            Assert.That(ex.Message, Is.EqualTo("No running instance of the program found or failed to attach."));
        }

#endif


        [Test]
        public void CSiApplication_Application_Start_Default_is_Visible()
        {
            bool programIsVisible;
            using (CSiApplication app = new CSiApplication(CSiData.pathApp))
            {
                Assert.That(app.IsInitialized);
                programIsVisible = app.Visible();
            }
            Assert.IsTrue(programIsVisible);
        }

        [Test]
        public void CSiApplication_Application_Start_with_Custom_Units()
        {
            bool programWasOpened;
            eUnits units = eUnits.kgf_mm_C;
            using (CSiApplication app = new CSiApplication(CSiData.pathApp, units: units))
            {
                Assert.That(app.IsInitialized);
                programWasOpened = app.IsInitialized;
                Assert.AreEqual(units, app.Model.GetPresentUnits());
            }
            Assert.IsTrue(programWasOpened);
        }


        [Test]
        public void CSiApplication_Application_Start_with_Invisible_is_Invisible()
        {
            bool programIsVisible = true;
            using (CSiApplication app = new CSiApplication(CSiData.pathApp, visible: false))
            {
                Assert.That(app.IsInitialized);
                programIsVisible = app.Visible();
            }
            Assert.IsFalse(programIsVisible);
        }

        [Test]
        public void CSiApplication_Application_Start_with_Valid_Model_Path()
        {
            bool programWasOpened;
            using (CSiApplication app = new CSiApplication(CSiData.pathApp, modelPath: Path.Combine(CSiData.pathResources, CSiData.pathModelDemo + CSiData.extension)))
            {
                Assert.That(app.IsInitialized);
                programWasOpened = app.IsInitialized;
            }
            Assert.IsTrue(programWasOpened);
        }

        [Test]
        public void CSiApplication_Application_Start_with_Invalid_Model_Path_Throws_CSiException()
        {
            Assert.Throws<CSiException>(() =>
            {
                using (CSiApplication app = new CSiApplication(CSiData.pathApp, modelPath: "FooBar.BadType"))
                {

                }
            });
        }

        [Test]
        public void CSiApplication_Hide_Hidden_Hides()
        {
            using (CSiApplication app = new CSiApplication(CSiData.pathApp, visible: false))
            {
                Assert.That(app.IsInitialized);
                bool programIsVisible = app.Visible();
                Assert.IsFalse(programIsVisible);

                app.Hide();
                programIsVisible = app.Visible();
                Assert.IsFalse(programIsVisible);

                app.Hide();
                programIsVisible = app.Visible();
                Assert.IsFalse(programIsVisible);
            }
        }

        [Test]
        public void CSiApplication_Hide_Visible_Hides()
        {
            using (CSiApplication app = new CSiApplication(CSiData.pathApp, visible: true))
            {
                bool programIsVisible = app.Visible();
                Assert.IsTrue(programIsVisible);

                app.Hide();
                programIsVisible = app.Visible();
                Assert.IsFalse(programIsVisible);
            }
        }


        [Test]
        public void CSiApplication_Show_Hidden_Shows()
        {

            using (CSiApplication app = new CSiApplication(CSiData.pathApp, visible: false))
            {
                bool programIsVisible = app.Visible();
                Assert.IsFalse(programIsVisible);

                app.Unhide();
                programIsVisible = app.Visible();
                Assert.IsTrue(programIsVisible);

                app.Hide();
                programIsVisible = app.Visible();
                Assert.IsFalse(programIsVisible);

                app.Unhide();
                programIsVisible = app.Visible();
                Assert.IsTrue(programIsVisible);
            }
        }

        [Test]
        public void CSiApplication_Show_Visible_Shows()
        {
            using (CSiApplication app = new CSiApplication(CSiData.pathApp, visible: true))
            {
                bool programIsVisible = app.Visible();
                Assert.IsTrue(programIsVisible);

                app.Unhide();
                programIsVisible = app.Visible();
                Assert.IsTrue(programIsVisible);

                app.Unhide();
                programIsVisible = app.Visible();
                Assert.IsTrue(programIsVisible);
            }
        }
    }
}
