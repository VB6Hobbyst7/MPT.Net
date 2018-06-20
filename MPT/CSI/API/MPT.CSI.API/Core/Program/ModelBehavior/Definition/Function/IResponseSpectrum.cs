// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="IResponseSpectrum.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function
{
    /// <summary>
    /// Implements the response spectrum function in the application.
    /// </summary>
    public interface IResponseSpectrum
    {
        #region Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Response spectrum function according to user definitions.
        /// </summary>
        /// <value>The user.</value>
        User User { get; }

        /// <summary>
        /// Response spectrum function according to a file definition.
        /// </summary>
        /// <value>The file.</value>
        File File { get; }


        /// <summary>
        /// Response spectrum function.
        /// </summary>
        /// <value>The response spectrum function.</value>
        ResponseSpectrumFunction ResponseSpectrumFunction { get; }
#endif
        #endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the auto seismic load code used by the class.
        /// </summary>
        /// <param name="responseSpectrumFunction">The response spectrum function for the class to use.</param>
        void SetAutoCode(ResponseSpectrumFunction responseSpectrumFunction);
#endif
    }
}