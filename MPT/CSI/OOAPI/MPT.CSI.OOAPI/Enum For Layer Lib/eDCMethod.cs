
namespace MPT.CSI.API.Core
{
    /// <summary>
    /// Demand/Capacity method by which a frame element is judged to have failed.
    /// </summary>
    public enum eDCMethod
    {
        /// <summary>
        /// Design codes are used to determine D/C of the force or stress load vs. capacity. 
        /// Usually uses seismic code for ductile design.
        /// </summary>
        /// <remarks></remarks>
        Strength = 1,

        /// <summary>
        /// Calculations are done to determine plastic deformations of a frame section, which are compared against elongation and rotational limits.
        /// </summary>
        /// <remarks></remarks>
        Ductility = 2
    }
}
