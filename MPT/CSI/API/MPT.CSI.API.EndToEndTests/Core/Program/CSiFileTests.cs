using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;
using NUnit.Framework;

namespace MPT.CSI.API.EndToEndTests.Core.Program
{
    [TestFixture]
    public class CSiFileTests
    {
        protected CSiApplication _app;

        [SetUp]
        public void Setup()
        {
            _app = new CSiApplication(CSiData.pathApp);
        }

        [TearDown]
        public void TearDown()
        {
            _app.Dispose();
        }

        [Test]
        public void Open_Valid_Filepath_Opens_File()
        {
            string path = Path.Combine(CSiData.pathResources, CSiData.pathModelQuery + CSiData.extension);
            bool fileIsOpened = _app.Model.File.Open(path);
            Assert.That(fileIsOpened);
        }

        [Test]
        public void Open_Nonexisting_Filepath_Throws_IOException()
        {
            Assert.Throws<IOException>(() =>
            {
                _app.Model.File.Open(Path.Combine(CSiData.pathResources, "NonexistingFile.txt"));
            });
        }


        [Test]
        public void Open_Invalid_Extension_Filepath_Throws_CSiException()
        {
            bool result = _app.Model.File.Open(Path.Combine(CSiData.pathResources, "InvalidFile.txt"));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ModelIsLoaded_Is_False_For_No_Model_Opened_Or_Initialized()
        {
            Assert.IsFalse(_app.Model.File.ModelIsLoaded());
        }

        [Test]
        public void ModelIsLoaded_Is_False_For_New_Model()
        {
            _app.Model.File.NewBlank();
            Assert.IsFalse(_app.Model.File.ModelIsLoaded());
        }

        [Test]
        public void ModelIsLoaded_Is_True_For_Opened_Model()
        {
            string existingFileName = CSiData.pathModelQuery + CSiData.extension;
            string path = Path.Combine(CSiData.pathResources, existingFileName);
            bool fileIsOpened = _app.Model.File.Open(path);
            Assert.That(fileIsOpened);

            Assert.IsTrue(_app.Model.File.ModelIsLoaded());
        }

        

        [Test]
        public void GetModelFileName_After_Initializing_New_File_Returns_Untitled()
        {
            _app.Model.File.NewBlank();
            string fileName = _app.Model.File.GetModelFileName(includePath: false);

            Assert.AreEqual(CSiFile.NEW_FILE_NAME, fileName);
        }

        [Test]
        public void GetModelFileName_After_Opening_Existing_File_Returns_FileName()
        {
            string existingFileName = CSiData.pathModelQuery + CSiData.extension;
            string path = Path.Combine(CSiData.pathResources, existingFileName);
            bool fileIsOpened = _app.Model.File.Open(path);
            Assert.That(fileIsOpened);

            string fileName = _app.Model.File.GetModelFileName(includePath: false);
            Assert.AreEqual(existingFileName, fileName);
        }


        [Test]
        public void GetModelFileName_With_Full_Path_After_Opening_Existing_File_Returns_Full_Path()
        {
            string path = Path.Combine(CSiData.pathResources, CSiData.pathModelQuery + CSiData.extension);
            bool fileIsOpened = _app.Model.File.Open(path);
            Assert.That(fileIsOpened);

            string fileName = _app.Model.File.GetModelFileName(includePath: true);
            Assert.AreEqual(path, fileName);
        }

        [Test]
        public void GetModelFileName_With_Full_Path_After_Initializing_New_File_Returns_Untitled()
        {
            _app.Model.File.NewBlank();
            string fileName = _app.Model.File.GetModelFileName(includePath: true);

            Assert.AreEqual(CSiFile.NEW_FILE_NAME, fileName);
        }

        [Test]
        public void NewBlank()
        {
            bool noExceptionThrown = false;
            _app.Model.File.NewBlank();
            noExceptionThrown = true;
            Assert.IsTrue(noExceptionThrown);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        [Test]
        public void New2DFrame(e2DFrameType tempType,
            int numberStories,
            double storyHeight,
            int numberBays,
            double bayWidth,
            bool restraint = true,
            string beam = "Default",
            string column = "Default",
            string brace = "Default")
        {
            Assert.IsTrue(false);
        }
#endif
#if BUILD_SAP2000v19 || BUILD_SAP2000v20

        [Test]
        public void New3DFrame(e3DFrameType tempType,
            int numberStories,
            double storyHeight,
            int numberBaysX, int numberBaysY,
            double bayWidthX, double bayWidthY,
            bool restraint = true,
            string beam = "Default",
            string column = "Default",
            string area = "Default",
            int numberXDivisions = 4, int numberYDivisions = 4)
        {
            Assert.IsTrue(false);
        }
        [Test]
        public void NewBeam(int numberSpans,
            double spanLength,
            bool restraint = true,
            string beam = "Default")
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void NewWall(int numberXDivisions,
            int numberZDivisions,
            double divisionWidthX,
            double divisionWidthZ,
            bool restraint = true,
            string area = "Default")
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void NewSolidBlock(double xWidth,
            double yWidth,
            double height,
            bool restraint = true,
            string solid = "Default",
            int numberXDivisions = 5,
            int numberYDivisions = 8,
            int numberZDivisions = 10)
        {
            Assert.IsTrue(false);
        }

#elif BUILD_ETABS2013 || BUILD_ETABS2014 || BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017        
        
        [Test]
        public void NewGridOnly(int numberOfStorys,
                    double typicalStoryHeight,
                    double bottomStoryHeight,
                    int numberLinesX,
                    int numberLinesY,
                    double spacingX,
                    double spacingY)
        {
          Assert.IsTrue(false);
        }

        [Test]
        public void NewSteelDeck(int numberOfStorys,
                    double typicalStoryHeight,
                    double bottomStoryHeight,
                    int numberLinesX,
                    int numberLinesY,
                    double spacingX,
                    double spacingY)
        {
            Assert.IsTrue(false);
        }
#endif
    }

    /// <exclude />
    [TestFixture]
    public class CSiFileTests_Save : BaseTests
    {
        protected CSiApplication _app;

        [SetUp]
        public void Setup()
        {
            // This test should wait until all processes are closed to avoid naming interference.
            // TODO: This could be streamlined in the future with temp files?
            Process[] pname = Process.GetProcessesByName(CSiData.processName);
            delayTestStart(until: (pname.Length == 0), attempts: 20, wait: 1000);

            _app = new CSiApplication(CSiData.pathApp, 
                numberOfExitAttempts: 60, 
                intervalBetweenExitAttempts: 1000);
        }

        [TearDown]
        public void TearDown()
        {
            _app.Dispose();
        }

        
        [Test]
        public void Save_Saved_Saves()
        {
            string pathOpen = Path.Combine(CSiData.pathResources, CSiData.pathModelSave + CSiData.extension);
            _app.Model.File.Open(pathOpen);
            bool fileIsSaved = _app.Model.File.Save();

            Assert.That(fileIsSaved);
        }

        [Test]
        public void Save_Saved_with_New_FilePath_Saves()
        {
            string pathOpen = Path.Combine(CSiData.pathResources, CSiData.pathModelSave + CSiData.extension);
            _app.Model.File.Open(pathOpen);

            string pathSave = Path.Combine(CSiData.pathResources, "Temp" + CSiData.extension);
            bool fileIsSaved = _app.Model.File.Save(pathSave);
            File.Delete(pathSave);

            Assert.That(fileIsSaved);
        }

        [Test]
        public void Save_Saved_with_Same_FilePath_Saves()
        {
            string pathOpen = Path.Combine(CSiData.pathResources, CSiData.pathModelSave + CSiData.extension);
            _app.Model.File.Open(pathOpen);
            bool fileIsSaved = _app.Model.File.Save(pathOpen);

            Assert.That(fileIsSaved);
        }

        [Test]
        public void Save_Saved_with_Same_ReadOnly_FilePath_Throws_IOException()
        {
            string pathOpen = Path.Combine(CSiData.pathResources, CSiData.pathModelSaveReadOnly + CSiData.extension);
            _app.Model.File.Open(pathOpen);
            var ex = Assert.Throws<IOException>(() => _app.Model.File.Save(pathOpen));
            Assert.That(ex.Message, Is.EqualTo("The saved path provided is for a read only file.\n Please change the file access or provide a different file name."));
        }

        [Test]
        public void Save_Unsaved_Throws_IOException()
        {
            _app.Model.File.NewBlank();
            var ex = Assert.Throws<IOException>(() => _app.Model.File.Save());
            Assert.That(ex.Message, Is.EqualTo("The current model has not been previously saved. Please provide a file name."));
        }


        [Test]
        public void Save_Unsaved_with_New_FilePath_Saves()
        {
            string pathSave = Path.Combine(CSiData.pathResources, "Temp" + CSiData.extension);
            _app.Model.File.NewBlank();

            bool fileIsSaved = _app.Model.File.Save(pathSave);
            File.Delete(pathSave);

            Assert.IsTrue(fileIsSaved);
        }
        
    }

    /// <exclude />
    [TestFixture]
    public class CSiFileTests_Separate_Process : BaseTests
    {
        protected CSiApplication _app;

        [SetUp]
        public void Setup()
        {
            // This test should wait until all processes are closed, as tests in this class involve attaching to processes.
            Process[] pname = Process.GetProcessesByName(CSiData.processName);
            delayTestStart(until: (pname.Length == 0), attempts: 20, wait: 1000);
        }

        [TearDown]
        public void TearDown()
        {
            _app.ApplicationExit(fileSave: false, numberOfAttempts: 60, intervalBetweenAttempts: 1000); // Called explicitly to handle applications started in a separate process.
            _app.Dispose();
        }

        [Test]
        public void GetModelFileName_Is_Null_For_Opened_Model_While_Still_Initializing_Application()
        {
            string pathModel = Path.Combine(CSiData.pathResources, CSiData.pathModelDemo + CSiData.extension);
            ProcessStartInfo processInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = CSiData.pathApp,
                Arguments = "\"" + pathModel + "\""
            };
            Process.Start(processInfo);

            _app = new CSiApplication(numberOfAttempts: 60, intervalBetweenAttempts: 1000,
                                      numberOfExitAttempts: 60, intervalBetweenExitAttempts: 1000);
            Assert.That(string.IsNullOrEmpty(_app.Model.File.FileName));

            string fileName = _app.Model.File.GetModelFileName(includePath: false);
            Assert.IsNull(fileName);
        }

        [Test]
        public void ModelIsLoaded_Is_False_For_Opened_Model_While_Still_Initializing_Application()
        {
            string pathModel = Path.Combine(CSiData.pathResources, CSiData.pathModelDemo + CSiData.extension);
            ProcessStartInfo processInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = CSiData.pathApp,
                Arguments = "\"" + pathModel + "\""
            };
            Process.Start(processInfo);

            _app = new CSiApplication(numberOfAttempts: 60, intervalBetweenAttempts: 1000,
                                      numberOfExitAttempts: 60, intervalBetweenExitAttempts: 1000);
            Assert.That(string.IsNullOrEmpty(_app.Model.File.FileName));

            Assert.IsFalse(_app.Model.File.ModelIsLoaded());
        }

        [Test]
        public void UpdateModelFileName()
        {
            string pathModel = Path.Combine(CSiData.pathResources, CSiData.pathModelDemo + CSiData.extension);
            ProcessStartInfo processInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = CSiData.pathApp,
                Arguments = "\"" + pathModel + "\""
            };
            Process.Start(processInfo);
            
            _app = new CSiApplication(numberOfAttempts: 60, intervalBetweenAttempts: 1000,
                                      numberOfExitAttempts: 60, intervalBetweenExitAttempts: 1000);
            Assert.That(string.IsNullOrEmpty(_app.Model.File.FileName));

            while (!_app.Model.File.ModelIsLoaded())
            {
                // Waits until model is fully loaded.
            }

            string fileName = _app.Model.File.UpdateModelFileName(includePath: false);
            Assert.That(fileName, Is.EqualTo(Path.GetFileName(pathModel)));
            Assert.That(_app.Model.File.FileName, Is.EqualTo(fileName));
        }

        [Test]
        public void UpdateModelFileName_IncludePath_Returns_Full_Path()
        {
            string pathModel = Path.Combine(CSiData.pathResources, CSiData.pathModelDemo + CSiData.extension);
            ProcessStartInfo processInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = CSiData.pathApp,
                Arguments = "\"" + pathModel + "\""
            };
            Process.Start(processInfo);

            _app = new CSiApplication(numberOfAttempts: 60, intervalBetweenAttempts: 1000,
                                      numberOfExitAttempts: 60, intervalBetweenExitAttempts: 1000);
            Assert.That(string.IsNullOrEmpty(_app.Model.File.FileName));

            while (!_app.Model.File.ModelIsLoaded())
            {
                // Waits until model is fully loaded.
            }

            string fileName = _app.Model.File.UpdateModelFileName(includePath: true);
            Assert.That(fileName, Is.EqualTo(pathModel));

            string fileNameAndPathFromProperties = Path.Combine(_app.Model.File.FilePath, _app.Model.File.FileName); 
            Assert.That(fileNameAndPathFromProperties, Is.EqualTo(fileName));
        }

        [Test]
        public void UpdateModelFilePath()
        {
            string pathModel = Path.Combine(CSiData.pathResources, CSiData.pathModelDemo + CSiData.extension);
            ProcessStartInfo processInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = CSiData.pathApp,
                Arguments = "\"" + pathModel + "\""
            };
            Process.Start(processInfo);

            _app = new CSiApplication(numberOfAttempts: 60, intervalBetweenAttempts: 1000,
                                      numberOfExitAttempts: 60, intervalBetweenExitAttempts: 1000);
            Assert.That(string.IsNullOrEmpty(_app.Model.File.FilePath));

            while (!_app.Model.File.ModelIsLoaded())
            {
                // Waits until model is fully loaded.
            }

            string filePath = _app.Model.File.UpdateModelFilePath();
            Assert.That(filePath, Is.EqualTo(CSiData.pathResources + "\\"));

            Assert.That(_app.Model.File.FilePath, Is.EqualTo(filePath));
        }
    }
}
