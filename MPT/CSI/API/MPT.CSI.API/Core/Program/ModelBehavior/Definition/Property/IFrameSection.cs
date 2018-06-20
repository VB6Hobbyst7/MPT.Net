using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.Frame;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Implements the frame properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableFrameModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableFrameModifiers" />
    public interface IFrameSection: IChangeableName, ICountable, IDeletable, IListableNames,
        IObservableFrameModifiers, IChangeableFrameModifiers
    {
        #region Properties                            
        /// <summary>
        /// Gets the section designer.
        /// </summary>
        /// <value>The section designer.</value>
        SectionDesigner SectionDesigner { get; }
        #endregion

        #region General

        /// <summary>
        /// This function retrieves the names of all defined frame properties of the specified type.
        /// </summary>
        /// <param name="names">Frame property names retrieved by the program.</param>
        /// <param name="frameType">The frame type to filter the name list by.</param>
        void GetNameList(out string[] names,
            eFrameSectionType frameType);


#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
/// <summary>
/// Gets all frame properties.
/// </summary>
/// <param name="names">Frame property names retrieved by the program.</param>
/// <param name="frameType">The frame type retrieved by the program.</param>
/// <param name="t3">The section depth. [L].</param>
/// <param name="t2">The flange width. [L].</param>
/// <param name="tf">The flange thickness. [L].</param>
/// <param name="tw">The web thickness. [L].</param>
/// <param name="t2b">The bottom flange width. [L].</param>
/// <param name="tfb">The bottom flange thickness. [L].</param>
        void GetAllFrameProperties(ref string[] names,
            ref eFrameSectionType[] frameType,
            ref double[] t3,
            ref double[] t2,
            ref double[] tf,
            ref double[] tw,
            ref double[] t2b,
            ref double[] tfb);
#endif
        #endregion

        #region Methods: Section

        /// <summary>
        /// This function retrieves properties for frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="Ag">The gross cross-sectional area. [L^2].</param>
        /// <param name="As2">The shear area for forces in the section local 2-axis direction. [L^2].</param>
        /// <param name="As3">The shear area for forces in the section local 3-axis direction. [L^2].</param>
        /// <param name="J">The torsional constant. [L^4].</param>
        /// <param name="I22">The moment of inertia for bending about the local 2 axis. [L^4].</param>
        /// <param name="I33">The moment of inertia for bending about the local 3 axis. [L^4].</param>
        /// <param name="S22">The section modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="S33">The section modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="Z22">The plastic modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="Z33">The plastic modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="r22">The radius of gyration about the local 2 axis. [L].</param>
        /// <param name="r33">The radius of gyration about the local 3 axis. [L].</param>
        void GetSectionProperties(string name,
            ref double Ag,
            ref double As2,
            ref double As3,
            ref double J,
            ref double I22,
            ref double I33,
            ref double S22,
            ref double S33,
            ref double Z22,
            ref double Z33,
            ref double r22,
            ref double r33);




        /// <summary>
        /// This function retrieves the property type for the specified frame section property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="frameSectionType">Type of frame section.</param>
        void GetType(string name,
            out eFrameSectionType frameSectionType);

#if !BUILD_ETABS2015 
        /// <summary>
        /// This function retrieves the rebar design type for the specified frame section property.
        /// This function applies only to the following section property types: 
        /// <see cref="eFrameSectionType.TSection"/>;
        /// <see cref="eFrameSectionType.Angle"/>; 
        /// <see cref="eFrameSectionType.Rectangular"/>; 
        /// <see cref="eFrameSectionType.Circle"/>; 
        /// Calling this function for any other type of frame section property returns an error.
        /// A nonzero rebar type is returned only if the frame section property has a concrete material.
        /// TODO: Handle this. </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="rebarType">Design type of the rebar.</param>
        void GetRebarType(string name,
            ref eRebarType rebarType);
#endif
        #endregion

        #region Methods: Imported Section

        /// <summary>
        /// This function imports a frame section property from a property file.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added. 
        /// This name does not need to be the same as the <paramref name="sectionName"/> item.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="fileName">The name of the frame section property file from which to get the frame section property specified by the <paramref name="sectionName"/> item.
        /// In most cases you can input just the name of the property file (e.g.Sections8.pro) and the program will be able to find it.
        /// In some cases you may have to input the full path to the property file.
        /// TODO: Handle this.</param>
        /// <param name="sectionName">The name of the frame section property, inside the property file specified by the <paramref name="fileName"/> item, that is to be imported.</param>
        /// <param name="color">The display color assigned to the section. 
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void ImportSectionProperty(string name,
            string nameMaterial,
            string fileName,
            string sectionName,
            int color = -1,
            string notes = "",
            string GUID = "");

#if !BUILD_ETABS2015 
        /// <summary>
        /// This function retrieves the names of the section property file from which an imported frame section originated, and it also retrieves the section name used in the property file.
        /// If the specified frame section property was not imported, blank strings are returned for <paramref name="nameInFile"/> and <paramref name="fileName"/>.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameInFile">The name of the specified frame section property in the frame section property file.</param>
        /// <param name="fileName">The name of the frame section property file from which the specified frame section property was obtained.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="frameSectionType">Type of frame section.</param>
        void GetNameInPropertyFile(string name,
            ref string nameInFile,
            ref string fileName,
            ref string nameMaterial,
            ref eFrameSectionType frameSectionType);
#endif

        /// <summary>
        /// This function retrieves the names of all defined frame section properties of a specified type in a specified frame section property file.
        /// </summary>
        /// <param name="fileName">The name of the frame section property file from which to get the name list.
        /// In most cases, inputting only the name of the property file (e.g.Sections8.pro) is required, and the program will be able to find it.
        /// In some cases, inputting the full path to the property file may be necessary. </param>
        /// <param name="sectionNames">The property names obtained from the frame section property file.</param>
        /// <param name="frameSectionTypes">The frame section property type for each property obtained from the frame section property file.</param>
        /// <param name="frameSectionType">Type of frame section to filter the list by.
        /// If no value is input for <paramref name="frameSectionType"/>, names are returned for all frame section properties in the specified file regardless of type.</param>
        void GetPropertyFileNameList(string fileName,
            ref string[] sectionNames,
            ref eFrameSectionType[] frameSectionTypes,
            eFrameSectionType frameSectionType);
        #endregion

        #region Methods: Get/Set Sections - Other

        /// <summary>
        ///This function retrieves frame section property data for a general frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="Ag">The gross cross-sectional area. [L^2].</param>
        /// <param name="As2">The shear area for forces in the section local 2-axis direction. [L^2].</param>
        /// <param name="As3">The shear area for forces in the section local 3-axis direction. [L^2].</param>
        /// <param name="J">The torsional constant. [L^4].</param>
        /// <param name="I22">The moment of inertia for bending about the local 2 axis. [L^4].</param>
        /// <param name="I33">The moment of inertia for bending about the local 3 axis. [L^4].</param>
        /// <param name="S22">The section modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="S33">The section modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="Z22">The plastic modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="Z33">The plastic modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="r22">The radius of gyration about the local 2 axis. [L].</param>
        /// <param name="r33">The radius of gyration about the local 3 axis. [L].</param>
        /// <param name="color">The display color assigned to the section. </param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetGeneral(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double Ag,
            ref double As2,
            ref double As3,
            ref double J,
            ref double I22,
            ref double I33,
            ref double S22,
            ref double S33,
            ref double Z22,
            ref double Z33,
            ref double r22,
            ref double r33,
            ref int color,
            ref string notes,
            ref string GUID);


        /// <summary>
        /// This function initializes a general frame section property. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="Ag">The gross cross-sectional area. [L^2].</param>
        /// <param name="As2">The shear area for forces in the section local 2-axis direction. [L^2].</param>
        /// <param name="As3">The shear area for forces in the section local 3-axis direction. [L^2].</param>
        /// <param name="J">The torsional constant. [L^4].</param>
        /// <param name="I22">The moment of inertia for bending about the local 2 axis. [L^4].</param>
        /// <param name="I33">The moment of inertia for bending about the local 3 axis. [L^4].</param>
        /// <param name="S22">The section modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="S33">The section modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="Z22">The plastic modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="Z33">The plastic modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="r22">The radius of gyration about the local 2 axis. [L].</param>
        /// <param name="r33">The radius of gyration about the local 3 axis. [L].</param>
        /// <param name="color">The display color assigned to the section. 
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetGeneral(string name,
            string nameMaterial,
            double t3,
            double t2,
            double Ag,
            double As2,
            double As3,
            double J,
            double I22,
            double I33,
            double S22,
            double S33,
            double Z22,
            double Z33,
            double r22,
            double r33,
            int color = -1,
            string notes = "",
            string GUID = "");




        /// <summary>
        /// Gets the non prismatic.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.</param>
        /// <param name="startSections">The names of the frame section properties at the start of each segment.</param>
        /// <param name="endSections">The names of the frame section properties at the start of each segment.</param>
        /// <param name="lengths">The length of each segment. 
        /// The length may be variable or absolute as indicated by the <paramref name="prismaticTypes"/> item. [L] when length is absolute.</param>
        /// <param name="prismaticTypes">The prismatic length type of each segment.</param>
        /// <param name="EI33">The variation type for EI33 in each segment.</param>
        /// <param name="EI22">The variation type for EI22 in each segment.</param>
        /// <param name="color">The display color assigned to the section. </param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetNonPrismatic(string name,
            ref string[] startSections,
            ref string[] endSections,
            ref double[] lengths,
            ref ePrismaticType[] prismaticTypes,
            ref ePrismaticInertiaType[] EI33,
            ref ePrismaticInertiaType[] EI22,
            ref int color,
            ref string notes,
            ref string GUID);


        /// <summary>
        /// This function assigns data to a nonprismatic frame section property.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="startSections">The names of the frame section properties at the start of each segment.
        /// Auto select lists and nonprismatic sections are not allowed.</param>
        /// <param name="endSections">The names of the frame section properties at the start of each segment.
        /// Auto select lists and nonprismatic sections are not allowed.</param>
        /// <param name="lengths">The length of each segment. 
        /// The length may be variable or absolute as indicated by the <paramref name="prismaticTypes"/> item. [L] when length is absolute.</param>
        /// <param name="prismaticTypes">The prismatic length type of each segment.</param>
        /// <param name="EI33">The variation type for EI33 in each segment.</param>
        /// <param name="EI22">The variation type for EI22 in each segment.</param>
        /// <param name="color">The display color assigned to the section. 
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetNonPrismatic(string name,
            string[] startSections,
            string[] endSections,
            double[] lengths,
            ePrismaticType[] prismaticTypes,
            ePrismaticInertiaType[] EI33,
            ePrismaticInertiaType[] EI22,
            int color = -1,
            string notes = "",
            string GUID = "");




#if !BUILD_ETABS2015
        /// <summary>
        /// Gets the section designer section.
        /// </summary>
        /// <param name="name">The name of an existing section designer pro.</param>
        /// <param name="nameMaterial">The name of the base material property for the sec.</param>
        /// <param name="shapeNames">The name of each shape in the section designer section.</param>
        /// <param name="sectionTypes">The type of each shape in the section designer section.</param>
        /// <param name="designType">The design option for the section.</param>
        /// <param name="color">The display color assigned to the section. </param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetSectionDesignerSection(string name,
            ref string nameMaterial,
            ref string[] shapeNames,
            ref eSectionDesignerSectionType[] sectionTypes,
            ref eSectionDesignerDesignOption designType,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// Sets the section designer section.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the base material property for the section.</param>
        /// <param name="designType">The design option for the section.
        /// When <paramref name="designType"/> = <see cref="eSectionDesignerDesignOption.GeneralSteel"/> is assigned, the material property specified by the <paramref name="nameMaterial"/> item must be a steel material; 
        /// otherwise the program sets <paramref name="designType"/> = <see cref="eSectionDesignerDesignOption.NoDesign"/>.
        /// Similarly, when <paramref name="designType"/> = <see cref="eSectionDesignerDesignOption.ConcreteColumnCheck"/> or <paramref name="designType"/> = <see cref="eSectionDesignerDesignOption.ConcreteColumnDesign"/> is assigned, the material property specified by the <paramref name="nameMaterial"/> item must be a concrete material; 
        /// otherwise the program sets <paramref name="designType"/> = <see cref="eSectionDesignerDesignOption.NoDesign"/>.</param>
        /// <param name="color">The display color assigned to the section. 
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetSectionDesignerSection(string name,
            string nameMaterial,
            eSectionDesignerDesignOption designType,
            int color = -1,
            string notes = "",
            string GUID = "");
