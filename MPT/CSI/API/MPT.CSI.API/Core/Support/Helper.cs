// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="Helper.cs" company="">
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

namespace MPT.CSI.API.Core.Support
{
    /// <summary>
    /// The new OAPI helper class to get a reference to cOAPI interface.
    /// This is done by either starting a new process or attaching to an existing process.
    /// </summary>
    public class Helper
    {
        #region Fields

        /// <summary>
        /// The helper
        /// </summary>
        private readonly CSiProgram.cHelper _helper;

        #endregion

        #region Initialization

        ///// <summary>
        ///// Initializes a new instance of the <see cref="Helper" /> class.
        ///// </summary>
        //public Helper()
        //{
        //    _helper = new CSiProgram.Helper();
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="Helper" /> class.
        /// </summary>
        /// <param name="helper">The CSi helper object.</param>
        public Helper(CSiProgram.cHelper helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Helper.</returns>
        /// <exception cref="CSiException">Cannot create an instance of the Helper object.</exception>
        public static Helper Initialize()
        {
            Helper newHelper;
            try
            {
                CSiProgram.cHelper helper = new CSiProgram.Helper();
                newHelper = new Helper(helper);
            }
            catch (Exception ex)
            {
                throw new CSiException("Cannot create an instance of the Helper object.", ex);
                // TODO: Replace the exception with a logger.
            }
            return newHelper;
        }
        #endregion

        #region Methods: Public        
        /// <summary>
        /// Creates the base application object to call API methods on based on the full application path.
        /// </summary>
        /// <param name="fullPath">Path to the CSi application that the class manipulates.
        /// Make sure this is a valid path before using this method.</param>
        /// <returns>CSiProgram.cOAPI.</returns>
        public CSiProgram.cOAPI CreateObject(string fullPath)
        {
            return _helper.CreateObject(fullPath);
        }

#if !BUILD_SAP2000v18  && !BUILD_SAP2000v17  && !BUILD_SAP2000v16 && !BUILD_CSiBridgev18  && !BUILD_CSiBridgev17  && !BUILD_CSiBridgev16 && !BUILD_ETABS2015 
        /// <summary>
        /// Creates the base application object to call API methods on based on the program ID, and automatically launches the most recently installed version of the application.
        /// </summary>
        /// <param name="progId">Program ID, such as "CSI.ETABS.API.ETABSObject".</param>
        /// <returns>CSiProgram.cOAPI.</returns>
        public CSiProgram.cOAPI CreateObjectProgId(string progId)
        {
            return _helper.CreateObjectProgID(progId);
        }
#endif

        /// <summary>
        /// Attaches to a current process, getting an application object that is currently running.
        /// </summary>
        /// <param name="typeName">Name of the type, such as "CSI.ETABS.API.ETABSObject".</param>
        /// <returns>CSiProgram.cOAPI.</returns>
        public CSiProgram.cOAPI GetObject(string typeName)
        {
            return _helper.GetObject(typeName);
        }
        #endregion
    }
}