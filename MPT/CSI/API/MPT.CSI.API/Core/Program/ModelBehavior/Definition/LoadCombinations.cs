// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="LoadCombinations.cs" company="">
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
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the load combinations in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.ILoadCombinations" />
    public class LoadCombinations : CSiApiBase, ILoadCombinations
    {
#else
    /// <summary>
    /// Represents the load combinations in the application.
    /// </summary>
    public class LoadCombinations : CSiApiBase, IDeletable, IListableNames
    {
#endif
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCombinations" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public LoadCombinations(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public

        /// <summary>
        /// This function adds a new load combination.
        /// The new load combination must have a different name from all other load combinations and all load cases.
        /// If the name is not unique, an error will be returned.
        /// </summary>
        /// <param name="nameLoadCombo">The name of a new load combination.</param>
        /// <param name="comboType">The load combination type to add.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Add(string nameLoadCombo,
            eLoadComboType comboType)
        {
            // TODO: Handle this: If the name is not unique, an error will be returned.
            _callCode = _sapModel.RespCombo.Add(nameLoadCombo, (int)comboType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function adds a new default design load combinations to the model.
        /// </summary>
        /// <param name="designSteel">True: Default steel design combinations are to be added to the model.</param>
        /// <param name="designConcrete">True: Default concrete design combinations are to be added to the model.</param>
        /// <param name="designAluminum">True: Default aluminum design combinations are to be added to the model</param>
        /// <param name="designColdFormed">True: Default cold formed design combinations are to be added to the model.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void AddDesignDefaultCombos(bool designSteel,
            bool designConcrete,
            bool designAluminum,
            bool designColdFormed)
        {
            _callCode = _sapModel.RespCombo.AddDesignDefaultCombos(designSteel, designConcrete, designAluminum, designColdFormed);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Change name of load combination.
        /// The new load combination name must be different from all other load combinations and all load cases. If the name is not unique, an error will be returned.
        /// </summary>
        /// <param name="nameLoadCombo">The existing name of a defined load combination.</param>
        /// <param name="newName">The new name for the combination.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string nameLoadCombo,
            string newName)
        {
            // TODO: Handle this: The new load combination name must be different from all other load combinations and all load cases. If the name is not unique, an error will be returned.
            _callCode = _sapModel.RespCombo.ChangeName(nameLoadCombo, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns the total number of load combinations defined in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.RespCombo.Count();
        }

        /// <summary>
        /// This function retrieves the total number of load case and/or combinations included in a specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="loadCaseComboCount">The number of load case and/or combinations included in the specified combination.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void CountCase(string nameLoadCombo,
            ref int loadCaseComboCount)
        {
            _callCode = _sapModel.RespCombo.CountCase(nameLoadCombo, ref loadCaseComboCount);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        /// <summary>
        /// This function deletes the specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string nameLoadCombo)
        {
            _callCode = _sapModel.RespCombo.Delete(nameLoadCombo);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function deletes one load case or load combination from the list of cases included in the specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="caseComboType">This parameter indicates whether the item is an analysis case (LoadCase) or a load combination (LoadCombo).</param>
        /// <param name="caseComboName">The name of the load case or load combination to be deleted from the specified combination.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void DeleteCase(string nameLoadCombo,
            eCaseComboType caseComboType,
            string caseComboName)
        {
            _callCode = _sapModel.RespCombo.DeleteCase(nameLoadCombo, 
                            EnumLibrary.Convert<eCaseComboType, CSiProgram.eCNameType>(caseComboType), 
                            caseComboName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // === Get/Set ===

        /// <summary>
        /// This function retrieves the names of all defined response combinations.
        /// </summary>
        /// <param name="namesCombo">Names of all defined response combinations.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] namesCombo)
        {
            namesCombo = new string[0];
            _callCode = _sapModel.RespCombo.GetNameList(ref _numberOfItems, ref namesCombo);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // == Case List ==

        /// <summary>
        /// This function returns all load cases and response combinations included in the load combination specified by the Name item.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="numberLoadCombos">The total number of load cases and load combinations included in the load combination specified by the Name item.</param>
        /// <param name="caseComboType">This parameter indicates whether the item is an analysis case (LoadCase) or a load combination (LoadCombo).</param>
        /// <param name="caseComboNames">This is an array of the names of the load cases or load combinations included in the load combination specified by the Name item.</param>
        /// <param name="scaleFactor">The scale factor multiplying the case or combination indicated by the caseComboNames item.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetCaseList(string nameLoadCombo,
            ref int numberLoadCombos,
            ref eCaseComboType[] caseComboType,
            ref string[] caseComboNames,
            ref double[] scaleFactor)
        {
            CSiProgram.eCNameType[] csiCaseComboType = new CSiProgram.eCNameType[0];
            _callCode = _sapModel.RespCombo.GetCaseList(nameLoadCombo, ref numberLoadCombos, ref csiCaseComboType, ref caseComboNames, ref scaleFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            caseComboType = new eCaseComboType[csiCaseComboType.Length+1];
            for (int i = 0; i < csiCaseComboType.Length; i++)
            {
                caseComboType[i] = EnumLibrary.Convert(csiCaseComboType[i], caseComboType[i]);
            }
        }

        /// <summary>
        /// This function adds or modifies one load case or response combination in the list of cases included in the load combination specified by the Name item.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="caseComboType">This parameter indicates whether the item is an analysis case (LoadCase) or a load combination (LoadCombo).</param>
        /// <param name="caseComboNames">This is an array of the names of the load cases or load combinations included in the load combination specified by the Name item.</param>
        /// <param name="scaleFactor">The scale factor multiplying the case or combination indicated by the caseComboNames item.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCaseList(string nameLoadCombo,
            eCaseComboType caseComboType,
            string caseComboNames,
            double scaleFactor)
        {
            CSiProgram.eCNameType csiCaseComboType = EnumLibrary.Convert<eCaseComboType, CSiProgram.eCNameType>(caseComboType);
            _callCode = _sapModel.RespCombo.SetCaseList(nameLoadCombo, ref csiCaseComboType, caseComboNames, scaleFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // == Note ==

        /// <summary>
        /// This function retrieves the user note for specified response combination. The note may be blank.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="note">The user note, if any, included with the specified combination.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNote(string nameLoadCombo,
            ref string note)
        {
            _callCode = _sapModel.RespCombo.GetNote(nameLoadCombo, ref note);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the user note for specified response combination. The note may be blank.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="note">The user note, if any, included with the specified combination.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetNote(string nameLoadCombo,
            string note)
        {
            _callCode = _sapModel.RespCombo.SetNote(nameLoadCombo, note);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        // == Type ==

        /// <summary>
        /// This function gets the combination type for specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="combo">The load combination type of the specified load combination.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetType(string nameLoadCombo,
            ref eLoadComboType combo)
        {
            int csiCombo = 0;
            _callCode = _sapModel.RespCombo.GetTypeOAPI(nameLoadCombo, ref csiCombo);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            combo = (eLoadComboType) csiCombo;
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function sets the combination type for specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="comboType">The load combination type of the specified load combination.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetType(string nameLoadCombo,
            eLoadComboType comboType)
        {
            _callCode = _sapModel.RespCombo.SetTypeOAPI(nameLoadCombo, (int)comboType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
