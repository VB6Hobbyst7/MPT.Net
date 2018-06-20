// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="IFunctions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements the functions in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IFunctions: IChangeableName, ICountable, IDeletable, IListableNames
    {
        #region Properties                    

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the power spectral density function.
        /// </summary>
        /// <value>The power spectral density function.</value>
        PowerSpectralDensity PowerSpectralDensity { get; }

        /// <summary>
        /// Gets the state of the steady function.
        /// </summary>
        /// <value>The state of the steady function.</value>
        SteadyState SteadyState  { get; }
#endif
        /// <summary>
        /// Gets the response spectrum function.
        /// </summary>
        /// <value>The response spectrum function.</value>
        ResponseSpectrum ResponseSpectrum { get; }

        /// <summary>
        /// Gets the time history function.
        /// </summary>
        /// <value>The time history function.</value>
        TimeHistory TimeHistoryDirectLinear { get; }
        #endregion

        #region Methods: Public

        /// <summary>
        /// This function retrieves the function type for the specified function.
        /// </summary>
        /// <param name="name">The name of an existing function.</param>
        /// <param name="functionType">This is the main type of function.</param>
        /// <param name="functionSubType">This is the function subtype, which is dependent upond the function type.
        /// See enumerations for code definitions.</param>
        void GetType(string name,
            ref eFunctionType functionType,
            ref int functionSubType);

        /// <summary>
        /// This function retrieves the time and function values for any defined function.
        /// </summary>
        /// <param name="name">The name of an existing function.</param>
        /// <param name="numberItems">The number of time and function value pairs retrieved.</param>
        /// <param name="timeValue">This is an array that includes the time value for each data point.
        /// [sec] for response spectrum and time history functions, [cyc/sec] for power spectral density and steady state functions</param>
        /// <param name="functionValue">This is an array that includes the function value for each data point.</param>
        void GetValues(string name,
            ref int numberItems,
            ref double[] timeValue,
            ref double[] functionValue);

        /// <summary>
        /// This function converts an existing function to a user defined function.
        /// An error is returned if the specified function is already a user defined function.
        /// </summary>
        /// <param name="name">The name of an existing function that is not a user defined function.</param>
        void ConvertToUser(string name);

        #endregion
    }
}