#endif
        #endregion

        #region Methods: Get/Set Sections - Steel: Auto-Select
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves frame section property data for an aluminum auto select list.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetAutoSelectAluminum(string name,
            ref string[] sectionNames,
            ref string autoStartSection,
            ref string notes,
            ref string GUID);


        /// <summary>
        /// This function assigns frame section properties to an aluminum auto select list. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.
        /// Auto select lists and nonprismatic (variable) sections are not allowed in this array.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetAutoSelectAluminum(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "");



        /// <summary>
        /// This function retrieves frame section property data for cold-formed steel auto select list.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetAutoSelectColdFormed(string name,
            ref string[] sectionNames,
            ref string autoStartSection,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function assigns frame section properties to a cold-formed steel auto select list. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.
        /// Auto select lists and nonprismatic (variable) sections are not allowed in this array.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetAutoSelectColdFormed(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "");
#endif



        /// <summary>
        /// This function retrieves frame section property data for a steel auto select list.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetAutoSelectSteel(string name,
            ref string[] sectionNames,
            ref string autoStartSection,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function assigns frame section properties to a steel auto select list. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.
        /// Auto select lists and nonprismatic (variable) sections are not allowed in this array.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetAutoSelectSteel(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "");
#endregion

#region Methods: Get/Set Sections - Steel

#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function retrieves frame section property data for a steel tee-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="r">The fillet radius. [L]</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetSteelTee(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double r,
            ref bool mirrorAbout3,
            ref int color,
            ref string notes,
            ref string GUID);


        /// <summary>
        /// This function initializes a steel tee-type frame section property. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="r">The fillet radius. [L]</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetSteelTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double r,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "");




        /// <summary>
        /// This function retrieves frame section property data for a steel angle-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="r">The fillet radius. [L]</param>
        /// <param name="mirrorAbout2">True: The section is mirrored about the local 2-axis.</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetSteelAngle(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double r,
            ref bool mirrorAbout2,
            ref bool mirrorAbout3,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a steel angle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="r">The fillet radius. [L]</param>
        /// <param name="mirrorAbout2">True: The section is mirrored about the local 2-axis.</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetSteelAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double r,
            bool mirrorAbout2,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "");
#endif

        /// <summary>
        /// This function retrieves frame section property data for a channel-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetChannel(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a channel-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetChannel(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "");




        /// <summary>
        /// This function retrieves frame section property data for a double angle-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="separation">The back-to-back distance between the angles. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetDoubleAngle(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double separation,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a double angle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="separation">The back-to-back distance between the angles. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetDoubleAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double separation,
            int color = -1,
            string notes = "",
            string GUID = "");





        /// <summary>
        /// This function retrieves frame section property data for a double channel-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="separation">The back-to-back distance between the angles. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetDoubleChannel(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double separation,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a double channel-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="separation">The back-to-back distance between the angles. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetDoubleChannel(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double separation,
            int color = -1,
            string notes = "",
            string GUID = "");




        /// <summary>
        /// This function retrieves frame section property data for an I-Section-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The top flange width. [L].</param>
        /// <param name="tf">The top flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="t2Bottom">The bottom flange width. [L].</param>
        /// <param name="tfBottom">The bottom flange thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetISection(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double t2Bottom,
            ref double tfBottom,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for an I-Section-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The top flange width. [L].</param>
        /// <param name="tf">The top flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="t2Bottom">The bottom flange width. [L].</param>
        /// <param name="tfBottom">The bottom flange thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetISection(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            ref double t2Bottom,
            ref double tfBottom,
            int color = -1,
            string notes = "",
            string GUID = "");
