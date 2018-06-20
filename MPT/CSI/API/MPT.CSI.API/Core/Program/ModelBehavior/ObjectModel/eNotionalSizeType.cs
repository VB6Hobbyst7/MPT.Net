// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eNotionalSizeType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Notional sizes available for sections in the application.
    /// </summary>
    public enum eNotionalSizeType
    {
        /// <summary>
        /// Program will determine the notional size based on the average thickness of an area element.
        /// </summary>
        Auto = 1,

        /// <summary>
        /// The notional size is based on the user-defined value.
        /// </summary>
        User = 2,

        /// <summary>
        /// Notional size will not be considered.
        /// In other words, the time-dependent effect of this section will not be considered.
        /// </summary>
        None = 3
    }
}
#endif