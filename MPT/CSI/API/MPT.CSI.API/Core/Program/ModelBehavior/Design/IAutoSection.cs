// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-03-2017
//
// Last Modified By : Mark
// Last Modified On : 10-03-2017
// ***********************************************************************
// <copyright file="IAutoSection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements an interface for automatically assigning frame sections based on design optimizations.
    /// </summary>
    public interface IAutoSection
    {
        // === Set Methods ===

        /// <summary>
        /// Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Selection type to use for applying the method.</param>
        void SetAutoSelectNull(string itemName, eItemType itemType = eItemType.Object);

        // === Get/Set Methods        
        /// <summary>
        /// Retrieves the names of all groups selected for design.
        /// These groups are used in the design optimization process, where the optimization is applied at a group level.
        /// </summary>
        /// <param name="nameGroups">The name of each group selected for design.</param>
        void GetGroup(ref string[] nameGroups);

        /// <summary>
        /// Selects or deselects a group for frame design.
        /// These groups are used in the design optimization process, where the optimization is applied at a group level.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <param name="selectForDesign">True: The specified group is selected as a design group for steel design.
        /// False: The group is not selected for steel design.</param>
        void SetGroup(string nameGroup, bool selectForDesign);
    }
}