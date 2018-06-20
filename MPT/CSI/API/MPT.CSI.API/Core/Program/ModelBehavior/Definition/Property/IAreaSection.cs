namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Implements the area properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableAreaModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableAreaModifiers" />
    public interface IAreaSection: IChangeableName, ICountable, IDeletable, IListableNames,
        IObservableAreaModifiers, IChangeableAreaModifiers
    {
        /// <summary>
        /// This function retrieves the property type for the specified area property.
        /// </summary>
        /// <param name="name">The name of an existing area property.</param>
        /// <param name="areaPropertyType">Type of the area property.</param>
        void GetType(string name,
            out eAreaSectionType areaPropertyType);

        /// <summary>
        /// This function retrieves the names of all defined area properties of the specified type.
        /// </summary>
        /// <param name="names">Area property names retrieved by the program.</param>
        /// <param name="areaType">The area type to filter the name list by.</param>
        void GetNameList(out string[] names,
            eAreaSectionType areaType);

        /// <summary>
        /// This function retrieves area property design parameters for a shell-type area section.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area property.</param>
        /// <param name="nameMaterial">The name of the material property for the area property.</param>
        /// <param name="rebarLayout">The rebar layout option.</param>
        /// <param name="designCoverTopDirection1">The cover to the centroid of the top reinforcing steel running in the local 1 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout"/> = <see cref="eShellSteelLayoutOption.OneLayer"/> or <see cref="eShellSteelLayoutOption.TwoLayers"/>.</param>
        /// <param name="designCoverTopDirection2">The cover to the centroid of the top reinforcing steel running in the local 2 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout"/> = <see cref="eShellSteelLayoutOption.OneLayer"/> or <see cref="eShellSteelLayoutOption.TwoLayers"/>.</param>
        /// <param name="designCoverBottomDirection1">The cover to the centroid of the bottom reinforcing steel running in the local 1 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout"/> = <see cref="eShellSteelLayoutOption.TwoLayers"/>.</param>
        /// <param name="designCoverBottomDirection2">The cover to the centroid of the bottom reinforcing steel running in the local 2 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout"/> = <see cref="eShellSteelLayoutOption.TwoLayers"/>.</param>
        void GetShellDesign(string name,
            out string nameMaterial,
            out eShellSteelLayoutOption rebarLayout,
            out double designCoverTopDirection1,
            out double designCoverTopDirection2,
            out double designCoverBottomDirection1,
            out double designCoverBottomDirection2);

        /// <summary>
        /// This function assigns the design parameters for shell-type area properties.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area property.</param>
        /// <param name="nameMaterial">The name of the material property for the area property.</param>
        /// <param name="rebarLayout">The rebar layout option.</param>
        /// <param name="designCoverTopDirection1">The cover to the centroid of the top reinforcing steel running in the local 1 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout"/> = <see cref="eShellSteelLayoutOption.OneLayer"/> or <see cref="eShellSteelLayoutOption.TwoLayers"/>.</param>
        /// <param name="designCoverTopDirection2">The cover to the centroid of the top reinforcing steel running in the local 2 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout"/> = <see cref="eShellSteelLayoutOption.OneLayer"/> or <see cref="eShellSteelLayoutOption.TwoLayers"/>.</param>
        /// <param name="designCoverBottomDirection1">The cover to the centroid of the bottom reinforcing steel running in the local 1 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout"/> = <see cref="eShellSteelLayoutOption.TwoLayers"/>.</param>
        /// <param name="designCoverBottomDirection2">The cover to the centroid of the bottom reinforcing steel running in the local 2 axis directions of the area object. [L]
        /// This item applies only when <paramref name="rebarLayout"/> = <see cref="eShellSteelLayoutOption.TwoLayers"/>.</param>
        void SetShellDesign(string name,
            string nameMaterial,
            eShellSteelLayoutOption rebarLayout,
            double designCoverTopDirection1,
            double designCoverTopDirection2,
            double designCoverBottomDirection1,
            double designCoverBottomDirection2);


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
        void GetShellLayer(string name,
            out string[] layerNames,
            out double[] distanceOffsets,
            out double[] thicknesses,
            out eShellLayerType[] layerTypes,
            out int[] numberOfIntegrationPoints,
            out string[] materialProperties,
            out double[] materialAngles,
            out eMaterialComponentBehaviorType[] S11Types,
            out eMaterialComponentBehaviorType[] S22Types,
            out eMaterialComponentBehaviorType[] S12Types);

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
        void SetShellLayer(string name,
            string[] layerNames,
            double[] distanceOffsets,
            double[] thicknesses,
            eShellLayerType[] layerTypes,
            int[] numberOfIntegrationPoints,
            string[] materialProperties,
            double[] materialAngles,
            eMaterialComponentBehaviorType[] S11Types,
            eMaterialComponentBehaviorType[] S22Types,
            eMaterialComponentBehaviorType[] S12Types);


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
        void GetASolid(string name,
            out string nameMaterial,
            out double materialAngle,
            out double arcAngle,
            out bool incompatibleBendingModes,
            out string coordinateSystem,
            out int color,
            out string notes,
            out string GUID);

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
        void SetASolid(string name,
            string nameMaterial,
            double materialAngle,
            double arcAngle,
            bool incompatibleBendingModes,
            string coordinateSystem = CoordinateSystems.Global,
            int color = -1,
            string notes = "",
            string GUID = "");

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
        void GetPlane(string name,
            out ePlaneType planeType,
            out string nameMaterial,
            out double materialAngle,
            out double thickness,
            out bool incompatibleModes,
            out int color,
            out string notes,
            out string GUID);

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
        void SetPlane(string name,
            ePlaneType planeType,
            string nameMaterial,
            double materialAngle,
            double thickness,
            bool incompatibleModes,
            int color = -1,
            string notes = "",
            string GUID = "");


        /// <summary>
        /// This function retrieves area property data for a shell-type area section.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area property.</param>
        /// <param name="shellType">Type of shell.</param>
        /// <param name="includeDrillingDOF">True: Drilling degrees of freedom are included in the element formulation in the analysis model. 
        /// This item does not apply when <paramref name="shellType"/> = <see cref="eShellType.PlateThin"/>, <see cref="eShellType.PlateThick"/> or <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="nameMaterial">The name of the material property for the area property. 
        /// This item does not apply when <paramref name="shellType"/> = <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="materialAngle">The material angle. [deg]
        /// This item does not apply when <paramref name="shellType"/> = <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="membraneThickness">The membrane thickness. [L]
        /// This item does not apply when <paramref name="shellType"/> = <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="bendingThickness">The bending thickness. [L]
        /// This item does not apply when <paramref name="shellType"/> = <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="color">The display color assigned to the property.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetShell(string name,
            out eShellType shellType,
            out bool includeDrillingDOF,
            out string nameMaterial,
            out double materialAngle,
            out double membraneThickness,
            out double bendingThickness,
            out int color,
            out string notes,
            out string GUID);

        /// <summary>
        /// The name of an existing or new area property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area property.</param>
        /// <param name="shellType">Type of shell.</param>
        /// <param name="includeDrillingDOF">True: Drilling degrees of freedom are included in the element formulation in the analysis model. 
        /// This item does not apply when <paramref name="shellType"/> = <see cref="eShellType.PlateThin"/>, <see cref="eShellType.PlateThick"/> or <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="nameMaterial">The name of the material property for the area property. 
        /// This item does not apply when <paramref name="shellType"/> = <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="materialAngle">The material angle. [deg]
        /// This item does not apply when <paramref name="shellType"/> = <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="membraneThickness">The membrane thickness. [L]
        /// This item does not apply when <paramref name="shellType"/> = <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="bendingThickness">The bending thickness. [L]
        /// This item does not apply when <paramref name="shellType"/> = <see cref="eShellType.ShellLayered"/>.</param>
        /// <param name="color">The display color assigned to the property.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetShell(string name,
            eShellType shellType,
            bool includeDrillingDOF,
            string nameMaterial,
            double materialAngle,
            double membraneThickness,
            double bendingThickness,
            int color = -1,
            string notes = "",
            string GUID = "");
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
        void GetDeck(string name,
            ref double thickness,
            ref eDeckType deckType,
            ref eShellType shellType,
            ref string nameMaterial,
            ref int color,
            ref string notes,
            ref string GUID);

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
        void SetDeck(string name,
            double thickness,
            eDeckType deckType,
            eShellType shellType,
            string nameMaterial,
            int color = -1,
            string notes = "",
            string GUID = "");

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
        void GetDeckFilled(string name,
            ref double slabDepth,
            ref double ribDepth,
            ref double ribWidthTop,
            ref double ribWidthBottom,
            ref double ribSpacing,
            ref double shearThickness,
            ref double unitWeight,
            ref double shearStudDiameter,
            ref double shearStudHeight,
            ref double shearStudFu);

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
        void SetDeckFilled(string name,
            double slabDepth,
            double ribDepth,
            double ribWidthTop,
            double ribWidthBottom,
            double ribSpacing,
            double shearThickness,
            double unitWeight,
            double shearStudDiameter,
            double shearStudHeight,
            double shearStudFu);

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
        void GetDeckUnfilled(string name,
            ref double ribDepth,
            ref double ribWidthTop,
            ref double ribWidthBottom,
            ref double ribSpacing,
            ref double shearThickness,
            ref double unitWeight);

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
        void SetDeckUnfilled(string name,
            double ribDepth,
            double ribWidthTop,
            double ribWidthBottom,
            double ribSpacing,
            double shearThickness,
            double unitWeight);


        // ===        
        /// <summary>
        /// This function retrieves property data for a solid slab deck section.
        /// </summary>
        /// <param name="name">The name of an existing deck property.</param>
        /// <param name="slabDepth">The slab depth. [L].</param>
        /// <param name="shearStudDiameter">The shear stud diameter. [L].</param>
        /// <param name="shearStudHeight">Height of the shear stud. [L].</param>
        /// <param name="shearStudFu">The shear stud tensile strength, Fu. [F/L^2].</param>
        void GetDeckSolidSlab(string name,
            ref double slabDepth,
            ref double shearStudDiameter,
            ref double shearStudHeight,
            ref double shearStudFu);

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
        void SetDeckSolidSlab(string name,
            double slabDepth,
            double shearStudDiameter,
            double shearStudHeight,
            double shearStudFu);


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
        void GetSlab(string name,
            ref double thickness,
            ref eSlabType slabType,
            ref eShellType shellType,
            ref string nameMaterial,
            ref int color,
            ref string notes,
            ref string GUID);

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
        void SetSlab(string name,
            double thickness,
            eSlabType slabType,
            eShellType shellType,
            string nameMaterial,
            int color = -1,
            string notes = "",
            string GUID = "");

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
        void GetSlabRibbed(string name,
            ref double overallDepth,
            ref double slabThickness,
            ref double stemWidthTop,
            ref double stemWithBottom,
            ref double ribSpacing,
            ref eLocalAxisPlane ribsParallelToAxis);

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
        void SetSlabRibbed(string name,
            double overallDepth,
            double slabThickness,
            double stemWidthTop,
            double stemWithBottom,
            double ribSpacing,
            eLocalAxisPlane ribsParallelToAxis);

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
        void GetSlabWaffle(string name,
            ref double overallDepth,
            ref double slabThickness,
            ref double stemWidthTop,
            ref double stemWithBottom,
            ref double ribSpacingLocal1,
            ref double ribSpacingLocal2);

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
        void SetSlabWaffle(string name,
            double overallDepth,
            double slabThickness,
            double stemWidthTop,
            double stemWithBottom,
            double ribSpacingLocal1,
            double ribSpacingLocal2);

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
        void GetWall(string name,
            ref double thickness,
            ref eWallPropertyType walltype,
            ref eShellType shellType,
            ref string nameMaterial,
            ref int color,
            ref string notes,
            ref string GUID);

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
        void SetWall(string name,
            double thickness,
            eWallPropertyType walltype,
            eShellType shellType,
            string nameMaterial,
            int color = -1,
            string notes = "",
            string GUID = "");

        // ===        
        /// <summary>
        /// Gets the wall automatic select list.
        /// </summary>
        /// <param name="name">The name of an existing auto select list wall property.</param>
        /// <param name="autoSelectList">The names of the wall properties included in the auto select list.</param>
        /// <param name="startingProperty">This is Median or the name of a wall property in the AutoSelectList array. 
        /// It is the starting section for the auto select list. 
        /// Median indicates the Median Property by Thickness.</param>
        void GetWallAutoSelectList(string name,
            ref string[] autoSelectList,
            ref string startingProperty);

        /// <summary>
        /// Initializes property data for an auto select list wall section.
        /// </summary>
        /// <param name="name">The name of an existing auto select list wall property.</param>
        /// <param name="autoSelectList">The names of the wall properties included in the auto select list.</param>
        /// <param name="startingProperty">This is Median or the name of a wall property in the AutoSelectList array. 
        /// It is the starting section for the auto select list. 
        /// Median indicates the Median Property by Thickness.</param>
        void SetWallAutoSelectList(string name,
            string[] autoSelectList,
            string startingProperty = "Median");
#endif
    }
}