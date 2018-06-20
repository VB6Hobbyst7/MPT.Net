using System.Windows.Forms;

using NUnit.Framework;


namespace MPT.FileSystem.UnitTests
{
    public class FilePathTests : LibraryTests_Base
    {
        [Test]
        public void Initialize_Initializes_With_Empty_Properties()
        {
            FilePath filePath = new FilePath();

            Assert.IsEmpty(filePath.Directory);
            Assert.IsEmpty(filePath.FileExtension);
            Assert.IsEmpty(filePath.FileName);
            Assert.IsEmpty(filePath.FileNameWithExtension);
            Assert.IsEmpty(filePath.PathChildStub);
            Assert.IsEmpty(filePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.IsEmpty(filePath.Path(""));
            Assert.IsEmpty(filePath.Path(_assemblyFolder));
            Assert.IsEmpty(filePath.Path(@"C:\Foo\Bar"));
            Assert.IsEmpty(filePath.Path(@"D:\Foo\Bar"));
            
            Assert.IsFalse(filePath.IsDirectoryOnly);
            Assert.IsFalse(filePath.IsFileNameOnly);
            Assert.IsFalse(filePath.IsSelected);
            Assert.IsFalse(filePath.IsValidPath);
        }

        [Test]
        public void Initialize_with_New_Directory_Path_Initializes_With_Directory_Properties()
        {
            string originalPath = _assemblyFolder;
            FilePath filePath = new FilePath(originalPath);

            Assert.AreEqual(originalPath, filePath.Directory);
            Assert.IsEmpty(filePath.FileName);
            Assert.IsEmpty(filePath.FileExtension);
            Assert.IsEmpty(filePath.FileNameWithExtension);
            Assert.IsEmpty(filePath.PathChildStub);
            Assert.AreEqual(originalPath, filePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.AreEqual("", filePath.Path(originalPath));

            string defaultBasePath = Application.StartupPath;
            string returnedTargetPath = originalPath;
            PathLibrary.RelativePath(ref returnedTargetPath, basePath: defaultBasePath);
            Assert.AreEqual(returnedTargetPath, filePath.Path(""));         // By default gives relative path to calling application if no default base path is supplied

            string assemblyPathWithoutDrive = originalPath.Substring(3);
            Assert.AreEqual(@"..\..\" + assemblyPathWithoutDrive, filePath.Path(@"C:\Foo\Bar"));
            Assert.AreEqual(originalPath, filePath.Path(@"D:\Foo\Bar")); // Does not handle different drive letters & defaults to assembly plath

            Assert.IsTrue(filePath.IsDirectoryOnly);
            Assert.IsFalse(filePath.IsFileNameOnly);
            Assert.IsTrue(filePath.IsValidPath);

            Assert.IsFalse(filePath.IsSelected);
            filePath.IsSelected = true;
            Assert.IsTrue(filePath.IsSelected);
        }

        [Test]
        public void Initialize_with_New_Invalid_Directory_Path_Initializes_With_Directory_Properties()
        {
            string originalPath = @"C:\Foo\Bar\Moo\Nar";
            FilePath filePath = new FilePath(originalPath);

            Assert.AreEqual(originalPath, filePath.Directory);
            Assert.IsEmpty(filePath.FileName);
            Assert.IsEmpty(filePath.FileExtension);
            Assert.IsEmpty(filePath.FileNameWithExtension);
            Assert.IsEmpty(filePath.PathChildStub);
            Assert.AreEqual(originalPath, filePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.AreEqual("", filePath.Path(originalPath));

            string defaultBasePath = Application.StartupPath;
            string returnedTargetPath = originalPath;
            PathLibrary.RelativePath(ref returnedTargetPath, basePath: defaultBasePath);
            Assert.AreEqual(returnedTargetPath, filePath.Path(""));         // By default gives relative path to calling application if no default base path is supplied
            
            Assert.AreEqual(@"Moo\Nar", filePath.Path(@"C:\Foo\Bar"));
            Assert.AreEqual(originalPath, filePath.Path(@"D:\Foo\Bar")); // Does not handle different drive letters & defaults to assembly plath

            Assert.IsTrue(filePath.IsDirectoryOnly);
            Assert.IsFalse(filePath.IsFileNameOnly);
            Assert.IsFalse(filePath.IsValidPath);

            Assert.IsFalse(filePath.IsSelected);
            filePath.IsSelected = true;
            Assert.IsTrue(filePath.IsSelected);
        }

        [Test]
        public void Initialize_with_New_File_Path_Initializes_With_File_Properties()
        {
            string fileExtension = "dll";
            string fileName = "MPT.FileSystem.UnitTests";
            string originalDirectory = _assemblyFolder;
            string originalPath = System.IO.Path.Combine(_assemblyFolder, fileName + "." + fileExtension);
            FilePath filePath = new FilePath(originalPath);

            Assert.AreEqual(originalDirectory, filePath.Directory);
            Assert.AreEqual(fileName, filePath.FileName);
            Assert.AreEqual(fileExtension, filePath.FileExtension);
            Assert.AreEqual(fileName + "." + fileExtension, filePath.FileNameWithExtension);
            Assert.IsEmpty(filePath.PathChildStub);
            Assert.AreEqual(originalPath, filePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.AreEqual(fileName + "." + fileExtension, filePath.Path(originalDirectory));

            string defaultBasePath = Application.StartupPath;
            string returnedTargetPath = originalPath;
            PathLibrary.RelativePath(ref returnedTargetPath, basePath: defaultBasePath);
            Assert.AreEqual(returnedTargetPath, filePath.Path(""));         // By default gives relative path to calling application if no default base path is supplied

            string assemblyPathWithoutDrive = originalPath.Substring(3);
            Assert.AreEqual(@"..\..\" + assemblyPathWithoutDrive, filePath.Path(@"C:\Foo\Bar"));
            Assert.AreEqual(originalPath, filePath.Path(@"D:\Foo\Bar")); // Does not handle different drive letters & defaults to assembly plath

            Assert.IsFalse(filePath.IsDirectoryOnly);
            Assert.IsFalse(filePath.IsFileNameOnly);
            Assert.IsTrue(filePath.IsValidPath);

            Assert.IsFalse(filePath.IsSelected);
            filePath.IsSelected = true;
            Assert.IsTrue(filePath.IsSelected);
        }

        [Test]
        public void Initialize_with_New_Invalid_File_Path_Initializes_With_File_Properties()
        {
            string fileExtension = "Bar";
            string fileName = "Foo";
            string originalDirectory = _assemblyFolder;
            string originalPath = System.IO.Path.Combine(_assemblyFolder, fileName + "." + fileExtension);
            FilePath filePath = new FilePath(originalPath);

            Assert.AreEqual(originalDirectory, filePath.Directory);
            Assert.AreEqual(fileName, filePath.FileName);
            Assert.AreEqual(fileExtension, filePath.FileExtension);
            Assert.AreEqual(fileName + "." + fileExtension, filePath.FileNameWithExtension);
            Assert.IsEmpty(filePath.PathChildStub);
            Assert.AreEqual(originalPath, filePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.AreEqual(fileName + "." + fileExtension, filePath.Path(originalDirectory));

            string defaultBasePath = Application.StartupPath;
            string returnedTargetPath = originalPath;
            PathLibrary.RelativePath(ref returnedTargetPath, basePath: defaultBasePath);
            Assert.AreEqual(returnedTargetPath, filePath.Path(""));         // By default gives relative path to calling application if no default base path is supplied

            string assemblyPathWithoutDrive = originalPath.Substring(3);
            Assert.AreEqual(@"..\..\" + assemblyPathWithoutDrive, filePath.Path(@"C:\Foo\Bar"));
            Assert.AreEqual(originalPath, filePath.Path(@"D:\Foo\Bar")); // Does not handle different drive letters & defaults to assembly plath

            Assert.IsFalse(filePath.IsDirectoryOnly);
            Assert.IsFalse(filePath.IsFileNameOnly);
            Assert.IsFalse(filePath.IsValidPath);

            Assert.IsFalse(filePath.IsSelected);
            filePath.IsSelected = true;
            Assert.IsTrue(filePath.IsSelected);
        }


        [Test]
        public void Initialize_with_New_File_Name_Initializes_With_File_Properties()
        {
            string fileExtension = "dll";
            string fileName = "MPT.FileSystem.UnitTests";
            //string originalPath = System.IO.Path.Combine(_assemblyFolder, fileName + "." + fileExtension);
            string defaultBasePath = Application.StartupPath;
            string originalDirectory = defaultBasePath;
            
            FilePath filePath = new FilePath(fileName + "." + fileExtension);
            
            Assert.IsEmpty(filePath.Directory);  
            Assert.AreEqual(fileName, filePath.FileName);
            Assert.AreEqual(fileExtension, filePath.FileExtension);
            Assert.AreEqual(fileName + "." + fileExtension, filePath.FileNameWithExtension);
            Assert.IsEmpty(filePath.PathChildStub);
            Assert.AreEqual(fileName + "." + fileExtension, filePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.AreEqual(fileName + "." + fileExtension, filePath.Path(defaultBasePath));           
            Assert.AreEqual(fileName + "." + fileExtension, filePath.Path(""));         // By default gives relative path to calling application if no default base path is supplied.
            Assert.AreEqual(fileName + "." + fileExtension, filePath.Path(@"C:\Foo\Bar"));
            Assert.AreEqual(fileName + "." + fileExtension, filePath.Path(@"D:\Foo\Bar")); // Does not handle different drive letters & defaults to assembly plath.

            Assert.IsFalse(filePath.IsDirectoryOnly);
            Assert.IsTrue(filePath.IsFileNameOnly);
            Assert.IsFalse(filePath.IsValidPath);

            Assert.IsFalse(filePath.IsSelected);
            filePath.IsSelected = true;
            Assert.IsTrue(filePath.IsSelected);
        }

        [TestCase(@"C:\Foo\Bar", ePathType.FileWithExtension)]
        [TestCase(@"C:\Foo\Bar\Foo.Bar", ePathType.Directory)]
        public void Initialize_with_New_Path_As_Non_Matching_Type_Initializes_With_Empty_Properties(string path, ePathType pathType)
        {

            FilePath filePath = new FilePath(path, pathType);

            Assert.IsEmpty(filePath.Directory);
            Assert.IsEmpty(filePath.FileExtension);
            Assert.IsEmpty(filePath.FileName);
            Assert.IsEmpty(filePath.FileNameWithExtension);
            Assert.IsEmpty(filePath.PathChildStub);
            Assert.IsEmpty(filePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.IsEmpty(filePath.Path(""));
            Assert.IsEmpty(filePath.Path(_assemblyFolder));
            Assert.IsEmpty(filePath.Path(@"C:\Foo\Bar"));
            Assert.IsEmpty(filePath.Path(@"D:\Foo\Bar"));

            Assert.IsFalse(filePath.IsDirectoryOnly);
            Assert.IsFalse(filePath.IsFileNameOnly);
            Assert.IsFalse(filePath.IsSelected);
            Assert.IsFalse(filePath.IsValidPath);
        }

        [TestCase(ePathType.FileAny)]
        [TestCase(ePathType.FileWithoutExtension)]
        public void Initialize_with_New_Path_As_Matching_No_Extension_File_Type_Initializes_With_File_Properties(ePathType pathType)
        {
            string fileExtension = "";
            string fileName = "Bar";
            string originalDirectory = @"C:\Foo";
            string originalPath = System.IO.Path.Combine(originalDirectory, fileName);
            
            FilePath filePath = new FilePath(originalPath, pathType);

            Assert.AreEqual(originalDirectory, filePath.Directory);
            Assert.AreEqual(fileName, filePath.FileName);
            Assert.AreEqual(fileExtension, filePath.FileExtension);
            Assert.AreEqual(fileName + "." + fileExtension, filePath.FileNameWithExtension);
            Assert.IsEmpty(filePath.PathChildStub);
            Assert.AreEqual(originalPath, filePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.AreEqual(fileName + "." + fileExtension, filePath.Path(originalDirectory));

            string defaultBasePath = Application.StartupPath;
            string returnedTargetPath = originalPath;
            PathLibrary.RelativePath(ref returnedTargetPath, basePath: defaultBasePath);
            Assert.AreEqual(returnedTargetPath, filePath.Path(""));         // By default gives relative path to calling application if no default base path is supplied

            string assemblyPathWithoutDrive = originalPath.Substring(3);
            Assert.AreEqual(@"..\..\" + assemblyPathWithoutDrive, filePath.Path(@"C:\Foo\Bar"));
            Assert.AreEqual(originalPath, filePath.Path(@"D:\Foo\Bar")); // Does not handle different drive letters & defaults to assembly plath

            Assert.IsFalse(filePath.IsDirectoryOnly);
            Assert.IsFalse(filePath.IsFileNameOnly);
            Assert.IsTrue(filePath.IsValidPath);

            Assert.IsFalse(filePath.IsSelected);
            filePath.IsSelected = true;
            Assert.IsTrue(filePath.IsSelected);
        }
        
        [Test]
        public void Initialize_with_New_Path_As_Matching_Directory_Type_Initializes_With_Directory_roperties()
        {
            string originalDirectory = @"C:\Foo\Bar\Foo.Bar";
            string originalPath = originalDirectory;

            FilePath filePath = new FilePath(originalPath, ePathType.Directory);

            Assert.AreEqual(originalPath, filePath.Directory);
            Assert.IsEmpty(filePath.FileName);
            Assert.IsEmpty(filePath.FileExtension);
            Assert.IsEmpty(filePath.FileNameWithExtension);
            Assert.IsEmpty(filePath.PathChildStub);
            Assert.AreEqual(originalPath, filePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.AreEqual("", filePath.Path(originalPath));

            string defaultBasePath = Application.StartupPath;
            string returnedTargetPath = originalPath;
            PathLibrary.RelativePath(ref returnedTargetPath, basePath: defaultBasePath);
            Assert.AreEqual(returnedTargetPath, filePath.Path(""));         // By default gives relative path to calling application if no default base path is supplied

            string assemblyPathWithoutDrive = originalPath.Substring(3);
            Assert.AreEqual(@"..\..\" + assemblyPathWithoutDrive, filePath.Path(@"C:\Foo\Bar"));
            Assert.AreEqual(originalPath, filePath.Path(@"D:\Foo\Bar")); // Does not handle different drive letters & defaults to assembly plath

            Assert.IsTrue(filePath.IsDirectoryOnly);
            Assert.IsFalse(filePath.IsFileNameOnly);
            Assert.IsTrue(filePath.IsValidPath);

            Assert.IsFalse(filePath.IsSelected);
            filePath.IsSelected = true;
            Assert.IsTrue(filePath.IsSelected);
        }

        [Test]
        public void Clones_Deep()
        {
            string fileExtension = "dll";
            string fileName = "MPT.FileSystem.UnitTests";
            string originalDirectory = _assemblyFolder;
            string originalPath = System.IO.Path.Combine(_assemblyFolder, fileName + "." + fileExtension);
            FilePath filePath = new FilePath(originalPath);
            filePath.SetPathChildStub("Foo");
            filePath.IsSelected = true;
            FilePath clonedFilePath = filePath.CloneStatic();


            Assert.AreEqual(filePath.Directory, clonedFilePath.Directory);
            Assert.AreEqual(filePath.FileName, clonedFilePath.FileName);
            Assert.AreEqual(filePath.FileExtension, clonedFilePath.FileExtension);
            Assert.AreEqual(filePath.FileNameWithExtension, clonedFilePath.FileNameWithExtension);
            Assert.AreEqual(filePath.PathChildStub, clonedFilePath.PathChildStub); 
            Assert.AreEqual(filePath.Path(), clonedFilePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.AreEqual(filePath.Path(originalDirectory), clonedFilePath.Path(originalDirectory));
            Assert.AreEqual(filePath.Path(""), clonedFilePath.Path(""));        
            Assert.AreEqual(filePath.Path(@"C:\Foo\Bar"), clonedFilePath.Path(@"C:\Foo\Bar"));
            Assert.AreEqual(filePath.Path(@"D:\Foo\Bar"), clonedFilePath.Path(@"D:\Foo\Bar"));

            Assert.AreEqual(filePath.IsDirectoryOnly, clonedFilePath.IsDirectoryOnly);
            Assert.AreEqual(filePath.IsFileNameOnly, clonedFilePath.IsFileNameOnly);
            Assert.AreEqual(filePath.IsValidPath, clonedFilePath.IsValidPath);


            Assert.AreNotEqual(filePath.IsSelected, clonedFilePath.IsSelected);
        }

        [Test]
        public void Implements_ICloneable()
        {
            string fileExtension = "dll";
            string fileName = "MPT.FileSystem.UnitTests";
            string originalDirectory = _assemblyFolder;
            string originalPath = System.IO.Path.Combine(_assemblyFolder, fileName + "." + fileExtension);
            FilePath filePath = new FilePath(originalPath);
            filePath.SetPathChildStub("Foo");
            filePath.IsSelected = true;
            FilePath clonedFilePath = (FilePath)filePath.Clone();


            Assert.AreEqual(filePath.Directory, clonedFilePath.Directory);
            Assert.AreEqual(filePath.FileName, clonedFilePath.FileName);
            Assert.AreEqual(filePath.FileExtension, clonedFilePath.FileExtension);
            Assert.AreEqual(filePath.FileNameWithExtension, clonedFilePath.FileNameWithExtension);
            Assert.AreEqual(filePath.PathChildStub, clonedFilePath.PathChildStub);
            Assert.AreEqual(filePath.Path(), clonedFilePath.Path());

            // Check empty, valid, and invalid relative paths
            Assert.AreEqual(filePath.Path(originalDirectory), clonedFilePath.Path(originalDirectory));
            Assert.AreEqual(filePath.Path(""), clonedFilePath.Path(""));
            Assert.AreEqual(filePath.Path(@"C:\Foo\Bar"), clonedFilePath.Path(@"C:\Foo\Bar"));
            Assert.AreEqual(filePath.Path(@"D:\Foo\Bar"), clonedFilePath.Path(@"D:\Foo\Bar"));

            Assert.AreEqual(filePath.IsDirectoryOnly, clonedFilePath.IsDirectoryOnly);
            Assert.AreEqual(filePath.IsFileNameOnly, clonedFilePath.IsFileNameOnly);
            Assert.AreEqual(filePath.IsValidPath, clonedFilePath.IsValidPath);
            

            Assert.AreNotEqual(filePath.IsSelected, clonedFilePath.IsSelected);
        }

        [Test]
        public void Equals_To_Be_Based_On_Property_Values()
        {
            string fileExtension = "dll";
            string fileName = "MPT.FileSystem.UnitTests";
            string originalPath = System.IO.Path.Combine(_assemblyFolder, fileName + "." + fileExtension);
            FilePath filePath1 = new FilePath(originalPath);
            FilePath filePath2 = new FilePath(originalPath);
            FilePath filePath3 = new FilePath();

            Assert.IsTrue(filePath1.Equals(filePath2));
            Assert.IsTrue(filePath1 == filePath2);

            Assert.IsTrue(filePath1 != filePath3);
            Assert.IsFalse(filePath1.Equals(filePath3));
        }


        [Test]
        public void Overrides_Equals_To_Be_Based_On_Property_Values()
        {
            string fileExtension = "dll";
            string fileName = "MPT.FileSystem.UnitTests";
            string originalPath = System.IO.Path.Combine(_assemblyFolder, fileName + "." + fileExtension);
            object filePath1 = new FilePath(originalPath);
            object filePath2 = new FilePath(originalPath);
            object filePath3 = new FilePath();
            object filePath4 = "FooBar";

            Assert.IsTrue(filePath1.Equals(filePath2));

            Assert.IsFalse(filePath1.Equals(filePath3));
            Assert.IsFalse(filePath1.Equals(filePath4));
        }

        [Test]
        public void Overrides_ToString()
        {
            string fileExtension = "dll";
            string fileName = "MPT.FileSystem.UnitTests";
            string originalPath = System.IO.Path.Combine(_assemblyFolder, fileName + "." + fileExtension);
            object filePath = new FilePath(originalPath);

            Assert.AreEqual("MPT.FileSystem.FilePath - " + originalPath, filePath.ToString());
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void SetPathChildStub_Empty_Returns_Empty(string pathChildStub)
        {
            string fileExtension = "dll";
            string fileName = "MPT.FileSystem.UnitTests";
            string originalPath = System.IO.Path.Combine(_assemblyFolder, fileName + "." + fileExtension);
            FilePath filePath = new FilePath(originalPath);

            filePath.SetPathChildStub(pathChildStub);

            Assert.IsEmpty(filePath.PathChildStub);
        }

        [TestCase("Foo")]
        [TestCase(@"Foo\Bar")]
        [TestCase(@"..\Bar")]
        public void SetPathChildStub_Using_Directory_Path(string pathChildStub)
        {
            string originalPath = System.IO.Path.Combine(_assemblyFolder, pathChildStub);
            FilePath filePath = new FilePath(originalPath);

            filePath.SetPathChildStub(_assemblyFolder);

            Assert.AreEqual(pathChildStub, filePath.PathChildStub);
        }

        [TestCase("Foo")]
        [TestCase(@"Foo\Bar")]
        [TestCase(@"..\Bar")]
        [TestCase(@"Foo\Foo.Bar")]
        public void SetPathChildStub_Using_File_Path(string pathChildStub)
        {
            string fileExtension = "dll";
            string fileName = "MPT.FileSystem.UnitTests";
            string originalPath = System.IO.Path.Combine(_assemblyFolder, pathChildStub, fileName + "." + fileExtension);
            FilePath filePath = new FilePath(originalPath);

            filePath.SetPathChildStub(_assemblyFolder);

            Assert.AreEqual(pathChildStub, filePath.PathChildStub);
        }


        [TestCase("", "", ExpectedResult = "")]
        public void SetProperties(string newPath, string filenameHasExtension)
        {

        }

        [TestCase("", "", "", ExpectedResult = "")]
        public void SetProperties_With_Relative_Reference(string newPath, string filenameHasExtension, string pathRelativeReference)
        {

        }

        [TestCase("", ExpectedResult = "")]
        public void SetProperties(string newPath)
        {

        }

        [TestCase("", "", ePathType.Any)]
        public void SetProperties_Of_Nonmatching_Type_Does_Not_Change_Paths(string oldPath, string newPath, ePathType pathType)
        {

        }

        [TestCase("", "", ExpectedResult = "")]
        public void SetProperties_With_Relative_Reference(string newPath, string pathRelativeReference)
        {

        }
    }
}
