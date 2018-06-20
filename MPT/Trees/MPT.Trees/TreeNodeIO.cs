using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT.Trees
{
    public class TreeNodeIO
    {
        public static void CreateDirectories(string rootPath, ITreeNode directories,
                                     bool isRootNode = false,
                                     string demarcator = @"\")
        {
            if (isRootNode)
            {
                rootPath = AssemblePath(rootPath, directories, demarcator);
                Directory.CreateDirectory(rootPath);
            }

            List<string> paths = AssembleChildPaths(rootPath, directories, demarcator);

            // Create directories for all first-level children
            foreach (string path in paths)
            {
                Directory.CreateDirectory(path);
            }

            // Create directories within all first-level children
            for (int i = 0; i < directories.Children.Count; i++)
            {
                CreateDirectories(paths[i], directories.Children[i], demarcator: demarcator);
            }
        }

        public static string AssemblePath(string rootPath, ITreeNode node, string demarcator = @"\")
        {
            return rootPath + demarcator + node.Name;
        }
        public static List<string> AssembleChildPaths(string rootPath, ITreeNode node, string demarcator = @"\")
        {
            List<string> paths = new List<string>();
            foreach (ITreeNode child in node.Children)
            {
                paths.Add(rootPath + demarcator + child.Name);
            }

            return paths;
        }

        public static void RemoveDirectories(string rootPath, ITreeNode directories)
        {
            string currentPath = Path.Combine(rootPath, directories.Name);
            if (Directory.Exists(currentPath))
            {
                try
                {
                    Directory.Delete(currentPath, recursive: true);
                }
                catch (Exception)
                {
                    // No action. This most likely occurs explorer is open in a folder or it is read only.
                }
            }
        }
    }
}
