// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="UBC_97.cs" company="">
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
    /// Represents the UBC_97 auto seismic load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic.AutoSeismicLoad" />
    /// <seealso cref="AutoSeismicLoad" />
    public class UBC_97 : AutoSeismicLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="UBC_97" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public UBC_97(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for the 1997 UBC code.
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
        /// <param name="soilType">Type of the soil.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /></param>
        /// <param name="R">The overstrength factor.</param>
        /// <param name="I">The importance factor.</param>
        /// <param name="Z">This is 0.075, 0.15, 0.2, 0.3 or 0.4, indicating the seismic zone factor.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /></param>
        /// <param name="seismicCoefficientSource">The seismic coefficient source.</param>
        /// <param name="Ca">The seismic coefficient, Ca.</param>
        /// <param name="Cv">The seismic coefficient, Cv.</param>
        /// <param name="nearSourceSource">Indicates the source  of the near source factor coefficients Na and Nv.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramref name="Z" /> = 0.4.</param>
        /// <param name="seismicSourceType">Type of the seismic source.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramref name="Z" /> = 0.4 and <paramref name="nearSourceSource" /> = <see cref="eSource.PerCode" />.</param>
        /// <param name="distanceToSeismicSource">This is the distance to the seismic source in kilometers.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramref name="Z" /> = 0.4 and <paramref name="nearSourceSource" /> = <see cref="eSource.PerCode" />.</param>
        /// <param name="Na">The near source factor coefficient, Na.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.UserDefined" />.</param>
        /// <param name="Nv">The near source factor coefficient, Nv.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.UserDefined" />.</param>
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
            ref eSiteClass_UBC_97 soilType,
            ref double R,
            ref double I,
            ref double Z,
            ref eSource seismicCoefficientSource,
            ref double Ca,
            ref double Cv,
            ref eSource nearSourceSource,
            ref eSourceType_UBC_97 seismicSourceType,
            ref double distanceToSeismicSource,
            ref double Na,
            ref double Nv)
        {
            int csiLoadDirection = 0;
            int csiPeriodOption = 0;
            int csiSoilType = 0;
            int csiSeismicCoefficientSource = 0;
            int csiNearSourceSource = 0;
            int csiSeismicSourceType = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetUBC97(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref csiPeriodOption,
                            ref Ct,
                            ref userSpecifiedPeriod,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref csiSeismicCoefficientSource,
                            ref csiSoilType,
                            ref Z,
                            ref Ca,
                            ref Cv,
                            ref csiNearSourceSource,
                            ref csiSeismicSourceType,
                            ref distanceToSeismicSource,
                            ref Na,
                            ref Nv,
                            ref I,
                            ref R);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            periodOption = (eTimePeriodOption)csiPeriodOption;
            soilType = (eSiteClass_UBC_97)csiSoilType;
            seismicCoefficientSource = (eSource)csiSeismicCoefficientSource;
            nearSourceSource = (eSource)csiNearSourceSource;
            seismicSourceType = (eSourceType_UBC_97)csiSeismicSourceType;
        }


        /// <summary>
        /// This function assigns auto seismic loading parameters for the 1997 UBC code.
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
        /// <param name="soilType">Type of the soil.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /></param>
        /// <param name="R">The overstrength factor.</param>
        /// <param name="I">The importance factor.</param>
        /// <param name="Z">This is 0.075, 0.15, 0.2, 0.3 or 0.4, indicating the seismic zone factor.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /></param>
        /// <param name="seismicCoefficientSource">The seismic coefficient source.</param>
        /// <param name="Ca">The seismic coefficient, Ca.</param>
        /// <param name="Cv">The seismic coefficient, Cv.</param>
        /// <param name="nearSourceSource">Indicates the source  of the near source factor coefficients Na and Nv.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramnamef name="Z" /> = 0.4.</param>
        /// <param name="seismicSourceType">Type of the seismic source.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramnamef name="Z" /> = 0.4 and <paramnamef name="nearSourceSource" /> = <see cref="eSource.PerCode" />.</param>
        /// <param name="distanceToSeismicSource">This is the distance to the seismic source in kilometers.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramnamef name="Z" /> = 0.4 and <paramnamef name="nearSourceSource" /> = <see cref="eSource.PerCode" />.</param>
        /// <param name="Na">The near source factor coefficient, Na.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.UserDefined" />.</param>
        /// <param name="Nv">The near source factor coefficient, Nv.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.UserDefined" />.</param>
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
            eSiteClass_UBC_97 soilType,
            double R,
            double I,
            double Z,
            eSource seismicCoefficientSource,
            double Ca,
            double Cv,
            eSource nearSourceSource,
            eSourceType_UBC_97 seismicSourceType,
            double distanceToSeismicSource,
            double Na,
            double Nv)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetUBC97(name,
                            (int)loadDirection,
                            eccentricity,
                            (int)periodOption,
                            Ct,
                            userSpecifiedPeriod,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            (int)seismicCoefficientSource,
                            (int)soilType,
                            Z,
                            Ca,
                            Cv,
                            (int)nearSourceSource,
                            (int)seismicSourceType,
                            distanceToSeismicSource,
                            Na,
                            Nv,
                            I,
                            R);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif
