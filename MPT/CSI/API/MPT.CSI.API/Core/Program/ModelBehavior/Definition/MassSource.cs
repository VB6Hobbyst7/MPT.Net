// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="MassSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents the mass source in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.IMassSource" />
    public class MassSource : CSiApiBase, IMassSource
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="MassSource" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public MassSource(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function changes the name of an existing mass source.
        /// If the new name already exists, a nonzero value is returned and the mass source name is not changed.
        /// </summary>
        /// <param name="nameMassSource">The name of an existing mass source.</param>
        /// <param name="newName">The new name for the mass source.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string nameMassSource,
            string newName)
        {
            // TODO: Handle: If the new name already exists, a nonzero value is returned and the mass source name is not changed.

            _callCode = _sapModel.SourceMass.ChangeName(nameMassSource, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function returns the number of defined mass sources.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.SourceMass.Count();
        }


        /// <summary>
        /// This function deletes an existing mass source.
        /// If the mass source to be deleted is the default mass source, a nonzero value is returned and th mass source is not deleted.
        /// </summary>
        /// <param name="nameMassSource">The name of the mass source to be deleted.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string nameMassSource)
        {
            // TODO: Handle: If the mass source to be deleted is the default mass source, a nonzero value is returned and th mass source is not deleted.

            _callCode = _sapModel.SourceMass.Delete(nameMassSource);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        // === Get/Set ===
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the names of all defined mass sources.
        /// </summary>
        /// <param name="namesMassSource">Mass source names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] namesMassSource)
        {
            namesMassSource = new string[0];
            _callCode = _sapModel.SourceMass.GetNameList(ref _numberOfItems, ref namesMassSource);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// This function retrieves the default mass source name.
        /// </summary>
        /// <param name="nameMassSource">The name of the mass source to be flagged as the default mass source.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDefault(ref string nameMassSource)
        {
            _callCode = _sapModel.SourceMass.GetDefault(ref nameMassSource);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the default mass source.
        /// Only one mass source can be the default mass source so when this assignment is set all other mass sources are automatically set to have their IsDefault flag False.
        /// </summary>
        /// <param name="nameMassSource">The name of the mass source to be flagged as the default mass source.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDefault(string nameMassSource)
        {
            _callCode = _sapModel.SourceMass.SetDefault(nameMassSource);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// This function gets the mass source data for an existing mass source.
        /// </summary>
        /// <param name="nameMassSource">The mass source name.</param>
        /// <param name="massFromElements">True: Element self mass is included in the mass.</param>
        /// <param name="massFromMasses">True: Assigned masses are included in the mass.</param>
        /// <param name="massFromLoads">True: Specified load patterns are included in the mass.</param>
        /// <param name="isDefault">True: Mass source is the default mass source.
        /// Only one mass source can be the default mass source so when this assignment is True all other mass sources are automatically set to have the IsDefault flag False.</param>
        /// <param name="namesLoadPatterns">This is an array of load pattern names specified for the mass source.</param>
        /// <param name="scaleFactors">This is an array of load pattern multipliers specified for the mass source.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMassSource(string nameMassSource,
            ref bool massFromElements,
            ref bool massFromMasses,
            ref bool massFromLoads,
            ref bool isDefault,
            ref string[] namesLoadPatterns,
            ref double[] scaleFactors)
        {
            _callCode = _sapModel.SourceMass.GetMassSource(nameMassSource, 
                            ref massFromElements, ref massFromMasses, ref massFromLoads, ref isDefault, 
                            ref _numberOfItems, ref namesLoadPatterns, ref scaleFactors);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function adds a new mass source to the model or reinitializes an existing mass source.
        /// </summary>
        /// <param name="nameMassSource">The mass source name.
        /// If a mass source with this name already exists then the mass source is reinitialized with the new data.
        /// All previous data assigned to the mass source is lost.
        /// If a mass source with this name does not exist then a new mass source is added.</param>
        /// <param name="massFromElements">True: Element self mass is included in the mass.</param>
        /// <param name="massFromMasses">True: Assigned masses are included in the mass.</param>
        /// <param name="massFromLoads">True: Specified load patterns are included in the mass.</param>
        /// <param name="isDefault">True: Mass source is the default mass source.
        /// Only one mass source can be the default mass source so when this assignment is True all other mass sources are automatically set to have the IsDefault flag False.</param>
        /// <param name="namesLoadPatterns">This is an array of load pattern names specified for the mass source.</param>
        /// <param name="scaleFactors">This is an array of load pattern multipliers specified for the mass source.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMassSource(string nameMassSource,
            bool massFromElements,
            bool massFromMasses,
            bool massFromLoads,
            bool isDefault,
            string[] namesLoadPatterns,
            double[] scaleFactors)
        {
            arraysLengthMatch(nameof(namesLoadPatterns), namesLoadPatterns.Length, nameof(scaleFactors), scaleFactors.Length);

            _callCode = _sapModel.SourceMass.SetMassSource(nameMassSource, 
                            massFromElements, massFromMasses, massFromLoads, isDefault, 
                            namesLoadPatterns.Length, ref namesLoadPatterns, ref scaleFactors);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
