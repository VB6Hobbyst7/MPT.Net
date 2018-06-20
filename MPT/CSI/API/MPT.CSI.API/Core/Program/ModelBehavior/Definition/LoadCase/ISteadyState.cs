// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="ISteadyState.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the steady state-derived load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ISetLoadCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IInitialLoadCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ILoadSteadyState" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IDampingSteadyState" />
    public interface ISteadyState: 
        ISetLoadCase, IInitialLoadCase, 
        ILoadSteadyState, IDampingSteadyState
    {
        /// <summary>
        /// This function retrieves the frequency data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing steady state load case.</param>
        /// <param name="frequencyFirst">The first frequency. [cyc/s].</param>
        /// <param name="frequencyLast">The last frequency. [cyc/s].</param>
        /// <param name="numberOfFrequencies">The number of frequency increments.</param>
        /// <param name="addModalFrequencies">True: Modal frequencies are added.</param>
        /// <param name="addSignedFractionalDeviations">True: Signed fractional deviations from modal frequencies are added.</param>
        /// <param name="addSpecifiedFrequencies">True: Specified frequencies are added.</param>
        /// <param name="modalCase">This is the name of an existing modal load case.
        /// It specifies the modal load case on which modal frequencies and modal frequency deviations are based.</param>
        /// <param name="numberSignedFractionalDeviations">The number of signed fractional deviations from modal frequencies that are added.
        /// This item applies only when <paramref name="addSignedFractionalDeviations" /> = True.</param>
        /// <param name="signedFractionalDeviations">The added signed fractional deviations from modal frequencies.
        /// This item applies only when <paramref name="addSignedFractionalDeviations" /> = True.</param>
        /// <param name="numberSpecifiedFrequencies">The number of specified frequencies that are added.
        /// This item applies only when <paramref name="addSpecifiedFrequencies" /> = True.</param>
        /// <param name="specifiedFrequencies">The added specified frequencies.
        /// This item applies only when <paramref name="addSpecifiedFrequencies" /> = True.</param>
        void GetFrequencyData(string name,
            ref double frequencyFirst,
            ref double frequencyLast,
            ref int numberOfFrequencies,
            ref bool addModalFrequencies,
            ref bool addSignedFractionalDeviations,
            ref bool addSpecifiedFrequencies,
            ref string modalCase,
            ref int numberSignedFractionalDeviations,
            ref double[] signedFractionalDeviations,
            ref int numberSpecifiedFrequencies,
            ref double[] specifiedFrequencies);

        /// <summary>
        /// This function sets the frequency data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing steady state load case.</param>
        /// <param name="frequencyFirst">The first frequency. [cyc/s].</param>
        /// <param name="frequencyLast">The last frequency. [cyc/s].</param>
        /// <param name="numberOfFrequencies">The number of frequency increments.</param>
        /// <param name="addModalFrequencies">True: Modal frequencies are added.</param>
        /// <param name="addSignedFractionalDeviations">True: Signed fractional deviations from modal frequencies are added.</param>
        /// <param name="addSpecifiedFrequencies">True: Specified frequencies are added.</param>
        /// <param name="modalCase">This is the name of an existing modal load case.
        /// It specifies the modal load case on which modal frequencies and modal frequency deviations are based.</param>
        /// <param name="signedFractionalDeviations">The added signed fractional deviations from modal frequencies.
        /// This item applies only when <paramref name="addSignedFractionalDeviations" /> = True.</param>
        /// <param name="specifiedFrequencies">The added specified frequencies.
        /// This item applies only when <paramref name="addSpecifiedFrequencies" /> = True.</param>
        void SetFrequencyData(string name,
            double frequencyFirst,
            double frequencyLast,
            int numberOfFrequencies,
            bool addModalFrequencies,
            bool addSignedFractionalDeviations,
            bool addSpecifiedFrequencies,
            string modalCase,
            double[] signedFractionalDeviations,
            double[] specifiedFrequencies);
    }
}
#endif