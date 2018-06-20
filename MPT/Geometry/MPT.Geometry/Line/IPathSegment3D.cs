using MPT.Math;

namespace MPT.Geometry.Line
{
    /// <summary>
    /// Interface for any segment along a path in space.
    /// </summary>
    public interface IPathSegment3D
    {
        /// <summary>
        /// First coordinate value.
        /// </summary>
        Point3D I { get; set; }

        /// <summary>
        /// Second coordinate value.
        /// </summary>
        Point3D J { get; set; }
    }
}
