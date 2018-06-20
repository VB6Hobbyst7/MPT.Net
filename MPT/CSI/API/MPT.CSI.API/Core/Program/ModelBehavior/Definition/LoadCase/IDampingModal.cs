// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IDampingModal.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents modal damping in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IDampingProportional" />
    public interface IDampingModal:
        IDampingProportional
    {
        /// <summary>
        /// This function retrieves the constant modal damping for all modes assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case that has constant damping.</param>
        /// <param name="damping">The constant damping for all modes (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        void GetDampingConstant(string name,
            ref double damping);


        /// <summary>
        /// This function retrieves the interpolated modal damping data assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case that has interpolated damping.</param>
        /// <param name="dampingType">The interpolated modal damping type.</param>
        /// <param name="periodsOrFrequencies">The periods or frequencies, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByFrequency" />.</param>
        /// <param name="damping">The damping for the specified period of frequency (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        void GetDampingInterpolated(string name,
            ref eDampingTypeInterpolated dampingType,
            ref double[] periodsOrFrequencies,
            ref double[] damping);




        /// <summary>
        /// This function retrieves the modal damping overrides assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="modes">The modes.</param>
        /// <param name="damping">The damping for the specified mode (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        void GetDampingOverrides(string name,
            ref int[] modes,
            ref double[] damping);




        /// <summary>
        /// This function retrieves the hysteretic damping type for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="dampingType">The modal damping type for the load case.</param>
        void GetDampingType(string name,
            ref eDampingType dampingType);

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function sets constant modal damping for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case that has constant damping.</param>
        /// <param name="damping">The constant damping for all modes (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        void SetDampingConstant(string name,
            double damping);

        /// <summary>
        /// This function retrieves the interpolated modal damping data assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case that has interpolated damping.</param>
        /// <param name="dampingType">The interpolated modal damping type.</param>
        /// <param name="periodsOrFrequencies">The periods or frequencies, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByFrequency" />.</param>
        /// <param name="damping">The damping for the specified period of frequency (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        void SetDampingInterpolated(string name,
            eDampingTypeInterpolated dampingType,
            double[] periodsOrFrequencies,
            double[] damping);

        /// <summary>
        /// This function retrieves the modal damping overrides assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="modes">The modes.</param>
        /// <param name="damping">The damping for the specified mode (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        void SetDampingOverrides(string name,
            int[] modes,
            double[] damping);
#endif
    }
}
