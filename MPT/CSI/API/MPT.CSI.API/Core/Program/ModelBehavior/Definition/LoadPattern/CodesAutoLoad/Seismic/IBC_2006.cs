// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 09-28-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="IBC_2006.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Represents the UBC_94 auto seismic load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic.AutoSeismicLoad" />
    /// <seealso cref="AutoSeismicLoad" />
    public class IBC_2006 : AutoSeismicLoad
    {

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="IBC_2006" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public IBC_2006(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for the 2006 IBC code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="CtType">The values of Ct and x.
        /// This only applies when <paramref name="periodOption" /> = <see cref="eTimePeriodOption.Approximate" /> or <see cref="eTimePeriodOption.ProgramCalculated" />.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramref name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" /></param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="omega">The system overstrength factor.</param>
        /// <param name="R">The response modification factor.</param>
        /// <param name="I">The importance factor.</param>
        /// <param name="Ss">The response acceleration for short periods, (g).</param>
        /// <param name="S1">The response acceleration for a one second period, (g).</param>
        /// <param name="Fa">The site coefficient Fa.</param>
        /// <param name="Fv">The site coefficient Fv.</param>
        /// <param name="Cd">The deflection amplification factor.</param>
        /// <param name="seismicCoefficientSource">The seismic coefficient source.</param>
        /// <param name="latitude">The latitude for which the seismic coefficients are obtained.
        /// This only applies when <paramref name="seismicCoefficientSource" /> = <see cref="eSeismicCoefficient_IBC_2006.SsAndS1FromUSGSLatLong" /> or <see cref="eSeismicCoefficient_IBC_2006.SsAndS1FromUSGSZipCode" />.</param>
        /// <param name="longitude">The longitude for which the seismic coefficients are obtained.
        /// This only applies when <paramref name="seismicCoefficientSource" /> = <see cref="eSeismicCoefficient_IBC_2006.SsAndS1FromUSGSLatLong" /> or <see cref="eSeismicCoefficient_IBC_2006.SsAndS1FromUSGSZipCode" />.</param>
        /// <param name="zipCode">The zip code for which the seismic coefficients are obtained.
        /// This only applies when <paramref name="seismicCoefficientSource" /> = <see cref="eSeismicCoefficient_IBC_2006.SsAndS1FromUSGSZipCode" />.</param>
        /// <param name="TL">The long-period transition period. [s].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref eCtType_IBC_2006 CtType,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_IBC_2006 siteClass,
            ref double omega,
            ref double R,
            ref double I,
            ref double Ss,
            ref double S1,
            ref double Fa,
            ref double Fv,
            ref double Cd,
            ref eSeismicCoefficient_IBC_2006 seismicCoefficientSource,
            ref double latitude,
            ref double longitude,
            ref string zipCode,
            ref double TL)
        {
            int csiLoadDirection = 0;
            int csiPeriodOption = 0;
            int csiSiteClass = 0;
            int csiSeismicCoefficientSource = 0;
            int csiCtType = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetIBC2006(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref csiPeriodOption,
                            ref csiCtType,
                            ref userSpecifiedPeriod,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref R,
                            ref omega,
                            ref Cd,
                            ref I,
                            ref csiSeismicCoefficientSource,
                            ref latitude,
                            ref longitude,
                            ref zipCode,
                            ref Ss,
                            ref S1,
                            ref TL,
                            ref csiSiteClass,
                            ref Fa,
                            ref Fv);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            periodOption = (eTimePeriodOption)csiPeriodOption;
            siteClass = (eSiteClass_IBC_2006)csiSiteClass;
            seismicCoefficientSource = (eSeismicCoefficient_IBC_2006)csiSeismicCoefficientSource;
            CtType = (eCtType_IBC_2006)csiCtType;
        }


        /// <summary>
        /// This function assigns auto seismic loading parameters for the 2006 IBC code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="CtType">The values of Ct and x.
        /// This only applies when <paramnamef name="periodOption" /> = <see cref="eTimePeriodOption.Approximate" /> or <see cref="eTimePeriodOption.ProgramCalculated" />.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramnamef name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" /></param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="omega">The system overstrength factor.</param>
        /// <param name="R">The response modification factor.</param>
        /// <param name="I">The importance factor.</param>
        /// <param name="Ss">The response acceleration for short periods, (g).</param>
        /// <param name="S1">The response acceleration for a one second period, (g).</param>
        /// <param name="Fa">The site coefficient Fa.</param>
        /// <param name="Fv">The site coefficient Fv.</param>
        /// <param name="Cd">The deflection amplification factor.</param>
        /// <param name="seismicCoefficientSource">The seismic coefficient source.</param>
        /// <param name="latitude">The latitude for which the seismic coefficients are obtained.
        /// This only applies when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSeismicCoefficient_IBC_2006.SsAndS1FromUSGSLatLong" /> or <see cref="eSeismicCoefficient_IBC_2006.SsAndS1FromUSGSZipCode" />.</param>
        /// <param name="longitude">The longitude for which the seismic coefficients are obtained.
        /// This only applies when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSeismicCoefficient_IBC_2006.SsAndS1FromUSGSLatLong" /> or <see cref="eSeismicCoefficient_IBC_2006.SsAndS1FromUSGSZipCode" />.</param>
        /// <param name="zipCode">The zip code for which the seismic coefficients are obtained.
        /// This only applies when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSeismicCoefficient_IBC_2006.SsAndS1FromUSGSZipCode" />.</param>
        /// <param name="TL">The long-period transition period. [s].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            eCtType_IBC_2006 CtType,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_IBC_2006 siteClass,
            double omega,
            double R,
            double I,
            double Ss,
            double S1,
            double Fa,
            double Fv,
            double Cd,
            eSeismicCoefficient_IBC_2006 seismicCoefficientSource,
            double latitude,
            double longitude,
            string zipCode,
            double TL)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetIBC2006(name,
                            (int)loadDirection,
                            eccentricity,
                            (int)periodOption,
                            (int)CtType,
                            userSpecifiedPeriod,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            R,
                            omega,
                            Cd,
                            I,
                            (int)seismicCoefficientSource,
                            latitude,
                            longitude,
                            zipCode,
                            Ss,
                            S1,
                            TL,
                            (int)siteClass,
                            Fa,
                            Fv);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

    }
}