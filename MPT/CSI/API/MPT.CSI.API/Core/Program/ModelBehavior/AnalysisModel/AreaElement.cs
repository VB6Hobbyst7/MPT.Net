// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-11-2017
//
// Last Modified By : Mark
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="AreaElement.cs" company="">
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

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
    /// <summary>
    /// Represents the area element in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IAreaElement" />
    public class AreaElement : CSiApiBase, IAreaElement
    {

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaElement" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AreaElement(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Query
        /// <summary>
        /// This function returns the total number of defined area elements in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.AreaElm.Count();
        }

        /// <summary>
        /// This function retrieves the names of all items.
        /// </summary>
        /// <param name="names">Names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.AreaElm.GetNameList(ref _numberOfItems, ref names);
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
            directionCosines = new double[9];
            _callCode = _sapModel.AreaElm.GetTransformationMatrix(nameObject, ref directionCosines);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of the point elements that define an area element.
        /// </summary>
        /// <param name="name">The name of an existing area element.</param>
        /// <param name="points">The names of the points that defined the area element.
        /// The point names are listed in the positive order around the element.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPoints(string name,
            out string[] points)
        {
            points = new string[0];
            _callCode = _sapModel.AreaElm.GetPoints(name, ref _numberOfItems, ref points);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves the name of the area object from which an area element was created.
        /// </summary>
        /// <param name="name">The name of an existing area element.</param>
        /// <param name="nameObject">The name of the area object from which the area element was created.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetObject(string name,
            out string nameObject)
        {
            nameObject = string.Empty;
            _callCode = _sapModel.AreaElm.GetObj(name, ref nameObject);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Axes
        /// <summary>
        /// This function retrieves the local axis angle assignment for the area .
        /// </summary>
        /// <param name="name">The name of an existing area element.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLocalAxes(string name,
            out AngleLocalAxes angleOffset)
        {
            double angleA = 0;
            _callCode = _sapModel.AreaElm.GetLocalAxes(name, ref angleA);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            angleOffset = new AngleLocalAxes {AngleA = angleA};
        }
        #endregion

        #region Modifiers
        /// <summary>
        /// This function retrieves the modifier assignment for area elements.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing area element or object.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetModifiers(string name,
            out AreaModifier modifiers)
        {
            double[] csiModifiers = new double[0];

            _callCode = _sapModel.AreaElm.GetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            modifiers = new AreaModifier();
            modifiers.FromArray(csiModifiers);
        }
        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// This function retrieves the section property assigned to an area element.
        /// </summary>
        /// <param name="name">The name of a defined area element.</param>
        /// <param name="propertyName">The name of the section property assigned to the area element.
        /// This item is None if there is no section property assigned to the area element.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSection(string name,
            out string propertyName)
        {
            propertyName = string.Empty;
            _callCode = _sapModel.AreaElm.GetProperty(name, ref propertyName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


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

            _callCode = _sapModel.AreaElm.GetThickness(name, ref csiThicknessType, ref thicknessPattern, ref thicknessPatternScaleFactor, ref thicknesses);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            thicknessType = (eAreaThicknessType)csiThicknessType;
        }


        /// <summary>
        /// This function retrieves the material overwrite assigned to an area element, if any.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <param name="name">The name of a defined area element.</param>
        /// <param name="propertyName">This is None, indicating that no material overwrite exists for the specified area element, or it is the name of an existing material property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMaterialOverwrite(string name,
            ref string propertyName)
        {
            _callCode = _sapModel.AreaElm.GetMaterialOverwrite(name, ref propertyName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the material temperature assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element.</param>
        /// <param name="temperature">This is the material temperature value assigned to the element. [T]</param>
        /// <param name="patternName">This is blank or the name of a defined joint pattern.
        /// If it is blank, the material temperature for the line element is uniform along the element at the value specified by <paramref name="temperature" />.
        /// If <paramref name="patternName" /> is the name of a defined joint pattern, the material temperature for the line element may vary from one end to the other.
        /// The material temperature at each end of the element is equal to the specified temperature multiplied by the pattern value at the joint at the end of the line element.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMaterialTemperature(string name,
            out double temperature,
            out string patternName)
        {
            temperature = 0;
            patternName = string.Empty;
            _callCode = _sapModel.AreaElm.GetMatTemp(name, ref temperature, ref patternName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Area Properties
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

            _callCode = _sapModel.AreaElm.GetThickness(name, ref csiOffsetType, ref offsetPattern, ref offsetPatternScaleFactor, ref offsets);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            offsetType = (eAreaOffsetType)csiOffsetType;
        }


        #endregion

        #region Loads
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the gravity load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of gravity loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each gravity load.</param>
        /// <param name="loadPatterns">The name of the coordinate system in which the gravity load multipliers are specified.</param>
        /// <param name="coordinateSystems">The name of the coordinate system associated with each gravity load.</param>
        /// <param name="xLoadMultiplier">Gravity load multipliers in the x direction of the specified coordinate system.</param>
        /// <param name="yLoadMultiplier">Gravity load multipliers in the y direction of the specified coordinate system.</param>
        /// <param name="zLoadMultiplier">Gravity load multipliers in the z direction of the specified coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadGravity(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] coordinateSystems,
            ref double[] xLoadMultiplier,
            ref double[] yLoadMultiplier,
            ref double[] zLoadMultiplier,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            _callCode = _sapModel.AreaElm.GetLoadGravity(name, ref numberItems, ref names, ref loadPatterns, ref coordinateSystems, 
                ref xLoadMultiplier, ref yLoadMultiplier, ref zLoadMultiplier, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the pore pressure load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="numberItems">The total number of pore pressure loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each pore pressure load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each pore pressure load.</param>
        /// <param name="porePressureLoadValues">The pore pressure values. [F/L^2]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify the pore pressure load.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadPorePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] porePressureLoadValues,
            ref string[] jointPatternNames,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            _callCode = _sapModel.AreaElm.GetLoadPorePressure(name, ref numberItems, ref names, ref loadPatterns, 
                ref porePressureLoadValues, ref jointPatternNames, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the strain load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of strain loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each strain load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each strain load.</param>
        /// <param name="component">Indicates the strain component associated with each strain load.</param>
        /// <param name="strainLoadValues">The strain values. [L/L]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify each strain load.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadStrain(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eStrainComponent[] component,
            ref double[] strainLoadValues,
            ref string[] jointPatternNames,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            int[] csiComponent = new int[0];

            _callCode = _sapModel.AreaElm.GetLoadStrain(name, ref numberItems, ref names, ref loadPatterns, 
                ref csiComponent, ref strainLoadValues, ref jointPatternNames, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            component = csiComponent.Cast<eStrainComponent>().ToArray();
        }

        /// <summary>
        /// This function retrieves the surface pressure assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of surface pressure loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each surface pressure load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each surface pressure load.</param>
        /// <param name="faceApplied">The element face to which each specified load assignment applies.
        /// Note that edge face n is from plane element point n to plane element point n + 1. For example, edge face 2 is from plane element point 2 to plane element point 3.</param>
        /// <param name="surfacePressureLoadValues">The surface pressure values. [F/L^2]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify each surface pressure load.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadSurfacePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eFace[] faceApplied,
            ref double[] surfacePressureLoadValues,
            ref string[] jointPatternNames,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            int[] csiFaceApplied = new int[0];

            _callCode = _sapModel.AreaElm.GetLoadSurfacePressure(name, ref numberItems, ref names, ref loadPatterns, 
                ref csiFaceApplied, ref surfacePressureLoadValues, ref jointPatternNames, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            faceApplied = csiFaceApplied.Cast<eFace>().ToArray();
        }
#endif
        /// <summary>
        /// This function retrieves the temperature load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="numberItems">The total number of temperature loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each temperature load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each temperature load.</param>
        /// <param name="temperatureLoadType">Indicates the type of temperature load for each load pattern.</param>
        /// <param name="temperatureLoadValues">Temperature load values. [T] for <paramref name="temperatureLoadType" /> = Temperature, [T/L] for all others.</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify the temperature load.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadTemperature(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadTemperatureType[] temperatureLoadType,
            ref double[] temperatureLoadValues,
            ref string[] jointPatternNames,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            int[] csiTemperatureLoadType = new int[0];

            _callCode = _sapModel.AreaElm.GetLoadTemperature(name, ref numberItems, ref names, ref loadPatterns, 
                ref csiTemperatureLoadType, ref temperatureLoadValues, ref jointPatternNames, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            temperatureLoadType = csiTemperatureLoadType.Cast<eLoadTemperatureType>().ToArray();
        }

        /// <summary>
        /// This function retrieves the uniform load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of uniform loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each uniform load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each uniform load.</param>
        /// <param name="coordinateSystems">The name of the coordinate system associated with each uniform load.</param>
        /// <param name="directionApplied">The direction that the load is applied for each load pattern.</param>
        /// <param name="uniformLoadValues">The uniform load values. [F/L^2]</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadUniform(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] coordinateSystems,
            ref eLoadDirection[] directionApplied,
            ref double[] uniformLoadValues,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            int[] csiDirectionApplied = new int[0];

            _callCode = _sapModel.AreaElm.GetLoadUniform(name, ref numberItems, ref names, ref loadPatterns, 
                ref coordinateSystems, ref csiDirectionApplied, ref uniformLoadValues, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            directionApplied = csiDirectionApplied.Cast<eLoadDirection>().ToArray();
        }


        #endregion
    }
}
