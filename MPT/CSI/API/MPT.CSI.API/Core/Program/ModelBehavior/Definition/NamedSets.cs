// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="NamedSets.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents the joint response spectrum set in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.INamedSets" />
    public class NamedSets : CSiApiBase, INamedSets
    {

#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedSets" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public NamedSets(CSiApiSeed seed) : base(seed) { }


#endregion

#region Methods: Public
        /// <summary>
        /// This function gets a joint response spectrum named set definition.
        /// </summary>
        /// <param name="nameSet">The name of an existing joint response spectrum named set.</param>
        /// <param name="loadCase">The name of a time history load case for which results will be extracted.</param>
        /// <param name="nameJoints">This is an array of joint names for which to generate response spectra.</param>
        /// <param name="coordinateSystem">The name of a coordinate system in which the direction for results is defined.
        /// This can be Global, Local, or any other user-defined coordinate system.</param>
        /// <param name="direction">This specifies the direction, in the specified coordinate system, in which the results are to be retrieved.
        /// Valid values for the direction are:
        /// 1 = Local 1, Global X, or user-defined coordinate system X
        /// 2 = Local 2, Global Y, or user-defined coordinate system Y
        /// 3 = Local 3, Global Z, or user-defined coordinate system Z</param>
        /// <param name="abscissa">Specifies the abscissa data type.</param>
        /// <param name="ordinate">Specifies the ordinate data type.</param>
        /// <param name="useDefaultFrequencies">True: Default frequencies are used for output.</param>
        /// <param name="useStructuralFrequencies">True: Structural frequencies are used for output.</param>
        /// <param name="userFrequencies">User-defined frequencies.</param>
        /// <param name="dampingValues">Critical damping ratio values.</param>
        /// <param name="abscissaPlotType">Abscissa axis scale type.</param>
        /// <param name="spectrumWidening">This specifies the percentage by which to widen the peaks of the spectrum.</param>
        /// <param name="ordinatePlotType">Ordinate axis scale type.</param>
        /// <param name="ordinateScaleFactor">This is the scale factor used to linearly scale the response spectrum ordinate values.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetJointRespSpec(string nameSet,
            ref string loadCase,
            ref string[] nameJoints,
            ref string coordinateSystem,
            ref int direction,
            ref eVibrationUnit abscissa,
            ref eSpectralResponse ordinate,
            ref bool useDefaultFrequencies,
            ref bool useStructuralFrequencies,
            ref double[] userFrequencies,
            ref double[] dampingValues,
            ref ePlotType abscissaPlotType,
            ref double spectrumWidening,
            ref ePlotType ordinatePlotType,
            ref double ordinateScaleFactor)
        {
            int csiAbscissa = 0;
            int csiOrdinate = 0;
            int csiAbscissaType = 0;
            int csiOrdinateType = 0;

            _callCode = _sapModel.NamedSet.GetJointRespSpec(nameSet, ref loadCase, 
                ref _numberOfItems, ref nameJoints, 
                ref coordinateSystem, ref direction, ref csiAbscissa, ref csiOrdinate, 
                ref useDefaultFrequencies, ref useStructuralFrequencies, 
                ref _numberOfItems, ref userFrequencies, 
                ref _numberOfItems, ref dampingValues, 
                ref csiAbscissaType, ref spectrumWidening, ref csiOrdinateType, ref ordinateScaleFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            abscissa = (eVibrationUnit) csiAbscissa;
            ordinate = (eSpectralResponse)csiOrdinate;
            abscissaPlotType = (ePlotType)csiAbscissaType;
            ordinatePlotType = (ePlotType)csiOrdinateType;
        }

        /// <summary>
        /// This function defines a joint response spectrum named set.
        /// </summary>
        /// <param name="nameSet">The name of an existing joint response spectrum named set.</param>
        /// <param name="loadCase">The name of a time history load case for which results will be extracted.</param>
        /// <param name="nameJoints">This is an array of joint names for which to generate response spectra.</param>
        /// <param name="coordinateSystem">The name of a coordinate system in which the direction for results is defined.
        /// This can be Global, Local, or any other user-defined coordinate system.</param>
        /// <param name="direction">This specifies the direction, in the specified coordinate system, in which the results are to be retrieved.
        /// Valid values for the direction are:
        /// 1 = Local 1, Global X, or user-defined coordinate system X
        /// 2 = Local 2, Global Y, or user-defined coordinate system Y
        /// 3 = Local 3, Global Z, or user-defined coordinate system Z</param>
        /// <param name="abscissa">Specifies the abscissa data type.</param>
        /// <param name="ordinate">Specifies the ordinate data type.</param>
        /// <param name="useDefaultFrequencies">True: Default frequencies are used for output.</param>
        /// <param name="useStructuralFrequencies">True: Structural frequencies are used for output.</param>
        /// <param name="userFrequencies">User-defined frequencies.</param>
        /// <param name="dampingValues">Critical damping ratio values.
        /// Must be at least one.</param>
        /// <param name="abscissaPlotType">Abscissa axis scale type.</param>
        /// <param name="spectrumWidening">This specifies the percentage by which to widen the peaks of the spectrum.</param>
        /// <param name="ordinatePlotType">Ordinate axis scale type.</param>
        /// <param name="ordinateScaleFactor">This is the scale factor used to linearly scale the response spectrum ordinate values.</param>
        /// <exception cref="CSiException">
        /// dampingValues + " must have at least one element."
        /// or
        /// API_DEFAULT_ERROR_CODE</exception>
        public void SetJointRespSpec(string nameSet,
            string loadCase,
            ref string[] nameJoints,
            string coordinateSystem,
            int direction,
            eVibrationUnit abscissa,
            eSpectralResponse ordinate,
            bool useDefaultFrequencies,
            bool useStructuralFrequencies,
            ref double[] userFrequencies,
            ref double[] dampingValues,
            ePlotType abscissaPlotType,
            double spectrumWidening,
            ePlotType ordinatePlotType,
            double ordinateScaleFactor)
        {
            if (dampingValues.Length < 1)
            {
                throw new CSiException(nameof(dampingValues) + " must have at least one element.");
            }

            int csiAbscissa = (int)abscissa;
            int csiOrdinate = (int)ordinate;
            int csiAbscissaType = (int)abscissaPlotType;
            int csiOrdinateType = (int)ordinatePlotType;

            _callCode = _sapModel.NamedSet.SetJointRespSpec(nameSet, loadCase, 
                            nameJoints.Length, ref nameJoints, 
                            coordinateSystem, direction, csiAbscissa, csiOrdinate, 
                            useDefaultFrequencies, useStructuralFrequencies,
                            userFrequencies.Length, ref userFrequencies,
                            dampingValues.Length, ref dampingValues, 
                            csiAbscissaType, spectrumWidening, csiOrdinateType, ordinateScaleFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif