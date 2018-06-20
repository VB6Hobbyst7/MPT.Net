// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="CSiPlugin.cs" company="">
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
namespace MPT.CSI.API.Core.Support
{
    /// <summary>
    /// In your plugin, all functionality must be implemented in a class called cPlugin. <para />
    /// This is a convenient base class to inherit from for the creation of such a class.
    /// The plugin must reference the ETABS2016 API library(either ETABS2016.DLL or ETABS2016.TLB ), which must be registered on the developer’s and the user’s systems.
    /// This registration takes place during the installation of ETABS.
    /// The ETABS2016.DLL is compiled as AnyCPU, hence it can be referenced by both 32bit and 64bit plugin projects.
    /// Please note that COM plugins must be registered for COM on the user’s system, before they can be used in ETABS.
    /// This will require that the ETABS2016.DLL be present in the directory where the COM plugin is being registered.
    /// It is the plugin developer's responsibility to instruct users how to install the plugin.
    /// Plugins should be installed only after ETABS has been installed.
    /// COM Plugin developers should consider that registering assemblies usually requires administrative permissions, which many users may not have.
    /// Plugins compiled as 32bit will only work with 32bit ETABS, and likewise for 64bit.
    /// For these reasons, we recommend that developers create.NET plugins that are compiled as "AnyCPU".
    /// This will allow your plugin to be called from both 32bit and 64bit ETABS, and will not require registration.
    /// Please make sure your plugin is stable, handles errors well, and does not cause any unintended changes to the user’s model.
    /// We will attempt to maintain a stable interface in ETABS, however, that cannot be guaranteed, and updates to your plugin may be required for future versions of ETABS.
    /// An example plugin project can be found at: https://wiki.csiamerica.com/display/kb/Sample+Plugin+1
    /// TODO: Look at this and ensure the libary works correctly with it.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract class CSiPlugin : IDisposable
    {
        #region Fields
        /// <summary>
        /// The plugin callback
        /// </summary>
        private readonly CSiProgram.cPluginCallback _pluginCallback;

        /// <summary>
        /// The error flag for the plugin.
        /// 0 = no error.
        /// </summary>
        protected readonly int _errorFlag = 0;
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiPlugin" /> class.
        /// </summary>
        protected CSiPlugin()
        {

        }

        ///// <summary>
        ///// Finalizes an instance of the <see cref="CSiPlugin"/> class.
        ///// </summary>
        //~CSiPlugin()
        //{
        //    Finish(_errorFlag);
        //}
        #endregion

        #region Methods: Public        
        /// <summary>
        /// It is OK for cPlugin.Main to return before the actual work is completed (e.g., return after displaying a form where the functionality implemented in the plugin can be accessed through different command buttons). <para />
        /// However, it is imperative to remember to call cPluginCallback.Finish to return the control back to ETABS when the plugin is ready to close. <para />
        /// If you want to provide multiple functionality in your plugin, you can provide options for the user when subroutine Main is called. <para />
        /// Options for the user to obtain information about the product, developer, technical support, and help should be provided.<para />
        /// Support for your plugin will not be provided by Computers and Structures, Inc.<para />
        /// As currently implemented, the cPlugin object will be destroyed between invocations from the ETABS Tools menu command that calls it, so data cannot be saved.
        /// </summary>
        /// <param name="sapModel">The application model to conduct API calls on.</param>
        /// <param name="pluginCallback">The plugin callback.</param>
        public abstract void Main(ref CSiProgram.cSapModel sapModel,
            ref CSiProgram.cPluginCallback pluginCallback);

        /// <summary>
        /// The return value should be zero if successful.<para />
        /// The string argument should be filled in by the function, and may be plain text or rich text. <para />
        /// If this function is found and returns zero, the string will be displayed when the user first adds the plugin to ETABS.<para />
        /// You can use this string to tell the user the purpose and author of the plugin. <para />
        /// This is in addition to any information you may provide when the user executes the plugin.
        /// </summary>
        /// <param name="text">The message to display to the program when the plugin is first added.</param>
        /// <returns>System.Int32.</returns>
        public abstract int Info(ref string text);

        #endregion

        #region Methods: Protected     
        /// <summary>
        /// To be called immediately before the plugin closes (e.g., if the plugin has a single main window, at the end of the close event of that form). <para />
        /// It expects an error flag (0 meaning no errors) to let the application know if the operation was successful or not. <para />
        /// The application will wait indefinitely for <see cref="PluginCallback.Finish(int)" /> to be called, so the plugin programmer must make sure that it is called when the plugin completes.
        /// </summary>
        /// <param name="errorFlag">An error flag (0 meaning no errors) to let the application know if the operation was successful or not.</param>
        protected void Finish(int errorFlag)
        {
            _pluginCallback?.Finish(errorFlag);
        }

        #region IDisposable Support
        /// <summary>
        /// The disposed value
        /// </summary>
        private bool _disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Finish(_errorFlag);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CSiPlugin() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        #endregion
    }
}