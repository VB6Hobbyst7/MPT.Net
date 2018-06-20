// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="AreaEditor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Edit
{
    /// <summary>
    /// Represents the area editor in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Edit.IAreaEditor" />
    public class AreaEditor : CSiApiBase, IAreaEditor
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaEditor" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AreaEditor(CSiApiSeed seed) : base(seed) { }


        #endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        #region Methods: Public

        /// <summary>
        /// This function meshes area objects.
        /// </summary>
        /// <param name="name">The name of an existing area object</param>
        /// <param name="meshType">The automatic mesh type for the object.
        /// Mesh options <see cref="eMeshType.SpecifiedNumber" />, <see cref="eMeshType.SpecifiedMaxSize" /> and <see cref="eMeshType.PointsOnAreaEdges" /> apply to quadrilaterals and triangles only.</param>
        /// <param name="numberOfAreas">The number of area objects created when the specified area object is divided.</param>
        /// <param name="areaNames">The name of each area object created when the specified area object is divided.</param>
        /// <param name="numberOfObjectsAlongPoint12">The number of objects created along the edge of the meshed object that runs from point 1 to point 2.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.</param>
        /// <param name="numberOfObjectsAlongPoint13">The number of objects created along the edge of the meshed object that runs from point 1 to point 3.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.</param>
        /// <param name="maxSizeOfObjectsAlongPoint12">The maximum size of objects created along the edge of the meshed object that runs from point 1 to point 2 [L]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.</param>
        /// <param name="maxSizeOfObjectsAlongPoint13">The maximum size of objects created along the edge of the meshed object that runs from point 1 to point 3. [L]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.</param>
        /// <param name="pointOnEdgeFromGrid">True: Points on the area object edges are determined from intersections of visible grid lines with the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="pointOnEdgeFromLine">True: Points on the area object edges are determined from intersections of straight line objects  with the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="pointOnEdgeFromPoint">True: Point on the area object edges are determined from point objects included in the group specified by the Group item that lie on the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="extendCookieCutLines">True: All straight line objects are extended to intersect the area object edges for the purpose of meshing the area object.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.CookieCutLinesIntersectingEdges" />, which provides cookie cut meshing based on straight line objects that intersect the area object edges.</param>
        /// <param name="rotation">An angle in degrees that the meshing lines are rotated from their default orientation. [deg]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.CookieCutPoints" />, which provides cookie cut meshing based on two perpendicular lines passing through point objects.</param>
        /// <param name="maxSizeGeneral">The maximum size of objects created by the General Divide Tool.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.GeneralDivideTool" />.</param>
        /// <param name="localAxesOnEdge">True: If both points along an edge of the original area object have the same local axes, the program makes the local axes for added points along the edge the same as the edge end points.</param>
        /// <param name="localAxesOnFace">True: If on the area object edges are determined from point objects that lie on the area object edges.</param>
        /// <param name="restraintsOnEdge">True: If both points along an edge of the original object have the same restraint/constraint, then, if an added point on that edge and the original corner points have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="restraintsOnFace">True: If all corner points on an object face have the same restraint/constraint, then, if an added point on that face and the original corner points for the face have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Divide(string name,
            eMeshType meshType,
            ref int numberOfAreas,
            ref string[] areaNames,
            int numberOfObjectsAlongPoint12 = 2,
            int numberOfObjectsAlongPoint13 = 2,
            double maxSizeOfObjectsAlongPoint12 = 0,
            double maxSizeOfObjectsAlongPoint13 = 0,
            bool pointOnEdgeFromGrid = false,
            bool pointOnEdgeFromLine = false,
            bool pointOnEdgeFromPoint = false,
            bool extendCookieCutLines = false,
            double rotation = 0,
            double maxSizeGeneral = 0,
            bool localAxesOnEdge = false,
            bool localAxesOnFace = false,
            bool restraintsOnEdge = false,
            bool restraintsOnFace = false
            )
        {
            _callCode = _sapModel.EditArea.Divide(name, (int)meshType, ref numberOfAreas, ref areaNames, 
                numberOfObjectsAlongPoint12, numberOfObjectsAlongPoint13, maxSizeOfObjectsAlongPoint12, maxSizeOfObjectsAlongPoint13, 
                pointOnEdgeFromGrid, pointOnEdgeFromLine, pointOnEdgeFromPoint, extendCookieCutLines,
                rotation, maxSizeGeneral,
                localAxesOnEdge, localAxesOnFace, restraintsOnEdge, restraintsOnFace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function expands or shrinks selected area objects.
        /// </summary>
        /// <param name="offsetType">Indicates the offset type for the selected area objects.</param>
        /// <param name="offset">The area edge offset distance.
        /// Positive distances expand the object and negative distances shrink the object.[L]</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void ExpandShrink(eAreaOffsetType offsetType,
            double offset)
        {
            _callCode = _sapModel.EditArea.ExpandShrink((int)offsetType, offset);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function merges selected area objects.
        /// </summary>
        /// <param name="numberOfAreas">The number of originally selected area objects that remain when the merge is successfully completed.</param>
        /// <param name="areaNames">The names of the selected area objects that remain when the merge is successfully completed.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Merge(ref int numberOfAreas,
            ref string[] areaNames)
        {
            _callCode = _sapModel.EditArea.Merge(ref numberOfAreas, ref areaNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function adds a point object at the midpoint of selected area object edges.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void PointAdd()
        {
            _callCode = _sapModel.EditArea.PointAdd();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function removes selected point objects from selected area objects.
        /// Note that in some cases this command can cause the area object to be deleted.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void PointRemove()
        {
            _callCode = _sapModel.EditArea.PointRemove();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function modifies the connectivity of an area object.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="pointNames">The names of the point objects that define the added area object.
        /// The point object names should be ordered to run clockwise or counter-clockwise around the area object.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void ChangeConnectivity(string name,
            ref string[] pointNames)
        {
            _callCode = _sapModel.EditArea.ChangeConnectivity(name, pointNames.Length, ref pointNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion
#endif
    }
}
