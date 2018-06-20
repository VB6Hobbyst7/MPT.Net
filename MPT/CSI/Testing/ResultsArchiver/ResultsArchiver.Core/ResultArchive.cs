// ***********************************************************************
// Assembly         : Csi.CSiTester.Core.DirectoryOrganizer
// Author           : Mark Thomas
// Created          : 12-18-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-18-2017
// ***********************************************************************
// <copyright file="ResultArchive.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System;
using MPT.Xml;

namespace Csi.Testing.ResultsArchiver.Core
{
    /// <summary>
    /// Creates the results archive by amassing the data for the valid location of the select results files and copying them to a new archival location.
    /// </summary>
    /// <seealso cref="System.IEquatable{ResultArchive}" />
    public class ResultArchive : IEquatable<ResultArchive>
    {
        /// <summary>
        /// The log destination directory.
        /// </summary>
        private const string LOG_DESTINATION_DIRECTORY = "logs";
       
        /// <summary>
        /// The test results XML file pattern.
        /// </summary>
        private const string TEST_RESULTS_XML_FILE_PATTERN = "test_results_*.xml";
        
        /// <summary>
        /// The test results file pattern.
        /// </summary>
        private const string TEST_RESULTS_FILE_PATTERN = "test_results_*.html";
       
        /// <summary>
        /// The log file pattern.
        /// </summary>
        private const string LOG_FILE_PATTERN = "*.LOG";

        /// <summary>
        /// The ignored directories.
        /// </summary>
        private readonly List<string> _ignoredDirectories = new List<string>()
        {
            "jstree",
            "temp"
        };


        /// <summary>
        /// Gets the path of the source log and results summary files.
        /// </summary>
        /// <value>The path source.</value>
        public string PathSource { get; }

        /// <summary>
        /// Gets the path destination for the aggregated result files.
        /// This is the directory containing all run results and should not include the name of the actual run directory.
        /// </summary>
        /// <value>The path destination.</value>
        public string PathDestination { get; }

        /// <summary>
        /// Gets state of whether or not an existing file will be overwritten during file copying.
        /// </summary>
        /// <value>The file overwrite state.</value>
        public bool OverWriteExisting { get; }

        /// <summary>
        /// Gets the application name.
        /// </summary>
        /// <value>The application name.</value>
        public string Application { get; }

        /// <summary>
        /// Gets the program version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; }
       
        /// <summary>
        /// Gets the suite name.
        /// </summary>
        /// <value>The suite.</value>
        public string Suite { get; }
        
        /// <summary>
        /// Gets the bit run.
        /// </summary>
        /// <value>The bit run.</value>
        public string BitRun { get; }
     
        /// <summary>
        /// Gets the run description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; }


        /// <summary>
        /// Gets the results summary HTML file location.
        /// </summary>
        /// <value>The results location.</value>
        public string ResultsLocation { get; private set; }

        /// <summary>
        /// The log file locations.
        /// </summary>
        private readonly List<string> _logLocations = new List<string>();
        /// <summary>
        /// Gets the log file locations.
        /// </summary>
        /// <value>The log locations.</value>
        public IList<string> LogLocations => _logLocations.AsReadOnly();


        /// <summary>
        /// Gets a value indicating whether this path source is a valid test result directory.
        /// </summary>
        /// <value><c>true</c> if this instance is valid test result directory; otherwise, <c>false</c>.</value>
        public bool IsValidTestResultDirectory { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the results and logs have been located at the source.
        /// </summary>
        /// <value><c>true</c> if the results and logs have been located; otherwise, <c>false</c>.</value>
        public bool ResultsAndLogsLocated { get; private set; }

        /// <summary>
        /// Returns true if the result archive is valid by containing the necessary files and a matching targeted run.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid => (IsValidTestResultDirectory &&
                                ResultsAndLogsLocated);


