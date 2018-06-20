// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="IDampingProportional.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Load case has proportional damping.
    /// </summary>
    public interface IDampingProportional
    {
        /// <summary>
        /// This function retrieves the proportional modal damping data assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing linear modal history analysis case that has proportional damping.</param>
        /// <param name="dampingType">The proportional modal damping type.</param>
        /// <param name="massProportionalDampingCoefficient">The mass proportional damping coefficient.</param>
        /// <param name="stiffnessProportionalDampingCoefficient">The stiffness proportional damping coefficient.</param>
        /// <param name="periodOrFrequencyPt1">The period or frequency for point 1, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="periodOrFrequencyPt2">The period or frequency for point 2, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="dampingPt1">The damping for point 1 (0 &lt;= <paramref name="dampingPt1" /> &lt; 1).
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="dampingPt2">The damping for point 2 (0 &lt;= <paramref name="dampingPt1" /> &lt; 1).
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        void GetDampingProportional(string name,
            ref eDampingTypeProportional dampingType,
            ref double massProportionalDampingCoefficient,
            ref double stiffnessProportionalDampingCoefficient,
            ref double periodOrFrequencyPt1,
            ref double periodOrFrequencyPt2,
            ref double dampingPt1,
            ref double dampingPt2);



#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function sets proportional modal damping data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing linear modal history analysis case that has proportional damping.</param>
        /// <param name="dampingType">The proportional modal damping type.</param>
        /// <param name="massProportionalDampingCoefficient">The mass proportional damping coefficient.</param>
        /// <param name="stiffnessProportionalDampingCoefficient">The stiffness proportional damping coefficient.</param>
        /// <param name="periodOrFrequencyPt1">The period or frequency for point 1, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="periodOrFrequencyPt2">The period or frequency for point 2, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="dampingPt1">The damping for point 1 (0 &lt;= <paramref name="dampingPt1" /> &lt; 1).
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="dampingPt2">The damping for point 2 (0 &lt;= <paramref name="dampingPt1" /> &lt; 1).
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        void SetDampingProportional(string name,
            eDampingTypeProportional dampingType,
            double massProportionalDampingCoefficient,
            double stiffnessProportionalDampingCoefficient,
            double periodOrFrequencyPt1,
            double periodOrFrequencyPt2,
            double dampingPt1,
            double dampingPt2);
#endif
    }
}