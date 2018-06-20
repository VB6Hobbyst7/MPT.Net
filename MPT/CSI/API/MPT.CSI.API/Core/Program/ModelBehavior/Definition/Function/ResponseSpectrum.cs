// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="ResponseSpectrum.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function
{
    /// <summary>
    /// Represents the response spectrum function in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.IResponseSpectrum" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class ResponseSpectrum : CSiApiBase, IResponseSpectrum
    {
        #region Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;
        /// <summary>
        /// The user
        /// </summary>
        private User _user;
        /// <summary>
        /// The file
        /// </summary>
        private File _file;
#endif
        #endregion

        #region Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Response spectrum function according to user definitions.
        /// </summary>
        /// <value>The user.</value>
        public User User => _user ?? (_user = new User(_seed));

        /// <summary>
        /// Response spectrum function according to a file definition.
        /// </summary>
        /// <value>The file.</value>
        public File File => _file ?? (_file = new File(_seed));


        /// <summary>
        /// Response spectrum function.
        /// </summary>
        /// <value>The response spectrum function.</value>
        public ResponseSpectrumFunction ResponseSpectrumFunction { get; private set; }
#endif
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseSpectrum" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public ResponseSpectrum(CSiApiSeed seed) : base(seed) { }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseSpectrum" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="responseSpectrumFunction">The response spectrum function for the class to use.</param>
        public ResponseSpectrum(CSiApiSeed seed, ResponseSpectrumFunction responseSpectrumFunction) : base(seed)
        {
            _seed = seed;
            ResponseSpectrumFunction = responseSpectrumFunction;
        }
#endif
        #endregion

        #region Methods: By Code        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the auto seismic load code used by the class.
        /// </summary>
        /// <param name="responseSpectrumFunction">The response spectrum function for the class to use.</param>
        public void SetAutoCode(ResponseSpectrumFunction responseSpectrumFunction)
        {
            ResponseSpectrumFunction = responseSpectrumFunction;
        }
#endif
        #endregion
    }
}
