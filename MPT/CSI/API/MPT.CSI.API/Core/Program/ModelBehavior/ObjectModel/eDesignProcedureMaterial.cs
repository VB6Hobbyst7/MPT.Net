// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-24-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eDesignProcedureMaterial.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Design procedure types by material available in the application.
    /// </summary>
    public enum eDesignProcedureType
    {
        /// <summary>
        /// STeel design.
        /// </summary>
        Steel = 1,

        /// <summary>
        /// Concrete design.
        /// </summary>
        Concrete = 2,

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Aluminum design.
        /// </summary>
        Aluminum = 7,

        /// <summary>
        /// Cold formed steel design.
        /// </summary>
        ColdFormed = 8,
#endif

        /// <summary>
        /// No design.
        /// </summary>
        NoDesign = 9
    }
}