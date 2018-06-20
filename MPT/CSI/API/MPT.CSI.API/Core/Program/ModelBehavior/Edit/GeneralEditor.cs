// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="GeneralEditor.cs" company="">
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
    /// Represents the general editor in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Edit.IGeneralEditor" />
    public class GeneralEditor : CSiApiBase, IGeneralEditor
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralEditor" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public GeneralEditor(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public     
        /// <summary>
        /// This function moves selected point, frame, cable, tendon, area, solid and link objects.
        /// </summary>
        /// <param name="offsets">The offsets used in the present coordinate system for moving the selected objects. [L]</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Move(Coordinate3DCartesian offsets)
        {
            _callCode = _sapModel.EditGeneral.Move(offsets.X, offsets.Y, offsets.Z);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ExtrudeAreaToSolidLinearNormal(string name,
            string sectionPropertyName,
            int numberPositive3,
            double thicknessPositive3,
            int numberNegative3,
            double thicknessNegative3,
            int numberOfSolids,
            ref string[] solidNames,
            bool remove = true)
        {
            _callCode = _sapModel.EditGeneral.ExtrudeAreaToSolidLinearNormal(name, sectionPropertyName,
               numberPositive3, thicknessPositive3, numberNegative3, thicknessNegative3,
               ref numberOfSolids, ref solidNames, remove);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


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
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ExtrudeAreaToSolidLinearUser(string name,
            string sectionPropertyName,
            Coordinate3DCartesian offsets,
            int numberOfIncrements,
            int numberOfSolids,
            ref string[] solidNames,
            bool remove = true)
        {
            _callCode = _sapModel.EditGeneral.ExtrudeAreaToSolidLinearUser(name, sectionPropertyName,
                offsets.X, offsets.Y, offsets.Z,
                numberOfIncrements, ref numberOfSolids, ref solidNames, remove);
            
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ExtrudeAreaToSolidRadial(string name,
            string sectionPropertyName,
            eAxisExtrusion rotateAxis,
            Coordinate3DCartesian radialExtrusionCenter,
            double incrementAngle,
            double totalRise,
            int numberOfAngleIncrements,
            ref int numberOfSolids,
            ref string[] solidNames,
            bool remove = true)
        {
            _callCode = _sapModel.EditGeneral.ExtrudeAreaToSolidRadial(name, sectionPropertyName, (int)rotateAxis,
                radialExtrusionCenter.X, radialExtrusionCenter.Y, radialExtrusionCenter.Z,
                incrementAngle, totalRise, numberOfAngleIncrements, ref numberOfSolids, ref solidNames, remove);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function creates new area objects by linearly extruding a specified straight frame object into area objects.
        /// </summary>
        /// <param name="name">The name of an existing frame object to be extruded.</param>
        /// <param name="sectionPropertyName">This is Default, None or the name of a defined area section property to be used for the new extruded area objects.</param>
        /// <param name="offsets">The offsets used, in the present coordinate system, to create each new area object.</param>
        /// <param name="numberOfAreas">The number of area objects created when the specified point object is extruded.</param>
        /// <param name="areaNames">The name of each area object created when the specified point object is extruded.</param>
        /// <param name="remove">True: The area object indicated by the <paramref name="name" /> item is deleted after the extrusion is complete.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ExtrudeFrameToAreaLinear(string name,
            string sectionPropertyName,
            Coordinate3DCartesian offsets,
            int numberOfAreas,
            ref string[] areaNames,
            bool remove = true)
        {
            _callCode = _sapModel.EditGeneral.ExtrudeFrameToAreaLinear(name, sectionPropertyName,
                offsets.X, offsets.Y, offsets.Z,
                numberOfAreas, ref areaNames, remove);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ExtrudeFrameToAreaRadial(string name,
            string sectionPropertyName,
            eAxisExtrusion rotateAxis,
            Coordinate3DCartesian radialExtrusionCenter,
            double incrementAngle,
            double totalRise,
            int numberOfAreas,
            ref string[] areaNames,
            bool remove = true)
        {
            _callCode = _sapModel.EditGeneral.ExtrudeFrameToAreaRadial(name, sectionPropertyName, (int)rotateAxis,
                radialExtrusionCenter.X, radialExtrusionCenter.Y, radialExtrusionCenter.Z,
                incrementAngle, totalRise, numberOfAreas, ref areaNames, remove);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function creates new frame objects by linearly extruding a specified point object into frame objects.
        /// </summary>
        /// <param name="name">The name of an existing point object to be extruded.</param>
        /// <param name="sectionPropertyName">This is Default, None or the name of a defined frame section property to be used for the new extruded frame objects.</param>
        /// <param name="offsets">The offsets used, in the present coordinate system, to create each new frame object.</param>
        /// <param name="numberOfFrames">The number of frame objects created when the specified point object is extruded.</param>
        /// <param name="frameNames">The name of each frame object created when the specified point object is extruded.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ExtrudePointToFrameLinear(string name,
            string sectionPropertyName,
            Coordinate3DCartesian offsets,
            int numberOfFrames,
            ref string[] frameNames)
        {
            _callCode = _sapModel.EditGeneral.ExtrudePointToFrameLinear(name, sectionPropertyName,
                offsets.X, offsets.Y, offsets.Z,
                numberOfFrames, ref frameNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ExtrudePointToFrameRadial(string name,
            string sectionPropertyName,
            eAxisExtrusion rotateAxis,
            Coordinate3DCartesian radialExtrusionCenter,
            double incrementAngle,
            double totalRise,
            int numberOfFrames,
            ref string[] frameNames)
        {
            _callCode = _sapModel.EditGeneral.ExtrudePointToFrameRadial(name, sectionPropertyName, (int)rotateAxis, 
                radialExtrusionCenter.X, radialExtrusionCenter.Y, radialExtrusionCenter.Z,
                incrementAngle, totalRise, numberOfFrames, ref frameNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function linearly replicates selected objects.
        /// </summary>
        /// <param name="offsets">The offsets used in the present coordinate system for replicating the selected objects. [L]</param>
        /// <param name="numberReplication">The number of times the selected objects are to be replicated.</param>
        /// <param name="numberOfObjects">The number of objects created by the replication.</param>
        /// <param name="objectNames">The object names created by the replication.</param>
        /// <param name="objectTypes">The object types created by the replication.</param>
        /// <param name="remove">True: The originally selected objects are deleted after the replication is complete.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ReplicateLinear(Coordinate3DCartesian offsets,
            int numberReplication,
            ref int numberOfObjects,
            ref string[] objectNames,
            ref eObjectType[] objectTypes,
            bool remove = false)
        {
            int[] csiObjectTypes = new int[0];

            _callCode = _sapModel.EditGeneral.ReplicateLinear(offsets.X, offsets.Y, offsets.Z,
                numberReplication, ref numberOfObjects, ref objectNames, ref csiObjectTypes, remove);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            objectTypes = csiObjectTypes.Cast<eObjectType>().ToArray();
        }

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
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ReplicateMirror(eAxisOrientation planeAxis,
            Coordinate3DCartesian planeAxisCoordinate1,
            Coordinate3DCartesian planeAxisCoordinate2, 
            Coordinate3DCartesian planeCoordinate,
            ref int numberOfObjects,
            ref string[] objectNames,
            ref eObjectType[] objectTypes,
            bool remove = false)
        {
            int[] csiObjectTypes = new int[0];

            _callCode = _sapModel.EditGeneral.ReplicateMirror((int)planeAxis,
                planeAxisCoordinate1.X, planeAxisCoordinate1.Y, planeAxisCoordinate1.Z,
                planeAxisCoordinate2.X, planeAxisCoordinate2.Y, planeAxisCoordinate2.Z,
                planeCoordinate.X, planeCoordinate.Y, planeCoordinate.Z,
                ref numberOfObjects, ref objectNames, ref csiObjectTypes, remove);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            objectTypes = csiObjectTypes.Cast<eObjectType>().ToArray();
        }


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
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ReplicateRadial(eAxisOrientation rotationAxis,
            Coordinate3DCartesian axisCoordinate,
            Coordinate3DCartesian axis3DCoordinate,
            int numberReplication,
            double incrementAngle,
            ref int numberOfObjects,
            ref string[] objectNames,
            ref eObjectType[] objectTypes,
            bool remove = false)
        {
            int[] csiObjectTypes = new int[0];
            _callCode = _sapModel.EditGeneral.ReplicateRadial((int)rotationAxis,
                axisCoordinate.X, axisCoordinate.Y, axisCoordinate.Z,
                axis3DCoordinate.X, axis3DCoordinate.Y, axis3DCoordinate.Z,
                numberReplication, incrementAngle, ref numberOfObjects, ref objectNames, ref csiObjectTypes, remove);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            objectTypes = csiObjectTypes.Cast<eObjectType>().ToArray();
        }
#endif

        #endregion
    }
}
