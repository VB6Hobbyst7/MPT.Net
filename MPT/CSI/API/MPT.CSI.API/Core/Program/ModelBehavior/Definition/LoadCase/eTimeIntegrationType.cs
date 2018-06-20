// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="eTimeIntegrationType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Time integration types available in the application.
    /// </summary>
    public enum eTimeIntegrationType
    {
        /// <summary>
        /// The Newmark time integration type.
        /// </summary>
        Newmark = 1,

        /// <summary>
        /// The Wilson time integration type.
        /// </summary>
        Wilson = 2,

        /// <summary>
        /// The Collocation time integration type.
        /// </summary>
        Collocation = 3,

        /// <summary>
        /// The Hilber-Hughes-YTaylor time integration type.
        /// </summary>
        HilberHughesTaylor = 4,

        /// <summary>
        /// The Chung and Hulbert time integration type.
        /// </summary>
        ChungHulbert = 5
    }
}
#endif