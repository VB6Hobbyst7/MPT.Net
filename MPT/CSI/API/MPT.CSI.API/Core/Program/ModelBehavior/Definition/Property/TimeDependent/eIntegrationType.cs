// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eIntegrationType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent
{
    /// <summary>
    /// The integration type used.
    /// </summary>
    public enum eIntegrationType
    {
        /// <summary>
        /// Full integeration.
        /// </summary>
        FullIntegeration = 0,

        /// <summary>
        /// Dirichlet series.
        /// </summary>
        DirichletSeries = 1
    }
}
#endif
