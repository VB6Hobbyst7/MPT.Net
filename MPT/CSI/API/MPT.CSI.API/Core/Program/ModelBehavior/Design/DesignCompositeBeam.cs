// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-02-2017
//
// Last Modified By : Mark
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="DesignCompositeBeam.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
#if BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Represents Composite Beam design in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignCompositeBeam" />
    public class DesignCompositeBeam : CSiApiBase, IDesignCompositeBeam
    {
#region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignCompositeBeam"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public DesignCompositeBeam(CSiApiSeed seed) : base(seed)
        {

        }
#endregion

#region Methods: Interface

        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        public void DeleteResults()
        {
            _callCode = _sapModel.DesignCompositeBeam.DeleteResults();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        public void ResetOverwrites()
        {
            _callCode = _sapModel.DesignCompositeBeam.ResetOverwrites();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Starts the frame design.
        /// </summary>
        public void StartDesign()
        {
            _callCode = _sapModel.DesignCompositeBeam.StartDesign();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 
        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public bool ResultsAreAvailable()
        {
            return _sapModel.DesignCompositeBeam.GetResultsAvailable();
        }
#endif

        /// <summary>
        /// This function retrieves the names of the frame objects that did not pass the design check or have not yet been checked, if any.
        /// </summary>
        /// <param name="numberNotPassedOrChecked">The number of concrete frame objects that did not pass the design check or have not yet been checked.</param>
        /// <param name="numberDidNotPass">The number of concrete frame objects that did not pass the design check.</param>
        /// <param name="numberNotChecked">The number of concrete frame objects that have not yet been checked.</param>
        /// <param name="namesNotPassedOrChecked">This is an array that includes the name of each frame object that did not pass the design check or has not yet been checked.</param>
        public void VerifyPassed(ref int numberNotPassedOrChecked,
            ref int numberDidNotPass,
            ref int numberNotChecked,
            ref string[] namesNotPassedOrChecked)
        {
            _callCode = _sapModel.DesignCompositeBeam.VerifyPassed(ref numberNotPassedOrChecked, ref numberDidNotPass, ref numberNotChecked, ref namesNotPassedOrChecked);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of the frame objects that have different analysis and design sections, if any.
        /// </summary>
        /// <param name="numberDifferentSections">The number of frame objects that have different analysis and design sections.</param>
        /// <param name="namesDifferentSections">This is an array that includes the name of each frame object that has different analysis and design sections.</param>
        public void VerifySections(ref int numberDifferentSections,
            ref string[] namesDifferentSections)
        {
            _callCode = _sapModel.DesignCompositeBeam.VerifySections(ref numberDifferentSections, ref namesDifferentSections);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // === Get/Set ===
        // TODO: Consider how to flexibly apply this as an enum.
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        public void GetCode(ref string codeName)
        {
            _callCode = _sapModel.DesignCompositeBeam.GetCode(ref codeName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // TODO: Consider how to flexibly apply this as an enum.
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        public void SetCode(string codeName)
        {
            _callCode = _sapModel.DesignCompositeBeam.SetCode(codeName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
        /// <param name="nameSection">The name of the design section for the specified frame object.</param>
        public void GetDesignSection(string nameFrame,
            ref string nameSection)
        {
            _callCode = _sapModel.DesignCompositeBeam.GetDesignSection(nameFrame, ref nameSection);
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
        public void SetDesignSection(string itemName,
            string nameSection,
            bool resetToLastAnalysisSection,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignCompositeBeam.SetDesignSection(itemName, 
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
        public void GetGroup(ref string[] nameGroups)
        {
            _callCode = _sapModel.DesignCompositeBeam.GetGroup(ref _numberOfItems, ref nameGroups);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Selects or deselects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <param name="selectForDesign">True: The specified group is selected as a design group for steel design. 
        /// False: The group is not selected for steel design.</param>
        public void SetGroup(string nameGroup,
            bool selectForDesign)
        {
            _callCode = _sapModel.DesignCompositeBeam.SetGroup(nameGroup, selectForDesign);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Gets the names of all load combinatiojns used for deflection design.
        /// </summary>
        /// <param name="nameLoadCombinations">The name of each load combination used for consideration of deflecton limits.</param>
        public void GetComboDeflection(ref string[] nameLoadCombinations)
        {
            _callCode = _sapModel.DesignCompositeBeam.GetComboDeflection(ref _numberOfItems, ref nameLoadCombinations);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Selects or deselects a load combination for deflection design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for deflection design. 
        /// False: The combination is not selected for deflection design.</param>
        public void SetComboDeflection(string nameLoadCombination,
            bool selectLoadCombination)
        {
            _callCode = _sapModel.DesignCompositeBeam.SetComboDeflection(nameLoadCombination, selectLoadCombination);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <param name="nameLoadCombinations">The name of the load combinations selected.</param>
        public void GetComboStrength(ref string[] nameLoadCombinations)
        {
            _callCode = _sapModel.DesignCompositeBeam.GetComboStrength(ref _numberOfItems, ref nameLoadCombinations);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Selects or deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for strength design. 
        /// False: The combination is not selected for strength design.</param>
        public void SetComboStrength(string nameLoadCombination,
            bool selectLoadCombination)
        {
            _callCode = _sapModel.DesignCompositeBeam.SetComboStrength(nameLoadCombination, selectLoadCombination);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // === Get ===       

        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="designSections">The design sections.</param>
        /// <param name="beamFy">The beam steel yield strengths, Fy. [F/L^2].</param>
        /// <param name="studDiameters">The stud diameters. [L].</param>
        /// <param name="studLayouts">The stud layouts.</param>
        /// <param name="isBeamShored">True: The is beam shored.</param>
        /// <param name="beamCambers">The beam cambers. [L]</param>
        /// <param name="passFail">The pass fail design status.</param>
        /// <param name="reactionsLeft">The left support reactions.</param>
        /// <param name="reactionsRight">The right support reactions.</param>
        /// <param name="MMaxNegative">The maximum negative moment.</param>
        /// <param name="MMaxPositive">The maximum positive moment.</param>
        /// <param name="percentCompositeConnection">The percent composite connection (PCC).</param>
        /// <param name="overallRatios">The overall ratios.</param>
        /// <param name="studRatios">The stud ratios.</param>
        /// <param name="strengthRatiosPM">The strength ratios considering PM (Axial &amp; Bending).</param>
        /// <param name="constructionRatiosPM">The construction ratios considering PM (Axial &amp; Bending).</param>
        /// <param name="strengthShearRatios">The strength shear ratios.</param>
        /// <param name="constructionShearRatios">The construction shear ratios.</param>
        /// <param name="deflectionRatiosPostConcreteDL">The deflection ratios post-concrete, DL (Dead Load).</param>
        /// <param name="deflectionRatiosSDL">The deflection ratios, SDL (Specified Dead Load).</param>
        /// <param name="deflectionRatiosLL">The deflection ratios, LL (Live Load).</param>
        /// <param name="deflectionRatiosTotalCamber">The deflection ratios from total camber.</param>
        /// <param name="frequencyRatios">The walking frequency ratios.</param>
        /// <param name="MDampingRatios">The M damping ratios.</param>
        /// <param name="itemType">This is one of the following items in the eItemType enumeration: Object = 0, Group = 1, SelectedObjects = 2
        /// If this item is Object, the design results are retrieved for the frame object specified by the Name item.
        /// If this item is Group, the design results are retrieved for all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, the design results are retrieved for all selected frame objects, and the Name item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSummaryResults(string name,
            ref string[] designSections,
            ref double[] beamFy,
            ref double[] studDiameters,
            ref string[] studLayouts,
            ref bool[] isBeamShored,
            ref double[] beamCambers,
            ref string[] passFail,
            ref double[] reactionsLeft,
            ref double[] reactionsRight,
            ref double[] MMaxNegative,
            ref double[] MMaxPositive,
            ref double[] percentCompositeConnection,
            ref double[] overallRatios,
            ref double[] studRatios,
            ref double[] strengthRatiosPM,
            ref double[] constructionRatiosPM,
            ref double[] strengthShearRatios,
            ref double[] constructionShearRatios,
            ref double[] deflectionRatiosPostConcreteDL,
            ref double[] deflectionRatiosSDL,
            ref double[] deflectionRatiosLL,
            ref double[] deflectionRatiosTotalCamber,
            ref double[] frequencyRatios,
            ref double[] MDampingRatios,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignCompositeBeam.GetSummaryResults(name, ref _numberOfItems, 
                            ref designSections, ref beamFy, ref studDiameters, ref studLayouts, ref isBeamShored, ref beamCambers,
                            ref passFail, ref reactionsLeft, ref reactionsRight, ref MMaxNegative, ref MMaxPositive, ref percentCompositeConnection,
                            ref overallRatios, ref studRatios, ref strengthRatiosPM, ref constructionRatiosPM, ref strengthShearRatios, ref constructionShearRatios,
                            ref deflectionRatiosPostConcreteDL, ref deflectionRatiosSDL, ref deflectionRatiosLL, ref deflectionRatiosTotalCamber, 
                            ref frequencyRatios, ref MDampingRatios,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // === Set
        /// <summary>
        /// Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Selection type to use for applying the method.</param>
        public void SetAutoSelectNull(string itemName,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignCompositeBeam.SetAutoSelectNull(itemName,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion


#region Methods: Public

        // === Get/Set ===
        /// <summary>
        /// Retrieves lateral displacement targets for steel design.
        /// </summary>
        /// <param name="numberItems">Number of lateral displacement targets specified.</param>
        /// <param name="loadCase">Name of the static linear load case associated with each lateral displacement target.</param>
        /// <param name="namePoint">Name of the point object associated to which the lateral displacement target applies.</param>
        /// <param name="displacementTargets">Lateral displacement targets. [L]</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified lateral displacement targets are active. 
        /// False: They are inactive.</param>
        public void GetTargetDisplacement(ref int numberItems,
            ref string[] loadCase,
            ref string[] namePoint,
            ref double[] displacementTargets,
            ref bool allSpecifiedTargetsActive)
        {
            _callCode = _sapModel.DesignCompositeBeam.GetTargetDispl(ref numberItems, ref loadCase, ref namePoint, ref displacementTargets, ref allSpecifiedTargetsActive);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets lateral displacement targets for steel design.
        /// </summary>
        /// <param name="numberItems">Number of lateral displacement targets specified.</param>
        /// <param name="loadCase">Name of the static linear load case associated with each lateral displacement target.</param>
        /// <param name="namePoint">Name of the point object associated to which the lateral displacement target applies.</param>
        /// <param name="displacementTargets">Lateral displacement targets. [L]</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified lateral displacement targets are active. 
        /// False: They are inactive.</param>
        public void SetTargetDisplacement(int numberItems,
            ref string[] loadCase,
            ref string[] namePoint,
            ref double[] displacementTargets,
            bool allSpecifiedTargetsActive)
        {
            _callCode = _sapModel.DesignCompositeBeam.SetTargetDispl(numberItems, ref loadCase, ref namePoint, ref displacementTargets, allSpecifiedTargetsActive);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// Retrieves time period targets for steel design.
        /// </summary>
        /// <param name="numberItems">Number of targets specified.</param>
        /// <param name="modalCase">Name of the modal load case for which the target applies.</param>
        /// <param name="modeNumbers">Mode number associated with each target.</param>
        /// <param name="periodTargets">Target periods. [s]</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active. 
        /// False: They are inactive.</param>
        public void GetTargetPeriod(ref int numberItems,
            ref string modalCase,
            ref int[] modeNumbers,
            ref double[] periodTargets,
            ref bool allSpecifiedTargetsActive)
        {
            _callCode = _sapModel.DesignCompositeBeam.GetTargetPeriod(ref numberItems, ref modalCase, ref modeNumbers, ref periodTargets, ref allSpecifiedTargetsActive);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets time period targets for steel design.
        /// </summary>
        /// <param name="numberItems">Number of targets specified.</param>
        /// <param name="modalCase">Name of the modal load case for which the target applies.</param>
        /// <param name="modeNumbers">Mode number associated with each target.</param>
        /// <param name="periodTargets">Target periods. [s]</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active. 
        /// False: They are inactive.</param>
        public void SetTargetPeriod(int numberItems,
            string modalCase,
            ref int[] modeNumbers,
            ref double[] periodTargets,
            bool allSpecifiedTargetsActive)
        {
            _callCode = _sapModel.DesignCompositeBeam.SetTargetPeriod(numberItems, modalCase, ref modeNumbers, ref periodTargets, allSpecifiedTargetsActive);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif