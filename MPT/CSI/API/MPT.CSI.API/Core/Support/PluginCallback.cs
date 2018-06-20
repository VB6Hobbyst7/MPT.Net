// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="PluginCallback.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
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
namespace MPT.CSI.API.Core.Support
{
    /// <summary>
    /// The callback function used by the plugin to interact with the application.
    /// </summary>
    public class PluginCallback
    {
        #region Fields
        /// <summary>
        /// The plugin callback
        /// </summary>
        private readonly CSiProgram.cPluginCallback _pluginCallback;
        #endregion

        #region Properties        
        /// <summary>
        /// Gets the error flag.
        /// </summary>
        /// <value>The error flag.</value>
        public int ErrorFlag => _pluginCallback.ErrorFlag;

        /// <summary>
        /// Gets a value indicating whether this <see cref="PluginCallback" /> is finished.
        /// </summary>
        /// <value><c>true</c> if finished; otherwise, <c>false</c>.</value>
        public bool Finished => _pluginCallback.Finished;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginCallback" /> class.
        /// </summary>
        /// <param name="sapPlugin">The callback function used by the plugin to interact with the application.</param>
        public PluginCallback(CSiProgram.cPluginCallback sapPlugin)
        {
            _pluginCallback = sapPlugin;
        }
        #endregion

        #region Methods: Public        
        /// <summary>
        /// To be called immediately before the plugin closes (e.g., if the plugin has a single main window, at the end of the close event of that form). <para />
        /// It expects an error flag (0 meaning no errors) to let the application know if the operation was successful or not. <para />
        /// The application will wait indefinitely for <see cref="PluginCallback.Finish(int)" /> to be called, so the plugin programmer must make sure that it is called when the plugin completes.
        /// </summary>
        /// <param name="errorFlag">An error flag (0 meaning no errors) to let the application know if the operation was successful or not.</param>
        public void Finish(int errorFlag)
        {
            _pluginCallback.Finish(errorFlag);
        }
        #endregion
    }
}