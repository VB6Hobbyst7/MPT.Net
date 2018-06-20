// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="AreaObject.cs" company="">
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
    /// Represents the area object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAreaObject" />
    public class AreaObject : CSiApiBase, IAreaObject
    {

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaObject" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AreaObject(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Query
        /// <summary>
        /// This function returns the total number of defined area properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.AreaObj.Count();
        }

        /// <summary>
        /// This function retrieves the names of all items.
        /// </summary>
        /// <param name="names">Names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.AreaObj.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the names of all defined area properties for a given story.
        /// </summary>
        /// <param name="storyName">Name of the story to filter the area names by.</param>
        /// <param name="names">Area object names retrieved by the program.</param>
        public void GetNameListOnStory(string storyName, 
            out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.AreaObj.GetNameListOnStory(storyName, ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="label">The object label.</param>
        /// <param name="story">The object story label.</param>
        public void GetLabelFromName(string name,
            ref string label,
            ref string story)
        {
            _callCode = _sapModel.AreaObj.GetLabelFromName(name, ref label, ref story);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <param name="names">The object names.</param>
        /// <param name="labels">The object labels.</param>
        /// <param name="stories">The story levels of the objects.</param>
        public void GetLabelNameList(ref string[] names,
            ref string[] labels,
            ref string[] stories)
        {
            _callCode = _sapModel.AreaObj.GetLabelNameList(ref _numberOfItems, ref names, ref labels, ref stories);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        /// <param name="label">The object label.</param>
        /// <param name="story">The object story level.</param>
        /// <param name="name">The object unique name.</param>
        public void GetNameFromLabel(string label,
            string story,
            ref string name)
        {
            _callCode = _sapModel.AreaObj.GetNameFromLabel(label, story, ref name);
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
            bool isGlobal = true)
        {
            directionCosines = new double[9];
            _callCode = _sapModel.AreaObj.GetTransformationMatrix(nameObject, ref directionCosines, isGlobal);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of the point objects that define an area object.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="points">The names of the points that defined the area object.
        /// The point names are listed in the positive order around the object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPoints(string name, 
            out string[] points)
        {
            points = new string[0];
            _callCode = _sapModel.AreaObj.GetPoints(name, ref _numberOfItems, ref points);
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
            _callCode = _sapModel.AreaObj.GetGUID(name, ref GUID);
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
            _callCode = _sapModel.AreaObj.SetGUID(name, GUID);
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
            _callCode = _sapModel.AreaObj.GetElm(name, 
                ref _numberOfItems, 
                ref elementNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Axes
        /// <summary>
        /// This function retrieves the local axis angle assignment for the area .
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]</param>
        /// <param name="isAdvanced">True area object local axes orientation was obtained using advanced local axes parameters.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLocalAxes(string name,
            out AngleLocalAxes angleOffset,
            out bool isAdvanced)
        {
            isAdvanced = false;
            double angleA = 0;

            _callCode = _sapModel.AreaObj.GetLocalAxes(name, ref angleA, ref isAdvanced);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            angleOffset = new AngleLocalAxes {AngleA = angleA};
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
            _callCode = _sapModel.AreaObj.SetLocalAxes(name, 
                            angleOffset.AngleA, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function gets the advanced local axes data for an existing object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="plane2">This is 12 or 13, indicating that the local plane determined by the plane reference vector is the 1-2 plane or the 1-3 plane.
        /// This item applies only when the <paramref name="isActive" /> = True.</param>
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
            ref eReferenceVector planeVectorOption,
            ref string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector)
        {
            int csiPlaneVectorOption = 0;
            int[] csiPlaneVectorDirection = new int[0];

            _callCode = _sapModel.AreaObj.GetLocalAxesAdvanced(name, ref isActive, ref plane2, ref csiPlaneVectorOption, ref planeCoordinateSystem, ref csiPlaneVectorDirection, ref planePoint, ref planeReferenceVector);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            planeVectorOption = (eReferenceVector)csiPlaneVectorOption;
            planeVectorDirection = csiPlaneVectorDirection.Cast<eReferenceVectorDirection>().ToArray();
        }


        /// <summary>
        /// This function sets the advanced local axes data for an existing object.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="plane2">This is 12 or 13, indicating that the local plane determined by the plane reference vector is the 1-2 plane or the 1-3 plane.
        /// This item applies only when the <paramref name="isActive" /> = True.</param>
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
            eReferenceVector planeVectorOption,
            string planeCoordinateSystem,
            eReferenceVectorDirection[] planeVectorDirection,
            string[] planePoint,
            double[] planeReferenceVector,
            eItemType itemType = eItemType.Object)
        {
            arraysLengthMatch(nameof(planeVectorDirection), planeVectorDirection.Length, nameof(planePoint), planePoint.Length);
            arraysLengthMatch(nameof(planeVectorDirection), planeVectorDirection.Length, nameof(planeReferenceVector), planeReferenceVector.Length);

            int[] csiPlaneVectorDirection = planeVectorDirection.Cast<int>().ToArray();

            _callCode = _sapModel.AreaObj.SetLocalAxesAdvanced(name, 
                            isActive, plane2, (int)planeVectorOption, planeCoordinateSystem, ref csiPlaneVectorDirection, 
                            ref planePoint, ref planeReferenceVector, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Modifiers
        /// <summary>
        /// This function retrieves the modifier assignment for area objects.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetModifiers(string name, 
            out AreaModifier modifiers)
        {
            double[] csiModifiers = new double[0];

            _callCode = _sapModel.AreaObj.GetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            modifiers = new AreaModifier();
            modifiers.FromArray(csiModifiers);
        }

        /// <summary>
        /// This function defines the modifier assignment for area objects.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetModifiers(string name, 
            AreaModifier modifiers)
        {
            if (modifiers == null) { return; }
            double[] csiModifiers = modifiers.ToArray();

            _callCode = _sapModel.AreaObj.SetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes a modifier assignment.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteModifiers(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.DeleteModifiers(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Creation & Groups
        /// <summary>
        /// This function changes the name of an existing area object.
        /// </summary>
        /// <param name="currentName">The existing name of a defined area object.</param>
        /// <param name="newName">The new name for the area object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, string newName)
        {
            _callCode = _sapModel.AreaObj.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// The function deletes a specified area object.
        /// It returns an error if the specified object can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.AreaObj.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function adds a new object whose corner points are at the specified coordinates.
        /// </summary>
        /// <param name="coordinates">Coordinates for the corner points of the object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.
        /// At least three coordinates are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <exception cref="CSiException">At least three coordinates must be provided for an area object. " +
        /// "Provided: " + coordinates.Length
        /// or API_DEFAULT_ERROR_CODE</exception>
        public void AddByCoordinate(ref Coordinate3DCartesian[] coordinates,
            ref string name,
            string nameProperty = "Default",
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
            if (coordinates.Length < 3)
            {
                throw new CSiException("At least three coordinates must be provided for an area object. " +
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
            
            _callCode = _sapModel.AreaObj.AddByCoord(coordinates.Length,
                ref xCoordinates, ref yCoordinates, ref zCoordinates,
                ref name, nameProperty, userName, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function adds a new object whose corner points are specified by name.
        /// </summary>
        /// <param name="pointNames">Names of the point objects that define the corner points of the added object.
        /// At least three points are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <exception cref="CSiException">At least three points must be provided for an area object. " +
        /// "Provided: " + pointNames.Length
        /// or API_DEFAULT_ERROR_CODE</exception>
        public void AddByPoint(string[] pointNames,
            ref string name,
            string nameProperty = "Default",
            string userName = "")
        {
            if (pointNames.Length < 3)
            {
                throw new CSiException("At least three points must be provided for an area object. " +
                                       "Provided: " + pointNames.Length);
            }

            _callCode = _sapModel.AreaObj.AddByPoint(pointNames.Length, ref pointNames, ref name, nameProperty, userName);
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
            _callCode = _sapModel.AreaObj.SetGroupAssign(name, 
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
            _callCode = _sapModel.AreaObj.GetGroupAssign(objectName,
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
            _callCode = _sapModel.AreaObj.GetSelected(name, ref isSelected);
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
            _callCode = _sapModel.AreaObj.SetSelected(name, 
                            isSelected, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the selected status for area object edges.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="areEdgesSelected">True: The specified area object edge is selected;
        /// Selected(0) = Selected status for edge 1;
        /// Selected(1) = Selected status for edge 2;
        /// Selected(n) = Selected status for edge(n + 1)</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSelectedEdge(string name,
            ref bool[] areEdgesSelected)
        {
            _callCode = _sapModel.AreaObj.GetSelectedEdge(name, ref _numberOfItems, ref areEdgesSelected);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the selected status for area object edges.
        /// </summary>
        /// <param name="name">The name of an existing area object</param>
        /// <param name="numberOfEdges">The area object edge that is to have its selected status set.</param>
        /// <param name="isEdgeSelected">True: The specified area object edge is selected.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSelectedEdge(string name,
            int numberOfEdges,
            bool isEdgeSelected)
        {
            _callCode = _sapModel.AreaObj.SetSelectedEdge(name, numberOfEdges, isEdgeSelected);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// This function retrieves the section property assigned to an area object.
        /// </summary>
        /// <param name="name">The name of a defined area object.</param>
        /// <param name="propertyName">The name of the section property assigned to the area object.
        /// This item is None if there is no section property assigned to the area object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSection(string name,
            out string propertyName)
        {
            propertyName = string.Empty;
            _callCode = _sapModel.AreaObj.GetProperty(name, ref propertyName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns the section property to an area object.
        /// </summary>
        /// <param name="name">The name of a defined area object.</param>
        /// <param name="propertyName">The name of the section property assigned to the area object.</param>
        /// <param name="itemType">If this item is Object, the assignment is made to the area object specified by the Name item.
        /// If this item is Group, the assignment is made to all area objects in the group specified by the Name item.
        /// If this item is SelectedObjects, assignment is made to all selected area objects, and the Name item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSection(string name, 
            string propertyName, 
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetProperty(name, 
                            propertyName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the thickness overwrite assignments for area elements.
        /// </summary>
        /// <param name="name">The name of an existing area element.</param>
        /// <param name="thicknessType">Indicates the thickness overwrite type.</param>
        /// <param name="thicknessPattern">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByJointPattern" />.
        /// It is the name of the defined joint pattern that is used to calculate the thicknesses.</param>
        /// <param name="thicknessPatternScaleFactor">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByJointPattern" />.
        /// It is the scale factor applied to the joint pattern when calculating the thicknesses. [L]</param>
        /// <param name="thicknesses">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByPoint" />.
        /// It is an array of thicknesses at each of the points that define the area element. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetThickness(string name,
            ref eAreaThicknessType thicknessType,
            ref string thicknessPattern,
            ref double thicknessPatternScaleFactor,
            ref double[] thicknesses)
        {
            int csiThicknessType = 0;

            _callCode = _sapModel.AreaObj.GetThickness(name, ref csiThicknessType, ref thicknessPattern, ref thicknessPatternScaleFactor, ref thicknesses);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            thicknessType = (eAreaThicknessType) csiThicknessType;
        }

        /// <summary>
        /// This function assigns the thickness overwrite assignments for area objects.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="thicknessType">Indicates the thickness overwrite type.</param>
        /// <param name="thicknessPattern">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByJointPattern" />.
        /// It is the name of the defined joint pattern that is used to calculate the thicknesses.</param>
        /// <param name="thicknessPatternScaleFactor">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByJointPattern" />.
        /// It is the scale factor applied to the joint pattern when calculating the thicknesses. [L]</param>
        /// <param name="thicknesses">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByPoint" />.
        /// It is an array of thicknesses at each of the points that define the area object. [L]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetThickness(string name,
            eAreaThicknessType thicknessType,
            string thicknessPattern,
            double thicknessPatternScaleFactor,
            double[] thicknesses,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetThickness(name, 
                            (int)thicknessType, thicknessPattern, thicknessPatternScaleFactor, ref thicknesses, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#endif



        /// <summary>
        /// This function retrieves the mass per unit area assignment from objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="value">The mass per unit length assigned to the object. [M/L^2]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMass(string name,
            out double value)
        {
            value = 0;
            _callCode = _sapModel.AreaObj.GetMass(name, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns mass per unit area to objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="value">The mass per unit length assigned to the object. [M/L^2]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMass(string name,
            double value,
            bool replace,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetMass(name, 
                            value, replace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes all mass assignments for the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteMass(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.DeleteMass(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the method to determine the notional size of an area section for the creep and shrinkage calculations.
        /// This function is currently worked for shell type area section.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area section property.</param>
        /// <param name="sizeType">The type to define the notional size of a section.</param>
        /// <param name="value">For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.Auto" />, the Value represents for the scale factor to the program-determined notional size.
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.User" />, the Value represents for the user-defined notional size [L].
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.None" />, the Value will not be used and can be set to 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNotionalSize(string name,
            ref eNotionalSizeType sizeType,
            ref double value)
        {
            string csiSizeType = "";

            _callCode = _sapModel.PropArea.GetNotionalSize(name, ref csiSizeType, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            sizeType = EnumLibrary.ConvertStringToEnumByDescription<eNotionalSizeType>(csiSizeType);
        }

        /// <summary>
        /// This function assigns the method to determine the notional size of an area section for the creep and shrinkage calculations.
        /// This function is currently worked for shell type area section.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area section property.</param>
        /// <param name="sizeType">The type to define the notional size of a section.</param>
        /// <param name="value">For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.Auto" />, represents for the scale factor to the program-determined notional size.
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.User" />, represents for the user-defined notional size [L].
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.None" />, will not be used and can be set to 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetNotionalSize(string name,
            eNotionalSizeType sizeType,
            double value)
        {
            string csiSizeType = EnumLibrary.GetEnumDescription(sizeType);

            _callCode = _sapModel.PropArea.SetNotionalSize(name, csiSizeType, value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the material temperature assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="temperature">This is the material temperature value assigned to the object. [T]</param>
        /// <param name="patternName">This is blank or the name of a defined joint pattern.
        /// If it is blank, the material temperature for the line object is uniform along the object at the value specified by <paramref name="temperature" />.
        /// If <paramref name="patternName"/> is the name of a defined joint pattern, the material temperature for the line object may vary from one end to the other.
        /// The material temperature at each end of the object is equal to the specified temperature multiplied by the pattern value at the joint at the end of the line object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMaterialTemperature(string name,
            out double temperature,
            out string patternName)
        {
            temperature = 0;
            patternName = string.Empty;
            _callCode = _sapModel.AreaObj.GetMatTemp(name, ref temperature, ref patternName);
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
            _callCode = _sapModel.AreaObj.SetMatTemp(name, 
                            temperature, patternName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        /// <summary>
        /// This function retrieves the material overwrite assigned to an area object, if any.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <param name="name">The name of a defined area object.</param>
        /// <param name="propertyName">This is None, indicating that no material overwrite exists for the specified area object, or it is the name of an existing material property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMaterialOverwrite(string name,
            ref string propertyName)
        {
            _callCode = _sapModel.AreaObj.GetMaterialOverwrite(name, ref propertyName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the material overwrite assignment for objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="propertyName">This is None or a blank string, indicating that no material overwrite exists for the specified element, or it is the name of an existing material property.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMaterialOverwrite(string name,
            string propertyName,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetMaterialOverwrite(name, 
                            propertyName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Area Properties
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves select data for all area objects in the model .
        /// </summary>
        /// <param name="areaNames">The area names.</param>
        /// <param name="designOrientations">The design orientations.</param>
        /// <param name="numberOfBoundaryPts">The number of boundary points.</param>
        /// <param name="pointDelimiters">The point delimiters.</param>
        /// <param name="pointNames">The point names for each area.</param>
        /// <param name="coordinates">The coordinates for each point for each area. [L].</param>
        public void GetAllAreas(ref string[] areaNames,
            ref eAreaDesignOrientation[] designOrientations,
            ref int numberOfBoundaryPts,
            ref int[] pointDelimiters,
            ref string[] pointNames,
            ref Coordinate3DCartesian[] coordinates)
        {
            CSiProgram.eAreaDesignOrientation[] csiDesignOrientations = new CSiProgram.eAreaDesignOrientation[0];
            double[] coordinatesX = new double[0];
            double[] coordinatesY = new double[0];
            double[] coordinatesZ = new double[0];

            _callCode = _sapModel.AreaObj.GetAllAreas(ref _numberOfItems, ref areaNames, ref csiDesignOrientations,
                ref numberOfBoundaryPts, ref pointDelimiters, ref pointNames, ref coordinatesX, ref coordinatesY, ref coordinatesZ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            designOrientations = new eAreaDesignOrientation[_numberOfItems];
            for (int i = 0; i < _numberOfItems; i++)
            {
                designOrientations[i] = EnumLibrary.Convert(csiDesignOrientations[i], designOrientations[i]);
            }

            coordinates = new Coordinate3DCartesian[_numberOfItems];
            for (int i = 0; i < _numberOfItems; i++)
            {
                coordinates[i].X = coordinatesX[i];
                coordinates[i].Y = coordinatesY[i];
                coordinates[i].Z = coordinatesZ[i];
            }
        }

        /// <summary>
        /// Retrieves whether the specified area object is an opening.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="isOpening">True: Specified area object is an opening.</param>
        public void GetOpening(string name,
            ref bool isOpening)
        {
            _callCode = _sapModel.AreaObj.GetOpening(name, ref isOpening);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Designates an area object(s) as an opening.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="isOpening">True: Specified area object is an opening.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetOpening(string name,
            bool isOpening,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetOpening(name, 
                            isOpening, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#else
        /// <summary>
        /// This function retrieves the joint offset assignments for area elements.
        /// </summary>
        /// <param name="name">The name of an existing area element.</param>
        /// <param name="offsetType">Indicates the joint offset type.</param>
        /// <param name="offsetPattern">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByJointPattern" />.
        /// It is the name of the defined joint pattern that is used to calculate the thicknesses.</param>
        /// <param name="offsetPatternScaleFactor">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByJointPattern" />.
        /// It is the scale factor applied to the joint pattern when calculating the thicknesses. [L]</param>
        /// <param name="offsets">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByPoint" />.
        /// It is an array of thicknesses at each of the points that define the area element. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetOffsets(string name,
            ref eAreaOffsetType offsetType,
            ref string offsetPattern,
            ref double offsetPatternScaleFactor,
            ref double[] offsets)
        {
            int csiOffsetType = 0;

            _callCode = _sapModel.AreaObj.GetOffsets(name, ref csiOffsetType, ref offsetPattern, ref offsetPatternScaleFactor, ref offsets);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            offsetType = (eAreaOffsetType) csiOffsetType;
        }

        /// <summary>
        /// This function assigns the joint offset assignments for area objects.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="offsetType">Indicates the joint offset type.</param>
        /// <param name="offsetPattern">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByJointPattern" />.
        /// It is the name of the defined joint pattern that is used to calculate the thicknesses.</param>
        /// <param name="offsetPatternScaleFactor">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByJointPattern" />.
        /// It is the scale factor applied to the joint pattern when calculating the thicknesses. [L]</param>
        /// <param name="offsets">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByPoint" />.
        /// It is an array of thicknesses at each of the points that define the area object. [L]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetOffsets(string name,
            eAreaOffsetType offsetType,
            string offsetPattern,
            double offsetPatternScaleFactor,
            double[] offsets,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetOffsets(name, 
                            (int)offsetType, offsetPattern, offsetPatternScaleFactor, ref offsets, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves automatic meshing assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="meshType">The automatic mesh type for the object.
        /// Mesh options <see cref="eMeshType.SpecifiedNumber" />, <see cref="eMeshType.SpecifiedMaxSize" /> and <see cref="eMeshType.PointsOnAreaEdges" /> apply to quadrilaterals and triangles only.</param>
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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAutoMesh(string name,
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
            ref double subMeshSize)
        {
            int csiMeshType = 0;

            _callCode = _sapModel.AreaObj.GetAutoMesh(name, ref csiMeshType, ref numberOfObjectsAlongPoint12, ref numberOfObjectsAlongPoint13, ref maxSizeOfObjectsAlongPoint12, ref maxSizeOfObjectsAlongPoint13, 
                ref pointOnEdgeFromLine, ref pointOnEdgeFromPoint, ref extendCookieCutLines, ref rotation, ref maxSizeGeneral, ref localAxesOnEdge, ref localAxesOnFace, ref restraintsOnEdge, ref restraintsOnFace,
                ref group, ref subMesh, ref subMeshSize);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            meshType = (eMeshType)csiMeshType;
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetAutoMesh(string name,
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
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetAutoMesh(name, 
                            (int)meshType, 
                            numberOfObjectsAlongPoint12, numberOfObjectsAlongPoint13, 
                            maxSizeOfObjectsAlongPoint12, maxSizeOfObjectsAlongPoint13,
                            pointOnEdgeFromLine, pointOnEdgeFromPoint, extendCookieCutLines, 
                            rotation, maxSizeGeneral, localAxesOnEdge, localAxesOnFace, restraintsOnEdge, restraintsOnFace,
                            group, subMesh, subMeshSize, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Support & Connections
#if BUILD_ETABS2016 || BUILD_ETABS2017
         /// <summary>
        /// Retrieves the named spring property assignment for an object.</summary>
        /// <param name="name">The name of an existing object .</param>
        /// <param name="nameSpring">The name of an existing point spring property.</param>
        public void GetSpringAssignment(string name,
            ref string nameSpring)
        {
            _callCode = _sapModel.AreaObj.GetSpringAssignment(name, ref nameSpring);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Assigns an existing named spring property to objects.</summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="nameSpring">The name of an existing point spring property.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetSpringAssignment(string name,
            string nameSpring,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetSpringAssignment(name, nameSpring);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the diaphragm for a specified object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="diaphragmName">The name of an existing diaphragm.</param>
        public void GetDiaphragm(string name,
            ref string diaphragmName)
        {
            _callCode = _sapModel.AreaObj.GetDiaphragm(name, ref diaphragmName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Assigns a diaphragm to an object .
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="diaphragmName">The name of an existing diaphragm.</param>
        public void SetDiaphragm(string name,
            string diaphragmName = "")
        {
            _callCode = _sapModel.AreaObj.SetDiaphragm(name, diaphragmName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        
#else
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

            _callCode = _sapModel.AreaObj.GetSpring(name, ref _numberOfItems, ref csiSpringTypes, ref stiffnesses, ref csiSpringSimpleTypes, ref linkProperties, ref csiFace, ref csiSpringLocalOneTypes, ref directions, ref areOutward, ref vectorComponentsX, ref vectorComponentsY, ref vectorComponentsZ, ref coordinateSystems, ref angleOffsets);
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

            _callCode = _sapModel.AreaObj.SetSpring(name, 
                            (int)springType, stiffness, (int)springSimpleType, linkProperty, (int)face, (int)springLocalOneType, 
                            direction, isOutward, ref csiVector, angleOffset, replace, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


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
            _callCode = _sapModel.AreaObj.DeleteSpring(name, 
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
            _callCode = _sapModel.AreaObj.GetEdgeConstraint(name, ref constraintExists);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function makes generated edge constraint assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="constraintExists">True: An automatic edge constraint is generated by the program for the object in the analysis model.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetEdgeConstraint(string name,
            bool constraintExists,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetEdgeConstraint(name, 
                            constraintExists, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Design
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the design orientation of an area object.
        /// </summary>
        /// <param name="name">The name of a defined area object.</param>
        /// <param name="designOrientation">The design orientation.</param>
        public void GetDesignOrientation(string name,
            ref eAreaDesignOrientation designOrientation)
        {
            CSiProgram.eAreaDesignOrientation csiDesignOrientation = CSiProgram.eAreaDesignOrientation.Other;

            _callCode = _sapModel.AreaObj.GetDesignOrientation(name, ref csiDesignOrientation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            designOrientation = EnumLibrary.Convert(csiDesignOrientation, designOrientation);
        }
#endif

        #region Design - Wall
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017

        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="namePier">The name of the pier assignment, if any, or "None".</param>
        public void GetPier(string name,
            ref string namePier)
        {
            _callCode = _sapModel.AreaObj.GetPier(name, ref namePier);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the pier label assignment of one or more objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="namePier">The name of the pier assignment, if any, or "None".</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetPier(string name,
                string namePier,
                eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetPier(name, 
                            namePier, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="nameSpandrel">The name of the spandrel assignment, if any, or "None".</param>
        public void GetSpandrel(string name,
            ref string nameSpandrel)
        {
            _callCode = _sapModel.AreaObj.GetSpandrel(name, ref nameSpandrel);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the spandrel label assignment of one or more objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="nameSpandrel">The name of the spandrel assignment, if any, or "None".</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetSpandrel(string name,
            string nameSpandrel,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetSpandrel(name, 
                            nameSpandrel, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===
        /// <summary>
        /// Retrieves rebar data for an area pier object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="layerIds">The rebar layer ids.</param>
        /// <param name="layerTypes">The rebar layer types.</param>
        /// <param name="clearCovers">The clear cover of each rebar layer.</param>
        /// <param name="barAreas">The bar areas for each layer.</param>
        /// <param name="barSpacings">The bar spacings for each layer.</param>
        /// <param name="numberOfBars">The number of bars in each layer.</param>
        /// <param name="isConfined">Status of whether or not the rebar layer is confined.</param>
        /// <param name="barSizeNames">The bar size names for each layer.</param>
        /// <param name="endZoneLengths">The end zone lengths for each layer.</param>
        /// <param name="endZoneThicknesses">The end zone thicknesses for each layer.</param>
        /// <param name="endZoneOffsets">The end zone offsets for each layer.</param>
        public void GetRebarDataPier(string name,
            ref string[] layerIds,
            ref eWallPierRebarLayerType[] layerTypes,
            ref double[] clearCovers,
            ref double[] barAreas,
            ref double[] barSpacings,
            ref int[] numberOfBars,
            ref bool[] isConfined,
            ref string[] barSizeNames,
            ref double[] endZoneLengths,
            ref double[] endZoneThicknesses,
            ref double[] endZoneOffsets)
        {
            CSiProgram.eWallPierRebarLayerType[] csiLayerTypes = new CSiProgram.eWallPierRebarLayerType[0];

            _callCode = _sapModel.AreaObj.GetRebarDataPier(name, ref _numberOfItems,
                            ref layerIds, ref csiLayerTypes,
                            ref clearCovers, ref barSizeNames, ref barAreas, ref barSpacings, ref numberOfBars, ref isConfined,
                            ref endZoneLengths, ref endZoneThicknesses, ref endZoneOffsets);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            layerTypes = new eWallPierRebarLayerType[_numberOfItems];
            for (int i = 0; i < _numberOfItems; i++)
            {
                layerTypes[i] = EnumLibrary.Convert(csiLayerTypes[i], layerTypes[i]);
            }
        }

        /// <summary>
        /// Retrieves rebar data for an area spandrel object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="layerIds">The rebar layer ids.</param>
        /// <param name="layerTypes">The rebar layer types.</param>
        /// <param name="clearCovers">The clear cover of each rebar layer.</param>
        /// <param name="barAreas">The bar areas for each layer.</param>
        /// <param name="barSpacings">The bar spacings for each layer.</param>
        /// <param name="numberOfBars">The number of bars in each layer.</param>
        /// <param name="isConfined">Status of whether or not the rebar layer is confined.</param>
        /// <param name="barSizeIndices">The rebar size indices in each layer.</param>
        public void GetRebarDataSpandrel(string name,
            ref string[] layerIds,
            ref eWallSpandrelRebarLayerType[] layerTypes,
            ref double[] clearCovers,
            ref double[] barAreas,
            ref double[] barSpacings,
            ref int[] numberOfBars,
            ref bool[] isConfined,
            ref int[] barSizeIndices)
        {
            CSiProgram.eWallSpandrelRebarLayerType[] csiLayerTypes = new CSiProgram.eWallSpandrelRebarLayerType[0];

            _callCode = _sapModel.AreaObj.GetRebarDataSpandrel(name, ref _numberOfItems,
                ref layerIds, ref csiLayerTypes,
                ref clearCovers, ref barSizeIndices, ref barAreas, ref barSpacings, ref numberOfBars, ref isConfined);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            layerTypes = new eWallSpandrelRebarLayerType[_numberOfItems];
            for (int i = 0; i < _numberOfItems; i++)
            {
                layerTypes[i] = EnumLibrary.Convert(csiLayerTypes[i], layerTypes[i]);
            }
        }
#endif
        #endregion
        #endregion

        #region Loads
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // LoadGravity
        /// <summary>
        /// This function retrieves the gravity load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, object or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of gravity loads retrieved for the specified objects.</param>
        /// <param name="names">The name of the object associated with each gravity load.</param>
        /// <param name="loadPatterns">The name of the coordinate system in which the gravity load multipliers are specified.</param>
        /// <param name="xLoadMultiplier">Gravity load multipliers in the x direction of the specified coordinate system.</param>
        /// <param name="yLoadMultiplier">Gravity load multipliers in the y direction of the specified coordinate system.</param>
        /// <param name="zLoadMultiplier">Gravity load multipliers in the z direction of the specified coordinate system.</param>
        /// <param name="coordinateSystems">The name of the coordinate system associated with each gravity load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
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
            _callCode = _sapModel.AreaObj.GetLoadGravity(name, ref numberItems, 
                            ref names, ref loadPatterns, ref coordinateSystems, ref xLoadMultiplier, ref yLoadMultiplier, ref zLoadMultiplier, 
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
            _callCode = _sapModel.AreaObj.SetLoadGravity(name, 
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
            _callCode = _sapModel.AreaObj.DeleteLoadGravity(name, 
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



        // LoadStrain
        /// <summary>
        /// This function retrieves the strain load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, object or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of strain loads retrieved for the specified objects.</param>
        /// <param name="names">The name of the object associated with each strain load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each strain load.</param>
        /// <param name="components">Indicates the strain component associated with each strain load.</param>
        /// <param name="strainLoadValues">The strain values. [L/L]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify each strain load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
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

            _callCode = _sapModel.AreaObj.GetLoadStrain(name, ref numberItems, 
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
            _callCode = _sapModel.AreaObj.SetLoadStrain(name, 
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
            _callCode = _sapModel.AreaObj.DeleteLoadStrain(name, 
                            loadPattern, (int)component, 
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

            _callCode = _sapModel.AreaObj.GetLoadSurfacePressure(name, ref numberItems, 
                            ref names, ref loadPatterns, ref csiFaceApplied, ref surfacePressureLoadValues, ref jointPatternNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            faceApplied = csiFaceApplied.Cast<eFace>().ToArray();
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
            _callCode = _sapModel.AreaObj.SetLoadSurfacePressure(name, 
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
            _callCode = _sapModel.AreaObj.DeleteLoadSurfacePressure(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        // LoadTemperature
        /// <summary>
        /// This function retrieves the temperature load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="numberItems">The total number of temperature loads retrieved for the specified objects.</param>
        /// <param name="names">The name of the object associated with each temperature load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each temperature load.</param>
        /// <param name="temperatureLoadType">Indicates the type of temperature load for each load pattern.</param>
        /// <param name="temperatureLoadValues">Temperature load values. [T] for <paramref name="temperatureLoadType" /> = Temperature, [T/L] for all others.</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify the temperature load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadTemperature(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadTemperatureType[] temperatureLoadType,
            ref double[] temperatureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
            int[] csiTemperatureLoadType = new int[0];

            _callCode = _sapModel.AreaObj.GetLoadTemperature(name, ref numberItems, 
                            ref names, ref loadPatterns, ref csiTemperatureLoadType, ref temperatureLoadValues, ref jointPatternNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            temperatureLoadType = csiTemperatureLoadType.Cast<eLoadTemperatureType>().ToArray();
        }

        /// <summary>
        /// This function assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing element or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the temperature load.</param>
        /// <param name="temperatureLoadType">Indicates the type of temperature load.</param>
        /// <param name="temperatureLoadValue">Temperature load value, [T] for <paramref name="temperatureLoadType" /> = Temperature, [T/L] for all others.</param>
        /// <param name="jointPatternName">The joint pattern name, if any, used to specify the temperature load.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadTemperature(string name,
            string loadPattern,
            eLoadTemperatureType temperatureLoadType,
            double temperatureLoadValue,
            string jointPatternName,
            bool replace,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetLoadTemperature(name, 
                            loadPattern, (int)temperatureLoadType, temperatureLoadValue, jointPatternName, replace, 
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
            _callCode = _sapModel.AreaObj.DeleteLoadTemperature(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // LoadRotate
        /// <summary>
        /// This function retrieves the rotate load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of rotate loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each rotate load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each rotate load.</param>
        /// <param name="rotateLoadValues">Rotate load values. [F/L^2]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadRotate(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] rotateLoadValues,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.GetLoadRotate(name, ref numberItems, 
                            ref names, ref loadPatterns, ref rotateLoadValues, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function assigns rotational loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the rotational load.</param>
        /// <param name="rotateLoadValue">Rotate load value, angular velocity. [Cyc/T]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadRotate(string name,
            string loadPattern,
            double rotateLoadValue)
        {
            _callCode = _sapModel.AreaObj.SetLoadRotate(name, loadPattern, rotateLoadValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the rotation load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadRotate(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.DeleteLoadRotate(name, 
                            loadPattern,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        // LoadUniform
        /// <summary>
        /// This function retrieves the uniform load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, object or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of uniform loads retrieved for the specified objects.</param>
        /// <param name="names">The name of the object associated with each uniform load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each uniform load.</param>
        /// <param name="coordinateSystems">The name of the coordinate system associated with each uniform load.</param>
        /// <param name="directionApplied">The direction that the load is applied for each load pattern.</param>
        /// <param name="uniformLoadValues">The uniform load values. [F/L^2]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadUniform(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] coordinateSystems,
            ref eLoadDirection[] directionApplied,
            ref double[] uniformLoadValues,
            eItemType itemType = eItemType.Object)
        {
            int[] csiDirectionApplied = new int[0];

            _callCode = _sapModel.AreaObj.GetLoadUniform(name, ref numberItems, 
                            ref names, ref loadPatterns, ref coordinateSystems, ref csiDirectionApplied, ref uniformLoadValues, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            directionApplied = csiDirectionApplied.Cast<eLoadDirection>().ToArray();
        }

        /// <summary>
        /// This function assigns uniform loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the uniform load.</param>
        /// <param name="directionApplied">The direction that the load is applied.</param>
        /// <param name="uniformLoadValue">The uniform load value. [F/L^2]</param>
        /// <param name="coordinateSystem">The name of the coordinate system associated with the uniform load.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadUniform(string name,
            string loadPattern,
            eLoadDirection directionApplied,
            double uniformLoadValue,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetLoadUniform(name, 
                            loadPattern, uniformLoadValue, (int)directionApplied, replace, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the uniform load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadUniform(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.DeleteLoadUniform(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        // LoadUniformToFrame
        /// <summary>
        /// This function retrieves the wind pressure load assignments to area objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="numberItems">The total number of wind pressure loads retrieved for the specified area objects.</param>
        /// <param name="loadPatterns">The name of a defined load pattern for each load.</param>
        /// <param name="areaNames">The name of the area object associated with each uniform load.</param>
        /// <param name="values">The uniform load value for each load. [F/L^2]</param>
        /// <param name="directions">Indicates the direction of the applied load for each load.</param>
        /// <param name="distributionTypes">Load distribution type for how the load is tranferred to element edges for each load.</param>
        /// <param name="coordinateSystems">This is the name of a defined coordinate system associated with wach uniform load</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadUniformToFrame(string name,
            ref int numberItems,
            ref string[] loadPatterns,
            ref string[] areaNames,
            ref double[] values,
            ref eLoadDirection[] directions,
            ref eLoadDistributionType[] distributionTypes,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
            int[] csiDirections = new int[0];
            int[] csiDistributionTypes = new int[0];

            _callCode = _sapModel.AreaObj.GetLoadUniformToFrame(name, ref numberItems, 
                            ref areaNames, ref loadPatterns, ref coordinateSystems, ref csiDirections, ref values, ref csiDistributionTypes, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            directions = csiDirections.Cast<eLoadDirection>().ToArray();
            distributionTypes = csiDistributionTypes.Cast<eLoadDistributionType>().ToArray();
        }

        /// <summary>
        /// This function assigns wind pressure loads to area objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadPattern">The name of a defined load pattern.</param>
        /// <param name="value">The uniform load value. [F/L^2]</param>
        /// <param name="direction">Indicates the direction of the applied load.</param>
        /// <param name="distributionType">Load distribution type for how the load is tranferred to element edges.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified area object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="coordinateSystem">This is <see cref="CoordinateSystems.Local" /> or the name of a defined coordinate system, indicating the coordinate system in which the uniform load is specified.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadUniformToFrame(string name,
            string loadPattern,
            double value,
            eLoadDirection direction,
            eLoadDistributionType distributionType,
            bool replace = true,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetLoadUniformToFrame(name, 
                            loadPattern, value, (int)direction, (int)distributionType, replace, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the uniform load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadUniformToFrame(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.DeleteLoadUniformToFrame(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // LoadWindPressure
        /// <summary>
        /// This function retrieves the wind pressure load assignments to area objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="numberItems">The total number of wind pressure loads retrieved for the specified area objects.</param>
        /// <param name="areaNames">The name of the area object associated with each wind pressure load.</param>
        /// <param name="loadPatterns">The name of a defined load pattern for each load.</param>
        /// <param name="windPressureTypes">Wind pressure type for each load.</param>
        /// <param name="pressureCoefficients">This is the wind pressure coefficient for each load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadWindPressure(string name,
            ref int numberItems,
            ref string[] areaNames,
            ref string[] loadPatterns,
            ref eWindPressureApplication[] windPressureTypes,
            ref double[] pressureCoefficients,
            eItemType itemType = eItemType.Object)
        {
            int[] csiWindPressureTypes = new int[0];

            _callCode = _sapModel.AreaObj.GetLoadWindPressure(name, ref numberItems, 
                            ref areaNames, ref loadPatterns, ref csiWindPressureTypes, ref pressureCoefficients, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            windPressureTypes = csiWindPressureTypes.Cast<eWindPressureApplication>().ToArray();
        }

        /// <summary>
        /// This function assigns wind pressure loads to area objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadPattern">The name of a defined load pattern.</param>
        /// <param name="windPressureType">Wind pressure type.</param>
        /// <param name="pressureCoefficient">This is the wind pressure coefficient.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadWindPressure(string name,
            string loadPattern,
            eWindPressureApplication windPressureType,
            double pressureCoefficient,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetLoadWindPressure(name, 
                            loadPattern, (int)windPressureType, pressureCoefficient, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function deletes the wind pressure load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadWindPressure(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.DeleteLoadWindPressure(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
