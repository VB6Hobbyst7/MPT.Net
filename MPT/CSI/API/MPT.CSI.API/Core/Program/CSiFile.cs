// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="CSiFile.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using IO = System.IO;
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program
{
    /// <summary>
    /// Implements all API functions within the 'File' namespace.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class CSiFile :  CSiApiBase
    {
        #region Fields        
        /// <summary>
        /// The new file name or path returned by the application before the model is saved.
        /// </summary>
        public const string NEW_FILE_NAME = "(Untitled)";

        /// <summary>
        /// File name of the current model, as tracked by the program and not obtained from the API.
        /// </summary>
        private string _fileName;

        /// <summary>
        /// File path of the current model, as tracked by the program and not obtained from the API.
        /// </summary>
        private string _filePath;
        #endregion

        #region Properties
        /// <summary>
        /// Filename of the current model file, with or without the full path.
        /// </summary>
        /// <returns>System.String.</returns>
        public string FileName =>  _fileName;

        /// <summary>
        /// Path to the current model file.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath => _filePath;

        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="CSiFile" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public CSiFile(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// Opens the specified model file if it exists.
        /// The file name must have an sdb, $2k, s2k, xlsx, xls, or mdb extension.
        /// Files with sdb extensions are opened as standard Sap2000 files.
        /// Files with $2k and s2k extensions are imported as text files.
        /// Files with xlsx and xls extensions are imported as Microsoft Excel files.
        /// Files with mdb extensions are imported as Microsoft Access files.
        /// This function returns zero if the file is successfully opened and nonzero if it is not opened.
        /// The function is only applicable when you are accessing the application API from an external application.
        /// It will return an error if you call it from VBA inside Sap2000.
        /// </summary>
        /// <param name="filePath">The full path of a model file to be opened in the application.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="IO.IOException">The following file does not exist: " + filePath</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public bool Open(string filePath)
        {
            if (!IO.File.Exists(filePath)) { throw new IO.IOException("The following file does not exist: " + filePath);}

            // TODO: Add checks for appropriate file types for the given open program.

            _callCode = _sapModel.File.OpenFile(filePath);
            if (apiCallIsSuccessful(_callCode))
            {
                while (!ModelIsLoaded())
                {
                    // Waits until model is fully loaded
                }
                setPaths(filePath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Saves the model to a file.
        /// If no file name is specified, the file is saved using its current name.
        /// </summary>
        /// <param name="filePath">The full path to which the model file is saved.</param>
        /// <returns><c>true</c> if the file has been saved, <c>false</c> otherwise.</returns>
        /// <exception cref="IO.IOException">The current model has not been previously saved. Please provide a file name.</exception>
        /// <exception cref="IO.IOException" >The saved path provided is for a read only file.\n Please change the file access or provide a different file name.</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public bool Save(string filePath = "")
        {
            if (string.IsNullOrEmpty(_filePath) && 
                string.IsNullOrEmpty(filePath)) { throw new IO.IOException("The current model has not been previously saved. Please provide a file name.");}
            if (IO.File.Exists(filePath))
            {
                IO.FileInfo fileInfo = new IO.FileInfo(filePath);
                if (fileInfo.IsReadOnly) { throw new IO.IOException("The saved path provided is for a read only file.\n Please change the file access or provide a different file name."); }
            }

            _callCode = _sapModel.File.Save(filePath);
            if (apiCallIsSuccessful(_callCode))
            {
                setPaths(filePath);
                return true;
            }
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            return false;
        }

   

        /// <summary>
        /// The function returns a string that represents the filename of the current model, with or without the full path.
        /// Object properties are also updated to the model.
        /// </summary>
        /// <param name="includePath">True: Returned filename includes the full path where the file is located.</param>
        /// <returns>System.String.</returns>
        public string UpdateModelFileName(bool includePath)
        {
            string fileName = GetModelFileName(includePath);
            if (string.CompareOrdinal(fileName, NEW_FILE_NAME) == 0)
            {
                _fileName = string.Empty;
                _filePath = string.Empty;
                return string.Empty;
            }

            if (!includePath && string.CompareOrdinal(fileName, _fileName) != 0)
            {
                _fileName = fileName;
            }
            else if (string.CompareOrdinal(fileName, _filePath) != 0)
            {
                setPaths(fileName);
            }

            return fileName;
        }

        /// <summary>
        /// Returns a string that represents the filepath of the current model.
        /// Object properties are also updated to the model.
        /// </summary>
        /// <returns>System.String.</returns>
        public string UpdateModelFilePath()
        {
            string filePath = _sapModel.GetModelFilepath();
            if (string.CompareOrdinal(filePath, NEW_FILE_NAME) == 0)
            {
                _fileName = string.Empty;
                _filePath = string.Empty;
                return string.Empty;
            }

            if (string.CompareOrdinal(filePath, _filePath) != 0)
            {
                setPaths(filePath);
            }
            return filePath;
        }

        /// <summary>
        /// The function returns a string that represents the filename of the current model, with or without the full path.
        /// </summary>
        /// <param name="includePath">True: Returned filename includes the full path where the file is located.</param>
        /// <returns>System.String.</returns>
        public string GetModelFileName(bool includePath = false)
        {
            return _sapModel.GetModelFilename(includePath);
        }

        /// <summary>
        /// Returns 'true' Models the is loaded.
        /// Otherwise, either the model is new and unsaved or is still in the process of loading.
        /// </summary>
        /// <returns><c>true</c> if the model is loaded, <c>false</c> otherwise.</returns>
        public bool ModelIsLoaded()
        {
            return (GetModelFileName() != null && // Can be null if checked while application is still initializing
                    GetModelFileName() != NEW_FILE_NAME);
        }

        /// <summary>
        /// Creates a new blank model from template.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void NewBlank()
        {
            _callCode = _sapModel.File.NewBlank();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Creates a new template model of a 2D Frame.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <param name="tempType">Type of the temporary.</param>
        /// <param name="numberStories">The number stories.</param>
        /// <param name="storyHeight">Height of the story.</param>
        /// <param name="numberBays">The number bays.</param>
        /// <param name="bayWidth">Width of the bay.</param>
        /// <param name="restraint">if set to <c>true</c> [restraint].</param>
        /// <param name="beam">The beam.</param>
        /// <param name="column">The column.</param>
        /// <param name="brace">The brace.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void New2DFrame(e2DFrameType tempType,
            int numberStories,
            double storyHeight,
            int numberBays,
            double bayWidth,
            bool restraint = true,
            string beam = "Default",
            string column = "Default",
            string brace = "Default")
        {
            _callCode = _sapModel.File.New2DFrame(EnumLibrary.Convert<e2DFrameType, CSiProgram.e2DFrameType>(tempType),
                numberStories,
                storyHeight,
                numberBays,
                bayWidth,
                restraint,
                beam, column, brace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            resetPaths();
        }
#endif
#if BUILD_SAP2000v19 || BUILD_SAP2000v20
        /// <summary>
        /// Creates a new template model of a 3D Frame.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <param name="tempType">Type of the temporary.</param>
        /// <param name="numberStories">The number stories.</param>
        /// <param name="storyHeight">Height of the story.</param>
        /// <param name="numberBaysX">The number bays x.</param>
        /// <param name="numberBaysY">The number bays y.</param>
        /// <param name="bayWidthX">The bay width x.</param>
        /// <param name="bayWidthY">The bay width y.</param>
        /// <param name="restraint">if set to <c>true</c> [restraint].</param>
        /// <param name="beam">The beam.</param>
        /// <param name="column">The column.</param>
        /// <param name="area">The area.</param>
        /// <param name="numberXDivisions">The number x divisions.</param>
        /// <param name="numberYDivisions">The number y divisions.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void New3DFrame(e3DFrameType tempType,
            int numberStories,
            double storyHeight,
            int numberBaysX, int numberBaysY,
            double bayWidthX, double bayWidthY,
            bool restraint = true,
            string beam = "Default",
            string column = "Default",
            string area = "Default",
            int numberXDivisions = 4, int numberYDivisions = 4)
        {
            _callCode = _sapModel.File.New3DFrame(EnumLibrary.Convert<e3DFrameType, CSiProgram.e3DFrameType>(tempType),
                                         numberStories,
                                         storyHeight,
                                         numberBaysX, bayWidthX,
                                         numberBaysY, bayWidthY,
                                         restraint,
                                         beam, column, area,
                                         numberXDivisions, numberYDivisions);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            resetPaths();
        }

        /// <summary>
        /// Creates a new template model of a Beam.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <param name="numberSpans">The number spans.</param>
        /// <param name="spanLength">Length of the span.</param>
        /// <param name="restraint">if set to <c>true</c> [restraint].</param>
        /// <param name="beam">The beam.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void NewBeam(int numberSpans,
            double spanLength,
            bool restraint = true,
            string beam = "Default")
        {
            _callCode = _sapModel.File.NewBeam(numberSpans,
                                    spanLength,
                                    restraint,
                                    beam);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            resetPaths();
        }

        /// <summary>
        /// Creates a new template model of a Wall.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <param name="numberXDivisions">The number x divisions.</param>
        /// <param name="numberZDivisions">The number z divisions.</param>
        /// <param name="divisionWidthX">The division width x.</param>
        /// <param name="divisionWidthZ">The division width z.</param>
        /// <param name="restraint">if set to <c>true</c> [restraint].</param>
        /// <param name="area">The area.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void NewWall(int numberXDivisions,
            int numberZDivisions,
            double divisionWidthX,
            double divisionWidthZ,
            bool restraint = true,
            string area = "Default")
        {
            _callCode = _sapModel.File.NewWall(numberXDivisions, divisionWidthX,
                                        numberZDivisions, divisionWidthZ,
                                        restraint,
                                        area);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            resetPaths();
        }

        /// <summary>
        /// Creates a new template model of a Solid.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <param name="xWidth">Width of the x.</param>
        /// <param name="yWidth">Width of the y.</param>
        /// <param name="height">The height.</param>
        /// <param name="restraint">if set to <c>true</c> [restraint].</param>
        /// <param name="solid">The solid.</param>
        /// <param name="numberXDivisions">The number x divisions.</param>
        /// <param name="numberYDivisions">The number y divisions.</param>
        /// <param name="numberZDivisions">The number z divisions.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void NewSolidBlock(double xWidth,
            double yWidth,
            double height,
            bool restraint = true,
            string solid = "Default",
            int numberXDivisions = 5,
            int numberYDivisions = 8,
            int numberZDivisions = 10)
        {
            _callCode = _sapModel.File.NewSolidBlock(xWidth, yWidth, height,
                                                restraint,
                                                solid,
                                                numberXDivisions, numberYDivisions, numberZDivisions);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            resetPaths();
        }
#elif BUILD_ETABS2013 || BUILD_ETABS2014 || BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017        
        /// <summary>
        /// Creates a new grid-only model from template. 
        /// </summary>
        /// <param name="numberOfStorys">The number of storys in the model.</param>
        /// <param name="typicalStoryHeight">The story height that will be used for all stories in the model, except the bottom story. [L].</param>
        /// <param name="bottomStoryHeight">The story height will be used for the bottom story. [L].</param>
        /// <param name="numberLinesX">The number of grid lines in the X direction.</param>
        /// <param name="numberLinesY">The number of grid lines in the Y direction.</param>
        /// <param name="spacingX">The uniform spacing for grid lines in the X direction. [L].</param>
        /// <param name="spacingY">The uniform spacing for grid lines in the Y direction. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void NewGridOnly(int numberOfStorys,
                    double typicalStoryHeight,
                    double bottomStoryHeight,
                    int numberLinesX,
                    int numberLinesY,
                    double spacingX,
                    double spacingY)
        {
            _callCode = _sapModel.File.NewGridOnly(numberOfStorys, typicalStoryHeight, bottomStoryHeight,
                    numberLinesX, numberLinesY, spacingX, spacingY);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            resetPaths();
        }

        /// <summary>
        /// Creates a new steel deck model from template. 
        /// </summary>
        /// <param name="numberOfStorys">The number of storys in the model.</param>
        /// <param name="typicalStoryHeight">The story height that will be used for all stories in the model, except the bottom story. [L].</param>
        /// <param name="bottomStoryHeight">The story height will be used for the bottom story. [L].</param>
        /// <param name="numberLinesX">The number of grid lines in the X direction.</param>
        /// <param name="numberLinesY">The number of grid lines in the Y direction.</param>
        /// <param name="spacingX">The uniform spacing for grid lines in the X direction. [L].</param>
        /// <param name="spacingY">The uniform spacing for grid lines in the Y direction. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void NewSteelDeck(int numberOfStorys,
                    double typicalStoryHeight,
                    double bottomStoryHeight,
                    int numberLinesX,
                    int numberLinesY,
                    double spacingX,
                    double spacingY)
        {
            _callCode = _sapModel.File.NewSteelDeck(numberOfStorys, typicalStoryHeight, bottomStoryHeight,
                    numberLinesX, numberLinesY, spacingX, spacingY);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            resetPaths();
        }
#endif
        #endregion

        #region Methods: Private
        private void setPaths(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            _fileName = IO.Path.GetFileName(filePath);
            _filePath = string.IsNullOrEmpty(_fileName) ? filePath : IO.Path.GetDirectoryName(filePath);
        }

        private void resetPaths()
        {
            _filePath = string.Empty;
            _fileName = string.Empty;
        }
        #endregion
    }
}