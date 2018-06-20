// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="ePhiZSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    /// <summary>
    /// Phi Z source for Chinese code.
    /// </summary>
    public enum ePhiZSource 
    {
        /// <summary>
        /// Modal analysis.
        /// </summary>
        ModalAnalysis = 0,

        /// <summary>
        /// Z/H ratio.
        /// </summary>
        ZHRatio = 1
    }
}
#endif
