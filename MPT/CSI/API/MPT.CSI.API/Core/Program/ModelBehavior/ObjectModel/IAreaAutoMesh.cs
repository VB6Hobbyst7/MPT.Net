// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IAreaAutoMesh.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can be automeshed as an area.
    /// </summary>
    public interface IAreaAutoMesh
    {
        /// <summary>
        /// This function retrieves automatic meshing assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="meshType">The automatic mesh type for the object.</param>
        /// <param name="numberOfObjectsAlongPoint12">The number of objects created along the edge of the meshed object that runs from point 1 to point 2.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.</param>
        /// <param name="numberOfObjectsAlongPoint13">The number of objects created along the edge of the meshed object that runs from point 1 to point 3.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.</param>
        /// <param name="maxSizeOfObjectsAlongPoint12">The maximum size of objects created along the edge of the meshed object that runs from point 1 to point 2 [L]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.</param>
        /// <param name="maxSizeOfObjectsAlongPoint13">The maximum size of objects created along the edge of the meshed object that runs from point 1 to point 3. [L]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.</param>
        /// <param name="pointOnEdgeFromLine">True: Points on the area object edges are determined from intersections of straight line objects included in the group specified by the <paramref name="group" /> item with the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="pointOnEdgeFromPoint">True: Point on the area object edges are determined from point objects included in the group specified by the Group item that lie on the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="extendCookieCutLines">True: All straight line objects included in the group specified by the <paramref name="group" /> item are extended to intersect the area object edges for the purpose of meshing the area object.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.CookieCutLinesIntersectingEdges" />, which provides cookie cut meshing based on straight line objects included in the group specified by the <paramref name="group" /> item that intersect the area object edges.</param>
        /// <param name="rotation">An angle in degrees that the meshing lines are rotated from their default orientation. [deg]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.CookieCutPoints" />, which provides cookie cut meshing based on two perpendicular lines passing through point objects included in the group specified by the <paramref name="group" /> item.</param>
        /// <param name="maxSizeGeneral">The maximum size of objects created by the General Divide Tool.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.GeneralDivideTool" />.</param>
        /// <param name="localAxesOnEdge">True: If both points along an edge of the original area object have the same local axes, the program makes the local axes for added points along the edge the same as the edge end points.</param>
        /// <param name="localAxesOnFace">True: If on the area object edges are determined from point objects included in the group specified by the <paramref name="group" /> item that lie on the area object edges.</param>
        /// <param name="restraintsOnEdge">True: If both points along an edge of the original object have the same restraint/constraint, then, if an added point on that edge and the original corner points have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="restraintsOnFace">True: If all corner points on an object face have the same restraint/constraint, then, if an added point on that face and the original corner points for the face have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="group">The name of a defined group.
        /// Some of the meshing options make use of point and line objects included in this group.</param>
        /// <param name="subMesh">True: After initial meshing, the program further meshes any area objects that have an edge longer than the length specified by the <paramref name="subMeshSize" /> = item.</param>
        /// <param name="subMeshSize">The maximum size of area objects to remain when the auto meshing is complete. [L]
        /// This item applies when <paramref name="subMesh" /> = True.</param>
        void GetAutoMesh(string name,
            ref eMeshType meshType,
            ref int numberOfObjectsAlongPoint12,
            ref int numberOfObjectsAlongPoint13,
            ref double maxSizeOfObjectsAlongPoint12,
            ref double maxSizeOfObjectsAlongPoint13,
            ref bool pointOnEdgeFromLine,
            ref bool pointOnEdgeFromPoint,
            ref bool extendCookieCutLines,
            ref double rotation,
            ref double maxSizeGeneral,
            ref bool localAxesOnEdge,
            ref bool localAxesOnFace,
            ref bool restraintsOnEdge,
            ref bool restraintsOnFace,
            ref string group,
            ref bool subMesh,
            ref double subMeshSize);

        /// <summary>
        /// This function retrieves automatic meshing assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="meshType">The automatic mesh type for the object.</param>
        /// <param name="numberOfObjectsAlongPoint12">The number of objects created along the edge of the meshed object that runs from point 1 to point 2.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.</param>
        /// <param name="numberOfObjectsAlongPoint13">The number of objects created along the edge of the meshed object that runs from point 1 to point 3.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.</param>
        /// <param name="maxSizeOfObjectsAlongPoint12">The maximum size of objects created along the edge of the meshed object that runs from point 1 to point 2 [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.</param>
        /// <param name="maxSizeOfObjectsAlongPoint13">The maximum size of objects created along the edge of the meshed object that runs from point 1 to point 3. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.</param>
        /// <param name="pointOnEdgeFromLine">True: Points on the area object edges are determined from intersections of straight line objects included in the group specified by the <paramref name="group" /> item with the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="pointOnEdgeFromPoint">True: Point on the area object edges are determined from point objects included in the group specified by the Group item that lie on the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="extendCookieCutLines">True: All straight line objects included in the group specified by the <paramref name="group" /> item are extended to intersect the area object edges for the purpose of meshing the area object.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.CookieCutLinesIntersectingEdges" />, which provides cookie cut meshing based on straight line objects included in the group specified by the <paramref name="group" /> item that intersect the area object edges.</param>
        /// <param name="rotation">An angle in degrees that the meshing lines are rotated from their default orientation. [deg]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.CookieCutPoints" />, which provides cookie cut meshing based on two perpendicular lines passing through point objects included in the group specified by the <paramref name="group" /> item.</param>
        /// <param name="maxSizeGeneral">The maximum size of objects created by the General Divide Tool.
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.GeneralDivideTool" />.</param>
        /// <param name="localAxesOnEdge">True: If both points along an edge of the original area object have the same local axes, the program makes the local axes for added points along the edge the same as the edge end points.</param>
        /// <param name="localAxesOnFace">True: If on the area object edges are determined from point objects included in the group specified by the <paramref name="group" /> item that lie on the area object edges.</param>
        /// <param name="restraintsOnEdge">True: If both points along an edge of the original object have the same restraint/constraint, then, if an added point on that edge and the original corner points have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="restraintsOnFace">True: If all corner points on an object face have the same restraint/constraint, then, if an added point on that face and the original corner points for the face have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="group">The name of a defined group.
        /// Some of the meshing options make use of point and line objects included in this group.</param>
        /// <param name="subMesh">True: After initial meshing, the program further meshes any area objects that have an edge longer than the length specified by the <paramref name="subMeshSize" /> = item.</param>
        /// <param name="subMeshSize">The maximum size of area objects to remain when the auto meshing is complete. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 12 inches if the database units are English or 30 centimeters if the database units are metric.
        /// This item applies when <paramref name="subMesh" /> = True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetAutoMesh(string name,
            eMeshType meshType,
            int numberOfObjectsAlongPoint12 = 2,
            int numberOfObjectsAlongPoint13 = 2,
            double maxSizeOfObjectsAlongPoint12 = 0,
            double maxSizeOfObjectsAlongPoint13 = 0,
            bool pointOnEdgeFromLine = false,
            bool pointOnEdgeFromPoint = false,
            bool extendCookieCutLines = false,
            double rotation = 0,
            double maxSizeGeneral = 0,
            bool localAxesOnEdge = false,
            bool localAxesOnFace = false,
            bool restraintsOnEdge = false,
            bool restraintsOnFace = false,
            string group = "ALL",
            bool subMesh = false,
            double subMeshSize = 0,
            eItemType itemType = eItemType.Object);
    }
}
#endif