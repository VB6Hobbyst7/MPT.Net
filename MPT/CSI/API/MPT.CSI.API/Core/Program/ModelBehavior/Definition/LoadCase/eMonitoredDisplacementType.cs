// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="eMonitoredDisplacementType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Monitored displacement load control typs available in the application.
    /// </summary>
    public enum eMonitoredDisplacementType
    {
        /// <summary>
        /// Displacement at a specified point object.
        /// </summary>
        AtSpecifiedPoint = 1,

        /// <summary>
        /// Generalized displacement.
        /// </summary>
        GeneralizedDisplacement = 2
    }
}