        /// <summary>
        /// Initializes a new instance of the <see cref="ResultArchive"/> class.
        /// </summary>
        /// <param name="pathSource">The path source of the log and results summary files.</param>
        /// <param name="pathDestination">The path destination for the aggregated result files. 
        /// This is the directory containing all run results and should not include the name of the actual run directory.</param>
        /// <param name="application">Application name.</param>
        /// <param name="suite">The suite name.</param>
        /// <param name="bitRun">The bit run.</param>
        /// <param name="version">The full program version. This should include the build number.</param>
        /// <param name="description">The run description.</param>
        /// <param name="overWriteExisting">True: Any existing files of matching names will be overridden.</param>
        public ResultArchive(
            string pathSource, 
            string pathDestination, 
            string application, 
            string suite, 
            string bitRun, 
            string version, 
            string description,
            bool overWriteExisting = false)
        {
            PathSource = pathSource;
            PathDestination = pathDestination;
            Application = application;
            Suite = suite;
            BitRun = bitRun;
            Version = version;
            Description = description;
            OverWriteExisting = overWriteExisting;
        }


        /// <summary>
        /// Determines whether the result archive is valid by containing the necessary files and a matching targeted run.
        /// </summary>
        /// <returns><c>true</c> if the result archive is valid; otherwise, <c>false</c>.</returns>
        public bool IsValidResult()
        {
            ConfirmValidTestResultDirectory();
            LocateResultsAndLogs();
            return IsValid;
        }

