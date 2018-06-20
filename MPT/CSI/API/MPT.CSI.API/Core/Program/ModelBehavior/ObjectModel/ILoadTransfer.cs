// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ILoadTransfer.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Frame can have loads applied to it from area objects.
    /// </summary>
    public interface ILoadTransfer
    {
        /// <summary>
        /// This function returns the load transfer option for a frame object.
        /// It indicates whether the frame receives load from an area object when the area object is loaded with a load of type uniform to frame.
        /// </summary>
        /// <param name="name">The name of an existing frame.</param>
        /// <param name="loadIsTransferred">Indicates if load is allowed to be transferred from area objects to this frame object.</param>
        void GetLoadTransfer(string name,
            ref bool loadIsTransferred);

        /// <summary>
        /// This function returns the load transfer option for frame objects.
        /// It indicates whether the frame receives load from an area object when the area object is loaded with a load of type uniform to frame.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadIsTransferred">Indicates if load is allowed to be transferred from area objects to this frame object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadTransfer(string name,
            bool loadIsTransferred,
            eItemType itemType = eItemType.Object);
    }
}
#endif