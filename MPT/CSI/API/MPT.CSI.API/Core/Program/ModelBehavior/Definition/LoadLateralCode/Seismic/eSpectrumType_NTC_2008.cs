// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSpectrumType_NTC_2008.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The spectrum type for NTC 2008 seismic lateral code forces.
    /// </summary>
    public enum eSpectrumType_NTC_2008
    {
        /// <summary>
        /// Elastic horizontal spectrum type.
        /// </summary>
        ElasticHorizontal = 1,

        /// <summary>
        /// Elastic vertical spectrum type.
        /// </summary>
        ElasticVertical = 2,

        /// <summary>
        /// Design horizontal spectrum type.
        /// </summary>
        DesignHorizontal = 3,

        /// <summary>
        /// Design vertical spectrum type.
        /// </summary>
        DesignVertical = 4,
    }
}
#endif