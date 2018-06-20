// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-08-2017
//
// Last Modified By : Mark
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="DesignColdFormed.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
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
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.ColdFormed;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Represents Cold-Formed Steel design in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignColdFormed" />
    public class DesignColdFormed : CSiApiBase, IDesignColdFormed
    {
        #region Properties        
        /// <summary>
        /// Gets cold-formed steel design code.
        /// </summary>
        /// <value>The cold-formed steel design code.</value>
        public ColdFormedSteelCode Code { get; private set; }
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignColdFormed" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public DesignColdFormed(CSiApiSeed seed) : base(seed)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignAluminum" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="code">The design code to use.</param>
        public DesignColdFormed(CSiApiSeed seed, 
            ColdFormedSteelCode code) : base(seed)
        {
            Code = code;
        }
        #endregion

        #region Methods: Interface
        /// <summary>
        /// Sets the design code used by the class.
        /// </summary>
        /// <param name="code">The design code for the class to use.</param>
        public void SetCode(ColdFormedSteelCode code)
        {
            Code = code;
        }

        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteResults()
        {
            _callCode = _sapModel.DesignColdFormed.DeleteResults();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ResetOverwrites()
        {
            _callCode = _sapModel.DesignColdFormed.ResetOverwrites();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Starts the frame design.
        /// </summary>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void StartDesign()
        {
            _callCode = _sapModel.DesignColdFormed.StartDesign();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public bool ResultsAreAvailable()
        {
            return _sapModel.DesignColdFormed.GetResultsAvailable();
        }
#endif


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
            _callCode = _sapModel.DesignColdFormed.VerifyPassed(ref numberNotPassedOrChecked, ref numberDidNotPass, ref numberNotChecked,ref  namesNotPassedOrChecked);
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
            _callCode = _sapModel.DesignColdFormed.VerifySections(ref numberDifferentSections, ref namesDifferentSections);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // === Get/Set ===
        // TODO: Consider how to flexibly apply this as an enum.
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetCode(ref string codeName)
        {
            _callCode = _sapModel.DesignColdFormed.GetCode(ref codeName);
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
            _callCode = _sapModel.DesignColdFormed.SetCode(codeName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
        /// <param name="nameSection">The name of the design section for the specified frame object.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDesignSection(string nameFrame, 
            ref string nameSection)
        {
            _callCode = _sapModel.DesignColdFormed.GetDesignSection(nameFrame, ref nameSection);
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
            _callCode = _sapModel.DesignColdFormed.SetDesignSection(itemName, 
                            nameSection, resetToLastAnalysisSection, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// Retrieves the names of all groups selected for design.
        /// These groups are used in the design optimization process, where the optimization is applied at a group level.
        /// </summary>
        /// <param name="nameGroups">The name of each group selected for design.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGroup(ref string[] nameGroups)
        {
            _callCode = _sapModel.DesignColdFormed.GetGroup(ref _numberOfItems, ref nameGroups);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Selects or deselects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <param name="selectForDesign">True: The specified group is selected as a design group for steel design.
        /// False: The group is not selected for steel design.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetGroup(string nameGroup,
            bool selectForDesign)
        {
            _callCode = _sapModel.DesignColdFormed.SetGroup(nameGroup, selectForDesign);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if  !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
        /// <summary>
        /// Retrieves the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetComboAutoGenerate(ref bool autoGenerate)
        {
            _callCode = _sapModel.DesignColdFormed.GetComboAutoGenerate(ref autoGenerate);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetComboAutoGenerate(bool autoGenerate)
        {
            _callCode = _sapModel.DesignColdFormed.SetComboAutoGenerate(autoGenerate);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        // ===

        /// <summary>
        /// Gets the names of all load combinatiojns used for deflection design.
        /// </summary>
        /// <param name="nameLoadCombinations">The name of each load combination used for consideration of deflecton limits.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetComboDeflection(ref string[] nameLoadCombinations)
        {
            _callCode = _sapModel.DesignColdFormed.GetComboDeflection(ref _numberOfItems, ref nameLoadCombinations);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Selects or deselects a load combination for deflection design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for deflection design.
        /// False: The combination is not selected for deflection design.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetComboDeflection(string nameLoadCombination,
            bool selectLoadCombination)
        {
            _callCode = _sapModel.DesignColdFormed.SetComboDeflection(nameLoadCombination, selectLoadCombination);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===
        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <param name="nameLoadCombinations">The name of the load combinations selected.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetComboStrength(ref string[] nameLoadCombinations)
        {
            _callCode = _sapModel.DesignColdFormed.GetComboStrength(ref _numberOfItems, ref nameLoadCombinations);
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
            _callCode = _sapModel.DesignColdFormed.SetComboStrength(nameLoadCombination, selectLoadCombination);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // === Get ===
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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSummaryResults(string name, 
            ref string[] frameNames, 
            ref double[] ratios, 
            ref int[] ratioTypes,
            ref double[] locations,
            ref string[] comboNames,
            ref string[] errorSummaries, 
            ref string[] warningSummaries, 
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignColdFormed.GetSummaryResults(name, ref _numberOfItems, 
                ref frameNames, ref ratios, ref ratioTypes, ref locations, ref comboNames, ref errorSummaries, ref warningSummaries, 
                EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // === Set ===
        /// <summary>
        /// Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Selection type to use for applying the method.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetAutoSelectNull(string itemName, 
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignColdFormed.SetAutoSelectNull(itemName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif
