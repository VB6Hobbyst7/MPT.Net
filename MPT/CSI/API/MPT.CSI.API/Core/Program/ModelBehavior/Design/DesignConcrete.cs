// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-08-2017
//
// Last Modified By : Mark
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="DesignConcrete.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Represents Concrete design in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignConcrete" />
    public class DesignConcrete : CSiApiBase, IDesignConcrete
    {
        #region Properties   

        /// <summary>
        /// Gets the concrete design code.
        /// </summary>
        /// <value>The concrete design code.</value>
        public ConcreteCode Code { get; private set; }


        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignConcrete" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public DesignConcrete(CSiApiSeed seed) : base(seed)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignConcrete" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="code">The design code to use.</param>
        public DesignConcrete(CSiApiSeed seed, 
            ConcreteCode code) : base(seed)
        {
            Code = code;
        }
        #endregion

        #region Methods: Interface
        /// <summary>
        /// Sets the design code used by the class.
        /// </summary>
        /// <param name="code">The design code for the class to use.</param>
        public void SetCode(ConcreteCode code)
        {
            Code = code;
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteResults()
        {
            _callCode = _sapModel.DesignConcrete.DeleteResults();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ResetOverwrites()
        {
            _callCode = _sapModel.DesignConcrete.ResetOverwrites();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        /// <summary>
        /// Starts the frame design.
        /// </summary>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void StartDesign()
        {
            _callCode = _sapModel.DesignConcrete.StartDesign();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public bool ResultsAreAvailable()
        {
            return _sapModel.DesignConcrete.GetResultsAvailable();
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the names of the frame objects that did not pass the design check or have not yet been checked, if any.
        /// </summary>
        /// <param name="numberNotPassedOrChecked">The number of concrete frame objects that did not pass the design check or have not yet been checked.</param>
        /// <param name="numberDidNotPass">The number of concrete frame objects that did not pass the design check.</param>
        /// <param name="numberNotChecked">The number of concrete frame objects that have not yet been checked.</param>
        /// <param name="namesNotPassedOrChecked">This is an array that includes the name of each frame object that did not pass the design check or has not yet been checked.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void VerifyPassed(ref int numberNotPassedOrChecked, 
            ref int numberDidNotPass, 
            ref int numberNotChecked, 
            ref string[] namesNotPassedOrChecked)
        {
            _callCode = _sapModel.DesignConcrete.VerifyPassed(ref numberNotPassedOrChecked, ref numberDidNotPass, ref numberNotChecked, ref namesNotPassedOrChecked);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of the frame objects that have different analysis and design sections, if any.
        /// </summary>
        /// <param name="numberDifferentSections">The number of frame objects that have different analysis and design sections.</param>
        /// <param name="namesDifferentSections">This is an array that includes the name of each frame object that has different analysis and design sections.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void VerifySections(ref int numberDifferentSections, 
            ref string[] namesDifferentSections)
        {
            _callCode = _sapModel.DesignConcrete.VerifySections(ref numberDifferentSections, ref namesDifferentSections);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        // === Get/Set ===
        // TODO: Consider how to flexibly apply this as an enum.
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetCode(ref string codeName)
        {
            _callCode = _sapModel.DesignConcrete.GetCode(ref codeName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // TODO: Consider how to flexibly apply this as an enum.
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCode(string codeName)
        {
            _callCode = _sapModel.DesignConcrete.SetCode(codeName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        // ===

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
        /// <param name="nameSection">The name of the design section for the specified frame object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDesignSection(string nameFrame,
            ref string nameSection)
        {
            _callCode = _sapModel.DesignConcrete.GetDesignSection(nameFrame, ref nameSection);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="nameSection">Name of an existing frame section property to be used as the design section for the specified frame objects.
        /// This item applies only when resetToLastAnalysisSection = False.</param>
        /// <param name="resetToLastAnalysisSection">True: The design section for the specified frame objects is reset to the last analysis section for the frame object.
        /// False: The design section is set to that specified by nameFrame.</param>
        /// <param name="itemType">Selection type to use for applying the method.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDesignSection(string itemName,
            string nameSection,
            bool resetToLastAnalysisSection,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignConcrete.SetDesignSection(itemName, 
                            nameSection, resetToLastAnalysisSection, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        /// <summary>
        /// Retrieves the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetComboAutoGenerate(ref bool autoGenerate)
        {
            _callCode = _sapModel.DesignConcrete.GetComboAutoGenerate(ref autoGenerate);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetComboAutoGenerate(bool autoGenerate)
        {
            _callCode = _sapModel.DesignConcrete.SetComboAutoGenerate(autoGenerate);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // ===

        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <param name="nameLoadCombinations">The name of the load combinations selected.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetComboStrength(ref string[] nameLoadCombinations)
        {
            _callCode = _sapModel.DesignConcrete.GetComboStrength(ref _numberOfItems, ref nameLoadCombinations);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Selects or deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for strength design.
        /// False: The combination is not selected for strength design.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetComboStrength(string nameLoadCombination, 
            bool selectLoadCombination)
        {
            _callCode = _sapModel.DesignConcrete.SetComboStrength(nameLoadCombination, selectLoadCombination);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion


        #region Methods: Public

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSummaryResultsBeam(string name, 
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
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignConcrete.GetSummaryResultsBeam(name, ref numberItems, 
                            ref frameName, ref location, 
                            ref topCombo, ref topArea, ref botCombo, ref botArea, ref VMajorCombo, ref VMajorArea, 
                            ref TLCombo, ref TLArea, ref TTCombo, ref TTArea, 
                            ref errorSummary, ref warningSummary, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSummaryResultsColumn(string name,
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
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignConcrete.GetSummaryResultsColumn(name, ref numberItems, 
                            ref frameName, ref myOption, ref location, 
                            ref PMMCombo, ref PMMArea, ref PMMRatio, ref VMajorCombo, ref AVMajor, ref VMinorCombo, ref AVMinor, 
                            ref errorSummary, ref warningSummary, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSummaryResultsJoint(string name,
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
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignConcrete.GetSummaryResultsJoint(name, 
                            ref numberItems, ref frameName, 
                            ref LCJSRatioMajor, ref JSRatioMajor, ref LCJSRatioMinor, ref JSRatioMinor, ref LCBCCRatioMajor, 
                            ref BCCRatioMajor, ref LCBCCRatioMinor, ref BCCRatioMinor, ref errorSummary, ref warningSummary, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
