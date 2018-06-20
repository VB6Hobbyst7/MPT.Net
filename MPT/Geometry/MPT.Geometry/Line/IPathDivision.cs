using MPT.Math;

namespace MPT.Geometry.Line
{
    /// <summary>
    /// Interface for paths that are divisible.
    /// </summary>
    public interface IPathDivision
    {
        /// <summary>
        /// Returns a point a given fraction of the distance between two points.
        /// </summary>
        /// <param name="fraction">Fraction of the way from point 1 to point 2.</param>
        /// <returns></returns>
        Point PointDivision(double fraction);
    }
}