#endregion

#region Methods: Get/Set Sections - Steel: Built-Up

        /// <summary>
        /// This function retrieves frame section property data for a cover plated I-Section-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="sectionName">The name of an existing I-type frame section property that is used for the I-section portion of the coverplated I section.</param>
        /// <param name="fyTopFlange">The yield strength of the top flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName"/> item is used.</param>
        /// <param name="fyWeb">The yield strength of the web of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName"/> item is used.</param>
        /// <param name="fyBottomFlange">The yield strength of the bottom flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName"/> item is used.</param>
        /// <param name="tc">The thickness of the top cover plate. [L]
        /// If the <paramref name="tc"/> or the <paramref name="bc"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bc">The width of the top cover plate. [L]
        /// If the <paramref name="tc"/> or the <paramref name="bc"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialTop">The name of the material property for the top cover plate.
        /// This item applies only if both the <paramref name="tc"/> and the <paramref name="bc"/> items are greater than 0.</param>
        /// <param name="tcBottom">The thickness of the bottom cover plate. [L]
        /// If the <paramref name="tcBottom"/> or the <paramref name="bcBottom"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bcBottom">The width of the bottom cover plate. [L]
        /// If the <paramref name="tcBottom"/> or the <paramref name="bcBottom"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialBottom">The name of the material property for the bottom cover plate.
        /// This item applies only if both the <paramref name="tcBottom"/> and the <paramref name="bcBottom"/> items are greater than 0.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetCoverPlatedI(string name,
            ref string sectionName,
            ref double fyTopFlange,
            ref double fyWeb,
            ref double fyBottomFlange,
            ref double tc,
            ref double bc,
            ref string nameMaterialTop,
            ref double tcBottom,
            ref double bcBottom,
            ref string nameMaterialBottom,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a cover plated I-Section-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionName">The name of an existing I-type frame section property that is used for the I-section portion of the coverplated I section.</param>
        /// <param name="fyTopFlange">The yield strength of the top flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName"/> item is used.</param>
        /// <param name="fyWeb">The yield strength of the web of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName"/> item is used.</param>
        /// <param name="fyBottomFlange">The yield strength of the bottom flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName"/> item is used.</param>
        /// <param name="tc">The thickness of the top cover plate. [L]
        /// If the <paramref name="tc"/> or the <paramref name="bc"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bc">The width of the top cover plate. [L]
        /// If the <paramref name="tc"/> or the <paramref name="bc"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialTop">The name of the material property for the top cover plate.
        /// This item applies only if both the <paramref name="tc"/> and the <paramref name="bc"/> items are greater than 0.</param>
        /// <param name="tcBottom">The thickness of the bottom cover plate. [L]
        /// If the <paramref name="tcBottom"/> or the <paramref name="bcBottom"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bcBottom">The width of the bottom cover plate. [L]
        /// If the <paramref name="tcBottom"/> or the <paramref name="bcBottom"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialBottom">The name of the material property for the bottom cover plate.
        /// This item applies only if both the <paramref name="tcBottom"/> and the <paramref name="bcBottom"/> items are greater than 0.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetCoverPlatedI(string name,
            string sectionName,
            double fyTopFlange,
            double fyWeb,
            double fyBottomFlange,
            double tc,
            double bc,
            string nameMaterialTop,
            double tcBottom,
            double bcBottom,
            string nameMaterialBottom,
            int color = -1,
            string notes = "",
            string GUID = "");




