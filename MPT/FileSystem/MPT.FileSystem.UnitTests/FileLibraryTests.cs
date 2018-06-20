using System.IO;
using NUnit.Framework;


namespace MPT.FileSystem.UnitTests
{
    public abstract class FileLibraryTests_Base : LibraryTests_Base
    {
        protected string _fileNameOriginal = "OriginalFile.txt";

        [SetUp]
        public override void Init()
        {
            base.Init();

            _pathOriginal = Path.Combine(_assemblyFolder, _fileNameOriginal);

            //ncrunch: no coverage start
            if (!File.Exists(_pathOriginal))
            {
                var myFile = File.Create(_pathOriginal);
                myFile.Close();
            }
            //ncrunch: no coverage end
        }

        [TearDown]
        public override void CleanUp()
        {
            cleanupFiles(_pathOriginal);
            cleanupFiles(_pathCopied);
        }
    }

    public class FileLibraryTests_Rename : FileLibraryTests_Base
    {
        [Test]
        public void RenameFile_Of_Existing_File_Renames_File()
        {
            string originalFilename = "OriginalFileName.txt";
            _pathCopied = Path.Combine(_assemblyFolder, originalFilename);
            var myFile = File.Create(_pathCopied);
            myFile.Close();

            Assert.IsTrue(File.Exists(_pathCopied));

            string newFileName = "NewFileName.txt";
            FileLibrary.RenameFile(_pathCopied, newFileName);
            string newPath = Path.Combine(_assemblyFolder, newFileName);

            Assert.IsTrue(File.Exists(newPath));
        }

        [Test]
        public void RenameFile_Of_Non_Existing_File_Events_Log()
        {
            var wasCalled = false;
            FileLibrary.Log += (e) => wasCalled = true;

            string originalFilename = "OriginalNotExistingFileName.txt";
            string newFileName = "NewFileName.txt";

            FileLibrary.RenameFile(originalFilename, newFileName);

            Assert.IsTrue(wasCalled);
        }
    }

    public class FileLibraryTests_Copy : FileLibraryTests_Base
    {
        [Test]
        public void ComponentCopyFileAction_Copies_New_File_Under_New_Name()
        {
            string fileNameCopied = "ComponentCopyFileAction_Copies_New_File_Under_New_Name.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);

            Assert.IsTrue(File.Exists(_pathCopied));
        }

        [Test]
        public void ComponentCopyFileAction_Copies_New_File_Under_Old_Name_To_New_Directory()
        {
            string newDirectory = Path.Combine(_assemblyFolder, "newDirectory");
            _pathCopied = Path.Combine(newDirectory, _fileNameOriginal);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);

            Assert.IsTrue(File.Exists(_pathCopied));
        }

        [Test]
        public void ComponentCopyFileAction_Copies_File_Overwriting()
        {
            // Copy First File
            string fileNameCopied = "ComponentCopyFileAction_Copies_File_Overwriting.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);

            // Test that file exists and modify file
            Assert.IsTrue(File.Exists(_pathCopied));

            using (StreamWriter sw = File.AppendText(_pathCopied))
            {
                sw.WriteLine("Test Text");
            }

            // Copy second time, and check that modification is no longer present
            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: true);
            string[] readText = File.ReadAllLines(_pathCopied);
            int expectedNumberOfLines = 0;


