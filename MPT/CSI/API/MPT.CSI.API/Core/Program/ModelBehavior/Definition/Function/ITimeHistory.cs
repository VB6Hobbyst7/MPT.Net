// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="ITimeHistory.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function
{
    /// <summary>
    /// Implements the time history function in the application.
    /// </summary>
    public interface ITimeHistory
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        #region Methods: File & User

        /// <summary>
        /// This function retrieves the definition of a time history function from file.
        /// </summary>
        /// <param name="name">The name of a defined time history function specified to be from a text file.</param>
        /// <param name="fileName">The full path of the text file containing the function data.</param>
        /// <param name="headerLinesSkip">The number of header lines in the text file to be skipped before starting to read function data.</param>
        /// <param name="prefixCharactersSkip">The number of prefix characters to be skipped on each line in the text file.</param>
        /// <param name="pointsPerLine">The number of function points included on each text file line.</param>
        /// <param name="valueType">Type of the value used in the file.</param>
        /// <param name="freeFormat">True: Data is provided in a free format. It is
        /// False: It is in a fixed format.</param>
        /// <param name="numberFixed">The number of characters per item.
        /// This item applies only when the <paramref name="freeFormat" /> item is False.</param>
        /// <param name="timeInterval">The time interval between function points.
        /// This item applies only when the <paramref name="valueType" /> item is <see cref="eFileValueType.EqualTimeIntervals" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetFromFile(string name,
            ref string fileName,
            ref int headerLinesSkip,
            ref int prefixCharactersSkip,
            ref int pointsPerLine,
            ref eFileValueType valueType,
            ref bool freeFormat,
            ref int numberFixed,
            ref double timeInterval);

        /// <summary>
        /// This function defines a time history function from file.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="fileName">The full path of the text file containing the function data.</param>
        /// <param name="headerLinesSkip">The number of header lines in the text file to be skipped before starting to read function data.</param>
        /// <param name="prefixCharactersSkip">The number of prefix characters to be skipped on each line in the text file.</param>
        /// <param name="pointsPerLine">The number of function points included on each text file line.</param>
        /// <param name="valueType">Type of the value used in the file.</param>
        /// <param name="freeFormat">True: Data is provided in a free format. It is
        /// False: It is in a fixed format.</param>
        /// <param name="numberFixed">The number of characters per item.
        /// This item applies only when the <paramref name="freeFormat" /> item is False.</param>
        /// <param name="timeInterval">The time interval between function points.
        /// This item applies only when the <paramref name="valueType" /> item is <see cref="eFileValueType.EqualTimeIntervals" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetFromFile(string name,
            string fileName,
            int headerLinesSkip,
            int prefixCharactersSkip,
            int pointsPerLine,
            eFileValueType valueType,
            bool freeFormat,
            int numberFixed = 10,
            double timeInterval = 0.02);




        /// <summary>
        /// This function retrieves the definition of a user defined time history function.
        /// </summary>
        /// <param name="name">The name of a user defined time history function.</param>
        /// <param name="times">The time for each data point. [s].</param>
        /// <param name="values">The function value for each data point.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetUser(string name,
            ref double[] times,
            ref double[] values);

        /// <summary>
        /// This function defines a user time history function.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="times">The time for each data point. [s].</param>
        /// <param name="values">The function value for each data point.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetUser(string name,
            double[] times,
            double[] values);



        /// <summary>
        /// This function retrieves the definition of a user periodic time history function.
        /// </summary>
        /// <param name="name">The name of a user defined periodic time history function.</param>
        /// <param name="frequencies">The frequency in Hz for each data point. [cyc/s].</param>
        /// <param name="values">The function value for each data point.</param>
        /// <param name="numberOfCycles">The number of cycles in the function.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetUserPeriodic(string name,
            ref double[] frequencies,
            ref double[] values,
            ref int numberOfCycles);

        /// <summary>
        /// This function defines a user periodic time history function.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="frequencies">The frequency in Hz for each data point. [cyc/s].</param>
        /// <param name="values">The function value for each data point.</param>
        /// <param name="numberOfCycles">The number of cycles in the function.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetUserPeriodic(string name,
            double[] frequencies,
            double[] values,
            int numberOfCycles);

        #endregion

        #region Methods: Template Functions             

        /// <summary>
        /// This function retrieves the definition of a ramp-type time history function.
        /// </summary>
        /// <param name="name">The name of a ramp-type time history function.</param>
        /// <param name="timeToInitialValue">The time it takes for the ramp function to initially reach its maximum value. [s]</param>
        /// <param name="amplitude">The maximum amplitude of the ramp function.</param>
        /// <param name="maxTime">The time at the end of the ramp function.
        /// This time must be greater than the <paramref name="timeToInitialValue" />. [s]</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetRamp(string name,
            ref double timeToInitialValue,
            ref double amplitude,
            ref double maxTime);

        /// <summary>
        /// This function defines a ramp-type time history function.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="timeToInitialValue">The time it takes for the ramp function to initially reach its maximum value. [s]</param>
        /// <param name="amplitude">The maximum amplitude of the ramp function.</param>
        /// <param name="maxTime">The time at the end of the ramp function.
        /// This time must be greater than the <paramref name="timeToInitialValue" />. [s]</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetRamp(string name,
            double timeToInitialValue,
            double amplitude,
            double maxTime);




        /// <summary>
        /// This function retrieves the definition of a cosine-type time history function.
        /// </summary>
        /// <param name="name">The name of a cosine-type time history function.</param>
        /// <param name="period">The period of the cosine function. [s]</param>
        /// <param name="steps">The number of steps in the cosine function.
        /// This item can not be less than 8.</param>
        /// <param name="cycles">The number of cycles in the cosine function.</param>
        /// <param name="amplitude">The amplitude of the cosine function.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetCosine(string name,
            ref double period,
            ref int steps,
            ref int cycles,
            ref double amplitude);

        /// <summary>
        /// This function defines a cosine-type time history function.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="period">The period of the cosine function. [s]</param>
        /// <param name="steps">The number of steps in the cosine function.
        /// This item can not be less than 8.</param>
        /// <param name="cycles">The number of cycles in the cosine function.</param>
        /// <param name="amplitude">The amplitude of the cosine function.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetCosine(string name,
            double period,
            int steps,
            int cycles,
            double amplitude);



        /// <summary>
        /// This function retrieves the definition of a sine-type time history function.
        /// </summary>
        /// <param name="name">The name of a sine-type time history function.</param>
        /// <param name="period">The period of the sine function. [s]</param>
        /// <param name="steps">The number of steps in the sine function.
        /// This item can not be less than 8.</param>
        /// <param name="cycles">The number of cycles in the sine function.</param>
        /// <param name="amplitude">The amplitude of the sine function.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetSine(string name,
            ref double period,
            ref int steps,
            ref int cycles,
            ref double amplitude);

        /// <summary>
        /// This function defines a sine-type time history function.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="period">The period of the sine function. [s]</param>
        /// <param name="steps">The number of steps in the sine function.
        /// This item can not be less than 8.</param>
        /// <param name="cycles">The number of cycles in the sine function.</param>
        /// <param name="amplitude">The amplitude of the sine function.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetSine(string name,
            double period,
            int steps,
            int cycles,
            double amplitude);




        /// <summary>
        /// Gets the sawtooth.
        /// </summary>
        /// <param name="name">The name of a sawtooth-type time history function.</param>
        /// <param name="period">The period of the function. [s]</param>
        /// <param name="timeToAmplitude">The time it takes for the sawtooth function to ramp up from a function value of zero to its maximum amplitude. [s]</param>
        /// <param name="cycles">The number of cycles in the function.</param>
        /// <param name="amplitude">The maximum amplitude of the function. [s]</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetSawtooth(string name,
            ref double period,
            ref double timeToAmplitude,
            ref int cycles,
            ref double amplitude);

        /// <summary>
        /// Sets the sawtooth.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="period">The period of the function. [s]</param>
        /// <param name="timeToAmplitude">The time it takes for the sawtooth function to ramp up from a function value of zero to its maximum amplitude. [s]</param>
        /// <param name="cycles">The number of cycles in the function.</param>
        /// <param name="amplitude">The maximum amplitude of the function. [s]</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetSawtooth(string name,
            double period,
            double timeToAmplitude,
            int cycles,
            double amplitude);




        /// <summary>
        /// Gets the triangular.
        /// </summary>
        /// <param name="name">The name of a triangular-type time history function.</param>
        /// <param name="period">The period of the function. [s]</param>
        /// <param name="cycles">The number of cycles in the function.</param>
        /// <param name="amplitude">The maximum amplitude of the function. [s]</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetTriangular(string name,
            ref double period,
            ref int cycles,
            ref double amplitude);

        /// <summary>
        /// Sets the triangular.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="period">The period of the function. [s]</param>
        /// <param name="cycles">The number of cycles in the function.</param>
        /// <param name="amplitude">The maximum amplitude of the function. [s]</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetTriangular(string name,
            double period,
            int cycles,
            double amplitude);

        #endregion
#endif
    }
}