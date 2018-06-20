// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="eFileValueType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Value type used in files defining loading functions.
    /// </summary>
    public enum eFileValueType
    {
        /// <summary>
        /// Values at equal time intervals.
        /// </summary>
        EqualTimeIntervals = 1,

        /// <summary>
        /// Time and function values.
        /// </summary>
        TimeAndFunction = 2
    }
#endif
}