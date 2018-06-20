#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Limit state used in NTC2008.
    /// </summary>
    public enum eLimitState_NTC_2008
    {
        /// <summary>
        /// SLC.
        /// </summary>
        SLO = 1,

        /// <summary>
        /// SLC.
        /// </summary>
        SLD = 2,

        /// <summary>
        /// SLV.
        /// </summary>
        SLV = 3,

        /// <summary>
        /// SLC.
        /// </summary>
        SLC = 4
    }
}
#endif
