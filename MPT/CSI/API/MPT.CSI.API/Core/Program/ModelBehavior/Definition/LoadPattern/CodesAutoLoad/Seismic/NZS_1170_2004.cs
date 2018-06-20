// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="NZS_1170_2004.cs" company="">
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
    public class NZS_1170_2004 : AutoSeismicLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NZS_1170_2004" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public NZS_1170_2004(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for the NZS 1170 2004 code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramref name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" /></param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="R">The return period factor, R.</param>
        /// <param name="Z">The hazard factor, Z.</param>
        /// <param name="distanceToSeismicSource">Distance to the fault in km, used to calculate the near fault factor.</param>
        /// <param name="Sp">The structural performance factor, Sp.</param>
        /// <param name="Mu">The structural ductility factor, u.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_NZS_1170_2004 siteClass,
            ref double R,
            ref double Z,
            ref double distanceToSeismicSource,
            ref double Sp,
            ref double Mu)
        {
            int csiLoadDirection = 0;
            int csiPeriodOption = 0;
            int csiSiteClass = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetNZS11702004(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref csiPeriodOption,
                            ref userSpecifiedPeriod,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref csiSiteClass,
                            ref Z,
                            ref R,
                            ref distanceToSeismicSource,
                            ref Sp,
                            ref Mu);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            periodOption = (eTimePeriodOption)csiPeriodOption;
            siteClass = (eSiteClass_NZS_1170_2004)csiSiteClass;
        }


        /// <summary>
        /// This function assigns auto seismic loading parameters for the NZS 1170 2004 code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramnamef name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" /></param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="R">The return period factor, R.</param>
        /// <param name="Z">The hazard factor, Z.</param>
        /// <param name="distanceToSeismicSource">Distance to the fault in km, used to calculate the near fault factor.</param>
        /// <param name="Sp">The structural performance factor, Sp.</param>
        /// <param name="Mu">The structural ductility factor, u.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_NZS_1170_2004 siteClass,
            double R,
            double Z,
            double distanceToSeismicSource,
            double Sp,
            double Mu)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetNZS11702004(name,
                            (int)loadDirection,
                            eccentricity,
                            (int)periodOption,
                            userSpecifiedPeriod,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            (int)siteClass,
                            Z,
                            R,
                            distanceToSeismicSource,
                            Sp,
                            Mu);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif
