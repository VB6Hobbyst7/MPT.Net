// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="AutoSeismic.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern
{
    /// <summary>
    /// Represents an auto seismic load pattern in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.IAutoSeismic" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class AutoSeismic : CSiApiBase, IAutoSeismic
    {
        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The user
        /// </summary>
        private User _user;
#endif
        #endregion

        #region Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Auto seismic load pattern according to user definitions.
        /// </summary>
        /// <value>The user.</value>
        public User User => _user ?? (_user = new User(_seed));
#endif

        /// <summary>
        /// Auto seismic load pattern.
        /// </summary>
        /// <value>The automatic seismic load.</value>
        public AutoSeismicLoad AutoSeismicLoad { get; private set; }
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoSeismic" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AutoSeismic(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AutoSeismic" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="autoSeismicLoad">The auto seismic load for the class to use.</param>
        public AutoSeismic(CSiApiSeed seed, AutoSeismicLoad autoSeismicLoad) : base(seed)
        {
            _seed = seed;
            AutoSeismicLoad = autoSeismicLoad;
        }
        #endregion

        #region Methods: Interface
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the code name used for auto seismic parameters in Quake-type load patterns.
        /// An error is returned if the specified load pattern is not a Quake-type load pattern.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern.</param>
        /// <param name="codeName">This is either blank or the name code used for the auto seismic parameters.
        /// Blank means no auto seismic load is specified for the Quake-type load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAutoCode(string name,
            ref string codeName)
        {
            _callCode = _sapModel.LoadPatterns.GetAutoSeismicCode(name, ref codeName);

            // TODO: Figure out how to handle: An error is returned if the specified load pattern is not a Quake-type load pattern.

            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Sets the auto load code used by the class.
        /// </summary>
        /// <param name="autoLoad">The auto load for the class to use.</param>
        public void SetAutoCode(AutoLoad autoLoad)
        {
            AutoSeismicLoad newAutoLoad = autoLoad as AutoSeismicLoad;
            if (newAutoLoad == null) { return; }
            SetAutoCode(newAutoLoad);
        }


        /// <summary>
        /// Sets the auto seismic load code used by the class.
        /// </summary>
        /// <param name="autoSeismicLoad">The auto seismic load for the class to use.</param>
        public void SetAutoCode(AutoSeismicLoad autoSeismicLoad)
        {
            AutoSeismicLoad = autoSeismicLoad;
        }

        /// <summary>
        /// This function sets the auto seismic loading type for the specified load pattern to None.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetNone(string name)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetNone(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Methods: Public
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves diaphragm eccentricity overrides for auto seismic loads.
        /// This function does not apply for User Load type auto seismic loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Quake-type load pattern that has an auto seismic load assigned.</param>
        /// <param name="diaphragmNames">This is an array that includes the names of the diaphragms which have eccentricity overrides.</param>
        /// <param name="eccentricities">This is an array that includes the eccentricity applied to each diaphragm. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDiaphragmEccentricityOverride(string patternName,
            ref string[] diaphragmNames,
            ref double[] eccentricities)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetDiaphragmEccentricityOverride(patternName, ref _numberOfItems, ref diaphragmNames, ref eccentricities);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns diaphragm eccentricity overrides for auto seismic loads.
        /// This function does not apply for User Load type auto seismic loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Quake-type load pattern that has an auto seismic load assigned.</param>
        /// <param name="diaphragmName">The name of an existing special rigid diaphragm constraint, that is, a diaphragm constraint with the following features:
        /// 1. The constraint type is CONSTRAINT_DIAPHRAGM = 2.
        /// 2. The constraint coordinate system is Global.
        /// 3. The constraint axis is Z.</param>
        /// <param name="eccentricity">The eccentricity applied to the specified diaphragm. [L</param>
        /// <param name="delete">True: Eccentricity override for the specified diaphragm is deleted.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDiaphragmEccentricityOverride(string patternName,
            string diaphragmName,
            double eccentricity,
            bool delete = false)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetDiaphragmEccentricityOverride(patternName, diaphragmName, eccentricity, delete);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
        
       



       



       


        


       
    }
}
