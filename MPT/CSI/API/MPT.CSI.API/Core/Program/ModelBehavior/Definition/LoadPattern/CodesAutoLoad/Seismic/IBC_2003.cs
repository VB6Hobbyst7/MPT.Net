// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="IBC_2003.cs" company="">
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
    /// Represents the IBC_2003 auto seismic load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic.AutoSeismicLoad" />
    /// <seealso cref="AutoSeismicLoad" />
    public class IBC_2003 : AutoSeismicLoad
    {

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="IBC_2003" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public IBC_2003(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for the 2003 IBC code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="Ct">The code-specified Ct factor. [L]
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
        /// <param name="Ss">The response acceleration for short periods, (g).</param>
        /// <param name="S1">The response acceleration for a one second period, (g).</param>
        /// <param name="seismicCoefficientSource">The seismic coefficient source.</param>
        /// <param name="Fa">The site coefficient Fa.</param>
        /// <param name="Fv">The site coefficient Fv.</param>
        /// <param name="Cd">The deflection amplification factor.</param>
        /// <param name="Sg">The sg.</param>
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
            ref eSiteClass_IBC_2003 siteClass,
            ref double omega,
            ref double R,
            ref double Ss,
            ref double S1,
            ref eSource seismicCoefficientSource,
            ref double Fa,
            ref double Fv,
            ref double Cd,
            ref eSeismicGroup_IBC_2003 Sg)
        {
            int csiLoadDirection = 0;
            int csiPeriodOption = 0;
            int csiSiteClass = 0;
            int csiSeismicCoefficientSource = 0;
            int csiSeismicGroup = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetIBC2003(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref csiPeriodOption,
                            ref Ct,
                            ref userSpecifiedPeriod,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref csiSeismicGroup,
                            ref csiSeismicCoefficientSource,
                            ref csiSiteClass,
                            ref Ss,
                            ref S1,
                            ref Fa,
                            ref Fv,
                            ref R,
                            ref omega,
                            ref Cd);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            periodOption = (eTimePeriodOption)csiPeriodOption;
            siteClass = (eSiteClass_IBC_2003)csiSiteClass;
            seismicCoefficientSource = (eSource)csiSeismicCoefficientSource;
            Sg = (eSeismicGroup_IBC_2003)csiSeismicGroup;
        }

        /// <summary>
        /// This function assigns auto seismic loading parameters for the 2003 IBC code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="Ct">The code-specified Ct factor. [L]
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
        /// <param name="Ss">The response acceleration for short periods, (g).</param>
        /// <param name="S1">The response acceleration for a one second period, (g).</param>
        /// <param name="seismicCoefficientSource">The seismic coefficient source.</param>
        /// <param name="Fa">The site coefficient Fa.</param>
        /// <param name="Fv">The site coefficient Fv.</param>
        /// <param name="Cd">The deflection amplification factor.</param>
        /// <param name="Sg">The sg.</param>
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
            eSiteClass_IBC_2003 siteClass,
            double omega,
            double R,
            double Ss,
            double S1,
            eSource seismicCoefficientSource,
            double Fa,
            double Fv,
            double Cd,
            eSeismicGroup_IBC_2003 Sg)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetIBC2003(name,
                            (int)loadDirection,
                            eccentricity,
                            (int)periodOption,
                            Ct,
                            userSpecifiedPeriod,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            (int)Sg,
                            (int)seismicCoefficientSource,
                            (int)siteClass,
                            Ss,
                            S1,
                            Fa,
                            Fv,
                            R,
                            omega,
                            Cd);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion
    }
}
#endif
