// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="NEHRP_97.cs" company="">
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
    public class NEHRP_97 : AutoSeismicLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NEHRP_97" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public NEHRP_97(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for the 1997 NEHRP code.
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
        /// <param name="siteClass">The site class.
        /// This only applies when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" />.</param>
        /// <param name="R">The response modification factor.</param>
        /// <param name="Ss">The response acceleration for short periods, (g).</param>
        /// <param name="S1">The response acceleration for a one second period, (g).</param>
        /// <param name="Fa">The site coefficient Fa.</param>
        /// <param name="Fv">The site coefficient Fv.</param>
        /// <param name="seismicCoefficientSource">The seismic coefficient source.</param>
        /// <param name="Sg">The seismic group.</param>
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
            ref eSiteClass_NEHRP_1997 siteClass,
            ref double R,
            ref double Ss,
            ref double S1,
            ref double Fa,
            ref double Fv,
            ref eSource seismicCoefficientSource,
            ref eSeismicGroup_NEHRP_1997 Sg)
        {
            int csiLoadDirection = 0;
            int csiPeriodOption = 0;
            int csiSiteClass = 0;
            int csiSeismicCoefficientSource = 0;
            int csiSg = 0;


            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetNEHRP97(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref csiPeriodOption,
                            ref Ct,
                            ref userSpecifiedPeriod,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref csiSg,
                            ref csiSeismicCoefficientSource,
                            ref csiSiteClass,
                            ref Ss,
                            ref S1,
                            ref Fa,
                            ref Fv,
                            ref R);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            periodOption = (eTimePeriodOption)csiPeriodOption;
            siteClass = (eSiteClass_NEHRP_1997)csiSiteClass;
            seismicCoefficientSource = (eSource)csiSeismicCoefficientSource;
            Sg = (eSeismicGroup_NEHRP_1997)csiSg;
        }


        /// <summary>
        /// This function assigns auto seismic loading parameters for the 1997 NEHRP code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="Ct">The code-specified Ct factor.
        /// This only applies when <paramref name="periodOption" /> = <see cref="eTimePeriodOption.Approximate" /> or <see cref="eTimePeriodOption.ProgramCalculated" />.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramnamef name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" /></param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="siteClass">The site class.
        /// This only applies when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" />.</param>
        /// <param name="R">The response modification factor.</param>
        /// <param name="Ss">The response acceleration for short periods, (g).</param>
        /// <param name="S1">The response acceleration for a one second period, (g).</param>
        /// <param name="Fa">The site coefficient Fa.</param>
        /// <param name="Fv">The site coefficient Fv.</param>
        /// <param name="seismicCoefficientSource">The seismic coefficient source.</param>
        /// <param name="Sg">The seismic group.</param>
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
            eSiteClass_NEHRP_1997 siteClass,
            double R,
            double Ss,
            double S1,
            double Fa,
            double Fv,
            eSource seismicCoefficientSource,
            eSeismicGroup_NEHRP_1997 Sg)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetNEHRP97(name,
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
                            R);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif
