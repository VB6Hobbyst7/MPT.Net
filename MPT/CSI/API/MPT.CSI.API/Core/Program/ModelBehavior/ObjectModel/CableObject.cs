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
    /// Represents the cable object in the application.
    /// </summary>
    public class CableObject : CSiApiBase, ICableObject
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="CableObject"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public CableObject(CSiApiSeed seed) : base(seed) { }
#endregion
        
#region Query
        /// <summary>
        /// This function returns the total number of defined cable properties in the model.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _sapModel.CableObj.Count();
        }

        /// <summary>
        /// This function retrieves the names of all defined cable properties.
        /// </summary>
        /// <param name="names">Cable object names retrieved by the program.</param>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.CableObj.GetNameList(ref _numberOfItems, ref names);
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
            bool isGlobal = true)
        {
            directionCosines = new double[9];
            _callCode = _sapModel.CableObj.GetTransformationMatrix(nameObject, ref directionCosines, isGlobal);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of the point objects that define a cable object.
        /// </summary>
        /// <param name="name">The name of an existing cable object.</param>
        /// <param name="points">The names of the points that defined the cable object.
        /// The point names are listed in the positive order around the object.</param>
        public void GetPoints(string name, 
            out string[] points)
        {
            string point1 = "";
            string point2 = "";

            _callCode = _sapModel.CableObj.GetPoints(name, ref point1, ref point2);
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
        public void GetGUID(string name,
            out string GUID)
        {
            GUID = string.Empty;
            _callCode = _sapModel.CableObj.GetGUID(name, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the GUID for the specified object. 
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="GUID">The GUID (Global Unique ID) for the specified object.</param>
        public void SetGUID(string name,
            string GUID = "")
        {
            _callCode = _sapModel.CableObj.SetGUID(name, GUID);
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
            double[] relativeDistanceI;
            double[] relativeDistanceJ;

            GetElement(name, out elementNames, out relativeDistanceI, out relativeDistanceJ);
        }

        /// <summary>
        /// This function retrieves the name of the element (analysis model) associated with a specified object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="elementNames">The name of each element created from the specified object.</param>
        /// <param name="relativeDistanceI">The relative distance along the frame object to the I-End of the cable element.</param>
        /// <param name="relativeDistanceJ">The relative distance along the frame object to the J-End of the cable element.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetElement(string name,
            out string[] elementNames,
            out double[] relativeDistanceI,
            out double[] relativeDistanceJ)
        {
            elementNames = new string[0];
            relativeDistanceI = new double[0];
            relativeDistanceJ = new double[0];

            _callCode = _sapModel.CableObj.GetElm(name, 
                ref _numberOfItems, 
                ref elementNames, 
                ref relativeDistanceI,
                ref relativeDistanceJ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Modifiers
        /// <summary>
        /// This function retrieves the modifier assignment for cable objects. 
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing cable object.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        public void GetModifiers(string name, 
                        out CableModifier modifiers)
        {
            double[] csiModifiers = new double[3];

            _callCode = _sapModel.CableObj.GetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            modifiers = new CableModifier();
            modifiers.FromArray(csiModifiers);
        }

        /// <summary>
        /// This function defines the modifier assignment for cable objects. 
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing cable object.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        public void SetModifiers(string name,
            CableModifier modifiers)
        {
            if (modifiers == null) { return; }
            double[] csiModifiers = modifiers.ToArray();

            _callCode = _sapModel.CableObj.SetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function deletes a modifier assignment. 
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are deleted for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are deleted for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are deleted for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void DeleteModifiers(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.DeleteModifiers(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion

#region Creation & Groups
        /// <summary>
        /// This function changes the name of an existing cable object.
        /// </summary>
        /// <param name="currentName">The existing name of a defined cable object.</param>
        /// <param name="newName">The new name for the cable object.</param>
        public void ChangeName(string currentName, string newName)
        {
            _callCode = _sapModel.CableObj.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// The function deletes a specified cable object.
        /// It returns an error if the specified object can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing cable object.</param>
        public void Delete(string name)
        {
            _callCode = _sapModel.CableObj.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function adds a new object whose corner points are at the specified coordinates.
        /// </summary>
        /// <param name="coordinates">Coordinates for the corner points of the object. 
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem"/> item.
        /// Two coordinates are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object. 
        /// If no <paramref name="userName"/> is specified, the program assigns a default name to the object. 
        /// If a <paramref name="userName"/> is specified and that name is not used for another object, the <paramref name="userName"/> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object. 
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object. 
        /// If a <paramref name="userName"/> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName"/>.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        public void AddByCoordinate(ref Coordinate3DCartesian[] coordinates,
            ref string name,
            string nameProperty = "Default",
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
            if (coordinates.Length != 2)
            {
                throw new CSiException("Two coordinates must be provided for a cable object. " +
                                       "Provided: " + coordinates.Length);
            }

            _callCode = _sapModel.CableObj.AddByCoord(
                coordinates[0].X, coordinates[0].Y, coordinates[0].Z,
                coordinates[1].X, coordinates[1].Y, coordinates[1].Z,
                ref name, nameProperty, userName, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function adds a new object whose corner points are specified by name.
        /// </summary>
        /// <param name="pointNames">Names of the point objects that define the corner points of the added object.
        /// Two points are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object. 
        /// If no <paramref name="userName"/> is specified, the program assigns a default name to the object. 
        /// If a <paramref name="userName"/> is specified and that name is not used for another object, the <paramref name="userName"/> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object. 
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object. 
        /// If a <paramref name="userName"/> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName"/>.</param>
        public void AddByPoint(string[] pointNames,
            ref string name,
            string nameProperty = "Default",
            string userName = "")
        {
            if (pointNames.Length != 2)
            {
                throw new CSiException("Two points must be provided for a cable object. " +
                                       "Provided: " + pointNames.Length);
            }

            _callCode = _sapModel.CableObj.AddByPoint(pointNames[0], pointNames[1], ref name, nameProperty, userName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function adds or removes objects from a specified group.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="groupName">The name of an existing group to which the assignment is made.</param>
        /// <param name="remove">False: The specified objects are added to the group specified by the <paramref name="groupName"/> item.  
        /// True: The objects are removed from the group.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetGroupAssign(string name,
            string groupName,
            bool remove = false,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetGroupAssign(name, 
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
            _callCode = _sapModel.CableObj.GetGroupAssign(objectName,
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
        public void GetSelected(string name,
            out bool isSelected)
        {
            isSelected = false;
            _callCode = _sapModel.CableObj.GetSelected(name, ref isSelected);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the selected status for an object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="isSelected">True: The specified object is selected.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the selected status is set for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the selected status is set for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the selected status is set for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetSelected(string name,
            bool isSelected,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetSelected(name, 
                            isSelected, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion

#region Cross-Section & Material Properties
        /// <summary>
        /// This function retrieves the section property assigned to a cable object.
        /// </summary>
        /// <param name="name">The name of a defined cable object.</param>
        /// <param name="propertyName">The name of the section property assigned to the cable object.</param>
        public void GetSection(string name,
            out string propertyName)
        {
            propertyName = string.Empty;
            _callCode = _sapModel.CableObj.GetProperty(name, ref propertyName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns the section property to a cable object.
        /// </summary>
        /// <param name="name">The name of a defined cable object.</param>
        /// <param name="propertyName">The name of the section property assigned to the cable object.</param>
        /// <param name="itemType">If this item is Object, the assignment is made to the cable object specified by the Name item.
        /// If this item is Group, the assignment is made to all cable objects in the group specified by the Name item.
        /// If this item is SelectedObjects, assignment is made to all selected cable objects, and the Name item is ignored.</param>
        public void SetSection(string name, 
            string propertyName, 
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetProperty(name, 
                            propertyName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves the mass per unit length assignment from objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        public void GetMass(string name,
            out double value)
        {
            value = 0;
            _callCode = _sapModel.CableObj.GetMass(name, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns mass per unit length to objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass. 
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetMass(string name,
            double value,
            bool replace,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetMass(name, 
                            value, replace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes all mass assignments for the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are deleted for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are deleted for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are deleted for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void DeleteMass(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.DeleteMass(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves frame object insertion point assignments. 
        /// The assignments include the cardinal point and end joint offsets.
        /// </summary>
        /// <param name="name">The name of an existing frame object</param>
        /// <param name="isStiffnessTransformed">True: Frame object stiffness is transformed for cardinal point and joint offsets from the frame section centroid.</param>
        /// <param name="offsetDistancesI">Three joint offset distances, in the Global coordinate system, at the I-End of the line element. [L]</param>
        /// <param name="offsetDistancesJ">Three joint offset distances, in the Global coordinate system, at the J-End of the line element. [L]</param>
        /// <param name="coordinateSystem">This is <see cref="CoordinateSystems.Local"/> or the name of a defined coordinate system. 
        /// It is the coordinate system in which the <paramref name="offsetDistancesI"/> and <paramref name="offsetDistancesJ"/> items are specified.</param>
        public void GetInsertionPoint(string name,
            ref Displacements offsetDistancesI,
            ref Displacements offsetDistancesJ,
            ref bool isStiffnessTransformed,
            ref string coordinateSystem)
        {
            double[] offset1 = new double[0];
            double[] offset2 = new double[0];

            _callCode = _sapModel.CableObj.GetInsertionPoint(name, ref isStiffnessTransformed, ref offset1, ref offset2, ref coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            if (offset1.Length < 3) { throw new CSiException("Array " + offset1 + " should have 3 elements, but has " + offset1.Length + " elements."); }
            offsetDistancesI.UX = offset1[0];
            offsetDistancesI.UY = offset1[1];
            offsetDistancesI.UZ = offset1[2];

            if (offset2.Length < 3) { throw new CSiException("Array " + offset2 + " should have 3 elements, but has " + offset2.Length + " elements."); }
            offsetDistancesJ.UX = offset2[0];
            offsetDistancesJ.UY = offset2[1];
            offsetDistancesJ.UZ = offset2[2];
        }

        /// <summary>
        /// This function assigns frame object insertion point data. 
        /// The assignments are reported as end joint offsets.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="isStiffnessTransformed">True: The frame object stiffness is transformed for cardinal point and joint offsets from the frame section centroid.</param>
        /// <param name="offsetDistancesI">Three joint offset distances, in the Global coordinate system, at the I-End of the frame object. [L]</param>
        /// <param name="offsetDistancesJ">Three joint offset distances, in the Global coordinate system, at the J-End of the frame object. [L]</param>
        /// <param name="coordinateSystem">This is <see cref="ePDeltaDirection.Local_1"/> or the name of a defined coordinate system. 
        /// It is the coordinate system in which the <paramref name="offsetDistancesI"/> and <paramref name="offsetDistancesJ"/> items are specified.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are set for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are set for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are set for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetInsertionPoint(string name,
            Displacements offsetDistancesI,
            Displacements offsetDistancesJ,
            bool isStiffnessTransformed,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
            double[] offset1 = offsetDistancesI.ToArray();
            double[] offset2 = offsetDistancesJ.ToArray();

            _callCode = _sapModel.CableObj.SetInsertionPoint(name, 
                            isStiffnessTransformed, ref offset1, ref offset2, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves the material temperature assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element.</param>
        /// <param name="temperature">This is the material temperature value assigned to the element. [T]</param>
        /// <param name="patternName">This is blank or the name of a defined joint pattern. 
        /// If it is blank, the material temperature for the line element is uniform along the element at the value specified by <paramref name="temperature"/>.
        /// If <paramref name="patternName"/> is the name of a defined joint pattern, the material temperature for the line element may vary from one end to the other.
        /// The material temperature at each end of the element is equal to the specified temperature multiplied by the pattern value at the joint at the end of the line element.</param>
        public void GetMaterialTemperature(string name,
            out double temperature,
            out string patternName)
        {
            temperature = 0;
            patternName = string.Empty;
            _callCode = _sapModel.CableObj.GetMatTemp(name, ref temperature, ref patternName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the material temperature assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element.</param>
        /// <param name="temperature">This is the material temperature value assigned to the element. [T]</param>
        /// <param name="patternName">This is blank or the name of a defined joint pattern. 
        /// If it is blank, the material temperature for the line element is uniform along the element at the value specified by <paramref name="temperature"/>.
        /// If <paramref name="patternName"/> is the name of a defined joint pattern, the material temperature for the line element may vary from one end to the other.
        /// The material temperature at each end of the element is equal to the specified temperature multiplied by the pattern value at the joint at the end of the line element.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetMaterialTemperature(string name,
            double temperature,
            string patternName,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetMatTemp(name, 
                            temperature, patternName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the material overwrite assigned to an element, if any. 
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <param name="name">The name of a defined element.</param>
        /// <param name="propertyName">This is None, indicating that no material overwrite exists for the specified element, or it is the name of an existing material property.</param>
        public void GetMaterialOverwrite(string name,
            ref string propertyName)
        {
            _callCode = _sapModel.CableObj.GetMaterialOverwrite(name, ref propertyName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the material overwrite assignment for objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="propertyName">This is None or a blank string, indicating that no material overwrite exists for the specified element, or it is the name of an existing material property.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are retrieved for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are retrieved for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetMaterialOverwrite(string name,
            string propertyName,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetMaterialOverwrite(name, 
                            propertyName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion

#region Cable Properties
        /// <summary>
        /// This function retrieves definition data for a specified cable object.
        /// </summary>
        /// <param name="name">The name of a defined cable object.</param>
        /// <param name="cableType">Cable definition parameter.</param>
        /// <param name="numberOfSegments">This is the number of segments into which the program internally divides the cable.</param>
        /// <param name="weight">The added weight per unit length used when calculating the cable shape. [F/L]</param>
        /// <param name="projectedLoad">The projected uniform gravity load used when calculating the cable shape. [F/L]</param>
        /// <param name="useDeformedGeometry">True: The program uses the deformed geometry for the cable object; otherwise it uses the undeformed geometry.</param>
        /// <param name="isModelUsingFrameElements">True: The analysis model uses frame elements to model the cable instead of using cable elements.</param>
        /// <param name="cableParameters">Parameters related to cable shape.
        /// Parameter(0) = Tension at I-End [F].
        /// Parameter(1) = Tension at J-End[F].
        /// Parameter(2) = Horizontal tension component[F].
        /// Parameter(3) = Maximum deformed vertical sag[L].
        /// Parameter(4) = Deformed low-point vertical sag[L].
        /// Parameter(5) = Deformed length[L].
        /// Parameter(6) = Deformed relative length.
        /// Parameter(7) = Maximum undeformed vertical sag[L].
        /// Parameter(8) = Undeformed low-point vertical sag[L].
        /// Parameter(9) = Undeformed length[L].
        /// Parameter(10) = Undeformed relative length.</param>
        public void GetCableData(string name,
            ref eCableGeometryDefinition cableType,
            ref int numberOfSegments,
            ref double weight,
            ref double projectedLoad,
            ref bool useDeformedGeometry,
            ref bool isModelUsingFrameElements,
            ref double[] cableParameters)
        {
            int csiCableType = 0;

            _callCode = _sapModel.CableObj.GetCableData(name, ref csiCableType, ref numberOfSegments, ref weight, ref projectedLoad, ref useDeformedGeometry, ref isModelUsingFrameElements, ref cableParameters);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            cableType = (eCableGeometryDefinition) csiCableType;
        }

        /// <summary>
        /// This function retrieves definition data for a specified cable object.
        /// </summary>
        /// <param name="name">The name of a defined cable object.</param>
        /// <param name="cableType">Cable definition parameter.</param>
        /// <param name="numberOfSegments">This is the number of segments into which the program internally divides the cable.</param>
        /// <param name="weight">The added weight per unit length used when calculating the cable shape. [F/L]</param>
        /// <param name="projectedLoad">The projected uniform gravity load used when calculating the cable shape. [F/L]</param>
        /// <param name="value">Value of the parameter used to define the cable shape. 
        /// The item that <paramref name="value"/> represents depends on the <paramref name="cableType"/> item.
        /// <paramref name="cableType"/> = <see cref="eCableGeometryDefinition.MinTensionIEnd"/>: Not Used.
        /// <paramref name="cableType"/> = <see cref="eCableGeometryDefinition.MinTensionJEnd"/>: Not Used.
        /// <paramref name="cableType"/> = <see cref="eCableGeometryDefinition.TensionIEnd"/>: Tension at I-End[F].
        /// <paramref name="cableType"/> = <see cref="eCableGeometryDefinition.TensionJEnd"/>: Tension at J-End[F].
        /// <paramref name="cableType"/> = <see cref="eCableGeometryDefinition.HorizontalTensionComponent"/>: Horizontal tension component[F].
        /// <paramref name="cableType"/> = <see cref="eCableGeometryDefinition.MaximumVerticalSag"/>: Maximum vertical sag[L]. 
        /// <paramref name="cableType"/> = <see cref="eCableGeometryDefinition.LowPointVerticalSag"/>: Low-point vertical sag[L]. 
        /// <paramref name="cableType"/> = <see cref="eCableGeometryDefinition.UndeformedLength"/>: Undeformed length[L]. 
        /// <paramref name="cableType"/> = <see cref="eCableGeometryDefinition.RelativeUndeformedLength"/>: Relative undeformed length.</param>
        /// <param name="useDeformedGeometry">True: The program uses the deformed geometry for the cable object; otherwise it uses the undeformed geometry.</param>
        /// <param name="isModelUsingFrameElements">True: The analysis model uses frame elements to model the cable instead of using cable elements.</param>
        public void SetCableData(string name,
            eCableGeometryDefinition cableType,
            int numberOfSegments,
            double weight,
            double projectedLoad,
            double value,
            bool useDeformedGeometry = false,
            bool isModelUsingFrameElements = false)
        {
            _callCode = _sapModel.CableObj.SetCableData(name, (int)cableType, numberOfSegments, weight, projectedLoad, value, useDeformedGeometry, isModelUsingFrameElements);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves geometric data for a specified cable object.
        /// </summary>
        /// <param name="name">The name of a defined cable object.</param>
        /// <param name="numberPoints">The number of points defining the cable geometry.</param>
        /// <param name="coordinates">Coordinates of the considered point on the cable in the coordinate system specified by the <paramref name="coordinateSystem"/> item. [L]</param>
        /// <param name="verticalSag">The cable vertical sag, measured from the chord, at the considered point. [L]</param>
        /// <param name="distanceAbsolute">The distance along the cable, measured from the cable I-End, to the considered point. [L]</param>
        /// <param name="distanceRelative">The relative distance along the cable, measured from the cable I-End, to the considered point.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the x, y and z coordinates are to be reported.
        /// It is Local or the name of a defined coordinate system.</param>
        public void GetCableGeometry(string name,
            ref int numberPoints,
            ref Coordinate3DCartesian[] coordinates,
            ref double[] verticalSag,
            ref double[] distanceAbsolute,
            ref double[] distanceRelative,
            string coordinateSystem = CoordinateSystems.Global)
        {
            double[] x = new double[0];
            double[] y = new double[0];
            double[] z = new double[01];

            _callCode = _sapModel.CableObj.GetCableGeometry(name, ref numberPoints, ref x, ref y, ref z, ref verticalSag, ref distanceAbsolute, ref distanceRelative, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            coordinates = new Coordinate3DCartesian[numberPoints - 1];
            for (int i = 0; i < numberPoints; i++)
            {
                coordinates[i].X = x[i];
                coordinates[i].Y = y[i];
                coordinates[i].Z = z[i];
            }
        }


        /// <summary>
        /// This function retrieves object output station data.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="outputStationType">Indicates how the output stations are specified.</param>
        /// <param name="maxStationSpacing">The maximum segment size, that is, the maximum station spacing. [L] 
        /// This item applies only when <paramref name="outputStationType"/> = <see cref="eOutputStationType.MaxSpacing"/>.</param>
        /// <param name="minStationNumber">The minimum number of stations. 
        /// This item applies only when <paramref name="outputStationType"/> = <see cref="eOutputStationType.MinStations"/>.</param>
        /// <param name="noOutputAndDesignAtElementEnds">True: No additional output stations are added at the ends of line elements when the object is internally meshed.</param>
        /// <param name="noOutputAndDesignAtPointLoads">True: No additional output stations are added at point load locations.</param>
        public void GetOutputStations(string name,
            ref eOutputStationType outputStationType,
            ref double maxStationSpacing,
            ref int minStationNumber,
            ref bool noOutputAndDesignAtElementEnds,
            ref bool noOutputAndDesignAtPointLoads)
        {
            int csiOutputStationType = 0;
            _callCode = _sapModel.CableObj.GetOutputStations(name, ref csiOutputStationType, ref maxStationSpacing, ref minStationNumber, ref noOutputAndDesignAtElementEnds, ref noOutputAndDesignAtPointLoads);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            outputStationType = (eOutputStationType)csiOutputStationType;
        }

        /// <summary>
        /// This function assigns object output station data.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="outputStationType">Indicates how the output stations are specified.</param>
        /// <param name="maxStationSpacing">The maximum segment size, that is, the maximum station spacing. [L] 
        /// This item applies only when <paramref name="outputStationType"/> = <see cref="eOutputStationType.MaxSpacing"/>.</param>
        /// <param name="minStationNumber">The minimum number of stations. 
        /// This item applies only when <paramref name="outputStationType"/> = <see cref="eOutputStationType.MinStations"/>.</param>
        /// <param name="noOutputAndDesignAtElementEnds">True: No additional output stations are added at the ends of line elements when the cable object is internally meshed.</param>
        /// <param name="noOutputAndDesignAtPointLoads">True: No additional output stations are added at point load locations.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetOutputStations(string name,
            eOutputStationType outputStationType,
            double maxStationSpacing,
            int minStationNumber,
            bool noOutputAndDesignAtElementEnds,
            bool noOutputAndDesignAtPointLoads,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetOutputStations(name, 
                            (int)outputStationType, maxStationSpacing, minStationNumber, 
                            noOutputAndDesignAtElementEnds, noOutputAndDesignAtPointLoads, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion

#region Loads
        // LoadGravity
        /// <summary>
        /// This function retrieves the gravity load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="numberItems">The total number of gravity loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each gravity load.</param>
        /// <param name="loadPatterns">The name of the coordinate system in which the gravity load multipliers are specified.</param>
        /// <param name="coordinateSystems">The name of the coordinate system associated with each gravity load.</param>
        /// <param name="xLoadMultiplier">Gravity load multipliers in the x direction of the specified coordinate system.</param>
        /// <param name="yLoadMultiplier">Gravity load multipliers in the y direction of the specified coordinate system.</param>
        /// <param name="zLoadMultiplier">Gravity load multipliers in the z direction of the specified coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are retrieved for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are retrieved for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are retrieved for all selected objects, and the <paramref name="name"/> item is ignored.</param>
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
            _callCode = _sapModel.CableObj.GetLoadGravity(name, ref numberItems, 
                            ref names, ref loadPatterns, ref coordinateSystems, 
                            ref xLoadMultiplier, ref yLoadMultiplier, ref zLoadMultiplier, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns gravity loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the gravity load.</param>
        /// <param name="xLoadMultiplier">Gravity load multiplier in the x direction of the specified coordinate system.</param>
        /// <param name="yLoadMultiplier">Gravity load multiplier in the y direction of the specified coordinate system.</param>
        /// <param name="zLoadMultiplier">Gravity load multiplier in the z direction of the specified coordinate system.</param>
        ///  <param name="coordinateSystem">The name of the coordinate system associated with the gravity load.</param>
        /// <param name="replace">True: All previous gravity loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetLoadGravity(string name,
            string loadPattern,
            double xLoadMultiplier,
            double yLoadMultiplier,
            double zLoadMultiplier,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetLoadGravity(name, 
                            loadPattern, xLoadMultiplier, yLoadMultiplier, zLoadMultiplier, replace, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the gravity load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are deleted for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are deleted for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are deleted for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void DeleteLoadGravity(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.DeleteLoadGravity(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        // LoadDeformation
        /// <summary>
        /// This function retrieves the deformation load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="numberItems">The total number of deformation loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each deformation load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each deformation load.</param>
        /// <param name="U1">Axial deformation load value. [L]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the load assignments are retrieved for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the load assignments are retrieved for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void GetLoadDeformation(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] U1,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.GetLoadDeformation(name, ref numberItems, 
                            ref names, ref loadPatterns, ref U1, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns deformation loads to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="U1">Axial deformation load value. [L]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetLoadDeformation(string name,
            string loadPattern,
            double U1,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetLoadDeformation(name, 
                            loadPattern, ref U1, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the deformation load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are deleted for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are deleted for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are deleted for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void DeleteLoadDeformation(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.DeleteLoadDeformation(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // LoadTargetForce
        /// <summary>
        /// This function retrieves the target force load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="numberItems">The total number of deformation loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each target force.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each target force.</param>
        /// <param name="forceValues">Target force values.</param>
        /// <param name="relativeForcesLocations">Relative distances along the line elements where the target force values apply.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the load assignments are retrieved for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the load assignments are retrieved for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void GetLoadTargetForce(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] forceValues,
            ref double[] relativeForcesLocations,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.GetLoadTargetForce(name, ref numberItems, 
                            ref names, ref loadPatterns, ref forceValues, ref relativeForcesLocations, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns the target force load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with each target force.</param>
        /// <param name="forceValue">Target force value.</param>
        /// <param name="relativeForceLocation">Relative distance along the line element where the target force value applies.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetLoadTargetForce(string name,
            string loadPattern,
            double forceValue,
            double relativeForceLocation,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetLoadTargetForce(name, 
                            loadPattern, ref forceValue, ref relativeForceLocation, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the target force load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are deleted for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are deleted for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are deleted for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void DeleteLoadTargetForce(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.DeleteLoadTargetForce(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // LoadStrain
        /// <summary>
        /// This function retrieves the strain load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="numberItems">The total number of strain loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each strain load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each strain load.</param>
        /// <param name="strainLoadValues">The strain values. [L/L]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify each strain load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are retrieved for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are retrieved for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are retrieved for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void GetLoadStrain(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] strainLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.GetLoadStrain(name, ref numberItems, 
                            ref names, ref loadPatterns, ref strainLoadValues, ref jointPatternNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns strain loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the strain load.</param>
        /// <param name="strainLoadValue">The strain value. [L/L]</param>
        /// <param name="jointPatternName">The joint pattern name, if any, used to specify the strain load.</param>
        /// <param name="replace">True: All previous strain loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetLoadStrain(string name,
            string loadPattern,
            double strainLoadValue,
            string jointPatternName,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetLoadStrain(name, 
                            loadPattern, strainLoadValue, replace, jointPatternName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the strain load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are deleted for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are deleted for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are deleted for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void DeleteLoadStrain(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.DeleteLoadStrain(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // LoadTemperature
        /// <summary>
        /// This function retrieves the temperature load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="numberItems">The total number of temperature loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each temperature load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each temperature load.</param>
        /// <param name="temperatureLoadValues">Temperature load values, [T].</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify the temperature load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the load assignments are retrieved for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the load assignments are retrieved for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void GetLoadTemperature(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] temperatureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.GetLoadTemperature(name, ref numberItems, 
                            ref names, ref loadPatterns, ref temperatureLoadValues, ref jointPatternNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing element or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the temperature load.</param>
        /// <param name="temperatureLoadValue">Temperature load value, [T].</param>
        /// <param name="jointPatternName">The joint pattern name, if any, used to specify the temperature load.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetLoadTemperature(string name,
            string loadPattern,
            double temperatureLoadValue,
            string jointPatternName,
            bool replace,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetLoadTemperature(name, 
                            loadPattern, temperatureLoadValue, jointPatternName, replace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the temperature load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are deleted for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are deleted for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are deleted for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void DeleteLoadTemperature(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.DeleteLoadTemperature(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        // LoadDistributed
        /// <summary>
        /// This function retrieves the distributed load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="numberItems">The total number of distributed loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each distributed load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each distributed load.</param>
        /// <param name="forceTypes">Force type for the distributed load for each load pattern.</param>
        /// <param name="coordinateSystems">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="loadValues">The load values across the cable for each load pattern. 
        /// [F/L] when <paramref name="forceTypes"/> is <see cref="eLoadForceType.Force"/>  and [F*L/L] when <paramref name="forceTypes"/> is <see cref="eLoadForceType.Moment"/>.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the load assignments are retrieved for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the load assignments are retrieved for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void GetLoadDistributed(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref eLoadDirection[] loadDirections,
            ref double[] loadValues,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
            int[] csiForceTypes = new int[0];
            int[] csiLoadDirections = new int[0];

            _callCode = _sapModel.CableObj.GetLoadDistributed(name, ref numberItems, 
                            ref names, ref loadPatterns, ref csiForceTypes, ref coordinateSystems, 
                            ref csiLoadDirections, ref loadValues, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forceTypes = csiForceTypes.Cast<eLoadForceType>().ToArray();
            loadDirections = csiLoadDirections.Cast<eLoadDirection>().ToArray();
        }

        /// <summary>
        /// This function assigns distributed loads to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the distributed load.</param>
        /// <param name="forceType">Force type for the distributed load for the load pattern.</param>
        /// <param name="loadDirection">Direction that the load is applied in for the load pattern.</param>
        /// <param name="loadValue">The load value across the cable. 
        /// [F/L] when <paramref name="forceType"/> is <see cref="eLoadForceType.Force"/> and [F*L/L] when <paramref name="forceType"/> is <see cref="eLoadForceType.Moment"/>.</param>
        /// <param name="coordinateSystem">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="replace">True: All previous distributed loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the load assignments are retrieved for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the load assignments are retrieved for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetLoadDistributed(string name,
            string loadPattern,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double loadValue,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.SetLoadDistributed(name, 
                            loadPattern, (int)forceType, (int)loadDirection, loadValue, coordinateSystem, replace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the distributed load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are deleted for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are deleted for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are deleted for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void DeleteLoadDistributed(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.CableObj.DeleteLoadDistributed(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // LoadDistributedWithGUID
        /// <summary>
        /// This function retrieves the distributed load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType"/>.</param>
        /// <param name="numberItems">The total number of distributed loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each distributed load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each distributed load.</param>
        /// <param name="GUIDs">This is an array that includes the global unique ID of each distributed load.</param>
        /// <param name="forceTypes">Force type for the distributed load for each load pattern.</param>
        /// <param name="coordinateSystems">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="loadValues">The load value aof the distributed load. 
        /// [F/L] when <paramref name="forceTypes"/> is <see cref="eLoadForceType.Force"/>  and [F*L/L] when <paramref name="forceTypes"/> is <see cref="eLoadForceType.Moment"/>.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the load assignments are retrieved for the objects specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the load assignments are retrieved for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void GetLoadDistributedWithGUID(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] GUIDs,
            ref eLoadForceType[] forceTypes,
            ref eLoadDirection[] loadDirections,
            ref double[] loadValues,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
            int[] csiForceTypes = new int[0];
            int[] csiLoadDirections = new int[0];

            _callCode = _sapModel.CableObj.GetLoadDistributedWithGUID(name, ref numberItems, 
                            ref names, ref loadPatterns, ref csiForceTypes, ref coordinateSystems,
                            ref csiLoadDirections, ref loadValues, ref GUIDs, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forceTypes = csiForceTypes.Cast<eLoadForceType>().ToArray();
            loadDirections = csiLoadDirections.Cast<eLoadDirection>().ToArray();
        }

        /// <summary>
        /// If the frame object is already assigned a distributed load with a global unique ID matching the specified global unique ID, this function modifies that distributed load. 
        /// Otherwise, this function assigns a new distributed load to the frame object and sets its global unique ID to the specified global unique ID.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the distributed load.</param>
        /// <param name="forceType">Force type for the distributed load for the load pattern.</param>
        /// <param name="loadDirection">Direction that the load is applied in for the load pattern.</param>
        /// <param name="loadValue">The load value of the distributed load. 
        /// [F/L] when <paramref name="forceType"/> is <see cref="eLoadForceType.Force"/>  and [F*L/L] when <paramref name="forceType"/> is <see cref="eLoadForceType.Moment"/>.</param>
        /// <param name="GUID">This is the global unique ID of a distributed load assigned to the frame object or if it is not the global unique id of a distributed load assigned to the frame object and it is not blank, the global unique ID which is assigned to the newly assigned load. 
        /// If left blank, a new load is assigned to the frame object and the value of this parameter is set to the global unique ID of the newly assigned load.</param>
        /// <param name="coordinateSystem">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="replace">True: If the input GUID is not the GUID of any distributed load assigned to the frame object, all previous distributed loads, if any, assigned to the specified frame object, in the specified load pattern, are deleted before making the new assignment. 
        /// If the input GUID is the GUID of a distributed load already assigned to the frame object, the parameters of the distributed load are updated with the values provided and this item is ignored.</param>
        public void SetLoadDistributedWithGUID(string name,
            string loadPattern,
            string GUID,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double loadValue,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true)
        {
            _callCode = _sapModel.CableObj.SetLoadDistributedWithGUID(name, loadPattern, (int)forceType, (int)loadDirection, loadValue, ref GUID, coordinateSystem, replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes the distributed load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="GUID">The global unique ID of one of the distributed loads on that frame object.</param>
        public void DeleteLoadDistributedWithGUID(string name,
            string GUID)
        {
            _callCode = _sapModel.CableObj.DeleteLoadDistributedWithGUID(name, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion

    }
}
#endif