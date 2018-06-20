// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="ResponseSpectrumFunction.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Represents the response spectrum function in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public abstract class ResponseSpectrumFunction : CSiApiBase
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseSpectrumFunction" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected ResponseSpectrumFunction(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Protected                
        /// <summary>
        /// Limits the damping ratio.
        /// </summary>
        /// <param name="dampingRatio">The damping ratio.</param>
        /// <returns>System.Double.</returns>
        protected double limitDampingRatio(double dampingRatio)
        {
            if (dampingRatio < 0)
            {
                return 0;
            }
            if (dampingRatio > 1)
            {
                return 1;
            }
            return dampingRatio;
        }

        // No other methods created, as this is meant to be a shared type for all response spectrum functions.
        #endregion
    }
}
