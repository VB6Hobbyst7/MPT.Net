// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-19-2017
// ***********************************************************************
// <copyright file="IHyperstatic.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the hyperstatic load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ISetLoadCase" />
    public interface IHyperstatic: ISetLoadCase
    {
        /// <summary>
        /// This function retrieves the base case for the specified hyperstatic load case.
        /// </summary>
        /// <param name="name">The name of an existing hyperstatic load case.</param>
        /// <param name="nameBaseCase">The name of an existing static linear load case that is the base case for the specified hyperstatic load case.</param>
        void GetBaseCase(string name,
            ref string nameBaseCase);

        /// <summary>
        /// This function sets the base case for the specified hyperstatic load case.
        /// </summary>
        /// <param name="name">The name of an existing hyperstatic load case.</param>
        /// <param name="nameBaseCase">The name of an existing static linear load case that is the base case for the specified hyperstatic load case.</param>
        void SetBaseCase(string name,
            string nameBaseCase);
    }
}
#endif