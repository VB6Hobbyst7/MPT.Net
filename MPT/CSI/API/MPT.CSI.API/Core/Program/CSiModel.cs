// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="CSiModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
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
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using MPT.CSI.API.Core.Program.ModelBehavior.BridgeAdvanced;
#endif
using MPT.CSI.API.Core.Support;
using MPT.CSI.API.Core.Program.ModelBehavior;

namespace MPT.CSI.API.Core.Program
{
    /// <summary>
    /// Main CSi program object.
    /// Most API functions are called on this or contained delegate objects.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiSeed" />
    public class CSiModel : CSiApiBase
    {
        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private CSiApiSeed _seed;

        /// <summary>
        /// The file
        /// </summary>
        private CSiFile _file;
        /// <summary>
        /// The analysis modeler
        /// </summary>
        private AnalysisModeler _analysisModeler;
        /// <summary>
        /// The object modeler
        /// </summary>
        private ObjectModeler _objectModeler;
        /// <summary>
        /// The analyzer
        /// </summary>
        private Analyzer _analyzer;
        /// <summary>
        /// The definitions
        /// </summary>
        private Definitions _definitions;
        /// <summary>
        /// The designer
        /// </summary>
        private Designer _designer;
        /// <summary>
        /// The editor
        /// </summary>
        private Editor _editor;
        /// <summary>
        /// The options
        /// </summary>
        private Options _options;
        /// <summary>
        /// The analysis results
        /// </summary>
        private AnalysisResults _analysisResults;
        /// <summary>
        /// The selector
        /// </summary>
        private Selector _selector;
        /// <summary>
        /// The viewer
        /// </summary>
        private Viewer _viewer;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The extended entity data
        /// </summary>
        private ExtendedEntityData _extendedEntityData;
#endif
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
        private Superstructure _superstructure;
#endif

        #endregion

        #region Properties               

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>The file.</value>
        public CSiFile File => _file ?? (_file = new CSiFile(_seed));

        /// <summary>
        /// Gets the analysis model.
        /// </summary>
        /// <value>The analysis model.</value>
        public AnalysisModeler AnalysisModel => _analysisModeler ?? (_analysisModeler = new AnalysisModeler(_seed));

        /// <summary>
        /// Gets the analyze.
        /// </summary>
        /// <value>The analyze.</value>
        public Analyzer Analyze => _analyzer ?? (_analyzer = new Analyzer(_seed));

        /// <summary>
        /// Gets the definitions.
        /// </summary>
        /// <value>The definitions.</value>
        public Definitions Definitions => _definitions ?? (_definitions = new Definitions(_seed));

        /// <summary>
        /// Gets the design.
        /// </summary>
        /// <value>The design.</value>
        public Designer Design => _designer ?? (_designer = new Designer(_seed));

        /// <summary>
        /// Gets the editor.
        /// </summary>
        /// <value>The editor.</value>
        public Editor Editor => _editor ?? (_editor = new Editor(_seed));

        /// <summary>
        /// Gets the object model.
        /// </summary>
        /// <value>The object model.</value>
        public ObjectModeler ObjectModel => _objectModeler ?? (_objectModeler = new ObjectModeler(_seed));

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        public Options Options => _options ?? (_options = new Options(_seed));

        /// <summary>
        /// Gets the analysis results.
        /// </summary>
        /// <value>The analysis results.</value>
        public AnalysisResults Results => _analysisResults ?? (_analysisResults = new AnalysisResults(_seed));

        /// <summary>
        /// Gets the selector.
        /// </summary>
        /// <value>The selector.</value>
        public Selector Selector => _selector ?? (_selector = new Selector(_seed));

        /// <summary>
        /// Gets the viewer.
        /// </summary>
        /// <value>The viewer.</value>
        public Viewer Viewer => _viewer ?? (_viewer = new Viewer(_seed));

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the extended entity data.
        /// </summary>
        /// <value>The extended entity data.</value>
        public ExtendedEntityData ExtendedEntityData => _extendedEntityData ?? (_extendedEntityData = new ExtendedEntityData(_seed));
#endif
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
        /// <summary>
        /// Gets the bridge superstructure.
        /// </summary>
        /// <value>The bridge superstructure.</value>
                public Superstructure Superstructure => _superstructure ?? (_superstructure = new Superstructure(_seed));
#endif

        /// <summary>
        /// True: Model is unlocked.
        /// With some exceptions, definitions and assignments can not be changed in a model while the model is locked.
        /// If an attempt is made to change a definition or assignment while the model is locked and that change is not allowed in a locked model, an error will be returned.
        /// </summary>
        /// <value><c>true</c> if this instance is model locked; otherwise, <c>false</c>.</value>
        public bool IsModelLocked => GetModelIsLocked();
        #endregion


        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiModel" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public CSiModel(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }
        #endregion

        #region Methods: Public

        /// <summary>
        /// This function clears the previous model and initializes the program for a new model.
        /// If it is later needed, you should save your previous model prior to calling this function.
        /// After calling the InitializeNewModel function, it is not necessary to also call the ApplicationStart function because the functionality of the ApplicationStart function is included in the InitializeNewModel function.
        /// </summary>
        /// <param name="units">The database units for the new model.
        /// All data is internally stored in the model in these units.</param>
        /// <returns><c>true</c> if a nuew model is successfully initialized, <c>false</c> otherwise.</returns>
        public bool InitializeNewModel(eUnits units = eUnits.kip_in_F)
        {
            _callCode = _sapModel.InitializeNewModel(EnumLibrary.Convert<eUnits, CSiProgram.eUnits>(units));
            if (!apiCallIsSuccessful(_callCode)) return false;
            _file = new CSiFile(_seed);
            return true;
        }

        // ==== Model States ===

        /// <summary>
        /// True: Model is unlocked.
        /// With some exceptions, definitions and assignments cannot be changed in a model while the model is locked.
        /// If an attempt is made to change a definition or assignment while the model is locked and that change is not allowed in a locked model, an error will be returned.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool GetModelIsLocked()
        {
            return _sapModel.GetModelIsLocked();
        }

