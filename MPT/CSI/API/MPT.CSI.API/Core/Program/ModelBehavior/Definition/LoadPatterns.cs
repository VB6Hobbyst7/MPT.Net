// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="LoadPatterns.cs" company="">
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
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents load patterns in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.ILoadPatterns" />
    public class LoadPatterns : CSiApiBase, ILoadPatterns
    {
        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

        /// <summary>
        /// The automatic seismic pattern
        /// </summary>
        private AutoSeismic _autoSeismicPattern;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The automatic wind pattern
        /// </summary>
        private AutoWind _autoWindPattern;

        /// <summary>
        /// The automatic wave pattern
        /// </summary>
        private AutoWave _autoWavePattern;
#endif
        #endregion


        #region Properties

        /// <summary>
        /// Represents an auto seismic load pattern in the application.
        /// </summary>
        /// <value>The automatic seismic pattern.</value>
        public AutoSeismic AutoSeismicPattern => _autoSeismicPattern ?? (_autoSeismicPattern = new AutoSeismic(_seed));


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Represents an auto wind load pattern in the application.
        /// </summary>
        /// <value>The automatic wind pattern.</value>
        public AutoWind AutoWindPattern => _autoWindPattern ?? (_autoWindPattern = new AutoWind(_seed));

        /// <summary>
        /// Represents an auto wave load pattern in the application.
        /// </summary>
        /// <value>The automatic wave pattern.</value>
        public AutoWave AutoWavePattern => _autoWavePattern ?? (_autoWavePattern = new AutoWave(_seed));
#endif
        #endregion


        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadPatterns" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public LoadPatterns(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Adds a new load pattern.
        /// An error is returned if the <paramref name="name" /> item is already used for an existing load pattern.
        /// </summary>
        /// <param name="name">Name for the new load pattern.</param>
        /// <param name="loadPatternType">Load pattern type.</param>
        /// <param name="selfWeightMultiplier">Self weight multiplier for the new load pattern.</param>
        /// <param name="addLoadCase">True: A linear static load case corresponding to the new load pattern is added.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Add(string name,
            eLoadPatternType loadPatternType,
            double selfWeightMultiplier = 0,
            bool addLoadCase = true)
        {
            // TODO: Decide how to handle this: An error is returned if the Name item is already used for an existing load pattern. 

            _callCode = _sapModel.LoadPatterns.Add(name, 
                            EnumLibrary.Convert<eLoadPatternType, CSiProgram.eLoadPatternType>(loadPatternType), 
                            selfWeightMultiplier, addLoadCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// The number of defined load patterns.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.LoadPatterns.Count();
        }

        /// <summary>
        /// The function deletes the specified load pattern.
        /// The load pattern is not deleted and the function returns an error if the load pattern is assigned to an load case or if it is the only defined load pattern.
        /// </summary>
        /// <param name="name">The name of an existing load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            // TODO: Decide how to handle this: The load pattern is not deleted and the function returns an error if the load pattern is assigned to an load case or if it is the only defined load pattern.

            _callCode = _sapModel.LoadPatterns.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined load cases.
        /// </summary>
        /// <param name="loadPatternNames">This is a one-dimensional array of load pattern names.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] loadPatternNames)
        {
            loadPatternNames = new string[0];
            _callCode = _sapModel.LoadPatterns.GetNameList(ref _numberOfItems, ref loadPatternNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function applies a new name to a load pattern.
        /// </summary>
        /// <param name="name">The name of a defined load pattern.</param>
        /// <param name="newName">The new name for the load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string name,
            string newName)
        {
            _callCode = _sapModel.LoadPatterns.ChangeName(name, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // === Get/Set


        /// <summary>
        /// This function retrieves the load type for a specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing load pattern.</param>
        /// <param name="loadPatternType">This is one of the items in the eLoadPatternType enumeration.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadType(string name,
            ref eLoadPatternType loadPatternType)
        {
            CSiProgram.eLoadPatternType csiLoadPatternType = CSiProgram.eLoadPatternType.Other;
            _callCode = _sapModel.LoadPatterns.GetLoadType(name, ref csiLoadPatternType);

            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadPatternType = EnumLibrary.Convert(csiLoadPatternType, loadPatternType);
        }

        /// <summary>
        /// This function assigns a load type to a load pattern.
        /// </summary>
        /// <param name="name">The name of an existing load pattern.</param>
        /// <param name="loadPatternType">This is one of the items in the eLoadPatternType enumeration.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadType(string name,
            eLoadPatternType loadPatternType)
        {
            _callCode = _sapModel.LoadPatterns.SetLoadType(name, 
                            EnumLibrary.Convert<eLoadPatternType, CSiProgram.eLoadPatternType>(loadPatternType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// This function retrieves the self weight multiplier for a specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing load pattern.</param>
        /// <param name="selfWeightMultiplier">The self weight multiplier for the specified load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSelfWtMultiplier(string name,
            ref double selfWeightMultiplier)
        {
            _callCode = _sapModel.LoadPatterns.GetSelfWTMultiplier(name, ref selfWeightMultiplier);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns a self weight multiplier to a load case.
        /// </summary>
        /// <param name="name">The name of an existing load pattern.</param>
        /// <param name="selfWeightMultiplier">The self weight multiplier for the specified load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSelfWtMultiplier(string name,
            double selfWeightMultiplier)
        {
            _callCode = _sapModel.LoadPatterns.SetSelfWTMultiplier(name, selfWeightMultiplier);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// Retrieves the code name used for auto seismic parameters in Quake-type load patterns.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern.</param>
        /// <param name="codeName">This is either blank or the name code used for the auto seismic parameters.
        /// Blank means no auto seismic load is specified for the Quake-type load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAutoSeismicCode(string name, ref string codeName)
        {
            _callCode = _sapModel.LoadPatterns.GetAutoSeismicCode(name, ref codeName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the code name used for auto wind parameters in wind-type load patterns.
        /// </summary>
        /// <param name="name">The name of an existing wind-type load pattern.</param>
        /// <param name="codeName">This is either blank or the name code used for the auto wind parameters.
        /// Blank means no auto wind load is specified for the wind-type load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAutoWindCode(string name, ref string codeName)
        {
            _callCode = _sapModel.LoadPatterns.GetAutoWindCode(name, ref codeName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Retrieves the code name used for auto wave parameters in wave-type load patterns.
        /// </summary>
        /// <param name="name">The name of an existing wave-type load pattern.</param>
        /// <param name="codeName">This is either blank or the name code used for the auto wave parameters.
        /// Blank means no auto wave load is specified for the wave-type load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAutoWaveCode(string name, ref string codeName)
        {
            _callCode = _sapModel.LoadPatterns.GetAutoWaveCode(name, ref codeName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
