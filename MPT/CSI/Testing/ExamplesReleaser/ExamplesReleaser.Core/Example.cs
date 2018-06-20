using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using MPT.Xml;

namespace Csi.Testing.ExamplesReleaser.Core
{
    /// <summary>
    /// Represents the example considered for release. If it is in the proper state, it will also contain the data and methods for copying the relevant files to their release location.
    /// </summary>
    /// <seealso cref="System.IEquatable{Example}" />
    public class Example : IEquatable<Example>
    {
        private static readonly XNamespace _ns = "http://www.csiberkeley.com";

        /// <summary>
        /// Gets the path of the model control file for the example.
        /// </summary>
        /// <value>The path of the model control file for the example.</value>
        public string PathModelControlFile { get; private set; }

        /// <summary>
        /// Gets the path to the model file.
        /// </summary>
        /// <value>The path to the model file.</value>
        public string PathModelFile { get; private set; }

        /// <summary>
        /// The the paths to all supporting files.
        /// </summary>
        private readonly List<string> _pathSupportingFiles = new List<string>();
        /// <summary>
        /// Gets the paths to all supporting files.
        /// </summary>
        /// <value>The the paths to all supporting files.</value>
        public IList<string> PathSupportingFiles => _pathSupportingFiles.AsReadOnly();

        /// <summary>
        /// The root path destination for released example files.
        /// </summary>
        /// <value>The path destination.</value>
        public string PathDestinationRoot { get; }

        /// <summary>
        /// Gets the suite directory, such as Analysis or Design.
        /// </summary>
        /// <value>The suite directory.</value>
        public string SuiteDirectory { get; private set; }

        /// <summary>
        /// Gets the directory of the sub-suite, such as steel frame design as a sub suite of design.
        /// </summary>
        /// <value>The directory of the sub-suite, such as steel frame design as a sub suite of design.</value>
        public string SubSuiteDirectory { get; private set; }

        /// <summary>
        /// Gets the application name.
        /// </summary>
        /// <value>The application name.</value>
        public string Application { get; }

