// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="IGeneralEditor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Edit
{
    /// <summary>
    /// Implements the general editor in the application.
    /// </summary>
    public interface IGeneralEditor
    {
        #region Methods: Public     

        /// <summary>
        /// This function moves selected point, frame, cable, tendon, area, solid and link objects.
        /// </summary>
        /// <param name="offsets">The offsets used in the present coordinate system for moving the selected objects. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void Move(Coordinate3DCartesian offsets);

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Extrudes the area to solid linear normal.
        /// </summary>
        /// <param name="name">The name of an existing area object to be extruded.</param>
        /// <param name="sectionPropertyName">This is Default, None or the name of a defined solid section property to be used for the new extruded solid objects.</param>
        /// <param name="numberPositive3">The number of solid objects created in the positive local 3-axis direction of the specified area object</param>
        /// <param name="thicknessPositive3">The thickness of the solid objects created in the positive local 3-axis direction of the specified area object.</param>
        /// <param name="numberNegative3">The number of solid objects created in the negative local 3-axis direction of the specified area object.</param>
        /// <param name="thicknessNegative3">The thickness of the solid objects created in the negative local 3-axis direction of the specified area object.</param>
        /// <param name="numberOfSolids">The number of solid objects created when the specified area object is extruded.</param>
        /// <param name="solidNames">The name of each solid object created when the specified area object is extruded.</param>
        /// <param name="remove">True: The area object indicated by the <paramref name="name" /> item is deleted after the extrusion is complete.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void ExtrudeAreaToSolidLinearNormal(string name,
            string sectionPropertyName,
            int numberPositive3,
            double thicknessPositive3,
            int numberNegative3,
            double thicknessNegative3,
            int numberOfSolids,
            ref string[] solidNames,
            bool remove = true);


        /// <summary>
        /// This function creates new solid objects by linearly extruding a specified area object, in a user specified direction, into solid objects.
        /// </summary>
        /// <param name="name">The name of an existing area object to be extruded.</param>
        /// <param name="sectionPropertyName">This is Default, None or the name of a defined solid section property to be used for the new extruded solid objects.</param>
        /// <param name="offsets">The offsets used, in the present coordinate system, to create each new solid object.</param>
        /// <param name="numberOfIncrements">The number of increments for the extrusion.</param>
        /// <param name="numberOfSolids">The number of solid objects created when the specified area object is extruded.
        /// Usually this item is returned the same as the <paramref name="numberOfIncrements" /> item.
        /// However, in some cases, such as when an area object with more than four sides is extruded, this item will be larger than the <paramref name="numberOfIncrements" /> item.</param>
        /// <param name="solidNames">The name of each solid object created when the specified area object is extruded.</param>
        /// <param name="remove">True: The area object indicated by the <paramref name="name" /> item is deleted after the extrusion is complete.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void ExtrudeAreaToSolidLinearUser(string name,
            string sectionPropertyName,
            Coordinate3DCartesian offsets,
            int numberOfIncrements,
            int numberOfSolids,
            ref string[] solidNames,
            bool remove = true);

        /// <summary>
        /// This function creates new solid objects by radially extruding a specified area object into solid objects.
        /// </summary>
        /// <param name="name">The name of an existing area object to be extruded.</param>
        /// <param name="sectionPropertyName">This is Default, None or the name of a defined solid section property to be used for the new extruded solid objects.</param>
        /// <param name="rotateAxis">The axis that the radial extrusion is around.</param>
        /// <param name="radialExtrusionCenter">These are the x, y and z coordinates, in the present coordinate system, of the point that the radial extrusion is around.
        /// For rotation about the X axis, the value of the x coordinate is irrelevant.
        /// Similarly, for rotation about the Y and Z axes, the y and z coordinates, respectively, are irrelevant. [L]</param>
        /// <param name="incrementAngle">The angle is rotated by this amount for each added solid object. [deg]</param>
        /// <param name="totalRise">The total rise over the full length of the extrusion. [L]</param>
        /// <param name="numberOfAngleIncrements">The number of angle increments for the extrusion.</param>
        /// <param name="numberOfSolids">The number of solid objects created when the specified area object is extruded.
        /// Usually this item is returned the same as the <paramref name="numberOfAngleIncrements" /> item.
        /// However, in some cases, such as when an area object with more than four sides is extruded, this item will be larger than the <paramref name="numberOfAngleIncrements" /> item.</param>
        /// <param name="solidNames">The name of each solid object created when the specified point object is extruded.</param>
        /// <param name="remove">True: The area object indicated by the <paramref name="name" /> item is deleted after the extrusion is complete.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void ExtrudeAreaToSolidRadial(string name,
            string sectionPropertyName,
            eAxisExtrusion rotateAxis,
            Coordinate3DCartesian radialExtrusionCenter,
            double incrementAngle,
            double totalRise,
            int numberOfAngleIncrements,
            ref int numberOfSolids,
            ref string[] solidNames,
            bool remove = true);

        /// <summary>
        /// This function creates new area objects by linearly extruding a specified straight frame object into area objects.
        /// </summary>
        /// <param name="name">The name of an existing frame object to be extruded.</param>
        /// <param name="sectionPropertyName">This is Default, None or the name of a defined area section property to be used for the new extruded area objects.</param>
        /// <param name="offsets">The offsets used, in the present coordinate system, to create each new area object.</param>
        /// <param name="numberOfAreas">The number of area objects created when the specified point object is extruded.</param>
        /// <param name="areaNames">The name of each area object created when the specified point object is extruded.</param>
        /// <param name="remove">True: The area object indicated by the <paramref name="name" /> item is deleted after the extrusion is complete.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void ExtrudeFrameToAreaLinear(string name,
            string sectionPropertyName,
            Coordinate3DCartesian offsets,
            int numberOfAreas,
            ref string[] areaNames,
            bool remove = true);

        /// <summary>
        /// This function creates new area objects by radially extruding a specified straight frame object into area objects.
        /// </summary>
        /// <param name="name">The name of an existing frame object to be extruded.</param>
        /// <param name="sectionPropertyName">This is Default, None or the name of a defined frame section property to be used for the new extruded area objects.</param>
        /// <param name="rotateAxis">The axis that the radial extrusion is around.</param>
        /// <param name="radialExtrusionCenter">These are the x, y and z coordinates, in the present coordinate system, of the point that the radial extrusion is around.
        /// For rotation about the X axis, the value of the x coordinate is irrelevant.
        /// Similarly, for rotation about the Y and Z axes, the y and z coordinates, respectively, are irrelevant. [L]</param>
        /// <param name="incrementAngle">The angle is rotated by this amount for each added area object. [deg]</param>
        /// <param name="totalRise">The total rise over the full length of the extrusion. [L]</param>
        /// <param name="numberOfAreas">The number of area objects created when the specified point object is extruded.</param>
        /// <param name="areaNames">The name of each area object created when the specified point object is extruded.</param>
        /// <param name="remove">True: The area object indicated by the <paramref name="name" /> item is deleted after the extrusion is complete.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void ExtrudeFrameToAreaRadial(string name,
            string sectionPropertyName,
            eAxisExtrusion rotateAxis,
            Coordinate3DCartesian radialExtrusionCenter,
            double incrementAngle,
            double totalRise,
            int numberOfAreas,
            ref string[] areaNames,
            bool remove = true);

        /// <summary>
        /// This function creates new frame objects by linearly extruding a specified point object into frame objects.
        /// </summary>
        /// <param name="name">The name of an existing point object to be extruded.</param>
        /// <param name="sectionPropertyName">This is Default, None or the name of a defined frame section property to be used for the new extruded frame objects.</param>
        /// <param name="offsets">The offsets used, in the present coordinate system, to create each new frame object.</param>
        /// <param name="numberOfFrames">The number of frame objects created when the specified point object is extruded.</param>
        /// <param name="frameNames">The name of each frame object created when the specified point object is extruded.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void ExtrudePointToFrameLinear(string name,
            string sectionPropertyName,
            Coordinate3DCartesian offsets,
            int numberOfFrames,
            ref string[] frameNames);

        /// <summary>
        /// This function creates new frame objects by radially extruding a specified point object into frame objects.
        /// </summary>
        /// <param name="name">The name of an existing point object to be extruded.</param>
        /// <param name="sectionPropertyName">This is Default, None or the name of a defined frame section property to be used for the new extruded frame objects.</param>
        /// <param name="rotateAxis">The axis that the radial extrusion is around.</param>
        /// <param name="radialExtrusionCenter">These are the x, y and z coordinates, in the present coordinate system, of the point that the radial extrusion is around.
        /// For rotation about the X axis, the value of the x coordinate is irrelevant.
        /// Similarly, for rotation about the Y and Z axes, the y and z coordinates, respectively, are irrelevant. [L]</param>
        /// <param name="incrementAngle">The angle is rotated by this amount for each added frame object. [deg]</param>
        /// <param name="totalRise">The total rise over the full length of the extrusion. [L]</param>
        /// <param name="numberOfFrames">The number of frame objects created when the specified point object is extruded.</param>
        /// <param name="frameNames">The name of each frame object created when the specified point object is extruded.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void ExtrudePointToFrameRadial(string name,
            string sectionPropertyName,
            eAxisExtrusion rotateAxis,
            Coordinate3DCartesian radialExtrusionCenter,
            double incrementAngle,
            double totalRise,
            int numberOfFrames,
            ref string[] frameNames);


        /// <summary>
        /// This function linearly replicates selected objects.
        /// </summary>
        /// <param name="offsets">The offsets used in the present coordinate system for replicating the selected objects. [L]</param>
        /// <param name="numberReplication">The number of times the selected objects are to be replicated.</param>
        /// <param name="numberOfObjects">The number of objects created by the replication.</param>
        /// <param name="objectNames">The object names created by the replication.</param>
        /// <param name="objectTypes">The object types created by the replication.</param>
        /// <param name="remove">True: The originally selected objects are deleted after the replication is complete.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void ReplicateLinear(Coordinate3DCartesian offsets,
            int numberReplication,
            ref int numberOfObjects,
            ref string[] objectNames,
            ref eObjectType[] objectTypes,
            bool remove = false);

        /// <summary>
        /// This function mirror replicates selected objects.
        /// </summary>
        /// <param name="planeAxis">The plane axis by which the object is mirrored.</param>
        /// <param name="planeAxisCoordinate1">The axis coordinate used to define the rotation axis. [L]
        /// When <paramref name="planeAxis" /> = <see cref="eAxisOrientation.ParallelToXAxis" />, x1, y1, x2 and y2 define the intersection of the mirror plane with the XY plane.
        /// When <paramref name="planeAxis" /> = <see cref="eAxisOrientation.ParallelToYAxis" />, y1, z1, y2 and z2 define the intersection of the mirror plane with the YZ plane.
        /// When <paramref name="planeAxis" /> = <see cref="eAxisOrientation.ParallelToZAxis" />, x1, z1, x2 and z2 define the intersection of the mirror plane with the XZ plane.</param>
        /// <param name="planeAxisCoordinate2">The axis coordinate used to define the rotation axis. [L]
        /// When <paramref name="planeAxis" /> = <see cref="eAxisOrientation.ParallelToXAxis" />, x1, y1, x2 and y2 define the intersection of the mirror plane with the XY plane.
        /// When <paramref name="planeAxis" /> = <see cref="eAxisOrientation.ParallelToYAxis" />, y1, z1, y2 and z2 define the intersection of the mirror plane with the YZ plane.
        /// When <paramref name="planeAxis" /> = <see cref="eAxisOrientation.ParallelToZAxis" />, x1, z1, x2 and z2 define the intersection of the mirror plane with the XZ plane.</param>
        /// <param name="planeCoordinate">When <paramref name="planeAxis" /> = <see cref="eAxisOrientation.Line3D" />, <paramref name="planeAxisCoordinate1" />, <paramref name="planeAxisCoordinate2" />, and <paramref name="planeCoordinate" /> are used to define the mirror plane. [L]</param>
        /// <param name="numberOfObjects">The number of objects created by the replication.</param>
        /// <param name="objectNames">The object names created by the replication.</param>
        /// <param name="objectTypes">The object types created by the replication.</param>
        /// <param name="remove">True: The originally selected objects are deleted after the replication is complete.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void ReplicateMirror(eAxisOrientation planeAxis,
            Coordinate3DCartesian planeAxisCoordinate1,
            Coordinate3DCartesian planeAxisCoordinate2,
            Coordinate3DCartesian planeCoordinate,
            ref int numberOfObjects,
            ref string[] objectNames,
            ref eObjectType[] objectTypes,
            bool remove = false);


        /// <summary>
        /// This function radially replicates selected objects..
        /// </summary>
        /// <param name="rotationAxis">The rotation axis used for the replication.</param>
        /// <param name="axisCoordinate">The axis coordinate used to define the rotation axis. [L]</param>
        /// <param name="axis3DCoordinate">The axis coordinate used to define the rotation axis when <paramref name="rotationAxis" /> = <see cref="eAxisOrientation.Line3D" />. [L]</param>
        /// <param name="numberReplication">The number of times the selected objects are to be replicated.</param>
        /// <param name="incrementAngle">The increment angle for each replication.</param>
        /// <param name="numberOfObjects">The number of objects created by the replication.</param>
        /// <param name="objectNames">The object names created by the replication.</param>
        /// <param name="objectTypes">The object types created by the replication.</param>
        /// <param name="remove">True: The originally selected objects are deleted after the replication is complete.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void ReplicateRadial(eAxisOrientation rotationAxis,
            Coordinate3DCartesian axisCoordinate,
            Coordinate3DCartesian axis3DCoordinate,
            int numberReplication,
            double incrementAngle,
            ref int numberOfObjects,
            ref string[] objectNames,
            ref eObjectType[] objectTypes,
            bool remove = false);
#endif

        #endregion
    }
}