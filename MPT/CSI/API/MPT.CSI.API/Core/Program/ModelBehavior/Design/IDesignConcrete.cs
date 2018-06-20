// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-03-2017
//
// Last Modified By : Mark
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IDesignConcrete.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
    /// <summary>
    /// Implements a design interface for all concrete-based frame elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignCode" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IResettable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IComboStrength" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IComboAuto" />
    public interface IDesignConcrete : IDesignCode, IResettable, IComboStrength, IComboAuto
#elif !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Implements a design interface for all concrete-based frame elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignCode" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IResettable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IComboStrength" />
    public interface IDesignConcrete : IDesignCode, IResettable, IComboStrength
#else
    /// <summary>
    /// Implements a design interface for all concrete-based frame elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignCode" />
    public interface IDesignConcrete : IDesignCode
#endif
    {
        #region Properties
        /// <summary>
        /// Gets the concrete design code.
        /// </summary>
        /// <value>The concrete design code.</value>
        ConcreteCode Code { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the design code used by the class.
        /// </summary>
        /// <param name="code">The design code for the class to use.</param>
        void SetCode(ConcreteCode code);

        /// <summary>
        /// Retrieves summary results for concrete design of beams.
        /// Torsion results are not included for all codes.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="numberItems">The number of frame objects for which results are obtained.</param>
        /// <param name="frameName">This is an array that includes each frame object name for which results are obtained.</param>
        /// <param name="location">This is an array that includes the distance from the I-end of the frame object to the location where the results are reported. [L]</param>
        /// <param name="topCombo">This is an array that includes the name of the design combination for which the controlling top longitudinal rebar area for flexure occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).</param>
        /// <param name="topArea">This is an array that includes the total top longitudinal rebar area required for the flexure at the specified location.
        /// It does not include the area of steel required for torsion. [L^2]</param>
        /// <param name="botCombo">This is an array that includes the name of the design combination for which the controlling bottom longitudinal rebar area for flexure occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific, multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).</param>
        /// <param name="botArea">This is an array that includes the total bottom longitudinal rebar area required for the flexure at the specified location.
        /// It does not include the area of steel required for torsion. [L^2]</param>
        /// <param name="VMajorCombo">This is an array that includes the name of the design combination for which the controlling shear occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific, multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).</param>
        /// <param name="VMajorArea">This is an array that includes the required area of transverse shear reinforcing per unit length along the frame object for shear at the specified location. [L^2/L]</param>
        /// <param name="TLCombo">This is an array that includes the name of the design combination for which the controlling longitudinal rebar area for torsion occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific, multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).</param>
        /// <param name="TLArea">This is an array that includes the total longitudinal rebar area required for torsion. [L^2]</param>
        /// <param name="TTCombo">This is an array that includes the name of the design combination for which the controlling transverse reinforcing for torsion occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific, multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).</param>
        /// <param name="TTArea">This is an array that includes the required area of transverse torsional shear reinforcing per unit length along the frame object for torsion at the specified location. [L^2/L]</param>
        /// <param name="errorSummary">This is an array that includes the design error messages for the frame object, if any.</param>
        /// <param name="warningSummary">This is an array that includes the design warning messages for the frame object, if any.</param>
        /// <param name="itemType">This is one of the following items in the eItemType enumeration.
        /// If this item is Object, the design results are retrieved for the frame object specified by the Name item.
        /// If this item is Group, the design results are retrieved for all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, the design results are retrieved for all selected frame objects, and the Name item is ignored.</param>
        void GetSummaryResultsBeam(string name,
            ref int numberItems,
            ref string[] frameName,
            ref double[] location,
            ref string[] topCombo,
            ref double[] topArea,
            ref string[] botCombo,
            ref double[] botArea,
            ref string[] VMajorCombo,
            ref double[] VMajorArea,
            ref string[] TLCombo,
            ref double[] TLArea,
            ref string[] TTCombo,
            ref double[] TTArea,
            ref string[] errorSummary,
            ref string[] warningSummary,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// Retrieves summary results for concrete design of columns.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="numberItems">The number of frame objects for which results are obtained.</param>
        /// <param name="frameName">This is an array that includes each frame object name for which results are obtained.</param>
        /// <param name="myOption">This is an array that includes 1 or 2, indicating the design option for each frame object: 1 = Check, 2 = Design</param>
        /// <param name="location">This is an array that includes the distance from the I-end of the frame object to the location where the results are reported. [L]</param>
        /// <param name="PMMCombo">This is an array that includes the name of the design combination for which the controlling PMM ratio or rebar area occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).</param>
        /// <param name="PMMArea">This is an array that includes the total longitudinal rebar area required for the axial force plus biaxial moment (PMM) design at the specified location. [L^2]</param>
        /// <param name="PMMRatio">This is an array that includes the axial force plus biaxial moment (PMM) stress ratio at the specified location.
        /// Item applies only when MyOption = 1 (check).</param>
        /// <param name="VMajorCombo">This is an array that includes the name of the design combination for which the controlling major shear occurs.</param>
        /// <param name="AVMajor">This is an array that includes the required area of transverse shear reinforcing per unit length along the frame object for major shear at the specified location. [L^2/L]</param>
        /// <param name="VMinorCombo">This is an array that includes the name of the design combination for which the controlling minor shear occurs.</param>
        /// <param name="AVMinor">This is an array that includes the required area of transverse shear reinforcing per unit length along the frame object for minor shear at the specified location. [L^2/L]</param>
        /// <param name="errorSummary">This is an array that includes the design error messages for the frame object, if any.</param>
        /// <param name="warningSummary">This is an array that includes the design warning messages for the frame object, if any.</param>
        /// <param name="itemType">This is one of the following items in the eItemType enumeration.
        /// If this item is Object, the design results are retrieved for the frame object specified by the Name item.
        /// If this item is Group, the design results are retrieved for all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, the design results are retrieved for all selected frame objects, and the Name item is ignored.</param>
        void GetSummaryResultsColumn(string name,
            ref int numberItems,
            ref string[] frameName,
            ref int[] myOption,
            ref double[] location,
            ref string[] PMMCombo,
            ref double[] PMMArea,
            ref double[] PMMRatio,
            ref string[] VMajorCombo,
            ref double[] AVMajor,
            ref string[] VMinorCombo,
            ref double[] AVMinor,
            ref string[] errorSummary,
            ref string[] warningSummary,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// Retrieves summary results for concrete design of joints.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="numberItems">The number of frame objects for which results are obtained.</param>
        /// <param name="frameName">This is an array that includes each frame object name for which results are obtained.</param>
        /// <param name="LCJSRatioMajor">This is an array that includes the name of the design combination for which the controlling joint shear ratio associated with the column major axis occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).</param>
        /// <param name="JSRatioMajor">This is an array that includes the joint shear ratio associated with the column major axis.
        /// This is the joint shear divided by the joint shear capacity.</param>
        /// <param name="LCJSRatioMinor">This is an array that includes the name of the design combination for which the controlling joint shear ratio associated with the column minor axis occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).</param>
        /// <param name="JSRatioMinor">This is an array that includes the joint shear ratio associated with the column minor axis.
        /// This is the joint shear divided by the joint shear capacity.</param>
        /// <param name="LCBCCRatioMajor">This is an array that includes the name of the design combination for which the controlling beam/column capacity ratio associated with the column major axis occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).</param>
        /// <param name="BCCRatioMajor">This is an array that includes the beam/column capacity ratio associated with the column major axis.
        /// This is the sum of the column capacities divided by the sum of the beam capacities at the top of the specified column.</param>
        /// <param name="LCBCCRatioMinor">This is an array that includes the name of the design combination for which the controlling beam/column capacity ratio associated with the column minor axis occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).</param>
        /// <param name="BCCRatioMinor">This is an array that includes the beam/column capacity ratio associated with the column minor axis. This is the sum of the column capacities divided by the sum of the beam capacities at the top of the specified column.</param>
        /// <param name="errorSummary">This is an array that includes the design error messages for the frame object, if any.</param>
        /// <param name="warningSummary">This is an array that includes the design warning messages for the frame object, if any.</param>
        /// <param name="itemType">This is one of the following items in the eItemType enumeration.
        /// If this item is Object, the design results are retrieved for the frame object specified by the Name item.
        /// If this item is Group, the design results are retrieved for all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, the design results are retrieved for all selected frame objects, and the Name item is ignored.</param>
        void GetSummaryResultsJoint(string name,
            ref int numberItems,
            ref string[] frameName,
            ref string[] LCJSRatioMajor,
            ref double[] JSRatioMajor,
            ref string[] LCJSRatioMinor,
            ref double[] JSRatioMinor,
            ref string[] LCBCCRatioMajor,
            ref double[] BCCRatioMajor,
            ref string[] LCBCCRatioMinor,
            ref double[] BCCRatioMinor,
            ref string[] errorSummary,
            ref string[] warningSummary,
            eItemType itemType = eItemType.Object);
        #endregion
    }
}