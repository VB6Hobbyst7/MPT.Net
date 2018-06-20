using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace MPT.FileSystem.UnitTests
{
    [TestFixture]
    public abstract class LibraryTests_Base
    {
        protected string _assemblyFolder;
        protected string _pathOriginal;
        protected string _pathCopied;

        [SetUp]
        public virtual void Init()
        {
            _assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        //ncrunch: no coverage start
        [TearDown]
        public virtual void CleanUp()
        {

        }
        //ncrunch: no coverage end

        protected static void cleanupFiles(string path)
        {
            if (!string.IsNullOrEmpty(path) &&
                File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                DeleteFileSystemInfo(file);
                //RemoveReadOnlyAttribute(path);
                //File.Delete(path);
            }
        }

        protected static void cleanupDirectories(string path)
        {
            if (!string.IsNullOrEmpty(path) &&
                Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                DeleteFileSystemInfo(dir);
                //RemoveReadOnlyAttribute(path);
                //Directory.Delete(path, recursive: true);
            }
        }

        protected static void RemoveReadOnlyAttribute(string path)
        {
            FileAttributes attributes = File.GetAttributes(path);
            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
                File.SetAttributes(path, attributes);
            }
        }

        protected static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        protected static void DeleteFileSystemInfo(FileSystemInfo fileSystemInfo)
        {
            var directoryInfo = fileSystemInfo as DirectoryInfo;
            if (directoryInfo != null)
            {
                foreach (var childInfo in directoryInfo.GetFileSystemInfos())
                {
                    DeleteFileSystemInfo(childInfo);
                }
            }

            fileSystemInfo.Attributes = FileAttributes.Normal;
            fileSystemInfo.Delete();
        }
    }
}