#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves frame section property data for a steel hybrid I-Section-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameMaterialTopFlange">The name of the material property for the top flange.</param>
        /// <param name="nameMaterialWeb">The name of the material property for the web.</param>
        /// <param name="nameMaterialBottomFlange">The name of the material property for the bottom flange.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The top flange width. [L].</param>
        /// <param name="tf">The top flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="t2Bottom">The bottom flange width. [L].</param>
        /// <param name="tfBottom">The bottom flange thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetHybridISection(string name,
            ref string nameMaterialTopFlange,
            ref string nameMaterialWeb,
            ref string nameMaterialBottomFlange,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double t2Bottom,
            ref double tfBottom,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a steel hybrid I-Section-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterialTopFlange">The name of the material property for the top flange.</param>
        /// <param name="nameMaterialWeb">The name of the material property for the web.</param>
        /// <param name="nameMaterialBottomFlange">The name of the material property for the bottom flange.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The top flange width. [L].</param>
        /// <param name="tf">The top flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="t2Bottom">The bottom flange width. [L].</param>
        /// <param name="tfBottom">The bottom flange thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetHybridISection(string name,
            string nameMaterialTopFlange,
            string nameMaterialWeb,
            string nameMaterialBottomFlange,
            double t3,
            double t2,
            double tf,
            double tw,
            double t2Bottom,
            double tfBottom,
            int color = -1,
            string notes = "",
            string GUID = "");





        /// <summary>
        /// This function retrieves frame section property data for a steel hybrid U-Section-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameMaterialTopFlange">The name of the material property for the top flange.</param>
        /// <param name="nameMaterialWeb">The name of the material property for the web.</param>
        /// <param name="nameMaterialBottomFlange">The name of the material property for the bottom flange.</param>
        /// <param name="D1">Web Depth (vertical, inside to inside of flanges). [L].</param>
        /// <param name="B1">Web Distance at Top (CL to CL). [L].</param>
        /// <param name="B2">Bottom Flange Width. [L].</param>
        /// <param name="B3">Top Flange Width (per each). [L].</param>
        /// <param name="B4">Bottom Flange Lip (Web CL to flange edge, may be zero). [L].</param>
        /// <param name="tw">Web Thickness. [L].</param>
        /// <param name="tf">Top Flange Thickness. [L].</param>
        /// <param name="tfb">Bottom Flange Thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetHybridUSection(string name,
            ref string nameMaterialTopFlange,
            ref string nameMaterialWeb,
            ref string nameMaterialBottomFlange,
            ref double D1,
            ref double B1,
            ref double B2,
            ref double B3,
            ref double B4,
            ref double tw,
            ref double tf,
            ref double tfb,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a steel hybrid U-Section-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterialTopFlange">The name of the material property for the top flange.</param>
        /// <param name="nameMaterialWeb">The name of the material property for the web.</param>
        /// <param name="nameMaterialBottomFlange">The name of the material property for the bottom flange.</param>
        /// <param name="D1">Web Depth (vertical, inside to inside of flanges). [L].</param>
        /// <param name="B1">Web Distance at Top (CL to CL). [L].</param>
        /// <param name="B2">Bottom Flange Width. [L].</param>
        /// <param name="B3">Top Flange Width (per each). [L].</param>
        /// <param name="B4">Bottom Flange Lip (Web CL to flange edge, may be zero). [L].</param>
        /// <param name="tw">Web Thickness. [L].</param>
        /// <param name="tf">Top Flange Thickness. [L].</param>
        /// <param name="tfb">Bottom Flange Thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetHybridUSection(string name,
            string nameMaterialTopFlange,
            string nameMaterialWeb,
            string nameMaterialBottomFlange,
            double D1,
            double B1,
            double B2,
            double B3,
            double B4,
            double tw,
            double tf,
            double tfb,
            int color = -1,
            string notes = "",
            string GUID = "");
#endif
#endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
#region Methods: Get/Set Sections - Cold-Formed Steel

        /// <summary>
        /// This function retrieves frame section property data for a cold formed C-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetColdC(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double thickness,
            ref double radius,
            ref double lipDepth,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a cold formed C-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetColdC(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            int color = -1,
            string notes = "",
            string GUID = "");





        /// <summary>
        /// This function retrieves frame section property data for a cold formed hat-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetColdHat(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double thickness,
            ref double radius,
            ref double lipDepth,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a cold formed hat-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetColdHat(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            int color = -1,
            string notes = "",
            string GUID = "");





        /// <summary>
        /// This function retrieves frame section property data for a cold formed Z-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="lipAngle">The lip angle measured from horizontal (0 &lt;= <paramref name="lipAngle"/>  &lt;= 90). [deg].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetColdZ(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double thickness,
            ref double radius,
            ref double lipDepth,
            ref double lipAngle,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a cold formed Z-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="lipAngle">The lip angle measured from horizontal (0 &lt;= <paramref name="lipAngle"/>  &lt;= 90). [deg].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetColdZ(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            double lipAngle,
            int color = -1,
            string notes = "",
            string GUID = "");
#endregion
#endif

#region Methods: Get/Set Sections - Steel/Concrete
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function retrieves frame section property data for a plate-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t2">Plate width. [L].</param>
        /// <param name="t3">Plate depth. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetPlate(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a plate-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t2">Plate width. [L].</param>
        /// <param name="t3">Plate depth. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void SetPlate(string name,
            string nameMaterial,
            double t3,
            double t2,
            int color = -1,
            string notes = "",
            string GUID = "");


        /// <summary>
        /// This function retrieves frame section property data for a rod-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetRod(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a rod-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void SetRod(string name,
            string nameMaterial,
            double t3,
            int color = -1,
            string notes = "",
            string GUID = "");
#endif

        /// <summary>
        /// This function retrieves frame section property data for a circle-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetCircle(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a circle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetCircle(string name,
            string nameMaterial,
            double t3,
            int color = -1,
            string notes = "",
            string GUID = "");




        /// <summary>
        /// This function retrieves frame section property data for a rectangle-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetRectangle(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a rectangle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetRectangle(string name,
            string nameMaterial,
            double t3,
            double t2,
            int color = -1,
            string notes = "",
            string GUID = "");





        /// <summary>
        /// This function retrieves frame section property data for a pipe-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="tw">The wall thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetPipe(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double tw,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a pipe-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="tw">The wall thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetPipe(string name,
            string nameMaterial,
            double t3,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "");





        /// <summary>
        /// This function retrieves frame section property data for a tube-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetTube(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for a tube-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetTube(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "");


        /// <summary>
        /// This function retrieves frame section property data for a tee-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetTee(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref int color,
            ref string notes,
            ref string GUID);


        /// <summary>
        /// This function initializes a tee-type frame section property. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "");




        /// <summary>
        /// This function retrieves frame section property data for an angle-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetAngle(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes frame section property data for an angle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "");
#endregion

