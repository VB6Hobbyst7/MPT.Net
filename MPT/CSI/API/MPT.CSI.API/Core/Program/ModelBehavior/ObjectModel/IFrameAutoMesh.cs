// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IFrameAutoMesh.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can be automeshed.
    /// </summary>
    public interface IFrameAutoMesh
    {
        /// <summary>
        /// This function retrieves  the automatic meshing assignments to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="isAutoMeshed">True: The frame object is automatically meshed by the program when the analysis model is created.</param>
        /// <param name="isAutoMeshedAtPoints">True: The frame object is automatically meshed at intermediate joints along its length.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="isAutoMeshedAtLines">True: The frame object is automatically meshed at intersections with other frames, area object edges and solid object edges.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="minElementNumber">The minimum number of elements into which the frame object is automatically meshed.
        /// If this item is zero, the number of elements is not checked when the automatic meshing is done.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="autoMeshMaxLength">The maximum length of auto meshed frame elements.
        /// If this item is zero, the element length is not checked when the automatic meshing is done. [L]
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        void GetAutoMesh(string name,
            ref bool isAutoMeshed,
            ref bool isAutoMeshedAtPoints,
            ref bool isAutoMeshedAtLines,
            ref int minElementNumber,
            ref double autoMeshMaxLength);

        /// <summary>
        /// This function makes automatic meshing assignments to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="isAutoMeshed">True: The frame object automatically meshed by the program when the analysis model is created.</param>
        /// <param name="isAutoMeshedAtPoints">True: The frame object is automatically meshed at intermediate joints along its length.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="isAutoMeshedAtLines">True: The frame object is automatically meshed at intersections with other frames, area object edges and solid object edges.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="minElementNumber">The minimum number of elements into which the frame object is automatically meshed.
        /// If this item is zero, the number of elements is not checked when the automatic meshing is done.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="autoMeshMaxLength">The maximum length of auto meshed frame elements.
        /// If this item is zero, the element length is not checked when the automatic meshing is done. [L]
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetAutoMesh(string name,
            bool isAutoMeshed,
            bool isAutoMeshedAtPoints,
            bool isAutoMeshedAtLines,
            int minElementNumber,
            double autoMeshMaxLength,
            eItemType itemType = eItemType.Object);
    }
}

#endif