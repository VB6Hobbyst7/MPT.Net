#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Aluminum
{
    /// <summary>
    /// Represents the aluminum frame design codes in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public abstract class AluminumCode : CSiApiBase
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="AluminumCode"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected AluminumCode(CSiApiSeed seed) : base(seed)
        { }

        // No methods created, as this is meant to be a shared type for all auto wind loads.

        #endregion
    }
}
#endif