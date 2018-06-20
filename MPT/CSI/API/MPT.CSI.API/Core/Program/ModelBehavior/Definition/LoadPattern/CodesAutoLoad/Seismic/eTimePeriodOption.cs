// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="eTimePeriodOption.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Indicates the time period option used in auto seismic loads.
    /// </summary>
    public enum eTimePeriodOption
    {
        /// <summary>
        /// Approximate.
        /// </summary>
        Approximate = 1,

        /// <summary>
        /// Program calculated.
        /// </summary>
        ProgramCalculated = 2,

        /// <summary>
        /// User defined.
        /// </summary>
        UserDefined = 3
    }
}
