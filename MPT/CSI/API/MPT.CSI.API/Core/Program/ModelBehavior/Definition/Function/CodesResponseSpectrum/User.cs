// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="User.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Represents user-defined response spectrum functions.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class User : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public User(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of a user defined response spectrum function.
        /// </summary>
        /// <param name="name">The name of a user defined response spectrum function.</param>
        /// <param name="periods">The frequency in Hz for each data point. [cyc/s].</param>
        /// <param name="values">The function value for each data point.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetFunction(string name,
            ref double[] periods,
            ref double[] values,
            ref double dampingRatio)
        {
            _callCode = _sapModel.Func.FuncRS.GetUser(name, ref _numberOfItems, ref periods, ref values, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function defines a user response spectrum function.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="periods">The frequency in Hz for each data point. [cyc/s].</param>
        /// <param name="values">The function value for each data point.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= DampRatio &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetFunction(string name,
            double[] periods,
            double[] values,
            double dampingRatio)
        {
            //if (periods.Length != values.Length)
            //{
            //    throw new CSiException("Periods and values are of unequal numbers. \nPeriod has " +
            //                           periods.Length + " entries, while Values has " + values.Length + " entries.");
            //}
            arraysLengthMatch(periods.Length, values.Length);
            int numberOfItems = periods.Length;

            dampingRatio = limitDampingRatio(dampingRatio);

            _callCode = _sapModel.Func.FuncRS.SetUser(name, numberOfItems, ref periods, ref values, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif