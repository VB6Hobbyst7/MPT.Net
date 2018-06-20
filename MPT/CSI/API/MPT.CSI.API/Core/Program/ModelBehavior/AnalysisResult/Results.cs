// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-08-2017
//
// Last Modified By : Mark
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="Results.cs" company="">
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

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult
{
    /// <summary>
    /// Represents the analysis results in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult.IResults" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Results : CSiApiBase, IResults
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Results" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Results(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Area        
        /// <summary>
        /// This function reports the area forces for the specified area elements that are assigned shell section properties (not plane or asolid properties).
        /// Note that the forces reported are per unit of in-plane length.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="F11">The area element internal F11 membrane direct force per length reported in the area element local coordinate system. [F/L].</param>
        /// <param name="F22">The area element internal F22 membrane direct force per length reported in the area element local coordinate system. [F/L].</param>
        /// <param name="F12">The area element internal F12 membrane shear force per length reported in the area element local coordinate system. [F/L].</param>
        /// <param name="FMax">The maximum principal membrane force per length. [F/L].</param>
        /// <param name="FMin">The minimum principal membrane force per length. [F/L].</param>
        /// <param name="FAngle">The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of the maximum principal membrane force. [deg].</param>
        /// <param name="FVM">The area element internal Von Mises membrane force per length. [F/L].</param>
        /// <param name="M11">The area element internal M11 plate bending moment per length reported in the area element local coordinate system.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F*L/L].</param>
        /// <param name="M22">The area element internal M22 plate bending moment per length reported in the area element local coordinate system.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F*L/L].</param>
        /// <param name="M12">The area element internal M12 plate twisting moment per length reported in the area element local coordinate system.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F*L/L].</param>
        /// <param name="MMax">The maximum principal plate moment per length.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F*L/L].</param>
        /// <param name="MMin">The minimum principal plate moment per length.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F*L/L].</param>
        /// <param name="MAngle">The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of the maximum principal plate moment.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [deg].</param>
        /// <param name="V13">The area element internal V13 plate transverse shear force per length reported in the area element local coordinate system.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F/L].</param>
        /// <param name="V23">The area element internal V23 plate transverse shear force per length reported in the area element local coordinate system.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F/L].</param>
        /// <param name="VMax">The maximum plate transverse shear force.  It is equal to the square root of the sum of the squares of V13 and V23.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F/L].</param>
        /// <param name="VAngle">The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of Vmax.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [deg].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void AreaForceShell(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] F11,
            ref double[] F22,
            ref double[] F12,
            ref double[] FMax,
            ref double[] FMin,
            ref double[] FAngle,
            ref double[] FVM,
            ref double[] M11,
            ref double[] M22,
            ref double[] M12,
            ref double[] MMax,
            ref double[] MMin,
            ref double[] MAngle,
            ref double[] V13,
            ref double[] V23,
            ref double[] VMax,
            ref double[] VAngle)
        {
            _callCode = _sapModel.Results.AreaForceShell(name, 
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType), 
                            ref _numberOfItems, 
                            ref objectNames, 
                            ref elementNames, 
                            ref pointNames, 
                            ref loadCases, 
                            ref stepTypes, 
                            ref stepNumbers, 
                            ref F11, ref F22, ref F12, ref FMax, ref FMin, ref FAngle, ref FVM, 
                            ref M11, ref M22, ref M12, ref MMax, ref MMin, ref MAngle, 
                            ref V13, ref V23, ref VMax, ref VAngle);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function reports the area joint forces for the point elements at each corner of the specified area elements that have shell-type properties (not plane or asolid)..
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="jointForces">The joint force components along/about the point element local axes directions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void AreaJointForceShell(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.Results.AreaJointForceShell(name, 
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType), 
                            ref _numberOfItems, 
                            ref objectNames, 
                            ref elementNames, 
                            ref pointNames, 
                            ref loadCases, 
                            ref stepTypes, 
                            ref stepNumbers, 
                            ref F1, ref F2, ref F3, 
                            ref M1, ref M2, ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            jointForces = new Loads[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                jointForces[i].F1 = F1[i];
                jointForces[i].F2 = F2[i];
                jointForces[i].F3 = F3[i];
                jointForces[i].M1 = M1[i];
                jointForces[i].M2 = M2[i];
                jointForces[i].M3 = M3[i];
            }
        }

        /// <summary>
        /// This function reports the area stresses for the specified area elements that are assigned shell section properties (not plane or asolid properties).
        /// Stresses are reported at each point element associated with the area element.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="stressesTop">The area element internal stresses, at the top of the area element, at the specified point element location. [F/L^2].</param>
        /// <param name="stressesBottom">The area element internal stresses, at the bottom of the area element, at the specified point element location. [F/L^2].</param>
        /// <param name="S13Avg">The area element average S13 out-of-plane shear stress at the specified point element location, for the specified layer and layer integration point. [F/L^2].</param>
        /// <param name="S23Avg">The area element average S13 out-of-plane shear stress at the specified point element location, for the specified layer and layer integration point. [F/L^2].</param>
        /// <param name="SMaxAvg">The area element maximum average out-of-plane shear stress for the specified layer and layer integration point.
        /// It is equal to the square root of the sum of the squares of <paramref name="S13Avg" /> and <paramref name="S23Avg" /> [F/L^2].</param>
        /// <param name="SAngleAvg">The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of <paramref name="SMaxAvg" />. [deg].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void AreaStressShell(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Stress[] stressesTop,
            ref Stress[] stressesBottom,
            ref double[] S13Avg,
            ref double[] S23Avg,
            ref double[] SMaxAvg,
            ref double[] SAngleAvg)
        {
            double[] S11Top = new double[0];
            double[] S22Top = new double[0];
            double[] S12Top = new double[0];
            double[] SMaxTop = new double[0];
            double[] SMinTop = new double[0];
            double[] SVMTop = new double[0];
            double[] SAngleTop = new double[0];

            double[] S11Bottom = new double[0];
            double[] S22Bottom = new double[0];
            double[] S12Bottom = new double[0];
            double[] SMaxBottom = new double[0];
            double[] SMinBottom = new double[0];
            double[] SVMBottom = new double[0];
            double[] SAngleBottom = new double[0];

            _callCode = _sapModel.Results.AreaStressShell(name,
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                            ref _numberOfItems,
                            ref objectNames,
                            ref elementNames,
                            ref pointNames,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref S11Top,
                            ref S22Top,
                            ref S12Top,
                            ref SMaxTop,
                            ref SMinTop,
                            ref SAngleTop,
                            ref SVMTop,
                            ref S11Bottom,
                            ref S22Bottom,
                            ref S12Bottom,
                            ref SMaxBottom,
                            ref SMinBottom,
                            ref SAngleBottom,
                            ref SVMBottom,
                            ref S13Avg,
                            ref S23Avg,
                            ref SMaxAvg,
                            ref SAngleAvg);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stressesTop = new Stress[_numberOfItems - 1];
            stressesBottom = new Stress[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                stressesTop[i].S11 = S11Top[i];
                stressesTop[i].S22 = S22Top[i];
                stressesTop[i].S12 = S12Top[i];
                stressesTop[i].SMax = SMaxTop[i];
                stressesTop[i].SMin = SMinTop[i];
                stressesTop[i].SVM = SVMTop[i];
                stressesTop[i].Angle = SAngleTop[i];

                stressesBottom[i].S11 = S11Bottom[i];
                stressesBottom[i].S22 = S22Bottom[i];
                stressesBottom[i].S12 = S12Bottom[i];
                stressesBottom[i].SMax = SMaxBottom[i];
                stressesBottom[i].SMin = SMinBottom[i];
                stressesBottom[i].SVM = SVMBottom[i];
                stressesBottom[i].Angle = SAngleBottom[i];
            }
        }

        /// <summary>
        /// This function reports the area stresses for the specified area elements that are assigned layered shell section properties.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="stresses">The area element internal stresses at the specified point element location. [F/L^2].</param>
        /// <param name="S13Avg">The area element average S13 out-of-plane shear stress at the specified point element location, for the specified layer and layer integration point. [F/L^2].</param>
        /// <param name="S23Avg">The area element average S13 out-of-plane shear stress at the specified point element location, for the specified layer and layer integration point. [F/L^2].</param>
        /// <param name="SMaxAvg">The area element maximum average out-of-plane shear stress for the specified layer and layer integration point.
        /// It is equal to the square root of the sum of the squares of <paramref name="S13Avg" /> and <paramref name="S23Avg" /> [F/L^2].</param>
        /// <param name="SAngleAvg">The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of <paramref name="SMaxAvg" />. [deg].</param>
        /// <param name="layers">The layer name associated with each result.</param>
        /// <param name="integrationPointNumbers">The integration point number within the specified layer of the area element associated with each result.</param>
        /// <param name="integrationPointLocations">The integration point relative location within the specified layer of the area element associated with each result.
        /// The location is between -1 (bottom of layer) and +1 (top of layer), inclusive.
        /// The midheight of the layer is at a value of 0.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void AreaStressShellLayered(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Stress[] stresses,
            ref double[] S13Avg,
            ref double[] S23Avg,
            ref double[] SMaxAvg,
            ref double[] SAngleAvg,
            ref string[] layers,
            ref int[] integrationPointNumbers,
            ref double[] integrationPointLocations)
        {
            double[] S11 = new double[0];
            double[] S22 = new double[0];
            double[] S12 = new double[0];
            double[] SMax = new double[0];
            double[] SMin = new double[0];
            double[] SVM = new double[0];
            double[] SAngle = new double[0];

            _callCode = _sapModel.Results.AreaStressShellLayered(name, 
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType), 
                            ref _numberOfItems, 
                            ref objectNames, 
                            ref elementNames, 
                            ref layers, 
                            ref integrationPointNumbers, 
                            ref integrationPointLocations, 
                            ref pointNames, 
                            ref loadCases, 
                            ref stepTypes, 
                            ref stepNumbers, 
                            ref S11, 
                            ref S22, 
                            ref S12, 
                            ref SMax, 
                            ref SMin, 
                            ref SAngle, 
                            ref SVM, 
                            ref S13Avg, 
                            ref S23Avg, 
                            ref SMaxAvg,
                            ref SAngleAvg);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stresses = new Stress[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                stresses[i].S11 = S11[i];
                stresses[i].S22 = S22[i];
                stresses[i].S12 = S12[i];
                stresses[i].SMax = SMax[i];
                stresses[i].SMin = SMin[i];
                stresses[i].SVM = SVM[i];
                stresses[i].Angle = SAngle[i];
            }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function reports the area joint forces for the point elements at each corner of the specified plane elements that have plane-type or asolid-type properties (not shell).
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="jointForces">The joint force components along/about the point element local axes directions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void AreaJointForcePlane(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.Results.AreaJointForcePlane(name, 
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType), 
                            ref _numberOfItems, 
                            ref objectNames, 
                            ref elementNames, 
                            ref pointNames, 
                            ref loadCases, 
                            ref stepTypes, 
                            ref stepNumbers, 
                            ref F1, ref F2, ref F3, 
                            ref M1, ref M2, ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            jointForces = new Loads[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                jointForces[i].F1 = F1[i];
                jointForces[i].F2 = F2[i];
                jointForces[i].F3 = F3[i];
                jointForces[i].M1 = M1[i];
                jointForces[i].M2 = M2[i];
                jointForces[i].M3 = M3[i];
            }
        }

        /// <summary>
        /// This function reports the stresses for the specified plane elements that are assigned plane or asolid section properties (not shell properties).
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="stresses">The area element internal stresses at the specified point element location. [F/L^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void AreaStressPlane(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Stress[] stresses)
        {
            double[] S11 = new double[0];
            double[] S22 = new double[0];
            double[] S33 = new double[0];
            double[] S12 = new double[0];
            double[] SMax = new double[0];
            double[] SMin = new double[0];
            double[] SVM = new double[0];
            double[] SAngle = new double[0];

            _callCode = _sapModel.Results.AreaStressPlane(name, 
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType), 
                            ref _numberOfItems, 
                            ref objectNames, 
                            ref elementNames, 
                            ref pointNames, 
                            ref loadCases, 
                            ref stepTypes, 
                            ref stepNumbers, 
                            ref S11, 
                            ref S22, 
                            ref S33, 
                            ref S12, 
                            ref SMax, 
                            ref SMin, 
                            ref SAngle, 
                            ref SVM);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stresses = new Stress[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                stresses[i].S11 = S11[i];
                stresses[i].S22 = S22[i];
                stresses[i].S33 = S33[i];
                stresses[i].S12 = S12[i];
                stresses[i].SMax = SMax[i];
                stresses[i].SMin = SMin[i];
                stresses[i].SVM = SVM[i];
                stresses[i].Angle = SAngle[i];
            }
        }
#endif

        #endregion


        #region Methods: Mass    
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function reports the assembled joint masses for the specified point elements.
        /// </summary>
        /// <param name="massSourceName">The name of an existing mass source definition.
        /// If this value is left empty or unrecognized, data for all mass sources will be returned.</param>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="pointElementNames">The point element name associated with each result.</param>
        /// <param name="massSourceNames">The mass source name associated with each result.</param>
        /// <param name="masses">The mass along/about the point element local 1, 2 and 3 axes directions, for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void AssembledJointMass(string massSourceName,
            string name,
            eItemTypeElement itemType,
            ref string[] pointElementNames,
            ref string[] massSourceNames,
            ref Mass[] masses)
#else
        /// <summary>
        /// This function reports the assembled joint masses for the specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement"/>, the result request is for the elements corresponding to the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemTypeElement.Element"/>, the result request is for the element specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement"/>, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement"/>, the result request is for elements corresponding to all selected objects, and the <paramref name="name"/> item is ignored.</param>
        /// <param name="pointElementNames">The point element name associated with each result.</param>
        /// <param name="masses">The mass along/about the point element local 1, 2 and 3 axes directions, for each result.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void AssembledJointMass(string name,
            eItemTypeElement itemType,
            ref string[] pointElementNames,
            ref Mass[] masses)
#endif
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];
#if BUILD_SAP2000v19 || BUILD_SAP2000v20 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
            _callCode = _sapModel.Results.AssembledJointMass_1(massSourceName,
                                    name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref pointElementNames,
                                    ref massSourceNames,
                                    ref U1,
                                    ref U2,
                                    ref U3,
                                    ref R1,
                                    ref R2,
                                    ref R3);
#else
            _callCode = _sapModel.Results.AssembledJointMass(
                             name,
                             EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                             ref _numberOfItems,
                             ref pointElementNames,
                             ref U1,
                             ref U2,
                             ref U3,
                             ref R1,
                             ref R2,
                             ref R3);
#endif
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            masses = new Mass[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                masses[i].U1 = U1[i];
                masses[i].U2 = U2[i];
                masses[i].U3 = U3[i];
                masses[i].R1 = R1[i];
                masses[i].R2 = R2[i];
                masses[i].R3 = R3[i];
            }
        }
        #endregion

        #region Methods: Base Reaction        
        /// <summary>
        /// This function reports the structure total base reactions.
        /// </summary>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="reactions">The base reaction force and moments in/about the global X, Y and Z directions, for each result.</param>
        /// <param name="baseReactionCoordinate">These are the global X, Y and Z coordinates of the point at which the base reactions are reported. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void BaseReaction(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Reactions[] reactions,
            ref Coordinate3DCartesian baseReactionCoordinate)
        {
            double[] Fx = new double[0];
            double[] Fy = new double[0];
            double[] Fz = new double[0];
            double[] Mx = new double[0];
            double[] My = new double[0];
            double[] Mz = new double[0];

            double gx = 0;
            double gy = 0;
            double gz = 0;
            
            _callCode = _sapModel.Results.BaseReact(ref _numberOfItems,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref Fx, ref Fy, ref Fz, 
                            ref Mx, ref My, ref Mz, 
                            ref gx, ref gy, ref gz);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            reactions = new Reactions[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                reactions[i].Fx = Fx[i];
                reactions[i].Fy = Fy[i];
                reactions[i].Fz = Fz[i];
                reactions[i].Mx = Mx[i];
                reactions[i].My = My[i];
                reactions[i].Mz = Mz[i];
            }
            
            baseReactionCoordinate.X = gx;
            baseReactionCoordinate.Y = gy;
            baseReactionCoordinate.Z = gz;
        }

        /// <summary>
        /// This function reports the structure total base reactions and includes information on the centroid of the translational reaction forces.
        /// Note that the reported base reaction centroids are not the same as the centroid of the applied loads
        /// </summary>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="reactions">The base reaction force and moments in/about the global X, Y and Z directions, for each result.</param>
        /// <param name="baseReactionCoordinate">These are the global X, Y and Z coordinates of the point at which the base reactions are reported. [L].</param>
        /// <param name="centroidFxCoordinates">These are arrays of the global X, Y and Z coordinates, respectively, of the centroid of all global X-direction translational reaction forces for each result. [L].</param>
        /// <param name="centroidFyCoordinates">These are arrays of the global X, Y and Z coordinates, respectively, of the centroid of all global Y-direction translational reaction forces for each result. [L].</param>
        /// <param name="centroidFzCoordinates">These are arrays of the global X, Y and Z coordinates, respectively, of the centroid of all global Z-direction translational reaction forces for each result. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void BaseReactionWithCentroid(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Reactions[] reactions,
            ref Coordinate3DCartesian baseReactionCoordinate,
            ref Coordinate3DCartesian[] centroidFxCoordinates,
            ref Coordinate3DCartesian[] centroidFyCoordinates,
            ref Coordinate3DCartesian[] centroidFzCoordinates)
        {
            double[] Fx = new double[0];
            double[] Fy = new double[0];
            double[] Fz = new double[0];
            double[] Mx = new double[0];
            double[] My = new double[0];
            double[] Mz = new double[0];

            double gx = 0;
            double gy = 0;
            double gz = 0;

            double[] centroidXFx = new double[0];
            double[] centroidYFx = new double[0];
            double[] centroidZFx = new double[0];

            double[] centroidXFy = new double[0];
            double[] centroidYFy = new double[0];
            double[] centroidZFy = new double[0];

            double[] centroidXFz = new double[0];
            double[] centroidYFz = new double[0];
            double[] centroidZFz = new double[0];

            _callCode = _sapModel.Results.BaseReactWithCentroid(ref _numberOfItems,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref Fx, ref Fy, ref Fz, 
                            ref Mx, ref My, ref Mz, 
                            ref gx, ref gy, ref gz,
                            ref centroidXFx, ref centroidYFx, ref centroidZFx,
                            ref centroidXFy, ref centroidYFy, ref centroidZFy,
                            ref centroidXFz, ref centroidYFz, ref centroidZFz);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            reactions = new Reactions[_numberOfItems - 1];
            centroidFxCoordinates = new Coordinate3DCartesian[_numberOfItems - 1];
            centroidFyCoordinates = new Coordinate3DCartesian[_numberOfItems - 1];
            centroidFzCoordinates = new Coordinate3DCartesian[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                reactions[i].Fx = Fx[i];
                reactions[i].Fy = Fy[i];
                reactions[i].Fz = Fz[i];
                reactions[i].Mx = Mx[i];
                reactions[i].My = My[i];
                reactions[i].Mz = Mz[i];

                centroidFxCoordinates[i].X = centroidXFx[i];
                centroidFxCoordinates[i].Y = centroidYFx[i];
                centroidFxCoordinates[i].Z = centroidZFx[i];

                centroidFyCoordinates[i].X = centroidXFy[i];
                centroidFyCoordinates[i].Y = centroidYFy[i];
                centroidFyCoordinates[i].Z = centroidZFy[i];

                centroidFzCoordinates[i].X = centroidXFz[i];
                centroidFzCoordinates[i].Y = centroidYFz[i];
                centroidFzCoordinates[i].Z = centroidZFz[i];
            }
            baseReactionCoordinate.X = gx;
            baseReactionCoordinate.Y = gy;
            baseReactionCoordinate.Z = gz;
        }
        #endregion

        #region Methods: Frame        
        /// <summary>
        /// This function reports the frame forces for the specified line elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="forces">The internal forces for each result.</param>
        /// <param name="objectStations">The distance measured from the I-end of the line object to the result location for each result.</param>
        /// <param name="elementStations">The distance measured from the I-end of the line element to the result location for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FrameForce(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Forces[] forces,
            ref double[] objectStations,
            ref double[] elementStations)
        {
            double[] P = new double[0];
            double[] V2 = new double[0];
            double[] V3 = new double[0];
            double[] T = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.Results.FrameForce(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref objectNames,
                                    ref objectStations,
                                    ref elementNames,
                                    ref elementStations,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref P,
                                    ref V2,
                                    ref V3,
                                    ref T,
                                    ref M2,
                                    ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forces = new Forces[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                forces[i].P = P[i];
                forces[i].V2 = V2[i];
                forces[i].V3 = V3[i];
                forces[i].T = T[i];
                forces[i].M2 = M2[i];
                forces[i].M3 = M3[i];
            }
        }


        /// <summary>
        /// This function reports the frame joint forces for the point elements at each end of the specified line elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="jointForces">The joint force components along/about the point element local axes directions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FrameJointForce(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.Results.FrameJointForce(name,
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                            ref _numberOfItems,
                            ref objectNames,
                            ref elementNames,
                            ref pointNames,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref F1, ref F2, ref F3,
                            ref M1, ref M2, ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            jointForces = new Loads[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                jointForces[i].F1 = F1[i];
                jointForces[i].F2 = F2[i];
                jointForces[i].F3 = F3[i];
                jointForces[i].M1 = M1[i];
                jointForces[i].M2 = M2[i];
                jointForces[i].M3 = M3[i];
            }
        }
        #endregion

        #region Methods: Joint        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
        /// <summary>
        /// This function reports the joint response spectra values, due to a time history analysis, for the specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="namedSet">The name of an existing joint response spectrum named set.
        /// See <see cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.NamedSets.SetJointRespSpec" />.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="coordinateSystems">The coordinate systems in which the results are reported.</param>
        /// <param name="directions">The directions for which the results are reported.</param>
        /// <param name="damping">The critical damping ratio, for each result.</param>
        /// <param name="percentSpectrumWidening">The percent spectrum widening, for each result.</param>
        /// <param name="abscissaValues">The period or frequency, as defined in each named set, for each result. [s or 1/s].</param>
        /// <param name="ordinateValues">The response quantity, as defined in each named set, for each result.
        /// The possible response quantities are spectral displacement [L], spectral velocity [L/s], pseudo spectral velocity [L/s], spectral acceleration [L/s2], or pseudo spectral acceleration [L/s2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void JointResponseSpectrum(string name,
            eItemTypeElement itemType,
            string namedSet,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] coordinateSystems,
            ref eDirection[] directions,
            ref double[] damping,
            ref double[] percentSpectrumWidening,
            ref double[] abscissaValues,
            ref double[] ordinateValues)
        {
            int[] csiDirections = new int[0];

            _callCode = _sapModel.Results.JointRespSpec(name,
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                            namedSet,
                            ref _numberOfItems,
                            ref objectNames,
                            ref elementNames,
                            ref loadCases,
                            ref coordinateSystems,
                            ref csiDirections,
                            ref damping,
                            ref percentSpectrumWidening,
                            ref abscissaValues,
                            ref ordinateValues);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            directions = csiDirections.Cast<eDirection>().ToArray();
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017        
        /// <summary>
        /// Reports the joint drifts.
        /// </summary>
        /// <param name="storyNames">Story names associated with each result.</param>
        /// <param name="labels">The point labels for each result.</param>
        /// <param name="names">The unique point names for each result.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step types, if any, for each result.</param>
        /// <param name="stepNumbers">The step numbers, if any, for each result.</param>
        /// <param name="displacementsX">The displacements in the x-direction [L].</param>
        /// <param name="displacementsY">The displacements in the y-direction [L].</param>
        /// <param name="driftsX">The drifts x in the x-direction.</param>
        /// <param name="driftsY">The drifts y in the x-direction.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void JointDrifts(ref string[] storyNames,
            ref string[] labels,
            ref string[] names,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] displacementsX,
            ref double[] displacementsY,
            ref double[] driftsX,
            ref double[] driftsY)
        {
            _callCode = _sapModel.Results.JointDrifts(
                            ref _numberOfItems,
                            ref storyNames,
                            ref labels,
                            ref names,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref displacementsX,
                            ref displacementsY,
                            ref driftsX,
                            ref driftsY);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        /// <summary>
        /// This function reports the joint accelerations for the specified point elements.
        /// The accelerations reported by this function are relative accelerations.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="accelerations">The accelerations along/about the point element local 1, 2 and 3 axes directions, respectively, for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void JointAcceleration(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] accelerations)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.Results.JointAcc(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref objectNames,
                                    ref elementNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref U1,
                                    ref U2,
                                    ref U3,
                                    ref R1,
                                    ref R2,
                                    ref R3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            accelerations = new Deformations[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                accelerations[i].U1 = U1[i];
                accelerations[i].U2 = U2[i];
                accelerations[i].U3 = U3[i];
                accelerations[i].R1 = R1[i];
                accelerations[i].R2 = R2[i];
                accelerations[i].R3 = R3[i];
            }
        }


        /// <summary>
        /// This function reports the joint absolute accelerations for the specified point elements.
        /// Absolute and relative accelerations are the same, except when reported for time history load cases subjected to acceleration loading.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="accelerations">The accelerations along/about the point element local 1, 2 and 3 axes directions, respectively, for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void JointAccelerationAbsolute(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] accelerations)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.Results.JointAccAbs(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref objectNames,
                                    ref elementNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref U1,
                                    ref U2,
                                    ref U3,
                                    ref R1,
                                    ref R2,
                                    ref R3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            accelerations = new Deformations[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                accelerations[i].U1 = U1[i];
                accelerations[i].U2 = U2[i];
                accelerations[i].U3 = U3[i];
                accelerations[i].R1 = R1[i];
                accelerations[i].R2 = R2[i];
                accelerations[i].R3 = R3[i];
            }
        }


        /// <summary>
        /// This function reports the joint displacements for the specified point elements.
        /// The displacements reported by this function are relative displacementst.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="displacements">The displacements in the point element local 1, 2 and 3 axes directions, respectively, for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void JointDisplacement(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] displacements)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.Results.JointDispl(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref objectNames,
                                    ref elementNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref U1,
                                    ref U2,
                                    ref U3,
                                    ref R1,
                                    ref R2,
                                    ref R3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            displacements = new Deformations[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                displacements[i].U1 = U1[i];
                displacements[i].U2 = U2[i];
                displacements[i].U3 = U3[i];
                displacements[i].R1 = R1[i];
                displacements[i].R2 = R2[i];
                displacements[i].R3 = R3[i];
            }
        }


        /// <summary>
        /// This function reports the absolute joint displacements for the specified point elements.
        /// Absolute and relative displacements are the same except when reported for time history load cases subjected to acceleration loading.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="displacements">The displacements in the point element local 1, 2 and 3 axes directions, respectively, for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void JointDisplacementAbsolute(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] displacements)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.Results.JointDisplAbs(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref objectNames,
                                    ref elementNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref U1,
                                    ref U2,
                                    ref U3,
                                    ref R1,
                                    ref R2,
                                    ref R3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            displacements = new Deformations[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                displacements[i].U1 = U1[i];
                displacements[i].U2 = U2[i];
                displacements[i].U3 = U3[i];
                displacements[i].R1 = R1[i];
                displacements[i].R2 = R2[i];
                displacements[i].R3 = R3[i];
            }
        }


        /// <summary>
        /// This function reports the joint reactions for the specified point elements.
        /// The reactions reported are from restraints, springs and grounded (one-joint) links.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="jointForces">The joint force components along/about the point element local axes directions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void JointReaction(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.Results.JointReact(name,
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                            ref _numberOfItems,
                            ref objectNames,
                            ref elementNames,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref F1, ref F2, ref F3,
                            ref M1, ref M2, ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            jointForces = new Loads[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                jointForces[i].F1 = F1[i];
                jointForces[i].F2 = F2[i];
                jointForces[i].F3 = F3[i];
                jointForces[i].M1 = M1[i];
                jointForces[i].M2 = M2[i];
                jointForces[i].M3 = M3[i];
            }
        }


        /// <summary>
        /// This function reports the joint velocities for the specified point elements.
        /// The velocities reported by this function are relative velocities.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="velocities">The velocities along/about the point element local 1, 2 and 3 axes directions, respectively, for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void JointVelocity(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] velocities)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.Results.JointVel(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref objectNames,
                                    ref elementNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref U1,
                                    ref U2,
                                    ref U3,
                                    ref R1,
                                    ref R2,
                                    ref R3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            velocities = new Deformations[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                velocities[i].U1 = U1[i];
                velocities[i].U2 = U2[i];
                velocities[i].U3 = U3[i];
                velocities[i].R1 = R1[i];
                velocities[i].R2 = R2[i];
                velocities[i].R3 = R3[i];
            }
        }


        /// <summary>
        /// This function reports the joint absolute velocities for the specified point elements.
        /// Absolute and relative velocities are the same, except when reported for time history load cases subjected to acceleration loading.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="velocities">The velocities along/about the point element local 1, 2 and 3 axes directions, respectively, for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void JointVelocityAbsolute(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] velocities)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.Results.JointVelAbs(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref objectNames,
                                    ref elementNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref U1,
                                    ref U2,
                                    ref U3,
                                    ref R1,
                                    ref R2,
                                    ref R3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            velocities = new Deformations[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                velocities[i].U1 = U1[i];
                velocities[i].U2 = U2[i];
                velocities[i].U3 = U3[i];
                velocities[i].R1 = R1[i];
                velocities[i].R2 = R2[i];
                velocities[i].R3 = R3[i];
            }
        }
        #endregion

        #region Methods: Link
        /// <summary>
        /// This function reports the link internal deformations.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="deformations">The internal deformation of the link along/about the link element local axes directions, for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void LinkDeformation(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] deformations)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.Results.LinkDeformation(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref objectNames,
                                    ref elementNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref U1,
                                    ref U2,
                                    ref U3,
                                    ref R1,
                                    ref R2,
                                    ref R3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            deformations = new Deformations[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                deformations[i].U1 = U1[i];
                deformations[i].U2 = U2[i];
                deformations[i].U3 = U3[i];
                deformations[i].R1 = R1[i];
                deformations[i].R2 = R2[i];
                deformations[i].R3 = R3[i];
            }
        }


        /// <summary>
        /// This function reports the link forces at the point elements at the ends of the specified link elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="forces">The internal forces for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void LinkForce(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Forces[] forces)
        {
            double[] P = new double[0];
            double[] V2 = new double[0];
            double[] V3 = new double[0];
            double[] T = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];


            _callCode = _sapModel.Results.LinkForce(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref objectNames,
                                    ref elementNames,
                                    ref pointNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref P,
                                    ref V2,
                                    ref V3,
                                    ref T,
                                    ref M2,
                                    ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forces = new Forces[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                forces[i].P = P[i];
                forces[i].V2 = V2[i];
                forces[i].V3 = V3[i];
                forces[i].T = T[i];
                forces[i].M2 = M2[i];
                forces[i].M3 = M3[i];
            }
        }


        /// <summary>
        /// This function reports the joint forces for the point elements at the ends of the specified link elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="jointForces">The joint force components along/about the point element local axes directions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void LinkJointForce(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];
            
            _callCode = _sapModel.Results.LinkJointForce(name,
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                            ref _numberOfItems,
                            ref objectNames,
                            ref elementNames,
                            ref pointNames,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref F1, ref F2, ref F3,
                            ref M1, ref M2, ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            jointForces = new Loads[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                jointForces[i].F1 = F1[i];
                jointForces[i].F2 = F2[i];
                jointForces[i].F3 = F3[i];
                jointForces[i].M1 = M1[i];
                jointForces[i].M2 = M2[i];
                jointForces[i].M3 = M3[i];
            }
        }
        #endregion

        #region Methods: Modal        
        /// <summary>
        /// This function reports the modal load participation ratios for each selected modal analysis case.
        /// </summary>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="itemTypes">This is an array that includes Load Pattern, Acceleration, Link or Panel Zone.
        /// It specifies the type of item for which the modal load participation is reported.</param>
        /// <param name="items">This is an array whose values depend on the ItemType.
        /// If the <paramref name="itemTypes" />  is Load Pattern, this is the name of the load pattern.
        /// If the <paramref name="itemTypes" /> is Acceleration, this is UX, UY, UZ, RX, RY, or RZ, indicating the acceleration direction.
        /// If the <paramref name="itemTypes" /> is Link, this is the name of the link followed by U1, U2, U3, R1, R2, or R3(in parenthesis), indicating the link degree of freedom for which the output is reported.
        /// If the <paramref name="itemTypes" /> is Panel Zone, this is the name of the joint to which the panel zone is assigned, followed by U1, U2, U3, R1, R2, or R3(in parenthesis), indicating the degree of freedom for which the output is reported.</param>
        /// <param name="percentStaticLoadParticipationRatio">The percent static load participation ratio for each result.</param>
        /// <param name="percentDynamicLoadParticipationRatio">The percent dynamic load participation ratio for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void ModalLoadParticipationRatios(ref string[] loadCases,
            ref string[] itemTypes,
            ref string[] items,
            ref double[] percentStaticLoadParticipationRatio,
            ref double[] percentDynamicLoadParticipationRatio)
        {
            _callCode = _sapModel.Results.ModalLoadParticipationRatios(ref _numberOfItems,
                            ref loadCases,
                            ref itemTypes,
                            ref items,
                            ref percentStaticLoadParticipationRatio,
                            ref percentDynamicLoadParticipationRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function reports the modal participating mass ratios for each mode of each selected modal analysis case.
        /// </summary>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="periods">The period for each result. [s].</param>
        /// <param name="massRatios">The modal participating mass ratio for the structure for each global degree of freedom.
        /// The ratio applies to the specified mode.</param>
        /// <param name="massRatioSums">The cumulative sum of the modal participating mass ratios for the each degree of freedom.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void ModalParticipatingMassRatios(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] periods,
            ref Mass[] massRatios,
            ref Mass[] massRatioSums)
        {
            double[] Ux = new double[0];
            double[] Uy = new double[0];
            double[] Uz = new double[0];
            double[] Rx = new double[0];
            double[] Ry = new double[0];
            double[] Rz = new double[0];

            double[] UxSum = new double[0];
            double[] UySum = new double[0];
            double[] UzSum = new double[0];
            double[] RxSum = new double[0];
            double[] RySum = new double[0];
            double[] RzSum = new double[0];

            _callCode = _sapModel.Results.ModalParticipatingMassRatios(ref _numberOfItems,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref periods,
                            ref Ux, ref Uy, ref Uz,
                            ref UxSum, ref UySum, ref UzSum,
                            ref Rx, ref Ry, ref Rz,
                            ref RxSum, ref RySum, ref RzSum);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            massRatios = new Mass[_numberOfItems - 1];
            massRatioSums = new Mass[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                massRatios[i].U1 = Ux[i];
                massRatios[i].U2 = Uy[i];
                massRatios[i].U3 = Uz[i];
                massRatios[i].R1 = Rx[i];
                massRatios[i].R2 = Ry[i];
                massRatios[i].R3 = Rz[i];

                massRatioSums[i].U1 = UxSum[i];
                massRatioSums[i].U2 = UySum[i];
                massRatioSums[i].U3 = UzSum[i];
                massRatioSums[i].R1 = RxSum[i];
                massRatioSums[i].R2 = RySum[i];
                massRatioSums[i].R3 = RzSum[i];
            }
        }


        /// <summary>
        /// This function reports the modal participation factors for each mode of each selected modal analysis case.
        /// </summary>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="periods">The period for each result. [s].</param>
        /// <param name="participationFactors">The modal participation factors for the structure translation and rotation degrees of freedom.
        /// The factor applies to the specified mode. [F*s^2] for translational, [F*L*s^2] for rotational.</param>
        /// <param name="modalMasses">The modal mass for the specified mode.
        /// This is a measure of the kinetic energy in the structure as it is deforming in the specified mode. [F*L*s^2].</param>
        /// <param name="modalStiffnesses">The modal stiffness for the specified mode.
        /// This is a measure of the strain energy in the structure as it is deforming in the specified mode. [F*L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void ModalParticipationFactors(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] periods,
            ref Mass[] participationFactors,
            ref double[] modalMasses,
            ref double[] modalStiffnesses)
        {
            double[] Ux = new double[0];
            double[] Uy = new double[0];
            double[] Uz = new double[0];
            double[] Rx = new double[0];
            double[] Ry = new double[0];
            double[] Rz = new double[0];

            _callCode = _sapModel.Results.ModalParticipationFactors(ref _numberOfItems,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref periods,
                            ref Ux, ref Uy, ref Uz,
                            ref Rx, ref Ry, ref Rz,
                            ref modalMasses,
                            ref modalStiffnesses);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            participationFactors = new Mass[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                participationFactors[i].U1 = Ux[i];
                participationFactors[i].U2 = Uy[i];
                participationFactors[i].U3 = Uz[i];
                participationFactors[i].R1 = Rx[i];
                participationFactors[i].R2 = Ry[i];
                participationFactors[i].R3 = Rz[i];
            }
        }


        /// <summary>
        /// This function reports the modal period, cyclic frequency, circular frequency and eigenvalue for each selected modal load case.
        /// </summary>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="periods">The period for each result. [s].</param>
        /// <param name="frequencies">The cyclic frequency for each result. [1/s].</param>
        /// <param name="circularFrequencies">The circular frequency for each result. [rad/s].</param>
        /// <param name="eigenvalues">The eigenvalue for the specified mode for each result. [rad^2/s^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void ModalPeriod(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] periods,
            ref double[] frequencies,
            ref double[] circularFrequencies,
            ref double[] eigenvalues)
        {
            _callCode = _sapModel.Results.ModalPeriod(ref _numberOfItems, 
                            ref loadCases, 
                            ref stepTypes, 
                            ref stepNumbers, 
                            ref periods, 
                            ref frequencies, 
                            ref circularFrequencies, 
                            ref eigenvalues);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function reports the modal displacements (mode shapes) for the specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="displacements">The displacement/rotation in the point element local 1, 2 and 3 axes directions, respectively, for each result. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void ModeShape(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] displacements)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.Results.ModeShape(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref objectNames,
                                    ref elementNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref U1,
                                    ref U2,
                                    ref U3,
                                    ref R1,
                                    ref R2,
                                    ref R3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            displacements = new Deformations[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                displacements[i].U1 = U1[i];
                displacements[i].U2 = U2[i];
                displacements[i].U3 = U3[i];
                displacements[i].R1 = R1[i];
                displacements[i].R2 = R2[i];
                displacements[i].R3 = R3[i];
            }
        }
        #endregion

        #region Methods: Panel Zone
        /// <summary>
        /// This function reports the panel zone (link) internal deformations.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="deformations">The internal deformation of the panel zone (link) along/about the link element local axes directions, for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void PanelZoneDeformation(string name,
            eItemTypeElement itemType,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] deformations)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.Results.PanelZoneDeformation(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref elementNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref U1,
                                    ref U2,
                                    ref U3,
                                    ref R1,
                                    ref R2,
                                    ref R3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            deformations = new Deformations[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                deformations[i].U1 = U1[i];
                deformations[i].U2 = U2[i];
                deformations[i].U3 = U3[i];
                deformations[i].R1 = R1[i];
                deformations[i].R2 = R2[i];
                deformations[i].R3 = R3[i];
            }
        }


        /// <summary>
        /// This function reports the panel zone (link) forces at the point elements at the ends of the specified panel zone (link) elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="forces">The internal forces for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void PanelZoneForce(string name,
            eItemTypeElement itemType,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Forces[] forces)
        {
            double[] P = new double[0];
            double[] V2 = new double[0];
            double[] V3 = new double[0];
            double[] T = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.Results.PanelZoneForce(name,
                                    EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                                    ref _numberOfItems,
                                    ref elementNames,
                                    ref pointNames,
                                    ref loadCases,
                                    ref stepTypes,
                                    ref stepNumbers,
                                    ref P,
                                    ref V2,
                                    ref V3,
                                    ref T,
                                    ref M2,
                                    ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forces = new Forces[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                forces[i].P = P[i];
                forces[i].V2 = V2[i];
                forces[i].V3 = V3[i];
                forces[i].T = T[i];
                forces[i].M2 = M2[i];
                forces[i].M3 = M3[i];
            }
        }
        #endregion

        #region Methods: Section Cut
        /// <summary>
        /// This function reports the section cut force for sections cuts that are specified to have an Analysis (F1, F2, F3, M1, M2, M3) result type.
        /// </summary>
        /// <param name="sectionCuts">The name of the section cut associated with each result.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="analysisForces">The joint force components along/about the point element local axes.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SectionCutAnalysis(ref string[] sectionCuts,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] analysisForces)
        {
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.Results.SectionCutAnalysis(ref _numberOfItems,
                            ref sectionCuts,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref F1, ref F2, ref F3,
                            ref M1, ref M2, ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            analysisForces = new Loads[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                analysisForces[i].F1 = F1[i];
                analysisForces[i].F2 = F2[i];
                analysisForces[i].F3 = F3[i];
                analysisForces[i].M1 = M1[i];
                analysisForces[i].M2 = M2[i];
                analysisForces[i].M3 = M3[i];
            }
        }



        /// <summary>
        /// This function reports the section cut force for sections cuts that are specified to have a Design (P, V2, V3, T, M2, M3) result type.
        /// </summary>
        /// <param name="sectionCuts">The section cuts.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="designForces">The internal forces for each result.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SectionCutDesign(ref string[] sectionCuts,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Forces[] designForces)
        {
            double[] P = new double[0];
            double[] V2 = new double[0];
            double[] V3 = new double[0];
            double[] T = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];
            
            _callCode = _sapModel.Results.SectionCutDesign(ref _numberOfItems,
                            ref sectionCuts,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref P, ref V2, ref V3,
                            ref T, ref M2, ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            designForces = new Forces[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                designForces[i].P = P[i];
                designForces[i].V2 = V2[i];
                designForces[i].V3 = V3[i];
                designForces[i].T = T[i];
                designForces[i].M2 = M2[i];
                designForces[i].M3 = M3[i];
            }
        }
        #endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        #region Methods: Solid        
        /// <summary>
        /// This function reports the joint forces for the point elements at each corner of the specified solid elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="jointForces">The joint force components along/about the point element local axes directions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SolidJointForce(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.Results.SolidJointForce(name,
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                            ref _numberOfItems,
                            ref objectNames,
                            ref elementNames,
                            ref pointNames,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref F1, ref F2, ref F3,
                            ref M1, ref M2, ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            jointForces = new Loads[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                jointForces[i].F1 = F1[i];
                jointForces[i].F2 = F2[i];
                jointForces[i].F3 = F3[i];
                jointForces[i].M1 = M1[i];
                jointForces[i].M2 = M2[i];
                jointForces[i].M3 = M3[i];
            }
        }


        /// <summary>
        /// This function reports the stresses for the specified solid elements.
        /// Stresses are reported at each point element associated with the solid element.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <param name="objectNames">The object name associated with each result, if any.</param>
        /// <param name="elementNames">The element name associated with each result, if any.</param>
        /// <param name="pointNames">The point element name associated with each result, if any.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="stresses">The solid element internal stresses at the specified point element location, reported in the solid element local coordinate system. [F/L^2].</param>
        /// <param name="SMid">The solid element middle principal stresses at the specified point element location. [F/L^2]</param>
        /// <param name="DirectionCosineMax1">The direction cosines defining the orientation of the maximum principal stress with respect to the solid element local-1 axis.</param>
        /// <param name="DirectionCosineMax2">The direction cosines defining the orientation of the maximum principal stress with respect to the solid element local-2 axis.</param>
        /// <param name="DirectionCosineMax3">The direction cosines defining the orientation of the maximum principal stress with respect to the solid element local-3 axis.</param>
        /// <param name="DirectionCosineMid1">The direction cosines defining the orientation of the middle principal stress with respect to the solid element local-1 axis.</param>
        /// <param name="DirectionCosineMid2">The direction cosines defining the orientation of the middle principal stress with respect to the solid element local-2 axis.</param>
        /// <param name="DirectionCosineMid3">The direction cosines defining the orientation of the middle principal stress with respect to the solid element local-3 axis.</param>
        /// <param name="DirectionCosineMin1">The direction cosines defining the orientation of the minimum principal stress with respect to the solid element local-1 axis.</param>
        /// <param name="DirectionCosineMin2">The direction cosines defining the orientation of the minimum principal stress with respect to the solid element local-2 axis.</param>
        /// <param name="DirectionCosineMin3">The direction cosines defining the orientation of the minimum principal stress with respect to the solid element local-3 axis.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SolidStress(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Stress[] stresses,
            ref double[] SMid,
            ref double[] DirectionCosineMax1,
            ref double[] DirectionCosineMax2,
            ref double[] DirectionCosineMax3,
            ref double[] DirectionCosineMid1,
            ref double[] DirectionCosineMid2,
            ref double[] DirectionCosineMid3,
            ref double[] DirectionCosineMin1,
            ref double[] DirectionCosineMin2,
            ref double[] DirectionCosineMin3)
        {
            double[] S11 = new double[0];
            double[] S22 = new double[0];
            double[] S33 = new double[0];
            double[] S12 = new double[0];
            double[] S13 = new double[0];
            double[] S23 = new double[0];
            double[] SMax = new double[0];
            double[] SMin = new double[0];
            double[] SVM = new double[0];

            _callCode = _sapModel.Results.SolidStress(name, 
                            EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType),
                            ref _numberOfItems,
                            ref objectNames,
                            ref elementNames,
                            ref pointNames,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref S11,
                            ref S22,
                            ref S33,
                            ref S12,
                            ref S13,
                            ref S23,
                            ref SMax,
                            ref SMid,
                            ref SMin,
                            ref SVM,
                            ref DirectionCosineMax1,
                            ref DirectionCosineMax2,
                            ref DirectionCosineMax3,
                            ref DirectionCosineMid1,
                            ref DirectionCosineMid2,
                            ref DirectionCosineMid3,
                            ref DirectionCosineMin1,
                            ref DirectionCosineMin2,
                            ref DirectionCosineMin3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stresses = new Stress[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                stresses[i].S11 = S11[i];
                stresses[i].S22 = S22[i];
                stresses[i].S33 = S33[i];
                stresses[i].S12 = S12[i];
                stresses[i].S13 = S13[i];
                stresses[i].S23 = S23[i];
                stresses[i].SMax = SMax[i];
                stresses[i].SMin = SMin[i];
                stresses[i].SVM = SVM[i];
            }
        }
        #endregion
#endif


#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        #region Methods: Shear Walls
        /// <summary>
        /// Retrieves pier forces for any defined pier objects in the model.
        /// </summary>
        /// <param name="storyNames">Story names associated with each result.</param>
        /// <param name="pierNames">The name of the pier object for each result.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="locations">The location on the pier of the result being reported.</param>
        /// <param name="forces">The internal forces for each result.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void PierForce(ref string[] storyNames,
            ref string[] pierNames,
            ref string[] loadCases,
            ref eLocationVertical[] locations,
            ref Forces[] forces)
        {
            string[] csiLocations = new string[0];
            double[] P = new double[0];
            double[] V2 = new double[0];
            double[] V3 = new double[0];
            double[] T = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.Results.PierForce(
                                    ref _numberOfItems,
                                    ref storyNames,
                                    ref pierNames,
                                    ref loadCases,
                                    ref csiLocations,
                                    ref P,
                                    ref V2,
                                    ref V3,
                                    ref T,
                                    ref M2,
                                    ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forces = new Forces[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                forces[i].P = P[i];
                forces[i].V2 = V2[i];
                forces[i].V3 = V3[i];
                forces[i].T = T[i];
                forces[i].M2 = M2[i];
                forces[i].M3 = M3[i];
            }

            locations = new eLocationVertical[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                locations[i] = EnumLibrary.ConvertStringToEnumByDescription<eLocationVertical>(csiLocations[i]);
            }

        }

        /// <summary>
        /// Retrieves spandrel forces for any defined pier objects in the model.
        /// </summary>
        /// <param name="storyNames">Story names associated with each result.</param>
        /// <param name="spandrelNames">The name of the spandrel object for each result.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="locations">The location on the pier of the result being reported.</param>
        /// <param name="forces">The internal forces for each result.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SpandrelForce(ref string[] storyNames,
            ref string[] spandrelNames,
            ref string[] loadCases,
            ref eLocationVertical[] locations,
            ref Forces[] forces)
        {
            string[] csiLocations = new string[0];
            double[] P = new double[0];
            double[] V2 = new double[0];
            double[] V3 = new double[0];
            double[] T = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.Results.SpandrelForce(
                                    ref _numberOfItems,
                                    ref storyNames,
                                    ref spandrelNames,
                                    ref loadCases,
                                    ref csiLocations,
                                    ref P,
                                    ref V2,
                                    ref V3,
                                    ref T,
                                    ref M2,
                                    ref M3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forces = new Forces[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                forces[i].P = P[i];
                forces[i].V2 = V2[i];
                forces[i].V3 = V3[i];
                forces[i].T = T[i];
                forces[i].M2 = M2[i];
                forces[i].M3 = M3[i];
            }

            locations = new eLocationVertical[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                locations[i] = EnumLibrary.ConvertStringToEnumByDescription<eLocationVertical>(csiLocations[i]);
            }
        }
        #endregion
#endif

        #region Methods: Misc        
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
        /// <summary>
        /// This function returns the longitudinal stresses for multiple cases/combos at a single stress point in a superstructure section cut in a bridge object. <para/>
        /// Use the functions in <see cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult.AnalysisResultsSetup"/> to control the loads and steps for which results are to be obtained.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by function <see cref="MPT.CSI.API.Core.Program.ModelBehavior.BridgeAdvanced.Superstructure.CountSuperstructureCut"/></param>
        /// <param name="pointIndex">The index number of the stress point in this section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by function <see cref="MPT.CSI.API.Core.Program.ModelBehavior.BridgeAdvanced.Superstructure.CountSuperstructureCutStressPoint"/>.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="stresses">This is an array that includes the longitudinal stress value for each result. [F/L^2].</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void BridgeSuperstructureCutLongitudinalStress(string name,
            int cutIndex,
            int pointIndex,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] stresses)
        {
            _callCode = _sapModel.Results.BridgeSuperCutLongitStress(name, cutIndex, pointIndex, ref _numberOfItems, ref loadCases, ref stepTypes, ref stepNumbers, ref stresses);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function generates the step label for analyzed staged-construction load cases.
        /// For load case types other then the staged construction, the label will be blank.
        /// </summary>
        /// <param name="loadCase">The name of an existing staged-construction load case.</param>
        /// <param name="stepNumber">This is an overall step number from the specified staged-construction load case.
        /// The range of values of <paramref name="stepNumber" /> for a given load case can be obtained from most analysis results calls.</param>
        /// <param name="label">The is the step label, including the name or number of the stage, the step number within the stage, and the age of the structure for time-dependent load cases.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void StepLabel(string loadCase,
            double stepNumber,
            ref string label)
        {
            _callCode = _sapModel.Results.StepLabel(loadCase, stepNumber, ref label);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function reports the joint response spectra values, due to a time history analysis, for the specified point elements.
        /// </summary>
        /// <param name="stories">Story levels associated with each result.</param>
        /// <param name="labels">The point labels for each result.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step types, if any, for each result.</param>
        /// <param name="stepNumbers">The step numbers, if any, for each result.</param>
        /// <param name="directions">The directions for which the results are reported.</param>
        /// <param name="drifts">The maximum drift values.</param>
        /// <param name="displacementsX">The displacements in the x-direction [L].</param>
        /// <param name="displacementsY">The displacements in the y-direction [L].</param>
        /// <param name="displacementsZ">The displacements in the z-direction [L].</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void StoryDrifts(ref string[] stories,
            ref string[] labels,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref string[] directions,
            ref double[] drifts,
            ref double[] displacementsX,
            ref double[] displacementsY,
            ref double[] displacementsZ)
        {
            _callCode = _sapModel.Results.StoryDrifts(
                            ref _numberOfItems,
                            ref stories,
                            ref loadCases,
                            ref stepTypes,
                            ref stepNumbers,
                            ref directions,
                            ref drifts,
                            ref labels,
                            ref displacementsX,
                            ref displacementsY,
                            ref displacementsZ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        /// <summary>
        /// This function reports buckling factors obtained from buckling load cases.
        /// </summary>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="bucklingFactors">The buckling factors.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void BucklingFactor(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] bucklingFactors)
        {
            _callCode = _sapModel.Results.BucklingFactor(ref _numberOfItems, ref loadCases, ref stepTypes, ref stepNumbers, ref bucklingFactors);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function reports the displacement values for the specified generalized displacements.
        /// </summary>
        /// <param name="name">The name of an existing generalized displacement for which results are returned.
        /// If the program does not recognize this name as a defined generalized displacement, it returns results for all selected generalized displacements, if any.
        /// For example, entering a blank string (i.e., "") for the name will prompt the program to return results for all selected generalized displacementse.</param>
        /// <param name="names">The generalized displacement name associated with each result.</param>
        /// <param name="loadCases">The name of the analysis case or load combination associated with each result.</param>
        /// <param name="stepTypes">The step type, if any, for each result.</param>
        /// <param name="stepNumbers">The step number, if any, for each result.</param>
        /// <param name="types">The generalized displacement type for each result.
        /// It is either Translation or Rotation.</param>
        /// <param name="generalizedDisplacements">The the generalized displacement values for each result.[L] when <paramref name="types" /> is Translation , [rad] when <paramref name="types" /> is Rotation.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GeneralizedDisplacement(string name,
            ref string[] names,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref string[] types,
            ref double[] generalizedDisplacements)
        {
            _callCode = _sapModel.Results.GeneralizedDispl(name, ref _numberOfItems, ref names, ref loadCases, ref stepTypes, ref stepNumbers, ref types, ref generalizedDisplacements);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
