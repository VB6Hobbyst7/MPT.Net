// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IChangeableThickness.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has changeable thickness properties.
    /// </summary>
    public interface IChangeableThickness
    {
        /// <summary>
        /// This function assigns the thickness overwrite assignments for area objects.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="thicknessType">Indicates the thickness overwrite type.</param>
        /// <param name="thicknessPattern">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByJointPattern" />.
        /// It is the name of the defined joint pattern that is used to calculate the thicknesses.</param>
        /// <param name="thicknessPatternScaleFactor">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByJointPattern" />.
        /// It is the scale factor applied to the joint pattern when calculating the thicknesses. [L]</param>
        /// <param name="thicknesses">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByPoint" />.
        /// It is an array of thicknesses at each of the points that define the area object. [L]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetThickness(string name,
            eAreaThicknessType thicknessType,
            string thicknessPattern,
            double thicknessPatternScaleFactor,
            double[] thicknesses,
            eItemType itemType = eItemType.Object);
    }
}
#endif