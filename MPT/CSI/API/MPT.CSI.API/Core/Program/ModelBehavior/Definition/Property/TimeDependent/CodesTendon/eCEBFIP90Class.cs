// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eCEBFIP90Class.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesTendon
{
    /// <summary>
    /// The CEB FIP-90 classes.
    /// </summary>
    public enum eCEBFIP90Class
    {
        /// <summary>
        /// Class 1.
        /// </summary>
        Class1 = 1,

        /// <summary>
        /// Class 2.
        /// </summary>
        Class2 = 2
    }
}
#endif
