// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="SolidObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using System.Linq;
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Represents the solid object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISolidObject" />
    public class SolidObject : CSiApiBase, ISolidObject
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidObject" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public SolidObject(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Query
        /// <summary>
        /// This function returns the total number of defined solid properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.SolidObj.Count();
        }

        /// <summary>
        /// This function retrieves the names of all defined solid properties.
        /// </summary>
        /// <param name="names">Solid object names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.SolidObj.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="nameObject">The name of an existing object.</param>
        /// <param name="directionCosines">Value is an array of nine direction cosines that define the transformation matrix from the local coordinate system to the global coordinate system.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTransformationMatrix(string nameObject,
            out double[] directionCosines)
        {
            GetTransformationMatrix(nameObject, out directionCosines, isGlobal: true);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="nameObject">The name of an existing object.</param>
        /// <param name="directionCosines">Value is an array of nine direction cosines that define the transformation matrix from the local coordinate system to the global coordinate system.</param>
        /// <param name="isGlobal">True: Transformation matrix is between the Global coordinate system and the object local coordinate system.
        /// False: Transformation matrix is between the present coordinate system and the object local coordinate system.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTransformationMatrix(string nameObject,
            out double[] directionCosines,
            bool isGlobal)
        {
            directionCosines = new double[9];
            _callCode = _sapModel.PointObj.GetTransformationMatrix(nameObject, ref directionCosines, isGlobal);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of the point objects that define a solid object.
        /// </summary>
        /// <param name="name">The name of an existing solid object.</param>
        /// <param name="points">The names of the points that defined the solid object.
        /// The point names are listed in the positive order around the object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPoints(string name, 
            out string[] points)
        {
            points = new string[0];
            _callCode = _sapModel.SolidObj.GetPoints(name, ref points);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves the GUID for the specified object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="GUID">The GUID (Global Unique ID) for the specified object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGUID(string name,
            out string GUID)
        {
            GUID = string.Empty;
            GUID = string.Empty;
            _callCode = _sapModel.SolidObj.GetGUID(name, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the GUID for the specified object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="GUID">The GUID (Global Unique ID) for the specified object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetGUID(string name,
            string GUID = "")
        {
            _callCode = _sapModel.SolidObj.SetGUID(name, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves the name of the element (analysis model) associated with a specified object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="elementNames">The name of each element created from the specified object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetElement(string name,
            out string[] elementNames)
        {
            elementNames = new string[0];
            _callCode = _sapModel.SolidObj.GetElm(name, 
                ref _numberOfItems, 
                ref elementNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Axes
        /// <summary>
        /// This function retrieves the local axis angle assignment of the object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="angleOffset">The local axes of the solid object are defined by first setting the positive local 1, 2 and 3 axes the same as the positive global X, Y and Z axes and then doing the following: [deg]
        /// 1.    Rotate about the 3 axis by angle a.
        /// 2.    Rotate about the resulting 2 axis by angle b.
        /// 3.    Rotate about the resulting 1 axis by angle c.</param>
        /// <param name="isAdvanced">True object local axes orientation was obtained using advanced local axes parameters.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLocalAxes(string name,
            out AngleLocalAxes angleOffset,
            out bool isAdvanced)
        {
            double angleA = 0;
            double angleB = 0;
            double angleC = 0;
            isAdvanced = false;

            _callCode = _sapModel.SolidObj.GetLocalAxes(name, ref angleA, ref angleB, ref angleC, ref isAdvanced);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            angleOffset = new AngleLocalAxes
            {
                AngleA = angleA,
                AngleB = angleB,
                AngleC = angleC
            };
        }

        /// <summary>
        /// This function retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="angleOffset">The local axes of the solid object are defined by first setting the positive local 1, 2 and 3 axes the same as the positive global X, Y and Z axes and then doing the following: [deg]
        /// 1.    Rotate about the 3 axis by angle a.
        /// 2.    Rotate about the resulting 2 axis by angle b.
        /// 3.    Rotate about the resulting 1 axis by angle c.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignment is made to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignment is made to all objects in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, assignment is made to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLocalAxes(string name,
            AngleLocalAxes angleOffset,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetLocalAxes(name,
                            angleOffset.AngleA, angleOffset.AngleB, angleOffset.AngleC, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function gets the advanced local axes data for an existing object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="plane2">This is 12 or 13, indicating that the local plane determined by the plane reference vector is the 1-2 plane or the 1-3 plane.
        /// This item applies only when the <paramref name="isActive" /> = True.</param>
        /// <param name="axisVectorOption">Indicates the axis reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="axisCoordinateSystem">The coordinate system used to define the axis reference vector coordinate directions and the axis user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="axisVectorDirection">Indicates the axis reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction.</param>
        /// <param name="axisPoint">Indicates the labels of two joints that define the axis reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Two Joints.</param>
        /// <param name="axisReferenceVector">Defines the axis reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is User Vector.</param>
        /// <param name="localPlaneByReferenceVector">This is 12, 13, 21, 23, 31 or 32, indicating that the local plane determined by the plane reference vector is the 1-2, 1-3, 2-1, 2-3, 3-1, or 3-2 plane.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeVectorOption">Indicates the plane reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeCoordinateSystem">The coordinate system used to define the plane reference vector coordinate directions and the plane user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="planeVectorDirection">Indicates the plane reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction.</param>
        /// <param name="planePoint">Indicates the labels of two joints that define the plane reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Two Joints.</param>
        /// <param name="planeReferenceVector">Defines the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is User Vector.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLocalAxesAdvanced(string name,
            ref bool isActive,
            ref int plane2,
            ref eReferenceVector axisVectorOption,
            ref string axisCoordinateSystem,
            ref eReferenceVectorDirection[] axisVectorDirection,
            ref string[] axisPoint,
            ref double[] axisReferenceVector,
            ref int localPlaneByReferenceVector,
            ref eReferenceVector planeVectorOption,
            ref string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector)
        {
            int csiAxisVectorOption = 0;
            int[] csiAxisVectorDirection = new int[0];
            int csiPlaneVectorOption = 0;
            int[] csiPlaneVectorDirection = new int[0];

            _callCode = _sapModel.SolidObj.GetLocalAxesAdvanced(name, ref isActive,
                ref csiAxisVectorOption, ref axisCoordinateSystem, ref csiAxisVectorDirection, ref axisPoint, ref axisReferenceVector,
                ref plane2,
                ref csiPlaneVectorOption, ref planeCoordinateSystem, ref csiPlaneVectorDirection, ref planePoint, ref planeReferenceVector);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            planeVectorOption = (eReferenceVector)csiPlaneVectorOption;
            planeVectorDirection = csiPlaneVectorDirection.Cast<eReferenceVectorDirection>().ToArray();
        }

        /// <summary>
        /// This function sets the advanced local axes data for an existing object.
        /// </summary>
        /// <param name="name">The name of an existing object or group depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="plane2">This is 12 or 13, indicating that the local plane determined by the plane reference vector is the 1-2 plane or the 1-3 plane.
        /// This item applies only when the <paramref name="isActive" /> = True.</param>
        /// <param name="axisVectorOption">Indicates the axis reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="axisCoordinateSystem">The coordinate system used to define the axis reference vector coordinate directions and the axis user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="axisVectorDirection">Indicates the axis reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction.</param>
        /// <param name="axisPoint">Indicates the labels of two joints that define the axis reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Two Joints.</param>
        /// <param name="axisReferenceVector">Defines the axis reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is User Vector.</param>
        /// <param name="localPlaneByReferenceVector">This is 12, 13, 21, 23, 31 or 32, indicating that the local plane determined by the plane reference vector is the 1-2, 1-3, 2-1, 2-3, 3-1, or 3-2 plane.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeVectorOption">Indicates the plane reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeCoordinateSystem">The coordinate system used to define the plane reference vector coordinate directions and the plane user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="planeVectorDirection">Indicates the plane reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction.</param>
        /// <param name="planePoint">Indicates the labels of two joints that define the plane reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Two Joints.</param>
        /// <param name="planeReferenceVector">Defines the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is User Vector.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLocalAxesAdvanced(string name,
            bool isActive,
            int plane2,
            eReferenceVector axisVectorOption,
            string axisCoordinateSystem,
            eReferenceVectorDirection[] axisVectorDirection,
            string[] axisPoint,
            double[] axisReferenceVector,
            int localPlaneByReferenceVector,
            eReferenceVector planeVectorOption,
            string planeCoordinateSystem,
            eReferenceVectorDirection[] planeVectorDirection,
            string[] planePoint,
            double[] planeReferenceVector,
            eItemType itemType = eItemType.Object)
        {
            arraysLengthMatch(nameof(axisVectorDirection), axisVectorDirection.Length, nameof(axisPoint), axisPoint.Length);
            arraysLengthMatch(nameof(axisVectorDirection), axisVectorDirection.Length, nameof(axisReferenceVector), axisReferenceVector.Length);

            arraysLengthMatch(nameof(planeVectorDirection), planeVectorDirection.Length, nameof(planePoint), planePoint.Length);
            arraysLengthMatch(nameof(planeVectorDirection), planeVectorDirection.Length, nameof(planeReferenceVector), planeReferenceVector.Length);

            int[] csiAxisVectorDirectionn = axisVectorDirection.Cast<int>().ToArray();
            int[] csiPlaneVectorDirection = planeVectorDirection.Cast<int>().ToArray();

            _callCode = _sapModel.SolidObj.SetLocalAxesAdvanced(name, isActive,
                            (int)axisVectorOption, axisCoordinateSystem, ref csiAxisVectorDirectionn, ref axisPoint, ref axisReferenceVector,
                            plane2,
                            (int)planeVectorOption, planeCoordinateSystem, ref csiPlaneVectorDirection, ref planePoint, ref planeReferenceVector,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Creation & Groups
        /// <summary>
        /// This function changes the name of an existing solid object.
        /// </summary>
        /// <param name="currentName">The existing name of a defined solid object.</param>
        /// <param name="newName">The new name for the solid object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, string newName)
        {
            _callCode = _sapModel.SolidObj.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// The function deletes a specified solid object.
        /// It returns an error if the specified object can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing solid object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.SolidObj.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function adds a new object whose corner points are at the specified coordinates.
        /// </summary>
        /// <param name="coordinates">Coordinates for the corner points of the object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.
        /// Eight coordinates are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <exception cref="CSiException">
        /// Eight coordinates must be provided for a solid object. " +
        ///                                        "Provided: " + coordinates.Length
        /// or
        /// API_DEFAULT_ERROR_CODE</exception>
        public void AddByCoordinate(ref Coordinate3DCartesian[] coordinates,
            ref string name,
            string nameProperty = "Default",
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
            if (coordinates.Length != 8)
            {
                throw new CSiException("Eight coordinates must be provided for a solid object. " +
                                       "Provided: " + coordinates.Length);
            }

            double[] xCoordinates = new double[coordinates.Length - 1];
            double[] yCoordinates = new double[coordinates.Length - 1];
            double[] zCoordinates = new double[coordinates.Length - 1];
            for (int i = 0; i < coordinates.Length; i++)
            {
                xCoordinates[i] = coordinates[i].X;
                yCoordinates[i] = coordinates[i].Y;
                zCoordinates[i] = coordinates[i].Z;
            }


            _callCode = _sapModel.SolidObj.AddByCoord(
                ref xCoordinates, ref yCoordinates, ref zCoordinates,
                ref name, nameProperty, userName, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function adds a new object whose corner points are specified by name.
        /// </summary>
        /// <param name="pointNames">Names of the point objects that define the corner points of the added object.
        /// Eight points are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <exception cref="CSiException">
        /// Eight points must be provided for a solid object. " +
        ///                                        "Provided: " + pointNames.Length
        /// or
        /// API_DEFAULT_ERROR_CODE</exception>
        public void AddByPoint(string[] pointNames,
            ref string name,
            string nameProperty = "Default",
            string userName = "")
        {
            if (pointNames.Length != 8)
            {
                throw new CSiException("Eight points must be provided for a solid object. " +
                                       "Provided: " + pointNames.Length);
            }

            _callCode = _sapModel.SolidObj.AddByPoint(ref pointNames, ref name, nameProperty, userName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function adds or removes objects from a specified group.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="groupName">The name of an existing group to which the assignment is made.</param>
        /// <param name="remove">False: The specified objects are added to the group specified by the <paramref name="groupName" /> item.
        /// True: The objects are removed from the group.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetGroupAssign(string name,
            string groupName,
            bool remove = false,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetGroupAssign(name, 
                            groupName, remove, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if BUILD_SAP2000v20
        /// <summary>
        /// This function retrieves the names of the groups to which a specified object is assigned.
        /// </summary>
        /// <param name="objectName">The name of an existing object.</param>
        /// <param name="groupNames">The names of the groups to which the object is assigned.</param>
        public void GetGroupAssign(string objectName,
            out string[] groupNames)
        {
            groupNames = new string[0];
            _callCode = _sapModel.SolidObj.GetGroupAssign(objectName,
                           ref _numberOfItems, ref groupNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        #endregion

        #region Selection
        /// <summary>
        /// This function retrieves the selected status for an object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="isSelected">True: The specified object is selected.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSelected(string name,
            out bool isSelected)
        {
            isSelected = false;
            _callCode = _sapModel.SolidObj.GetSelected(name, ref isSelected);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the selected status for an object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="isSelected">True: The specified object is selected.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the selected status is set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the selected status is set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the selected status is set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSelected(string name,
            bool isSelected,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetSelected(name, 
                            isSelected, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// This function retrieves the section property assigned to a solid object.
        /// </summary>
        /// <param name="name">The name of a defined solid object.</param>
        /// <param name="propertyName">The name of the section property assigned to the solid object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSection(string name, 
            out string propertyName)
        {
            propertyName = string.Empty;
            _callCode = _sapModel.SolidObj.GetProperty(name, ref propertyName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns the section property to a solid object.
        /// </summary>
        /// <param name="name">The name of a defined solid object.</param>
        /// <param name="propertyName">The name of the section property assigned to the solid object.</param>
        /// <param name="itemType">If this item is Object, the assignment is made to the solid object specified by the Name item.
        /// If this item is Group, the assignment is made to all solid objects in the group specified by the Name item.
        /// If this item is SelectedObjects, assignment is made to all selected solid objects, and the Name item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSection(string name, 
            string propertyName, 
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetProperty(name, 
                            propertyName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves the material temperature assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element.</param>
        /// <param name="temperature">This is the material temperature value assigned to the element. [T]</param>
        /// <param name="patternName">This is blank or the name of a defined joint pattern.
        /// If it is blank, the material temperature for the line element is uniform along the element at the value specified by <paramref name="temperature" />.
        /// If <paramref name="patternName"/> is the name of a defined joint pattern, the material temperature for the line element may vary from one end to the other.
        /// The material temperature at each end of the element is equal to the specified temperature multiplied by the pattern value at the joint at the end of the line element.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMaterialTemperature(string name,
            out double temperature,
            out string patternName)
        {
            temperature = 0;
            patternName = string.Empty;

            _callCode = _sapModel.SolidObj.GetMatTemp(name, ref temperature, ref patternName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the material temperature assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element.</param>
        /// <param name="temperature">This is the material temperature value assigned to the element. [T]</param>
        /// <param name="patternName">This is blank or the name of a defined joint pattern.
        /// If it is blank, the material temperature for the line element is uniform along the element at the value specified by <paramref name="temperature" />.
        /// If <paramref name="patternName"/> is the name of a defined joint pattern, the material temperature for the line element may vary from one end to the other.
        /// The material temperature at each end of the element is equal to the specified temperature multiplied by the pattern value at the joint at the end of the line element.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMaterialTemperature(string name,
            double temperature,
            string patternName,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetMatTemp(name, 
                            temperature, 
                            patternName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion

        #region Solid Properties
        /// <summary>
        /// This function retrieves automatic meshing assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="meshType">The automatic mesh type for the object.</param>
        /// <param name="numberOfObjectsAlongPoint12">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 2.</param>
        /// <param name="numberOfObjectsAlongPoint13">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 3.</param>
        /// <param name="numberOfObjectsAlongPoint15">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 5.</param>
        /// <param name="maxSizeOfObjectsAlongPoint12">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 2 [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric..</param>
        /// <param name="maxSizeOfObjectsAlongPoint13">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 3. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.</param>
        /// <param name="maxSizeOfObjectsAlongPoint15">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 5. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.</param>
        /// <param name="restraintsOnEdge">True: If both points along an edge of the original object have the same restraint/constraint, then, if an added point on that edge and the original corner points have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="restraintsOnFace">True: If all corner points on an object face have the same restraint/constraint, then, if an added point on that face and the original corner points for the face have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAutoMesh(string name,
            ref eMeshType meshType,
            ref int numberOfObjectsAlongPoint12,
            ref int numberOfObjectsAlongPoint13,
            ref int numberOfObjectsAlongPoint15,
            ref double maxSizeOfObjectsAlongPoint12,
            ref double maxSizeOfObjectsAlongPoint13,
            ref double maxSizeOfObjectsAlongPoint15,
            ref bool restraintsOnEdge,
            ref bool restraintsOnFace)
        {
            int csiMeshType = 0;

            _callCode = _sapModel.SolidObj.GetAutoMesh(name, ref csiMeshType, ref numberOfObjectsAlongPoint12, ref numberOfObjectsAlongPoint13, ref numberOfObjectsAlongPoint15, ref maxSizeOfObjectsAlongPoint12, ref maxSizeOfObjectsAlongPoint13, ref maxSizeOfObjectsAlongPoint15, ref restraintsOnEdge, ref restraintsOnFace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            meshType = (eMeshType) csiMeshType;
        }

        /// <summary>
        /// This function makes automatic meshing assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="meshType">The automatic mesh type for the object.</param>
        /// <param name="numberOfObjectsAlongPoint12">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 2.</param>
        /// <param name="numberOfObjectsAlongPoint13">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 3.</param>
        /// <param name="numberOfObjectsAlongPoint15">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 5.</param>
        /// <param name="maxSizeOfObjectsAlongPoint12">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 2. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.</param>
        /// <param name="maxSizeOfObjectsAlongPoint13">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 3. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.</param>
        /// <param name="maxSizeOfObjectsAlongPoint15">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 5. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.</param>
        /// <param name="restraintsOnEdge">True: If both points along an edge of the original object have the same restraint/constraint, then, if an added point on that edge and the original corner points have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="restraintsOnFace">True: If all corner points on an object face have the same restraint/constraint, then, if an added point on that face and the original corner points for the face have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetAutoMesh(string name,
            eMeshType meshType,
            int numberOfObjectsAlongPoint12 = 2,
            int numberOfObjectsAlongPoint13 = 2,
            int numberOfObjectsAlongPoint15 = 2,
            double maxSizeOfObjectsAlongPoint12 = 0,
            double maxSizeOfObjectsAlongPoint13 = 0,
            double maxSizeOfObjectsAlongPoint15 = 0,
            bool restraintsOnEdge = false,
            bool restraintsOnFace = false,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetAutoMesh(name, 
                            (int)meshType, 
                            numberOfObjectsAlongPoint12, numberOfObjectsAlongPoint13, numberOfObjectsAlongPoint15, 
                            maxSizeOfObjectsAlongPoint12, maxSizeOfObjectsAlongPoint13, maxSizeOfObjectsAlongPoint15, 
                            restraintsOnEdge, restraintsOnFace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Support & Connections
        /// <summary>
        /// This function retrieves the spring assignments to an object face.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="springTypes">The spring property type.</param>
        /// <param name="stiffnesses">Simple spring stiffness per unit area of the specified object face. [F/L^3]
        /// This item applies only when <paramref name="springTypes" /> = <see cref="eSpringType.Simple" />.</param>
        /// <param name="springSimpleTypes">The simple spring type.
        /// This item applies only when <paramref name="springTypes" /> = <see cref="eSpringType.Simple" />.</param>
        /// <param name="linkProperties">The name of the link property assigned to the spring.
        /// This item applies only when <paramref name="springTypes" /> = <see cref="eSpringType.Link" />.</param>
        /// <param name="faces">Indicates the object face to which the specified spring assignment applies.</param>
        /// <param name="springLocalOneTypes">Indicates the method used to specify the spring positive local 1-axis orientation.</param>
        /// <param name="directions">This is 1, 2, 3, -1, -2 or -3, indicating the object local axis that corresponds to the positive local 1-axis of the spring.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.Parallel" />.</param>
        /// <param name="areOutward">True: The spring positive local 1 axis is outward from the specified object face.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.Normal" />.</param>
        /// <param name="vectorComponentsX">Each value in this array is the X-axis or object local 1-axis component (depending on the <paramref name="coordinateSystems" /> specified) of the user specified direction vector for the spring local 1-axis.
        /// The direction vector is in the coordinate system specified by the <paramref name="coordinateSystems" /> item.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <param name="vectorComponentsY">Each value in this array is the Y-axis or object local 2-axis component (depending on the <paramref name="coordinateSystems" /> specified) of the user specified direction vector for the spring local 1-axis.
        /// The direction vector is in the coordinate system specified by the <paramref name="coordinateSystems" /> item.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <param name="vectorComponentsZ">Each value in this array is the Z-axis or object local 3-axis component (depending on the <paramref name="coordinateSystems" /> specified) of the user specified direction vector for the spring local 1-axis.
        /// The direction vector is in the coordinate system specified by the <paramref name="coordinateSystems" /> item.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <param name="angleOffsets">This is the angle that the link local 2-axis is rotated from its default orientation. [deg]
        /// This item applies only when <paramref name="springTypes" /> = <see cref="eSpringType.Link" />.</param>
        /// <param name="coordinateSystems">This is Local (meaning the solid object local coordinate system) or the name of a defined coordinate system.
        /// This item is the coordinate system in which the user specified direction vector is specified.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSpring(string name,
            ref eSpringType[] springTypes,
            ref double[] stiffnesses,
            ref eSpringSimpleType[] springSimpleTypes,
            ref string[] linkProperties,
            ref eFace[] faces,
            ref eSpringLocalOneType[] springLocalOneTypes,
            ref int[] directions,
            ref bool[] areOutward,
            ref double[] vectorComponentsX,
            ref double[] vectorComponentsY,
            ref double[] vectorComponentsZ,
            ref double[] angleOffsets,
            ref string[] coordinateSystems)
        {
            int[] csiSpringTypes = new int[0];
            int[] csiSpringSimpleTypes = new int[0];
            int[] csiFace = new int[0];
            int[] csiSpringLocalOneTypes = new int[0];

            _callCode = _sapModel.SolidObj.GetSpring(name, ref _numberOfItems, ref csiSpringTypes, ref stiffnesses, ref csiSpringSimpleTypes, ref linkProperties, ref csiFace, ref csiSpringLocalOneTypes, ref directions, ref areOutward, ref vectorComponentsX, ref vectorComponentsY, ref vectorComponentsZ, ref coordinateSystems, ref angleOffsets);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            springTypes = csiSpringTypes.Cast<eSpringType>().ToArray();
            springSimpleTypes = csiSpringSimpleTypes.Cast<eSpringSimpleType>().ToArray();
            faces = csiFace.Cast<eFace>().ToArray();
            springLocalOneTypes = csiSpringLocalOneTypes.Cast<eSpringLocalOneType>().ToArray();
        }

        /// <summary>
        /// This function makes spring assignments to objects.
        /// The springs are assigned to a specified object face.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="springType">The spring property type.</param>
        /// <param name="stiffness">Simple spring stiffness per unit area of the specified object face. [F/L^3]
        /// This item applies only when <paramref name="springType" /> = <see cref="eSpringType.Simple" />.</param>
        /// <param name="springSimpleType">The simple spring type.
        /// This item applies only when <paramref name="springType" /> = <see cref="eSpringType.Simple" />.</param>
        /// <param name="linkProperty">The name of the link property assigned to the spring.
        /// This item applies only when <paramref name="springType" /> = <see cref="eSpringType.Link" />.</param>
        /// <param name="face">Indicates the object face to which the specified spring assignment applies.</param>
        /// <param name="springLocalOneType">Indicates the method used to specify the spring positive local 1-axis orientation.</param>
        /// <param name="direction">This is 1, 2, 3, -1, -2 or -3, indicating the object local axis that corresponds to the positive local 1-axis of the spring.
        /// This item applies only when <paramref name="springLocalOneType" /> = <see cref="eSpringLocalOneType.Parallel" />.</param>
        /// <param name="isOutward">True: The spring positive local 1 axis is outward from the specified object face.
        /// This item applies only when <paramref name="springLocalOneType" /> = <see cref="eSpringLocalOneType.Normal" />.</param>
        /// <param name="vector">The direction vector of the spring positive local 1-axis.
        /// The direction vector is in the coordinate system specified by the <paramref name="coordinateSystem" /> item.
        /// This item applies only when <paramref name="springLocalOneType" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <param name="angleOffset">This is the angle that the link local 2-axis is rotated from its default orientation. [deg]
        /// This item applies only when <paramref name="springType" /> = <see cref="eSpringType.Link" />.</param>
        /// <param name="replace">True: All existing spring assignments to the object are removed before assigning the specified spring.
        /// False: The specified spring is added to any existing springs already assigned to the object.</param>
        /// <param name="coordinateSystem">This is Local (meaning the solid object local coordinate system) or the name of a defined coordinate system.
        /// This item is the coordinate system in which the user specified direction vector, <paramref name="vector" />, is specified.
        /// This item applies only when <paramref name="springLocalOneType" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSpring(string name,
            eSpringType springType,
            double stiffness,
            eSpringSimpleType springSimpleType,
            string linkProperty,
            eFace face,
            eSpringLocalOneType springLocalOneType,
            int direction,
            bool isOutward,
            Coordinate3DCartesian vector,
            double angleOffset,
            bool replace,
            string coordinateSystem = CoordinateSystems.Local,
            eItemType itemType = eItemType.Object)
        {
            double[] csiVector = vector.ToArray();

            _callCode = _sapModel.SolidObj.SetSpring(name, 
                            (int)springType, stiffness, (int)springSimpleType, linkProperty, (int)face, (int)springLocalOneType, 
                            direction, isOutward, ref csiVector, angleOffset, replace, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes all spring assignments for the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteSpring(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.DeleteSpring(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the generated edge constraint assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="constraintExists">True: An automatic edge constraint is generated by the program for the object in the analysis model.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetEdgeConstraint(string name,
            ref bool constraintExists)
        {
            _callCode = _sapModel.SolidObj.GetEdgeConstraint(name, ref constraintExists);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function makes generated edge constraint assignments to solid objects.
        /// </summary>
        /// <param name="name">The name of an existing solid object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="constraintExists">True: An automatic edge constraint is generated by the program for the object in the analysis model.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetEdgeConstraint(string name,
            bool constraintExists,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetEdgeConstraint(name, 
                            constraintExists, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Loads
        // LoadGravity
        /// <summary>
        /// This function retrieves the gravity load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of gravity loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each gravity load.</param>
        /// <param name="loadPatterns">The name of the coordinate system in which the gravity load multipliers are specified.</param>
        /// <param name="xLoadMultiplier">Gravity load multipliers in the x direction of the specified coordinate system.</param>
        /// <param name="yLoadMultiplier">Gravity load multipliers in the y direction of the specified coordinate system.</param>
        /// <param name="zLoadMultiplier">Gravity load multipliers in the z direction of the specified coordinate system.</param>
        /// <param name="coordinateSystems">The name of the coordinate system associated with each gravity load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadGravity(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] xLoadMultiplier,
            ref double[] yLoadMultiplier,
            ref double[] zLoadMultiplier,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.GetLoadGravity(name, ref numberItems, 
                            ref names, ref loadPatterns, ref coordinateSystems, 
                            ref xLoadMultiplier, ref yLoadMultiplier, ref zLoadMultiplier, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns gravity loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the gravity load.</param>
        /// <param name="xLoadMultiplier">Gravity load multiplier in the x direction of the specified coordinate system.</param>
        /// <param name="yLoadMultiplier">Gravity load multiplier in the y direction of the specified coordinate system.</param>
        /// <param name="zLoadMultiplier">Gravity load multiplier in the z direction of the specified coordinate system.</param>
        /// <param name="coordinateSystem">The name of the coordinate system associated with the gravity load.</param>
        /// <param name="replace">True: All previous gravity loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadGravity(string name,
            string loadPattern,
            double xLoadMultiplier,
            double yLoadMultiplier,
            double zLoadMultiplier,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetLoadGravity(name, 
                            loadPattern, xLoadMultiplier, yLoadMultiplier, zLoadMultiplier, replace, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the gravity load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadGravity(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.DeleteLoadGravity(name,
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        // LoadPorePressure
        /// <summary>
        /// This function retrieves the pore pressure load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="numberItems">The total number of pore pressure loads retrieved for the specified objects.</param>
        /// <param name="names">The name of the object associated with each pore pressure load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each pore pressure load.</param>
        /// <param name="porePressureLoadValues">The pore pressure values. [F/L^2]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify the pore pressure load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadPorePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] porePressureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.GetLoadPorePressure(name, ref numberItems, 
                            ref names, ref loadPatterns, ref porePressureLoadValues, ref jointPatternNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function assigns pore pressure loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the pore pressure load.</param>
        /// <param name="porePressureLoadValue">The pore pressure values. [F/L^2]</param>
        /// <param name="jointPatternName">The joint pattern name, if any, used to specify the pore pressure pressure load.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadPorePressure(string name,
            string loadPattern,
            double porePressureLoadValue,
            string jointPatternName,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetLoadPorePressure(name, 
                            loadPattern, porePressureLoadValue, jointPatternName, replace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the pore pressure load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadPorePressure(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.DeleteLoadPorePressure(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        // LoadSurfacePressure
        /// <summary>
        /// This function retrieves the surface pressure assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, object or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of surface pressure loads retrieved for the specified objects.</param>
        /// <param name="names">The name of the object associated with each surface pressure load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each surface pressure load.</param>
        /// <param name="faceApplied">The object face to which each specified load assignment applies.
        /// Note that edge face n is from plane object point n to plane object point n + 1. For example, edge face 2 is from plane object point 2 to plane object point 3.</param>
        /// <param name="surfacePressureLoadValues">The surface pressure values. [F/L^2]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify each surface pressure load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadSurfacePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eFace[] faceApplied,
            ref double[] surfacePressureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
            int[] csiFaceApplied = new int[0];

            _callCode = _sapModel.SolidObj.GetLoadSurfacePressure(name, ref numberItems, 
                            ref names, ref loadPatterns, ref csiFaceApplied, ref surfacePressureLoadValues, ref jointPatternNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            faceApplied = new eFace[csiFaceApplied.Length - 1];
            for (int i = 0; i < csiFaceApplied.Length; i++)
            {
                faceApplied[i] = (eFace)csiFaceApplied[i];
            }
        }


        /// <summary>
        /// This function assigns surface pressure loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the surface pressure load.</param>
        /// <param name="faceApplied">The element face to which the specified load assignment applies.
        /// Note that edge face n is from plane element point n to plane element point n + 1. For example, edge face 2 is from plane element point 2 to plane element point 3.</param>
        /// <param name="surfacePressureLoadValue">The surface pressure values. [F/L^2]</param>
        /// <param name="jointPatternName">The joint pattern name, if any, used to specify the surface pressure load.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadSurfacePressure(string name,
            string loadPattern,
            eFace faceApplied,
            double surfacePressureLoadValue,
            string jointPatternName,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetLoadSurfacePressure(name, 
                            loadPattern, (int)faceApplied, surfacePressureLoadValue, jointPatternName, replace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the surface pressure load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadSurfacePressure(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.DeleteLoadSurfacePressure(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // LoadStrain
        /// <summary>
        /// This function retrieves the strain load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of strain loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each strain load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each strain load.</param>
        /// <param name="components">Indicates the strain component associated with each strain load.</param>
        /// <param name="strainLoadValues">The strain values. [L/L]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify each strain load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadStrain(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eStrainComponent[] components,
            ref double[] strainLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
            int[] csiComponents = new int[0];

            _callCode = _sapModel.SolidObj.GetLoadStrain(name, ref numberItems, 
                            ref names, ref loadPatterns, ref csiComponents, ref strainLoadValues, ref jointPatternNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            components = csiComponents.Cast<eStrainComponent>().ToArray();
        }

        /// <summary>
        /// This function assigns strain loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the strain load.</param>
        /// <param name="component">Indicates the strain component associated with the strain load.</param>
        /// <param name="strainLoadValue">The strain value. [L/L]</param>
        /// <param name="jointPatternName">The joint pattern name, if any, used to specify the strain load.</param>
        /// <param name="replace">True: All previous strain loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadStrain(string name,
            string loadPattern,
            eStrainComponent component,
            double strainLoadValue,
            string jointPatternName,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetLoadStrain(name, 
                            loadPattern, (int)component, strainLoadValue, replace, jointPatternName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the strain load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="component">Indicates the strain component associated with the strain load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadStrain(string name,
            string loadPattern,
            eStrainComponent component,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.DeleteLoadStrain(name, 
                            loadPattern, (int)component, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // LoadTemperature
        /// <summary>
        /// This function retrieves the temperature load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="numberItems">The total number of temperature loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each temperature load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each temperature load.</param>
        /// <param name="temperatureLoadValues">Temperature load values, [T].</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify the temperature load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadTemperature(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] temperatureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.GetLoadTemperature(name, ref numberItems, 
                            ref names, ref loadPatterns, ref temperatureLoadValues, ref jointPatternNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing element or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the temperature load.</param>
        /// <param name="temperatureLoadValue">Temperature load value, [T].</param>
        /// <param name="jointPatternName">The joint pattern name, if any, used to specify the temperature load.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadTemperature(string name,
            string loadPattern,
            double temperatureLoadValue,
            string jointPatternName,
            bool replace,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.SetLoadTemperature(name, 
                            loadPattern, temperatureLoadValue, jointPatternName, replace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the temperature load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadTemperature(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.SolidObj.DeleteLoadTemperature(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#endregion
    }
}
#endif