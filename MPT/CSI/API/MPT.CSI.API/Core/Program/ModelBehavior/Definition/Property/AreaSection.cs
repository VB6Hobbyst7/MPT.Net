// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="AreaSection.cs" company="">
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
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Represents the area properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.IAreaSection" />
    public class AreaSection : CSiApiBase, IAreaSection
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaSection" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AreaSection(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing area property.
        /// </summary>
        /// <param name="currentName">The existing name of a defined area property.</param>
        /// <param name="newName">The new name for the area property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, 
            string newName)
        {
            _callCode = _sapModel.PropArea.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns the total number of defined area properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.PropArea.Count();
        }

        /// <summary>
        /// The function deletes a specified area property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing area property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.PropArea.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined area properties.
        /// </summary>
        /// <param name="names">Area property names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PropArea.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined area properties of the specified type.
        /// </summary>
        /// <param name="names">Area property names retrieved by the program.</param>
        /// <param name="areaType">The area type to filter the name list by.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names,
            eAreaSectionType areaType)
        {
            names = new string[0];

            _callCode = _sapModel.PropArea.GetNameList(ref _numberOfItems, ref names, (int)areaType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // === Get/Set ===

        /// <summary>
        /// This function retrieves the modifier assignment for area properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing area property.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetModifiers(string name, 
            out AreaModifier modifiers)
        {
            double[] csiModifiers = new double[0];

            _callCode = _sapModel.PropArea.GetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            modifiers = new AreaModifier();
            modifiers.FromArray(csiModifiers);
        }

        /// <summary>
        /// This function defines the modifier assignment for area properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing area property.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetModifiers(string name, 
            AreaModifier modifiers)
        {
            if (modifiers == null) { return; }
            double[] csiModifiers = modifiers.ToArray();

            _callCode = _sapModel.PropArea.SetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Methods: Public        
        /// <summary>
        /// This function retrieves the property type for the specified area property.
        /// </summary>
        /// <param name="name">The name of an existing area property.</param>
        /// <param name="areaPropertyType">Type of the area property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetType(string name,
            out eAreaSectionType areaPropertyType)
        {
            int csiAreaPropertyType = 0;
            _callCode = _sapModel.PropArea.GetTypeOAPI(name, ref csiAreaPropertyType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            areaPropertyType = (eAreaSectionType)csiAreaPropertyType;
        }


        /// <summary>
        /// This function retrieves area property design parameters for a shell-type area section.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area property.</param>
        /// <param name="nameMaterial">The name of the material property for the area property.</param>
        /// <param name="rebarLayout">The rebar layout option.</param>
        /// <param name="designCoverTopDirection1">The cover to the centroid of the top reinforcing steel running in the local 1 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout" /> = <see cref="eShellSteelLayoutOption.OneLayer" /> or <see cref="eShellSteelLayoutOption.TwoLayers" />.</param>
        /// <param name="designCoverTopDirection2">The cover to the centroid of the top reinforcing steel running in the local 2 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout" /> = <see cref="eShellSteelLayoutOption.OneLayer" /> or <see cref="eShellSteelLayoutOption.TwoLayers" />.</param>
        /// <param name="designCoverBottomDirection1">The cover to the centroid of the bottom reinforcing steel running in the local 1 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout" /> = <see cref="eShellSteelLayoutOption.TwoLayers" />.</param>
        /// <param name="designCoverBottomDirection2">The cover to the centroid of the bottom reinforcing steel running in the local 2 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout" /> = <see cref="eShellSteelLayoutOption.TwoLayers" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetShellDesign(string name,
            out string nameMaterial,
            out eShellSteelLayoutOption rebarLayout,
            out double designCoverTopDirection1,
            out double designCoverTopDirection2,
            out double designCoverBottomDirection1,
            out double designCoverBottomDirection2)
        {
            int csiRebarLayout = 0;
            nameMaterial = string.Empty;
            designCoverTopDirection1 = 0;
            designCoverTopDirection2 = 0;
            designCoverBottomDirection1 = 0;
            designCoverBottomDirection2 = 0;

            _callCode = _sapModel.PropArea.GetShellDesign(name, 
                ref nameMaterial, 
                ref csiRebarLayout, 
                ref designCoverTopDirection1, 
                ref designCoverTopDirection2, 
                ref designCoverBottomDirection1, 
                ref designCoverBottomDirection2);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            rebarLayout = (eShellSteelLayoutOption)csiRebarLayout;
        }

        /// <summary>
        /// This function assigns the design parameters for shell-type area properties.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area property.</param>
        /// <param name="nameMaterial">The name of the material property for the area property.</param>
        /// <param name="rebarLayout">The rebar layout option.</param>
        /// <param name="designCoverTopDirection1">The cover to the centroid of the top reinforcing steel running in the local 1 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout" /> = <see cref="eShellSteelLayoutOption.OneLayer" /> or <see cref="eShellSteelLayoutOption.TwoLayers" />.</param>
        /// <param name="designCoverTopDirection2">The cover to the centroid of the top reinforcing steel running in the local 2 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout" /> = <see cref="eShellSteelLayoutOption.OneLayer" /> or <see cref="eShellSteelLayoutOption.TwoLayers" />.</param>
        /// <param name="designCoverBottomDirection1">The cover to the centroid of the bottom reinforcing steel running in the local 1 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout" /> = <see cref="eShellSteelLayoutOption.TwoLayers" />.</param>
        /// <param name="designCoverBottomDirection2">The cover to the centroid of the bottom reinforcing steel running in the local 2 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout" /> = <see cref="eShellSteelLayoutOption.TwoLayers" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetShellDesign(string name,
            string nameMaterial,
            eShellSteelLayoutOption rebarLayout,
            double designCoverTopDirection1,
            double designCoverTopDirection2,
            double designCoverBottomDirection1,
            double designCoverBottomDirection2)
        {
            _callCode = _sapModel.PropArea.SetShellDesign(name, nameMaterial, (int)rebarLayout, designCoverTopDirection1, designCoverTopDirection2, designCoverBottomDirection1, designCoverBottomDirection2);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// The name of an existing shell-type area property that is specified to be a layered shell property.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area property that is specified to be a layered shell property.</param>
        /// <param name="layerNames">This is an array that includes the name of each layer.</param>
        /// <param name="distanceOffsets">This is an array that includes the distance from the area reference surface (area object joint location plus offsets) to the mid-height of the layer. [L].</param>
        /// <param name="thicknesses">This is an array that includes the thickness of each layer. [L].</param>
        /// <param name="layerTypes">The layer types.</param>
        /// <param name="numberOfIntegrationPoints">The number of integration points in the thickness direction for the layer.
        /// The locations are determined by the program using standard Guass-quadrature rules.</param>
        /// <param name="materialProperties">This is an array that includes the name of the material property for the layer.</param>
        /// <param name="materialAngles">This is an array that includes the material angle for the layer. [deg].</param>
        /// <param name="S11Types">The material component behavior in the S11 direction.</param>
        /// <param name="S22Types">The material component behavior in the S22 directions.</param>
        /// <param name="S12Types">The material component behavior in the S12 directions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetShellLayer(string name,
            out string[] layerNames,
            out double[] distanceOffsets,
            out double[] thicknesses,
            out eShellLayerType[] layerTypes,
            out int[] numberOfIntegrationPoints,
            out string[] materialProperties,
            out double[] materialAngles,
            out eMaterialComponentBehaviorType[] S11Types,
            out eMaterialComponentBehaviorType[] S22Types,
            out eMaterialComponentBehaviorType[] S12Types)
        {
            int[] csiLayerTypes = new int[0];
            int[] csiS11Types = new int[0];
            int[] csiS22Types = new int[0];
            int[] csiS12Types = new int[0];
            layerNames = new string[0];
            distanceOffsets = new double[0];
            thicknesses = new double[0];
            numberOfIntegrationPoints = new int[0];
            materialProperties = new string[0];
            materialAngles = new double[0];

            _callCode = _sapModel.PropArea.GetShellLayer_1(name, 
                ref _numberOfItems, 
                ref layerNames, 
                ref distanceOffsets, 
                ref thicknesses, 
                ref csiLayerTypes, 
                ref numberOfIntegrationPoints, 
                ref materialProperties, 
                ref materialAngles, 
                ref csiS11Types, 
                ref csiS22Types, 
                ref csiS12Types);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            layerTypes = csiLayerTypes.Cast<eShellLayerType>().ToArray();
            S11Types = csiS11Types.Cast<eMaterialComponentBehaviorType>().ToArray();
            S22Types = csiS22Types.Cast<eMaterialComponentBehaviorType>().ToArray();
            S12Types = csiS12Types.Cast<eMaterialComponentBehaviorType>().ToArray();
        }

        /// <summary>
        /// The name of an existing shell-type area property that is specified to be a layered shell property
        /// </summary>
        /// <param name="name">The name of an existing shell-type area property that is specified to be a layered shell property.</param>
        /// <param name="layerNames">This is an array that includes the name of each layer.</param>
        /// <param name="distanceOffsets">This is an array that includes the distance from the area reference surface (area object joint location plus offsets) to the mid-height of the layer. [L].</param>
        /// <param name="thicknesses">This is an array that includes the thickness of each layer. [L].</param>
        /// <param name="layerTypes">The layer types.</param>
        /// <param name="numberOfIntegrationPoints">The number of integration points in the thickness direction for the layer.
        /// The locations are determined by the program using standard Guass-quadrature rules.</param>
        /// <param name="materialProperties">This is an array that includes the name of the material property for the layer.</param>
        /// <param name="materialAngles">This is an array that includes the material angle for the layer. [deg].</param>
        /// <param name="S11Types">The material component behavior in the S11 direction.</param>
        /// <param name="S22Types">The material component behavior in the S22 directions.</param>
        /// <param name="S12Types">The material component behavior in the S12 directions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetShellLayer(string name,
            string[] layerNames,
            double[] distanceOffsets,
            double[] thicknesses,
            eShellLayerType[] layerTypes,
            int[] numberOfIntegrationPoints,
            string[] materialProperties,
            double[] materialAngles,
            eMaterialComponentBehaviorType[] S11Types,
            eMaterialComponentBehaviorType[] S22Types,
            eMaterialComponentBehaviorType[] S12Types)
        {
            arraysLengthMatch(nameof(layerNames), layerNames.Length, nameof(distanceOffsets), distanceOffsets.Length);
            arraysLengthMatch(nameof(layerNames), layerNames.Length, nameof(thicknesses), thicknesses.Length);
            arraysLengthMatch(nameof(layerNames), layerNames.Length, nameof(layerTypes), layerTypes.Length);
            arraysLengthMatch(nameof(layerNames), layerNames.Length, nameof(numberOfIntegrationPoints), numberOfIntegrationPoints.Length);
            arraysLengthMatch(nameof(layerNames), layerNames.Length, nameof(materialProperties), materialProperties.Length);
            arraysLengthMatch(nameof(layerNames), layerNames.Length, nameof(materialAngles), materialAngles.Length);
            arraysLengthMatch(nameof(layerNames), layerNames.Length, nameof(S11Types), S11Types.Length);
            arraysLengthMatch(nameof(layerNames), layerNames.Length, nameof(S22Types), S22Types.Length);
            arraysLengthMatch(nameof(layerNames), layerNames.Length, nameof(S12Types), S12Types.Length);

            int[] csiLayerTypes = layerTypes.Cast<int>().ToArray();
            int[] csiS11Types = S11Types.Cast<int>().ToArray();
            int[] csiS22Types = S22Types.Cast<int>().ToArray();
            int[] csiS12Types = S12Types.Cast<int>().ToArray();
            _numberOfItems = layerNames.Length;

            _callCode = _sapModel.PropArea.SetShellLayer_1(name, ref _numberOfItems, ref layerNames, ref distanceOffsets, ref thicknesses, ref csiLayerTypes, ref numberOfIntegrationPoints, ref materialProperties, ref materialAngles, ref csiS11Types, ref csiS22Types, ref csiS12Types);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves area property data for an asolid-type area section.
        /// </summary>
        /// <param name="name">The name of an existing asolid-type area property.</param>
        /// <param name="nameMaterial">The name of the material property for the area property.</param>
        /// <param name="materialAngle">The material angle. [deg]</param>
        /// <param name="arcAngle">The arc angle through which the area object is passed to define the asolid element. [deg]</param>
        /// <param name="incompatibleBendingModes">True: Incompatible bending modes are included in the stiffness formulation.
        /// In general, incompatible modes significantly improve the bending behavior of the object.</param>
        /// <param name="coordinateSystem">The area object is rotated about the Z-axis in this coordinate system to define the asolid element.</param>
        /// <param name="color">The display color assigned to the property.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetASolid(string name,
            out string nameMaterial,
            out double materialAngle,
            out double arcAngle,
            out bool incompatibleBendingModes,
            out string coordinateSystem,
            out int color,
            out string notes,
            out string GUID)
        {
            nameMaterial = string.Empty;
            materialAngle = 0;
            arcAngle = 0;
            incompatibleBendingModes = false;
            coordinateSystem = string.Empty;
            color = 0;
            notes = string.Empty;
            GUID = string.Empty;

            _callCode = _sapModel.PropArea.GetAsolid(name, 
                ref nameMaterial, 
                ref materialAngle, 
                ref arcAngle, 
                ref incompatibleBendingModes, 
                ref coordinateSystem, 
                ref color, 
                ref notes, 
                ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes an asolid-type area property.
        /// If this function is called for an existing area property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing asolid-type area property.</param>
        /// <param name="nameMaterial">The name of the material property for the area property.</param>
        /// <param name="materialAngle">The material angle. [deg]</param>
        /// <param name="arcAngle">The arc angle through which the area object is passed to define the asolid element. [deg]
        /// A value of zero means 1 radian (approximately 57.3 degrees).</param>
        /// <param name="incompatibleBendingModes">True: Incompatible bending modes are included in the stiffness formulation.
        /// In general, incompatible modes significantly improve the bending behavior of the object.</param>
        /// <param name="coordinateSystem">The area object is rotated about the Z-axis in this coordinate system to define the asolid element.</param>
        /// <param name="color">The display color assigned to the property.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetASolid(string name,
            string nameMaterial,
            double materialAngle,
            double arcAngle,
            bool incompatibleBendingModes,
            string coordinateSystem = CoordinateSystems.Global,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropArea.SetAsolid(name, nameMaterial, materialAngle, arcAngle, incompatibleBendingModes, coordinateSystem, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves area property data for a plane-type area section.
        /// </summary>
        /// <param name="name">The name of an existing plane-type area property.</param>
        /// <param name="planeType">Type of the plane.</param>
        /// <param name="nameMaterial">The name of the material property for the area property.</param>
        /// <param name="materialAngle">The material angle. [deg]</param>
        /// <param name="thickness">The plane thickness. [L]</param>
        /// <param name="incompatibleModes">True: Incompatible bending modes are included in the stiffness formulation.
        /// In general, incompatible modes significantly improve the bending behavior of the object.</param>
        /// <param name="color">The display color assigned to the property.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetPlane(string name,
            out ePlaneType planeType,
            out string nameMaterial,
            out double materialAngle,
            out double thickness,
            out bool incompatibleModes,
            out int color,
            out string notes,
            out string GUID)
        {
            int csiPlaneType = 0;
            nameMaterial = string.Empty;
            materialAngle = 0;
            thickness = 0;
            incompatibleModes = false;
            color = 0;
            notes = string.Empty;
            GUID = string.Empty;

            _callCode = _sapModel.PropArea.GetPlane(name, 
                ref csiPlaneType, 
                ref nameMaterial, 
                ref materialAngle, 
                ref thickness, 
                ref incompatibleModes, 
                ref color, 
                ref notes, 
                ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            planeType = (ePlaneType) csiPlaneType;
        }

        /// <summary>
        /// This function initializes a plane-type area property.
        /// If this function is called for an existing area property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing plane-type area property.</param>
        /// <param name="planeType">Type of the plane.</param>
        /// <param name="nameMaterial">The name of the material property for the area property.</param>
        /// <param name="materialAngle">The material angle. [deg]</param>
        /// <param name="thickness">The plane thickness. [L]</param>
        /// <param name="incompatibleModes">True: Incompatible bending modes are included in the stiffness formulation.
        /// In general, incompatible modes significantly improve the bending behavior of the object.</param>
        /// <param name="color">The display color assigned to the property.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetPlane(string name,
            ePlaneType planeType,
            string nameMaterial,
            double materialAngle,
            double thickness,
            bool incompatibleModes,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropArea.SetPlane(name, (int)planeType, nameMaterial, materialAngle, thickness, incompatibleModes, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves area property data for a shell-type area section.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area property.</param>
        /// <param name="shellType">Type of shell.</param>
        /// <param name="includeDrillingDOF">True: Drilling degrees of freedom are included in the element formulation in the analysis model.
        /// This item does not apply when <paramref name="shellType" /> = <see cref="eShellType.PlateThin" />, <see cref="eShellType.PlateThick" /> or <see cref="eShellType.ShellLayered" />.</param>
        /// <param name="nameMaterial">The name of the material property for the area property.
        /// This item does not apply when <paramref name="shellType" /> = <see cref="eShellType.ShellLayered" />.</param>
        /// <param name="materialAngle">The material angle. [deg]
        /// This item does not apply when <paramref name="shellType" /> = <see cref="eShellType.ShellLayered" />.</param>
        /// <param name="membraneThickness">The membrane thickness. [L]
        /// This item does not apply when <paramref name="shellType" /> = <see cref="eShellType.ShellLayered" />.</param>
        /// <param name="bendingThickness">The bending thickness. [L]
        /// This item does not apply when <paramref name="shellType" /> = <see cref="eShellType.ShellLayered" />.</param>
        /// <param name="color">The display color assigned to the property.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetShell(string name,
            out eShellType shellType,
            out bool includeDrillingDOF,
            out string nameMaterial,
            out double materialAngle,
            out double membraneThickness,
            out double bendingThickness,
            out int color,
            out string notes,
            out string GUID)
        {
            int csiShellType = 0;
            includeDrillingDOF = false;
            nameMaterial = string.Empty;
            materialAngle = 0;
            membraneThickness = 0;
            bendingThickness = 0;
            color = 0;
            notes = string.Empty;
            GUID = string.Empty;

            _callCode = _sapModel.PropArea.GetShell_1(name, 
                ref csiShellType, 
                ref includeDrillingDOF,
                ref nameMaterial, 
                ref materialAngle, 
                ref membraneThickness, 
                ref bendingThickness, 
                ref color, 
                ref notes, 
                ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            shellType = (eShellType)csiShellType;
        }

        /// <summary>
        /// The name of an existing or new area property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area property.</param>
        /// <param name="shellType">Type of shell.</param>
        /// <param name="includeDrillingDOF">True: Drilling degrees of freedom are included in the element formulation in the analysis model.
        /// This item does not apply when <paramref name="shellType" /> = <see cref="eShellType.PlateThin" />, <see cref="eShellType.PlateThick" /> or <see cref="eShellType.ShellLayered" />.</param>
        /// <param name="nameMaterial">The name of the material property for the area property.
        /// This item does not apply when <paramref name="shellType" /> = <see cref="eShellType.ShellLayered" />.</param>
        /// <param name="materialAngle">The material angle. [deg]
        /// This item does not apply when <paramref name="shellType" /> = <see cref="eShellType.ShellLayered" />.</param>
        /// <param name="membraneThickness">The membrane thickness. [L]
        /// This item does not apply when <paramref name="shellType" /> = <see cref="eShellType.ShellLayered" />.</param>
        /// <param name="bendingThickness">The bending thickness. [L]
        /// This item does not apply when <paramref name="shellType" /> = <see cref="eShellType.ShellLayered" />.</param>
        /// <param name="color">The display color assigned to the property.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetShell(string name,
            eShellType shellType,
            bool includeDrillingDOF,
            string nameMaterial,
            double materialAngle,
            double membraneThickness,
            double bendingThickness,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropArea.SetShell_1(name, (int)shellType, includeDrillingDOF, nameMaterial, materialAngle, membraneThickness, bendingThickness, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function retrieves property data for a deck section.
        /// </summary>
        /// <param name="name">The name of an existing deck property.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="thickness">The thickness of the deck. [L].</param>
        /// <param name="deckType">The deck type.</param>
        /// <param name="shellType">The shell type for the deck.
        /// Please note that for deck properties, this is always <see cref="eShellType.Membrane"/></param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDeck(string name,
            ref double thickness,
            ref eDeckType deckType,
            ref eShellType shellType,
            ref string nameMaterial,
            ref int color,
            ref string notes,
            ref string GUID)
        {
            CSiProgram.eDeckType csiDeckType = CSiProgram.eDeckType.Filled;
            CSiProgram.eShellType csiShellType = CSiProgram.eShellType.ShellThick;

            _callCode = _sapModel.PropArea.GetDeck(name, ref csiDeckType, ref csiShellType, ref nameMaterial, ref thickness, ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            shellType = EnumLibrary.Convert(csiShellType, shellType);
            deckType = EnumLibrary.Convert(csiDeckType, deckType);
        }

        /// <summary>
        /// This function initializes area section property data for a deck section.
        /// If this function is called for an existing section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new deck property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="thickness">The thickness of the deck. [L].</param>
        /// <param name="deckType">The deck type.</param>
        /// <param name="shellType">The shell type for the deck.
        /// Please note that for deck properties, this is always <see cref="eShellType.Membrane"/></param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDeck(string name,
            double thickness,
            eDeckType deckType,
            eShellType shellType,
            string nameMaterial,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            CSiProgram.eDeckType csiDeckType = EnumLibrary.Convert<eDeckType, CSiProgram.eDeckType>(deckType); 
            CSiProgram.eShellType csiShellType = EnumLibrary.Convert<eShellType, CSiProgram.eShellType>(shellType);

            _callCode = _sapModel.PropArea.SetDeck(name, csiDeckType, csiShellType, nameMaterial, thickness, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===        
        /// <summary>
        /// This function retrieves property data for a filled deck section.
        /// </summary>
        /// <param name="name">The name of an existing deck property.</param>
        /// <param name="slabDepth">The slab depth. [L].</param>
        /// <param name="ribDepth">The rib depth. [L].</param>
        /// <param name="ribWidthTop">The rib width top. [L].</param>
        /// <param name="ribWidthBottom">The rib width bottom. [L].</param>
        /// <param name="ribSpacing">The rib spacing. [L].</param>
        /// <param name="shearThickness">The shear thickness. [L].</param>
        /// <param name="unitWeight">The unit weight. [F/L^3].</param>
        /// <param name="shearStudDiameter">The shear stud diameter. [L].</param>
        /// <param name="shearStudHeight">Height of the shear stud. [L].</param>
        /// <param name="shearStudFu">The shear stud tensile strength, Fu. [F/L^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDeckFilled(string name,
            ref double slabDepth,
            ref double ribDepth,
            ref double ribWidthTop,
            ref double ribWidthBottom,
            ref double ribSpacing,
            ref double shearThickness,
            ref double unitWeight,
            ref double shearStudDiameter,
            ref double shearStudHeight,
            ref double shearStudFu)
        {
            _callCode = _sapModel.PropArea.GetDeckFilled(
                                name,
                                ref slabDepth,
                                ref ribDepth,
                                ref ribWidthTop,
                                ref ribWidthBottom,
                                ref ribSpacing,
                                ref shearThickness,
                                ref unitWeight,
                                ref shearStudDiameter,
                                ref shearStudHeight,
                                ref shearStudFu);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes area section property data for a filled deck section.
        /// If this function is called for an existing section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new deck property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="slabDepth">The slab depth. [L].</param>
        /// <param name="ribDepth">The rib depth. [L].</param>
        /// <param name="ribWidthTop">The rib width top. [L].</param>
        /// <param name="ribWidthBottom">The rib width bottom. [L].</param>
        /// <param name="ribSpacing">The rib spacing. [L].</param>
        /// <param name="shearThickness">The shear thickness. [L].</param>
        /// <param name="unitWeight">The unit weight. [F/L^3].</param>
        /// <param name="shearStudDiameter">The shear stud diameter. [L].</param>
        /// <param name="shearStudHeight">Height of the shear stud. [L].</param>
        /// <param name="shearStudFu">The shear stud tensile strength, Fu. [F/L^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDeckFilled(string name,
            double slabDepth,
            double ribDepth,
            double ribWidthTop,
            double ribWidthBottom,
            double ribSpacing,
            double shearThickness,
            double unitWeight,
            double shearStudDiameter,
            double shearStudHeight,
            double shearStudFu)
        {
            _callCode = _sapModel.PropArea.SetDeckFilled(
                                name,
                                slabDepth,
                                ribDepth,
                                ribWidthTop,
                                ribWidthBottom,
                                ribSpacing,
                                shearThickness,
                                unitWeight,
                                shearStudDiameter,
                                shearStudHeight,
                                shearStudFu);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===        
        /// <summary>
        /// This function retrieves property data for an unfilled deck section.
        /// </summary>
        /// <param name="name">The name of an existing deck property.</param>
        /// <param name="ribDepth">The rib depth. [L].</param>
        /// <param name="ribWidthTop">The rib width top. [L].</param>
        /// <param name="ribWidthBottom">The rib width bottom. [L].</param>
        /// <param name="ribSpacing">The rib spacing. [L].</param>
        /// <param name="shearThickness">The shear thickness. [L].</param>
        /// <param name="unitWeight">The unit weight. [F/L^3].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDeckUnfilled(string name,
            ref double ribDepth,
            ref double ribWidthTop,
            ref double ribWidthBottom,
            ref double ribSpacing,
            ref double shearThickness,
            ref double unitWeight)
        {
            _callCode = _sapModel.PropArea.GetDeckUnfilled(
                name,
                ref ribDepth,
                ref ribWidthTop,
                ref ribWidthBottom,
                ref ribSpacing,
                ref shearThickness,
                ref unitWeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes area section property data for an unfilled deck section.
        /// If this function is called for an existing section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new deck property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="ribDepth">The rib depth. [L].</param>
        /// <param name="ribWidthTop">The rib width top. [L].</param>
        /// <param name="ribWidthBottom">The rib width bottom. [L].</param>
        /// <param name="ribSpacing">The rib spacing. [L].</param>
        /// <param name="shearThickness">The shear thickness. [L].</param>
        /// <param name="unitWeight">The unit weight. [F/L^3].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDeckUnfilled(string name,
            double ribDepth,
            double ribWidthTop,
            double ribWidthBottom,
            double ribSpacing,
            double shearThickness,
            double unitWeight)
        {
            _callCode = _sapModel.PropArea.SetDeckUnfilled(
                            name,
                            ribDepth,
                            ribWidthTop,
                            ribWidthBottom,
                            ribSpacing,
                            shearThickness,
                            unitWeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // ===        
        /// <summary>
        /// This function retrieves property data for a solid slab deck section.
        /// </summary>
        /// <param name="name">The name of an existing deck property.</param>
        /// <param name="slabDepth">The slab depth. [L].</param>
        /// <param name="shearStudDiameter">The shear stud diameter. [L].</param>
        /// <param name="shearStudHeight">Height of the shear stud. [L].</param>
        /// <param name="shearStudFu">The shear stud tensile strength, Fu. [F/L^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDeckSolidSlab(string name,
            ref double slabDepth,
            ref double shearStudDiameter,
            ref double shearStudHeight,
            ref double shearStudFu)
        {
            _callCode = _sapModel.PropArea.GetDeckSolidSlab(
                            name,
                            ref slabDepth,
                            ref shearStudDiameter,
                            ref shearStudHeight,
                            ref shearStudFu);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes area section property data for a solid slab deck section.
        /// If this function is called for an existing section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new deck property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="slabDepth">The slab depth. [L].</param>
        /// <param name="shearStudDiameter">The shear stud diameter. [L].</param>
        /// <param name="shearStudHeight">Height of the shear stud. [L].</param>
        /// <param name="shearStudFu">The shear stud tensile strength, Fu. [F/L^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDeckSolidSlab(string name,
            double slabDepth,
            double shearStudDiameter,
            double shearStudHeight,
            double shearStudFu)
        {
            _callCode = _sapModel.PropArea.SetDeckSolidSlab(
                            name,
                            slabDepth,
                            shearStudDiameter,
                            shearStudHeight,
                            shearStudFu);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // ===
        /// <summary>
        /// This function retrieves property data for a slab section.
        /// </summary>
        /// <param name="name">The name of an existing slab property.</param>
        /// <param name="nameMaterial">The name of the material property for the section.
        /// This item does not apply when <paramref name="shellType"/> is <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="thickness">The membrane thickness of the slab. [L].
        /// This item does not apply when <paramref name="shellType"/> is <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="slabType">The slab type.</param>
        /// <param name="shellType">The shell type for the slab.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSlab(string name,
            ref double thickness,
            ref eSlabType slabType,
            ref eShellType shellType,
            ref string nameMaterial,
            ref int color,
            ref string notes,
            ref string GUID)
        {
            CSiProgram.eSlabType csiSlabType = CSiProgram.eSlabType.Slab;
            CSiProgram.eShellType csiShellType = CSiProgram.eShellType.ShellThick;

            _callCode = _sapModel.PropArea.GetSlab(name, ref csiSlabType, ref csiShellType, ref nameMaterial, ref thickness, ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            shellType = EnumLibrary.Convert(csiShellType, shellType);
            slabType = EnumLibrary.Convert(csiSlabType, slabType);
        }

        /// <summary>
        /// This function initializes area section property data for a slab section.
        /// If this function is called for an existing section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new slab property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.
        /// This item does not apply when <paramref name="shellType"/> is <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="thickness">The membrane thickness of the slab. [L].
        /// This item does not apply when <paramref name="shellType"/> is <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="slabType">The slab type.</param>
        /// <param name="shellType">The shell type for the slab.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSlab(string name,
            double thickness,
            eSlabType slabType,
            eShellType shellType,
            string nameMaterial,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            CSiProgram.eSlabType csiSlabType = EnumLibrary.Convert<eSlabType, CSiProgram.eSlabType>(slabType);
            CSiProgram.eShellType csiShellType = EnumLibrary.Convert<eShellType, CSiProgram.eShellType>(shellType);

            _callCode = _sapModel.PropArea.SetSlab(name, csiSlabType, csiShellType, nameMaterial, thickness, color, notes, GUID);
        }

        // ===        
        /// <summary>
        /// This function retrieves property data for a ribbed slab section.
        /// </summary>
        /// <param name="name">The name of an existing slab property.</param>
        /// <param name="overallDepth">The overall depth. [L].</param>
        /// <param name="slabThickness">The slab thickness. [L].</param>
        /// <param name="stemWidthTop">The stem width top. [L].</param>
        /// <param name="stemWithBottom">The stem with bottom. [L].</param>
        /// <param name="ribSpacing">The rib spacing. [L].</param>
        /// <param name="ribsParallelToAxis">The local axis that the ribs are parallel to.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSlabRibbed(string name,
            ref double overallDepth,
            ref double slabThickness,
            ref double stemWidthTop,
            ref double stemWithBottom,
            ref double ribSpacing,
            ref eLocalAxisPlane ribsParallelToAxis)
        {
            int ribsParallelTo = 0;

            _callCode = _sapModel.PropArea.GetSlabRibbed(
                            name,
                            ref overallDepth,
                            ref slabThickness,
                            ref stemWidthTop,
                            ref stemWithBottom,
                            ref ribSpacing,
                            ref ribsParallelTo);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            ribsParallelToAxis = (eLocalAxisPlane)ribsParallelTo;
        }

        /// <summary>
        /// This function initializes area section property data for a ribbed slab section.
        /// If this function is called for an existing section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new slab property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="overallDepth">The overall depth. [L].</param>
        /// <param name="slabThickness">The slab thickness. [L].</param>
        /// <param name="stemWidthTop">The stem width top. [L].</param>
        /// <param name="stemWithBottom">The stem with bottom. [L].</param>
        /// <param name="ribSpacing">The rib spacing. [L].</param>
        /// <param name="ribsParallelToAxis">The local axis that the ribs are parallel to.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSlabRibbed(string name,
            double overallDepth,
            double slabThickness,
            double stemWidthTop,
            double stemWithBottom,
            double ribSpacing,
            eLocalAxisPlane ribsParallelToAxis)
        {
            int ribsParallelTo = (int)ribsParallelToAxis;

            _callCode = _sapModel.PropArea.SetSlabRibbed(
                            name,
                            overallDepth,
                            slabThickness,
                            stemWidthTop,
                            stemWithBottom,
                            ribSpacing,
                            ribsParallelTo);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===        
        /// <summary>
        /// This function retrieves property data for a waffle slab section.
        /// </summary>
        /// <param name="name">The name of an existing slab property.</param>
        /// <param name="overallDepth">The overall depth. [L].</param>
        /// <param name="slabThickness">The slab thickness. [L].</param>
        /// <param name="stemWidthTop">The stem width top. [L].</param>
        /// <param name="stemWithBottom">The stem with bottom. [L].</param>
        /// <param name="ribSpacingLocal1">The rib spacing parallel to the local 1-axis.</param>
        /// <param name="ribSpacingLocal2">The rib spacing parallel to the local 2-axis.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSlabWaffle(string name,
            ref double overallDepth,
            ref double slabThickness,
            ref double stemWidthTop,
            ref double stemWithBottom,
            ref double ribSpacingLocal1,
            ref double ribSpacingLocal2)
        {
            _callCode = _sapModel.PropArea.GetSlabWaffle(
                            name,
                            ref overallDepth,
                            ref slabThickness,
                            ref stemWidthTop,
                            ref stemWithBottom,
                            ref ribSpacingLocal1,
                            ref ribSpacingLocal2);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes area section property data for a waffle slab section.
        /// If this function is called for an existing section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new slab property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="overallDepth">The overall depth. [L].</param>
        /// <param name="slabThickness">The slab thickness. [L].</param>
        /// <param name="stemWidthTop">The stem width top. [L].</param>
        /// <param name="stemWithBottom">The stem with bottom. [L].</param>
        /// <param name="ribSpacingLocal1">The rib spacing parallel to the local 1-axis.</param>
        /// <param name="ribSpacingLocal2">The rib spacing parallel to the local 2-axis.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSlabWaffle(string name,
            double overallDepth,
            double slabThickness,
            double stemWidthTop,
            double stemWithBottom,
            double ribSpacingLocal1,
            double ribSpacingLocal2)
        {
            _callCode = _sapModel.PropArea.SetSlabWaffle(
                            name,
                            overallDepth,
                            slabThickness,
                            stemWidthTop,
                            stemWithBottom,
                            ribSpacingLocal1,
                            ribSpacingLocal2);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===
        /// <summary>
        /// This function retrieves property data for a wall section.
        /// </summary>
        /// <param name="name">The name of an existing wall property.</param>
        /// <param name="nameMaterial">The name of the material property for the section.
        /// This item does not apply when <paramref name="shellType"/> is <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="thickness">The membrane thickness of the wall. [L].
        /// This item does not apply when <paramref name="shellType"/> is <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="walltype">The wall type.</param>
        /// <param name="shellType">The shell type for the wall.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetWall(string name,
            ref double thickness,
            ref eWallPropertyType walltype,
            ref eShellType shellType,
            ref string nameMaterial,
            ref int color,
            ref string notes,
            ref string GUID)
        {
            CSiProgram.eWallPropType csiWallType = CSiProgram.eWallPropType.Specified;
            CSiProgram.eShellType csiShellType = CSiProgram.eShellType.ShellThick;

            _callCode = _sapModel.PropArea.GetWall(name, ref csiWallType, ref csiShellType, ref nameMaterial, ref thickness, ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            shellType = EnumLibrary.Convert(csiShellType, shellType);
            walltype = EnumLibrary.Convert(csiWallType, walltype);
        }

        /// <summary>
        /// This function initializes area section property data for a wall section.
        /// If this function is called for an existing section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new wall property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.
        /// This item does not apply when <paramref name="shellType"/> is <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="thickness">The membrane thickness of the wall. [L].
        /// This item does not apply when <paramref name="shellType"/> is <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="walltype">The wall type.</param>
        /// <param name="shellType">The shell type for the wall.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetWall(string name,
            double thickness,
            eWallPropertyType walltype,
            eShellType shellType,
            string nameMaterial,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            CSiProgram.eWallPropType csiWallType = EnumLibrary.Convert<eWallPropertyType, CSiProgram.eWallPropType>(walltype);
            CSiProgram.eShellType csiShellType = EnumLibrary.Convert<eShellType, CSiProgram.eShellType>(shellType);

            _callCode = _sapModel.PropArea.SetWall(name, csiWallType, csiShellType, nameMaterial, thickness, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===        
        /// <summary>
        /// Gets the wall automatic select list.
        /// </summary>
        /// <param name="name">The name of an existing auto select list wall property.</param>
        /// <param name="autoSelectList">The names of the wall properties included in the auto select list.</param>
        /// <param name="startingProperty">This is Median or the name of a wall property in the AutoSelectList array. 
        /// It is the starting section for the auto select list. 
        /// Median indicates the Median Property by Thickness.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetWallAutoSelectList(string name,
            ref string[] autoSelectList,
            ref string startingProperty)
        {
            _callCode = _sapModel.PropArea.GetWallAutoSelectList(name, ref autoSelectList, ref startingProperty);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        /// <summary>
        /// Initializes property data for an auto select list wall section.
        /// </summary>
        /// <param name="name">The name of an existing auto select list wall property.</param>
        /// <param name="autoSelectList">The names of the wall properties included in the auto select list.</param>
        /// <param name="startingProperty">This is Median or the name of a wall property in the AutoSelectList array. 
        /// It is the starting section for the auto select list. 
        /// Median indicates the Median Property by Thickness.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetWallAutoSelectList(string name,
            string[] autoSelectList,
            string startingProperty = "Median")
        {
            _callCode = _sapModel.PropArea.SetWallAutoSelectList(name, autoSelectList, startingProperty);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