        /// <summary>
        /// Gets a value indicating whether this example is of a matching program.
        /// </summary>
        /// <value><c>true</c> if this example is of a  matching program; otherwise, <c>false</c>.</value>
        public bool IsMatchingProgram { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this example is set for public release.
        /// </summary>
        /// <value><c>true</c> if this example is set for public release; otherwise, <c>false</c>.</value>
        public bool IsSetPublic { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this example is complete.
        /// </summary>
        /// <value><c>true</c> if this example is complete; otherwise, <c>false</c>.</value>
        public bool IsComplete { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the example has been turned off from regTest runs.
        /// </summary>
        /// <value><c>true</c> if the example has been turned off from regTest runs; otherwise, <c>false</c>.</value>
        public bool IsNotTurnedOff { get; private set; }

        /// <summary>
        /// Returns true if the example is valid for released based on the current example state.
        /// </summary>
        /// <value><c>true</c> if this example is valid for release; otherwise, <c>false</c>.</value>
        public bool IsValid => (IsMatchingProgram &&
                                IsSetPublic &&
                                IsComplete &&
                                IsNotTurnedOff);

        /// <summary>
        /// Initializes a new instance of the <see cref="Example"/> class.
        /// </summary>
        /// <param name="pathModelControlFile">The path of the model control file for the example.</param>
        /// <param name="pathDestinationRoot">The root path destination for released example files.</param>
        /// <param name="application">Application name.</param>
        public Example(
            string pathModelControlFile,
            string pathDestinationRoot,
            string application)
        {
            PathModelControlFile = pathModelControlFile;
            PathDestinationRoot = pathDestinationRoot;
            Application = application;
        }

        /// <summary>
        /// Determines whether the example is valid for release by the presence and content of a model control file.
        /// </summary>
        /// <returns><c>true</c> if example is valid for release; otherwise, <c>false</c>.</returns>
        public bool IsValidForRelease()
        {
            confirmValidExampleDirectory();
            return IsValid;
        }


        /// <summary>
        /// Confirms the valid example directory by the presence and content of a model control XML file.
        /// </summary>
        private void confirmValidExampleDirectory()
        {
            var xmlRoot = getXmlRoot(PathModelControlFile);
            if (xmlRoot == null) return;
            IsMatchingProgram = isMatchingProgram(xmlRoot);
            IsSetPublic = isPublic(xmlRoot);
            IsComplete = isComplete(xmlRoot);
            IsNotTurnedOff = isActive(xmlRoot);
            if (IsValid) getExampleData(xmlRoot);
        }

        /// <summary>
        /// Gets the XML root node.
        /// </summary>
        /// <param name="xmlFilePath">The configuration XML file path.</param>
        /// <returns>XElement.</returns>
        private static XElement getXmlRoot(string xmlFilePath)
        {
            XDocument xml = null;
            try
            {
                xml = XDocument.Load(xmlFilePath);
            }
            catch (Exception)
            {
                // Do nothing
            }
            return xml?.Root;
        }

        /// <summary>
        /// Determines whether the XML result summary is of a matching version and build number.
        /// </summary>
        /// <param name="root">The XML root.</param>
        /// <returns><c>true</c> if [is matching version] [the specified root]; otherwise, <c>false</c>.</returns>
        private bool isMatchingProgram(XContainer root)
        {
            var queryPrograms = from program in root.Descendants(_ns + "target_program")
                                                    .Descendants(_ns + "program")
                                select program.Element(_ns + "name").ElementValueNull();
            
            return queryPrograms
                    .Where(program => !string.IsNullOrEmpty(program))
                    .Any(program => string.CompareOrdinal(program, Application) == 0);
        }

        /// <summary>
        /// Determines whether the specified example is set to be public.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns><c>true</c> if the specified example is set to be public; otherwise, <c>false</c>.</returns>
        private static bool isPublic(XElement root)
        {
            string isPublic = root.AttributeValueNull("is_public");
            return string.CompareOrdinal(isPublic.ToLower(), "yes") == 0;
        }

        /// <summary>
        /// Determines whether the specified example is complete.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns><c>true</c> if the specified example is complete; otherwise, <c>false</c>.</returns>
        private static bool isComplete(XElement root)
        {
            string isDone = root.AttributeValueNull("status");
            return string.CompareOrdinal(isDone.ToLower(), "done") == 0;
        }

        /// <summary>
        /// Determines whether the example is active in the regTest system.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns><c>true</c> if the example is active in the regTest system; otherwise, <c>false</c>.</returns>
        private static bool isActive(XContainer root)
        {
            // check all keywords to not contain disabling trigger
            var queryKeywords = from program in root.Descendants(_ns + "keywords")
                                select program.Elements(_ns + "keyword");

            return queryKeywords.All(queryKeyword => queryKeyword
                                                        .Select(queryKeywordList => queryKeywordList.ElementValueNull())
                                                        .Where(keyword => !string.IsNullOrEmpty(keyword))
                                                        .All(keyword => string.CompareOrdinal(keyword, "exclude from regression testing") != 0));
        }

        /// <summary>
        /// Gets the example data necessary to release the example.
        /// </summary>
        /// <param name="root">The root.</param>
        private void getExampleData(XContainer root)
        {
            string pathExample = Directory.GetParent(PathModelControlFile).FullName;
            // Get model file path
            string modelPath = root.Element(_ns + "path").ElementValueNull();
            if (modelPath[0] == '\\') modelPath = modelPath.Substring(1);
            PathModelFile = Path.Combine(pathExample, modelPath);

            // Get supporting files paths
            var queryAttachments = from attachment in root.Descendants(_ns + "attachments")
                                       select attachment.Elements(_ns + "attachment");

            foreach (var queryAttachment in queryAttachments)
            {
                foreach (var attachment in queryAttachment)
                {
                    string attachmentTitle = attachment.Element(_ns + "title").ElementValueNull();
                    if (!attachmentTitle.Contains("Supporting File:")) continue;

                    string attachmentPath = attachment.Element(_ns + "path").ElementValueNull();
                    if (attachmentPath[0] == '\\') attachmentPath = attachmentPath.Substring(1);
                    if (!string.IsNullOrEmpty(attachmentPath)) _pathSupportingFiles.Add(Path.Combine(pathExample, attachmentPath));
                }
            }


            SuiteDirectory = getSuiteFromKeywords(root);

            // Get the sub-suite directory, if any.
            SubSuiteDirectory = getSubSuiteFromKeywords(root);
            if (string.IsNullOrEmpty(SubSuiteDirectory)) SubSuiteDirectory = getSubSuiteFromClassification(root);
        }

        /// <summary>
        /// Gets the suite from keywords of types.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>System.String.</returns>
        private static string getSuiteFromKeywords(XContainer root)
        {
            // check keywords for a sub suite type
            var queryKeywords = from program in root.Descendants(_ns + "keywords")
                                select program.Elements(_ns + "keyword");

            foreach (var queryKeywordSet in queryKeywords)
            {
                foreach (var queryKeyword in queryKeywordSet)
                {
                    string keyword = queryKeyword.ElementValueNull();

                    if (!string.IsNullOrEmpty(keyword) && keyword.Contains("Type:"))
                    {
                        return keyword.Replace("Type:", "").Trim();
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the sub suite from keywords of design types.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>System.String.</returns>
        private static string getSubSuiteFromKeywords(XContainer root)
        {
            // check keywords for a sub suite type
            var queryKeywords = from program in root.Descendants(_ns + "keywords")
                                select program.Elements(_ns + "keyword");

            foreach (var queryKeywordSet in queryKeywords)
            {
                foreach (var queryKeyword in queryKeywordSet)
                {
                    string keyword = queryKeyword.ElementValueNull();

                    if (!string.IsNullOrEmpty(keyword) && keyword.Contains("Type Design:"))
                    {
                        return keyword.Replace("Type Design:", "").Trim();
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the sub suite from the example secondary classification.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>System.String.</returns>
        private static string getSubSuiteFromClassification(XContainer root)
        {
            var queryClassification = from classification in root.Descendants(_ns + "classification")
                                                                 .Descendants(_ns + "value")
                                      select classification.Element(_ns + "level_2");

            string subSuite = queryClassification.FirstOrDefault().ElementValueNull();
            string[] subclassifications = subSuite.Split('-');
            return subclassifications.Count() < 2 ? string.Empty : subclassifications[1].Trim();
        }

        // TODO: Should ReleaseExample() be part of Example.cs, or ExampleReleaser.cs? Consider analogue of analysisResults project.
        /// <summary>
        /// Releases the example by copying all relevant files to the target directory.
        /// </summary>
        public void ReleaseExample()
        {
            if (!IsValid) return;
            copyExampleFiles();
        }
        
        /// <summary>
        /// Copies the model file and any supporting files related to the example.
        /// </summary>
        private void copyExampleFiles()
        {
            if (!File.Exists(PathModelFile)) return;

            string fileName = Path.GetFileName(PathModelFile);
            if (string.IsNullOrEmpty(fileName)) return;

            string fileExtension = Path.GetExtension(PathModelFile);
            if (string.IsNullOrEmpty(fileExtension)) return;

            if (!Directory.Exists(PathDestinationRoot)) Directory.CreateDirectory(PathDestinationRoot);

            string pathDestinationPartialRoot = Path.Combine(PathDestinationRoot, SuiteDirectory);
            if (!Directory.Exists(pathDestinationPartialRoot)) Directory.CreateDirectory(pathDestinationPartialRoot);

            string pathDestinationFullRoot = Path.Combine(PathDestinationRoot, SuiteDirectory, SubSuiteDirectory);
            if (!Directory.Exists(pathDestinationFullRoot)) Directory.CreateDirectory(pathDestinationFullRoot);

            string fileDestination = Path.Combine(pathDestinationFullRoot, fileName);
            File.Copy(PathModelFile, fileDestination, overwrite: true);

            foreach (string pathSupportingFile in _pathSupportingFiles)
            {
                if (!File.Exists(pathSupportingFile)) continue;

                fileName = Path.GetFileName(pathSupportingFile);
                if (string.IsNullOrEmpty(fileName)) continue;
                fileDestination = Path.Combine(pathDestinationFullRoot, fileName);

                File.Copy(pathSupportingFile, fileDestination, overwrite: true);
            }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Example other)
        {
            if (_pathSupportingFiles.Count != other?._pathSupportingFiles.Count)  return false;
            if (_pathSupportingFiles.Any(pathSupportingFile => !other.PathSupportingFiles.Contains(pathSupportingFile)))
            {
                return false;
            }

            return (PathModelControlFile == other.PathModelControlFile &&
                    PathModelFile == other.PathModelFile &&
                    PathDestinationRoot == other.PathDestinationRoot &&
                    Application == other.Application &&
                    IsSetPublic == other.IsSetPublic &&
                    IsComplete == other.IsComplete &&
                    IsNotTurnedOff == other.IsNotTurnedOff);
        }
    }
}
