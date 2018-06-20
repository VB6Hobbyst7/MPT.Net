// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="NTC_2008.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Response spectrum function from NTC_2008.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class NTC_2008 : ResponseSpectrumFunction
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NTC_2008" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected NTC_2008(CSiApiSeed seed) : base(seed) { }
#endregion

#region Methods: Public
        /// <summary>
        /// The name of an NTC2008 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NTC2008  response spectrum function.</param>
        /// <param name="parametersOption">The option for defining the parameters.</param>
        /// <param name="latitude">The latitude for which the parameters are obtained. <para />
        /// These items are used only when <paramref name="parametersOption" /> = <see cref="eParameters_NTC_2008.ByLatLong" />.</param>
        /// <param name="longitude">The longitude for which the parameters are obtained. <para />
        /// These items are used only when <paramref name="parametersOption" /> = <see cref="eParameters_NTC_2008.ByLatLong" />.</param>
        /// <param name="island">The island. <para />
        /// This item is used only when <paramref name="parametersOption" /> = <see cref="eParameters_NTC_2008.ByIsland" />.</param>
        /// <param name="limitState">The limit state.</param>
        /// <param name="usageClass">The usage class.</param>
        /// <param name="nominalLife">The nominal life to be considered.</param>
        /// <param name="peakAcceleration">The peak ground acceleration, ag/g.</param>
        /// <param name="F0">The magnitude factor, F0.</param>
        /// <param name="Tcs">The reference period, Tc* [s].</param>
        /// <param name="spectrumType">The type of spectrum to consider.</param>
        /// <param name="soilType">The subsoil type.</param>
        /// <param name="topography">The topography type.</param>
        /// <param name="hRatio">The ratio for the site altitude at the base of the hill to the height of the hill.</param>
        /// <param name="damping">The damping, in percent.
        /// This is only applicable for <paramref name="spectrumType" /> = <see cref="eSpectrumType_NTC_2008.ElasticHorizontal" /> or <see cref="eSpectrumType_NTC_2008.ElasticVertical" />.</param>
        /// <param name="q">The behavior correction factor.
        /// This is only applicable for <paramref name="spectrumType" /> = <see cref="eSpectrumType_NTC_2008.DesignHorizontal" /> or <see cref="eSpectrumType_NTC_2008.DesignVertical" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref eParameters_NTC_2008 parametersOption,
            ref double latitude,
            ref double longitude,
            ref eIsland_NTC_2008 island,
            ref eLimitState_NTC_2008 limitState,
            ref eUsageClass_NTC_2008 usageClass,
            ref double nominalLife,
            ref double peakAcceleration,
            ref double F0,
            ref double Tcs,
            ref eSpectrumType_NTC_2008 spectrumType,
            ref eSiteClass_NTC_2008 soilType,
            ref eTopography_NTC_2008 topography,
            ref double hRatio,
            ref double damping,
            ref double q)
        {
            int csiParametersOption = 0;
            int csiIsland = 0;
            int csiLimitState = 0;
            int csiUsageClass = 0;
            int csiSpectrumType = 0;
            int csiSoilType = 0;
            int csiTopography = 0;

            _callCode = _sapModel.Func.FuncRS.GetNTC2008(name, ref csiParametersOption, ref latitude, ref longitude, ref csiIsland, ref csiLimitState, ref csiUsageClass, ref nominalLife, ref peakAcceleration,
                ref F0, ref Tcs, ref csiSpectrumType, ref csiSoilType, ref csiTopography, ref hRatio, ref damping, ref q);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            parametersOption = (eParameters_NTC_2008)csiParametersOption;
            island = (eIsland_NTC_2008)csiIsland;
            limitState = (eLimitState_NTC_2008)csiLimitState;
            usageClass = (eUsageClass_NTC_2008)csiUsageClass;
            spectrumType = (eSpectrumType_NTC_2008)csiSpectrumType;
            soilType = (eSiteClass_NTC_2008)csiSoilType;
            topography = (eTopography_NTC_2008)csiTopography;
        }

        /// <summary>
        /// This function defines a NTC2008 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NTC2008  response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="parametersOption">The option for defining the parameters.</param>
        /// <param name="latitude">The latitude for which the parameters are obtained. <para />
        /// These items are used only when <paramref name="parametersOption" /> = <see cref="eParameters_NTC_2008.ByLatLong" />.</param>
        /// <param name="longitude">The longitude for which the parameters are obtained. <para />
        /// These items are used only when <paramref name="parametersOption" /> = <see cref="eParameters_NTC_2008.ByLatLong" />.</param>
        /// <param name="island">The island. <para />
        /// This item is used only when <paramref name="parametersOption" /> = <see cref="eParameters_NTC_2008.ByIsland" />.</param>
        /// <param name="limitState">The limit state.</param>
        /// <param name="usageClass">The usage class.</param>
        /// <param name="nominalLife">The nominal life to be considered.</param>
        /// <param name="peakAcceleration">The peak ground acceleration, ag/g.</param>
        /// <param name="F0">The magnitude factor, F0.</param>
        /// <param name="Tcs">The reference period, Tc* [s].</param>
        /// <param name="spectrumType">The type of spectrum to consider.</param>
        /// <param name="soilType">The subsoil type.</param>
        /// <param name="topography">The topography type.</param>
        /// <param name="hRatio">The ratio for the site altitude at the base of the hill to the height of the hill.</param>
        /// <param name="damping">The damping, in percent.
        /// This is only applicable for <paramref name="spectrumType" /> = <see cref="eSpectrumType_NTC_2008.ElasticHorizontal" /> or <see cref="eSpectrumType_NTC_2008.ElasticVertical" />.</param>
        /// <param name="q">The behavior correction factor.
        /// This is only applicable for <paramref name="spectrumType" /> = <see cref="eSpectrumType_NTC_2008.DesignHorizontal" /> or <see cref="eSpectrumType_NTC_2008.DesignVertical" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            eParameters_NTC_2008 parametersOption,
            double latitude,
            double longitude,
            eIsland_NTC_2008 island,
            eLimitState_NTC_2008 limitState,
            eUsageClass_NTC_2008 usageClass,
            double nominalLife,
            double peakAcceleration,
            double F0,
            double Tcs,
            eSpectrumType_NTC_2008 spectrumType,
            eSiteClass_NTC_2008 soilType,
            eTopography_NTC_2008 topography,
            double hRatio,
            double damping,
            double q)
        {
            damping = 100*limitDampingRatio(damping/100);
            _callCode = _sapModel.Func.FuncRS.SetNTC2008(name, 
                            (int)parametersOption, latitude, longitude, (int)island, 
                            (int)limitState, (int)usageClass, nominalLife, peakAcceleration,
                            F0, Tcs, (int)spectrumType, (int)soilType, (int)topography, hRatio, damping, q);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif