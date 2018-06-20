using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Management;
using System.Management.Instrumentation;

using NUnit.Framework;

namespace MPT.FileSystem.UnitTests
{
    public abstract class FolderLibraryTests_Directories_Base : LibraryTests_Base
    {
        protected string _directoryNameOriginal;
        protected string _directoryOriginal;
        protected string _directoryCopied;       

        [SetUp]
        public override void Init()
        {
            base.Init();
            _directoryNameOriginal = Path.GetRandomFileName();
        }

        [TearDown]
        public override void CleanUp()
        {
            cleanupDirectories(_pathOriginal);
            cleanupDirectories(_pathCopied);
            cleanupDirectories(_directoryOriginal);
            cleanupDirectories(_directoryCopied);
        }

    }

    public class FolderLibraryTests_Querying_Access : FolderLibraryTests_Directories_Base
    {

        protected string _user;
        protected FileSystemRights _fileSystemRights;
        protected string _directoryName;
        protected bool _noPermissionsRestricted;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _user = Environment.UserDomainName + @"\" + Environment.UserName;
            _directoryName = Path.GetRandomFileName(); //"ReadableWriteableDeletableDirectory";
            _pathOriginal = Path.Combine(_assemblyFolder, _directoryName);
            _noPermissionsRestricted = false;

            Directory.CreateDirectory(_pathOriginal);
        }

        [TearDown]
        public override void CleanUp()
        {
            if (!_noPermissionsRestricted)
            {
                RemoveDirectorySecurity(_directoryName, _user, _fileSystemRights, AccessControlType.Deny);
                _noPermissionsRestricted = true;
            }
            else
            {
                _noPermissionsRestricted = false;
            }
            

            base.CleanUp();
        }

        // Adds an ACL entry on the specified directory for the specified account.
        public static void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        // Removes an ACL entry on the specified directory for the specified account.
        public static void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        [TestCase("NonexistingPath")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void ReadableWriteableDeletableDirectory_Of_InvalidPaths_Returns_False(string path)
        {
            _noPermissionsRestricted = true;
            Assert.IsFalse(FolderLibrary.ReadableWriteableDeletableDirectory(path));
        }

        [Test]
        public void ReadableWriteableDeletableDirectory_Of_ReadableWriteableDeletableDirectory_Returns_True()
        {
            _noPermissionsRestricted = true;
            Assert.IsTrue(FolderLibrary.ReadableWriteableDeletableDirectory(_pathOriginal));
        }


        public void ReadableWriteableDeletableDirectory_Of_NotReadableDirectory_Returns_False()
        {
            _fileSystemRights = FileSystemRights.ReadData;
            AddDirectorySecurity(_directoryName, _user, _fileSystemRights, AccessControlType.Deny);
            
            Assert.IsFalse(FolderLibrary.ReadableWriteableDeletableDirectory(_pathOriginal));
        }

        [TestCase(FileSystemRights.Write)]      // False on writing folder
        [TestCase(FileSystemRights.WriteData)]  // False on writing file
        public void ReadableWriteableDeletableDirectory_Of_NotWriteable_Returns_False(FileSystemRights fileSystemRights)
        {
            _fileSystemRights = fileSystemRights;
            AddDirectorySecurity(_directoryName, _user, _fileSystemRights, AccessControlType.Deny);

            Assert.IsFalse(FolderLibrary.ReadableWriteableDeletableDirectory(_pathOriginal));
        }

        [TestCase(FileSystemRights.Write)]      // False on writing folder
        [TestCase(FileSystemRights.WriteData)]  // False on writing file
        public void ReadableWriteableDeletableDirectory_Of_NotWriteable_Events_Log(FileSystemRights fileSystemRights)
        {
            _fileSystemRights = fileSystemRights;
            AddDirectorySecurity(_directoryName, _user, _fileSystemRights, AccessControlType.Deny);

            Assert.IsFalse(FolderLibrary.ReadableWriteableDeletableDirectory(_pathOriginal));
        }

        //[TestCase(FileSystemRights.Delete)]
        //[TestCase(FileSystemRights.DeleteSubdirectoriesAndFiles)]
        public void ReadableWriteableDeletableDirectory_Of_NotDeletableDirectory_Returns_False(FileSystemRights fileSystemRights)
        {
            string subDirName = Path.Combine(_pathOriginal, "ReadableWriteableDeletableDirectory");
            Directory.CreateDirectory(subDirName);

            _fileSystemRights = fileSystemRights;
            AddDirectorySecurity(_directoryName, _user, _fileSystemRights, AccessControlType.Deny);
            AddDirectorySecurity(subDirName, _user, _fileSystemRights, AccessControlType.Deny);

            Assert.IsFalse(FolderLibrary.ReadableWriteableDeletableDirectory(_pathOriginal));
        }
    }

