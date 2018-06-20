// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="INamedSets.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements the joint response spectrum set in the application.
    /// </summary>
    public interface INamedSets
    {
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
        void GetJointRespSpec(string nameSet,
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
            ref double ordinateScaleFactor);

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
        /// <param name="dampingValues">Critical damping ratio values.</param>
        /// <param name="abscissaPlotType">Abscissa axis scale type.</param>
        /// <param name="spectrumWidening">This specifies the percentage by which to widen the peaks of the spectrum.</param>
        /// <param name="ordinatePlotType">Ordinate axis scale type.</param>
        /// <param name="ordinateScaleFactor">This is the scale factor used to linearly scale the response spectrum ordinate values.</param>
        void SetJointRespSpec(string nameSet,
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
            double ordinateScaleFactor);
#endregion
    }
}
#endif