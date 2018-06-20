
namespace MPT.CSI.API.Core
{
    /// <summary>
    /// Framing types available in the application, inculding seismic classifications.
    /// AISC360_05_IBC2006
    /// </summary>
    public enum eFramingTypeSeismic
    {
        /// <summary>
        /// Program default.
        /// </summary>
        ProgramDefault = 0,

        /// <summary>
        /// Special Moment Frame.
        /// </summary>
        SMF = 1,

        /// <summary>
        /// Intermediate Moment Frame.
        /// </summary>
        IMF = 2,

        /// <summary>
        /// Ordinary Moment Frame.
        /// </summary>
        OMF = 3,

        /// <summary>
        /// Special Concentric Braced Frame.
        /// </summary>
        SCBF = 4,

        /// <summary>
        /// Ordinary Concentric Braced Frame.
        /// </summary>
        OCBF = 5,

        // TODO: Determine what this is
        /// <summary>
        /// Ordinary Concentric Braced Frame?.
        /// </summary>
        OCBFI = 6,

        /// <summary>
        /// Eccentric braced Frame.
        /// </summary>
        EBF = 7
    }
}
