// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="ASCE_7_02.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    /// <summary>
    /// Represemts the ASCE_7_02 auto wind load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind.AutoWindLoad" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class ASCE_7_02 : AutoWindLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="ASCE_7_02" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public ASCE_7_02(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto wind loading parameters for ASCE 7-02.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load case with an auto wind assignment.</param>
        /// <param name="exposureFrom">The source of the wind exposure.</param>
        /// <param name="directionAngle">The direction angle for the wind load.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpw">The windward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpl">The leeward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="ASCECase">This is 1, 2, 3, 4 or 5, indicating the desired case from ASCE7-02 Figure 6-9. 1, 2, 3 and 4 refer to cases 1 through 4 in the figure.
        /// 5 means to create all cases.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="e1">Value e1 in ASCE7-02 Figure 6-9.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="e2">Value e2 in ASCE7-02 Figure 6-9.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the wind load are user specified.
        /// Else, the elevations are determined by the program.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto wind loads are applied. [L].</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto wind loads are applied. [L].</param>
        /// <param name="windSpeed">Design reference wind speed in miles per hour.</param>
        /// <param name="exposureType">Exposure category.</param>
        /// <param name="importanceFactor">The importance factor.</param>
        /// <param name="Kzt">The topographical factor.</param>
        /// <param name="gustFactor">The gust factor.</param>
        /// <param name="Kd">The directionality factor.</param>
        /// <param name="solidGrossRatio">The solid area divided by gross area ratio for open frame structure loading.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.FrameObjects" /> or <see cref="eWindExposure.AreaAndFrameObjects" />.</param>
        /// <param name="userSpecifiedExposure">True: Wind exposure widths are provided by the user.
        /// Else, wind exposure widths are calculated by the program from the extents of the diaphragms.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eWindExposure exposureFrom,
            ref double directionAngle,
            ref double Cpw,
            ref double Cpl,
            ref int ASCECase,
            ref double e1,
            ref double e2,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double windSpeed,
            ref eExposureTypeAToD exposureType,
            ref double importanceFactor,
            ref double Kzt,
            ref double gustFactor,
            ref double Kd,
            ref double solidGrossRatio,
            ref bool userSpecifiedExposure)
        {
            int csiExposureFrom = 0;
            int csiExposureType = 0;

            _callCode = _sapModel.LoadPatterns.AutoWind.GetASCE702(name,
                            ref csiExposureFrom,
                            ref directionAngle,
                            ref Cpw,
                            ref Cpl,
                            ref ASCECase,
                            ref e1,
                            ref e2,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref windSpeed,
                            ref csiExposureType,
                            ref importanceFactor,
                            ref Kzt,
                            ref gustFactor,
                            ref Kd,
                            ref solidGrossRatio,
                            ref userSpecifiedExposure);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            exposureFrom = (eWindExposure)csiExposureFrom;
            exposureType = (eExposureTypeAToD)csiExposureType;
        }


        /// <summary>
        /// This function assigns auto wind loading parameters for ASCE 7-02.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load case with an auto wind assignment.</param>
        /// <param name="exposureFrom">The source of the wind exposure.</param>
        /// <param name="directionAngle">The direction angle for the wind load.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpw">The windward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpl">The leeward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="ASCECase">This is 1, 2, 3, 4 or 5, indicating the desired case from ASCE7-02 Figure 6-9. 1, 2, 3 and 4 refer to cases 1 through 4 in the figure.
        /// 5 means to create all cases.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="e1">Value e1 in ASCE7-02 Figure 6-9.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="e2">Value e2 in ASCE7-02 Figure 6-9.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the wind load are user specified.
        /// Else, the elevations are determined by the program.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto wind loads are applied. [L].</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto wind loads are applied. [L].</param>
        /// <param name="windSpeed">Design reference wind speed in miles per hour.</param>
        /// <param name="exposureType">Exposure category.</param>
        /// <param name="importanceFactor">The importance factor.</param>
        /// <param name="Kzt">The topographical factor.</param>
        /// <param name="gustFactor">The gust factor.</param>
        /// <param name="Kd">The directionality factor.</param>
        /// <param name="solidGrossRatio">The solid area divided by gross area ratio for open frame structure loading.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.FrameObjects" /> or <see cref="eWindExposure.AreaAndFrameObjects" />.</param>
        /// <param name="userSpecifiedExposure">True: Wind exposure widths are provided by the user.
        /// Else, wind exposure widths are calculated by the program from the extents of the diaphragms.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eWindExposure exposureFrom,
            double directionAngle,
            double Cpw,
            double Cpl,
            int ASCECase,
            double e1,
            double e2,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double windSpeed,
            eExposureTypeAToD exposureType,
            double importanceFactor,
            double Kzt,
            double gustFactor,
            double Kd,
            double solidGrossRatio = 0.2,
            bool userSpecifiedExposure = false)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.SetASCE702(name,
                            (int)exposureFrom,
                            directionAngle,
                            Cpw,
                            Cpl,
                            ASCECase,
                            e1,
                            e2,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            windSpeed,
                            (int)exposureType,
                            importanceFactor,
                            Kzt,
                            gustFactor,
                            Kd,
                            solidGrossRatio,
                            userSpecifiedExposure);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif
