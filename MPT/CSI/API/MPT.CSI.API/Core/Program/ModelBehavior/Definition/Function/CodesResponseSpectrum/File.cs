// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="File.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Represents response spectrum functions read from files.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class File : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="File" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public File(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of a response spectrum function from file.
        /// </summary>
        /// <param name="name">The name of a defined response spectrum function specified to be from a text file.</param>
        /// <param name="fileName">The full path of the text file containing the function data.</param>
        /// <param name="headerLinesSkip">The number of header lines in the text file to be skipped before starting to read function data.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= DampRatio &lt; 1.</param>
        /// <param name="valueType">Type of the value used in the file.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetFunction(string name,
            ref string fileName,
            ref int headerLinesSkip,
            ref double dampingRatio,
            ref eFileValueType valueType)
        {
            int csiValueType = 0;

            // TODO: What if file path is invalid? Other ways this may fail?
            _callCode = _sapModel.Func.FuncRS.GetFromFile(name, ref fileName, ref headerLinesSkip, ref dampingRatio, ref csiValueType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            valueType = (eFileValueType)csiValueType;
        }

        /// <summary>
        /// This function defines a response spectrum function from file.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="fileName">The full path of the text file containing the function data.</param>
        /// <param name="headerLinesSkip">The number of header lines in the text file to be skipped before starting to read function data.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= DampRatio &lt; 1.</param>
        /// <param name="valueType">Type of the value used in the file.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetFunction(string name,
            string fileName,
            int headerLinesSkip,
            double dampingRatio,
            eFileValueType valueType)
        {
            // TODO: What if file path is invalid? Other ways this may fail?
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetFromFile(name, fileName, headerLinesSkip, dampingRatio, (int)valueType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif