// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-11-2017
//
// Last Modified By : Mark
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="LineElement.cs" company="">
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
    /// Represents the line element in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.ILineElement" />
    public class LineElement : CSiApiBase, ILineElement
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="LineElement" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public LineElement(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Query
        /// <summary>
        /// This function returns the total number of defined line elements in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.LineElm.Count();
        }

        /// <summary>
        /// This function retrieves the names of all items.
        /// </summary>
        /// <param name="names">Names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.LineElm.GetNameList(ref _numberOfItems, ref names);
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
            _callCode = _sapModel.LineElm.GetTransformationMatrix(nameObject, ref directionCosines);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of the point elements that define a line element.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="points">The names of the points that defined the line element.
        /// The point names are listed in the positive order around the element.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPoints(string name,
            out string[] points)
        {
            string point1 = "";
            string point2 = "";

            _callCode = _sapModel.LineElm.GetPoints(name, ref point1, ref point2);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            points = new string[2];
            points[0] = point1;
            points[1] = point2;
        }

        /// <summary>
        /// This function retrieves the name of the line object from which a line element was created.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="nameObject">The name of the line object from which the line element was created.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetObject(string name,
            out string nameObject)
        {
            eLineTypeObject csiObjectType;
            double relativeDistanceI;
            double relativeDistanceJ;

            GetObject(name, out nameObject, out csiObjectType, out relativeDistanceI, out relativeDistanceJ);
        }

        /// <summary>
        /// This function retrieves the name of the line object from which a line element was created.
        /// It also retrieves the type of line object that it is.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="nameObject">The name of the line object from which the line element was created.</param>
        /// <param name="objectType">Type of object or defined item that is associated with the line element.</param>
        /// <param name="relativeDistanceI">The relative distance from the I-End of the object identified by the <paramref name="objectType" /> item to the I-End of the considered line element.
        /// The relative distance is calculated as the distance from the I-End of the object to the I-End of the line element divided by the length of the object.</param>
        /// <param name="relativeDistanceJ">The relative distance from the I-End of the object identified by the <paramref name="objectType" /> item to the J-End of the considered line element.
        /// The relative distance is calculated as the distance from the I-End of the object to the J-End of the line element divided by the length of the object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetObject(string name,
            out string nameObject,
            out eLineTypeObject objectType,
            out double relativeDistanceI,
            out double relativeDistanceJ)
        {
            nameObject = string.Empty;
            relativeDistanceI = 0;
            relativeDistanceJ = 0;
            int csiObjectType = 0;
            _callCode = _sapModel.LineElm.GetObj(name, ref nameObject, ref csiObjectType, ref relativeDistanceI, ref relativeDistanceJ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            objectType = (eLineTypeObject)csiObjectType;
        }
        #endregion

        #region Axes
        /// <summary>
        /// This function retrieves the local axis angle assignment for the line element.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLocalAxes(string name,
            out AngleLocalAxes angleOffset)
        {
            double angleA = 0;
            _callCode = _sapModel.LineElm.GetLocalAxes(name, ref angleA);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            angleOffset = new AngleLocalAxes {AngleA = angleA};
        }


        #endregion

        #region Modifiers
        /// <summary>
        /// This function retrieves the modifier assignment for line elements.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing line element or object.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetModifiers(string name,
            out FrameModifier modifiers)
        {
            double[] csiModifiers = new double[8];

            _callCode = _sapModel.LineElm.GetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            modifiers = new FrameModifier();
            modifiers.FromArray(csiModifiers);
        }


        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// This function retrieves the section property assigned to a line element.
        /// </summary>
        /// <param name="name">The name of a defined line element.</param>
        /// <param name="propertyName">The name of the frame, cable or tendon section property assigned to the line element.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSection(string name,
            out string propertyName)
        {
            propertyName = string.Empty;
            int csiObjectType = 0;
            bool isPrismatic = false;
            double nonPrismaticTotalLength = 0;
            double nonPrismaticRelativeStartLocation = 0;

            _callCode = _sapModel.LineElm.GetProperty(name, ref propertyName, ref csiObjectType, ref isPrismatic, ref nonPrismaticTotalLength, ref nonPrismaticRelativeStartLocation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the section property assigned to an line element, as well as the type and nonprismatic properties.
        /// </summary>
        /// <param name="name">The name of a defined line element.</param>
        /// <param name="propertyName">The name of the frame section, cable or tendon property assigned to the line element.</param>
        /// <param name="objectType">The type of object from which the line element was created.</param>
        /// <param name="isPrismatic">True: Specified property is a nonprismatic (variable) frame section property.</param>
        /// <param name="nonPrismaticTotalLength">Total assumed length of the nonprismatic section.
        /// A zero value for this item means that the section length is the same as the line element length.</param>
        /// <param name="nonPrismaticRelativeStartLocation">Relative distance along the nonprismatic section to the I-End (start) of the line element.
        /// This item is ignored when <paramref name="nonPrismaticTotalLength" /> is 0.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSection(string name,
            out string propertyName,
            out eLineTypeObject objectType,
            out bool isPrismatic,
            out double nonPrismaticTotalLength,
            out double nonPrismaticRelativeStartLocation)
        {
            int csiObjectType = 0;
            propertyName = string.Empty;
            objectType = 0;
            isPrismatic = false;
            nonPrismaticTotalLength = -1;
            nonPrismaticRelativeStartLocation = -1;

            _callCode = _sapModel.LineElm.GetProperty(name, 
                ref propertyName, 
                ref csiObjectType, 
                ref isPrismatic, 
                ref nonPrismaticTotalLength, 
                ref nonPrismaticRelativeStartLocation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            objectType = (eLineTypeObject)csiObjectType;
        }

        /// <summary>
        /// This function retrieves the material overwrite assigned to a line element, if any.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <param name="name">The name of a defined line element.</param>
        /// <param name="propertyName">This is None, indicating that no material overwrite exists for the specified line element, or it is the name of an existing material property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMaterialOverwrite(string name,
            ref string propertyName)
        {
            _callCode = _sapModel.LineElm.GetMaterialOverwrite(name, ref propertyName);
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
        /// If PatternName is the name of a defined joint pattern, the material temperature for the line element may vary from one end to the other.
        /// The material temperature at each end of the element is equal to the specified temperature multiplied by the pattern value at the joint at the end of the line element.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMaterialTemperature(string name,
            out double temperature,
            out string patternName)
        {
            temperature = 0;
            patternName = string.Empty;
            _callCode = _sapModel.LineElm.GetMatTemp(name, ref temperature, ref patternName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        /// <summary>
        /// This function retrieves line element insertion point assignments.
        /// The assignments are reported as end joint offsets.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="offsetDistancesI">Three joint offset distances, in the Global coordinate system, at the I-End of the line element. [L]</param>
        /// <param name="offsetDistancesJ">Three joint offset distances, in the Global coordinate system, at the J-End of the line element. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetInsertionPoint(string name,
            ref Displacements offsetDistancesI,
            ref Displacements offsetDistancesJ)
        {
            double[] csiOffsetDistancesI = new double[0];
            double[] csiOffsetDistancesJ = new double[0];

            _callCode = _sapModel.LineElm.GetInsertionPoint(name, ref csiOffsetDistancesI, ref csiOffsetDistancesJ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            offsetDistancesI.UX = csiOffsetDistancesI[0];
            offsetDistancesI.UY = csiOffsetDistancesI[1];
            offsetDistancesI.UZ = csiOffsetDistancesI[2];

            offsetDistancesJ.UX = csiOffsetDistancesJ[0];
            offsetDistancesJ.UY = csiOffsetDistancesJ[1];
            offsetDistancesJ.UZ = csiOffsetDistancesJ[2];
        }



        /// <summary>
        /// This function retrieves the tension/compression force limit assignments to line elements.
        /// Note that the tension and compression limits are only used in nonlinear analyses.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="limitCompressionExists">True: A compression force limit exists for the line element.</param>
        /// <param name="limitCompression">The compression force limit for the line element. [F]</param>
        /// <param name="limitTensionExists">True: A tension force limit exists for the line element.</param>
        /// <param name="limitTension">The tension force limit for the line element. [F]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTensionCompressionLimits(string name,
            ref bool limitCompressionExists,
            ref double limitCompression,
            ref bool limitTensionExists,
            ref double limitTension)
        {
            _callCode = _sapModel.LineElm.GetTCLimits(name, ref limitCompressionExists, ref limitCompression, ref limitTensionExists, ref limitTension);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Frame Properties
        /// <summary>
        /// This function retrieves the line element end offsets along the 1-axis of the element.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="lengthIEnd">The offset length along the 1-axis of the line element at the I-End of the line element. [L]</param>
        /// <param name="lengthJEnd">The offset along the 1-axis of the line element at the J-End of the line element. [L]</param>
        /// <param name="rigidZoneFactor">The rigid zone factor.
        /// This is the fraction of the end offset length assumed to be rigid for bending and shear deformations.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetEndLengthOffset(string name,
            ref double lengthIEnd,
            ref double lengthJEnd,
            ref double rigidZoneFactor)
        {
            _callCode = _sapModel.LineElm.GetEndLengthOffset(name, ref lengthIEnd, ref lengthJEnd, ref rigidZoneFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        #endregion

        #region Support & Connections
        /// <summary>
        /// This function retrieves the release assignments for a frame end release.
        /// </summary>
        /// <param name="name">The name of an existing frame end release.</param>
        /// <param name="iEndRelease">Booleans indicating the I-End releases.</param>
        /// <param name="jEndRelease">Booleans indicating the J-End releases.</param>
        /// <param name="iEndFixity">Values indicating the I-End partial fixity springs.</param>
        /// <param name="jEndFixity">Values indicating the J-End partial fixity springs.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReleases(string name,
            out DegreesOfFreedomLocal iEndRelease,
            out DegreesOfFreedomLocal jEndRelease,
            out Fixity iEndFixity,
            out Fixity jEndFixity)
        {
            bool[] csiiEndRelease = new bool[6];
            bool[] csijEndRelease = new bool[6];
            double[] csiiEndFixity = new double[6];
            double[] csijEndFixity = new double[6];

            _callCode = _sapModel.LineElm.GetReleases(name, ref csiiEndRelease, ref csijEndRelease, ref csiiEndFixity, ref csijEndFixity);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            iEndRelease = new DegreesOfFreedomLocal();
            iEndRelease.FromArray(csiiEndRelease);

            jEndRelease = new DegreesOfFreedomLocal();
            jEndRelease.FromArray(csijEndRelease);

            iEndFixity = new Fixity();
            iEndFixity.FromArray(csiiEndFixity);

            jEndFixity = new Fixity();
            jEndFixity.FromArray(csijEndFixity);
        }

        #endregion

        #region Loads
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the deformation load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of deformation loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each deformation load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each deformation load.</param>
        /// <param name="degreesOfFreedom">Indicates if the considered degree of freedom has a deformation load for each load pattern.</param>
        /// <param name="deformations">Deformation load values for each load pattern.
        /// The deformations specified for a given degree of freedom are applicable only if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadDeformation(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref DegreesOfFreedomLocal[] degreesOfFreedom,
            ref Deformations[] deformations,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            bool[] dof1 = new bool[0];
            bool[] dof2 = new bool[0];
            bool[] dof3 = new bool[0];
            bool[] dof4 = new bool[0];
            bool[] dof5 = new bool[0];
            bool[] dof6 = new bool[0];

            double[] u1Deformation = new double[0];
            double[] u2Deformation = new double[0];
            double[] u3Deformation = new double[0];
            double[] r1Deformation = new double[0];
            double[] r2Deformation = new double[0];
            double[] r3Deformation = new double[0];

            _callCode = _sapModel.LineElm.GetLoadDeformation(name, ref numberItems, ref names, ref loadPatterns, 
                ref dof1, ref dof2, ref dof3, ref dof4, ref dof5, ref dof6, 
                ref u1Deformation, ref u2Deformation, ref u3Deformation, ref r1Deformation, ref r2Deformation, ref r3Deformation, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom = new DegreesOfFreedomLocal[numberItems - 1];
            deformations = new Deformations[numberItems - 1];
            for (int i = 0; i < numberItems; i++)
            {
                degreesOfFreedom[i].U1 = dof1[i];
                degreesOfFreedom[i].U2 = dof2[i];
                degreesOfFreedom[i].U3 = dof3[i];
                degreesOfFreedom[i].R1 = dof4[i];
                degreesOfFreedom[i].R2 = dof5[i];
                degreesOfFreedom[i].R3 = dof6[i];

                deformations[i].U1 = u1Deformation[i];
                deformations[i].U2 = u2Deformation[i];
                deformations[i].U3 = u3Deformation[i];
                deformations[i].R1 = r1Deformation[i];
                deformations[i].R2 = r2Deformation[i];
                deformations[i].R3 = r3Deformation[i];
            }
        }

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
            _callCode = _sapModel.LineElm.GetLoadGravity(name, ref numberItems, ref names, ref loadPatterns, 
                ref coordinateSystems, ref xLoadMultiplier, ref yLoadMultiplier, ref zLoadMultiplier, 
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

            _callCode = _sapModel.LineElm.GetLoadStrain(name, ref numberItems, ref names, ref loadPatterns, 
                ref csiComponent, ref strainLoadValues, ref jointPatternNames, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            component = csiComponent.Cast<eStrainComponent>().ToArray();
        }

        /// <summary>
        /// This function retrieves the target force assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of deformation loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each target force.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each target force.</param>
        /// <param name="forcesActive">Boolean values indicating if the considered degree of freedom has a target force assignment for each load pattern.</param>
        /// <param name="deformations">Target force values for each load pattern.
        /// The target forces specified for a given degree of freedom are only applicable if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="relativeForcesLocation">Relative distances along the line elements where the target force values apply for each load pattern.
        /// The relative distances specified for a given degree of freedom are only applicable if the corresponding dofn item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadTargetForce(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref ForcesActive[] forcesActive,
            ref Deformations[] deformations,
            ref Forces[] relativeForcesLocation,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            bool[] dof1 = new bool[0];
            bool[] dof2 = new bool[0];
            bool[] dof3 = new bool[0];
            bool[] dof4 = new bool[0];
            bool[] dof5 = new bool[0];
            bool[] dof6 = new bool[0];

            double[] u1Deformation = new double[0];
            double[] u2Deformation = new double[0];
            double[] u3Deformation = new double[0];
            double[] r1Deformation = new double[0];
            double[] r2Deformation = new double[0];
            double[] r3Deformation = new double[0];

            double[] T1 = new double[0];
            double[] T2 = new double[0];
            double[] T3 = new double[0];
            double[] T4 = new double[0];
            double[] T5 = new double[0];
            double[] T6 = new double[0];

            _callCode = _sapModel.LineElm.GetLoadTargetForce(name, ref numberItems, ref names, ref loadPatterns, 
                ref dof1, ref dof2, ref dof3, ref dof4, ref dof5, ref dof6, 
                ref u1Deformation, ref u2Deformation, ref u3Deformation, ref r1Deformation, ref r2Deformation, ref r3Deformation, 
                ref T1, ref T2, ref T3, ref T4, ref T5, ref T6, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forcesActive = new ForcesActive[numberItems - 1];
            deformations = new Deformations[numberItems - 1];
            relativeForcesLocation = new Forces[numberItems - 1];
            for (int i = 0; i < numberItems; i++)
            {
                forcesActive[i].P = dof1[i];
                forcesActive[i].V2 = dof2[i];
                forcesActive[i].V3 = dof3[i];
                forcesActive[i].T = dof4[i];
                forcesActive[i].M2 = dof5[i];
                forcesActive[i].M3 = dof6[i];

                deformations[i].U1 = u1Deformation[i];
                deformations[i].U2 = u2Deformation[i];
                deformations[i].U3 = u3Deformation[i];
                deformations[i].R1 = r1Deformation[i];
                deformations[i].R2 = r2Deformation[i];
                deformations[i].R3 = r3Deformation[i];

                relativeForcesLocation[i].P = T1[i];
                relativeForcesLocation[i].V2 = T2[i];
                relativeForcesLocation[i].V3 = T3[i];
                relativeForcesLocation[i].T = T4[i];
                relativeForcesLocation[i].M2 = T5[i];
                relativeForcesLocation[i].M3 = T6[i];
            }
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

            _callCode = _sapModel.LineElm.GetLoadTemperature(name, ref numberItems, ref names, ref loadPatterns, 
                ref csiTemperatureLoadType, ref temperatureLoadValues, ref jointPatternNames, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            temperatureLoadType = csiTemperatureLoadType.Cast<eLoadTemperatureType>().ToArray();
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the P-Delta force assignments to line elements.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="numberForces">The number of P-Delta forces assigned to the line element.</param>
        /// <param name="pDeltaForces">P-Delta force values assigned to the line element. [F]</param>
        /// <param name="directions">The direction of each P-Delta force assignment.</param>
        /// <param name="coordinateSystems">This is an array that contains the name of the coordinate system in which each projected P-Delta force is defined.
        /// This item is blank when the <paramref name="directions" /> item is <see cref="ePDeltaDirection.Local_1" />, that is, when the P-Delta force is defined in the line element local 1-axis direction.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPDeltaForce(string name,
            ref int numberForces,
            ref double[] pDeltaForces,
            ref ePDeltaDirection[] directions,
            ref string[] coordinateSystems)
        {
            int[] csiDirections = new int[0];

            _callCode = _sapModel.LineElm.GetPDeltaForce(name, ref numberForces, ref pDeltaForces, ref csiDirections, ref coordinateSystems);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            directions = csiDirections.Cast<ePDeltaDirection>().ToArray();
        }
#endif
        /// <summary>
        /// This function retrieves the distributed load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of distributed loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each distributed load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each distributed load.</param>
        /// <param name="forceTypes">Force type for the distributed load for each load pattern.</param>
        /// <param name="coordinateSystems">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="relativeDistanceStartFromI">The relative distance from the I-End of the element to the start of the distributed load.</param>
        /// <param name="relativeDistanceEndFromI">The relative distance from the I-End of the element to the end of the distributed load.</param>
        /// <param name="absoluteDistanceStartFromI">The actual distance from the I-End of the element to the start of the distributed load. [L]</param>
        /// <param name="absoluteDistanceEndFromI">The actual distance from the I-End of the element to the end of the distributed load. [L]</param>
        /// <param name="startLoadValues">The load value at the start of the distributed load.
        /// [F/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="endLoadValues">The load value at the end of the distributed load.
        /// [F/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadDistributed(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref string[] coordinateSystems,
            ref eLoadDirection[] loadDirections,
            ref double[] relativeDistanceStartFromI,
            ref double[] relativeDistanceEndFromI,
            ref double[] absoluteDistanceStartFromI,
            ref double[] absoluteDistanceEndFromI,
            ref double[] startLoadValues,
            ref double[] endLoadValues,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            int[] csiForceTypes = new int[0];
            int[] csiLoadDirections = new int[0];

            _callCode = _sapModel.LineElm.GetLoadDistributed(name, ref numberItems, ref names, ref loadPatterns, 
                ref csiForceTypes, ref coordinateSystems, 
                ref csiLoadDirections, ref relativeDistanceStartFromI, ref relativeDistanceEndFromI, 
                ref absoluteDistanceStartFromI, ref absoluteDistanceEndFromI, ref startLoadValues, ref endLoadValues, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forceTypes = csiForceTypes.Cast<eLoadForceType>().ToArray();
            loadDirections = csiLoadDirections.Cast<eLoadDirection>().ToArray();
        }

        /// <summary>
        /// This function retrieves the distributed load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of point loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each point load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each point load.</param>
        /// <param name="forceTypes">Force type for the point load for each load pattern.</param>
        /// <param name="coordinateSystems">Coordinated system used for each point load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="relativeDistanceFromI">The relative distance from the I-End of the element to the location of the point load.</param>
        /// <param name="absoluteDistanceFromI">The actual distance from the I-End of the element to the location of the point load. [L]</param>
        /// <param name="pointLoadValues">The load value of the point loads.
        /// [F] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadPoint(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref string[] coordinateSystems,
            ref eLoadDirection[] loadDirections,
            ref double[] relativeDistanceFromI,
            ref double[] absoluteDistanceFromI,
            ref double[] pointLoadValues,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            int[] csiForceTypes = new int[0];
            int[] csiLoadDirections = new int[0];

            _callCode = _sapModel.LineElm.GetLoadPoint(name, ref numberItems, ref names, ref loadPatterns, 
                ref csiForceTypes, ref coordinateSystems, ref csiLoadDirections, ref relativeDistanceFromI, 
                ref absoluteDistanceFromI, ref pointLoadValues, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            forceTypes = csiForceTypes.Cast<eLoadForceType>().ToArray();
            loadDirections = csiLoadDirections.Cast<eLoadDirection>().ToArray();
        }
        #endregion
    }
}