    public class FolderLibraryTests_Querying : FolderLibraryTests_Directories_Base
    {
        [TestCase("NonexistingPath")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void DirectoryContainsFiles_InvalidPaths_Returns_False(string path)
        {
            Assert.IsFalse(FolderLibrary.DirectoryContainsFiles(path));
        }


        [Test]
        public void DirectoryContainsFiles_Containing_Files_Returns_True()
        {
            string directoryName = "DirectoryContainsFiles_Containing_Files_Returns_True";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);

            string fileName = "Sample1.txt";
            var myFile = File.Create(Path.Combine(_pathOriginal, fileName));
            myFile.Close();

            Assert.IsTrue(FolderLibrary.DirectoryContainsFiles(_pathOriginal));
        }

        [Test]
        public void DirectoryContainsFiles_Containing_Only_Directories_Returns_False()
        {
            string directoryName = "DirectoryContainsFiles_Containing_Only_Directories_Returns_False";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);

            string subDirectoryName = "subdirectory1";
            string subPath = Path.Combine(_pathOriginal, subDirectoryName);

            Directory.CreateDirectory(subPath);

            Assert.IsFalse(FolderLibrary.DirectoryContainsFiles(_pathOriginal));
        }

        [Test]
        public void DirectoryContainsFiles_Containing_Directories_That_Contain_Files_Returns_False()
        {
            string directoryName = "DirectoryContainsFiles_Containing_Directories_That_Contain_Files_Returns_False";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);

            string subDirectoryName = "subdirectory1";
            string subPath = Path.Combine(_pathOriginal, subDirectoryName);

            Directory.CreateDirectory(subPath);

            string fileName = "Sample1.txt";
            var myFile = File.Create(Path.Combine(subPath, fileName));
            myFile.Close();

