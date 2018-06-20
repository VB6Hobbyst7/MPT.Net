// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eLevel_Italian_3274.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The spectral level, direction and building type for Italian 3274 response spectrum function.
    /// </summary>
    public enum eLevel_Italian_3274
    {
        /// <summary>
        /// SLU/Horizontal/Building. (SLU = ultimate strength design).
        /// </summary>
        SLU_H_Building = 0,

        /// <summary>
        /// SLU/Horizontal/Bridge. (SLU = ultimate strength design).
        /// </summary>
        SLU_H_Bridge = 1,

        /// <summary>
        /// SLU/Vertical/Building. (SLU = ultimate strength design).
        /// </summary>
        SLU_V_Building = 2,

        /// <summary>
        /// SLU/Vertical/Bridge. (SLU = ultimate strength design).
        /// </summary>
        SLU_V_Bridge = 3,

        /// <summary>
        /// EL/Horizontal/Building. (EL = elastic design).
        /// </summary>
        EL_H_Building = 4,

        /// <summary>
        /// EL/Horizontal/Bridge. (EL = elastic design).
        /// </summary>
        EL_H_Bridge = 5,

        /// <summary>
        /// EL/Vertical/Building. (EL = elastic design).
        /// </summary>
        EL_V_Building = 6,

        /// <summary>
        /// EL/Vertical/Bridge. (EL = elastic design).
        /// </summary>
        EL_V_Bridge = 7,
    }
}
#endif