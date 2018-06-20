// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="CSiApplication.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.Threading;
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
    /// Manipulates the CSi application, such as opening, closing, setting visiblity and active object, etc.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class CSiApplication : CSiApiBase, IDisposable
    {
        #region Fields

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        private const string TYPE_NAME = "CSI.ETABS.API.ETABSObject";
#else
        /// <summary>
        /// The type name
        /// </summary>
        private const string TYPE_NAME = "CSI.SAP2000.API.SapObject";
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        private const string PROCESS_NAME = "ETABS";
#elif BUILD_CSiBridgev16 || BUILD_CSiBridgev17 || BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
        private const string PROCESS_NAME = "CSibridge";
#else
        private const string PROCESS_NAME = "SAP2000";
#endif        
        /// <summary>
        /// The number of exit attempts before the library stops attempting to exit the application.
        /// </summary>
        private readonly int _numberOfExitAttempts;

        /// <summary>
        /// The interval between exit attempts of the application.
        /// </summary>
        private readonly int _intervalBetweenExitAttempts;

        /// <summary>
        /// The seed
        /// </summary>
        private CSiApiSeed _seed;

        /// <summary>
        /// The model
        /// </summary>
        private CSiModel _model;

        #endregion

        #region Properties               

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>The file.</value>
        public CSiModel Model => _model ?? ( !IsInitialized? _model = null : _model = new CSiModel(_seed));

        /// <summary>
        /// Path to the CSi application that the class manipulates.
        /// This might not be specifed if the object attaches to a process or initializes the default isntalled program.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the application is initialized.
        /// </summary>
        /// <value><c>true</c> if the application is initialized; otherwise, <c>false</c>.</value>
        public bool IsInitialized => (_sapObject != null && _sapModel != null);

        #endregion

        #region Initialization     
        // TODO: Current Initializers throw an exception after partial initialization. Consider how to better handle. Or at least work better w/ model file exception such that application closes.
        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApplication"/> class at the specified path.
        /// When the model is not visible it does not appear on screen and it does not appear in the Windows task bar.
        /// If no filename is specified, you can later open a model or create a model through the API.
        /// The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension.
        /// Files with .sdb extensions are opened as standard SAP2000 files.
        /// Files with .$2k and .s2k extensions are imported as text files.
        /// Files with .xls extensions are imported as Microsoft Excel files.
        /// Files with .mdb extensions are imported as Microsoft Access files.
        /// </summary>
        /// <param name="applicationPath">The application path.</param>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.  
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <param name="numberOfExitAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenExitAttempts">The interval between exit attempts of the application.</param>
        public CSiApplication(string applicationPath,
            eUnits units = eUnits.kip_in_F,
            bool visible = true,
            string modelPath = "",
            int numberOfExitAttempts = 1,
            int intervalBetweenExitAttempts = 0)
        {
            _numberOfExitAttempts = numberOfExitAttempts;
            _intervalBetweenExitAttempts = intervalBetweenExitAttempts;

            initializeProgram(applicationPath, units, visible, modelPath);
        }

#if !BUILD_SAP2000v18 && !BUILD_SAP2000v17 && !BUILD_SAP2000v16 && !BUILD_CSiBridgev18 && !BUILD_CSiBridgev17 && !BUILD_CSiBridgev16 && !BUILD_ETABS2015

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApplication"/> class using the default installed application.
        /// When the model is not visible it does not appear on screen and it does not appear in the Windows task bar.
        /// If no filename is specified, you can later open a model or create a model through the API.
        /// The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension.
        /// Files with .sdb extensions are opened as standard SAP2000 files.
        /// Files with .$2k and .s2k extensions are imported as text files.
        /// Files with .xls extensions are imported as Microsoft Excel files.
        /// Files with .mdb extensions are imported as Microsoft Access files.
        /// </summary>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.  
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <param name="numberOfExitAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenExitAttempts">The interval between exit attempts of the application.</param>
        public CSiApplication(eUnits units = eUnits.kip_in_F,
            bool visible = true,
            string modelPath = "",
            int numberOfExitAttempts = 1,
            int intervalBetweenExitAttempts = 0)
        {
            _numberOfExitAttempts = numberOfExitAttempts;
            _intervalBetweenExitAttempts = intervalBetweenExitAttempts;

            initializeProgram("", units, visible, modelPath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApplication"/> class by attaching to an existing process.
        /// </summary>
        /// <param name="numberOfAttempts">The number of attempts before the library stops attempting to attach to the application.</param>
        /// <param name="intervalBetweenAttempts">The interval between attempts of attaching to the application.</param>
        /// <param name="numberOfExitAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenExitAttempts">The interval between exit attempts of the application.</param>
        public CSiApplication(int numberOfAttempts, 
            int intervalBetweenAttempts,
            int numberOfExitAttempts = 1,
            int intervalBetweenExitAttempts = 0)
        {
            _numberOfExitAttempts = numberOfExitAttempts;
            _intervalBetweenExitAttempts = intervalBetweenExitAttempts;

            bool isAttachedToProcess = false;
            int currentAttemptNumber = 0;
            while (!isAttachedToProcess && 
                    (currentAttemptNumber < numberOfAttempts))
            {
                isAttachedToProcess = AttachToProcess();
                if (!isAttachedToProcess)
                {
                    currentAttemptNumber++;
                    Thread.Sleep(intervalBetweenAttempts);
                }
            }
        }
#endif
#endregion

        #region Methods: Public
        /// <summary>
        /// Opens a fresh instance of the CSi program.
        /// When the model is not visible it does not appear on screen and it does not appear in the Windows task bar.
        /// If no filename is specified, you can later open a model or create a model through the API.
        /// The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension.
        /// Files with .sdb extensions are opened as standard SAP2000 files.
        /// Files with .$2k and .s2k extensions are imported as text files.
        /// Files with .xls extensions are imported as Microsoft Excel files.
        /// Files with .mdb extensions are imported as Microsoft Access files.
        /// </summary>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.  
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void InitializeNewProgram(eUnits units = eUnits.kip_in_F,
                                    bool visible = true,
                                    string modelPath = "")
        {
            applicationStart(units, visible,  modelPath);
        }

        /// <summary>
        /// Attaches to an existing process.
        /// The program can be ended manually or by calling ApplicationExit
        /// Please note that if multiple instances of ETABS are manually started, an API client can only attach to the instance that was started first.
        /// </summary>
        /// <returns><c>true</c> if the process is successfully attached to, <c>false</c> otherwise.</returns>
        public bool AttachToProcess()
        {
            try
            {
                if (!processIsRunning()) { throw new CSiException("No running instance of the program " + PROCESS_NAME + " can be found.");}

                Helper helper = Helper.Initialize();
                _sapObject = helper.GetObject(TYPE_NAME);
                if (_sapObject == null)
                {
                    resetObject();
                }
                else
                {
                    //Get a reference to cSapModel to access all API classes and functions
                    _sapModel = _sapObject.SapModel;
                    _seed = new CSiApiSeed(_sapObject, _sapModel);
                }
                return IsInitialized;
            }
            catch (Exception ex)
            {
                resetObject();
                throw new CSiException("No running instance of the program found or failed to attach.", ex);
                // TODO: Replace the exception with a logger.
                //return false;
            };
        }

        /// <summary>
        /// This function closes the application.
        /// If the model file is saved then it is saved with its current name.
        /// </summary>
        /// <param name="fileSave">True: The existing model file is saved prior to closing program.</param>
        /// <param name="numberOfAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenAttempts">The interval between exit attempts of the application.</param>
        /// <exception cref="CSiException">currentAttemptNumber + " of " + numberOfAttempts + " attempts to close the application failed."</exception>
        /// <exception cref="CSiException">"The application was unable to be closed."</exception>
        public void ApplicationExit(bool fileSave, 
            int numberOfAttempts = -1,
            int intervalBetweenAttempts = -1)
        {
            if (!IsInitialized) { return; }
            if (numberOfAttempts == -1) numberOfAttempts = _numberOfExitAttempts;
            if (intervalBetweenAttempts == -1) intervalBetweenAttempts = _intervalBetweenExitAttempts;

            bool isClosed = false;
            int currentAttemptNumber = 0;
            while (!isClosed &&
                    (currentAttemptNumber < numberOfAttempts))
            {
                _callCode = _sapObject.ApplicationExit(fileSave);
                isClosed = (_callCode == 0);
                if (!isClosed)
                {
                    currentAttemptNumber++;
                    Thread.Sleep(intervalBetweenAttempts);
                }
            }
            resetObject();
            if (!throwCurrentApiException(_callCode)) return;
            if (currentAttemptNumber == numberOfAttempts)
                throw new CSiException(currentAttemptNumber + " of " + numberOfAttempts + " attempts to close the application failed.");
            throw new CSiException("The application was unable to be closed.");
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the OAPI version number.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double GetOAPIVersionNumber()
        {
            return _sapObject.GetOAPIVersionNumber();
        }
#endif

        /// <summary>
        /// This function hides the application.
        /// When the application is hidden it is not visible on the screen or on the Windows task bar.
        /// If the application is already hidden, no action is taken.
        /// </summary>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Hide()
        {
            _callCode = _sapObject.Hide();
            // Note: This will normally throw an exception if the application is already hidden.
            // In the wrapper it is chosen to take no action as the user can harmlessly query the visibility.
            // if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function unhides the application.
        /// When the application is hidden it is not visible on the screen or on the Windows task bar.
        /// If the application is already visible, no action is taken.
        /// </summary>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Unhide()
        {
            _callCode = _sapObject.Unhide();
            // Note: This will normally throw an exception if the application is already visible.
            // In the wrapper it is chosen to take no action as the user can harmlessly query the visibility.
            // if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// True: The application is visible on the screen.
        /// </summary>
        public bool Visible()
        {
            return _sapObject.Visible();
        }

        /// <summary>
        /// This function sets the active instance of a _SapObject in the system Running Object Table (ROT), replacing the previous instance(s).
        /// When a new _SapObject is created using the OAPI, it is automatically added to the system ROT if none is already present.
        /// Subsequent instances of the _SapObject do not alter the ROT as long as at least one active instance of a _SapObject is present in the ROT.
        /// The Windows API call GetObject() can be used to attach to the active _SapObject instance registered in the ROT.
        /// When the application is started normally (i.e. not from the OAPI), it does not add an instance of the _SapObject to the ROT, hence the GetObject() call cannot be used to attach to the active _SapObject instance.
        /// The Windows API call CreateObject() or New Sap2000v16._SapObject command can be used to attach to an instance of SAP2000 that is started normally (i.e. not from the OAPI).
        /// If there are multiple such instances, the first instance that will be attached to is the one that is started first.
        /// </summary>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetAsActiveObject()
        {
            _callCode = _sapObject.SetAsActiveObject();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function removes the current instance of a _sapObject from the system Running Object Table (ROT).
        /// </summary>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void UnsetAsActiveObject()
        {
            _callCode = _sapObject.UnsetAsActiveObject();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion

        
        #region IDisposable        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ApplicationExit(fileSave: false);
        }
        #endregion

        #region Methods: Private                
        /// <summary>
        /// Resets all API objects to null.
        /// </summary>
        private void resetObject()
        {
            _sapModel = null;
            _sapObject = null;
        }


        /// <summary>
        /// This function starts the program.
        /// When the model is not visible it does not appear on screen and it does not appear in the Windows task bar.
        /// If no filename is specified, you can later open a model or create a model through the API.
        /// The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension.
        /// Files with .sdb extensions are opened as standard SAP2000 files.
        /// Files with .$2k and .s2k extensions are imported as text files.
        /// Files with .xls extensions are imported as Microsoft Excel files.
        /// Files with .mdb extensions are imported as Microsoft Access files.
        /// </summary>
        /// <param name="applicationPath">The application path. If not specified, then the default installed application will be used.</param>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.  
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <returns><c>true</c> if the program is successfully initialied, <c>false</c> otherwise.</returns>
        /// <exception cref="IO.IOException">The following CSi program path is invalid: " + Path</exception>
        private bool initializeProgram(string applicationPath = "",
                                    eUnits units = eUnits.kip_in_F,
                                    bool visible = true,
                                    string modelPath = "")
        {
            if (string.IsNullOrWhiteSpace(applicationPath))
            {
#if BUILD_ETABS2013
                // TODO: Consider ETABS2013 deprecation
#elif BUILD_SAP2000v16
                // TODO: Consider SAP2000v16 deprecation
                // No action needed, allow method to continute to next option.
#else
#endif
#if !BUILD_SAP2000v18 && !BUILD_SAP2000v17 && !BUILD_SAP2000v16 && !BUILD_CSiBridgev18 && !BUILD_CSiBridgev17 && !BUILD_CSiBridgev16 && !BUILD_ETABS2015
                return initializeProgramFromLatestInstallation(units, visible, modelPath);
#else 
                return false;
#endif
            }

            if (!IO.File.Exists(applicationPath))
            {
                throw new IO.IOException("The following CSi program path is invalid: " + applicationPath);
            }
            return initializeProgramSpecific(applicationPath, units, visible, modelPath);
        }

        /// <summary>
        /// Performs the application-specific steps of initializing the program at the provided file path.
        /// This initializes SapObject and SapModel.
        /// </summary>
        /// <param name="applicationPath">Path to the CSi application that the class manipulates.
        /// Make sure this is a valid path before using this method.</param>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.  
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <returns><c>true</c> if program is successfully initialized, <c>false</c> otherwise.</returns>
        private bool initializeProgramSpecific(string applicationPath, 
                                    eUnits units = eUnits.kip_in_F,
                                    bool visible = true,
                                    string modelPath = "")
        {
            try
            {
#if BUILD_ETABS2013
                //  Create program object
                Assembly myAssembly = Assembly.LoadFrom(applicationPath);

                //  Create an instance of ETABSObject and get a reference to cOAPI interface 
                _sapObject = DirectCast(myAssembly.CreateInstance(TYPE_NAME), cOAPI);
#elif BUILD_SAP2000v16
                // NOTE: No path is needed for SAP2000v16. Instead, the tested program will automatically use the
                //    version currently installed. To change the version, say for testing, run the desired v16 version as 
                //    administrator first in order to register.
                _sapObject = new SAP2000v16.SapObject;
#else
                // Old Method: 32bit OAPI clients can only call 32bit ETABS 2014 and 64bit OAPI clients can only call 64bit ETABS 2014
                // Create program object
                // _SapObject = DirectCast(myAssembly.CreateInstance("CSI.SAP2000.API.SapObject"), cOAPI)

                // New Method: 32bit & 64bit API clients can call 32 & 64bit program versions
                // Use the OAPI helper class to get a reference to cOAPI interface
                Helper helper = Helper.Initialize();
                _sapObject = helper.CreateObject(applicationPath);
#endif
                // start Sap2000 application
                applicationStart(units, visible, modelPath);

                // create SapModel object
                _sapModel = _sapObject.SapModel;
                _seed = new CSiApiSeed(_sapObject, _sapModel);

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
                delayedETABSInitialization(units, visible, modelPath);
#endif

                Path = applicationPath;
                return IsInitialized;
            }
            catch (Exception ex)
            {
                resetObject();
                throw new CSiException("Cannot start a new instance of the program from " + applicationPath, ex);
                // TODO: Replace the exception with a logger.
                //return false;
            }
        }


#if !BUILD_SAP2000v18 && !BUILD_SAP2000v17 && !BUILD_SAP2000v16 && !BUILD_CSiBridgev18 && !BUILD_CSiBridgev17 && !BUILD_CSiBridgev16 && !BUILD_ETABS2015
        /// <summary>
        /// Performs the application-specific steps of initializing the default installed program.
        /// This initializes SapObject and SapModel.
        /// </summary>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.  
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <returns><c>true</c> if program is successfully initialized, <c>false</c> otherwise.</returns>
        private bool initializeProgramFromLatestInstallation(eUnits units = eUnits.kip_in_F,
                                                            bool visible = true,
                                                            string modelPath = "")
        {
            try
            {
                Helper helper = Helper.Initialize();
                _sapObject = helper.CreateObjectProgId(TYPE_NAME);

                // start Sap2000 application
                applicationStart(units, visible, modelPath);

                // create SapModel object
                _sapModel = _sapObject.SapModel;
                _seed = new CSiApiSeed(_sapObject, _sapModel);

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
                delayedETABSInitialization(units, visible, modelPath);
#endif

                // TODO: Assign path of installation to Path property.

                return IsInitialized;
            }
            catch (Exception ex)
            {
                resetObject();
                throw new CSiException("Cannot start a new instance of the program.", ex);
                // TODO: Replace the exception with a logger.
                //return false;
            }
        }
#endif

        /// <summary>
        /// This function starts the application.
        /// When the model is not visible it does not appear on screen and it does not appear in the Windows task bar.
        /// If no filename is specified, you can later open a model or create a model through the API.
        /// The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension.
        /// Files with .sdb extensions are opened as standard SAP2000 files.
        /// Files with .$2k and .s2k extensions are imported as text files.
        /// Files with .xls extensions are imported as Microsoft Excel files.
        /// Files with .mdb extensions are imported as Microsoft Access files.
        /// </summary>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.  
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        private void applicationStart(eUnits units = eUnits.kip_in_F,
                                    bool visible = true,
                                    string modelPath = "")
        {
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            _callCode = _sapObject.ApplicationStart();
            if (_seed == null) return;

            Model.InitializeNewModel(units);
            if (!string.IsNullOrWhiteSpace(modelPath)) Model.File.Open(modelPath);
            if (!visible) Hide();
#else
            _callCode = _sapObject.ApplicationStart(EnumLibrary.Convert<eUnits, CSiProgram.eUnits>(units), visible, modelPath);
#endif
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        
        /// <summary>
        /// Performs the rest of the applicationStart method for ETABS once the seed object has been created.
        /// </summary>
        /// <param name="units"></param>
        /// <param name="visible"></param>
        /// <param name="modelPath"></param>
        private void delayedETABSInitialization(eUnits units = eUnits.kip_in_F,
                                                bool visible = true,
                                                string modelPath = "")
        {
            Model.InitializeNewModel(units);
            if (!string.IsNullOrWhiteSpace(modelPath)) Model.File.Open(modelPath);
            if (!visible) Hide();
        }


        private static bool processIsRunning()
        {
            Process[] pname = Process.GetProcessesByName(PROCESS_NAME);
            return (pname.Length != 0);
        }
        
#endregion
    }
}