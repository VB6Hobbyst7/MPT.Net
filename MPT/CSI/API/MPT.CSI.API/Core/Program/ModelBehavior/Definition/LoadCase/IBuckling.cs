// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="IBuckling.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the buckling load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ISetLoadCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IInitialLoadCase" />
    public interface IBuckling:
        ISetLoadCase, IInitialLoadCase
    {
        /// <summary>
        /// This function retrieves various parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing buckling load case.</param>
        /// <param name="numberOfBucklingModes">The number of buckling modes requested.</param>
        /// <param name="eigenvalueConvergenceTolerance">The relative convergence tolerance for eigenvalues.</param>
        void GetParameters(string name,
            ref int numberOfBucklingModes,
            ref double eigenvalueConvergenceTolerance);


        /// <summary>
        /// This function sets various parameters for the specified buckling load case.
        /// </summary>
        /// <param name="name">The name of an existing buckling load case.</param>
        /// <param name="numberOfBucklingModes">The number of buckling modes requested.</param>
        /// <param name="eigenvalueConvergenceTolerance">The relative convergence tolerance for eigenvalues.</param>
        void SetParameters(string name,
            int numberOfBucklingModes,
            double eigenvalueConvergenceTolerance);
    }
#else
    /// <summary>
    /// Represents the buckling load case in the application.
    /// </summary>
    public interface IBuckling 
    {
    }
#endif
}
