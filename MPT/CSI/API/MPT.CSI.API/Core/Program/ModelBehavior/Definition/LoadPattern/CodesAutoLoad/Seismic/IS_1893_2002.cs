// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="IS_1893_2002.cs" company="">
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
    public class IS_1893_2002 : AutoSeismicLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="IS_1893_2002" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public IS_1893_2002(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for the 2002 IS1893 code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="Ct">The code-specified Ct factor. [L].
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
        /// <param name="R">The response modification factor.</param>
        /// <param name="I">The importance factor.</param>
        /// <param name="ZOption">The method of input for the seismic zone factor.</param>
        /// <param name="Z">The seismic zone factor, Z.
        /// If <paramref name="ZOption" /> = <see cref="eSource.PerCode" />, this item should be one of the following: 0.10, 0.16, 0.24, 0.36.</param>
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
            ref eSiteClass_IS_1893_2002 soilType,
            ref double R,
            ref double I,
            ref eSource ZOption,
            ref double Z)
        {
            int csiLoadDirection = 0;
            int csiPeriodOption = 0;
            int csiSoilType = 0;
            int csiSeismicZoneOption = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetIS1893_2002(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref csiPeriodOption,
                            ref Ct,
                            ref userSpecifiedPeriod,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref csiSeismicZoneOption,
                            ref Z,
                            ref csiSoilType,
                            ref I,
                            ref R);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            periodOption = (eTimePeriodOption)csiPeriodOption;
            soilType = (eSiteClass_IS_1893_2002)csiSoilType;
            ZOption = (eSource)csiSeismicZoneOption;
        }


        /// <summary>
        /// This function assigns auto seismic loading parameters for the 2002 IS1893 code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="Ct">The code-specified Ct factor. [L].
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
        /// <param name="R">The response modification factor.</param>
        /// <param name="I">The importance factor.</param>
        /// <param name="ZOption">The method of input for the seismic zone factor.</param>
        /// <param name="Z">The seismic zone factor, Z.
        /// If <paramnamef name="ZOption" /> = <see cref="eSource.PerCode" />, this item should be one of the following: 0.10, 0.16, 0.24, 0.36.</param>
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
            eSiteClass_IS_1893_2002 soilType,
            double R,
            double I,
            eSource ZOption,
            double Z)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetIS1893_2002(name,
                            (int)loadDirection,
                            eccentricity,
                            (int)periodOption,
                            Ct,
                            userSpecifiedPeriod,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            (int)ZOption,
                            Z,
                            (int)soilType,
                            I,
                            R);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif
