// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="eT1Source.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    /// <summary>
    /// T1 source for Chinese code.
    /// </summary>
    public enum eT1Source 
    {
        /// <summary>
        /// Modal analysis.
        /// </summary>
        ModalAnalysis = 0,

        /// <summary>
        /// User defined.
        /// </summary>
        UserDefined = 1
    }
}
#endif
