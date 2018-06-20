using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

using NUnit.Framework;



namespace MPT.FileSystem.UnitTests
{
    [TestFixture]
    public class PathLibraryTests_Relative_Absolute_Paths
    {
        // Note: basePath is never tested with a path to a file. 
        // This is because paths generated need not currently exist, which means it is impossible to tell if a name is for a file or directory.
        [TestCase("","", ExpectedResult ="")]
        [TestCase("", null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", "", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", " ", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", null, ExpectedResult = "")]
        [TestCase("", @"C:\Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = "")]
        [TestCase(" ", @"C:\Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = "")]
        [TestCase(null, @"C:\Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = "")]
        [TestCase(@"\Foo\Bar\Moo\Nar", null, ExpectedResult = "")]
        [TestCase(@"\Foo\Bar\Moo\Nar", "ChildDir", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar", ExpectedResult = "")] // Same location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = "ChildDir")] // Child location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar", ExpectedResult = @"..\..")] // Parent location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", ExpectedResult = @"..\..\Soo\War")] // Lateral location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Bar\Foo\Soo\War", ExpectedResult = @"..\..\Bar\Foo\Soo\War")] // Repeating directory names
        [TestCase(@"C:\Foo Bar\Moo\Nar", @"C:\Foo Bar\Bar\Foo\Soo War", ExpectedResult = @"..\..\Bar\Foo\Soo War")] // Directory names with spaces
        [TestCase(@"   C:\Foo\Bar\Moo\\Nar\ ", @"     C:\Foo\Bar\\Soo\War\ ", ExpectedResult = @"..\..\Soo\War")] // Dirty path
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Soo\War", ExpectedResult = @"..\..\..\..\Soo\War")] // Lateral location from root
        public string ConvertPathAbsoluteToRelative_Of_Directory(string basePath, string targetPath)
        {
            return PathLibrary.ConvertPathAbsoluteToRelative(basePath, targetPath);
        }

        [TestCase("", "", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", "", ExpectedResult = "")]
        [TestCase("", null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\file.txt", ExpectedResult = "file.txt")] // Same location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir\file.txt", ExpectedResult = @"ChildDir\file.txt")] // Child location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\file.txt", ExpectedResult = @"..\..\file.txt")] // Parent location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War\file.txt", ExpectedResult = @"..\..\Soo\War\file.txt")] // Lateral location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Bar\Foo\Soo\War\Bar.txt", ExpectedResult = @"..\..\Bar\Foo\Soo\War\Bar.txt")] // Repeating directory names
        public string ConvertPathAbsoluteToRelative_Of_File(string basePath, string targetPath)
        {
            return PathLibrary.ConvertPathAbsoluteToRelative(basePath, targetPath, isTargetPathToFile: true);
        }

        //[TestCase("", "", @"RelativeChild", ExpectedResult = "")]
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", "", @"RelativeChild", ExpectedResult = @"C:\Foo\Bar\Moo\Nar")]
        //[TestCase("", null, @"RelativeChild", ExpectedResult = "")]
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", null, @"RelativeChild", ExpectedResult = @"C:\Foo\Bar\Moo\Nar")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar", @"RelativeChild", ExpectedResult = @"..")] // Same location, child offset
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", @"RelativeChild", ExpectedResult = @"..\ChildDir")] // Child location, child offset
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar", @"RelativeChild", ExpectedResult = @"..\..\..")] // Parent location, child offset
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", @"RelativeChild", ExpectedResult = @"..\..\..\Soo\War")] // Lateral location, child offset
        // Below are not yet supported.
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar", @"..", ExpectedResult = @"Nar")] // Same location, parent offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", @"..", ExpectedResult = @"Nar\ChildDir")] // Child location, parent offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar", @"..", ExpectedResult = @"..\..")] // Parent location, parent offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", @"..", ExpectedResult = @"..\..\Soo\War")] // Lateral location, parent offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar", @"..\RelativeLateral", ExpectedResult = @"..\Nar")] // Same location, lateral offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", @"..\RelativeLateral", ExpectedResult = @"..\Nar\ChildDir")] // Child location, lateral offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar", @"..\RelativeLateral", ExpectedResult = @"..\..")] // Parent location, lateral offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", @"..\RelativeLateral", ExpectedResult = @"..\..\Soo\War")] // Lateral location, lateral offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar", @"..\RelativeLateral\ChildDir", ExpectedResult = @"..\..\..")] // Parent location, lateral offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", @"..\RelativeLateral\ChildDir", ExpectedResult = @"..\..\..\Soo\War")] // Lateral location, lateral offset
        public string ConvertPathAbsoluteToRelative_Of_Relative_Base(string basePath, string targetPath, string relativePathFromBase)
        {
            return PathLibrary.ConvertPathAbsoluteToRelative(basePath, targetPath, relativePathFromBase: relativePathFromBase);
        }

        [TestCase(@"\Foo\Bar", "")]
        [TestCase(@"C:\Foo\Bar", @"\Foo\Bar")]
        public void ConvertPathAbsoluteToRelative_3_Messenger(string basePath, string targetPath)
        {
            var wasCalled = false;
            PathLibrary.Messenger += (e) => wasCalled = true;

            PathLibrary.ConvertPathAbsoluteToRelative(basePath, targetPath);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ConvertPathAbsoluteToRelative_With_NonRelative_RelativePath_Argument_Events_Messenger()
        {
            var wasCalled = false;
            PathLibrary.Messenger += (e) => wasCalled = true;

            string basePath = @"C:\Foo\Bar\Moo\Nar";
            string targetPath = @"C:\Foo\Bar";
            string relativePathFromBase = @"C:\Foo\Bar\Moo\Nar\Sar";

            PathLibrary.ConvertPathAbsoluteToRelative(basePath, targetPath, relativePathFromBase: relativePathFromBase);

            Assert.IsTrue(wasCalled);
        }

        // Note: basePath is never tested with a path to a file. 
        // This is because paths generated need not currently exist, which means it is impossible to tell if a name is for a file or directory.
        [TestCase("", "", ExpectedResult = "")]
        [TestCase("", null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", "", ExpectedResult = @"C:\Foo\Bar\Moo\Nar")] // Same location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", " ", ExpectedResult = @"C:\Foo\Bar\Moo\Nar")] // Same location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", null, ExpectedResult = @"C:\Foo\Bar\Moo\Nar")] // Same location
        [TestCase("", "ChildDir", ExpectedResult = "")]
        [TestCase(" ", "ChildDir", ExpectedResult = "")]
        [TestCase(null, "ChildDir", ExpectedResult = "")]
        [TestCase(@"\Foo\Bar\Moo\Nar", null, ExpectedResult = "")]
        [TestCase(@"\Foo\Bar\Moo\Nar", "ChildDir", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", "ChildDir", ExpectedResult = @"C:\Foo\Bar\Moo\Nar\ChildDir")] // Child location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..", ExpectedResult = @"C:\Foo\Bar")] // Parent location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\Soo\War", ExpectedResult = @"C:\Foo\Bar\Soo\War")] // Lateral location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\Bar\Foo\Soo\War", ExpectedResult = @"C:\Foo\Bar\Bar\Foo\Soo\War")] // Repeating directory names
        [TestCase(@"C:\Foo Bar\Moo\Nar", @"..\..\Bar\Foo\Soo War", ExpectedResult = @"C:\Foo Bar\Bar\Foo\Soo War")] // Directory names with spaces
        [TestCase(@"   C:\Foo\Bar\Moo\\Nar\ ", @"     ..\..\Soo\War ", ExpectedResult = @"C:\Foo\Bar\Soo\War")] // Dirty path
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\..\..\Soo\War", ExpectedResult = @"C:\Soo\War")] // Lateral location from root
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\..\..\..\Soo\War", ExpectedResult = @"C:\Soo\War")] // Lateral location from above root
        public string ConvertPathRelativeToAbsolute_Of_Directory(string basePath, string targetPath)
        {
            return PathLibrary.ConvertPathRelativeToAbsolute(basePath, targetPath);
        }

        [TestCase("", "", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", "", ExpectedResult = @"C:\Foo\Bar\Moo\Nar")]
        [TestCase("", null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", null, ExpectedResult = @"C:\Foo\Bar\Moo\Nar")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", "file.txt", ExpectedResult = @"C:\Foo\Bar\Moo\Nar\file.txt")] // Same location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"ChildDir\file.txt", ExpectedResult = @"C:\Foo\Bar\Moo\Nar\ChildDir\file.txt")] // Child location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\file.txt", ExpectedResult = @"C:\Foo\Bar\file.txt")] // Parent location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\Soo\War\file.txt", ExpectedResult = @"C:\Foo\Bar\Soo\War\file.txt")] // Lateral location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\Bar\Foo\Soo\War\Bar.txt", ExpectedResult = @"C:\Foo\Bar\Bar\Foo\Soo\War\Bar.txt")] // Repeating directory names
        public string ConvertPathRelativeToAbsolute_Of_File(string basePath, string targetPath)
        {
            return PathLibrary.ConvertPathRelativeToAbsolute(basePath, targetPath);
        }

        //[TestCase("", "", @"RelativeChild", ExpectedResult = "")]
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", "", @"RelativeChild", ExpectedResult = @"C:\Foo\Bar\Moo\Nar")]
        //[TestCase("", null, @"RelativeChild", ExpectedResult = "")]
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", null, @"RelativeChild", ExpectedResult = @"C:\Foo\Bar\Moo\Nar")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..", @"RelativeChild", ExpectedResult = @"C:\Foo\Bar\Moo\Nar")] // Same location, child offset
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\ChildDir", @"RelativeChild", ExpectedResult = @"C:\Foo\Bar\Moo\Nar\ChildDir")] // Child location, child offset
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\..", @"RelativeChild", ExpectedResult = @"C:\Foo\Bar")] // Parent location, child offset
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\..\Soo\War", @"RelativeChild", ExpectedResult = @"C:\Foo\Bar\Soo\War")] // Lateral location, child offset
        // Below are not yet supported.
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar", @"..", ExpectedResult = @"Nar")] // Same location, parent offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", @"..", ExpectedResult = @"Nar\ChildDir")] // Child location, parent offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar", @"..", ExpectedResult = @"..\..")] // Parent location, parent offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", @"..", ExpectedResult = @"..\..\Soo\War")] // Lateral location, parent offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar", @"..\RelativeLateral", ExpectedResult = @"..\Nar")] // Same location, lateral offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", @"..\RelativeLateral", ExpectedResult = @"..\Nar\ChildDir")] // Child location, lateral offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar", @"..\RelativeLateral", ExpectedResult = @"..\..")] // Parent location, lateral offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", @"..\RelativeLateral", ExpectedResult = @"..\..\Soo\War")] // Lateral location, lateral offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar", @"..\RelativeLateral\ChildDir", ExpectedResult = @"..\..\..")] // Parent location, lateral offset
        //[TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", @"..\RelativeLateral\ChildDir", ExpectedResult = @"..\..\..\Soo\War")] // Lateral location, lateral offset
        public string ConvertPathRelativeToAbsolute_Of_Relative_Base(string basePath, string targetPath, string relativePathFromBase)
        {
            return PathLibrary.ConvertPathRelativeToAbsolute(basePath, targetPath, relativePathFromBase: relativePathFromBase);
        }

        [TestCase(@"\Foo\Bar", "")]
        [TestCase(@"C:\Foo\Bar", @"C:\Foo\Bar")]
        public void ConvertPathRelativeToAbsolute_Events_Messenger(string basePath, string targetPath)
        {
            var wasCalled = false;
            PathLibrary.Messenger += (e) => wasCalled = true;

            PathLibrary.ConvertPathRelativeToAbsolute(basePath, targetPath);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ConvertPathRelativeToAbsolute_With_NonRelative_RelativePath_Argument_Events_Messenger()
        {
            var wasCalled = false;
            PathLibrary.Messenger += (e) => wasCalled = true;

            string basePath = @"C:\Foo\Bar\Moo\Nar";
            string targetPath = @"C:\Foo\Bar";
            string relativePathFromBase = @"C:\Foo\Bar\Moo\Nar\Sar";

            PathLibrary.ConvertPathRelativeToAbsolute(basePath, targetPath, relativePathFromBase: relativePathFromBase);

            Assert.IsTrue(wasCalled);
        }

        // Note: basePath is never tested with a path to a file. 
        // This is because paths generated need not currently exist, which means it is impossible to tell if a name is for a file or directory.
        [TestCase(@"C:\Foo\Bar", @"C:\Foo\Bar", ExpectedResult = true)]
        [TestCase(@"C:\Foo\Bar", "", ExpectedResult = true)]
        [TestCase(@"C:\Foo\Bar", null, ExpectedResult = true)]
        [TestCase(@"C:\Foo\Bar", " ", ExpectedResult = true)]
        [TestCase(@"", @"C:\Foo\Bar", ExpectedResult = true)]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", "ChildDir", ExpectedResult = true)] // Child location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..", ExpectedResult = true)] // Parent location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\Soo\War", ExpectedResult = true)] // Lateral location
        [TestCase(@"Foo\Bar", "", ExpectedResult = false)] // False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = false)] // Child location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar", ExpectedResult = false)] // Parent location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", ExpectedResult = false)] // Lateral location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = false)] // Child location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar", ExpectedResult = false)] // Parent location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar\Soo\War", ExpectedResult = false)] // Lateral location: False result expected
        public bool AbsolutePath_Returns_Success(string basePath, string targetPath)
        {
            return PathLibrary.AbsolutePath(ref basePath, basePath: basePath);
        }

        [TestCase(@"C:\Foo\Bar", @"C:\Foo\Bar", ExpectedResult = @"C:\Foo\Bar")]
        [TestCase(@"C:\Foo\Bar", "", ExpectedResult = @"C:\Foo\Bar")]
        [TestCase(@"C:\Foo\Bar", null, ExpectedResult = @"C:\Foo\Bar")]
        [TestCase(@"C:\Foo\Bar", " ", ExpectedResult = @"C:\Foo\Bar")]
        [TestCase(@"", @"C:\Foo\Bar", ExpectedResult = @"C:\Foo\Bar")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", "ChildDir", ExpectedResult = @"C:\Foo\Bar\Moo\Nar\ChildDir")] // Child location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..", ExpectedResult = @"C:\Foo\Bar")] // Parent location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..\..\Soo\War", ExpectedResult = @"C:\Foo\Bar\Soo\War")] // Lateral location
        [TestCase(@"Foo\Bar", "", ExpectedResult = "")] // False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = @"C:\Foo\Bar\Moo\Nar\ChildDir")] // Child location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar", ExpectedResult = @"C:\Foo\Bar")] // Parent location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", ExpectedResult = @"C:\Foo\Bar\Soo\War")] // Lateral location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = @"Foo\Bar\Moo\Nar\ChildDir")] // Child location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar", ExpectedResult = @"Foo\Bar")] // Parent location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar\Soo\War", ExpectedResult = @"Foo\Bar\Soo\War")] // Lateral location: False result expected
        public string AbsolutePath_Modifies_Path(string basePath, string targetPath)
        {
            string returnedTargetPath = targetPath;
            PathLibrary.AbsolutePath(ref returnedTargetPath, basePath: basePath);
            return returnedTargetPath;
        }

        [TestCase(@"", @"Soo\War")] // Testing default base path applied
        [TestCase(@" ", @"Soo\War")] // Testing default base path applied
        [TestCase(null, @"Soo\War")] // Testing default base path applied
        public void AbsolutePath_Using_Default_Base_Modifies_Path(string basePath, string targetPath)
        {
            string returnedTargetPath = targetPath;
            string expectedPath = Path.Combine(Application.StartupPath, @"Soo\War");
            PathLibrary.AbsolutePath(ref returnedTargetPath, basePath: basePath);

            Assert.AreEqual(expectedPath, returnedTargetPath);
        }

        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"..", @"RelativeChild", ExpectedResult = @"C:\Foo\Bar\Moo\Nar")] // Same location, child offset
        public string AbsolutePath_Of_Relative_Base_Modifies_Path_Based_On_Success(string basePath, string targetPath, string relativePathFromBase)
        {
            string returnedTargetPath = targetPath;
            PathLibrary.AbsolutePath(ref returnedTargetPath, relativePathFromBase, basePath);
            return returnedTargetPath;
        }

        // Note: basePath is never tested with a path to a file. 
        // This is because paths generated need not currently exist, which means it is impossible to tell if a name is for a file or directory.
        [TestCase(@"C:\Foo\Bar", @"C:\Foo\Bar", ExpectedResult = true)]
        [TestCase(@"C:\Foo\Bar", "", ExpectedResult = true)]
        [TestCase(@"C:\Foo\Bar", null, ExpectedResult = true)]
        [TestCase(@"C:\Foo\Bar", " ", ExpectedResult = true)]
        [TestCase(@"", @"C:\Foo\Bar", ExpectedResult = true)] // Testing default base path applied
        [TestCase(@"", @"Soo\War", ExpectedResult = true)] // Testing default base path applied
        [TestCase(@" ", @"Soo\War", ExpectedResult = true)] // Testing default base path applied
        [TestCase(null, @"Soo\War", ExpectedResult = true)] // Testing default base path applied
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = true)] // Child location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar", ExpectedResult = true)] // Parent location
        [TestCase(@"C:\Foo\Bar\Moo\Nar",  @"C:\Foo\Bar\Soo\War", ExpectedResult = true)] // Lateral location
        [TestCase(@"Foo\Bar", "", ExpectedResult = false)]
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = false)] // Child location
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar", ExpectedResult = false)] // Parent location
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", ExpectedResult = false)] // Lateral location
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = false)] // Child location
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar", ExpectedResult = false)] // Parent location
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar\Soo\War", ExpectedResult = false)] // Lateral location
        public bool RelativePath_Returns_Success(string basePath, string targetPath)
        {
            return PathLibrary.RelativePath(ref targetPath, basePath: basePath);
        }


        [TestCase(@"C:\Foo\Bar", @"C:\Foo\Bar", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar", "", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar", null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar", " ", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = "ChildDir")] // Child location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar", ExpectedResult = @"..\..")] // Parent location
        [TestCase(@"C:\Foo\Bar\Moo\Nar",  @"C:\Foo\Bar\Soo\War", ExpectedResult = @"..\..\Soo\War")] // Lateral location
        [TestCase(@"Foo\Bar", "", ExpectedResult = "")] // False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = @"C:\Foo\Bar\Moo\Nar\ChildDir")] // Child location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar", ExpectedResult = @"C:\Foo\Bar")] // Parent location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War", ExpectedResult = @"C:\Foo\Bar\Soo\War")] // Lateral location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar\Moo\Nar\ChildDir", ExpectedResult = @"Foo\Bar\Moo\Nar\ChildDir")] // Child location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar", ExpectedResult = @"Foo\Bar")] // Parent location: False result expected
        [TestCase(@"Foo\Bar\Moo\Nar", @"Foo\Bar\Soo\War", ExpectedResult = @"Foo\Bar\Soo\War")] // Lateral location: False result expected
        public string RelativePath_Modifies_Path(string basePath, string targetPath)
        {
            string returnedTargetPath = targetPath;
            PathLibrary.RelativePath(ref returnedTargetPath, basePath: basePath);
            return returnedTargetPath;
        }


        // Note: basePath is never tested with a path to a file. 
        // This is because paths generated need not currently exist, which means it is impossible to tell if a name is for a file or directory.
        [TestCase(@"C:\Foo\Bar", @"C:\Foo\Bar\file.txt", ExpectedResult = "file.txt")]
        [TestCase(@"C:\Foo\Bar", "", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar", null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar", " ", ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar\ChildDir\file.txt", ExpectedResult = @"ChildDir\file.txt")] // Child location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\file.txt", ExpectedResult = @"..\..\file.txt")] // Parent location
        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Soo\War\file.txt", ExpectedResult = @"..\..\Soo\War\file.txt")] // Lateral location
        public string RelativePath_With_File_Modifies_Path(string basePath, string targetPath)
        {
            string returnedTargetPath = targetPath;
            PathLibrary.RelativePath(ref returnedTargetPath, isFile: true, basePath: basePath);
            return returnedTargetPath;
        }

        [TestCase(@"", @"C:\Foo\Bar")] // Testing default base path applied
        [TestCase(@" ", @"C:\Foo\Bar")] // Testing default base path applied
        [TestCase(null, @"C:\Foo\Bar")] // Testing default base path applied
        public void RelativePath_Using_Default_Base_With_Absolute_Target_Modifies_Path(string basePath, string targetPath)
        {
            // Relying on default assigned to base in method
            string returnedTargetPathWithDefaultBase = targetPath;
            PathLibrary.RelativePath(ref returnedTargetPathWithDefaultBase, basePath: basePath);

            // Using expected default as base
            string defaultBasePath = Application.StartupPath;
            string returnedTargetPath = targetPath;
            PathLibrary.RelativePath(ref returnedTargetPath, basePath: defaultBasePath);
            
            Assert.AreEqual(returnedTargetPathWithDefaultBase, returnedTargetPath);
        }

        [TestCase(@"", @"Soo\War")] // Testing default base path applied
        [TestCase(@" ", @"Soo\War")] // Testing default base path applied
        [TestCase(null, @"Soo\War")] // Testing default base path applied
        public void RelativePath_Using_Default_Base_With_Relative_Target_Does_Not_Modify_Path(string basePath, string targetPath)
        {
            string returnedTargetPath = targetPath;
            PathLibrary.RelativePath(ref returnedTargetPath, basePath: basePath);
            
            Assert.AreEqual(targetPath, returnedTargetPath);
        }

        [TestCase(@"C:\Foo\Bar\Moo\Nar", @"C:\Foo\Bar\Moo\Nar", @"RelativeChild", ExpectedResult = @"..")] // Same location, child offset
        public string RelativePath_Of_Relative_Base_Modifies_Path_Based_On_Success(string basePath, string targetPath, string relativePathFromBase)
        {
            string returnedTargetPath = targetPath;
            PathLibrary.RelativePath(ref returnedTargetPath, relativePathFromBase: relativePathFromBase, basePath: basePath);
            return returnedTargetPath;
        }
    }

    [TestFixture]
    public class PathLibraryTests_Browsing_Related
    {
        [Test]
        public void PathStartupApp()
        {
            // Currently is that of Visual Studio as running.
            string expectedPath = Application.StartupPath; 
            Assert.AreEqual(expectedPath, PathLibrary.PathStartupApp());
        }

        [Test]
        public void PathStartupDll()
        {
            // Currently is that of NCrunch copied assembly (
            // These are always slightly different for expected vs. the DLL assembly
            // Therefore, they need to be truncated and modified for some predicatble differences.
            string expectedPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            expectedPath = expectedPath?.Replace(".UnitTests", "");
            expectedPath = TruncatePath(expectedPath, "NCrunch", "MPT.FileSystem");

            string actualPath = PathLibrary.PathStartupDll();
            actualPath = TruncatePath(actualPath, "NCrunch", "MPT.FileSystem");

            Assert.AreEqual(expectedPath, actualPath);
        }

        [Test]
        public void DefaultPathApp()
        {
            string folderName = "FooBar";
            // Currently is that of Visual Studio as running.
            string expectedPath = Path.Combine(Application.StartupPath, folderName); 

            Assert.AreEqual(expectedPath, PathLibrary.DefaultPathApp(folderName));
        }

        [Test]
        public void DefaultPathDll()
        {
            string folderName = "FooBar";
            string expectedPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            expectedPath = Path.Combine(expectedPath, folderName);
            expectedPath = expectedPath.Replace(".UnitTests", "");
            expectedPath = TruncatePath(expectedPath, "NCrunch", "MPT.FileSystem");

            string actualPath = PathLibrary.DefaultPathDll(folderName);
            actualPath = TruncatePath(actualPath, "NCrunch", "MPT.FileSystem");

            Assert.AreEqual(expectedPath, actualPath);
        }

        [Test]
        public void CurrentDirectoryPath_Of_Existing_Absolute_Directory_Path_Returns_Unmodified_Path()
        {
            // Get path two levels up from running application
            string existingPathDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));

            Assert.AreEqual(existingPathDirectory, PathLibrary.CurrentDirectoryPath(existingPathDirectory));
        }

        [Test]
        public void CurrentDirectoryPath_Of_Existing_Absolute_File_Path_Returns_Unmodified_Path()
        {
            // Get running application path
            string existingPathFile = Process.GetCurrentProcess().MainModule.FileName;
            string expectedPath = Application.StartupPath;

            Assert.AreEqual(expectedPath, PathLibrary.CurrentDirectoryPath(existingPathFile));
        }

        [Test]
        public void CurrentDirectoryPath_Of_Existing_Relative_Directory_Path_Returns_Unmodified_Path()
        {
            string existingRelativePath = @"..\..\";

            // Get path two levels up from running application
            string existingPathDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, existingRelativePath));
            existingPathDirectory = existingPathDirectory.Remove(existingPathDirectory.Length-1); // This removes the trailing backslash

            Assert.AreEqual(existingPathDirectory, PathLibrary.CurrentDirectoryPath(existingRelativePath));
        }

        [Test]
        public void CurrentDirectoryPath_Of_Existing_Relative_File_Path_Returns_Unmodified_Path()
        {
            string existingRelativePath = @"..\NCrunch for Visual Studio 2015\nCrunch.TestHost452.x86.exe";

            // Get running application path
            string existingPathFile = Process.GetCurrentProcess().MainModule.FileName;
            string expectedPath = Application.StartupPath;
            

            Assert.AreEqual(expectedPath, PathLibrary.CurrentDirectoryPath(existingRelativePath));
        }

        [Test]
        public void CurrentDirectoryPath_Of_Existing_Path_With_Default_Relative_Path_Returns_Unmodified_Path()
        {
            // Get path two levels up from running application
            string existingPathDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
            string nonExistingSubFolder = @"Foo\Bar";

            Assert.AreEqual(existingPathDirectory, PathLibrary.CurrentDirectoryPath(existingPathDirectory, nonExistingSubFolder));
        }

        [TestCase("")] // Empty path
        [TestCase(@"C:\Foo\Bar")] // Path to nonexisting directory
        [TestCase(@"C:\Foo\Bar.txt")] // Path to nonexisting file
        public void CurrentDirectoryPath_Of_NonExisting_Path_Returns_Running_Application_Directory_Path(string path)
        {
            string expectedPath = Application.StartupPath;
  
            Assert.AreEqual(expectedPath, PathLibrary.CurrentDirectoryPath(path));
        }


        [TestCase("")] // Empty path
        [TestCase(@"C:\Foo\Bar")] // Path to nonexisting directory
        [TestCase(@"C:\Foo\Bar.txt")] // Path to nonexisting file
        public void CurrentDirectoryPath_Of_NonExisting_Path_With_Valid_Default_Relative_Path_Returns_Combined_Default_Path(string path)
        {
            // Get path one level up from running application
            string existingPathDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\"));

            // Get path at level of running application
            string subFolder = "NCrunch for Visual Studio 2015";

            string expectedPath = Path.Combine(existingPathDirectory, subFolder);

            Assert.AreEqual(expectedPath, PathLibrary.CurrentDirectoryPath(path, subFolder));
        }

        [TestCase("")] // Empty path
        [TestCase(@"C:\Foo\Bar")] // Path to nonexisting directory
        [TestCase(@"C:\Foo\Bar.txt")] // Path to nonexisting file
        public void CurrentDirectoryPath_Of_NonExisting_Path_With_Invalid_Default_Relative_Path_Returns_Running_Application_Directory_Path(string path)
        {
            string expectedPath = Application.StartupPath;

            Assert.AreEqual(expectedPath, PathLibrary.CurrentDirectoryPath(path, @"Foo\Bar"));
        }




        private string TruncatePath(string path,
            string startingSkippedgDirectory,
            string endingSkippedDirectory)
        {
            string[] paths = path?.Split(Convert.ToChar(@"\"));
            string truncatedPath = "";
            bool skipDirectories = false;
            for (int i = 0; i < paths.Length; i++)
            {
                if (paths[i].CompareTo(endingSkippedDirectory) == 0)
                {
                    skipDirectories = false;
                }

                if (!skipDirectories)
                {
                    truncatedPath = Path.Combine(truncatedPath, paths[i]);
                }

                if (paths[i].CompareTo(startingSkippedgDirectory) == 0)
                {
                    skipDirectories = true;
                }
            }
            return truncatedPath;
        }
    }

    [TestFixture]
    public class PathLibraryTests_Check_Path_Properties
    {

        [TestCase("", ExpectedResult = true)]
        [TestCase(" ", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        [TestCase("Foo", ExpectedResult = true)]
        [TestCase(@"Foo\Bar", ExpectedResult = true)]
        [TestCase(@"..\Foo\Bar", ExpectedResult = true)]
        [TestCase(@"..\C:\Foo\Bar", ExpectedResult = true)]
        [TestCase(@"C:\Foo\Bar", ExpectedResult = false)]
        public bool IsRelativePath(string path)
        {
            return PathLibrary.IsRelativePath(path);
        }

        [TestCase(@"", ExpectedResult = false)]
        [TestCase(@" ", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        [TestCase(@"a", ExpectedResult = false)]
        [TestCase(@"C:\", ExpectedResult = true)]
        [TestCase(@"A:\", ExpectedResult = true)]
        [TestCase(@"C:\\", ExpectedResult = true)]
        [TestCase(@"C:\SampleDirectory", ExpectedResult = true)]
        [TestCase(@"C:/SampleDirectory", ExpectedResult = false)]
        [TestCase(@"..\SampleDirectory", ExpectedResult = false)]
        [TestCase(@"1:\SampleDirectory", ExpectedResult = false)]
        public bool IsValidDirectory(string path)
        {
            return PathLibrary.IsValidDirectory(path);
        }

        [TestCase(@"", ExpectedResult = false)]
        [TestCase(@" ", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        [TestCase(@"C:\", ExpectedResult = false)]
        [TestCase(@"Foo?Bar", ExpectedResult = false)]
        [TestCase(@"Foo:Bar", ExpectedResult = false)]
        [TestCase(@"Foo*Bar", ExpectedResult = false)]
        [TestCase(@"Foo|Bar", ExpectedResult = false)]
        [TestCase(@"Foo<Bar", ExpectedResult = false)]
        [TestCase(@"Foo>Bar", ExpectedResult = false)]
        [TestCase(@"Foo/Bar", ExpectedResult = false)]
        [TestCase(@"Foo/\Bar", ExpectedResult = false)]
        [TestCase(@"Foo""Bar", ExpectedResult = false)]
        [TestCase(@"C:/Valid/File/Name", ExpectedResult = false)]
        [TestCase(@"Valid File Name", ExpectedResult = true)]
        [TestCase(@"Valid File Name.txt", ExpectedResult = true)]
        [TestCase(@"Valid_File_Name", ExpectedResult = true)]
        [TestCase(@"Valid-File-Name", ExpectedResult = true)]
        [TestCase(@"Valid!File!Name", ExpectedResult = true)]
        [TestCase(@"ValidFileName2345", ExpectedResult = true)]
        [TestCase(@"2345ValidFileName", ExpectedResult = true)]
        [TestCase(@"C:\\Valid\\File\\Name", ExpectedResult = true)]
        public bool IsValidName(string name)
        {
            return PathLibrary.IsValidName(name);
        }

        [TestCase(@"", ExpectedResult = false)]
        [TestCase(@" ", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        [TestCase(@"InvalidPath", ExpectedResult = false)]
        [TestCase(@"C:\", ExpectedResult = false)]
        [TestCase(@"C:\ValidPath\ValidSubDirectory\invalid?FileName.txt", ExpectedResult = false)]
        [TestCase(@"C:\ValidPath\ValidSubDirectory\invalid*FileName.txt", ExpectedResult = false)]
        [TestCase(@"C:\ValidPath\ValidSubDirectory\invalid|FileName.txt", ExpectedResult = false)]
        [TestCase(@"C:\ValidPath\ValidSubDirectory\invalid<FileName.txt", ExpectedResult = false)]
        [TestCase(@"C:\ValidPath\ValidSubDirectory\invalid>FileName.txt", ExpectedResult = false)]
        [TestCase(@"C:\ValidPath\ValidSubDirectory\invalid:FileName.txt", ExpectedResult = false)]
        [TestCase(@"C:\ValidPath\ValidSubDirectory\invalid""FileName.txt", ExpectedResult = false)]
        [TestCase(@"C:\ValidPath\ValidSubDirectory\invalid/FileName.txt", ExpectedResult = false)]
        [TestCase(@"1:\ValidPath\ValidSubDirectory\validFileName.txt", ExpectedResult = false)]
        [TestCase(@"C:\ValidPath", ExpectedResult = true)]
        [TestCase(@"C:\ValidPath\ValidSubDirectory", ExpectedResult = true)]
        [TestCase(@"C:\ValidPath\ValidSubDirectory\validFileName.txt", ExpectedResult = true)]
        public bool IsValidPath(string path)
        {
            return PathLibrary.IsValidPath(path);
        }

        [TestCase(@"", ExpectedResult = false)]
        [TestCase(@" ", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        [TestCase(@"NoExtension", ExpectedResult = false)]
        [TestCase(@"NoExtension.FooBar", ExpectedResult = true)]
        [TestCase(@"Extension.sdb", ExpectedResult = true)]
        [TestCase(@"Extension.123", ExpectedResult = true)]
        public bool FileNameHasExtension_Auto_Length_Extension(string name)
        {
            return PathLibrary.FileNameHasExtension(name);
        }

        [TestCase(@"", 3, ExpectedResult = false)]
        [TestCase(@" ", 3, ExpectedResult = false)]
        [TestCase(null, 3, ExpectedResult = false)]
        [TestCase(@"NoExtension", 5, ExpectedResult = false)]
        [TestCase(@"NoExtension.FooBar", 5, ExpectedResult = false)]
        [TestCase(@"Extension.", 0, ExpectedResult = false)]
        [TestCase(@"Extension.", -1, ExpectedResult = false)]
        [TestCase(@"Extension.sdbad", 5, ExpectedResult = true)]
        [TestCase(@"Extension.12345", 5, ExpectedResult = true)]
        public bool FileNameHasExtension_Custom_Length_Extension(string name, int extensionLength)
        {
            return PathLibrary.FileNameHasExtension(name, extensionLength);
        }

        [TestCase(@"C:\Foo\Bar\FooBar.txt", ExpectedResult = false)]
        [TestCase(@"C:\Foo\Bar\FooBar", ExpectedResult = true)]
        [TestCase(@"..\Foo\Bar\FooBar.txt", ExpectedResult = false)]
        [TestCase(@"..\Foo\Bar\FooBar", ExpectedResult = true)]
        [TestCase(@"C:\Foo.Bar\FooBar.txt", ExpectedResult = false)]
        [TestCase(@"C:\Foo.Bar\FooBar", ExpectedResult = true)]
        public bool FileNameHasNoExtension(string path)
        {
            return PathLibrary.FileNameHasNoExtension(path);
        }

        [TestCase(@"", @"", ExpectedResult = false)]
        [TestCase(@" ", @"", ExpectedResult = false)]
        [TestCase(@" ", @" ", ExpectedResult = false)]
        [TestCase(null, @" ", ExpectedResult = false)]
        [TestCase(null, null, ExpectedResult = false)]
        [TestCase(@".k??", @".q~1", ExpectedResult = false)]
        [TestCase(@"k??", @"k~1", ExpectedResult = true)]
        [TestCase(@".k??", @"k~1", ExpectedResult = true)]
        [TestCase(@"k??", @".k~1", ExpectedResult = true)]
        [TestCase(@".k??", @".k~1", ExpectedResult = true)]
        [TestCase(@".k~?", @".k~1", ExpectedResult = true)]
        [TestCase(@".k~1", @".k~1", ExpectedResult = false)]
        [TestCase(@".k???", @".k~1", ExpectedResult = false)]
        [TestCase(@".k???", @".k~1a", ExpectedResult = true)]
        public bool GenericExtensionMatch(string fileExtensionGeneric, string fileExtensionSourceActual)
        {
            return PathLibrary.GenericExtensionsMatch(fileExtensionGeneric, fileExtensionSourceActual);
        }

        [TestCase("", ExpectedResult = false)]
        [TestCase("Foo.Bar", ExpectedResult = true)]
        [TestCase(@"C:\Moo\Far\Foo.Bar", ExpectedResult = true)]
        public bool FileNameExtensionMatch_With_No_Comparison_Criteria(string fileNameSource)
        {
            return PathLibrary.FileNameExtensionsMatch(fileNameSource);
        }

        [TestCase("Foo.Bar", "Foo", ExpectedResult = true)]
        [TestCase("Foo Boo.Bar", "Foo Boo", ExpectedResult = true)]
        [TestCase("Foo.Bar", "foo", ExpectedResult = true)]
        [TestCase(@"C:\Moo\Far\Foo.Bar", "Foo", ExpectedResult = true)]
        [TestCase("Foo.Bar", "Noo", ExpectedResult = false)]
        [TestCase("Foo.Bar", "noo", ExpectedResult = false)]
        [TestCase(@"C:\Moo\Far\Foo.Bar", "Noo", ExpectedResult = false)]
        public bool FileNameExtensionMatch_By_FileNames(string fileNameSource, string fileNameCompare)
        {
            return PathLibrary.FileNameExtensionsMatch(fileNameSource, fileNameCompare);
        }

        [TestCase("FooNoo.Bar", "Foo", ExpectedResult = true)]
        [TestCase("FooNoo.Bar", "foo", ExpectedResult = true)]
        [TestCase("FooMoo NooMoo.Bar", "Foo", ExpectedResult = true)]
        [TestCase("NooFooNoo.Bar", "Foo", ExpectedResult = true)]
        [TestCase(@"C:\Moo\Far\FooNoo.Bar", "Foo", ExpectedResult = true)]
        [TestCase("Foo.Bar", "Noo", ExpectedResult = false)]
        [TestCase("Foo.Bar", "noo", ExpectedResult = false)]
        [TestCase(@"C:\Moo\Far\Foo.Bar", "Noo", ExpectedResult = false)]
        public bool FileNameExtensionMatch_By_Partial_FileName_Matches(string fileNameSource, string fileNameCompare)
        {
            return PathLibrary.FileNameExtensionsMatch(fileNameSource, fileNameCompare, partialNameMatch: true);
        }

        [TestCase("Foo.Bar", ".Bar", ExpectedResult = true)]
        [TestCase("Foo.Bar", ".bar", ExpectedResult = true)]
        [TestCase("Foo.Bar", "Bar", ExpectedResult = true)]
        [TestCase("Foo.Bar", "*.Bar", ExpectedResult = true)]
        [TestCase(@"C:\Moo\Far\Foo.Bar", ".Bar", ExpectedResult = true)]
        [TestCase("Foo.Bar", ".Noo", ExpectedResult = false)]
        [TestCase("Foo.Bar", ".noo", ExpectedResult = false)]
        [TestCase(@"C:\Moo\Far\Foo.Bar", ".Noo", ExpectedResult = false)]
        public bool FileNameExtensionMatch_By_FileExtensions(string fileNameSource, string fileExtensionCompare)
        {
            return PathLibrary.FileNameExtensionsMatch(fileNameSource, fileExtensionCompare: fileExtensionCompare);
        }

        [TestCase("Foo.Bar", ".B?", ExpectedResult = false)]
        [TestCase("Foo.Bar", ".B??", ExpectedResult = true)]
        [TestCase("Foo.Bar", ".B???", ExpectedResult = false)]
        [TestCase("Foo.Bar", ".b??", ExpectedResult = true)]
        [TestCase("Foo.Bar", "B??", ExpectedResult = true)]
        [TestCase("Foo.Bar", "*.B??", ExpectedResult = true)]
        [TestCase(@"C:\Moo\Far\Foo.Bar", ".B??", ExpectedResult = true)]
        public bool FileNameExtensionMatch_By_Generic_FileExtensions(string fileNameSource, string fileExtensionCompare)
        {
            return PathLibrary.FileNameExtensionsMatch(fileNameSource, fileExtensionCompare: fileExtensionCompare);
        }

        [TestCase("Foo.Bar", "Foo", ".Bar", ExpectedResult = true)]
        [TestCase("Foo Noo.Bar", "Foo Noo", ".Bar", ExpectedResult = true)]
        [TestCase("Foo.Bar", "foo", ".bar", ExpectedResult = true)]
        [TestCase("Foo.Bar", "Moo", ".Nar", ExpectedResult = false)]
        [TestCase("Foo.Bar", "Moo", ".Bar", ExpectedResult = false)]
        [TestCase("Foo.Bar", "Foo", "Bar", ExpectedResult = true)]
        [TestCase("Foo.Bar", "Foo", "*.Bar", ExpectedResult = true)]
        [TestCase(@"C:\Moo\Far\Foo.Bar", "Foo", ".Bar", ExpectedResult = true)]
        public bool FileNameExtensionMatch_By_FileNames_And_Extensions(string fileNameSource, string fileNameCompare, string fileExtensionCompare)
        {
            return PathLibrary.FileNameExtensionsMatch(fileNameSource, fileNameCompare, fileExtensionCompare);
        }
    }

    [TestFixture]
    public class PathLibraryTests_Gets_Portion_Of_Path
    {
        [Test]
        public void FileName_To_Existing_Directory_Returns_Nothing()
        {
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Assert.AreEqual("",  PathLibrary.FileName(basePath));
        }

        [Test]
        public void FileName_To_Existing_Relative_Directory_Returns_Nothing()
        {
            string relativePath = @"..\Debug";
            Assert.AreEqual("", PathLibrary.FileName(relativePath));
        }

        [TestCase(@"", ExpectedResult = "")]
        [TestCase(@" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\FooBar.txt", ExpectedResult = "FooBar.txt")]
        [TestCase(@"..\Foo\Bar\FooBar.txt", ExpectedResult = "FooBar.txt")]
        [TestCase(@"FooBar.txt", ExpectedResult = "FooBar.txt")]
        public string FileName_With_Extension(string path)
        {
            return PathLibrary.FileName(path);
        }

        [TestCase(@"", ExpectedResult = "")]
        [TestCase(@" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\FooBar.txt", ExpectedResult = "FooBar")]
        [TestCase(@"..\Foo\Bar\FooBar.txt", ExpectedResult = "FooBar")]
        [TestCase(@"FooBar.txt", ExpectedResult = "FooBar")]
        public string FileName_Without_Extension(string path)
        {
            return PathLibrary.FileName(path, noExtension: true);
        }

        [TestCase(@"", ExpectedResult = "")]
        [TestCase(@" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\FooBar.txt", ExpectedResult = "FooBar")]
        [TestCase(@"..\Foo\Bar\FooBar.txt", ExpectedResult = "FooBar")]
        [TestCase(@"FooBar.txt", ExpectedResult = "FooBar")]
        public string FileNameWithoutExtension(string path)
        {
            return PathLibrary.FileNameWithoutExtension(path);
        }

        [TestCase(@"", ExpectedResult = "")]
        [TestCase(@" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\FooBar.txt", ExpectedResult = @"C:\Foo\Bar")]
        [TestCase(@"C:\Foo\Bar", ExpectedResult = @"C:\Foo")]
        [TestCase(@"..\Foo\Bar", ExpectedResult = @"..\Foo")]
        [TestCase(@"FooBar.txt", ExpectedResult = @"FooBar.txt")]
        public string PathDirectoryStub(string pathDirectory)
        {
            return PathLibrary.PathDirectoryStub(pathDirectory);
        }

        [TestCase(@"", 1, ExpectedResult = "")]
        [TestCase(@" ", 1, ExpectedResult = "")]
        [TestCase(null, 1, ExpectedResult = "")]
        [TestCase(@"C:\Foo\Bar\FooBar.txt", -1, ExpectedResult = @"C:\Foo\Bar\FooBar.txt")]
        [TestCase(@"C:\Foo\Bar", -1, ExpectedResult = @"C:\Foo\Bar")]
        [TestCase(@"..\Foo\Bar", -1, ExpectedResult = @"..\Foo\Bar")]
        [TestCase(@"FooBar.txt", -1, ExpectedResult = @"FooBar.txt")]
        [TestCase(@"C:\Foo\Bar\FooBar.txt", 0, ExpectedResult = @"C:\Foo\Bar\FooBar.txt")]
        [TestCase(@"C:\Foo\Bar", 0, ExpectedResult = @"C:\Foo\Bar")]
        [TestCase(@"..\Foo\Bar", 0, ExpectedResult = @"..\Foo\Bar")]
        [TestCase(@"FooBar.txt", 0, ExpectedResult = @"FooBar.txt")]
        [TestCase(@"C:\Foo\Bar\FooBar.txt", 1, ExpectedResult = @"C:\Foo\Bar")]
        [TestCase(@"C:\Foo\Bar", 1, ExpectedResult = @"C:\Foo")]
        [TestCase(@"..\Foo\Bar", 1, ExpectedResult = @"..\Foo")]
        [TestCase(@"FooBar.txt", 1, ExpectedResult = @"FooBar.txt")]
        [TestCase(@"C:\Foo\Bar\FooBar.txt", 2, ExpectedResult = @"C:\Foo")]
        [TestCase(@"C:\Foo\Bar", 2, ExpectedResult = @"C:")]
        [TestCase(@"..\Foo\Bar", 2, ExpectedResult = @"..")]
        [TestCase(@"FooBar.txt", 2, ExpectedResult = @"FooBar.txt")]
        public string PathDirectorySubStub(string pathDirectory, int numberOfDirectories)
        {
            return PathLibrary.PathDirectorySubStub(pathDirectory, numberOfDirectories);
        }

        [Test]
        public void UniquePathSegment_Given_Null_List_Returns_Empty()
        {
            string expectedPathSegment = "";

            List<string> directories = null;
            int numberDirectoriesEqual = 2;

            Assert.AreEqual(expectedPathSegment,PathLibrary.UniquePathSegment(directories, numberDirectoriesEqual));
        }

        [Test]
        public void UniquePathSegment_Given_Empty_List_Returns_Empty()
        {
            string expectedPathSegment = "";

            List<string> directories = new List<string>();
            int numberDirectoriesEqual = 2;

            Assert.AreEqual(expectedPathSegment, PathLibrary.UniquePathSegment(directories, numberDirectoriesEqual));
        }

        [Test]
        public void UniquePathSegment_Given_Valid_Arguments_Returns_Unique_Path_Segment()
        {
            string expectedPathSegment = @"Moo\Far";

            List<string> directories = new List<string>() {"Foo", "Bar", "Moo", "Far"};
            int numberDirectoriesEqual = 2;

            Assert.AreEqual(expectedPathSegment, PathLibrary.UniquePathSegment(directories, numberDirectoriesEqual));
        }

        [Test]
        public void UniquePathSegment_Given_No_Directories_Equal_Returns_All()
        {
            string expectedPathSegment = @"Foo\Bar\Moo\Far";

            List<string> directories = new List<string>() { "Foo", "Bar", "Moo", "Far" };
            int numberDirectoriesEqual = 0;

            Assert.AreEqual(expectedPathSegment, PathLibrary.UniquePathSegment(directories, numberDirectoriesEqual));
        }

        [Test]
        public void UniquePathSegment_Given_Invalid_Directories_Equal_Returns_All()
        {
            string expectedPathSegment = @"Foo\Bar\Moo\Far";

            List<string> directories = new List<string>() { "Foo", "Bar", "Moo", "Far" };
            int numberDirectoriesEqual = -2;

            Assert.AreEqual(expectedPathSegment, PathLibrary.UniquePathSegment(directories, numberDirectoriesEqual));
        }

        [Test]
        public void UniquePathSegment_Given_Valid_Arguments_And_Substitute_Directories_Returns_Unique_Path_Segment_With_Substitutes()
        {
            string expectedPathSegment = @"Noo\Noo\Noo\Noo";
            string substituteDirectory = "Noo";
            List<string> directories = new List<string>() { "Foo", "Bar", "Moo", "Far" };
            int numberDirectoriesEqual = -2;

            Assert.AreEqual(expectedPathSegment, PathLibrary.UniquePathSegment(directories, numberDirectoriesEqual, substituteDirectory));
        }

        // FolderSource
    }

    [TestFixture]
    public class PathLibraryTests_Adjusting_Cleaning_Paths
    {
        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = " ")]
        [TestCase(@"\", ExpectedResult = @"\")]
        [TestCase("a", ExpectedResult = "a")]
        [TestCase(@"\a\", ExpectedResult = @"\a\")]
        [TestCase(@"\\a\\", ExpectedResult = @"\a\")]
        [TestCase(@"C:\\Foo\\Bar\\Moo\\Far\\", ExpectedResult = @"C:\Foo\Bar\Moo\Far\")]
        [TestCase(@"C:\Foo\Bar\Moo\Far\", ExpectedResult = @"C:\Foo\Bar\Moo\Far\")]
        public string CleanAllRepeatingBackSlashes(string path)
        {
            return PathLibrary.CleanAllRepeatingBackSlashes(path);
        }

        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = " ")]
        [TestCase(@"\", ExpectedResult = @"\")]
        [TestCase("a", ExpectedResult = "a")]
        [TestCase(@"\a\", ExpectedResult = @"\a")]
        [TestCase(@"\\a\\", ExpectedResult = @"\\a")]
        [TestCase(@"C:\\Foo\\Bar\\Moo\\Far\\", ExpectedResult = @"C:\\Foo\\Bar\\Moo\\Far")]
        [TestCase(@"C:\Foo\Bar\Moo\Far\", ExpectedResult = @"C:\Foo\Bar\Moo\Far")]
        public string TrimBackSlash_Default_End(string path)
        {
            return PathLibrary.TrimBackSlash(path);
        }

        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = " ")]
        [TestCase("a", ExpectedResult = "a")]
        [TestCase(@"\a\", ExpectedResult = @"a\")]
        [TestCase(@"\\a\\", ExpectedResult = @"a\\")]
        [TestCase(@"C:\\Foo\\Bar\\Moo\\Far\\", ExpectedResult = @"C:\\Foo\\Bar\\Moo\\Far\\")]
        [TestCase(@"C:\Foo\Bar\Moo\Far\", ExpectedResult = @"C:\Foo\Bar\Moo\Far\")]
        public string TrimBackSlash_Start(string path)
        {
            return PathLibrary.TrimBackSlash(path, trimStart: true, trimEnd: false);
        }

        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = " ")]
        [TestCase("a", ExpectedResult = "a")]
        [TestCase(@"\a\", ExpectedResult = @"\a\")]
        [TestCase(@"\\a\\", ExpectedResult = @"\\a\\")]
        [TestCase(@"C:\\Foo\\Bar\\Moo\\Far\\", ExpectedResult = @"C:\\Foo\\Bar\\Moo\\Far\\")]
        [TestCase(@"C:\Foo\Bar\Moo\Far\", ExpectedResult = @"C:\Foo\Bar\Moo\Far\")]
        public string TrimBackSlash_None(string path)
        {
            return PathLibrary.TrimBackSlash(path, trimStart: false, trimEnd: false);
        }

        [TestCase("", ExpectedResult = @"")]
        [TestCase(null, ExpectedResult = @"")]
        [TestCase(" ", ExpectedResult = @"")]
        [TestCase(@"\", ExpectedResult = @"\")]
        [TestCase("a", ExpectedResult = @"a\")]
        [TestCase(@"\a\", ExpectedResult = @"\a\")]
        [TestCase(@"\\a\\", ExpectedResult = @"\\a\")]
        [TestCase(@"C:\\Foo\\Bar\\Moo\\Far\\", ExpectedResult = @"C:\\Foo\\Bar\\Moo\\Far\")]
        [TestCase(@"C:\Foo\Bar\Moo\Far", ExpectedResult = @"C:\Foo\Bar\Moo\Far\")]
        public string AddBackSlash_Default_End(string path)
        {
            return PathLibrary.AddBackSlash(path);
        }

        [TestCase("", ExpectedResult = @"")]
        [TestCase(null, ExpectedResult = @"")]
        [TestCase(" ", ExpectedResult = @"")]
        [TestCase("a", ExpectedResult = @"\a")]
        [TestCase(@"\a\", ExpectedResult = @"\a\")]
        [TestCase(@"\\a\\", ExpectedResult = @"\a\\")]
        [TestCase(@"C:\\Foo\\Bar\\Moo\\Far\\", ExpectedResult = @"\C:\\Foo\\Bar\\Moo\\Far\\")]
        [TestCase(@"C:\Foo\Bar\Moo\Far\", ExpectedResult = @"\C:\Foo\Bar\Moo\Far\")]
        public string AddBackSlash_Start(string path)
        {
            return PathLibrary.AddBackSlash(path, addStart: true, addEnd: false);
        }

        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("a", ExpectedResult = "a")]
        [TestCase(@"\a\", ExpectedResult = @"\a\")]
        [TestCase(@"\\a\\", ExpectedResult = @"\\a\\")]
        [TestCase(@"C:\\Foo\\Bar\\Moo\\Far\\", ExpectedResult = @"C:\\Foo\\Bar\\Moo\\Far\\")]
        [TestCase(@"C:\Foo\Bar\Moo\Far\", ExpectedResult = @"C:\Foo\Bar\Moo\Far\")]
        public string AddBackSlash_None(string path)
        {
            return PathLibrary.AddBackSlash(path, addStart: false, addEnd: false);
        }

        [TestCase(".foo", ExpectedResult = "foo")]
        [TestCase("foo", ExpectedResult = "foo")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string FileExtensionClean(string fileExtension)
        {
            return PathLibrary.FileExtensionClean(fileExtension);
        }

        [TestCase(".foo", ExpectedResult = ".foo")]
        [TestCase("foo", ExpectedResult = ".foo")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string FileExtensionWithPeriod(string fileExtension)
        {
            return PathLibrary.FileExtensionWithPeriod(fileExtension);
        }

        [TestCase("", ExpectedResult ="")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(@" \\\\Foo Foo\\\\Bar Bar\\\\\\   ", ExpectedResult = @"Foo Foo\Bar Bar")]
        public string CleanPath(string path)
        {
            return PathLibrary.CleanPath(path);
        }
    }

    [TestFixture]
    public class PathLibraryTests_Lists : LibraryTests_Base
    {
        [TearDown]
        public override void CleanUp()
        {
            cleanupDirectories(_pathOriginal);
        }

        [Test]
        public void ListFilePathsInDirectory_of_Valid_Directory_Returns_List_of_Matching_Extensions()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string extension = ".txt";

            string fileName1 = "FooBar" + extension;
            string filePath1 = Path.Combine(_pathOriginal, fileName1);
            var file1 = File.Create(filePath1);
            file1.Close();

            string fileName2 = "MooNar" + extension;
            string filePath2 = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", fileName2);
            var file2 = File.Create(filePath2);
            file2.Close();

            string filePath2a = Path.Combine(_pathOriginal, "ChildFolder5", fileName2);
            var file2a = File.Create(filePath2a);
            file2a.Close();

            string fileName3 = "FooBar" + ".pdf";
            string filePath3 = Path.Combine(_pathOriginal, "ChildFolder5", fileName3);
            var file3 = File.Create(filePath3);
            file3.Close();

            List<string> filePaths = PathLibrary.ListFilePathsInDirectory(_pathOriginal, includeSubDirectories: false, ofFileExtension: extension);

            Assert.AreEqual(1, filePaths.Count);
            Assert.IsTrue(filePaths.Contains(filePath1));
            Assert.IsFalse(filePaths.Contains(filePath2));
            Assert.IsFalse(filePaths.Contains(filePath2a));
            Assert.IsFalse(filePaths.Contains(filePath3));
        }

        [Test]
        public void ListFilePathsInDirectory_of_Valid_Directory_And_SubDirectories_Returns_List_of_Matching_Extensions()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string extension = ".txt";

            string fileName1 = "FooBar" + extension;
            string filePath1 = Path.Combine(_pathOriginal, fileName1);
            var file1 = File.Create(filePath1);
            file1.Close();

            string fileName2 = "MooNar" + extension;
            string filePath2 = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", fileName2);
            var file2 = File.Create(filePath2);
            file2.Close();

            string filePath2a = Path.Combine(_pathOriginal, "ChildFolder5", fileName2);
            var file2a = File.Create(filePath2a);
            file2a.Close();

            string fileName3 = "FooBar" + ".pdf";
            string filePath3 = Path.Combine(_pathOriginal, "ChildFolder5", fileName3);
            var file3 = File.Create(filePath3);
            file3.Close();

            List<string> filePaths = PathLibrary.ListFilePathsInDirectory(_pathOriginal, includeSubDirectories: true, ofFileExtension: extension);

            Assert.AreEqual(3, filePaths.Count);
            Assert.IsTrue(filePaths.Contains(filePath1));
            Assert.IsTrue(filePaths.Contains(filePath2));
            Assert.IsTrue(filePaths.Contains(filePath2a));
            Assert.IsFalse(filePaths.Contains(filePath3));
        }

        [Test]
        public void ListFilePathsInDirectory_of_Valid_Directory_And_SubDirectories_Returns_List_of_Matching_Names()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string extension = ".txt";

            string fileName = "FooBar";
            string fileName1 = fileName + extension;
            string filePath1 = Path.Combine(_pathOriginal, fileName1);
            var file1 = File.Create(filePath1);
            file1.Close();

            string fileName2 = "MooNar" + extension;
            string filePath2 = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", fileName2);
            var file2 = File.Create(filePath2);
            file2.Close();

            string filePath2a = Path.Combine(_pathOriginal, "ChildFolder5", fileName2);
            var file2a = File.Create(filePath2a);
            file2a.Close();

            string fileName3 = fileName + ".pdf";
            string filePath3 = Path.Combine(_pathOriginal, "ChildFolder5", fileName3);
            var file3 = File.Create(filePath3);
            file3.Close();

            List<string> filePaths = PathLibrary.ListFilePathsInDirectory(_pathOriginal, includeSubDirectories: true, ofFileName: fileName);

            Assert.AreEqual(2, filePaths.Count);
            Assert.IsTrue(filePaths.Contains(filePath1));
            Assert.IsFalse(filePaths.Contains(filePath2));
            Assert.IsFalse(filePaths.Contains(filePath2a));
            Assert.IsTrue(filePaths.Contains(filePath3));
        }

        [Test]
        public void ListFilePathsInDirectory_of_Valid_Directory_And_SubDirectories_Returns_List_of_Matching_Names_and_Extensions()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string extension = ".txt";

            string fileName = "FooBar";
            string fileName1 = fileName + extension;
            string filePath1 = Path.Combine(_pathOriginal, fileName1);
            var file1 = File.Create(filePath1);
            file1.Close();

            string fileName2 = "MooNar" + extension;
            string filePath2 = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", fileName2);
            var file2 = File.Create(filePath2);
            file2.Close();

            string filePath2a = Path.Combine(_pathOriginal, "ChildFolder5", fileName2);
            var file2a = File.Create(filePath2a);
            file2a.Close();

            string fileName3 = fileName + ".pdf";
            string filePath3 = Path.Combine(_pathOriginal, "ChildFolder5", fileName3);
            var file3 = File.Create(filePath3);
            file3.Close();

            List<string> filePaths = PathLibrary.ListFilePathsInDirectory(_pathOriginal, includeSubDirectories: true, ofFileName: fileName, ofFileExtension: extension);

            Assert.AreEqual(1,filePaths.Count);
            Assert.IsTrue(filePaths.Contains(filePath1));
            Assert.IsFalse(filePaths.Contains(filePath2));
            Assert.IsFalse(filePaths.Contains(filePath2a));
            Assert.IsFalse(filePaths.Contains(filePath3));
        }

        [Test]
        public void ListFilePathsInDirectory_of_Valid_Directory_And_SubDirectories_Excluding_File_Name_Returns_List_of_NonMatching_Names()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string extension = ".txt";

            string fileName = "FooBar";
            string fileName1 = fileName + extension;
            string filePath1 = Path.Combine(_pathOriginal, fileName1);
            var file1 = File.Create(filePath1);
            file1.Close();

            string fileName2 = "MooNar" + extension;
            string filePath2 = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", fileName2);
            var file2 = File.Create(filePath2);
            file2.Close();

            string filePath2a = Path.Combine(_pathOriginal, "ChildFolder5", fileName2);
            var file2a = File.Create(filePath2a);
            file2a.Close();

            string fileName3 = fileName + ".pdf";
            string filePath3 = Path.Combine(_pathOriginal, "ChildFolder5", fileName3);
            var file3 = File.Create(filePath3);
            file3.Close();

            List<string> filePaths = PathLibrary.ListFilePathsInDirectory(_pathOriginal, includeSubDirectories: true, ofFileName: fileName, excludeOfFileName: true);

            Assert.AreEqual(2, filePaths.Count);
            Assert.IsFalse(filePaths.Contains(filePath1));
            Assert.IsTrue(filePaths.Contains(filePath2));
            Assert.IsTrue(filePaths.Contains(filePath2a));
            Assert.IsFalse(filePaths.Contains(filePath3));
        }

        [Test]
        public void ListFilePathsInDirectory_of_Valid_Directory_And_SubDirectories_Excluding_File_Extension_Returns_List_of_NonMatching_Extensions()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string extension = ".txt";

            string fileName = "FooBar";
            string fileName1 = fileName + extension;
            string filePath1 = Path.Combine(_pathOriginal, fileName1);
            var file1 = File.Create(filePath1);
            file1.Close();

            string fileName2 = "MooNar" + extension;
            string filePath2 = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", fileName2);
            var file2 = File.Create(filePath2);
            file2.Close();

            string filePath2a = Path.Combine(_pathOriginal, "ChildFolder5", fileName2);
            var file2a = File.Create(filePath2a);
            file2a.Close();

            string fileName3 = fileName + ".pdf";
            string filePath3 = Path.Combine(_pathOriginal, "ChildFolder5", fileName3);
            var file3 = File.Create(filePath3);
            file3.Close();

            List<string> filePaths = PathLibrary.ListFilePathsInDirectory(_pathOriginal, includeSubDirectories: true, ofFileExtension: extension, excludeOfFileName: true);

            Assert.AreEqual(1, filePaths.Count);
            Assert.IsFalse(filePaths.Contains(filePath1));
            Assert.IsFalse(filePaths.Contains(filePath2));
            Assert.IsFalse(filePaths.Contains(filePath2a));
            Assert.IsTrue(filePaths.Contains(filePath3));
        }

        [Test]
        public void ListFilePathsInDirectory_of_Valid_Directory_And_SubDirectories_Excluding_File_Name_and_Extension_Returns_List_of_NonMatching_Files()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string extension = ".txt";

            string fileName = "FooBar";
            string fileName1 = fileName + extension;
            string filePath1 = Path.Combine(_pathOriginal, fileName1);
            var file1 = File.Create(filePath1);
            file1.Close();

            string fileName2 = "MooNar" + extension;
            string filePath2 = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", fileName2);
            var file2 = File.Create(filePath2);
            file2.Close();

            string filePath2a = Path.Combine(_pathOriginal, "ChildFolder5", fileName2);
            var file2a = File.Create(filePath2a);
            file2a.Close();

            string fileName3 = fileName + ".pdf";
            string filePath3 = Path.Combine(_pathOriginal, "ChildFolder5", fileName3);
            var file3 = File.Create(filePath3);
            file3.Close();

            List<string> filePaths = PathLibrary.ListFilePathsInDirectory(_pathOriginal, includeSubDirectories: true, ofFileName: fileName, ofFileExtension: extension, excludeOfFileName: true);

            Assert.AreEqual(3, filePaths.Count);
            Assert.IsFalse(filePaths.Contains(filePath1));
            Assert.IsTrue(filePaths.Contains(filePath2));
            Assert.IsTrue(filePaths.Contains(filePath2a));
            Assert.IsTrue(filePaths.Contains(filePath3));
        }

        [Test]
        public void ListFilePathsReadOnly_of_Valid_Directory_Returns_List_of_Matching_Files()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string extension = ".txt";

            string fileName = "FooBar1";
            string fileName1 = fileName + extension;
            string filePath1 = Path.Combine(_pathOriginal, fileName1);
            var file1 = File.Create(filePath1);
            file1.Close();
            File.SetAttributes(filePath1, FileAttributes.ReadOnly);

            string fileName2 = "MooNar1" + extension;
            string filePath2 = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", fileName2);
            var file2 = File.Create(filePath2);
            file2.Close();
            
            string filePath2a = Path.Combine(_pathOriginal, "ChildFolder5", fileName2);
            var file2a = File.Create(filePath2a);
            file2a.Close();

            string fileName3 = fileName + ".pdf";
            string filePath3 = Path.Combine(_pathOriginal, "ChildFolder5", fileName3);
            var file3 = File.Create(filePath3);
            file3.Close();

            

            List<string> filePaths = PathLibrary.ListFilePathsReadOnly(_pathOriginal, includeSubDirectories: false);

            Assert.AreEqual(1, filePaths.Count);
            Assert.IsTrue(filePaths.Contains(filePath1));
        }

        [Test]
        public void ListFilePathsReadOnly_of_Valid_Directory_And_SubDirectories_Returns_List_of_Matching_Files()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string extension = ".txt";

            string fileName = "FooBar2";
            string fileName1 = fileName + extension;
            string filePath1 = Path.Combine(_pathOriginal, fileName1);
            var file1 = File.Create(filePath1);
            file1.Close();

            string fileName2 = "MooNar2" + extension;
            string filePath2 = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", fileName2);
            var file2 = File.Create(filePath2);
            file2.Close();
            File.SetAttributes(filePath2, FileAttributes.ReadOnly);

            string filePath2a = Path.Combine(_pathOriginal, "ChildFolder5", fileName2);
            var file2a = File.Create(filePath2a);
            file2a.Close();

            string fileName3 = fileName + ".pdf";
            string filePath3 = Path.Combine(_pathOriginal, "ChildFolder5", fileName3);
            var file3 = File.Create(filePath3);
            file3.Close();
            File.SetAttributes(filePath3, FileAttributes.ReadOnly);

            List<string> filePaths = PathLibrary.ListFilePathsReadOnly(_pathOriginal, includeSubDirectories: true);

            Assert.AreEqual(2, filePaths.Count);
            Assert.IsTrue(filePaths.Contains(filePath2));
            Assert.IsTrue(filePaths.Contains(filePath3));
        }

        [Test]
        public void ListFilePathsReadOnly_of_Valid_Directory_And_SubDirectories_Excluding_ReadOnly_Returns_List_of_NonMatching_Files()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string extension = ".txt";

            string fileName = "FooBar3";
            string fileName1 = fileName + extension;
            string filePath1 = Path.Combine(_pathOriginal, fileName1);
            var file1 = File.Create(filePath1);
            file1.Close();

            string fileName2 = "MooNar3" + extension;
            string filePath2 = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", fileName2);
            var file2 = File.Create(filePath2);
            file2.Close();
            File.SetAttributes(filePath2, FileAttributes.ReadOnly);

            string filePath2a = Path.Combine(_pathOriginal, "ChildFolder5", fileName2);
            var file2a = File.Create(filePath2a);
            file2a.Close();

            string fileName3 = fileName + ".pdf";
            string filePath3 = Path.Combine(_pathOriginal, "ChildFolder5", fileName3);
            var file3 = File.Create(filePath3);
            file3.Close();

            List<string> filePaths = PathLibrary.ListFilePathsReadOnly(_pathOriginal, includeSubDirectories: true, excludeFile: true);

            Assert.AreEqual(3, filePaths.Count);
            Assert.IsTrue(filePaths.Contains(filePath1));
            Assert.IsFalse(filePaths.Contains(filePath2));
            Assert.IsTrue(filePaths.Contains(filePath2a));
            Assert.IsTrue(filePaths.Contains(filePath3));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void ListDirectoryNames_Of_Invalid_Path_Returns_Empty_List(string path)
        {
            List<string> allDirectories = PathLibrary.ListDirectoryNames(path);
            Assert.AreEqual(0, allDirectories.Count);
        }

        [TestCase(@"\Foo\Bar\Mee\Too.But_Not\You","Bar")]
        [TestCase(@"..\Foo\Bar\Mee\Too.But_Not\You", "Bar")]
        [TestCase(@"C:\Foo\Bar\Mee\Too.But_Not\You", "Bar")]
        [TestCase(@"C:\\Foo\\Bar\\Mee\\Too.But_Not\\You", "Bar")]
        [TestCase(@"C:\\Foo\\\Bar\\\Mee\\\Too.But_Not\\You", "Bar")]
        public void ListDirectoryNames_Of_Valid_Path_Returns_List_Of_Directory_Names(string path, string expectedDirectory)
        {
            List<string> allDirectories = PathLibrary.ListDirectoryNames(path);
            Assert.IsTrue(allDirectories.Contains(expectedDirectory));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        [TestCase("FooBar")]
        public void ListDirectories_Of_Invalid_Path_Returns_Empty_List(string path)
        {
            List<string> allDirectories = PathLibrary.ListDirectories(path);
            Assert.AreEqual(0, allDirectories.Count);
        }

        [Test]
        public void ListDirectories_With_No_SubDirectories_Included_Lists_All_First_Child_Directories()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);
            
            List<string> allDirectories = PathLibrary.ListDirectories(_pathOriginal, listSubDirectories: false);
            
            // Check random child folder
            string expectedChild = Path.Combine(_pathOriginal, "ChildFolder2");
            Assert.IsTrue(allDirectories.Contains(expectedChild));
        }

        [Test]
        public void ListDirectories_With_No_SubDirectories_Included_Does_Not_List_Second_Child_Directories()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            List<string> allDirectories = PathLibrary.ListDirectories(_pathOriginal, listSubDirectories: false);

            // Check that random subchild folder is not present
            string expectedSubChild = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder");
            Assert.IsFalse(allDirectories.Contains(expectedSubChild));
        }

        [Test]
        public void ListDirectories_With_SubDirectories_Included_Lists_All_Child_Directories_At_Any_Level()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            List<string> allDirectories = PathLibrary.ListDirectories(_pathOriginal);

            // Check that random subSubchild folder is present
            string expectedSubChild = Path.Combine(_pathOriginal, "ChildFolder3", "SubChildFolder", "SubSubChildFolder");
            Assert.IsTrue(allDirectories.Contains(expectedSubChild));
        }

        [Test]
        public void ListDirectoriesByName_of_Valid_Directory_Returns_List_of_Matching_Names()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string folderName = "ChildFolder3";
            string expectedPath = Path.Combine(_pathOriginal, folderName);

            List<string> directoryPaths = PathLibrary.ListDirectoriesByName(_pathOriginal, folderName, includeSubDirectories: false);

            Assert.AreEqual(1, directoryPaths.Count);
            Assert.IsTrue(directoryPaths.Contains(expectedPath));
        }

        [Test]
        public void ListDirectoriesByName_of_Valid_Directory_And_SubDirectories_Returns_List_of_Matching_Names()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string folderName = "SubChildFolder";
            string expectedPath = Path.Combine(_pathOriginal, "ChildFolder3", folderName);

            List <string> directoryPaths = PathLibrary.ListDirectoriesByName(_pathOriginal, folderName, includeSubDirectories: true);

            Assert.AreEqual(3, directoryPaths.Count);
            Assert.IsTrue(directoryPaths.Contains(expectedPath));
        }

        [Test]
        public void ListDirectoriesByName_of_Valid_Directory_And_SubDirectories_Excluding_Name_Returns_List_of_NonMatching_Names()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, "ParentFolder");
            createDirectoryHierarchy(_pathOriginal);

            string folderName = "SubChildFolder";
            string expectedPath = Path.Combine(_pathOriginal, "ChildFolder3");
            string unExpectedPath = Path.Combine(expectedPath, folderName);
            
            List<string> directoryPaths = PathLibrary.ListDirectoriesByName(_pathOriginal, folderName, includeSubDirectories: true, excludeFolder: true);

            Assert.AreEqual(10, directoryPaths.Count);
            Assert.IsTrue(directoryPaths.Contains(expectedPath));
            Assert.IsFalse(directoryPaths.Contains(unExpectedPath));
        }

        #region Helper Methods
        private void createDirectoryHierarchy(string rootPath)
        {
            Directory.CreateDirectory(rootPath);

            string pathFirstChild = Path.Combine(rootPath, "ChildFolder");
            for (int i = 0; i < 6; i++)
            {
                string currentPathFirstChild = pathFirstChild + i;
                Directory.CreateDirectory(currentPathFirstChild);
                // Add child folders on the odd numbered folders
                if (i % 2 != 0)
                {
                    string pathSecondChild = Path.Combine(currentPathFirstChild, "SubChildFolder");
                    Directory.CreateDirectory(pathSecondChild);

                    string pathThirdChild = Path.Combine(pathSecondChild, "SubSubChildFolder");
                    Directory.CreateDirectory(pathThirdChild);
                }
            }
        }
        #endregion
    }
}
