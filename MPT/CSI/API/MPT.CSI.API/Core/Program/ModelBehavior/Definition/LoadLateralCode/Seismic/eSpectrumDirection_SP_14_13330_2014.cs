// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSpectrumDirection_SP_14_13330_2014.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The direction to which the SP 14.13330.2014 response spectrum is applied.
    /// </summary>
    public enum eSpectrumDirection_SP_14_13330_2014
    {
        /// <summary>
        /// Response spectrum is applied in the horizontal direction to a building.
        /// </summary>
        BuildingHorizontal = 1,

        /// <summary>
        /// Response spectrum is applied in the vertical direction to a building.
        /// </summary>
        BuildingVertical = 2,

        /// <summary>
        /// Response spectrum is applied in the horizontal direction to a bridge.
        /// </summary>
        BridgeHorizontal = 3,

        /// <summary>
        /// Response spectrum is applied in the vertical direction to a bridge.
        /// </summary>
        BridgeVertical = 4,
    }
}
#endif  
