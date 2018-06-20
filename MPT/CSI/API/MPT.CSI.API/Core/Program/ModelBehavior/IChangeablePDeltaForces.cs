// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-24-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IChangeablePDeltaForces.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Element can assign P-Delta force data.
    /// </summary>
    public interface IChangeablePDeltaForces
    {
        /// <summary>
        /// This function assigns the P-Delta force assignments to frame objects.
        /// </summary>
        /// <param name="name">The name of a frame line object .</param>
        /// <param name="pDeltaForce">P-Delta force value assigned to the frame object. [F]</param>
        /// <param name="direction">The direction of the P-Delta force assignment.</param>
        /// <param name="replaceExistingLoads">True: All existing P-Delta force assignments to the frame object are removed before assigning the specified P-Delta force.
        /// False: the specified P-Delta force is added to any existing P-Delta forces already assigned to the frame object.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the projected P-Delta force is defined.
        /// This item is blank when the <paramref name="direction" /> item is <see cref="ePDeltaDirection.Local_1" />, that is, when the P-Delta force is defined in the frame object local 1-axis direction.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetPDeltaForce(string name,
            double pDeltaForce,
            ePDeltaDirection direction,
            bool replaceExistingLoads,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object);
    }
}
#endif