#region Methods: Get/Set Sections - Concrete: Reinforced
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function retrieves frame section property data for a concrete L-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="twF">The vertical leg thickness at the flange. [L].</param>
        /// <param name="tfT">The vertical leg thickness at the tip. [L].</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void GetConcreteTee(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double twF,
            ref double tfT,
            ref bool mirrorAbout3,
            ref int color,
            ref string notes,
            ref string GUID);


        /// <summary>
        /// This function initializes frame section property data for a concrete L-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="twF">The vertical leg thickness at the flange. [L].</param>
        /// <param name="tfT">The vertical leg thickness at the tip. [L].</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetConcreteTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double twF,
            double tfT,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "");


        /// <summary>
        /// This function retrieves frame section property data for a concrete L-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="twC">The vertical leg thickness at the corner. [L].</param>
        /// <param name="tfT">The vertical leg thickness at the tip. [L].</param>
        /// <param name="mirrorAbout2">True: The section is mirrored about the local 2-axis.</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void GetConcreteL(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double twC,
            ref double tfT,
            ref bool mirrorAbout2,
            ref bool mirrorAbout3,
            ref int color,
            ref string notes,
            ref string GUID);


        /// <summary>
        /// This function initializes frame section property data for a concrete L-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="twC">The vertical leg thickness at the corner. [L].</param>
        /// <param name="tfT">The vertical leg thickness at the tip. [L].</param>
        /// <param name="mirrorAbout2">True: The section is mirrored about the local 2-axis.</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetConcreteL(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double twC,
            double tfT,
            bool mirrorAbout2,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "");
