#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Soil profile type.
    /// Used by UBC97.
    /// </summary>
    public enum eSoilProfileType
    {
        /// <summary>
        /// SA.
        /// </summary>
        SA = 1,

        /// <summary>
        /// SB.
        /// </summary>
        SB = 2,

        /// <summary>
        /// SC.
        /// </summary>
        SC = 3,

        /// <summary>
        /// SD.
        /// </summary>
        SD = 4,

        /// <summary>
        /// SE.
        /// </summary>
        SE = 5
    }
}
#endif
