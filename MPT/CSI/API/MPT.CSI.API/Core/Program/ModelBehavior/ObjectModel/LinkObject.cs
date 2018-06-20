// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="LinkObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
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
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Represents the link object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILinkObject" />
    public class LinkObject : CSiApiBase, ILinkObject
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkObject" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public LinkObject(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Query
        /// <summary>
        /// This function returns the total number of defined link properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.LinkObj.Count();
        }

        /// <summary>
        /// This function retrieves the names of all defined link properties.
        /// </summary>
        /// <param name="names">Link object names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.LinkObj.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the names of all defined link object properties for a given story.
        /// </summary>
        /// <param name="storyName">Name of the story to filter the link object names by.</param>
        /// <param name="names">Link object object names retrieved by the program.</param>
        public void GetNameListOnStory(string storyName,
                       out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.LinkObj.GetNameListOnStory(storyName, ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
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
            _callCode = _sapModel.LinkObj.GetTransformationMatrix(nameObject, ref directionCosines, isGlobal);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of the point objects that define a link object.
        /// </summary>
        /// <param name="name">The name of an existing link object.</param>
        /// <param name="points">The names of the points that defined the link object.
        /// The point names are listed in the positive order around the object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPoints(string name, 
            out string[] points)
        {
            string point1 = "";
            string point2 = "";

            _callCode = _sapModel.LinkObj.GetPoints(name, ref point1, ref point2);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            points = new string[2];
            points[0] = point1;
            points[1] = point2;
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
            _callCode = _sapModel.LinkObj.GetGUID(name, ref GUID);
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
            _callCode = _sapModel.LinkObj.SetGUID(name, GUID);
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
            string csiElementName = "";
            _callCode = _sapModel.LinkObj.GetElm(name, 
                ref csiElementName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            elementNames = new string[1];
            elementNames[0] = csiElementName;
        }
        #endregion

        #region Axes
        /// <summary>
        /// This function retrieves the local axis angle assignment of the object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]</param>
        /// <param name="isAdvanced">True object local axes orientation was obtained using advanced local axes parameters.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLocalAxes(string name,
            out AngleLocalAxes angleOffset,
            out bool isAdvanced)
        {
            isAdvanced = false;
            double angle = 0;

            _callCode = _sapModel.LinkObj.GetLocalAxes(name, ref angle, ref isAdvanced);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            angleOffset = new AngleLocalAxes {AngleA = angle};
        }

        /// <summary>
        /// This function retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 2 and 3 axes are rotated about the positive local 1 axis, from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +1 axis is pointing toward you. [deg]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignment is made to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignment is made to all objects in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, assignment is made to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLocalAxes(string name,
            AngleLocalAxes angleOffset,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.LinkObj.SetLocalAxes(name, 
                            angleOffset.AngleA, 
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

            _callCode = _sapModel.LinkObj.GetLocalAxesAdvanced(name, ref isActive,  
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

            _callCode = _sapModel.LinkObj.SetLocalAxesAdvanced(name, isActive, 
                            (int)axisVectorOption, axisCoordinateSystem, ref csiAxisVectorDirectionn, ref axisPoint, ref axisReferenceVector,
                            plane2,
                            (int)planeVectorOption, planeCoordinateSystem, ref csiPlaneVectorDirection, ref planePoint, ref planeReferenceVector, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Creation & Groups
        /// <summary>
        /// This function changes the name of an existing link object.
        /// </summary>
        /// <param name="currentName">The existing name of a defined link object.</param>
        /// <param name="newName">The new name for the link object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, string newName)
        {
            _callCode = _sapModel.LinkObj.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// The function deletes a specified link object.
        /// It returns an error if the specified object can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing link object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.LinkObj.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function adds a new object whose corner points are at the specified coordinates.
        /// </summary>
        /// <param name="coordinates">Coordinates for the corner points of the object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.
        /// Two coordinates are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        public void AddByCoordinate(ref Coordinate3DCartesian[] coordinates,
            ref string name,
            string nameProperty = "Default",
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
            bool isSingleJoint = false;
            AddByCoordinate(ref coordinates, ref name, isSingleJoint, nameProperty, userName, coordinateSystem);
        }

        /// <summary>
        /// This function adds a new link object whose corner points are at the specified coordinates.
        /// </summary>
        /// <param name="coordinates">Coordinates for the corner points of the object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.
        /// Two coordinates are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="isSingleJoint">True: A one-joint link is added.
        /// False: A two-joint link is added.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <exception cref="CSiException">
        /// Only one coordinate may be provided for a single joint link object. " +
        ///                                            "Provided: " + coordinates.Length
        /// or
        /// Two coordinates must be provided for a two-joint link object. " +
        ///                                            "Provided: " + coordinates.Length
        /// or
        /// API_DEFAULT_ERROR_CODE</exception>
        public void AddByCoordinate(ref Coordinate3DCartesian[] coordinates,
            ref string name,
            bool isSingleJoint,
            string nameProperty = "Default",
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
            if (isSingleJoint)
            {
                if (coordinates.Length != 1)
                {
                    throw new CSiException("Only one coordinate may be provided for a single joint link object. " +
                                           "Provided: " + coordinates.Length);
                }
            }
            else
            {
                if (coordinates.Length != 2)
                {
                    throw new CSiException("Two coordinates must be provided for a two-joint link object. " +
                                           "Provided: " + coordinates.Length);
                }
            }
            
            _callCode = _sapModel.LinkObj.AddByCoord(
                coordinates[0].X, coordinates[0].Y, coordinates[0].Z,
                coordinates[1].X, coordinates[1].Y, coordinates[1].Z,
                ref name, isSingleJoint, nameProperty, userName, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function adds a new object whose corner points are specified by name.
        /// </summary>
        /// <param name="pointNames">Names of the point objects that define the corner points of the added object.
        /// Two points are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        public void AddByPoint(string[] pointNames,
            ref string name,
            string nameProperty = "Default",
            string userName = "")
        {
            bool isSingleJoint = false;
            AddByPoint(pointNames, ref name, isSingleJoint, nameProperty, userName);
        }

        /// <summary>
        /// This function adds a new link object whose corner points are specified by name.
        /// </summary>
        /// <param name="pointNames">Names of the point objects that define the corner points of the added object.
        /// Two points are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="isSingleJoint">True: A one-joint link is added.
        /// False: A two-joint link is added.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <exception cref="CSiException">
        /// Only one point may be provided for a single joint link object. " +
        ///                                            "Provided: " + pointNames.Length
        /// or
        /// Two points must be provided for a two-joint link object. " +
        ///                                            "Provided: " + pointNames.Length
        /// or
        /// API_DEFAULT_ERROR_CODE</exception>
        public void AddByPoint(string[] pointNames,
            ref string name,
            bool isSingleJoint,
            string nameProperty = "Default",
            string userName = "")
        {
            if (isSingleJoint)
            {
                if (pointNames.Length != 1)
                {
                    throw new CSiException("Only one point may be provided for a single joint link object. " +
                                           "Provided: " + pointNames.Length);
                }
            }
            else
            {
                if (pointNames.Length != 2)
                {
                    throw new CSiException("Two points must be provided for a two-joint link object. " +
                                           "Provided: " + pointNames.Length);
                }
            }

            _callCode = _sapModel.LinkObj.AddByPoint(pointNames[0], pointNames[1], ref name, isSingleJoint, nameProperty, userName);
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
            _callCode = _sapModel.LinkObj.SetGroupAssign(name, 
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
            _callCode = _sapModel.LinkObj.GetGroupAssign(objectName,
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
            _callCode = _sapModel.LinkObj.GetSelected(name, ref isSelected);
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
            _callCode = _sapModel.LinkObj.SetSelected(name, 
                            isSelected, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// This function retrieves the section property assigned to a link object.
        /// </summary>
        /// <param name="name">The name of a defined link object.</param>
        /// <param name="propertyName">The name of the section property assigned to the link object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSection(string name, 
            out string propertyName)
        {
            propertyName = string.Empty;
            _callCode = _sapModel.LinkObj.GetProperty(name, ref propertyName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns the section property to a link object.
        /// </summary>
        /// <param name="name">The name of a defined link object.</param>
        /// <param name="propertyName">The name of the section property assigned to the link object.</param>
        /// <param name="itemType">If this item is Object, the assignment is made to the link object specified by the Name item.
        /// If this item is Group, the assignment is made to all link objects in the group specified by the Name item.
        /// If this item is SelectedObjects, assignment is made to all selected link objects, and the Name item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSection(string name, 
            string propertyName, 
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.LinkObj.SetProperty(name, 
                            propertyName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the frequency dependent property assignment to a link element.
        /// If no frequency dependent property is assigned to the link, the PropName is returned as None.
        /// </summary>
        /// <param name="name">The name of an existing link element.</param>
        /// <param name="propertyName">The name of the frequency dependent link property assigned to the link element.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSectionFrequencyDependent(string name,
            ref string propertyName)
        {
            _callCode = _sapModel.LinkObj.GetPropertyFD(name, ref propertyName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns the frequency dependent property assignment to a link element.
        /// </summary>
        /// <param name="name">The name of an existing link object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="propertyName">The name of the frequency dependent link property assigned to the link element.
        /// None means that no frequency dependent link property is assigned to the link object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSectionFrequencyDependent(string name,
            string propertyName,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.LinkObj.SetPropertyFD(name, 
                            propertyName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Loads
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
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
            _callCode = _sapModel.LinkObj.GetLoadGravity(name, ref numberItems, 
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
            _callCode = _sapModel.LinkObj.SetLoadGravity(name, 
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
            _callCode = _sapModel.LinkObj.DeleteLoadGravity(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // LoadDeformation
        /// <summary>
        /// This function retrieves the deformation load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of deformation loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each deformation load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each deformation load.</param>
        /// <param name="degreesOfFreedom">Indicates if the considered degree of freedom has a deformation load.</param>
        /// <param name="deformations">Deformation load values.
        /// The deformations specified for a given degree of freedom are applicable only if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadDeformation(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref DegreesOfFreedomLocal[] degreesOfFreedom,
            ref Deformations[] deformations,
            eItemType itemType = eItemType.Object)
        {
            bool[] dof1 = new bool[0];
            bool[] dof2 = new bool[0];
            bool[] dof3 = new bool[0];
            bool[] dof4 = new bool[0];
            bool[] dof5 = new bool[0];
            bool[] dof6 = new bool[0];

            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.LinkObj.GetLoadDeformation(name, ref numberItems, ref names, ref loadPatterns,
                            ref dof1, ref dof2, ref dof3, ref dof4, ref dof5, ref dof6,
                            ref U1, ref U2, ref U3, ref R1, ref R2, ref R3,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom = new DegreesOfFreedomLocal[numberItems];
            deformations = new Deformations[numberItems];
            for (int i = 0; i < numberItems; i++)
            {
                degreesOfFreedom[i].U1 = dof1[i];
                degreesOfFreedom[i].U2 = dof2[i];
                degreesOfFreedom[i].U3 = dof3[i];
                degreesOfFreedom[i].R1 = dof4[i];
                degreesOfFreedom[i].R2 = dof5[i];
                degreesOfFreedom[i].R3 = dof6[i];

                deformations[i].U1 = U1[i];
                deformations[i].U2 = U2[i];
                deformations[i].U3 = U3[i];
                deformations[i].R1 = R1[i];
                deformations[i].R2 = R2[i];
                deformations[i].R3 = R3[i];
            }
        }

        /// <summary>
        /// This function assigns deformation loads to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="degreesOfFreedom">Indicates if the considered degree of freedom has a deformation load.</param>
        /// <param name="deformations">Deformation load values.
        /// The deformations specified for a given degree of freedom are applicable only if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadDeformation(string name,
            string loadPattern,
            DegreesOfFreedomLocal degreesOfFreedom,
            Deformations deformations,
            eItemType itemType = eItemType.Object)
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            double[] csiDeformations = deformations.ToArray();

            _callCode = _sapModel.LinkObj.SetLoadDeformation(name, 
                            loadPattern, ref csiDegreesOfFreedom, ref csiDeformations, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the deformation load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadDeformation(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.LinkObj.DeleteLoadDeformation(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        // LoadTargetForce
        /// <summary>
        /// This function retrieves the target force load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of deformation loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each target force.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each target force.</param>
        /// <param name="forcesActive">Boolean values indicating if the considered degree of freedom has a target force assignment.</param>
        /// <param name="forcesValues">Target force values.
        /// The target forces specified for a given degree of freedom are only applicable if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="relativeForcesLocations">Relative distances along the line elements where the target force values apply.
        /// The relative distances specified for a given degree of freedom are only applicable if the corresponding dofn item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadTargetForce(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref ForcesActive[] forcesActive,
            ref Forces[] forcesValues,
            ref Forces[] relativeForcesLocations,
            eItemType itemType = eItemType.Object)
        {
            bool[] dof1 = new bool[0];
            bool[] dof2 = new bool[0];
            bool[] dof3 = new bool[0];
            bool[] dof4 = new bool[0];
            bool[] dof5 = new bool[0];
            bool[] dof6 = new bool[0];

            double[] P = new double[0];
            double[] V2 = new double[0];
            double[] V3 = new double[0];
            double[] T = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            double[] T1 = new double[0];
            double[] T2 = new double[0];
            double[] T3 = new double[0];
            double[] T4 = new double[0];
            double[] T5 = new double[0];
            double[] T6 = new double[0];

            _callCode = _sapModel.LinkObj.GetLoadTargetForce(name, ref numberItems, ref names, ref loadPatterns,
                            ref dof1, ref dof2, ref dof3, ref dof4, ref dof5, ref dof6,
                            ref P, ref V2, ref V3, ref T, ref M2, ref M3,
                            ref T1, ref T2, ref T3, ref T4, ref T5, ref T6,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forcesActive = new ForcesActive[numberItems - 1];
            forcesValues = new Forces[numberItems - 1];
            relativeForcesLocations = new Forces[numberItems - 1];
            for (int i = 0; i < numberItems; i++)
            {
                forcesActive[i].P = dof1[i];
                forcesActive[i].V2 = dof2[i];
                forcesActive[i].V3 = dof3[i];
                forcesActive[i].T = dof4[i];
                forcesActive[i].M2 = dof5[i];
                forcesActive[i].M3 = dof6[i];

                forcesValues[i].P = P[i];
                forcesValues[i].V2 = V2[i];
                forcesValues[i].V3 = V3[i];
                forcesValues[i].T = T[i];
                forcesValues[i].M2 = M2[i];
                forcesValues[i].M3 = M3[i];

                relativeForcesLocations[i].P = T1[i];
                relativeForcesLocations[i].V2 = T2[i];
                relativeForcesLocations[i].V3 = T3[i];
                relativeForcesLocations[i].T = T4[i];
                relativeForcesLocations[i].M2 = T5[i];
                relativeForcesLocations[i].M3 = T6[i];
            }
        }

        /// <summary>
        /// This function assigns the target force load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with each target force.</param>
        /// <param name="forcesActive">Boolean values indicating if the considered degree of freedom has a target force assignment.</param>
        /// <param name="forceValues">Target force values.
        /// The target forces specified for a given degree of freedom are only applicable if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="relativeForcesLocation">Relative distances along the line elements where the target force values apply.
        /// The relative distances specified for a given degree of freedom are only applicable if the corresponding dofn item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadTargetForce(string name,
            string loadPattern,
            ForcesActive forcesActive,
            Forces forceValues,
            Forces relativeForcesLocation,
            eItemType itemType = eItemType.Object)
        {
            bool[] csiForcesActive = forcesActive.ToArray();
            double[] csiForceValue = forceValues.ToArray();
            double[] csiRelativeForcesLocation = relativeForcesLocation.ToArray();

            _callCode = _sapModel.LinkObj.SetLoadTargetForce(name, 
                            loadPattern, ref csiForcesActive, ref csiForceValue, ref csiRelativeForcesLocation, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the target force load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadTargetForce(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.LinkObj.DeleteLoadTargetForce(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
