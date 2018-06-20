#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Spectrum type to use in NTC2008.
    /// </summary>
    public enum eSpectrumType_NTC_2008
    {
        /// <summary>
        /// Elastic horizontal.
        /// </summary>
        ElasticHorizontal = 1,

        /// <summary>
        /// Elastic vertical.
        /// </summary>
        ElasticVertical = 2,

        /// <summary>
        /// Design horizontal.
        /// </summary>
        DesignHorizontal = 3,

        /// <summary>
        /// Design vertical.
        /// </summary>
        DesignVertical = 4
    }
}
#endif
