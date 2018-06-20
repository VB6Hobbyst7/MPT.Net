// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSpectrumDirection_JTG_B02_2013.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The direction to which the JTG B02-2013 response spectrum is applied.
    /// </summary>
    public enum eSpectrumDirection_JTG_B02_2013
    {
        /// <summary>
        /// Response spectrum is applied in the horizontal direction.
        /// </summary>
        Horizontal = 1,

        /// <summary>
        /// Response spectrum is applied in the vertical direction on rock.
        /// </summary>
        VerticalRock = 2,

        /// <summary>
        /// Response spectrum is applied in the vertical direction on soil.
        /// </summary>
        VerticalSoil = 3,
    }
}
#endif
