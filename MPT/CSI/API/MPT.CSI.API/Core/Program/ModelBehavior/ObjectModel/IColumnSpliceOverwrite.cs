// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IColumnSpliceOverwrite.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has gettable/settable column splice overwrites.
    /// </summary>
    public interface IColumnSpliceOverwrite
    {
        /// <summary>
        /// Retrieves the frame object column splice overwrite assignment.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="spliceOption">The option used for defining the splice overwrite.</param>
        /// <param name="height">Specifies the height of the splice above the story at the bottom of the column object, if <paramref name="spliceOption"/> = <see cref="eColumnSpliceOption.SpliceAtHeightAboveStoryAtBottomOfColumn"/></param>
        void GetColumnSpliceOverwrite(string name,
            ref eColumnSpliceOption spliceOption,
            ref double height);

        /// <summary>
        /// Sets the frame object column splice overwrite assignment.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="spliceOption">The option used for defining the splice overwrite.</param>
        /// <param name="height">Specifies the height of the splice above the story at the bottom of the column object, if <paramref name="spliceOption"/> = <see cref="eColumnSpliceOption.SpliceAtHeightAboveStoryAtBottomOfColumn"/></param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        void SetColumnSpliceOverwrite(string name,
            eColumnSpliceOption spliceOption,
            double height,
            eItemType itemType = eItemType.Object);
    }
}
#endif