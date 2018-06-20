// ***********************************************************************
// Assembly         : Csi.CSiTester.Core.DirectoryOrganizer
// Author           : Mark Thomas
// Created          : 12-18-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-18-2017
// ***********************************************************************
// <copyright file="ResultArchiver.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using MPT.Xml;

namespace Csi.Testing.ResultsArchiver.Core
{
    /// <summary>
    /// For a given program and build, aggregates all of the result summary files and logs into an archival directory.
    /// </summary>
    public class ResultArchiver
    {
        /// <summary>
        /// The directory for run results.
        /// </summary>
        public const string DIR_RESULTS = "Results";


        /// <summary>
        /// The directory for files used to run tests.
        /// </summary>
        public const string DIR_TESTING = "Testing";

        /// <summary>
        /// The directory for archived run results.
        /// </summary>
        public const string DIR_ARCHIVE = "Archive";

        /// <summary>
        /// The configuration filename.
        /// </summary>
        public const string CONFIG_FILENAME = "ResultsArchiver.Config.xml";

        /// <summary>
        /// The run type where a single run is performed for each example selected.
        /// </summary>
        private const string RUNTYPE_SINGLE = "Single";

        /// <summary>
        /// The run type where each example is run 9 times, corresponding to each of the 9 analysis settings indicated by [P]rocess, [S]ingle Threading, and [B]its.
        /// </summary>
        private const string RUNTYPE_PSB = "PSB";

        /// <summary>
        /// The run type where examples under development are being run outside of any organized suite.
        /// </summary>
        private const string RUNTYPE_DEVELOPMENT = "Examples Development";
        
        /// <summary>
        /// The analysis bit settings run.
        /// </summary>
        private static readonly List<string> _bitsRun = new List<string>()
        {
            "32",
            "64"
        };

        /// <summary>
        /// Gets the list of analysis bit settings that can be run.
        /// </summary>
        /// <value>The bits run.</value>
        public static IList<string> BitsRun => _bitsRun.AsReadOnly();

        /// <summary>
        /// The standard description used for a PSB test run.
        /// </summary>
        private const string DESCRIPTION_PSB_STANDARD = "P2 S2 B1 - Standard";

        /// <summary>
        /// The standard description used for a single test run.
        /// </summary>
        private const string DESCRIPTION_SINGLE_RUN_STANDARD = "Run as is";

        /// <summary>
        /// The PSB test descriptions.
        /// </summary>
        private static readonly List<string> _psbTestDescriptions = new List<string>()
        {
            "P1 S1 B1",
            "P1 S2 B1",
            "P1 S3 B1",
            "P2 S1 B1",
            "P2 S1 B2",
            DESCRIPTION_PSB_STANDARD,
            "P2 S2 B2",
            "P2 S3 B1",
            "P2 S3 B2",
        };

        /// <summary>
        /// Gets the PSB test descriptions. These correspond to analysis settings (Process, Solver, Bit).
        /// </summary>
        /// <value>The PSB test descriptions.</value>
        public static IList<string> PsbTestDescriptions => _psbTestDescriptions.AsReadOnly();

        /// <summary>
        /// The standard suite names.
        /// </summary>
        private static readonly List<string> _standardSuites = new List<string>()
        {
            "Analysis",
            "Design",
            "Regression"
        };

        /// <summary>
        /// Gets the standard suite names.
        /// </summary>
        /// <value>The standard suite names.</value>
        public static IList<string> StandardSuites => _standardSuites.AsReadOnly();

        /// <summary>
        /// The list of result archives that are able to be created.
        /// </summary>
        private readonly List<ResultArchive> _resultDestinations = new List<ResultArchive>();

        /// <summary>
        /// Gets the path root to <see cref="ResultArchiver"/> or the running executable. All paths to expected results, archives, etc. are relative on this.
        /// </summary>
        /// <value>The path root to <see cref="ResultArchiver"/> or the running executable.</value>
        public string PathRoot { get; private set; }

        /// <summary>
        /// Gets the application name.
        /// </summary>
        /// <value>The application name.</value>
        public string Application { get; }

        /// <summary>
        /// Gets the full version of the program, including the build number.
        /// </summary>
        /// <value>The full version of the program, including the build number.</value>
        public string Version { get; }

        /// <summary>
        /// The suite names.
        /// </summary>
        private readonly List<string> _suiteNames = new List<string>();
        /// <summary>
        /// Gets the suite names.
        /// </summary>
        /// <value>The suite names.</value>
        public IList<string> SuiteNames => _suiteNames.AsReadOnly();

        /// <summary>
        /// The test descriptions. These are used to match the correct results files with the corresponding test description value.
        /// </summary>
        private List<string> _testDescriptions = new List<string>();
        /// <summary>
        /// Gets the test descriptions. These are used to match the correct results files with the corresponding test description value.
        /// </summary>
        /// <value>The test descriptions.</value>
        public IList<string> TestDescriptions => _testDescriptions.AsReadOnly();

        /// <summary>
        /// Gets the type of test run.
        /// </summary>
        /// <value>The type of test run.</value>
        public string RunType { get; }

