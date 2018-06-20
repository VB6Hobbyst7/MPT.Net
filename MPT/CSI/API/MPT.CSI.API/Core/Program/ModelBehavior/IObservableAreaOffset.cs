// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-06-2017
// ***********************************************************************
// <copyright file="IObservableAreaOffset.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object has gettable offset properties.
    /// </summary>
    public interface IObservableAreaOffset
    {
        /// <summary>
        /// This function retrieves the joint offset assignments for area elements.
        /// </summary>
        /// <param name="name">The name of an existing area element.</param>
        /// <param name="offsetType">Indicates the joint offset type.</param>
        /// <param name="offsetPattern">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByJointPattern" />.
        /// It is the name of the defined joint pattern that is used to calculate the thicknesses.</param>
        /// <param name="offsetPatternScaleFactor">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByJointPattern" />.
        /// It is the scale factor applied to the joint pattern when calculating the thicknesses. [L]</param>
        /// <param name="offsets">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByPoint" />.
        /// It is an array of thicknesses at each of the points that define the area element. [L]</param>
        void GetOffsets(string name,
            ref eAreaOffsetType offsetType,
            ref string offsetPattern,
            ref double offsetPatternScaleFactor,
            ref double[] offsets);
    }
}