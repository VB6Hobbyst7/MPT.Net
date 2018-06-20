// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="IExternalResults.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the external results load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ISetLoadCase" />
    public interface IExternalResults: ISetLoadCase
    {
        /// <summary>
        /// In the absence of a call to this function, the default values are <paramref name="firstStep" /> = 1 and <paramref name="lastStep" /> = 1.
        /// The number of steps available for this load case will be <paramref name="lastStep" /> – <paramref name="firstStep" /> + 1.
        /// </summary>
        /// <param name="name">The name of an existing external results load case.</param>
        /// <param name="firstStep">The number of the first step for which external results are to be subsequently provided for frame objects in conjunction with this load case.
        /// The value may be 0 or 1.
        /// A value of zero is typically used for cases that include the initial conditions, such as time-history cases.</param>
        /// <param name="lastStep">The number of the last step for which external results are to be subsequently provided for frame objects in conjunction with this load case.
        /// The value must be greater than or equal to <paramref name="firstStep" />.</param>
        void SetNumberSteps(string name,
            int firstStep,
            int lastStep);
    }
}
#endif
