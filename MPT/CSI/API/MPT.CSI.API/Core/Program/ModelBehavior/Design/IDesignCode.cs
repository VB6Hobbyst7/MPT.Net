// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-08-2017
//
// Last Modified By : Mark
// Last Modified On : 10-03-2017
// ***********************************************************************
// <copyright file="IDesignCode.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements a design interface for all frame elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignRun" />
    public interface IDesignCode : IDesignRun
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the names of the frame objects that did not pass the design check or have not yet been checked, if any.
        /// </summary>
        /// <param name="numberNotPassedOrChecked">The number of concrete frame objects that did not pass the design check or have not yet been checked.</param>
        /// <param name="numberDidNotPass">The number of concrete frame objects that did not pass the design check.</param>
        /// <param name="numberNotChecked">The number of concrete frame objects that have not yet been checked.</param>
        /// <param name="namesNotPassedOrChecked">This is an array that includes the name of each frame object that did not pass the design check or has not yet been checked.</param>
        void VerifyPassed(ref int numberNotPassedOrChecked, ref int numberDidNotPass, ref int numberNotChecked, ref string[] namesNotPassedOrChecked);

        /// <summary>
        /// This function retrieves the names of the frame objects that have different analysis and design sections, if any.
        /// </summary>
        /// <param name="numberDifferentSections">The number of frame objects that have different analysis and design sections.</param>
        /// <param name="namesDifferentSections">This is an array that includes the name of each frame object that has different analysis and design sections.</param>
        void VerifySections(ref int numberDifferentSections, ref string[] namesDifferentSections);
#endif

        // === Get/Set Methods ===        
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        void GetCode(ref string codeName);

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        void SetCode(string codeName);

        // ===

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="name">Name of a frame object with a frame design procedure.</param>
        /// <param name="propName">The name of the design section for the specified frame object.</param>
        void GetDesignSection(string name, ref string propName);

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="nameSection">Name of an existing frame section property to be used as the design section for the specified frame objects.
        /// This item applies only when resetToLastAnalysisSection = False.</param>
        /// <param name="resetToLastAnalysisSection">True: The design section for the specified frame objects is reset to the last analysis section for the frame object.
        /// False: The design section is set to that specified by nameFrame.</param>
        /// <param name="itemType">Selection type to use for applying the method.</param>
        void SetDesignSection(string itemName, string nameSection, bool resetToLastAnalysisSection, eItemType itemType = eItemType.Object);
        
    }
}
