#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Structure type.
    /// Used by NBCC codes.
    /// </summary>
    public enum eStructureType
    {
        /// <summary>
        /// Steel moment frame.
        /// </summary>
        SteelMomentFrame = 0,

        /// <summary>
        /// Concrete moment frame.
        /// </summary>
        ConcreteMomentFrame = 1,

        /// <summary>
        /// Other moment frame.
        /// </summary>
        OtherMomentFrame = 2,

        /// <summary>
        /// Braced frame.
        /// </summary>
        BracedFrame = 3,

        /// <summary>
        /// Shear wall.
        /// </summary>
        ShearWall = 4
    }
}
#endif
