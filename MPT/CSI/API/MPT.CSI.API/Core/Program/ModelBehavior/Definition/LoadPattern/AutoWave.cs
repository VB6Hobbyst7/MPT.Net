// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="AutoWave.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wave;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern
{
    /// <summary>
    /// Represents an auto wave load pattern in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.IAutoWave" />
    public class AutoWave : CSiApiBase, IAutoWave
    {
        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

        /// <summary>
        /// The wave load generic
        /// </summary>
        private WaveLoadGeneric _WaveLoadGeneric;
        #endregion

        #region Properties
        /// <summary>
        /// Auto wave load pattern according to generic definitions.
        /// </summary>
        /// <value>The wave load generic.</value>
        public WaveLoadGeneric WaveLoadGeneric => _WaveLoadGeneric ?? (_WaveLoadGeneric = new WaveLoadGeneric(_seed));

        /// <summary>
        /// Auto wave load pattern.
        /// </summary>
        /// <value>The automatic wave load.</value>
        public AutoWaveLoad AutoWaveLoad { get; private set; }
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoWave" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AutoWave(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AutoSeismic" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="autoWindLoad">The auto wind load for the class to use.</param>
        public AutoWave(CSiApiSeed seed, AutoWaveLoad autoWindLoad) : base(seed)
        {
            _seed = seed;
            AutoWaveLoad = autoWindLoad;
        }
        #endregion

        #region Methods: Interface

        /// <summary>
        /// This function retrieves the code name used for auto wave parameters in Wave-type load patterns.
        /// An error is returned if the specified load pattern is not a Wave-type load pattern.
        /// </summary>
        /// <param name="name">The name of an existing Wave-type load pattern.</param>
        /// <param name="codeName">This is either blank or the name code used for the auto wave parameters.
        /// Blank means no auto wave load is specified for the Wave-type load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAutoCode(string name,
            ref string codeName)
        {
            _callCode = _sapModel.LoadPatterns.GetAutoWaveCode(name, ref codeName);

            // TODO: Figure out how to handle: An error is returned if the specified load pattern is not a Wave-type load pattern.

            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// Sets the auto load code used by the class.
        /// </summary>
        /// <param name="autoLoad">The auto load for the class to use.</param>
        public void SetAutoCode(AutoLoad autoLoad)
        {
            AutoWaveLoad newAutoLoad = autoLoad as AutoWaveLoad;
            if (newAutoLoad == null) { return; }
            SetAutoCode(newAutoLoad);
        }


        /// <summary>
        /// Sets the auto wave load code used by the class.
        /// </summary>
        /// <param name="autoWaveLoad">The auto wave load for the class to use.</param>
        public void SetAutoCode(AutoWaveLoad autoWaveLoad)
        {
            AutoWaveLoad = autoWaveLoad;
        }

        /// <summary>
        /// This function sets the auto seastate loading type for the specified load pattern to None.
        /// </summary>
        /// <param name="name">The name of an existing seastate-type load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetNone(string name)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeastate.SetNone(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#endregion

    }
}
#endif