        /// <summary>
        /// Sets whether or not the model is locked.
        /// With some exceptions, definitions and assignments can not be changed in a model while the model is locked.
        /// If an attempt is made to change a definition or assignment while the model is locked and that change is not allowed in a locked model, an error will be returned.
        /// </summary>
        /// <param name="lockModel">True: Model will be locked.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetModelIsLocked(bool lockModel)
        {
            _callCode = _sapModel.SetModelIsLocked(lockModel);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // ==== User Comment ===
        /// <summary>
        /// Retrieves the data in the user comments and log.
        /// </summary>
        /// <param name="userComment">The user comment.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetUserComment(ref string userComment)
        {
            _callCode = _sapModel.GetUserComment(ref userComment);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the user comments and log data.
        /// </summary>
        /// <param name="commentUser">The data to be added to the user comments and log.</param>
        /// <param name="numberLinesBlank">The number of carriage return and line feeds to be included before the specified comment.
        /// This item is ignored if there are no existing comments.</param>
        /// <param name="replace">True: All existing comments are replaced with the specified comment.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetUserComment(string commentUser, 
            int numberLinesBlank = 1, 
            bool replace = false)
        {
            _callCode = _sapModel.SetUserComment(commentUser, numberLinesBlank, replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        // ==== Units ===
        /// <summary>
        /// Returns the database units for the model.
        /// All data is internally stored in the model in these units and converted to the present units as needed.
        /// </summary>
        /// <returns>eUnits.</returns>
        public eUnits GetDatabaseUnits()
        {
            return EnumLibrary.Convert<CSiProgram.eUnits, eUnits>(_sapModel.GetDatabaseUnits());
        }

        /// <summary>
        /// Returns the units presently specified for the model.
        /// </summary>
        /// <returns>eUnits.</returns>
        public eUnits GetPresentUnits()
        {
            return EnumLibrary.Convert<CSiProgram.eUnits, eUnits>(_sapModel.GetPresentUnits());
        }

        /// <summary>
        /// Sets the units presently specified for the model.
        /// </summary>
        /// <param name="units">The units.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetPresentUnits(eUnits units)
        {
            _callCode = _sapModel.SetPresentUnits(EnumLibrary.Convert<eUnits, CSiProgram.eUnits>(units));
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }


#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Returns the database units for the model. 
        /// All data is internally stored in the model in these units and converted to the present units as needed.
        /// </summary>
        /// <returns></returns>
        public void GetDatabaseUnits(ref eForce forceUnits,
            ref eLength lengthUnits,
            ref eTemperature temperatureUnits)
        {
            CSiProgram.eForce csiForceUnits = CSiProgram.eForce.NotApplicable;
            CSiProgram.eLength csiLengthUnits = CSiProgram.eLength.NotApplicable;
            CSiProgram.eTemperature csiTemperatureUnits = CSiProgram.eTemperature.NotApplicable;

            _callCode = _sapModel.GetDatabaseUnits_2(ref csiForceUnits, ref csiLengthUnits, ref csiTemperatureUnits);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            forceUnits = EnumLibrary.Convert(csiForceUnits, forceUnits);
            lengthUnits = EnumLibrary.Convert(csiLengthUnits, lengthUnits);
            temperatureUnits = EnumLibrary.Convert(csiTemperatureUnits, temperatureUnits);
        }

        /// <summary>
        /// Returns the units presently specified for the model.
        /// </summary>
        /// <returns></returns>
        public void GetPresentUnits(ref eForce forceUnits,
            ref eLength lengthUnits,
            ref eTemperature temperatureUnits)
        {
            CSiProgram.eForce csiForceUnits = CSiProgram.eForce.NotApplicable;
            CSiProgram.eLength csiLengthUnits = CSiProgram.eLength.NotApplicable;
            CSiProgram.eTemperature csiTemperatureUnits = CSiProgram.eTemperature.NotApplicable;

            _callCode = _sapModel.GetPresentUnits_2(ref csiForceUnits, ref csiLengthUnits, ref csiTemperatureUnits);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            forceUnits = EnumLibrary.Convert(csiForceUnits, forceUnits);
            lengthUnits = EnumLibrary.Convert(csiLengthUnits, lengthUnits);
            temperatureUnits = EnumLibrary.Convert(csiTemperatureUnits, temperatureUnits);
        }

        /// <summary>
        /// Sets the units presently specified for the model.
        /// </summary>
        /// <returns></returns>
        public void SetPresentUnits(eForce forceUnits,
            eLength lengthUnits,
            eTemperature temperatureUnits)
        {
            CSiProgram.eForce csiForceUnits = CSiProgram.eForce.NotApplicable;
            CSiProgram.eLength csiLengthUnits = CSiProgram.eLength.NotApplicable;
            CSiProgram.eTemperature csiTemperatureUnits = CSiProgram.eTemperature.NotApplicable;

            _callCode = _sapModel.SetPresentUnits_2(csiForceUnits, csiLengthUnits, csiTemperatureUnits);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }
#endif

        // ==== Get/Set Methods ===

        /// <summary>
        /// Retrieves the value of the program auto merge tolerance.
        /// </summary>
        /// <param name="mergeTolerance">The program auto merge tolerance. [L</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetMergeTolerance(ref double mergeTolerance)
        {
            _callCode = _sapModel.GetMergeTol(ref mergeTolerance);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        /// <summary>
        /// Sets the value of the program auto merge toleranc
        /// </summary>
        /// <param name="mergeTolerance">The program auto merge tolerance. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetMergeTolerance(double mergeTolerance)
        {
            _callCode = _sapModel.SetMergeTol(mergeTolerance);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        /// <summary>
        /// Returns the name of the present coordinate system.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetPresentCoordSystem()
        {
            return _sapModel.GetPresentCoordSystem();
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the present coordinate system.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of a defined coordinate system.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetPresentCoordSystem(string nameCoordinateSystem)
        {
            _callCode = _sapModel.SetPresentCoordSystem(nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        /// <summary>
        /// Retrieves the project information data.
        /// </summary>
        /// <param name="projectInfoItems">This is an array that includes the name of the project information item.</param>
        /// <param name="projectInfoData">This is an array that includes the data for the specified project information item.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetProjectInfo(ref string[] projectInfoItems,
            ref string[] projectInfoData)
        {
            _callCode = _sapModel.GetProjectInfo(ref _numberOfItems, ref projectInfoItems, ref projectInfoData);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        /// <summary>
        /// Sets the data for an item in the project information.
        /// </summary>
        /// <param name="projectInfoItem">Name of the project information item</param>
        /// <param name="projectInfoData">Data for the specified project information item.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetProjectInfo(string projectInfoItem,
            string projectInfoData)
        {
            _callCode = _sapModel.SetProjectInfo(projectInfoItem, projectInfoData);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }






        /// <summary>
        /// This function returns the program version.
        /// </summary>
        /// <param name="versionName">The program version name that is externally displayed to the user.</param>
        /// <param name="versionNumber">The program version number that is used internally by the program and not displayed to the user.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetVersion(ref string versionName,
            ref double versionNumber)
        {
            _callCode = _sapModel.GetVersion(ref versionName, ref versionNumber);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        #endregion
        
    }
}
