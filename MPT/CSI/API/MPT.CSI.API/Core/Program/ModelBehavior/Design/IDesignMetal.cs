// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-08-2017
//
// Last Modified By : Mark
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IDesignMetal.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
    /// <summary>
    /// Implements a design interface for all metal-based frame elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignCode" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IResettable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IComboStrength" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IComboDeflection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IComboAuto" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IAutoSection" />
    public interface IDesignMetal : IDesignCode, IResettable, IComboStrength, IComboDeflection, IComboAuto, IAutoSection
#elif !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Implements a design interface for all metal-based frame elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignCode" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IResettable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IComboStrength" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IComboDeflection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IAutoSection" />
    public interface IDesignMetal : IDesignCode, IResettable, IComboStrength, IComboDeflection, IAutoSection
#else
    /// <summary>
    /// Implements a design interface for all metal-based frame elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignCode" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IResettable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IComboStrength" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IComboDeflection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IAutoSection" />
    public interface IDesignMetal : IDesignCode, IResettable, IComboStrength, IComboDeflection, IAutoSection
#endif
    {
        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="frameNames">This is an array that includes each frame object name for which results are obtained.</param>
        /// <param name="ratios">This is an array that includes the controlling stress or capacity ratio for each frame object.</param>
        /// <param name="ratioTypes">This is an array that includes the controlling stress or capacity ratio for each frame object.</param>
        /// <param name="locations">This is an array that includes the distance from the I-end of the frame object to the location where the controlling stress or capacity ratio occurs. [L]</param>
        /// <param name="comboNames">This is an array that includes the name of the design combination for which the controlling stress or capacity ratio occurs.</param>
        /// <param name="errorSummaries">This is an array that includes the design error messages for the frame object, if any.</param>
        /// <param name="warningSummaries">This is an array that includes the design warning messages for the frame object, if any.</param>
        /// <param name="itemType">This is one of the following items in the eItemType enumeration: Object = 0, Group = 1, SelectedObjects = 2
        /// If this item is Object, the design results are retrieved for the frame object specified by the Name item.
        /// If this item is Group, the design results are retrieved for all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, the design results are retrieved for all selected frame objects, and the Name item is ignored.</param>
        void GetSummaryResults(string name, 
            ref string[] frameNames, 
            ref double[] ratios, 
            ref int[] ratioTypes,
            ref double[] locations,
            ref string[] comboNames,
            ref string[] errorSummaries,
            ref string[] warningSummaries,
            eItemType itemType = eItemType.Object);
    }
}