            Assert.AreEqual(expectedNumberOfLines, readText.Length);
        }

        [Test]
        public void ComponentCopyFileAction_Copies_NonExisting_File()
        {
            string nonExistingFilePath = Path.Combine(_assemblyFolder, "NonExistingFile.txt");

            string fileNameCopied = "ComponentCopyFileAction_Copies_NonExisting_File.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(nonExistingFilePath, _pathCopied, overWriteFile: false);

            Assert.IsFalse(File.Exists(_pathCopied));
        }

        [Test]
        public void ComponentCopyFileAction_Copies_Blank_Source_Path()
        {
            string fileNameCopied = "ComponentCopyFileAction_Copies_Blank_Source_Path.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);
            FileLibrary.ComponentCopyFileAction("", _pathCopied, overWriteFile: false);

            Assert.IsFalse(File.Exists(_pathCopied));
        }

        [Test]
        public void ComponentCopyFileAction_Copies_Blank_Destination_Path()
        {
            FileLibrary.ComponentCopyFileAction(_pathOriginal, "", overWriteFile: false);

            Assert.IsFalse(File.Exists(_pathCopied));
        }

        [Test]
        public void ComponentCopyFileAction_Copies_File_Where_Source_Equals_Destination()
        {
            // Copy First File
            string fileNameCopied = "ComponentCopyFileAction_Copies_File_Where_Source_Equals_Destination.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);

            // Test that file exists and modify file
            bool fileExists = File.Exists(_pathCopied);
            Assert.IsTrue(fileExists);

            using (StreamWriter sw = File.AppendText(_pathCopied))
            {
                sw.WriteLine("Test Text");
            }

            // Copy second time, and check that modification is no longer present
            FileLibrary.ComponentCopyFileAction(_pathCopied, _pathCopied, overWriteFile: true);
            string[] readText = File.ReadAllLines(_pathCopied);
            int expectedNumberOfLines = 1;

            Assert.AreEqual(expectedNumberOfLines, readText.Length);
        }

        [Test]
        public void ComponentCopyFileAction_Copies_File_Not_Overwriting_With_No_Message()
        {
            // Copy First File
            string fileNameCopied = "ComponentCopyFileAction_Copies_File_Not_Overwriting_With_No_Message.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);

            // Test that file exists and modify file
            bool fileExists = File.Exists(_pathCopied);
            Assert.IsTrue(fileExists);

            using (StreamWriter sw = File.AppendText(_pathCopied))
            {
                sw.WriteLine("Test Text");
            }

            // Copy second time, and check that modification is no longer present
            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);
            string[] readText = File.ReadAllLines(_pathCopied);
            int expectedNumberOfLines = 1;

            Assert.AreEqual(expectedNumberOfLines, readText.Length);
        }


        [Test]
        public void ComponentCopyFileAction_Copies_File_Not_Overwriting_Events_Message()
        {
            // Copy First File
            string fileNameCopied = "ComponentCopyFileAction_Copies_File_Not_Overwriting_With_Message_Event.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);

            // Test that file exists and modify file
            bool fileExists = File.Exists(_pathCopied);
            Assert.IsTrue(fileExists);

            using (StreamWriter sw = File.AppendText(_pathCopied))
            {
                sw.WriteLine("Test Text");
            }

            // Set up event listener
            var wasCalled = false;
            FileLibrary.Messenger += (e) => wasCalled = true;

            // Copy second time
            string expectedPromptText = "File Cannot be Overwritten";
            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied,
                overWriteFile: false,
                promptMessage: expectedPromptText);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ComponentCopyFileAction_Events_Log_From_Exception()
        {
            // Set up event listener
            var wasCalled = false;
            FileLibrary.Log += (e) => wasCalled = true;

            FileLibrary.ComponentCopyFileAction(_pathOriginal, "", overWriteFile: false);

            Assert.IsTrue(wasCalled);
        }
    }

    public class FileLibraryTests_Delete : FileLibraryTests_Base
    {
        [Test]
        public void ComponentDeleteFileAction_Deletes_Existing_File()
        {
            string fileNameCopied = "ComponentDeleteFileAction_Deletes_Existing_File.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);
            Assert.IsTrue(File.Exists(_pathCopied));

            FileLibrary.ComponentDeleteFileAction(_pathCopied);
            Assert.IsFalse(File.Exists(_pathCopied));
        }

        [Test]
        public void ComponentDeleteFileAction_Handles_NonExisting_File()
        {
            string fileNameCopied = "ComponentDeleteFileAction_Handles_NonExisting_File.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            Assert.IsFalse(File.Exists(_pathCopied));

            FileLibrary.ComponentDeleteFileAction(_pathCopied);
            Assert.IsFalse(File.Exists(_pathCopied));
        }

        [Test]
        public void ComponentDeleteFileAction_Events_Log_From_Exception()
        {
            string fileNameCopied = "ComponentDeleteFileAction_Events_Log_From_Exception.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);
            Assert.IsTrue(File.Exists(_pathCopied));

            File.SetAttributes(_pathCopied, FileAttributes.ReadOnly);

            // Set up event listener
            var wasCalled = false;
            FileLibrary.Log += (e) => wasCalled = true;

            FileLibrary.ComponentDeleteFileAction(_pathCopied);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void DeleteFile_Not_Existing_Returns_True()
        {
            string fileNameCopied = "DeleteFile_Not_Existing_Returns_True.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            Assert.IsFalse(File.Exists(_pathCopied));
            Assert.IsTrue(FileLibrary.DeleteFile(_pathCopied));
        }

        [Test]
        public void DeleteFile_Existing_Deletes_File_And_Returns_True()
        {
            //string fileNameCopied = "DeleteFile_Existing_Deletes_File_And_Returns_True.txt";
            string fileNameCopied = "DeleteFile_1.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: true);

            Assert.IsTrue(File.Exists(_pathCopied));

            Assert.IsTrue(FileLibrary.DeleteFile(_pathCopied));
            Assert.IsFalse(File.Exists(_pathCopied));
        }

        [Test]
        public void DeleteFile_Existing_ReadOnly_Not_Overwritten_Does_Not_Delete_File_And_Returns_False()
        {
            string fileNameCopied = "DeleteFile_Existing_ReadOnly_Not_Overwritten_Does_Not_Delete_File_And_Returns_False.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);
            Assert.IsTrue(File.Exists(_pathCopied));

            File.SetAttributes(_pathCopied, FileAttributes.ReadOnly);

            Assert.IsFalse(FileLibrary.DeleteFile(_pathCopied));
            Assert.IsTrue(File.Exists(_pathCopied));
        }

        [Test]
        public void DeleteFile_Existing_ReadOnly_Overwritten_Deletes_File_And_Returns_True()
        {
            //string fileNameCopied = "DeleteFile_Existing_ReadOnly_Overwritten_Deletes_File_And_Returns_True.txt";
            string fileNameCopied = "DeleteFile_2.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            FileLibrary.ComponentCopyFileAction(_pathOriginal, _pathCopied, overWriteFile: false);
            Assert.IsTrue(File.Exists(_pathCopied));

            File.SetAttributes(_pathCopied, FileAttributes.ReadOnly);

            Assert.IsTrue(FileLibrary.DeleteFile(_pathCopied, includeReadOnly: true));
            Assert.IsFalse(File.Exists(_pathCopied));
        }
    }

    public class FileLibraryTests_Attributes : FileLibraryTests_Base
    {
        [Test]
        public void ComponentSetAttributeAction_For_Existing_File()
        {
            // Set up temporary sample file to alter
            string fileNameCopied = "ComponentSetAttributeAction_For_Existing_File.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);
            File.Copy(_pathOriginal, _pathCopied);

            FileLibrary.ComponentSetAttributeAction(_pathCopied, FileAttributes.ReadOnly);

            Assert.AreEqual(FileAttributes.ReadOnly, (File.GetAttributes(_pathCopied) & FileAttributes.ReadOnly));
        }

        [Test]
        public void ComponentSetAttributeAction_For_NonExisting_File()
        {
            string fileNameCopied = "ComponentSetAttributeAction_For_NonExisting_File.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            Assert.DoesNotThrow(() => FileLibrary.ComponentSetAttributeAction(_pathCopied, FileAttributes.ReadOnly));
        }

        [Test]
        public void ComponentRemoveAttributeAction_For_Existing_File()
        {
            // Set up temporary sample file to alter
            string fileNameCopied = "ComponentRemoveAttributeAction_For_Existing_File.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);
            File.Copy(_pathOriginal, _pathCopied);

            File.SetAttributes(_pathCopied, FileAttributes.ReadOnly);

            FileInfo infoReader = new FileInfo(_pathCopied);
            Assert.IsTrue(infoReader.IsReadOnly);

            FileLibrary.ComponentRemoveAttributeAction(_pathCopied, FileAttributes.ReadOnly);
            infoReader = new FileInfo(_pathCopied);
            Assert.IsFalse(infoReader.IsReadOnly);
        }
        
        [Test]
        public void ComponentRemoveAttributeAction_For_NonExisting_File()
        {
            // Set up temporary sample file to alter
            string fileNameCopied = "ComponentRemoveAttributeAction_For_NonExisting_File.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);

            Assert.DoesNotThrow(() => FileLibrary.ComponentRemoveAttributeAction(_pathCopied, FileAttributes.ReadOnly));
        }

        [Test]
        public void SetFileNotReadOnly_For_Existing_File()
        {
            // Set up temporary sample file to alter
            string fileNameCopied = "SetFileNotReadOnly_For_Existing_File.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);
            File.Copy(_pathOriginal, _pathCopied);

            FileInfo infoReader = new FileInfo(_pathCopied);
            Assert.IsFalse(infoReader.IsReadOnly);

            File.SetAttributes(_pathCopied, FileAttributes.ReadOnly);
            infoReader = new FileInfo(_pathCopied);
            Assert.IsTrue(infoReader.IsReadOnly);

            FileLibrary.SetFileNotReadOnly(_pathCopied);

            infoReader = new FileInfo(_pathCopied);
            Assert.IsFalse(infoReader.IsReadOnly);
        }

        [Test]
        public void IsFileReadOnly_For_Readable_File_Returns_False()
        {
            // Set up temporary sample file to alter
            string fileNameCopied = "IsReadableFileReadOnly.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);
            File.Copy(_pathOriginal, _pathCopied);

            Assert.IsFalse(FileLibrary.IsFileReadOnly(_pathCopied));
        }

        [Test]
        public void IsFileReadOnly_For_ReadOnly_File_Returns_True()
        {
            // Set up temporary sample file to alter
            string fileNameCopied = "IsReadOnlyFileReadOnly.txt";
            _pathCopied = Path.Combine(_assemblyFolder, fileNameCopied);
            File.Copy(_pathOriginal, _pathCopied);

            File.SetAttributes(_pathCopied, FileAttributes.ReadOnly);

            Assert.IsTrue(FileLibrary.IsFileReadOnly(_pathCopied));
        }
    }

}