#endif


        /// <summary>
        /// This function retrieves beam rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete or this function returns an error.
        /// This function applies only to the following section types. Calling this function for any other type of frame section property returns an error:
        /// <see cref="eFrameSectionType.TSection"/>; 
        /// <see cref="eFrameSectionType.Angle"/>;
        /// <see cref="eFrameSectionType.Rectangular"/>;
        /// <see cref="eFrameSectionType.Circle"/>
        /// TODO: Handle
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="materialNameLongitudinal">The name of the rebar material property for the longitudinal rebar.</param>
        /// <param name="materialNameConfinement">The name of the rebar material property for the confinement rebar.</param>
        /// <param name="coverTop">The distance from the top of the beam to the centroid of the top longitudinal reinforcement. [L].</param>
        /// <param name="coverBottom">The distance from the bottom of the beam to the centroid of the bottom longitudinal reinforcement. [L].</param>
        /// <param name="topLeftArea">The total area of longitudinal reinforcement at the top left end of the beam. [L^2].</param>
        /// <param name="topRightArea">The total area of longitudinal reinforcement at the top right end of the beam. [L^2].</param>
        /// <param name="bottomLeftArea">The total area of longitudinal reinforcement at the bottom left end of the beam. [L^2].</param>
        /// <param name="bottomRightArea">The total area of longitudinal reinforcement at the bottom right end of the beam. [L^2].</param>
        void GetRebarBeam(string name,
            ref string materialNameLongitudinal,
            ref string materialNameConfinement,
            ref double coverTop,
            ref double coverBottom,
            ref double topLeftArea,
            ref double topRightArea,
            ref double bottomLeftArea,
            ref double bottomRightArea);


        /// <summary>
        /// This function assigns beam rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete or this function returns an error.
        /// This function applies only to the following section types. Calling this function for any other type of frame section property returns an error:
        /// <see cref="eFrameSectionType.TSection"/>; 
        /// <see cref="eFrameSectionType.Angle"/>;
        /// <see cref="eFrameSectionType.Rectangular"/>;
        /// <see cref="eFrameSectionType.Circle"/>
        /// TODO: Handle
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="materialNameLongitudinal">The material name longitudinal.</param>
        /// <param name="materialNameConfinement">The material name confinement.</param>
        /// <param name="coverTop">The cover top.</param>
        /// <param name="coverBottom">The cover bottom.</param>
        /// <param name="topLeftArea">The top left area.</param>
        /// <param name="topRightArea">The top right area.</param>
        /// <param name="bottomLeftArea">The bottom left area.</param>
        /// <param name="bottomRightArea">The bottom right area.</param>
        void SetRebarBeam(string name,
            string materialNameLongitudinal,
            string materialNameConfinement,
            double coverTop,
            double coverBottom,
            double topLeftArea,
            double topRightArea,
            double bottomLeftArea,
            double bottomRightArea);



        /// <summary>
        /// This function retrieves column rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete or else this function returns an error.
        /// Calling this function for any type of frame section property other than the following returns an error:
        /// <see cref="eFrameSectionType.Rectangular"/>;
        /// <see cref="eFrameSectionType.Circle"/>;
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="materialPropertyLongitudinal">The name of the rebar material property for the longitudinal rebar.</param>
        /// <param name="materialNameConfinement">The name of the rebar material property for the confinement rebar.</param>
        /// <param name="rebarConfiguration">The rebar configuration.
        /// For circular frame section properties this item must be <see cref="eRebarConfiguration.Circular"/>; otherwise an error is returned.
        /// TODO: Handle This</param>
        /// <param name="confinementType">Type of the confinement.
        /// This item applies only when <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Circular"/>. 
        /// If Pattern = 1, the confinement bar type is assumed to be ties.</param>
        /// <param name="cover">The clear cover for the confinement steel (ties). 
        /// In the special case of circular reinforcement in a rectangular column, this is the minimum clear cover. [L].</param>
        /// <param name="numberOfCircularBars">The total number of longitudinal reinforcing bars in the column.
        /// This item applies to a circular rebar configuration, <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Circular"/>.</param>
        /// <param name="numberOfRectangularBars3Axis">The number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 3-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="numberOfRectangularBars2Axis">is the number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 2-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="rebarSize">The rebar name for the longitudinal rebar in the column.</param>
        /// <param name="tieSize">The rebar name for the confinement rebar in the column.</param>
        /// <param name="tieSpacingLongitudinal">The longitudinal spacing of the confinement bars (ties). [L].</param>
        /// <param name="numberOfConfinementBars2Axis">It is the number of confinement bars (tie legs) running in the local 2-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="numberOfConfinementBars3Axis">It is the number of confinement bars (tie legs) running in the local 3-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="toBeDesigned">True: The column longitudinal rebar is to be designed; otherwise it is to be checked.</param>
        void GetRebarColumn(string name,
            ref string materialPropertyLongitudinal,
            ref string materialNameConfinement,
            ref int rebarConfiguration,
            ref int confinementType,
            ref double cover,
            ref int numberOfCircularBars,
            ref int numberOfRectangularBars3Axis,
            ref int numberOfRectangularBars2Axis,
            ref string rebarSize,
            ref string tieSize,
            ref double tieSpacingLongitudinal,
            ref int numberOfConfinementBars2Axis,
            ref int numberOfConfinementBars3Axis,
            ref bool toBeDesigned);


        /// <summary>
        /// This function assigns column rebar data to frame sections.
        /// The material assigned to the specified frame section property must be concrete or else this function returns an error.
        /// Calling this function for any type of frame section property other than the following returns an error:
        /// <see cref="eFrameSectionType.Rectangular"/>;
        /// <see cref="eFrameSectionType.Circle"/>;
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="materialPropertyLongitudinal">The name of the rebar material property for the longitudinal rebar.</param>
        /// <param name="materialNameConfinement">The name of the rebar material property for the confinement rebar.</param>
        /// <param name="rebarConfiguration">The rebar configuration.
        /// For circular frame section properties this item must be <see cref="eRebarConfiguration.Circular"/>; otherwise an error is returned.
        /// TODO: Handle This</param>
        /// <param name="confinementType">Type of the confinement.
        /// This item applies only when <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Circular"/>. 
        /// If Pattern = 1, the confinement bar type is assumed to be ties.</param>
        /// <param name="cover">The clear cover for the confinement steel (ties). 
        /// In the special case of circular reinforcement in a rectangular column, this is the minimum clear cover. [L].</param>
        /// <param name="numberOfCircularBars">The total number of longitudinal reinforcing bars in the column.
        /// This item applies to a circular rebar configuration, <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Circular"/>.</param>
        /// <param name="numberOfRectangularBars3Axis">The number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 3-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="numberOfRectangularBars2Axis">is the number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 2-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="rebarSize">The rebar name for the longitudinal rebar in the column.</param>
        /// <param name="tieSize">The rebar name for the confinement rebar in the column.</param>
        /// <param name="tieSpacingLongitudinal">The longitudinal spacing of the confinement bars (ties). [L].</param>
        /// <param name="numberOfConfinementBars2Axis">It is the number of confinement bars (tie legs) running in the local 2-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="numberOfConfinementBars3Axis">It is the number of confinement bars (tie legs) running in the local 3-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="toBeDesigned">True: The column longitudinal rebar is to be designed; otherwise it is to be checked.</param>
        void SetRebarColumn(string name,
            string materialPropertyLongitudinal,
            string materialNameConfinement,
            eRebarConfiguration rebarConfiguration,
            eConfinementType confinementType,
            double cover,
            int numberOfCircularBars,
            int numberOfRectangularBars3Axis,
            int numberOfRectangularBars2Axis,
            string rebarSize,
            string tieSize,
            double tieSpacingLongitudinal,
            int numberOfConfinementBars2Axis,
            int numberOfConfinementBars3Axis,
            bool toBeDesigned);
#endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
#region Methods: Get/Set Sections - Concrete: Precast


        /// <summary>
        /// This function retrieves frame section property data for a precast concrete I girder frame section.
        /// </summary>
        /// <param name="name">The name of an existing precast concrete I girder frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="B1">The horizontal section dimension B1 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="B2">The horizontal section dimension B2 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="B3">The horizontal section dimension B3 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="B4">The horizontal section dimension B4 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D1">The vertical section dimension D1 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D2">The vertical section dimension D2 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D3">The vertical section dimension D3 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D4">The vertical section dimension D4 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D5">The vertical section dimension D5 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D6">The vertical section dimension D6 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D7">The vertical section dimension D7 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="T1">The web thickness dimension T1 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="T2">The web thickness dimension T2 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="C1">The bottom flange chamfer dimension, denoted as C1 on the precast concrete I girder definition form.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetPrecastI(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double B1,
            ref double B2,
            ref double B3,
            ref double B4,
            ref double D1,
            ref double D2,
            ref double D3,
            ref double D4,
            ref double D5,
            ref double D6,
            ref double D7,
            ref double T1,
            ref double T2,
            ref double C1,
            ref int color,
            ref string notes,
            ref string GUID);


        /// <summary>
        /// This function initializes a precast concrete I girder frame section property. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="B1">The horizontal section dimension B1 defined on the precast concrete I girder definition form. B1 &gt; 0, [L].</param>
        /// <param name="B2">The horizontal section dimension B2 defined on the precast concrete I girder definition form. B2 &gt; 0, [L].</param>
        /// <param name="B3">The horizontal section dimension B3 defined on the precast concrete I girder definition form. B3 &gt;= 0, [L].</param>
        /// <param name="B4">The horizontal section dimension B4 defined on the precast concrete I girder definition form. B4 &gt;= 0, [L].</param>
        /// <param name="D1">The vertical section dimension D1 defined on the precast concrete I girder definition form. D1 &gt; 0, [L].</param>
        /// <param name="D2">The vertical section dimension D2 defined on the precast concrete I girder definition form. D2 &gt; 0, [L].</param>
        /// <param name="D3">The vertical section dimension D3 defined on the precast concrete I girder definition form. D3 &gt;= 0, [L].</param>
        /// <param name="D4">The vertical section dimension D4 defined on the precast concrete I girder definition form. D4 &gt;= 0, [L].</param>
        /// <param name="D5">The vertical section dimension D5 defined on the precast concrete I girder definition form. D5 &gt; 0, [L].</param>
        /// <param name="D6">The vertical section dimension D6 defined on the precast concrete I girder definition form. D6 &gt;= 0, [L].</param>
        /// <param name="D7">The vertical section dimension D7 defined on the precast concrete I girder definition form. D7 &gt;= 0, [L].</param>
        /// <param name="T1">The web thickness dimension T1 defined on the precast concrete I girder definition form. T1 &gt; 0, [L].</param>
        /// <param name="T2">The web thickness dimension T2 defined on the precast concrete I girder definition form. T2 &gt; 0, [L].</param>
        /// <param name="C1">The bottom flange chamfer dimension, denoted as C1 on the precast concrete I girder definition form.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetPrecastI(string name,
            string nameMaterial,
            double B1,
            double B2,
            double B3,
            double B4,
            double D1,
            double D2,
            double D3,
            double D4,
            double D5,
            double D6,
            double D7,
            double T1,
            double T2,
            double C1,
            int color = -1,
            string notes = "",
            string GUID = "");




        /// <summary>
        /// This function retrieves frame section property data for a precast concrete U girder frame section.
        /// </summary>
        /// <param name="name">The name of an existing precast concrete I girder frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="B1">The horizontal section dimension B1 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="B2">The horizontal section dimension B2 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="B3">The horizontal section dimension B3 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="B4">The horizontal section dimension B4 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="B5">The horizontal section dimension B5 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="B6">The horizontal section dimension B6 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D1">The vertical section dimension D1 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D2">The vertical section dimension D2 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D3">The vertical section dimension D3 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D4">The vertical section dimension D4 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D5">The vertical section dimension D5 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D6">The vertical section dimension D6 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D7">The vertical section dimension D7 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetPrecastU(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double B1,
            ref double B2,
            ref double B3,
            ref double B4,
            ref double B5,
            ref double B6,
            ref double D1,
            ref double D2,
            ref double D3,
            ref double D4,
            ref double D5,
            ref double D6,
            ref double D7,
            ref int color,
            ref string notes,
            ref string GUID);


        /// <summary>
        /// This function initializes a precast concrete U girder frame section property. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="B1">The horizontal section dimension B1 defined on the precast concrete U girder definition form. B1 &gt; 0, [L].</param>
        /// <param name="B2">The horizontal section dimension B2 defined on the precast concrete U girder definition form. B2 &gt; 0, [L].</param>
        /// <param name="B3">The horizontal section dimension B3 defined on the precast concrete U girder definition form. B3 &gt; 0, [L].</param>
        /// <param name="B4">The horizontal section dimension B4 defined on the precast concrete U girder definition form. B4 &gt;= 0, [L].</param>
        /// <param name="B5">The horizontal section dimension B5 defined on the precast concrete U girder definition form. B5 &gt;= 0, [L].</param>
        /// <param name="B6">The horizontal section dimension B6 defined on the precast concrete U girder definition form. B6 &gt;= 0, [L].</param>
        /// <param name="D1">The vertical section dimension D1 defined on the precast concrete U girder definition form. D1 &gt; 0, [L].</param>
        /// <param name="D2">The vertical section dimension D2 defined on the precast concrete U girder definition form. D2 &gt; 0, [L].</param>
        /// <param name="D3">The vertical section dimension D3 defined on the precast concrete U girder definition form. D3 &gt;= 0, [L].</param>
        /// <param name="D4">The vertical section dimension D4 defined on the precast concrete U girder definition form. D4 &gt;= 0, [L].</param>
        /// <param name="D5">The vertical section dimension D5 defined on the precast concrete U girder definition form. D5 &gt;= 0, [L].</param>
        /// <param name="D6">The vertical section dimension D6 defined on the precast concrete U girder definition form. D6 &gt;= 0, [L].</param>
        /// <param name="D7">The vertical section dimension D7 defined on the precast concrete U girder definition form. D7 &gt;= 0, [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetPrecastU(string name,
            string nameMaterial,
            double B1,
            double B2,
            double B3,
            double B4,
            double B5,
            double B6,
            double D1,
            double D2,
            double D3,
            double D4,
            double D5,
            double D6,
            double D7,
            int color = -1,
            string notes = "",
            string GUID = "");

#endregion
#endif
    }
}