// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="IDirectionalCombination.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Directional combinations are used in the load case.
    /// </summary>
    public interface IDirectionalCombination
    {

        /// <summary>
        /// This function retrieves the directional combination option assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="directionalCombination">The directional combination option.</param>
        /// <param name="scaleFactor">The abslute value scale factor.
        /// This item applies only when <paramref name="directionalCombination" /> = <see cref="eDirectionalCombination.ABS" /></param>
        void GetDirectionalCombination(string name,
            ref eDirectionalCombination directionalCombination,
            ref double scaleFactor);

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function sets the directional combination option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="directionalCombination">The directional combination option.</param>
        /// <param name="scaleFactor">The abslute value scale factor.
        /// This item applies only when <paramref name="directionalCombination" /> = <see cref="eDirectionalCombination.ABS" /></param>
        void SetDirectionalCombination(string name,
            eDirectionalCombination directionalCombination,
            double scaleFactor);
#endif
    }
}