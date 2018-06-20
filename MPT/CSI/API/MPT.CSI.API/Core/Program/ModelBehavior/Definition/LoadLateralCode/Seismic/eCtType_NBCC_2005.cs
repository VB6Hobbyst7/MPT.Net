// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eCtType_NBCC_2005.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The structure type for NBCC 2005 seismic lateral code forces.
    /// </summary>
    public enum eCtType_NBCC_2005
    {
        /// <summary>
        /// Steel moment frame structure.
        /// </summary>
        SteelMomentFrame = 0,

        /// <summary>
        /// Concrete moment frame structure.
        /// </summary>
        ConcreteMomentFrame = 1,


        /// <summary>
        /// All other moment frame structures,
        /// </summary>
        OtherMomentFrame = 2,

        /// <summary>
        /// Braced frame structure.
        /// </summary>
        BracedFrame = 3,

        /// <summary>
        /// Shear wall structure.
        /// </summary>
        ShearWall = 4
    }
}
#endif