#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Structure type.
    /// Used by NBCC95 code.
    /// </summary>
    public enum eStructureTypeSimple
    {
        /// <summary>
        /// Moment frame.
        /// </summary>
        MomentFrame = 1,

        /// <summary>
        /// Other.
        /// </summary>
        Other = 2,
    }
}
#endif
