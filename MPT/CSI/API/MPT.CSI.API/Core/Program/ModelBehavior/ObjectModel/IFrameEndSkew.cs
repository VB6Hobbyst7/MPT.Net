// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IFrameEndSkew.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has gettable/settable end skew attributes.
    /// </summary>
    public interface IFrameEndSkew
    {
        /// <summary>
        /// This function retrieves frame object end skew assignments.
        /// End skew data is only used in the program to plot the extruded view of bridge objects that have been updated as spine models.
        /// TODO: Handle? End skew assignments are only applicable to straight frame objects. An error is returned if skew data is requested for a curved frame object.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="skewI">The angle in degrees measured counter clockwise from the positive local 3-axis to a line parallel to the I-End of the frame object (-90 &lt; <paramref name="skewI" /> &lt; 90). [deg]</param>
        /// <param name="skewJ">The angle in degrees measured counter clockwise from the positive local 3-axis to a line parallel to the J-End of the frame object (-90 &lt; <paramref name="skewJ" /> &lt; 90). [deg]</param>
        void GetEndSkew(string name,
            ref double skewI,
            ref double skewJ);

        /// <summary>
        /// This function assigns frame object end skew assignments.
        /// End skew data is only used in the program to plot the extruded view of bridge objects that have been updated as spine models.
        /// TODO: Handle? End skew assignments are only applicable to straight frame objects. An error is returned if skew data is requested for a curved frame object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="skewI">The angle in degrees measured counter clockwise from the positive local 3-axis to a line parallel to the I-End of the frame object (-90 &lt; <paramref name="skewI" /> &lt; 90). [deg]</param>
        /// <param name="skewJ">The angle in degrees measured counter clockwise from the positive local 3-axis to a line parallel to the J-End of the frame object (-90 &lt; <paramref name="skewJ" /> &lt; 90). [deg]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetEndSkew(string name,
            double skewI,
            double skewJ,
            eItemType itemType = eItemType.Object);
    }
}
#endif