        /// <summary>
        /// Confirms the valid test result directory by the presence and content of a results summary XML file.
        /// </summary>
        public void ConfirmValidTestResultDirectory()
        {
            // Get results xml file
            string resultXmlFile;
            var resultFiles = Directory.GetFiles(PathSource, TEST_RESULTS_XML_FILE_PATTERN);
            if (resultFiles.Count() == 1)
                resultXmlFile = resultFiles[0];
            else
                return;

            try
            {
                var xml = XDocument.Load(resultXmlFile);
                if (xml.Root == null) return;
                if (!isMatchingVersion(xml.Root)) return;
                if (!isMatchingBit(xml.Root)) return;
                if (isMatchingDescription(xml.Root))
                    IsValidTestResultDirectory = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Determines whether the XML result summary is of a matching version and build number.
        /// </summary>
        /// <param name="root">The XML root.</param>
        /// <returns><c>true</c> if the XML result summary is of a matching version and build number; otherwise, <c>false</c>.</returns>
        private bool isMatchingVersion(XContainer root)
        {
            var queryVersion = from version in root.Descendants("current_test_configuration")
                                                   .Descendants("testing")
                                                   .Descendants("program")
                               select version.Element("version").ElementValueNull() + '.' +
                                      version.Element("build").ElementValueNull();
            return (from versionFromFile in queryVersion
                    where !string.IsNullOrEmpty(versionFromFile)
                    select string.CompareOrdinal(Version, versionFromFile) == 0).FirstOrDefault();
        }


        /// <summary>
        /// Determines whether the XML result summary is of a matching bit setting for the analysis run.
        /// </summary>
        /// <param name="root">The XML root.</param>
        /// <returns><c>true</c> if the XML result summary is of a matching bit setting for the analysis run; otherwise, <c>false</c>.</returns>
        private bool isMatchingBit(XContainer root)
        {
            var queryPath = from path in root.Descendants("current_test_configuration")
                                                .Descendants("testing")
                                                .Descendants("program")
                            select path.Element("path").ElementValueNull();
            // Match is made by splitting file path at '-bit' and taking the last 2 characters in the path name
            return (from path in queryPath
                    where !string.IsNullOrEmpty(path)
                    select path.Split(new[] {"-bit"}, StringSplitOptions.None) into splitPath
                    select splitPath[0] into bitFromFile
                    let bitLength = BitRun.Length
                    select (bitFromFile.Length - bitLength) >= 0 ? bitFromFile.Substring((bitFromFile.Length - bitLength), bitLength) : bitFromFile into bitFromFile
                    select (string.CompareOrdinal(BitRun, bitFromFile) == 0)).FirstOrDefault();
        }


        /// <summary>
        /// Determines whether the XML result summary is of a matching run description.
        /// </summary>
        /// <param name="root">The XML root.</param>
        /// <returns><c>true</c> if the XML result summary is of a matching run description; otherwise, <c>false</c>.</returns>
        private bool isMatchingDescription(XContainer root)
        {
            var queryDescription = from description in root.Descendants("current_test_configuration")
                                           .Descendants("testing")
                                    select description.Element("test_description").ElementValueNull();
            return (from descriptionFromFile in queryDescription
                    where !string.IsNullOrEmpty(descriptionFromFile)
                    select (string.CompareOrdinal(Description, descriptionFromFile) == 0)).FirstOrDefault();
        }


        /// <summary>
        /// Locates the result summary HTML and log files at the source path and populates the corresponding object properties.
        /// </summary>
        public void LocateResultsAndLogs()
        {
            // Get results html file
            var resultFiles = Directory.GetFiles(PathSource, TEST_RESULTS_FILE_PATTERN);
            if (resultFiles.Count() == 1)
                ResultsLocation = resultFiles[0];
            else
                return;

            // Get log files
            var modelDirectories = new DirectoryInfo(PathSource).GetDirectories();
            foreach (DirectoryInfo modelDirectory in modelDirectories)
            {
                if (!isValidDirectory(modelDirectory.Name)) continue;

                string pathModels = Path.Combine(PathSource, modelDirectory.Name);
                List<string> logLocations = Directory.GetFiles(
                                            pathModels, 
                                            LOG_FILE_PATTERN, 
                                            SearchOption.AllDirectories).ToList();
                _logLocations.AddRange(logLocations);
            }

            ResultsAndLogsLocated = (!string.IsNullOrEmpty(ResultsLocation) &&
                                      _logLocations.Count > 0);
        }

        /// <summary>
        /// Determines whether the specified model directory is valid for containing log files.
        /// </summary>
        /// <param name="modelDirectory">The model directory.</param>
        /// <returns><c>true</c> if the specified model directory is valid for containing log files; otherwise, <c>false</c>.</returns>
        private bool isValidDirectory(string modelDirectory)
        {
            return _ignoredDirectories.All(ignoredDirectory => string.CompareOrdinal(modelDirectory, ignoredDirectory) != 0);
        }




        /// <summary>
        /// Creates the results archive by creating a new directory at the destination path and copying select log and results files to the directory.
        /// </summary>
        public void CreateResultsArchive()
        {
            if (!IsValid) return;
            string pathRoot = copyResultsFile();
            if (string.IsNullOrEmpty(pathRoot)) return;

            copyLogFiles(pathRoot);
        }


        /// <summary>
        /// Copies the results summary HTML file to the destination path under a new directory name.
        /// </summary>
        /// <returns>System.String.</returns>
        private string copyResultsFile()
        {
            // Modify properties for directory names.
            string buildVersion = Version.Split('.').Last();
            string mainVersion = Version.Substring(0, Version.Length - (buildVersion.Length + 1));
            string versionDirectory = Application + " v" + mainVersion + " Build " + buildVersion;

            // Form directories and copy files.
            string currentDestination = Path.Combine(PathDestination, versionDirectory, BitRun + "-bit", Suite, Description);
            string fileName = Path.GetFileName(ResultsLocation);
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            Directory.CreateDirectory(currentDestination);
            string fileDestination = Path.Combine(currentDestination, fileName);
            if (!OverWriteExisting && File.Exists(fileDestination)) return string.Empty;
            File.Copy(ResultsLocation, fileDestination, overwrite: OverWriteExisting);

            return currentDestination;
        }


        /// <summary>
        /// Copies the log files to the destination + run directory path under a 'logs' directory.
        /// </summary>
        /// <param name="pathRoot">The path root.</param>
        private void copyLogFiles(string pathRoot)
        {
            string currentDestination = Path.Combine(pathRoot, LOG_DESTINATION_DIRECTORY);
            Directory.CreateDirectory(currentDestination);
            foreach (string logLocation in _logLocations)
            {
                if (string.IsNullOrEmpty(logLocation)) continue;

                string fileDestination = Path.Combine(currentDestination, Path.GetFileName(logLocation));
                if (!OverWriteExisting && File.Exists(fileDestination)) continue;

                File.Copy(logLocation, fileDestination, overwrite: OverWriteExisting);
            }
        }


        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(ResultArchive other)
        {
            return (other != null &&
                    Suite == other.Suite &&
                    Version == other.Version &&
                    Description == other.Description &&
                    BitRun == other.BitRun);
        }
    }
}