        /// <summary>
        /// True: Existing files can be overwritten when copying files.
        /// </summary>
        /// <value>The overwrite state.</value>
        public bool OverWriteExisting { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultArchiver"/> class.
        /// </summary>
        /// <param name="configFilePath">Full path and/or filename to any configuration file that is not at the location of the <paramref name="pathRoot"/> and/or not of the default configuration file name.</param>
        /// <param name="pathRoot">Root path overwrite. If not provided, it is taken to the the location of the running assembly.</param>
        public ResultArchiver(string configFilePath = "", string pathRoot = "")
        {
            if (setRootPath(pathRoot) == null) return;

            string configXmlFile = getConfigurationFilePath(configFilePath);
            if (!File.Exists(configXmlFile)) return;

            XElement xmlRoot = getXmlRoot(configXmlFile);
            if (xmlRoot == null) return;

            Application = getApplicationName(xmlRoot);
            Version = getApplicationVersion(xmlRoot);
            RunType = getRunType(xmlRoot);
            OverWriteExisting = getOverWriteExisting(xmlRoot);

            _suiteNames = getSuiteNames(xmlRoot);

            _testDescriptions = getTestDescriptions(xmlRoot);
            confirmTestDescriptions(_testDescriptions);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultArchiver"/> class.
        /// </summary>
        /// <param name="application">The application name.</param>
        /// <param name="version">The application version, including the build number, in the form of xx.x.x.xxxx.</param>
        /// <param name="runType">Type of test run.</param>
        /// <param name="suiteNames">The suite names to filter archived results by.</param>
        /// <param name="testDescriptions">The test descriptions to filter archived results by.</param>
        /// <param name="overWriteExisting">True: Any existing files of matching names will be overridden.</param>
        public ResultArchiver(
            string application, 
            string version, 
            string runType, 
            List<string> suiteNames = null, 
            List<string> testDescriptions = null,
            bool overWriteExisting = false)
        {
            if (setRootPath() == null) return;

            Application = application;
            Version = version;
            RunType = getRunType(runType);
            OverWriteExisting = overWriteExisting;

            _suiteNames = suiteNames ?? new List<string>();

            _testDescriptions = testDescriptions;
            confirmTestDescriptions(_testDescriptions);
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
        /// Confirms the test descriptions. If none are provided, a default is used.
        /// </summary>
        /// <param name="testDescriptions">The test descriptions.</param>
        private void confirmTestDescriptions(IReadOnlyCollection<string> testDescriptions = null)
        {
            if (testDescriptions == null || testDescriptions.Count == 0)
            {
                _testDescriptions = isPsb() ? new List<string>(PsbTestDescriptions) : new List<string> { DESCRIPTION_SINGLE_RUN_STANDARD };
            }
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
                if (File.Exists(Path.Combine(PathRoot, configFileName)))
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
        /// Gets the application version from the XML element.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>System.String.</returns>
        private static string getApplicationVersion(XContainer root)
        {
            return root.Element("version").ElementValueNull();
        }

        /// <summary>
        /// Gets the type of test run indicated in the XML element.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>System.String.</returns>
        private static string getRunType(XElement root)
        {
            string runType = root.AttributeValueNull("runType");
            return getRunType(runType);
        }

        /// <summary>
        /// Gets the type of test run in a standardized form.
        /// </summary>
        /// <param name="runType">Type of the run.</param>
        /// <returns>System.String.</returns>
        private static string getRunType(string runType)
        {
            if (runType == null) return "";

            switch (runType.ToLower())
            {
                case "single":
                    return RUNTYPE_SINGLE;
                case "psb":
                    return RUNTYPE_PSB;
                case "development":
                    return RUNTYPE_DEVELOPMENT;
                default:
                    return runType;
            }
        }

        /// <summary>
        /// Gets the file overwrite state from the XML element.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns><c>true</c> if the file is to overwrite an existing file, <c>false</c> otherwise.</returns>
        private static bool getOverWriteExisting(XElement root)
        {
            string overWriteExisting = root.AttributeValueNull("overWriteExisting");
            return getAttributeBoolean(overWriteExisting);
        }

        /// <summary>
        /// Gets the boolean attribute value in a standardized form.
        /// </summary>
        /// <param name="attributeBoolean">The boolean as a string.</param>
        /// <returns><c>true</c> if string is of a 'true' form, <c>false</c> otherwise.</returns>
        private static bool getAttributeBoolean(string attributeBoolean)
        {
            if (attributeBoolean == null) return false;

            switch (attributeBoolean.ToLower())
            {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the test suite names from the XML element.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        private static List<string> getSuiteNames(XContainer root)
        {
            return  root.Descendants("suite_name")
                        .Select(x => x.ElementValueNull())
                        .ToList();
        }

        /// <summary>
        /// Gets the test run descriptions from the XML element.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        private static List<string> getTestDescriptions(XContainer root)
        {
            return root.Descendants("test_description")
                        .Select(x => x.ElementValueNull())
                        .ToList();
        }

        /// <summary>
        /// Archives the result summary and log files of the specified test descriptions.
        /// </summary>
        public void Archive()
        {
            if (!Directory.Exists(PathRoot) ||
                string.IsNullOrEmpty(RunType) ||
                string.IsNullOrEmpty(Version) ||
                string.IsNullOrEmpty(Application)) return;
            
            // Get list of suites
            string pathTestingSource = Path.Combine(PathRoot, DIR_TESTING, RunType);
            if (!Directory.Exists(pathTestingSource)) return;

            // It is assumed that all folders starting with '_' are auxiliary
            List<string> suites = (from directoryInfo in new DirectoryInfo(pathTestingSource).GetDirectories()
                                   where directoryInfo.Name[0] != '_'
                                   select directoryInfo.Name).ToList();

            // Get results to archive
            foreach (string suite in suites)
            {
                // Remove 'regTest-' prefix to non-PSB child directories
                string suiteName = suite.Replace("regTest-", string.Empty);

                // Only get suite results for those specified, or all  if none are specified
                if (_suiteNames.Count == 0 || _suiteNames.Contains(suiteName))
                {
                    getSuiteResults(suiteName, _testDescriptions);
                }
            }

            // Create archive folder
            createArchives();
        }


        /// <summary>
        /// Gets the suite result files as a <seealso cref="ResultArchive"/> for each test description that can be found and adds it to the object internal list if unique.
        /// </summary>
        /// <param name="suite">The suite name to get results for.</param>
        /// <param name="testDescriptions">The test descriptions to get results for.</param>
        private void getSuiteResults(string suite,
            List<string> testDescriptions)
        {
            string pathResults = Path.Combine(PathRoot, DIR_RESULTS);
            pathResults = isDevelopment() ? 
                Path.Combine(pathResults, RUNTYPE_DEVELOPMENT, Application) : 
                Path.Combine(pathResults, Application, suite, RunType);

            string pathArchive = Path.Combine(PathRoot, DIR_ARCHIVE);

            // Look through all directories, starting with the newest and working backwards by date
            // See: https://stackoverflow.com/questions/5049202/sorting-files-by-date/23839158#23839158
            var sortedDirectories = new DirectoryInfo(pathResults).GetDirectories()
                                              .OrderByDescending(f => f.LastWriteTime.Year <= 1601 ? f.CreationTime : f.LastWriteTime)
                                              .ToList();

            // Get directories for matching results
            try
            {
                foreach (DirectoryInfo sortedDirectory in sortedDirectories)
                {
                    string pathResultsSortedByTime = sortedDirectory.FullName;
                    int totalMatchingResults = 0;
                    foreach (string bitRun in _bitsRun)
                    {
                        totalMatchingResults += getResults(pathResultsSortedByTime, pathArchive, suite, bitRun, testDescriptions);
                        if ((testDescriptions.Count * _bitsRun.Count) == totalMatchingResults) break;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        /// <summary>
        /// Creates a <seealso cref="ResultArchive"/> for each test description that can be found and adds it to the object internal list if unique.
        /// </summary>
        /// <param name="pathSource">The path source of the log and results summary files.</param>
        /// <param name="pathDestination">The path destination for the aggregated result files. 
        /// This is the directory containing all run results and should not include the name of the actual run directory.</param>
        /// <param name="suite">The suite name.</param>
        /// <param name="bitRun">The bit run.</param>
        /// <param name="testDescriptions">The test descriptions to get results for.</param>
        /// <returns>System.Int32.</returns>
        private int getResults(string pathSource, 
            string pathDestination, 
            string suite, 
            string bitRun, 
            IReadOnlyCollection<string> testDescriptions)
        {
            int matchingResults = 0;
            foreach (string testDescription in testDescriptions)
            {
                try
                {
                    ResultArchive resultDestination = new ResultArchive(pathSource, pathDestination, Application, suite, bitRun, Version, testDescription, OverWriteExisting);
                    if (resultDestination.IsValidResult() &&
                        !_resultDestinations.Contains(resultDestination))
                    {
                        _resultDestinations.Add(resultDestination);
                        matchingResults++;
                    }
                    if (testDescriptions.Count == matchingResults) break;
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            return matchingResults;
        }

        /// <summary>
        /// Creates the archives of run result log and summary files by copying the files to the archived directories.
        /// </summary>
        private void createArchives()
        {
            foreach (ResultArchive resultDestination in _resultDestinations)
            {
                resultDestination.CreateResultsArchive();
            }
        }


        /// <summary>
        /// Determines whether this instance is for a PSB test run.
        /// </summary>
        /// <returns><c>true</c> if this instance is for a PSB test run; otherwise, <c>false</c>.</returns>
        private bool isPsb() { return (string.CompareOrdinal(RunType, RUNTYPE_PSB) == 0); }

        /// <summary>
        /// Determines whether this instance is for an example development test run.
        /// </summary>
        /// <returns><c>true</c> if this instance is an example development test run; otherwise, <c>false</c>.</returns>
        private bool isDevelopment() { return (string.CompareOrdinal(RunType, RUNTYPE_DEVELOPMENT) == 0); }
    }
}
