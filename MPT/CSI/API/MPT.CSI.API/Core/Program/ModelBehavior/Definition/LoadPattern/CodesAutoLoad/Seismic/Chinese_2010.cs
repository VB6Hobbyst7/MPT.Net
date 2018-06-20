// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="Chinese_2010.cs" company="">
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
    public class Chinese_2010 : AutoSeismicLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Chinese_2010" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Chinese_2010(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for the Chinese 2010 code.
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
        /// <param name="alphaMax">The maximum influence factor.</param>
        /// <param name="seismicIntensity">The seismic intensity.</param>
        /// <param name="damping">The damping ratio.</param>
        /// <param name="Tg">The characteristic ground period. [s].</param>
        /// <param name="periodTimeDiscount">The period time discount factor.</param>
        /// <param name="enhancementFactor">The enhancement factor.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double alphaMax,
            ref eSeismicIntensity_Chinese_2010 seismicIntensity,
            ref double damping,
            ref double Tg,
            ref double periodTimeDiscount,
            ref double enhancementFactor)
        {
            int csiLoadDirection = 0;
            int csiPeriodOption = 0;
            int csiSeismicIntensity = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetChinese2010(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref csiPeriodOption,
                            ref userSpecifiedPeriod,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref alphaMax,
                            ref csiSeismicIntensity,
                            ref damping,
                            ref Tg,
                            ref periodTimeDiscount,
                            ref enhancementFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            periodOption = (eTimePeriodOption)csiPeriodOption;
            seismicIntensity = (eSeismicIntensity_Chinese_2010)csiSeismicIntensity;
        }


        /// <summary>
        /// This function assigns auto seismic loading parameters for the Chinese 2010 code.
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
        /// <param name="alphaMax">The maximum influence factor.</param>
        /// <param name="seismicIntensity">The seismic intensity.</param>
        /// <param name="damping">The damping ratio.</param>
        /// <param name="Tg">The characteristic ground period. [s].</param>
        /// <param name="periodTimeDiscount">The period time discount factor.</param>
        /// <param name="enhancementFactor">The enhancement factor.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double alphaMax,
            eSeismicIntensity_Chinese_2010 seismicIntensity,
            double damping,
            double Tg,
            double periodTimeDiscount,
            double enhancementFactor)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetChinese2010(name,
                            (int)loadDirection,
                            eccentricity,
                            (int)periodOption,
                            userSpecifiedPeriod,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            alphaMax,
                            (int)seismicIntensity,
                            damping,
                            Tg,
                            periodTimeDiscount,
                            enhancementFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif
