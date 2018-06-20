// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="NBCC_1995.cs" company="">
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
    public class NBCC_1995 : AutoSeismicLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NBCC_1995" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public NBCC_1995(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for the 1995 NBCC code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="CtType">The structure type.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramref name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" /></param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="R">The force modification factor.</param>
        /// <param name="I">The importance factor.</param>
        /// <param name="Ds">The dimension of the lateral load resisting system in the direction of the applied forces. [L].
        /// This only applies when <paramref name="CtType" /> = <see cref="eCtType_NBCC_1995.Other" />.</param>
        /// <param name="Za">The acceleration related zone, Za.</param>
        /// <param name="Zv">The velocity related zone, Zv.</param>
        /// <param name="ZvRatioSource">How the zonal velocity ratio, V, is specified.</param>
        /// <param name="ZvRatio">The zonal velocity ratio, V.</param>
        /// <param name="F">The foundation factor.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref eCtType_NBCC_1995 CtType,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double R,
            ref double I,
            ref double Ds,
            ref eZa_NBCC_1995 Za,
            ref eZv_NBCC_1995 Zv,
            ref eSource ZvRatioSource,
            ref double ZvRatio,
            ref double F)
        {
            int csiLoadDirection = 0;
            int csiPeriodOption = 0;
            int csiCtType = 0;
            int csiZa = 0;
            int csiZv = 0;
            int csiZvRatioSource = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetNBCC95(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref csiCtType,
                            ref Ds,
                            ref csiPeriodOption,
                            ref userSpecifiedPeriod,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref csiZa,
                            ref csiZv,
                            ref csiZvRatioSource,
                            ref ZvRatio,
                            ref I,
                            ref F,
                            ref R);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            periodOption = (eTimePeriodOption)csiPeriodOption;
            CtType = (eCtType_NBCC_1995)csiCtType;
            Za = (eZa_NBCC_1995)csiZa;
            Zv = (eZv_NBCC_1995)csiZv;
            ZvRatioSource = (eSource)csiZvRatioSource;
        }


        /// <summary>
        /// This function assigns auto seismic loading parameters for the 1995 NBCC code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="periodOption">The time period option.</param>
        /// <param name="CtType">The structure type.</param>
        /// <param name="userSpecifiedPeriod">The user specified time period. [s]
        /// This only applies when <paramnamef name="periodOption" /> = <see cref="eTimePeriodOption.UserDefined" /></param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="R">The force modification factor.</param>
        /// <param name="I">The importance factor.</param>
        /// <param name="Ds">The dimension of the lateral load resisting system in the direction of the applied forces. [L].
        /// This only applies when <paramnamef name="CtType" /> = <see cref="eCtType_NBCC_1995.Other" />.</param>
        /// <param name="Za">The acceleration related zone, Za.</param>
        /// <param name="Zv">The velocity related zone, Zv.</param>
        /// <param name="ZvRatioSource">How the zonal velocity ratio, V, is specified.</param>
        /// <param name="ZvRatio">The zonal velocity ratio, V.</param>
        /// <param name="F">The foundation factor.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            eCtType_NBCC_1995 CtType,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double R,
            double I,
            double Ds,
            eZa_NBCC_1995 Za,
            eZv_NBCC_1995 Zv,
            eSource ZvRatioSource,
            double ZvRatio,
            double F)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetNBCC95(name,
                            (int)loadDirection,
                            eccentricity,
                            (int)CtType,
                            Ds,
                            (int)periodOption,
                            userSpecifiedPeriod,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            (int)Za,
                            (int)Zv,
                            (int)ZvRatioSource,
                            ZvRatio,
                            I,
                            F,
                            R);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif
