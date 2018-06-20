// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="PointObject.cs" company="">
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
    /// Represents the point object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IPointObject" />
    public class PointObject : CSiApiBase, IPointObject
    {

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="PointObject" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public PointObject(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Query
        /// <summary>
        /// This function returns the total number of defined point properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.PointObj.Count();
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// If the <paramref name="name" /> item is provided, the function returns the total number of constraint assignments made to the specified point object.
        /// If the <paramref name="name" /> item is not specified or is specified as an empty string, the function returns the total number of constraint assignments to all point objects in the model.
        /// If the <paramref name="name" /> item is specified but it is not recognized by the program as a valid point object, an error is returned.
        /// TODO: Handle last case.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public int CountConstraint(string name = "")
        {
            int count = 0;

            _callCode = _sapModel.PointObj.CountConstraint(ref count, name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return count;
        }
#endif

        /// <summary>
        /// This function returns the total number of point objects in the model with restraint assignments.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int CountRestraint()
        {
            return _sapModel.PointObj.CountRestraint();
        }

        /// <summary>
        /// This function returns the total number of point objects in the model with spring assignments.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int CountSpring()
        {
            return _sapModel.PointObj.CountSpring();
        }


        /// <summary>
        /// This function returns the total number of panel zone assignments to point objects in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int CountPanelZone()
        {
            return _sapModel.PointObj.CountPanelZone();
        }

        /// <summary>
        /// If neither the <paramref name="name" /> item nor the <paramref name="loadPattern" /> item is provided, the function returns the total number of point load assignments to point objects in the model.
        /// If the <paramref name="name" /> item is provided but not the <paramref name="loadPattern" /> item, the function returns the total number of point load assignments made for the specified point object.
        /// If the <paramref name="name" /> item is not provided but the <paramref name="loadPattern" /> item is specified, the function returns the total number of point load assignments made to all point objectts for the specified load pattern.
        /// If both the <paramref name="name" /> item and the <paramref name="loadPattern" /> item are provided, the function the total number of point load assignments made to the specified point object for the specified load pattern.
        /// If the <paramref name="name" /> item or the <paramref name="loadPattern" /> item is provided but is not recognized by the program as valid, an error is returned.
        /// TODO: Handle last case.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <param name="loadPattern">The name of an existing load pattern.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public int CountLoadForce(string name = "",
            string loadPattern = "")
        {
            int count = 0;

            _callCode = _sapModel.PointObj.CountLoadForce(ref count, name, loadPattern);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return count;
        }

        /// <summary>
        /// If neither the <paramref name="name" /> item nor the <paramref name="loadPattern" /> item is provided, the function returns the total number of ground displacement load assignments to point objects in the model.
        /// If the <paramref name="name" /> item is provided but not the <paramref name="loadPattern" /> item, the function returns the total number of ground displacement load assignments made for the specified point object.
        /// If the <paramref name="name" /> item is not provided but the <paramref name="loadPattern" /> item is specified, the function returns the total number of ground displacement load assignments made to all point objects for the specified load pattern.
        /// If both the <paramref name="name" /> item and the <paramref name="loadPattern" /> item are provided, the function the total number of ground displacement load assignments made to the specified point object for the specified load pattern.
        /// If the <paramref name="name" /> item or the <paramref name="loadPattern" /> item is provided but is not recognized by the program as valid, an error is returned.
        /// TODO: Handle last case.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <param name="loadPattern">The name of an existing load pattern.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public int CountLoadDisplacements(string name = "",
            string loadPattern = "")
        {
            int count = 0;

            _callCode = _sapModel.PointObj.CountLoadDispl(ref count, name, loadPattern);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return count;
        }


        /// <summary>
        /// This function retrieves the names of all defined point properties.
        /// </summary>
        /// <param name="names">Point object names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PointObj.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the names of all defined point object properties for a given story.
        /// </summary>
        /// <param name="storyName">Name of the story to filter the point object names by.</param>
        /// <param name="names">Point object object names retrieved by the program.</param>
        public void GetNameListOnStory(string storyName,
                        out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PointObj.GetNameListOnStory(storyName, ref _numberOfItems, ref names);
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
            _callCode = _sapModel.PointObj.GetLabelFromName(name, ref label, ref story);
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
            _callCode = _sapModel.PointObj.GetLabelNameList(ref _numberOfItems, ref names, ref labels, ref stories);
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
            _callCode = _sapModel.PointObj.GetNameFromLabel(label, story, ref name);
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
            _callCode = _sapModel.PointObj.GetTransformationMatrix(nameObject, ref directionCosines, isGlobal);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Returns the x, y and z coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <paramref name="coordinateSystem" />.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="coordinate">The cartesian x-, y-, z-coordinate of the specified point object in the specified coordinate system.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the joint coordinates are returned.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetCoordinate(string name,
            ref Coordinate3DCartesian coordinate,
            string coordinateSystem = CoordinateSystems.Global)
        {
            double x = 0;
            double y = 0;
            double z = 0;

            _callCode = _sapModel.PointObj.GetCoordCartesian(name, ref x, ref y, ref z, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            coordinate.X = x;
            coordinate.Y = y;
            coordinate.Z = z;
        }

        /// <summary>
        /// Returns the r, Theta and z coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <paramref name="coordinateSystem" />.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="coordinate">The cylindrical r-, theta-, z-coordinate of the specified point object in the specified coordinate system.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the joint coordinates are returned.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetCoordinate(string name,
            ref Coordinate3DCylindrical coordinate,
            string coordinateSystem = CoordinateSystems.Global)
        {
            double radius = 0;
            double theta = 0;
            double z = 0;

            _callCode = _sapModel.PointObj.GetCoordCylindrical(name, ref radius, ref theta, ref z, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            coordinate.Radius = radius;
            coordinate.Theta = theta;
            coordinate.Z = z;
        }

        /// <summary>
        /// Returns the r, a and b coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <paramref name="coordinateSystem" />.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="coordinate">The spherical r-, a-, b-coordinate of the specified point object in the specified coordinate system.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the joint coordinates are returned.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetCoordinate(string name,
            ref Coordinate3DSpherical coordinate,
            string coordinateSystem = CoordinateSystems.Global)
        {
            double radius = 0;
            double theta = 0;
            double phi = 0;

            _callCode = _sapModel.PointObj.GetCoordSpherical(name, ref radius, ref theta, ref phi, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            coordinate.Radius = radius;
            coordinate.Theta = theta;
            coordinate.Phi = phi;
        }


        /// <summary>
        /// This function returns a list of objects connected to a specified point object.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="numberItems">This is the total number of objects connected to the specified point object.</param>
        /// <param name="objectTypes">The element/object type of each object connected to the specified point object.</param>
        /// <param name="objectNames">The element/object name of each object connected to the specified point object.</param>
        /// <param name="pointNumbers">The point number within the considered element/object that corresponds to the specified point object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetConnectivity(string name,
            ref int numberItems,
            ref eObjectType[] objectTypes,
            ref string[] objectNames,
            ref int[] pointNumbers)
        {
            int[] csiObjectTypes = new int[0];

            _callCode = _sapModel.PointObj.GetConnectivity(name, ref numberItems, ref csiObjectTypes, ref objectNames, ref pointNumbers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            objectTypes = csiObjectTypes.Cast<eObjectType>().ToArray();
        }



        /// <summary>
        /// This function returns the total number of objects (line, area, solid and link) that connect to the specified point object.
        /// </summary>
        /// <param name="name">The name of a point object.</param>
        /// <param name="commonTo">The total number of objects (line, area, solid and link) that connect to the specified point object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetCommonTo(string name,
            ref int commonTo)
        {
            _callCode = _sapModel.PointObj.GetCommonTo(name, ref commonTo);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the GUID for the specified point object.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="GUID">The GUID (Global Unique ID) for the specified object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGUID(string name,
            out string GUID)
        {
            GUID = string.Empty;
            _callCode = _sapModel.PointObj.GetGUID(name, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the GUID for the specified point object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the point object.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="GUID">The GUID (Global Unique ID) for the specified object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetGUID(string name,
            string GUID = "")
        {
            _callCode = _sapModel.PointObj.SetGUID(name, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves the name of the point element (analysis model) associated with a specified point object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="elementNames">The name of each element created from the specified object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetElement(string name,
            out string[] elementNames)
        {
            string elementName = "";
            _callCode = _sapModel.PointObj.GetElm(name, ref elementName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            elementNames = new string[1];
            elementNames[0] = elementName;
        }
        #endregion

        #region Axes
        /// <summary>
        /// This function retrieves the local axis angle assignment for the point element.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]
        /// For some objects, the following rotations are also performed:
        /// 2. Rotate about the resulting 2 axis by angle b.
        /// 3. Rotate about the resulting 1 axis by angle c.</param>
        /// <param name="isAdvanced">True: Object local axes orientation was obtained using advanced local axes parameters.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLocalAxes(string name,
            out AngleLocalAxes angleOffset,
            out bool isAdvanced)
        {
            isAdvanced = false;
            double angleA = 0;
            double angleB = 0;
            double angleC = 0;

            _callCode = _sapModel.PointObj.GetLocalAxes(name, 
                ref angleA, 
                ref angleB, 
                ref angleC,
                ref isAdvanced);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            angleOffset = new AngleLocalAxes
            {
                AngleA = angleA,
                AngleB = angleB,
                AngleC = angleC
            };
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
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
            _callCode = _sapModel.PointObj.SetLocalAxes(name, 
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

            _callCode = _sapModel.PointObj.GetLocalAxesAdvanced(name, ref isActive,
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

            _callCode = _sapModel.PointObj.SetLocalAxesAdvanced(name, isActive,
                            (int)axisVectorOption, axisCoordinateSystem, ref csiAxisVectorDirectionn, ref axisPoint, ref axisReferenceVector,
                            plane2,
                            (int)planeVectorOption, planeCoordinateSystem, ref csiPlaneVectorDirection, ref planePoint, ref planeReferenceVector,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Creation & Groups
        /// <summary>
        /// This function changes the name of an existing point object.
        /// </summary>
        /// <param name="currentName">The existing name of a defined point object.</param>
        /// <param name="newName">The new name for the point object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, string newName)
        {
            _callCode = _sapModel.PointObj.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// The function deletes special point objects that have no other objects connected to them.
        /// Point objects can be deleted only if they have no other objects(e.g., frame, cable, tendon, area, solid link) connected to them.
        /// If a point object is not specified to be a Special Point, the program automatically deletes that point object when it has no other objects connected to it.
        /// If a point object is specified to be a Special Point, to delete it, first delete all other objects connected to the point and then call this function to delete the point.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        public void Delete(string name)
        {
            DeleteSpecialPoint(name);
        }


        /// <summary>
        /// This function adds a point object to a model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// </summary>
        /// <param name="coordinate">Coordinate for the point object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// If a point is merged with another point, this will be the name of the point object with which it was merged.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all pointobjects have a merge number of zero.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void AddByCoordinate(Coordinate3DCartesian coordinate,
            ref string name,
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0)
        {
            _callCode = _sapModel.PointObj.AddCartesian(coordinate.X, coordinate.Y, coordinate.Z, ref name, userName, coordinateSystem, mergeOff, mergeNumber);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function adds a point object to a model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// </summary>
        /// <param name="coordinate">Coordinate for the point object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// If a point is merged with another point, this will be the name of the point object with which it was merged.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all pointobjects have a merge number of zero.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void AddByCoordinate(Coordinate3DCylindrical coordinate,
            ref string name,
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0)
        {
            _callCode = _sapModel.PointObj.AddCylindrical(coordinate.Radius, coordinate.Theta, coordinate.Z, ref name, userName, coordinateSystem, mergeOff, mergeNumber);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function adds a point object to a model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// </summary>
        /// <param name="coordinate">Coordinate for the point object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// If a point is merged with another point, this will be the name of the point object with which it was merged.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all pointobjects have a merge number of zero.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void AddByCoordinate(Coordinate3DSpherical coordinate,
            ref string name,
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0)
        {
            _callCode = _sapModel.PointObj.AddSpherical(coordinate.Radius, coordinate.Theta, coordinate.Phi, ref name, userName, coordinateSystem, mergeOff, mergeNumber);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


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
            _callCode = _sapModel.PointObj.SetGroupAssign(name, 
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
            _callCode = _sapModel.PointObj.GetGroupAssign(objectName,
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
            _callCode = _sapModel.PointObj.GetSelected(name, ref isSelected);
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
            _callCode = _sapModel.PointObj.SetSelected(name, 
                            isSelected, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Returns the name, x, y and z coordinates of all point object in the model in Present Units. 
        /// The coordinates are reported in the coordinate system specified by <paramref name="coordinateSystem"/>.
        /// </summary>
        /// <param name="names">The name of an existing point object.</param>
        /// <param name="coordinates">The cartesian x-, y-, z-coordinate of the specified point object in the specified coordinate system.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the joint coordinates are returned.</param>
        public void GetAllPoints(ref string[] names,
            ref Coordinate3DCartesian[] coordinates,
            string coordinateSystem = CoordinateSystems.Global)
        {
            double[] xCoordinates = new double[0];
            double[] yCoordinates = new double[0];
            double[] zCoordinates = new double[0];

            _callCode = _sapModel.PointObj.GetAllPoints(ref _numberOfItems, ref names, ref xCoordinates, ref yCoordinates, ref zCoordinates, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            coordinates = new Coordinate3DCartesian[_numberOfItems];
            for (int i = 0; i < _numberOfItems; i++)
            {
                coordinates[i].X = xCoordinates[i];
                coordinates[i].Y = yCoordinates[i];
                coordinates[i].Z = zCoordinates[i];
            }
        }
#endif
        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// This function retrieves the point mass assignment values for a point object. The masses are always returned in the point local coordinate system.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="masses">Mass assignment values.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMass(string name,
            out Mass masses)
        {
            double[] csiMasses = new double[6];

            _callCode = _sapModel.PointObj.GetMass(name, ref csiMasses);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            masses = new Mass();
            masses.FromArray(csiMasses);
        }

        /// <summary>
        /// This function assigns point mass to a point object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="masses">Mass assignment values.</param>
        /// <param name="isLocalCoordinateSystem">True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMass(string name,
            Mass masses,
            bool isLocalCoordinateSystem = true,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
            double[] csiMasses = masses.ToArray();

            _callCode = _sapModel.PointObj.SetMass(name, 
                            ref csiMasses, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType), 
                            isLocalCoordinateSystem, replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns point mass to a point object.
        /// The program calculates the mass by multiplying the specified values by the mass per unit volume of the specified material property.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="masses">Mass assignment values.</param>
        /// <param name="materialProperty">The name of an existing material property.</param>
        /// <param name="isLocalCoordinateSystem">True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMass(string name,
            MassVolume masses,
            string materialProperty,
            bool isLocalCoordinateSystem = true,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
            double[] csiMasses = masses.ToArray();

            _callCode = _sapModel.PointObj.SetMassByVolume(name, 
                            materialProperty, ref csiMasses, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType), 
                            isLocalCoordinateSystem, replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns point mass to a point object as a mass by weight.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="masses">Mass assignment values.</param>
        /// <param name="isLocalCoordinateSystem">True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMass(string name,
            MassWeight masses,
            bool isLocalCoordinateSystem = true,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
            double[] csiMasses = masses.ToArray();

            _callCode = _sapModel.PointObj.SetMassByWeight(name, 
                            ref csiMasses, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType),
                            isLocalCoordinateSystem, replace);
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
            _callCode = _sapModel.PointObj.DeleteMass(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Point Properties
        /// <summary>
        /// This function retrieves the special point status for a point object.
        /// Special points are allowed to exist in the model even if no objects (line, area, solid, link) are connected to them.
        /// Points that are not special are automatically deleted if no objects connect to them.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="isSpecialPoint">True: The point object is specified as a special point.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSpecialPoint(string name,
            ref bool isSpecialPoint)
        {
            _callCode = _sapModel.PointObj.GetSpecialPoint(name, ref isSpecialPoint);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the special point status for a point object.
        /// Special points are allowed to exist in the model even if no objects (line, area, solid, link) are connected to them.
        /// Points that are not special are automatically deleted if no objects connect to them.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="isSpecialPoint">True: The point object is specified as a special point.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSpecialPoint(string name,
            bool isSpecialPoint,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.SetSpecialPoint(name, 
                            isSpecialPoint, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// The function deletes special point objects that have no other objects connected to them.
        /// Point objects can be deleted only if they have no other objects(e.g., frame, cable, tendon, area, solid link) connected to them.
        /// If a point object is not specified to be a Special Point, the program automatically deletes that point object when it has no other objects connected to it.
        /// If a point object is specified to be a Special Point, to delete it, first delete all other objects connected to the point and then call this function to delete the point.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteSpecialPoint(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.DeleteSpecialPoint(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the merge number for a point object.
        /// By default the merge number for a point is zero.
        /// Points with different merge numbers are not automatically merged by the program.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="mergeNumber">The merge number assigned to the specified point object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMergeNumber(string name,
            ref int mergeNumber)
        {
            _callCode = _sapModel.PointObj.GetMergeNumber(name, ref mergeNumber);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns the merge number for a point object.
        /// By default the merge number for a point is zero.
        /// Points with different merge numbers are not automatically merged by the program.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="mergeNumber">The merge number assigned to the specified point object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMergeNumber(string name,
            int mergeNumber,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.SetMergeNumber(name, 
                            mergeNumber, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// This function retrieves the joint pattern value for a specific point object and joint pattern.
        /// Joint pattern values are unitless.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <param name="value">The value that the specified point object has for the specified joint pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPatternValue(string name,
            string patternName,
            ref double value)
        {
            _callCode = _sapModel.PointObj.GetPatternValue(name, patternName, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the joint pattern value for a specified point object and joint pattern.
        /// The joint pattern value is calculated as:
        /// Value = [(z – zpoint) * w] + u
        /// where z(<paramref name="zCoordinateAtZeroPressure" />), w(<paramref name="weightPerUnitVolume" />) and u(<paramref name="uniformForcePerUnitArea" />) are described in the Parameters section and zpoint is the Z coordinate of the considered point object in the present coordinate system.
        /// All appropriate unit conversions are used to calculate the value in the database units, but thereafter it is assumed to be unitless.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <param name="zCoordinateAtZeroPressure">The Z coordinate at zero pressure in the present coordinate system. [L]</param>
        /// <param name="weightPerUnitVolume">A weight per unit volume. [F/L^3]</param>
        /// <param name="uniformForcePerUnitArea">An added uniform force per unit area. [F/L^2]</param>
        /// <param name="restrictionCurrent">This restriction applies before the pattern value has been added to any existing pattern value assigned to the point object.</param>
        /// <param name="restrictionCombined">This restriction applies after the pattern value has been added to or replaced any existing pattern value assigned to the point object.
        /// This restriction applies even if there was no existing joint pattern value on the point object.</param>
        /// <param name="replace">True: Joint pattern value calculated as shown in the Remarks section replaces any previous joint pattern value for the point object.
        /// False: Joint pattern value calculated as shown in the Remarks section is added to any previous joint pattern value for the point object and then the Restriction items are checked.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPatternByPressure(string name,
            string patternName,
            double zCoordinateAtZeroPressure,
            double weightPerUnitVolume,
            double uniformForcePerUnitArea,
            ePatternRestriction restrictionCurrent,
            ePatternRestriction restrictionCombined = ePatternRestriction.AllValuesUsed,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
            int csiRestrictionCurrent = (int)restrictionCurrent;
            _callCode = _sapModel.PointObj.SetPatternByPressure(name, 
                            patternName, zCoordinateAtZeroPressure, weightPerUnitVolume, ref uniformForcePerUnitArea, ref csiRestrictionCurrent, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType), (int)restrictionCombined, replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the joint pattern value for a specified point object and joint pattern.
        /// The joint pattern value is calculated as:
        /// Value = ax + by + cz + d
        /// where a, b, c and d are function input parameters and x, y and z are the coordinates of the considered point object in the present coordinate system.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <param name="a">The x-coefficient in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="b">The y-coefficient a in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="c">The z-coefficient a in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="d">The constant in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="restrictionCombined">This restriction applies after the pattern value has been added to or replaced any existing pattern value assigned to the point object.
        /// This restriction applies even if there was no existing joint pattern value on the point object.</param>
        /// <param name="replace">True: Joint pattern value calculated as shown in the Remarks section replaces any previous joint pattern value for the point object.
        /// False: Joint pattern value calculated as shown in the Remarks section is added to any previous joint pattern value for the point object and then the Restriction items are checked.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPatternByCoordinates(string name,
            string patternName,
            double a,
            double b,
            double c,
            double d,
            ePatternRestriction restrictionCombined = ePatternRestriction.AllValuesUsed,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.SetPatternByXYZ(name, 
                            patternName, a, b, c, d, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType), 
                            (int)restrictionCombined, replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes all joint pattern assignments, associated with the specified joint pattern, from the specified point object(s).
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeletePatternValue(string name,
            string patternName,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.DeletePatternValue(name, 
                            patternName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif



        /// <summary>
        /// This function retrieves the panel zone assignment data for a point object.
        /// If no panel zone assignment is made to the point object, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="propertyType">Method by which properties are determined for panel zones.</param>
        /// <param name="thickness">The thickness of the doubler plate.
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.ElasticFromColumnAndDoublerPlate" />. [L]</param>
        /// <param name="k1">The spring stiffness for major axis bending (about the local 3 axis of the column and panel zone).
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]</param>
        /// <param name="k2">The spring stiffness for minor axis bending (about the local 2 axis of the column and panel zone).
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]</param>
        /// <param name="linkProperty">The name of the link property used to define the panel zone.
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromLink" />.</param>
        /// <param name="connectivity">Panel zone connection types.</param>
        /// <param name="localAxisFrom">Method by which the local axis is defined.
        /// The <paramref name="localAxisFrom" /> item can be <see cref="ePanelZoneLocalAxis.UserDefined" /> only when the <paramref name="propertyType" /> item is <see cref="ePanelZonePropertyType.FromLink" />.</param>
        /// <param name="localAxisAngle">This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromLink" /> and <paramref name="localAxisFrom" /> = <see cref="ePanelZoneLocalAxis.UserDefined" />.
        /// It is the angle measured counter clockwise from the positive global X-axis to the local 2-axis of the panel zone. [deg]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPanelZone(string name,
            ref ePanelZonePropertyType propertyType,
            ref double thickness,
            ref double k1,
            ref double k2,
            ref string linkProperty,
            ref ePanelZoneConnectivity connectivity,
            ref ePanelZoneLocalAxis localAxisFrom,
            ref double localAxisAngle)
        {
            int csiPropertyType = 0;
            int csiConnectivity = 0;
            int csiLocalAxisFrom = 0;

            _callCode = _sapModel.PointObj.GetPanelZone(name, ref csiPropertyType, ref thickness, ref k1, ref k2, ref linkProperty, ref csiConnectivity, ref csiLocalAxisFrom, ref localAxisAngle);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            propertyType = (ePanelZonePropertyType)csiPropertyType;
            connectivity = (ePanelZoneConnectivity)csiConnectivity;
            localAxisFrom = (ePanelZoneLocalAxis)csiLocalAxisFrom;
        }

        /// <summary>
        /// This function makes panel zone assignments to point objects. Any existing panel zone assignments are replaced by the new assignments.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="propertyType">Method by which properties are determined for panel zones.</param>
        /// <param name="thickness">The thickness of the doubler plate.
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.ElasticFromColumnAndDoublerPlate" />. [L]</param>
        /// <param name="k1">The spring stiffness for major axis bending (about the local 3 axis of the column and panel zone).
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]</param>
        /// <param name="k2">The spring stiffness for minor axis bending (about the local 2 axis of the column and panel zone).
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]</param>
        /// <param name="linkProperty">The name of the link property used to define the panel zone.
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromLink" />.</param>
        /// <param name="connectivity">Panel zone connection types.</param>
        /// <param name="localAxisFrom">Method by which the local axis is defined.
        /// The <paramref name="localAxisFrom" /> item can be <see cref="ePanelZoneLocalAxis.UserDefined" /> only when the <paramref name="propertyType" /> item is <see cref="ePanelZonePropertyType.FromLink" />.</param>
        /// <param name="localAxisAngle">This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromLink" /> and <paramref name="localAxisFrom" /> = <see cref="ePanelZoneLocalAxis.UserDefined" />.
        /// It is the angle measured counter clockwise from the positive global X-axis to the local 2-axis of the panel zone. [deg]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPanelZone(string name,
            ePanelZonePropertyType propertyType,
            double thickness,
            double k1,
            double k2,
            string linkProperty,
            ePanelZoneConnectivity connectivity,
            ePanelZoneLocalAxis localAxisFrom,
            double localAxisAngle,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.SetPanelZone(name, 
                            (int)propertyType, thickness, k1, k2, linkProperty, (int)connectivity, (int)localAxisFrom, localAxisAngle,  
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes all panel zone assignments from the specified point object(s).
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeletePanelZone(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.DeletePanelZone(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Support & Connections
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the diaphragm for a specified object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="diaphragmOption">The diaphragm option.</param>
        /// <param name="diaphragmName">The name of an existing diaphragm. 
        /// This item will only be filled if <paramref name="diaphragmOption"/> = <see cref="eDiaphragmOption.DefinedDiaphragm"/>.</param>
        public void GetDiaphragm(string name,
            ref eDiaphragmOption diaphragmOption,
            ref string diaphragmName)
        {
            CSiProgram.eDiaphragmOption csiDiaphragmOption = CSiProgram.eDiaphragmOption.Disconnect;

            _callCode = _sapModel.PointObj.GetDiaphragm(name, ref csiDiaphragmOption, ref diaphragmName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            diaphragmOption = EnumLibrary.Convert(csiDiaphragmOption, diaphragmOption);
        }

        /// <summary>
        /// Assigns a diaphragm to an object .
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="diaphragmOption">The diaphragm option.</param>
        /// <param name="diaphragmName">The name of an existing diaphragm.</param>
        public void SetDiaphragm(string name,
            eDiaphragmOption diaphragmOption,
            string diaphragmName = "")
        {
            CSiProgram.eDiaphragmOption csiDiaphragmOption = EnumLibrary.Convert<eDiaphragmOption, CSiProgram.eDiaphragmOption>(diaphragmOption);

            _callCode = _sapModel.PointObj.SetDiaphragm(name, csiDiaphragmOption, diaphragmName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#else
        /// <summary>
        /// This function returns a list of constraint assignments made to one or more specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of constraint assignments returned.</param>
        /// <param name="pointNames">The name of the point element to which the specified constraint assignment applies.</param>
        /// <param name="constraintNames">The name of the constraint that is assigned to the point element specified by the <paramref name="pointNames" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetConstraint(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] constraintNames,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.GetConstraint(name, ref numberItems, 
                            ref pointNames, ref constraintNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function makes joint constraint assignments to point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="constraintName">The name of the existing joint constraint.</param>
        /// <param name="replace">True: All existing point assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetConstraint(string name,
            string constraintName,
            bool replace = true,
            eItemType itemType = eItemType.Group)
        {
            _callCode = _sapModel.PointObj.SetConstraint(name, 
                            ref constraintName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType), replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes all constraint assignments from the specified point object(s).
        /// </summary>
        /// <param name="name">The name of an existing point object or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteConstraint(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.DeleteConstraint(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        /// <summary>
        /// This function returns a list of constraint assignments made to one or more specified point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="degreesOfFreedom">These are the restraint assignments for each local degree of freedom (DOF), where 'True' means the DOF is fixed.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetRestraint(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom)
        {

            bool[] values = new bool[0];

            _callCode = _sapModel.PointObj.GetRestraint(name, ref values);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom.FromArray(values);
        }

        /// <summary>
        /// This function returns a list of constraint assignments made to one or more specified point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="degreesOfFreedom">These are the restraint assignments for each local degree of freedom (DOF), where 'True' means the DOF is fixed.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetRestraint(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            eItemType itemType = eItemType.Object)
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();

            _callCode = _sapModel.PointObj.SetRestraint(name, 
                            ref csiDegreesOfFreedom, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes all restraint assignments from the specified point object(s).
        /// </summary>
        /// <param name="name">The name of an existing point object or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteRestraint(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.DeleteRestraint(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// This function retrieves uncoupled spring stiffness assignments for a point object;
        /// that is, it retrieves the diagonal terms in the 6x6 spring matrix for the point object.
        /// The spring stiffnesses reported are the sum of all springs assigned to the point object either directly or indirectly through line, area and solid spring assignments.
        /// The spring stiffness values are reported in the point local coordinate system.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="stiffnesses">Spring stiffness values for each decoupled degree of freedom.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSpring(string name,
            ref Stiffness stiffnesses)
        {
            double[] csiStiffnesses = new double[0];

            _callCode = _sapModel.PointObj.GetSpring(name, ref csiStiffnesses);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stiffnesses.FromArray(csiStiffnesses);
        }

        /// <summary>
        /// This function assigns uncoupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="stiffnesses">Spring stiffness values for each decoupled degree of freedom.</param>
        /// <param name="isLocalCoordinateSystem">True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSpring(string name,
            Stiffness stiffnesses,
            bool isLocalCoordinateSystem = false,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
            double[] csiStiffnesses = stiffnesses.ToArray();

            _callCode = _sapModel.PointObj.SetSpring(name, 
                            ref csiStiffnesses, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType), 
                            isLocalCoordinateSystem, replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves coupled spring stiffness assignments for a point object;
        /// that is, it retrieves the spring matrix of 21 stiffness values for the point object.
        /// The spring stiffnesses reported are the sum of all springs assigned to the point element either directly or indirectly through line, area and solid spring assignments.
        /// The spring stiffness values are reported in the point local coordinate system.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="stiffnesses">Spring stiffness values for each coupled degree of freedom.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSpringCoupled(string name,
            ref StiffnessCoupled stiffnesses)
        {
            double[] csiStiffnesses = new double[0];

            _callCode = _sapModel.PointObj.GetSpringCoupled(name, ref csiStiffnesses);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stiffnesses.FromArray(csiStiffnesses);
        }

        /// <summary>
        /// This function assigns coupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="stiffnesses">Spring stiffness values for each coupled degree of freedom.</param>
        /// <param name="isLocalCoordinateSystem">True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSpringCoupled(string name,
            StiffnessCoupled stiffnesses,
            bool isLocalCoordinateSystem = false,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
            double[] csiStiffnesses = stiffnesses.ToArray();

            _callCode = _sapModel.PointObj.SetSpringCoupled(name, 
                            ref csiStiffnesses, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType), 
                            isLocalCoordinateSystem, replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the named spring property assignment for an object.</summary>
        /// <param name="name">The name of an existing object .</param>
        /// <param name="nameSpring">The name of an existing point spring property.</param>
        public void GetSpringAssignment(string name,
            ref string nameSpring)
        {
            _callCode = _sapModel.PointObj.GetSpringAssignment(name, ref nameSpring);
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
            _callCode = _sapModel.PointObj.SetSpringAssignment(name, nameSpring);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        // ===

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
            _callCode = _sapModel.PointObj.DeleteSpring(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function indicates if the spring assignments to a point object are coupled, that is, if there are off-diagonal terms in the 6x6 spring matrix for the point element.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <returns><c>true</c> if [is spring coupled] [the specified name]; otherwise, <c>false</c>.</returns>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public bool IsSpringCoupled(string name)
        {
            bool isCoupled = false;

            _callCode = _sapModel.PointObj.IsSpringCoupled(name, ref isCoupled);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return isCoupled;
        }


        #endregion

        #region Loads
        // LoadForce
        /// <summary>
        /// This function returns a list of force load assignments made to one or more specified point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object, or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of force load assignments returned.</param>
        /// <param name="pointNames">The name of the point object to which the specified force load assignment applies.</param>
        /// <param name="loadPatterns">The name of the load pattern for each load.</param>
        /// <param name="loadPatternSteps">The load pattern step for each load.
        /// In most cases this item does not apply and will be returned as 0.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="forces">The forces assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for objects corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadForce(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Loads[] forces,
            eItemType itemType = eItemType.Object)
        {
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.PointObj.GetLoadForce(name, ref numberItems, 
                            ref pointNames, ref loadPatterns, ref loadPatternSteps, ref coordinateSystem, 
                            ref F1, ref F2, ref F3, ref M1, ref M2, ref M3, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forces = new Loads[numberItems - 1];
            for (int i = 0; i < numberItems; i++)
            {
                forces[i].F1 = F1[i];
                forces[i].F2 = F2[i];
                forces[i].F3 = F3[i];
                forces[i].M1 = M1[i];
                forces[i].M2 = M2[i];
                forces[i].M3 = M3[i];
            }
        }


        /// <summary>
        /// This function makes point load assignments to point objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadPattern">The name of the load pattern.</param>
        /// <param name="force">The force assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="replace">True: All previous point loads, if any, assigned to the specified point object(s) in the specified load pattern are deleted before making the new assignment.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadForce(string name,
            string loadPattern,
            Loads force,
            bool replace = false, 
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
            double[] values = force.ToArray();

            _callCode = _sapModel.PointObj.SetLoadForce(name, 
                            loadPattern, ref values, replace, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes all point load assignments, for the specified load pattern, from the specified point object(s).
        /// </summary>
        /// <param name="name">The name of an existing point object, or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of a defined load pattern.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are deleted for the objects corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are deleted for objects corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadForce(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.DeleteLoadForce(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // LoadForceWithGUID
        /// <summary>
        /// This function retrieves the joint force load assignments to point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object, or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of force load assignments returned.</param>
        /// <param name="pointNames">The name of the point object to which the specified force load assignment applies.</param>
        /// <param name="loadPatterns">The name of the load pattern for each load.</param>
        /// <param name="GUIDs">This is an array that includes the global unique ID of the distributed load.</param>
        /// <param name="loadPatternSteps">The load pattern step for each load.
        /// In most cases this item does not apply and will be returned as 0.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="forces">The forces assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for objects corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadForceWithGUID(string name, 
            ref int numberItems, 
            ref string[] pointNames, 
            ref string[] loadPatterns, 
            ref string[] GUIDs, 
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem, 
            ref Loads[] forces, 
            eItemType itemType = eItemType.Object)
        {
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.PointObj.GetLoadForceWithGUID(name, ref numberItems, 
                            ref pointNames, ref loadPatterns, ref loadPatternSteps, ref coordinateSystem, 
                            ref F1, ref F2, ref F3, ref M1, ref M2, ref M3, ref GUIDs, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function makes point load assignments to point objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group.</param>
        /// <param name="loadPattern">The name of the load pattern.</param>
        /// <param name="force">The force assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="GUID">This is the global unique ID of a load force assigned to the point object or if it is not the global unique id of a load force assigned to the point object and it is not blank, the global unique ID which is assigned to the newly assigned load.
        /// If left blank, a new load is assigned to the point object and the value of this parameter is set to the global unique ID of the newly assigned load.</param>
        /// <param name="replace">True: If the input GUID is not the GUID of any load force assigned to the point object, all previous loads force, if any, assigned to the specified point object, in the specified load pattern, are deleted before making the new assignment.
        /// If the input GUID is the GUID of a load force already assigned to the point object, the parameters of the distributed load are updated with the values provided and this item is ignored.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadForceWithGUID(string name,
            string loadPattern,
            Loads force,
            string GUID,
            bool replace = false,
            string coordinateSystem = CoordinateSystems.Global)
        {
            double[] values = force.ToArray();

            _callCode = _sapModel.PointObj.SetLoadForceWithGUID(name, loadPattern, ref values, ref GUID, replace, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the point load assignment with the specified global unique ID for the specified point object.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="GUID">The global unique ID of one of the point loads on that point object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadForceWithGUID(string name,
            string GUID)
        {
            _callCode = _sapModel.PointObj.DeleteLoadForceWithGUID(name, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif



        // LoadDisplacement
        /// <summary>
        /// This function returns a list of ground displacement load assignments made to one or more specified point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object, or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of ground displacement load assignments returned.</param>
        /// <param name="pointNames">The name of the point object to which the specified ground displacement load assignment applies.</param>
        /// <param name="loadPatterns">The name of the load pattern for each load.</param>
        /// <param name="loadPatternSteps">The load pattern step for each load.
        /// In most cases this item does not apply and will be returned as 0.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the ground displacement.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="displacements">The ground displacements assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for objects corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadDisplacement(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Displacements[] displacements,
            eItemType itemType = eItemType.Object)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.PointObj.GetLoadDispl(name, ref numberItems, 
                            ref pointNames, ref loadPatterns, ref loadPatternSteps, ref coordinateSystem, 
                            ref U1, ref U2, ref U3, ref R1, ref R2, ref R3, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            displacements = new Displacements[numberItems - 1];
            for (int i = 0; i < numberItems; i++)
            {
                displacements[i].UX = U1[i];
                displacements[i].UY = U2[i];
                displacements[i].UZ = U3[i];
                displacements[i].RX = R1[i];
                displacements[i].RY = R2[i];
                displacements[i].RZ = R3[i];
            }
        }

        /// <summary>
        /// This function makes ground displacement load assignments to point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object, or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern.</param>
        /// <param name="force">The force assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="replace">True: All previous ground displacement loads, if any, assigned to the specified point object(s) in the specified load pattern are deleted before making the new assignment.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadDisplacement(string name,
            string loadPattern,
            Loads force,
            bool replace = false,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
            double[] values = force.ToArray();

            _callCode = _sapModel.PointObj.SetLoadDispl(name, 
                            loadPattern, ref values, replace, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function deletes all ground displacement load assignments, for the specified load pattern, from the specified point object(s).
        /// </summary>
        /// <param name="name">The name of an existing point object, or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of a defined load pattern.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are deleted for the objects corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are deleted for objects corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteLoadDisplacement(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.PointObj.DeleteLoadDispl(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
