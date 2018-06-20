// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="NTC_2008.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Represents the UBC_94 auto seismic load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic.AutoSeismicLoad" />
    /// <seealso cref="AutoSeismicLoad" />
    public class NTC_2008 : AutoSeismicLoad
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NTC_2008" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public NTC_2008(CSiApiSeed seed) : base(seed) { }


#endregion

#region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for the NTC 2008 code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="C1Type">Type of the c1.
        /// This only applies when <paramref name="periodOption" /> = <see cref="eTimePeriodOption.Approximate" /> or <see cref="eTimePeriodOption.ProgramCalculated" />.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramref name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" />.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="soilType">Type of the soil.</param>
        /// <param name="PGA">The peak ground acceleration, ag/g.</param>
        /// <param name="parameterOptions">The option for defining the parameters.</param>
        /// <param name="latitude">The latitude for which the seismic coefficients are obtained.
        /// This only applies when <paramref name="parameterOptions" /> = <see cref="eParameters_NTC_2008.ByLatLong" /></param>
        /// <param name="longitude">The longitude for which the seismic coefficients are obtained.
        /// This only applies when <paramref name="parameterOptions" /> = <see cref="eParameters_NTC_2008.ByLatLong" /></param>
        /// <param name="island">The island for which the seismic coefficients are obtained.
        /// This only applies when <paramref name="parameterOptions" /> = <see cref="eParameters_NTC_2008.ByIsland" /></param>
        /// <param name="limitState">The limit state.</param>
        /// <param name="usageClass">The usage class.</param>
        /// <param name="nominalLife">The nominal life to be considered.</param>
        /// <param name="F0">The magnitude factor, F0.</param>
        /// <param name="Tcs">The reference period, Tc* [s].</param>
        /// <param name="spectrumType">The type of spectrum to consider.</param>
        /// <param name="topography">The topography type.</param>
        /// <param name="hRatio">The ratio for the site altitude at the base of the hill to the height of the hill..</param>
        /// <param name="damping">The damping, in percent.
        /// This only applies when <paramref name="spectrumType" /> = <see cref="eSpectrumType_NTC_2008.ElasticHorizontal" /> or <see cref="eSpectrumType_NTC_2008.ElasticVertical" />.</param>
        /// <param name="q">The behavior correction factor, q.
        /// This only applies when <paramref name="spectrumType" /> = <see cref="eSpectrumType_NTC_2008.DesignHorizontal" /> or <see cref="eSpectrumType_NTC_2008.DesignVertical" />.</param>
        /// <param name="lambda">The correction factor, Lambda.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref eC1Type_NTC_2008 C1Type,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_Eurocode_8_2004 soilType,
            ref double PGA,
            ref eParameters_NTC_2008 parameterOptions,
            ref double latitude,
            ref double longitude,
            ref eIsland_NTC_2008 island,
            ref eLimitState_NTC_2008 limitState,
            ref eUsageClass_NTC_2008 usageClass,
            ref double nominalLife,
            ref double F0,
            ref double Tcs,
            ref eSpectrumType_NTC_2008 spectrumType,
            ref eTopography_NTC_2008 topography,
            ref double hRatio,
            ref double damping,
            ref double q,
            ref double lambda)
        {
            int csiLoadDirection = 0;
            int csiPeriodOption = 0;
            int csiC1Type = 0;
            int csiSoilType = 0;
            int csiParameterOptions = 0;
            int csiIsland = 0;
            int csiLimitState = 0;
            int csiUsageClass = 0;
            int csiSpectrumType = 0;
            int csiTopography = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetNTC2008(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref csiPeriodOption,
                            ref csiC1Type,
                            ref userSpecifiedPeriod,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref csiParameterOptions,
                            ref latitude,
                            ref longitude,
                            ref csiIsland,
                            ref csiLimitState,
                            ref csiUsageClass,
                            ref nominalLife,
                            ref PGA,
                            ref F0,
                            ref Tcs,
                            ref csiSpectrumType,
                            ref csiSoilType,
                            ref csiTopography,
                            ref hRatio,
                            ref q,
                            ref damping,
                            ref lambda);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            periodOption = (eTimePeriodOption)csiPeriodOption;
            C1Type = (eC1Type_NTC_2008)csiC1Type;
            soilType = (eSiteClass_Eurocode_8_2004)csiSoilType;
            parameterOptions = (eParameters_NTC_2008)csiParameterOptions;
            island = (eIsland_NTC_2008)csiIsland;
            limitState = (eLimitState_NTC_2008)csiLimitState;
            usageClass = (eUsageClass_NTC_2008)csiUsageClass;
            spectrumType = (eSpectrumType_NTC_2008)csiSpectrumType;
            topography = (eTopography_NTC_2008)csiTopography;
        }


        /// <summary>
        /// This function assigns auto seismic loading parameters for the NTC 2008 code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="C1Type">Type of the c1.
        /// This only applies when <paramnamef name="periodOption" /> = <see cref="eTimePeriodOption.Approximate" /> or <see cref="eTimePeriodOption.ProgramCalculated" />.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramnamef name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" />.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="soilType">Type of the soil.</param>
        /// <param name="PGA">The peak ground acceleration, ag/g.</param>
        /// <param name="parameterOptions">The option for defining the parameters.</param>
        /// <param name="latitude">The latitude for which the seismic coefficients are obtained.
        /// This only applies when <paramnamef name="parameterOptions" /> = <see cref="eParameters_NTC_2008.ByLatLong" /></param>
        /// <param name="longitude">The longitude for which the seismic coefficients are obtained.
        /// This only applies when <paramnamef name="parameterOptions" /> = <see cref="eParameters_NTC_2008.ByLatLong" /></param>
        /// <param name="island">The island for which the seismic coefficients are obtained.
        /// This only applies when <paramnamef name="parameterOptions" /> = <see cref="eParameters_NTC_2008.ByIsland" /></param>
        /// <param name="limitState">The limit state.</param>
        /// <param name="usageClass">The usage class.</param>
        /// <param name="nominalLife">The nominal life to be considered.</param>
        /// <param name="F0">The magnitude factor, F0.</param>
        /// <param name="Tcs">The reference period, Tc* [s].</param>
        /// <param name="spectrumType">The type of spectrum to consider.</param>
        /// <param name="topography">The topography type.</param>
        /// <param name="hRatio">The ratio for the site altitude at the base of the hill to the height of the hill..</param>
        /// <param name="damping">The damping, in percent.
        /// This only applies when <paramnamef name="spectrumType" /> = <see cref="eSpectrumType_NTC_2008.ElasticHorizontal" /> or <see cref="eSpectrumType_NTC_2008.ElasticVertical" />.</param>
        /// <param name="q">The behavior correction factor, q.
        /// This only applies when <paramnamef name="spectrumType" /> = <see cref="eSpectrumType_NTC_2008.DesignHorizontal" /> or <see cref="eSpectrumType_NTC_2008.DesignVertical" />.</param>
        /// <param name="lambda">The correction factor, Lambda.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            eC1Type_NTC_2008 C1Type,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_Eurocode_8_2004 soilType,
            double PGA,
            eParameters_NTC_2008 parameterOptions,
            double latitude,
            double longitude,
            eIsland_NTC_2008 island,
            eLimitState_NTC_2008 limitState,
            eUsageClass_NTC_2008 usageClass,
            double nominalLife,
            double F0,
            double Tcs,
            eSpectrumType_NTC_2008 spectrumType,
            eTopography_NTC_2008 topography,
            double hRatio,
            double damping,
            double q,
            double lambda)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetNTC2008(name,
                            (int)loadDirection,
                            eccentricity,
                            (int)periodOption,
                            (int)C1Type,
                            userSpecifiedPeriod,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            (int)parameterOptions,
                            latitude,
                            longitude,
                            (int)island,
                            (int)limitState,
                            (int)usageClass,
                            nominalLife,
                            PGA,
                            F0,
                            Tcs,
                            (int)spectrumType,
                            (int)soilType,
                            (int)topography,
                            hRatio,
                            q,
                            damping,
                            lambda);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif
