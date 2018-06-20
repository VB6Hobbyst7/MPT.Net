﻿// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="HorizontalProjection.cs" company="MPTinc">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;

using MPT.Math;
using Segment = MPT.Geometry.Line.LineSegment;

namespace MPT.Geometry.Intersection
{
    /// <summary>
    /// Handles calculations related to horizontal projections.
    /// </summary>
    public static class ProjectionHorizontal
    {
        /// <summary>
        /// The numbers of shape boundary intersections a horizontal line makes when projecting to the right from the provided point.
        /// If the point is on a vertex or segment, the function returns either 0 or 1.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="shapeBoundary">The shape boundary composed of n points.</param>
        /// <param name="includePointOnSegment">if set to <c>true</c> [include point on segment].</param>
        /// <param name="includePointOnVertex">if set to <c>true</c> [include point on vertex].</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.ArgumentException">Shape boundary describes a shape. Closure to the shape boundary is needed.</exception>
        public static int NumberOfIntersections(
            Point coordinate, 
            Point[] shapeBoundary,
            bool includePointOnSegment = true,
            bool includePointOnVertex = true,
            double tolerance = GeometryLibrary.ZeroTolerance)
        {
            if (shapeBoundary[0] != shapeBoundary[shapeBoundary.Length - 1])
                throw new ArgumentException("Shape boundary describes a shape. Closure to the shape boundary is needed.");

            // 1. Check horizontal line projection from a pt. n to the right
            // 2. Count # of intersections of the line with shape edges   
            
            // Note shape coordinates from XML already repeat starting node as an ending node.
            // No need to handle wrap-around below.

            int numberOfIntersections = 0;
            for (int i = 0; i < shapeBoundary.Length - 1; i++)
            {
                Point vertexI = shapeBoundary[i];
                if (PointIntersection.PointsOverlap(coordinate, vertexI))
                {
                    return includePointOnVertex ? 1 : 0;
                }

                Point vertexJ = shapeBoundary[i + 1];

                if (!PointIsLeftOfSegmentEnd(coordinate.X, vertexI, vertexJ))
                {
                    // Pt is to the right of the segment.
                    continue;
                }
                bool pointIsWithinSegmentHeight = PointIsWithinSegmentHeight(
                                                    coordinate.Y, 
                                                    vertexI, vertexJ, 
                                                    includeEnds: includePointOnSegment);
                if (!pointIsWithinSegmentHeight)
                {
                    // Point is out of vertical bounds of the segment extents.
                    continue;
                }
                bool pointIsWithinSegmentWidth = ProjectionVertical.PointIsWithinSegmentWidth(
                                                    coordinate.X, 
                                                    vertexI, vertexJ, 
                                                    includeEnds: includePointOnSegment);
                if (Segment.IsVertical(vertexI, vertexJ))
                {
                    if (pointIsWithinSegmentWidth)
                    { // Point is on vertical segment
                        return includePointOnSegment ? 1 : 0;
                    }
                    // Point hits vertical segment
                    numberOfIntersections++;
                    continue;
                }
                if (Segment.IsHorizontal(vertexI, vertexJ))
                {   // Segment would be parallel to line projection.
                    // Point is collinear since it is within segment height
                    if (pointIsWithinSegmentWidth)
                    { // Point is on horizontal segment
                        return includePointOnSegment ? 1 : 0;
                    }
                    continue;
                }

                double xIntersection = IntersectionPointX(coordinate.Y, vertexI, vertexJ);
                if (PointIsLeftOfSegmentIntersection(coordinate.X, xIntersection, vertexI, vertexJ))
                {
                    numberOfIntersections++;
                }
                else if (NMath.Abs(coordinate.X - xIntersection) < tolerance)
                { // Point is on sloped segment
                    return includePointOnSegment ? 1 : 0;
                }
            }
            return numberOfIntersections;
        }



        /// <summary>
        /// Determines if the point lies within the segment height.
        /// </summary>
        /// <param name="yPtN">The y-coordinate of pt n.</param>
        /// <param name="ptI">The vertex i.</param>
        /// <param name="ptJ">The vertex j.</param>
        /// <param name="includeEnds">if set to <c>true</c> [include ends].</param>
        /// <returns><c>true</c> if the point lies within the segment height, <c>false</c> otherwise.</returns>
        public static bool PointIsWithinSegmentHeight(
            double yPtN,
            Point ptI,
            Point ptJ,
            bool includeEnds = true)
        {
            if (includeEnds)
            {
                return (NMath.Min(ptI.Y, ptJ.Y) <= yPtN && yPtN <= NMath.Max(ptI.Y, ptJ.Y));
            }
            return (NMath.Min(ptI.Y, ptJ.Y) < yPtN && yPtN < NMath.Max(ptI.Y, ptJ.Y));
        }

        /// <summary>
        /// Determines if the point lies to the left of the segment end.
        /// </summary>
        /// <param name="xPtN">The x-coordinate of pt n.</param>
        /// <param name="ptI">Vertex i.</param>
        /// <param name="ptJ">Vertex j.</param>
        /// <param name="includeEnds">if set to <c>true</c> [include ends].</param>
        /// <returns><c>true</c> if the point lies to the left of the segment end, <c>false</c> otherwise.</returns>
        public static bool PointIsLeftOfSegmentEnd(
            double xPtN,
            Point ptI,
            Point ptJ,
            bool includeEnds = true)
        {
            if (includeEnds)
            {
               return xPtN <= NMath.Max(ptI.X, ptJ.X);
            }
            return xPtN < NMath.Max(ptI.X, ptJ.X);
        }

        /// <summary>
        /// The x-coordinate of the intersection of the horizontally projected line with the provided segment.
        /// </summary>
        /// <param name="yPtN">The y-coordinate of pt n, where the projection starts.</param>
        /// <param name="ptI">Vertex i of the segment.</param>
        /// <param name="ptJ">Vertex j of the segment.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.ArgumentException">Segment is horizontal, so intersection point is either infinity or NAN.</exception>
        public static double IntersectionPointX(
            double yPtN,
            Point ptI,
            Point ptJ)
        {
            if (Segment.IsHorizontal(ptI, ptJ))
                throw new ArgumentException("Segment is horizontal, so intersection point is either infinity or NAN.");
            return (yPtN - ptI.Y) * ((ptJ.X - ptI.X) / (ptJ.Y - ptI.Y)) + ptI.X;
        }

        /// <summary>
        /// Determines if the point is to the left of the horizontally projected segment intersection.
        /// </summary>
        /// <param name="xPtN">The x-coordinate of pt n.</param>
        /// <param name="xIntersection">The x-coordinate of the intersection of the projected line.</param>
        /// <param name="vertexI">The vertex i.</param>
        /// <param name="vertexJ">The vertex j.</param>
        /// <param name="includeEnds">if set to <c>true</c> [include ends].</param>
        /// <returns><c>true</c> if the point is to the left of the horizontally projected segment intersection, <c>false</c> otherwise.</returns>
        public static bool PointIsLeftOfSegmentIntersection(
            double xPtN, 
            double xIntersection,
            Point vertexI,
            Point vertexJ,
            bool includeEnds = true)
        {
            return (xPtN < xIntersection && 
                ProjectionVertical.PointIsWithinSegmentWidth(xIntersection, vertexI, vertexJ, includeEnds: includeEnds));
        }
    }
}
