// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="TimeHistory.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function
{
    /// <summary>
    /// Represents the time history function in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.ITimeHistory" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class TimeHistory : CSiApiBase, ITimeHistory
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistory" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public TimeHistory(CSiApiSeed seed) : base(seed) { }


        #endregion

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetFromFile(string name,
            ref string fileName,
            ref int headerLinesSkip,
            ref int prefixCharactersSkip,
            ref int pointsPerLine,
            ref eFileValueType valueType,
            ref bool freeFormat,
            ref int numberFixed,
            ref double timeInterval)
        {
            int csiValueType = 0;

            // TODO: What if file path is invalid? Other ways this may fail?
            _callCode = _sapModel.Func.FuncTH.GetFromFile_1(name, ref fileName, ref headerLinesSkip, ref prefixCharactersSkip, ref pointsPerLine, ref csiValueType, ref freeFormat, ref numberFixed, ref timeInterval);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            valueType = (eFileValueType)csiValueType;
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetFromFile(string name,
            string fileName,
            int headerLinesSkip,
            int prefixCharactersSkip,
            int pointsPerLine,
            eFileValueType valueType,
            bool freeFormat,
            int numberFixed = 10,
            double timeInterval = 0.02)
        {
            // TODO: What if file path is invalid? Other ways this may fail?
            _callCode = _sapModel.Func.FuncTH.SetFromFile_1(name, fileName, headerLinesSkip, prefixCharactersSkip, pointsPerLine, (int)valueType, freeFormat, numberFixed, timeInterval);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the definition of a user defined time history function.
        /// </summary>
        /// <param name="name">The name of a user defined time history function.</param>
        /// <param name="times">The time for each data point. [s].</param>
        /// <param name="values">The function value for each data point.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetUser(string name,
            ref double[] times,
            ref double[] values)
        {
            _callCode = _sapModel.Func.FuncTH.GetUser(name, ref _numberOfItems, ref times, ref values);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function defines a user time history function.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="times">The time for each data point. [s].</param>
        /// <param name="values">The function value for each data point.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetUser(string name,
            double[] times,
            double[] values)
        {
            arraysLengthMatch(nameof(times), times.Length, nameof(values), values.Length);
            int numberOfItems = times.Length;

            _callCode = _sapModel.Func.FuncTH.SetUser(name, numberOfItems, ref times, ref values);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves the definition of a user periodic time history function.
        /// </summary>
        /// <param name="name">The name of a user defined periodic time history function.</param>
        /// <param name="frequencies">The frequency in Hz for each data point. [cyc/s].</param>
        /// <param name="values">The function value for each data point.</param>
        /// <param name="numberOfCycles">The number of cycles in the function.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetUserPeriodic(string name,
            ref double[] frequencies,
            ref double[] values,
            ref int numberOfCycles)
        {
            _callCode = _sapModel.Func.FuncTH.GetUserPeriodic(name, ref numberOfCycles, ref _numberOfItems, ref frequencies, ref values);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function defines a user periodic time history function.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="frequencies">The frequency in Hz for each data point. [cyc/s].</param>
        /// <param name="values">The function value for each data point.</param>
        /// <param name="numberOfCycles">The number of cycles in the function.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetUserPeriodic(string name,
            double[] frequencies,
            double[] values,
            int numberOfCycles)
        {
            arraysLengthMatch(nameof(frequencies), frequencies.Length, nameof(values), values.Length);
            int numberOfItems = frequencies.Length;

            _callCode = _sapModel.Func.FuncTH.SetUserPeriodic(name, numberOfCycles, numberOfItems, ref frequencies, ref values);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetRamp(string name,
            ref double timeToInitialValue,
            ref double amplitude,
            ref double maxTime)
        {
            _callCode = _sapModel.Func.FuncTH.GetRamp(name, ref timeToInitialValue, ref amplitude, ref maxTime);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function defines a ramp-type time history function.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="timeToInitialValue">The time it takes for the ramp function to initially reach its maximum value. [s]</param>
        /// <param name="amplitude">The maximum amplitude of the ramp function.</param>
        /// <param name="maxTime">The time at the end of the ramp function.
        /// This time must be greater than the <paramref name="timeToInitialValue" />. [s]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetRamp(string name,
            double timeToInitialValue,
            double amplitude,
            double maxTime)
        {
            if (maxTime < timeToInitialValue) { maxTime = timeToInitialValue; }

            _callCode = _sapModel.Func.FuncTH.SetRamp(name, timeToInitialValue, amplitude, maxTime);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the definition of a cosine-type time history function.
        /// </summary>
        /// <param name="name">The name of a cosine-type time history function.</param>
        /// <param name="period">The period of the cosine function. [s]</param>
        /// <param name="steps">The number of steps in the cosine function.
        /// This item can not be less than 8.</param>
        /// <param name="cycles">The number of cycles in the cosine function.</param>
        /// <param name="amplitude">The amplitude of the cosine function.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetCosine(string name,
            ref double period,
            ref int steps,
            ref int cycles,
            ref double amplitude)
        {
            _callCode = _sapModel.Func.FuncTH.GetCosine(name, ref period, ref steps, ref cycles, ref amplitude);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetCosine(string name,
            double period,
            int steps,
            int cycles,
            double amplitude)
        {
            if (steps < 8) { steps = 8;}

            _callCode = _sapModel.Func.FuncTH.SetCosine(name, period, steps, cycles, amplitude);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves the definition of a sine-type time history function.
        /// </summary>
        /// <param name="name">The name of a sine-type time history function.</param>
        /// <param name="period">The period of the sine function. [s]</param>
        /// <param name="steps">The number of steps in the sine function.
        /// This item can not be less than 8.</param>
        /// <param name="cycles">The number of cycles in the sine function.</param>
        /// <param name="amplitude">The amplitude of the sine function.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetSine(string name,
            ref double period,
            ref int steps,
            ref int cycles,
            ref double amplitude)
        {
            _callCode = _sapModel.Func.FuncTH.GetSine(name, ref period, ref steps, ref cycles, ref amplitude);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetSine(string name,
            double period,
            int steps,
            int cycles,
            double amplitude)
        {
            if (steps < 8) { steps = 8; }

            _callCode = _sapModel.Func.FuncTH.SetSine(name, period, steps, cycles, amplitude);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Gets the sawtooth.
        /// </summary>
        /// <param name="name">The name of a sawtooth-type time history function.</param>
        /// <param name="period">The period of the function. [s]</param>
        /// <param name="timeToAmplitude">The time it takes for the sawtooth function to ramp up from a function value of zero to its maximum amplitude. [s]</param>
        /// <param name="cycles">The number of cycles in the function.</param>
        /// <param name="amplitude">The maximum amplitude of the function. [s]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetSawtooth(string name,
            ref double period,
            ref double timeToAmplitude,
            ref int cycles,
            ref double amplitude)
        {
            _callCode = _sapModel.Func.FuncTH.GetSawtooth(name, ref period, ref timeToAmplitude, ref cycles, ref amplitude);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the sawtooth.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="period">The period of the function. [s]</param>
        /// <param name="timeToAmplitude">The time it takes for the sawtooth function to ramp up from a function value of zero to its maximum amplitude. [s]</param>
        /// <param name="cycles">The number of cycles in the function.</param>
        /// <param name="amplitude">The maximum amplitude of the function. [s]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetSawtooth(string name,
            double period,
            double timeToAmplitude,
            int cycles,
            double amplitude)
        {
            _callCode = _sapModel.Func.FuncTH.SetSawtooth(name, period, timeToAmplitude, cycles, amplitude);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Gets the triangular.
        /// </summary>
        /// <param name="name">The name of a triangular-type time history function.</param>
        /// <param name="period">The period of the function. [s]</param>
        /// <param name="cycles">The number of cycles in the function.</param>
        /// <param name="amplitude">The maximum amplitude of the function. [s]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetTriangular(string name,
            ref double period,
            ref int cycles,
            ref double amplitude)
        {
            _callCode = _sapModel.Func.FuncTH.GetTriangular(name, ref period, ref cycles, ref amplitude);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the triangular.
        /// </summary>
        /// <param name="name">The name of an existing or new function.
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="period">The period of the function. [s]</param>
        /// <param name="cycles">The number of cycles in the function.</param>
        /// <param name="amplitude">The maximum amplitude of the function. [s]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetTriangular(string name,
            double period,
            int cycles,
            double amplitude)
        {
            _callCode = _sapModel.Func.FuncTH.SetTriangular(name, period, cycles, amplitude);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
#endif
    }
}
