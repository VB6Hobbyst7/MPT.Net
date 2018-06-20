#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Seismic intensity.
    /// Used in Chinese code.
    /// </summary>
    public enum eSeismicIntensity
    {
        /// <summary>
        /// 6  (0.05g).
        /// </summary>
        I6_05g = 1,

        /// <summary>
        /// 7  (0.10g).
        /// </summary>
        I7_10g = 2,

        /// <summary>
        /// 7  (0.15g).
        /// </summary>
        I7_15g = 3,

        /// <summary>
        /// 8  (0.20g).
        /// </summary>
        I8_20g = 4,

        /// <summary>
        /// 8  (0.30g).
        /// </summary>
        I8_30g = 5,

        /// <summary>
        /// 9  (0.40g).
        /// </summary>
        I9_40g = 6,
    }
}
#endif