            Assert.IsFalse(FolderLibrary.DirectoryContainsFiles(_pathOriginal));
        }

        [Test]
        public void DirectoryContainsFiles_Containing_Nothing_Returns_False()
        {
            string directoryName = "DirectoryContainsFiles_Containing_Nothing_Returns_False";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);

            Assert.IsFalse(FolderLibrary.DirectoryContainsFiles(_pathOriginal));
        }

        [TestCase("NonexistingPath")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void DirectoryContainsDirectories_InvalidPaths_Returns_False(string path)
        {
            Assert.IsFalse(FolderLibrary.DirectoryContainsDirectories(path));
        }

        [Test]
        public void DirectoryContainsDirectories_Containing_Files_Returns_False()
        {
            string directoryName = "DirectoryContainsFiles_Containing_Files_Returns_True";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);

            string fileName = "Sample1.txt";
            var myFile = File.Create(Path.Combine(_pathOriginal, fileName));
            myFile.Close();

            Assert.IsFalse(FolderLibrary.DirectoryContainsDirectories(_pathOriginal));
        }

        [Test]
        public void DirectoryContainsDirectories_Containing_Only_Directories_Returns_True()
        {
            string directoryName = "DirectoryContainsFiles_Containing_Only_Directories_Returns_False";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);

            string subDirectoryName = "subdirectory1";
            string subPath = Path.Combine(_pathOriginal, subDirectoryName);

            Directory.CreateDirectory(subPath);

            Assert.IsTrue(FolderLibrary.DirectoryContainsDirectories(_pathOriginal));
        }

        [Test]
        public void DirectoryContainsDirectories_Containing_Directories_That_Contain_Files_Returns_True()
        {
            string directoryName = "DirectoryContainsFiles_Containing_Directories_That_Contain_Files_Returns_False";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);

            string subDirectoryName = "subdirectory1";
            string subPath = Path.Combine(_pathOriginal, subDirectoryName);

            Directory.CreateDirectory(subPath);

            string fileName = "Sample1.txt";
            var myFile = File.Create(Path.Combine(subPath, fileName));
            myFile.Close();

            Assert.IsTrue(FolderLibrary.DirectoryContainsDirectories(_pathOriginal));
        }

        [Test]
        public void DirectoryContainsDirectories_Containing_Nothing_Returns_Falsee()
        {
            string directoryName = "DirectoryContainsFiles_Containing_Nothing_Returns_False";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);

            Assert.IsFalse(FolderLibrary.DirectoryContainsDirectories(_pathOriginal));
        }
    }

    public class FolderLibraryTests_Naming : FolderLibraryTests_Directories_Base
    {
        [Test]
        public void RenameFolder_Of_Existing_Folder_Renames_File()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, _directoryNameOriginal);
            Directory.CreateDirectory(_pathOriginal);

            Assert.IsTrue(Directory.Exists(_pathOriginal));

            string newFolderName = "NewFolderName" + Path.GetRandomFileName();
            FolderLibrary.RenameFolder(_pathOriginal, newFolderName);
            _pathCopied = Path.Combine(_assemblyFolder, newFolderName);

            Assert.IsTrue(Directory.Exists(_pathCopied));
        }

        [Test]
        public void RenameFolder_Of_Non_Existing_Folder_Events_Log()
        {
            var wasCalled = false;
            FolderLibrary.Log += (e) => wasCalled = true;

            string originalFolderName = "OriginalFolderName";
            string newFolderName = "NewFolderName";

            FolderLibrary.RenameFolder(originalFolderName, newFolderName);

            Assert.IsTrue(wasCalled);
        }
    }

    public class FolderLibraryTests_ReadOnly_Attributes : FolderLibraryTests_Directories_Base
    {
    }

    public class FolderLibraryTests_Deleting : FolderLibraryTests_Directories_Base
    {
    }

    public class FolderLibraryTests_Copying_Moving : FolderLibraryTests_Directories_Base
    {
    }

    public class FolderLibraryTests_Misc : FolderLibraryTests_Directories_Base
    {
        [TestCase("CreateReplaceDirectoryAndIncrementNameIfDuplicate_Of_Invalid_Folder_Returns_Blank?")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void CreateReplaceDirectoryAndIncrementNameIfDuplicate_Of_Invalid_Folder_Returns_Blank(string path)
        {
            int highestIncrementNumber = 3;
            Assert.AreEqual("", FolderLibrary.CreateReplaceDirectoryAndIncrementNameIfDuplicate(path, highestIncrementNumber));
        }

        [TestCase("CreateReplaceDirectoryAndIncrementNameIfDuplicate_Of_Invalid_Folder_Throws_Log?")]
        public void CreateReplaceDirectoryAndIncrementNameIfDuplicate_Of_Invalid_Folder_Throws_Log(string path)
        {
            var wasCalled = false;
            FolderLibrary.Log += (e) => wasCalled = true;

            _pathOriginal = Path.Combine(_assemblyFolder, _directoryNameOriginal);

            int highestIncrementNumber = 3;

            FolderLibrary.CreateReplaceDirectoryAndIncrementNameIfDuplicate(path, highestIncrementNumber);
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void CreateReplaceDirectoryAndIncrementNameIfDuplicate_Of_NonExisting_Folder_Creates_Folder_Returns_Path()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, _directoryNameOriginal);

            int highestIncrementNumber = 3;
            Assert.AreEqual(_pathOriginal, FolderLibrary.CreateReplaceDirectoryAndIncrementNameIfDuplicate(_pathOriginal, highestIncrementNumber));
        }

        [Test]
        public void CreateReplaceDirectoryAndIncrementNameIfDuplicate_Of_Existing_Folder_Creates_Incremented_Folder_Returns_Path()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, _directoryNameOriginal);
            string pathOriginalSubDirectories = Path.Combine(_pathOriginal, "FirstDir");

            Directory.CreateDirectory(pathOriginalSubDirectories);
            Directory.CreateDirectory(pathOriginalSubDirectories + "1");
            string newIteratedPath = pathOriginalSubDirectories + "2";

            int highestIncrementNumber = 3;
            Assert.AreEqual(newIteratedPath, FolderLibrary.CreateReplaceDirectoryAndIncrementNameIfDuplicate(pathOriginalSubDirectories, highestIncrementNumber));
        }

        [Test]
        public void CreateReplaceDirectoryAndIncrementNameIfDuplicate_Of_Invalid_Increment_Limit_Creates_Incremented_Folder_1_Returns_Path()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, _directoryNameOriginal);
            string pathOriginalSubDirectories = Path.Combine(_pathOriginal, "FirstDir");

            Directory.CreateDirectory(pathOriginalSubDirectories);
            Directory.CreateDirectory(pathOriginalSubDirectories + "1");
            string newIteratedPath = pathOriginalSubDirectories + "1";

            int highestIncrementNumber = -5;
            Assert.AreEqual(newIteratedPath, FolderLibrary.CreateReplaceDirectoryAndIncrementNameIfDuplicate(pathOriginalSubDirectories, highestIncrementNumber));
        }

        [Test]
        public void CreateReplaceDirectoryAndIncrementNameIfDuplicate_Of_Existing_Max_Increment_Folder_Replaces_Folder_Returns_Path()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, _directoryNameOriginal);
            string pathOriginalSubDirectories = Path.Combine(_pathOriginal, "FirstDir");

            Directory.CreateDirectory(pathOriginalSubDirectories);
            Directory.CreateDirectory(pathOriginalSubDirectories + "1");
            Directory.CreateDirectory(pathOriginalSubDirectories + "2");
            Directory.CreateDirectory(pathOriginalSubDirectories + "3");
            string newIteratedPath = pathOriginalSubDirectories + "3";

            int highestIncrementNumber = 3;
            Assert.AreEqual(newIteratedPath, FolderLibrary.CreateReplaceDirectoryAndIncrementNameIfDuplicate(pathOriginalSubDirectories, highestIncrementNumber));
        }
    }

    public class FolderLibraryTests_Component_Functions : FolderLibraryTests_Directories_Base
    {
        [Test]
        public void ComponentCreateDirectory_Creates_Directory()
        {
            string directoryName = "ComponentCreateDirectory_Creates_Directory";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            FolderLibrary.ComponentCreateDirectory(_pathOriginal);

            Assert.IsTrue(Directory.Exists(_pathOriginal));
        }

        [Test]
        public void ComponentCreateDirectory_Events_Log_From_Exception()
        {
            // Set up event listener
            var wasCalled = false;
            FolderLibrary.Log += (e) => wasCalled = true;

            // Create directory with an invalid path
            string directoryName = "ComponentCreateDirectory_Creates_Directory_By_Invalid_Path";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            FolderLibrary.ComponentCreateDirectory(_pathOriginal + "!?");

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ComponentCopyDirectory_Copies_Directory()
        {
            // Create first directory
            string directoryName = "ComponentCopyDirectory_Copies_Directory_Base";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);
            Assert.IsTrue(Directory.Exists(_pathOriginal));

            // Copy directory
            string copiedDirectoryName = "ComponentCopyDirectory_Copies_Directory_Copied";
            _pathCopied = Path.Combine(_assemblyFolder, copiedDirectoryName);

            FolderLibrary.ComponentCopyDirectory(_pathOriginal, _pathCopied);

            Assert.IsTrue(Directory.Exists(_pathCopied));
        }

        [Test]
        public void ComponentCopyDirectory_Events_Log_From_Exception()
        {
            // Create first directory
            string directoryName = "ComponentCopyDirectory_Copies_Directory_Base";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);
            Assert.IsTrue(Directory.Exists(_pathOriginal));
            
            // Set up event listener
            var wasCalled = false;
            FolderLibrary.Log += (e) => wasCalled = true;

            // Copy directory with an invalid path
            string copiedDirectoryName = "ComponentCopyDirectory_Copies_Directory_Copied";
            _pathCopied = Path.Combine(_assemblyFolder, copiedDirectoryName);
            
            FolderLibrary.ComponentCopyDirectory(_pathOriginal, _pathCopied + "!?");
            
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ComponentDeleteDirectoryAction_Deletes_Directory()
        {
            // Create directory
            string directoryName = "ComponentDeleteDirectoryAction_Deletes_Directory_Base";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);
            Assert.IsTrue(Directory.Exists(_pathOriginal));

            // Remove Directory
            FolderLibrary.ComponentDeleteDirectoryAction(_pathOriginal);
            
            Assert.IsFalse(Directory.Exists(_pathOriginal));
        }

        [Test]
        public void ComponentDeleteDirectoryAction_Deletes_Directory_Including_SubDirectories()
        {
            // Create parent directory
            string directoryName = "ComponentDeleteDirectoryAction_Parent";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);
            Assert.IsTrue(Directory.Exists(_pathOriginal));

            // Create child directory
            string subdirectoryName = "ComponentDeleteDirectoryAction_Child";
            string pathSubdirectory = Path.Combine(_pathOriginal, subdirectoryName);

            Directory.CreateDirectory(pathSubdirectory);
            Assert.IsTrue(Directory.Exists(pathSubdirectory));

            // Remove parent directory
            FolderLibrary.ComponentDeleteDirectoryAction(_pathOriginal, removeOtherFilesDirectoriesInPath: true);

            Assert.IsFalse(Directory.Exists(_pathOriginal));
        }

        [Test]
        public void ComponentDeleteDirectoryAction_Deletes_Directory_Including_SubDirectories_And_Files()
        {
            // Create parent directory
            string directoryName = "ComponentDeleteDirectoryAction_Parent";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);
            Assert.IsTrue(Directory.Exists(_pathOriginal));

            // Create child directory
            string subdirectoryName = "ComponentDeleteDirectoryAction_Child";
            string pathSubdirectory = Path.Combine(_pathOriginal, subdirectoryName);

            Directory.CreateDirectory(pathSubdirectory);
            Assert.IsTrue(Directory.Exists(pathSubdirectory));

            // Create file within child directory
            string fileName = "ComponentDeleteDirectoryAction_File.txt";
            string pathFile = Path.Combine(pathSubdirectory, fileName);
            var myFile = File.Create(pathFile);
            myFile.Close();
            
            Assert.IsTrue(File.Exists(pathFile));

            // Remove parent directory
            FolderLibrary.ComponentDeleteDirectoryAction(_pathOriginal, removeOtherFilesDirectoriesInPath: true);

            Assert.IsFalse(Directory.Exists(_pathOriginal));
        }

        [Test]
        public void ComponentDeleteDirectoryAction_Events_Log_From_Exception()
        {
            // Create parent directory
            string directoryName = "ComponentDeleteDirectoryAction_Events_Log_From_Exception_Base";
            _pathOriginal = Path.Combine(_assemblyFolder, directoryName);

            Directory.CreateDirectory(_pathOriginal);
            Assert.IsTrue(Directory.Exists(_pathOriginal));

            // Create child directory
            string subdirectoryName = "ComponentDeleteDirectoryAction_Events_Log_From_Exception_Sub";
            string pathSubdirectory = Path.Combine(_pathOriginal, subdirectoryName);

            Directory.CreateDirectory(pathSubdirectory);
            Assert.IsTrue(Directory.Exists(pathSubdirectory));

            // Set up event listener
            var wasCalled = false;
            FolderLibrary.Log += (e) => wasCalled = true;

            // Remove parent directory
            FolderLibrary.ComponentDeleteDirectoryAction(_pathOriginal, removeOtherFilesDirectoriesInPath: false);

            Assert.IsTrue(wasCalled);
        }
    }

}
