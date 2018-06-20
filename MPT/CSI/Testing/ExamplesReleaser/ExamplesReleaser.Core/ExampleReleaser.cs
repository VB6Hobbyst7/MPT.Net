using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using MPT.Xml;

namespace Csi.Testing.ExamplesReleaser.Core
{
    public class ExampleReleaser
    {
        /// <summary>
        /// The model control XML file pattern.
        /// </summary>
        public const string MODEL_CONTROL_XML_FILE_PATTERN = "*_MC.xml";

        /// <summary>
        /// The configuration filename.
        /// </summary>
        public const string CONFIG_FILENAME = "ExamplesReleaser.Config.xml";

        /// <summary>
        /// The directory for released examples to be copied to.
        /// </summary>
        public const string DIR_RELEASE = "Verification";

        /// <summary>
        /// The directory that examples are expected to be taken from.
        /// </summary>
        public const string DIR_SOURCE = "Verification";

        /// <summary>
        /// The list of examples that are set to be released.
        /// </summary>
        private readonly List<Example> _examplesToRelease = new List<Example>();

        /// <summary>
        /// Gets the list of examples set to be released.
        /// </summary>
        /// <value>The examples to be released.</value>
        public IList<Example> ExamplesToRelease => _examplesToRelease.AsReadOnly();

        /// <summary>
        /// Gets the path root to <see cref="ExampleReleaser"/> or the running executable. All paths to source and destination files are relative to this.
        /// </summary>
        /// <value>The path root to <see cref="ExampleReleaser"/> or the running executable.</value>
        public string PathRoot { get; private set; }

        /// <summary>
        /// The root directory for all examples to be released.
        /// For faster program speed, it is recommended for this to be as specific as possible for the relevant directories (e.g. choose one within the program to be released).
        /// </summary>
        /// <value>The path source.</value>
        public string PathExamplesSource { get; private set; }

        /// <summary>
        /// Path where the examples to be released will be copied to.
        /// </summary>
        /// <value>The path release.</value>
        public string PathExamplesRelease { get; private set; }


        /// <summary>
        /// Gets the application name.
        /// </summary>
        /// <value>The application name.</value>
        public string Application { get; }


        /// <summary>
        /// The standard suite names that are released.
        /// </summary>
        private static readonly List<string> _standardSuites = new List<string>()
        {
            "Analysis",
            "Design"
        };

        /// <summary>
        /// Gets the standard suite names that are released.
        /// </summary>
        /// <value>The standard suite names that are released.</value>
        public static IList<string> StandardSuites => _standardSuites.AsReadOnly();




        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleReleaser"/> class.
        /// </summary>
        /// <param name="configFilePath">Full path and/or filename to any configuration file that is not at the location of the <paramref name="pathRoot"/> and/or not of the default configuration file name.</param>
        /// <param name="pathRoot">Root path overwrite. If not provided, it is taken to the the location of the running assembly.</param>
        public ExampleReleaser(string configFilePath = "", string pathRoot = "")
        {
            if (setRootPath(pathRoot) == null) return;

            string configXmlFile = getConfigurationFilePath(configFilePath);
            if (!File.Exists(configXmlFile)) return;

            XElement xmlRoot = getXmlRoot(configXmlFile);
            if (xmlRoot == null) return;

            Application = getApplicationName(xmlRoot);
            setPathExamplesSource(getPathExamplesSource(xmlRoot));
            setPathExamplesRelease(getPathExamplesRelease(xmlRoot));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleReleaser"/> class.
        /// </summary>
        /// <param name="application">The application name.</param>
        /// <param name="pathSource">The root directory for all examples to be released.
        /// For faster program speed, it is recommended for this to be as specific as possible for the relevant directories (e.g. choose one within the program to be released).</param>
        /// <param name="pathRelease">Path where the examples to be released will be copied to.</param>
        public ExampleReleaser(string application,
            string pathSource,
            string pathRelease)
        {
            if (setRootPath() == null) return;

            Application = application;
            setPathExamplesSource(pathSource);
            setPathExamplesRelease(pathRelease);
        }

        /// <summary>
        /// Gets the configuration file path based on the content of the provided file name or path.
        /// </summary>
        /// <param name="configFilePath">The configuration file path.</param>
        /// <returns>System.String.</returns>
        private string getConfigurationFilePath(string configFilePath)
        {
            string configFileName;
            if (string.IsNullOrWhiteSpace(configFilePath))
            {
                configFileName = CONFIG_FILENAME;
                configFilePath = PathRoot;
            }
            else
            {
                configFileName = Path.GetFileName(configFilePath);
                if (File.Exists(configFilePath) || File.Exists(Path.Combine(PathRoot, configFileName)))
                {
                    configFilePath = Path.GetDirectoryName(configFilePath);
                    if (string.IsNullOrEmpty(configFilePath))
                    {
                        configFilePath = PathRoot;
                    }
                }
                else
                {
                    configFileName = CONFIG_FILENAME;
                }
                configFilePath = configFilePath ?? PathRoot;
            }

            return Path.Combine(configFilePath, configFileName);
        }

        /// <summary>
        /// Gets the XML root node.
        /// </summary>
        /// <param name="configXmlFilePath">The configuration XML file path.</param>
        /// <returns>XElement.</returns>
        private static XElement getXmlRoot(string configXmlFilePath)
        {
            XDocument xml = null;
            try
            {
                xml = XDocument.Load(configXmlFilePath);
            }
            catch (Exception)
            {
                // Do nothing
            }
            return xml?.Root;
        }

        /// <summary>
        /// Gets the application name from the XML element.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>System.String.</returns>
        private static string getApplicationName(XContainer root)
        {
            return root.Element("application").ElementValueNull();
        }

        /// <summary>
        /// Gets the path to examples to consider for release from the XML element.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>System.String.</returns>
        private static string getPathExamplesSource(XContainer root)
        {
            return root.Element("path_source").ElementValueNull();
        }

        /// <summary>
        /// Gets the path to copy released examples to from the XML element.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>System.String.</returns>
        private static string getPathExamplesRelease(XContainer root)
        {
            return root.Element("path_release").ElementValueNull();
        }

        /// <summary>
        /// Sets the root path as specified or as default.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        private string setRootPath(string path = "")
        {
            PathRoot = string.IsNullOrWhiteSpace(path)
                ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                : path;
            return PathRoot;
        }

        /// <summary>
        /// Sets the examples source path as specified or as default.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        private void setPathExamplesSource(string path)
        {
            if (Directory.Exists(path))
            {
                PathExamplesSource = path;
            }
            else if (!string.IsNullOrEmpty(path) &&
                Directory.Exists(Path.Combine(PathRoot, path)))
            {
                PathExamplesSource = Path.Combine(PathRoot, path);
            }
            else
            {
                PathExamplesSource = Path.Combine(PathRoot, @"\..\", DIR_SOURCE);
            }
        }

        /// <summary>
        /// Sets the examples release path as specified or as default.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        private void setPathExamplesRelease(string path)
        {
            PathExamplesRelease = Directory.Exists(path) ? path : Path.Combine(PathRoot, DIR_RELEASE);
        }

        /// <summary>
        /// Copies all examples to be released to the appropriate directory.
        /// </summary>
        public void Release()
        {
            if (!Directory.Exists(PathRoot) ||
                string.IsNullOrEmpty(Application)) return;

            // Get list of examples
            if (!Directory.Exists(PathExamplesSource)) return;

            // Get all directories of the type for release
            //var directories = new DirectoryInfo(PathExamplesSource).GetDirectories();
            var directories = new DirectoryInfo(PathExamplesSource).GetDirectories("*", SearchOption.AllDirectories);
            List<string> examplesSuites = (from directoryInfo in directories
                                                where _standardSuites.Contains(directoryInfo.Name)
                                                select directoryInfo.FullName).ToList();


            // Get results to archive
            foreach (string exampleSuite in examplesSuites)
            {
                getReleasedExamples(exampleSuite);
            }

            // Create archive folder
            releaseExamples();
        }


        /// <summary>
        /// Gets the examples to be released as an <seealso cref="Example"/> for each model control file set for release that can be found and adds it to the object internal list if unique.
        /// </summary>
        /// <param name="exampleSuitePath">The example directory path to get examples from.</param>
        private void getReleasedExamples(string exampleSuitePath)
        {
            // TODO: This method might be redundant/excessive
            List<string> suiteSubDirectories = (from directoryInfo in new DirectoryInfo(exampleSuitePath).GetDirectories()
                                                where directoryInfo.Name[0] != '_'
                                                select directoryInfo.FullName).ToList();

            // Get directories for matching results
            foreach (string suiteSubDirectory in suiteSubDirectories)
            {
                getExamples(suiteSubDirectory, PathExamplesRelease);
            }
        }


        /// <summary>
        /// Creates an <seealso cref="Example"/> for each valid model control file that can be found and adds it to the object internal list if unique.
        /// </summary>
        /// <param name="pathSource">The path source of example files. File from all subdirectories will be checked.</param>
        /// <param name="pathDestination">The root path destination for released example files.</param>
        private void getExamples(string pathSource,
            string pathDestination)
        {
            // Get model control xml files
            var resultFiles = Directory.GetFiles(pathSource, MODEL_CONTROL_XML_FILE_PATTERN, SearchOption.AllDirectories);

            foreach (string modelControlFilePath in resultFiles)
            {
                Example releasedExample = new Example(modelControlFilePath, pathDestination, Application);
                if (releasedExample.IsValidForRelease() &&
                    !_examplesToRelease.Contains(releasedExample))
                {
                    _examplesToRelease.Add(releasedExample);
                }
            }
        }

        /// <summary>
        /// Triggers each released example to copy itself to the appropriate location.
        /// </summary>
        private void releaseExamples()
        {
            foreach (Example resultDestination in _examplesToRelease)
            {
                resultDestination.ReleaseExample();
            }
        }
    }
}
