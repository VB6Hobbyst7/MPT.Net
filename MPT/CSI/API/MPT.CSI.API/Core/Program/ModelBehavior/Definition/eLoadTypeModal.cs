// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eLoadTypeModal.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Load type to set in modal load cases.
    /// </summary>
    public enum eLoadTypeModal
    {
        /// <summary>
        /// Load is applied as a force.
        /// </summary>
        Load = 1,

        /// <summary>
        /// Load is applied as an acceleration to the mass.
        /// </summary>
        Accel = 2,

        /// <summary>
        /// Load is applied as a link force.
        /// </summary>
        Link = 3
    }
}
#endif