// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="Eurocode_8_2004.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Represents the UBC_94 auto seismic load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic.AutoSeismicLoad" />
    /// <seealso cref="AutoSeismicLoad" />
    public class Eurocode_8_2004 : AutoSeismicLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Eurocode_8_2004" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Eurocode_8_2004(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for the Eurocode 8 2004 code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="Ct">The code-specified Ct factor.
        /// This only applies when <paramref name="periodOption" /> = <see cref="eTimePeriodOption.Approximate" /> or <see cref="eTimePeriodOption.ProgramCalculated" />.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramref name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" /></param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="soilType">Type of the soil.</param>
        /// <param name="ag">The design ground acceleration, ag. [g].</param>
        /// <param name="country">The country for which the Nationally Determined Parameters (NDPs) are specified.</param>
        /// <param name="spectrumType">Type of spectrum.</param>
        /// <param name="S">The soil factor, S.</param>
        /// <param name="Tb">The lower limit of period of the constant spectral acceleration branch, Tb.</param>
        /// <param name="Tc">The upper limit of period of the constant spectral acceleration branch, Tc.</param>
        /// <param name="Td">The period defining the start of the constant displacement range, Td.</param>
        /// <param name="beta">The lower bound factor, Beta.</param>
        /// <param name="q">The behavior factor, q.</param>
        /// <param name="lambda">The correction factor, Lambda.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref double Ct,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_Eurocode_8_2004 soilType,
            ref double ag,
            ref eParameters_Eurocode_8_2004 country,
            ref eSpectrumType_Eurocode_8_2004 spectrumType,
            ref double S,
            ref double Tb,
            ref double Tc,
            ref double Td,
            ref double beta,
            ref double q,
            ref double lambda)
        {
            int csiLoadDirection = 0;
            int csiPeriodOption = 0;
            int csiSoilType = 0;
            int csiCountry = 0;
            int csiSpectrumType = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetEurocode82004_1(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref csiPeriodOption,
                            ref Ct,
                            ref userSpecifiedPeriod,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref csiCountry,
                            ref csiSpectrumType,
                            ref csiSoilType,
                            ref ag,
                            ref S,
                            ref Tb,
                            ref Tc,
                            ref Td,
                            ref beta,
                            ref q,
                            ref lambda);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            periodOption = (eTimePeriodOption)csiPeriodOption;
            soilType = (eSiteClass_Eurocode_8_2004)csiSoilType;
            country = (eParameters_Eurocode_8_2004)csiCountry;
            spectrumType = (eSpectrumType_Eurocode_8_2004)csiSpectrumType;
        }

        /// <summary>
        /// This function assigns auto seismic loading parameters for the Eurocode 8 2004 code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="Ct">The code-specified Ct factor.
        /// This only applies when <paramnamef name="periodOption" /> = <see cref="eTimePeriodOption.Approximate" /> or <see cref="eTimePeriodOption.ProgramCalculated" />.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramnamef name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" /></param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="soilType">Type of the soil.</param>
        /// <param name="ag">The design ground acceleration, ag. [g].</param>
        /// <param name="country">The country for which the Nationally Determined Parameters (NDPs) are specified.</param>
        /// <param name="spectrumType">Type of spectrum.</param>
        /// <param name="S">The soil factor, S.</param>
        /// <param name="Tb">The lower limit of period of the constant spectral acceleration branch, Tb.</param>
        /// <param name="Tc">The upper limit of period of the constant spectral acceleration branch, Tc.</param>
        /// <param name="Td">The period defining the start of the constant displacement range, Td.</param>
        /// <param name="beta">The lower bound factor, Beta.</param>
        /// <param name="q">The behavior factor, q.</param>
        /// <param name="lambda">The correction factor, Lambda.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            double Ct,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_Eurocode_8_2004 soilType,
            double ag,
            eParameters_Eurocode_8_2004 country,
            eSpectrumType_Eurocode_8_2004 spectrumType,
            double S,
            double Tb,
            double Tc,
            double Td,
            double beta,
            double q,
            double lambda)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetEurocode82004_1(name,
                            (int)loadDirection,
                            eccentricity,
                            (int)periodOption,
                            Ct,
                            userSpecifiedPeriod,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            (int)country,
                            (int)spectrumType,
                            (int)soilType,
                            ag,
                            S,
                            Tb,
                            Tc,
                            Td,
                            beta,
                            q,
                            lambda);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif