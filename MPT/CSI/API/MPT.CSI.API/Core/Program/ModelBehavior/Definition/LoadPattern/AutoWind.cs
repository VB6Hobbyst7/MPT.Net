// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-12-2017
//
// Last Modified By : Mark
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="AutoWind.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind;
using User = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind.User;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern
{
    /// <summary>
    /// Represents an auto wind load pattern in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.IAutoWind" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class AutoWind : CSiApiBase, IAutoWind
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
        /// Auto wind load pattern according to user definitions.
        /// </summary>
        /// <value>The user.</value>
        public User User => _user ?? (_user = new User(_seed));

        /// <summary>
        /// Auto wind load pattern.
        /// </summary>
        /// <value>The automatic wind load.</value>
        public AutoWindLoad AutoWindLoad { get; private set; }
#endif
        #endregion


        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoWind" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AutoWind(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoWind" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="autoWindLoad">The auto wind load for the class to use.</param>
        public AutoWind(CSiApiSeed seed, AutoWindLoad autoWindLoad) : base(seed)
        {
            _seed = seed;
            AutoWindLoad = autoWindLoad;
        }
#endif
        #endregion

        #region Methods: Interface
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the code name used for auto seismic parameters in Wind-type load patterns.
        /// An error is returned if the specified load pattern is not a Wind-type load pattern.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load pattern.</param>
        /// <param name="codeName">This is either blank or the name code used for the auto wind parameters.
        /// Blank means no auto wind load is specified for the Wind-type load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAutoCode(string name,
            ref string codeName)
        {
            _callCode = _sapModel.LoadPatterns.GetAutoWindCode(name, ref codeName);

            // TODO: Figure out how to handle: An error is returned if the specified load pattern is not a Wind-type load pattern.

            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the auto load code used by the class.
        /// </summary>
        /// <param name="autoLoad">The auto load for the class to use.</param>
        public void SetAutoCode(AutoLoad autoLoad)
        {
            AutoWindLoad newAutoLoad  = autoLoad as AutoWindLoad;
            if (newAutoLoad == null) { return; }
            SetAutoCode(newAutoLoad);
        }


        /// <summary>
        /// Sets the auto wind load code used by the class.
        /// </summary>
        /// <param name="autoWindLoad">The auto wind load for the class to use.</param>
        public void SetAutoCode(AutoWindLoad autoWindLoad)
        {
            AutoWindLoad = autoWindLoad;
        }

        /// <summary>
        /// This function set the auto wind loading type for the specified load pattern to None.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetNone(string name)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.SetNone(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Methods: Public
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_CSiBridgev16 || BUILD_CSiBridgev17
        /// <summary>
        /// This function retrieves exposure parameters for auto wind loads determined from extents of rigid diaphragms.
        /// This function does not apply for User-type auto wind loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Wind-type load pattern that has an auto wind load assigned.</param>
        /// <param name="diaphragmNames">This is an array that includes the names of the diaphragms that have eccentricity overrides.</param>
        /// <param name="xCoordinates">This is an array that includes the global X-coordinate of the point where the wind force load is applied to the diaphragm. [L]</param>
        /// <param name="yCoordinates">This is an array that includes the global Y-coordinate of the point where the wind force load is applied to the diaphragm. [L]</param>
        /// <param name="diaphragmWidth">This is an array that includes the exposure width for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmHeight">This is an array that includes the exposure height for the wind load applied to the specified diaphragm. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetExposure(string patternName,
            ref string[] diaphragmNames,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] diaphragmWidth,
            ref double[] diaphragmHeight)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.GetExposure(patternName,
                            ref _numberOfItems,
                            ref diaphragmNames,
                            ref xCoordinates,
                            ref yCoordinates,
                            ref diaphragmWidth,
                            ref diaphragmHeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns exposure parameters for auto wind loads determined from extents of rigid diaphragms.
        /// This function does not apply for User-type auto wind loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Wind-type load pattern that has an auto wind load assigned.</param>
        /// <param name="diaphragmName">The name of an existing special rigid diaphragm constraint, that is, a diaphragm constraint with the following features:
        /// 1. The constraint type is CONSTRAINT_DIAPHRAGM = 2.
        /// 2. The constraint coordinate system is Global.
        /// 3. The constraint axis is Z.</param>
        /// <param name="xCoordinate">The global X-coordinate of the point where the wind force is applied. [L]</param>
        /// <param name="yCoordinate">The global Y-coordinate of the point where the wind force is applied. [L]</param>
        /// <param name="diaphragmWidth">The exposure width for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmHeight">The exposure height for the wind load applied to the specified diaphragm. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetExposure(string patternName,
            string diaphragmName,
            double xCoordinate,
            double yCoordinate,
            double diaphragmWidth,
            double diaphragmHeight)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.SetExposure(patternName,
                            diaphragmName,
                            xCoordinate,
                            yCoordinate,
                            diaphragmWidth,
                            diaphragmHeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#else
        /// <summary>
        /// This function retrieves exposure parameters for auto wind loads determined from extents of rigid diaphragms.
        /// This function does not apply for User-type auto wind loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Wind-type load pattern that has an auto wind load assigned.</param>
        /// <param name="diaphragmNames">This is an array that includes the names of the diaphragms that have eccentricity overrides.</param>
        /// <param name="xCoordinates">This is an array that includes the global X-coordinate of the point where the wind force load is applied to the diaphragm. [L]</param>
        /// <param name="yCoordinates">This is an array that includes the global Y-coordinate of the point where the wind force load is applied to the diaphragm. [L]</param>
        /// <param name="diaphragmWidth">This is an array that includes the exposure width for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmDepth">This is an array that includes the exposure depth for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmHeight">This is an array that includes the exposure height for the wind load applied to the specified diaphragm. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetExposure(string patternName,
            ref string[] diaphragmNames,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] diaphragmWidth,
            ref double[] diaphragmDepth,
            ref double[] diaphragmHeight)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.GetExposure_1(patternName, ref _numberOfItems, ref diaphragmNames, ref xCoordinates, ref yCoordinates, ref diaphragmWidth, ref diaphragmDepth, ref diaphragmHeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns exposure parameters for auto wind loads determined from extents of rigid diaphragms.
        /// This function does not apply for User-type auto wind loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Wind-type load pattern that has an auto wind load assigned.</param>
        /// <param name="diaphragmName">The name of an existing special rigid diaphragm constraint, that is, a diaphragm constraint with the following features:
        /// 1. The constraint type is CONSTRAINT_DIAPHRAGM = 2.
        /// 2. The constraint coordinate system is Global.
        /// 3. The constraint axis is Z.</param>
        /// <param name="xCoordinate">The global X-coordinate of the point where the wind force is applied. [L]</param>
        /// <param name="yCoordinate">The global Y-coordinate of the point where the wind force is applied. [L]</param>
        /// <param name="diaphragmWidth">The exposure width for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmDepth">The exposure depth for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmHeight">The exposure height for the wind load applied to the specified diaphragm. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetExposure(string patternName,
            string diaphragmName,
            double xCoordinate,
            double yCoordinate,
            double diaphragmWidth,
            double diaphragmDepth,
            double diaphragmHeight)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.SetExposure_1(patternName, diaphragmName, xCoordinate, yCoordinate, diaphragmWidth, diaphragmDepth, diaphragmHeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
#endif