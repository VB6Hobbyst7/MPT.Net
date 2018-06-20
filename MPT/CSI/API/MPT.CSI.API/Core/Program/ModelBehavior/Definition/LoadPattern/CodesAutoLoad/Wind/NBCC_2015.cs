// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="NBCC_2015.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v19 || BUILD_SAP2000v20 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    /// <summary>
    /// Represents the UBC_94 auto wind load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind.AutoWindLoad" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class NBCC_2015 : AutoWindLoad
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NBCC_2015" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public NBCC_2015(CSiApiSeed seed) : base(seed) { }


#endregion

#region Methods: Public
        /// <summary>
        /// This function retrieves auto wind loading parameters for NBCC 2015.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load case with an auto wind assignment.</param>
        /// <param name="exposureFrom">The source of the wind exposure.</param>
        /// <param name="directionAngle">The direction angle for the wind load. This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpw">The windward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpl">The leeward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="NBCCCase">This is 1, 2, 3, 4, or 5, indicating the desired case from NBCC 2105 Figure A-4.1.7.9(1).
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
        /// <param name="q">The velocity pressure in kPa.</param>
        /// <param name="gustFactor">The gust factor.</param>
        /// <param name="topographicFactor">The topographic factor.</param>
        /// <param name="importanceFactor">The importance factor.</param>
        /// <param name="terrainType">Type of the terrain.</param>
        /// <param name="CeWindward">The windward exposure factor, Ce.
        /// This item applies only when <paramref name="terrainType" /> = <see cref="eTerrainType.User" />.</param>
        /// <param name="CeLeeward">The windward exposure factor, Ce.
        /// This item applies only when <paramref name="terrainType" /> = <see cref="eTerrainType.User" />.</param>
        /// <param name="userSpecifiedExposure">True: Wind exposure widths are provided by the user.
        /// Else, wind exposure widths are calculated by the program from the extents of the diaphragms.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eWindExposure exposureFrom,
            ref double directionAngle,
            ref double Cpw,
            ref double Cpl,
            ref int NBCCCase,
            ref double e1,
            ref double e2,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double q,
            ref double gustFactor,
            ref double topographicFactor,
            ref double importanceFactor,
            ref eTerrainType terrainType,
            ref double CeWindward,
            ref double CeLeeward,
            ref bool userSpecifiedExposure)
        {
            int csiExposureFrom = 0;
            int csiTerrainType = 0;

            _callCode = _sapModel.LoadPatterns.AutoWind.GetNBCC2015(name,
                            ref csiExposureFrom,
                            ref directionAngle,
                            ref Cpw,
                            ref Cpl,
                            ref NBCCCase,
                            ref e1,
                            ref e2,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref q,
                            ref gustFactor,
                            ref topographicFactor,
                            ref importanceFactor,
                            ref csiTerrainType,
                            ref CeWindward,
                            ref CeLeeward,
                            ref userSpecifiedExposure);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            exposureFrom = (eWindExposure)csiExposureFrom;
            terrainType = (eTerrainType)csiTerrainType;
        }


        /// <summary>
        /// This function assigns auto wind loading parameters for NBCC 2015.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load case with an auto wind assignment.</param>
        /// <param name="exposureFrom">The source of the wind exposure.</param>
        /// <param name="directionAngle">The direction angle for the wind load.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpw">The windward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpl">The leeward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="NBCCCase">This is 1, 2, 3, 4, or 5, indicating the desired case from NBCC 2105 Figure A-4.1.7.9(1).
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
        /// <param name="q">The velocity pressure in kPa.</param>
        /// <param name="gustFactor">The gust factor.</param>
        /// <param name="topographicFactor">The topographic factor.</param>
        /// <param name="importanceFactor">The importance factor.</param>
        /// <param name="terrainType">Type of the terrain.</param>
        /// <param name="CeWindward">The windward exposure factor, Ce.
        /// This item applies only when <paramref name="terrainType" /> = <see cref="eTerrainType.User" />.</param>
        /// <param name="CeLeeward">The windward exposure factor, Ce.
        /// This item applies only when <paramref name="terrainType" /> = <see cref="eTerrainType.User" />.</param>
        /// <param name="userSpecifiedExposure">True: Wind exposure widths are provided by the user.
        /// Else, wind exposure widths are calculated by the program from the extents of the diaphragms.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eWindExposure exposureFrom,
            double directionAngle,
            double Cpw,
            double Cpl,
            int NBCCCase,
            double e1,
            double e2,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double q,
            double gustFactor,
            double topographicFactor,
            double importanceFactor,
            eTerrainType terrainType,
            double CeWindward,
            double CeLeeward,
            bool userSpecifiedExposure = false)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.SetNBCC2015(name,
                            (int)exposureFrom,
                            directionAngle,
                            Cpw,
                            Cpl,
                            NBCCCase,
                            e1,
                            e2,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            q,
                            gustFactor,
                            topographicFactor,
                            importanceFactor,
                            (int)terrainType,
                            CeWindward,
                            CeLeeward,
                            userSpecifiedExposure);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif