using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using WinForms = System.Windows.Forms;
using Win32Forms = Microsoft.Win32;

using NUnit.Framework;

namespace MPT.FileSystem.UnitTests
{
    [TestFixture]
    public class BrowseLibraryTests
    {
        [Test]
        public void BrowseForFilesFactory_with_Defaults_Returns_Folder_Browser_with_Default_Properties()
        {
            Win32Forms.OpenFileDialog dialog = BrowseLibrary.BrowseForFilesFactory();

            Assert.AreEqual("", dialog.DefaultExt);
            Assert.AreEqual("All Files" + "|*.*", dialog.Filter);
            Assert.AreEqual("", dialog.InitialDirectory);
            Assert.AreEqual(false, dialog.Multiselect);
            Assert.AreEqual("Browse for File", dialog.Title);
        }

        [Test]
        public void BrowseForFilesFactory_Returns_Folder_Browser_with_Valid_Properties()
        {
            string initialDirectory = @"C:\Foo\Bar";
            string fileTypeLabel = "FooBar Files";
            bool multiSelect = false;

            Win32Forms.OpenFileDialog dialog = BrowseLibrary.BrowseForFilesFactory(initialDirectory,
                                                                                    fileTypeLabel,
                                                                                    multiSelect: multiSelect);

            Assert.AreEqual("", dialog.DefaultExt);
            Assert.AreEqual("FooBar Files" + "|*.*", dialog.Filter);
            Assert.AreEqual(initialDirectory, dialog.InitialDirectory);
            Assert.AreEqual(multiSelect, dialog.Multiselect);
            Assert.AreEqual("Browse for File", dialog.Title);
        }

        [Test]
        public void BrowseForFilesFactory_with_Empty_FileTypes_Returns_Folder_Browser_with_Valid_Properties()
        {
            string initialDirectory = @"C:\Foo\Bar";
            bool multiselect = false;
            List<string> fileTypes = new List<string>() {};

            Win32Forms.OpenFileDialog dialog = BrowseLibrary.BrowseForFilesFactory(initialDirectory,
                                                                                    "",
                                                                                    fileTypes,
                                                                                    multiselect);

            Assert.AreEqual("", dialog.DefaultExt);
            Assert.AreEqual("All Files" + "|*.*", dialog.Filter);
            Assert.AreEqual(initialDirectory, dialog.InitialDirectory);
            Assert.AreEqual(multiselect, dialog.Multiselect);
            Assert.AreEqual("Browse for File", dialog.Title);
        }

        [Test]
        public void BrowseForFilesFactory_with_Multi_FileTypes_Returns_Folder_Browser_with_Valid_Properties()
        {
            string initialDirectory = @"C:\Foo\Bar";
            bool multiselect = false;
            List<string> fileTypes = new List<string>() { "Foo", "Bar" };

            Win32Forms.OpenFileDialog dialog = BrowseLibrary.BrowseForFilesFactory(initialDirectory,
                                                                                    "",
                                                                                    fileTypes,
                                                                                    multiselect);

            Assert.AreEqual("Foo", dialog.DefaultExt);
            Assert.AreEqual("Files " + "(*.Foo)|*.Foo|" + "Files " + "(*.Bar)|*.Bar", dialog.Filter);
            Assert.AreEqual(initialDirectory, dialog.InitialDirectory);
            Assert.AreEqual(multiselect, dialog.Multiselect);
            Assert.AreEqual("Browse for File", dialog.Title);
        }

        [Test]
        public void BrowseForFilesFactory_with_Multi_Select_Returns_Folder_Browser_with_Valid_Properties()
        {
            string initialDirectory = @"C:\Foo\Bar";
            string fileTypeLabel = "FooBar Files";
            bool multiSelect = true;

            Win32Forms.OpenFileDialog dialog = BrowseLibrary.BrowseForFilesFactory(initialDirectory,
                                                                                    fileTypeLabel,
                                                                                    multiSelect: multiSelect);

            Assert.AreEqual("", dialog.DefaultExt);
            Assert.AreEqual(fileTypeLabel + "|*.*", dialog.Filter);
            Assert.AreEqual(initialDirectory, dialog.InitialDirectory);
            Assert.AreEqual(multiSelect, dialog.Multiselect);
            Assert.AreEqual("Browse for Files", dialog.Title);
        }

        [Test]
        public void BrowseForFolderFactory_with_Defaults_Returns_Folder_Browser_with_Default_Properties()
        {
            WinForms.FolderBrowserDialog dialog = BrowseLibrary.BrowseForFolderFactory();

            Assert.AreEqual("", dialog.Description);
            Assert.AreEqual("", dialog.SelectedPath);
            Assert.AreEqual(false, dialog.ShowNewFolderButton);
        }

        [Test]
        public void BrowseForFolderFactory_Returns_Folder_Browser_with_Valid_Properties()
        {
            string description = "Description";
            string selectedPath = @"C:\Foo\Bar";
            bool showNewFolderButton = true;

            WinForms.FolderBrowserDialog dialog = BrowseLibrary.BrowseForFolderFactory(description, 
                                                                                        selectedPath, 
                                                                                        showNewFolderButton);

            Assert.AreEqual(description, dialog.Description);
            Assert.AreEqual(selectedPath, dialog.SelectedPath);
            Assert.AreEqual(showNewFolderButton, dialog.ShowNewFolderButton);
        }

        [Test]
        public void BrowseForFolderFactory_with_Dirty_Starting_Path_Returns_Folder_Browser_with_Valid_Properties()
        {
            string description = "Description";
            string selectedPathDirty = @"  \C:\\Foo\\Bar\  ";
            string selectedPathClean = @"C:\Foo\Bar";
            bool showNewFolderButton = true;

            WinForms.FolderBrowserDialog dialog = BrowseLibrary.BrowseForFolderFactory(description,
                                                                                        selectedPathDirty,
                                                                                        showNewFolderButton);

            Assert.AreEqual(description, dialog.Description);
            Assert.AreEqual(selectedPathClean, dialog.SelectedPath);
            Assert.AreEqual(showNewFolderButton, dialog.ShowNewFolderButton);
        }

        [Test]
        public void OpenExplorerAtFolder_with_NonExisting_Directory_Events_Message()
        {
            var wasCalled = false;
            BrowseLibrary.Messenger += (e) => wasCalled = true;

            string nonExistingPath = @"C:\Foo\Bar";
            BrowseLibrary.OpenExplorerAtFolder(nonExistingPath);
            
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OpenExplorerAtFile_with_NonExisting_File_Events_Message()
        {
            var wasCalled = false;
            BrowseLibrary.Messenger += (e) => wasCalled = true;

            string existingFolderPath = Application.StartupPath;
            string nonExistingFile = @"Foo.Bar";
            string nonExistingFilePath = Path.Combine(existingFolderPath, nonExistingFile);
            BrowseLibrary.OpenExplorerAtFile(nonExistingFilePath);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OpenExplorerAtFile_with_NonExisting_Directory_Events_Message()
        {
            var wasCalled = false;
            BrowseLibrary.Messenger += (e) => wasCalled = true;

            string nonExistingPath = @"C:\Foo\Bar";
            BrowseLibrary.OpenExplorerAtFile(nonExistingPath);

            Assert.IsTrue(wasCalled);
        }
    }
}
