// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="PowerSpectralDensity.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;
using MPT.Enums;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the PSD load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ISteadyState" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class PowerSpectralDensity : CSiApiBase, ISteadyState
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerSpectralDensity" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public PowerSpectralDensity(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function initializes a load case.
        /// If this function is called for an existing load case, all items for the case are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new load case.
        /// If this is an existing case, that case is modified; otherwise, a new case is added.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCase(string name)
        {
            _callCode = _sapModel.LoadCases.PSD.SetCase(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the initial condition assumed for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="initialCase">This is blank, None, or the name of an existing analysis case.
        /// This item specifies if the load case starts from zero initial conditions, that is, an unstressed state, or if it starts using the stiffness that occurs at the end of a nonlinear static or nonlinear direct integration time history load case.
        /// If the specified initial case is a nonlinear static or nonlinear direct integration time history load case, the stiffness at the end of that case is used.
        /// If the initial case is anything else then zero initial conditions are assumed.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetInitialCase(string name,
            ref string initialCase)
        {
            _callCode = _sapModel.LoadCases.PSD.GetInitialCase(name, ref initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function sets the initial condition for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="initialCase">This is blank, None, or the name of an existing analysis case.
        /// This item specifies if the load case starts from zero initial conditions, that is, an unstressed state, or if it starts using the stiffness that occurs at the end of a nonlinear static or nonlinear direct integration time history load case.
        /// If the specified initial case is a nonlinear static or nonlinear direct integration time history load case, the stiffness at the end of that case is used.
        /// If the initial case is anything else then zero initial conditions are assumed.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetInitialCase(string name,
            string initialCase)
        {
            _callCode = _sapModel.LoadCases.PSD.SetInitialCase(name, initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves the load data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing power spectral density load case.</param>
        /// <param name="loadTypes">Either <see cref="eLoadType.Load" /> or <see cref="eLoadType.Accel" />, indicating the type of each load assigned to the load case.</param>
        /// <param name="loadNames">The name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />, this item is U1, U2, U3, R1, R2 or R3, indicating the direction of the load.</param>
        /// <param name="functions">The name of the power spectral density function associated with each load.</param>
        /// <param name="scaleFactor">The scale factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.</param>
        /// <param name="phaseAngle">The phase angle for each load. [deg].</param>
        /// <param name="coordinateSystems">This is an array that includes the name of the coordinate system associated with each load.
        /// If this item is a blank string, the Global coordinate system is assumed.
        /// This item applies only when <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />.</param>
        /// <param name="angles">This is an array that includes the angle between the acceleration local 1 axis and the +X-axis of the coordinate system specified by <paramref name="coordinateSystems" />.
        /// The rotation is about the Z-axis of the specified coordinate system. [deg].
        /// This item applies only when <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoads(string name,
            ref eLoadType[] loadTypes,
            ref string[] loadNames,
            ref string[] functions,
            ref double[] scaleFactor,
            ref double[] phaseAngle,
            ref string[] coordinateSystems,
            ref double[] angles)
        {
            string[] csiLoadTypes = new string[0];

            _callCode = _sapModel.LoadCases.PSD.GetLoads(name, ref _numberOfItems, ref csiLoadTypes, ref loadNames, ref functions, ref scaleFactor, ref phaseAngle, ref coordinateSystems, ref angles);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadTypes = new eLoadType[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                loadTypes[i] = EnumLibrary.ConvertStringToEnumByDescription<eLoadType>(csiLoadTypes[i]);
            }
        }

        /// <summary>
        /// This function sets the load data for the specified analysis case.
        /// </summary>
        /// <param name="name">The name of an existing power spectral density load case.</param>
        /// <param name="loadTypes">Either <see cref="eLoadType.Load" /> or <see cref="eLoadType.Accel" />, indicating the type of each load assigned to the load case.</param>
        /// <param name="loadNames">The name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />, this item is U1, U2, U3, R1, R2 or R3, indicating the direction of the load.</param>
        /// <param name="functions">The name of the power spectral density function associated with each load.</param>
        /// <param name="scaleFactor">The scale factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.</param>
        /// <param name="phaseAngle">The phase angle for each load. [deg].</param>
        /// <param name="coordinateSystems">This is an array that includes the name of the coordinate system associated with each load.
        /// If this item is a blank string, the Global coordinate system is assumed.
        /// This item applies only when <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />.</param>
        /// <param name="angles">This is an array that includes the angle between the acceleration local 1 axis and the +X-axis of the coordinate system specified by <paramref name="coordinateSystems" />.
        /// The rotation is about the Z-axis of the specified coordinate system. [deg].
        /// This item applies only when <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoads(string name,
            eLoadType[] loadTypes,
            string[] loadNames,
            string[] functions,
            double[] scaleFactor,
            double[] phaseAngle,
            string[] coordinateSystems,
            double[] angles)
        {
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(loadNames), loadNames.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(functions), functions.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(scaleFactor), scaleFactor.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(phaseAngle), phaseAngle.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(coordinateSystems), coordinateSystems.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(angles), angles.Length);

            string[] csiLoadTypes = new string[loadTypes.Length - 1];
            for (int i = 0; i < loadTypes.Length; i++)
            {
                csiLoadTypes[i] = loadTypes[i].ToString();
            }

            _callCode = _sapModel.LoadCases.PSD.SetLoads(name, loadTypes.Length, ref csiLoadTypes, ref loadNames, ref functions, ref scaleFactor, ref phaseAngle, ref coordinateSystems, ref angles);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Methods: Damping & Frequencies
        /// <summary>
        /// This function retrieves the frequency data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing power spectral density load case.</param>
        /// <param name="frequencyFirst">The first frequency. [cyc/s].</param>
        /// <param name="frequencyLast">The last frequency. [cyc/s].</param>
        /// <param name="numberOfFrequencies">The number of frequency increments.</param>
        /// <param name="addModalFrequencies">True: Modal frequencies are added.</param>
        /// <param name="addSignedFractionalDeviations">True: Signed fractional deviations from modal frequencies are added.</param>
        /// <param name="addSpecifiedFrequencies">True: Specified frequencies are added.</param>
        /// <param name="modalCase">This is the name of an existing modal load case.
        /// It specifies the modal load case on which modal frequencies and modal frequency deviations are based.</param>
        /// <param name="numberSignedFractionalDeviations">The number of signed fractional deviations from modal frequencies that are added.
        /// This item applies only when <paramref name="addSignedFractionalDeviations" /> = True.</param>
        /// <param name="signedFractionalDeviations">The added signed fractional deviations from modal frequencies.
        /// This item applies only when <paramref name="addSignedFractionalDeviations" /> = True.</param>
        /// <param name="numberSpecifiedFrequencies">The number of specified frequencies that are added.
        /// This item applies only when <paramref name="addSpecifiedFrequencies" /> = True.</param>
        /// <param name="specifiedFrequencies">The added specified frequencies.
        /// This item applies only when <paramref name="addSpecifiedFrequencies" /> = True.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFrequencyData(string name,
            ref double frequencyFirst,
            ref double frequencyLast,
            ref int numberOfFrequencies,
            ref bool addModalFrequencies,
            ref bool addSignedFractionalDeviations,
            ref bool addSpecifiedFrequencies,
            ref string modalCase,
            ref int numberSignedFractionalDeviations,
            ref double[] signedFractionalDeviations,
            ref int numberSpecifiedFrequencies,
            ref double[] specifiedFrequencies)
        {
            _callCode = _sapModel.LoadCases.PSD.GetFreqData(name, ref frequencyFirst, ref frequencyLast, ref numberOfFrequencies,
                ref addModalFrequencies, ref addSignedFractionalDeviations, ref addSpecifiedFrequencies, ref modalCase,
                ref numberSignedFractionalDeviations, ref signedFractionalDeviations, ref numberSpecifiedFrequencies, ref specifiedFrequencies);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the frequency data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing power spectral density load case.</param>
        /// <param name="frequencyFirst">The first frequency. [cyc/s].</param>
        /// <param name="frequencyLast">The last frequency. [cyc/s].</param>
        /// <param name="numberOfFrequencies">The number of frequency increments.</param>
        /// <param name="addModalFrequencies">True: Modal frequencies are added.</param>
        /// <param name="addSignedFractionalDeviations">True: Signed fractional deviations from modal frequencies are added.</param>
        /// <param name="addSpecifiedFrequencies">True: Specified frequencies are added.</param>
        /// <param name="modalCase">This is the name of an existing modal load case.
        /// It specifies the modal load case on which modal frequencies and modal frequency deviations are based.</param>
        /// <param name="signedFractionalDeviations">The added signed fractional deviations from modal frequencies.
        /// This item applies only when <paramref name="addSignedFractionalDeviations" /> = True.</param>
        /// <param name="specifiedFrequencies">The added specified frequencies.
        /// This item applies only when <paramref name="addSpecifiedFrequencies" /> = True.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFrequencyData(string name,
            double frequencyFirst,
            double frequencyLast,
            int numberOfFrequencies,
            bool addModalFrequencies,
            bool addSignedFractionalDeviations,
            bool addSpecifiedFrequencies,
            string modalCase,
            double[] signedFractionalDeviations,
            double[] specifiedFrequencies)
        {
            _callCode = _sapModel.LoadCases.PSD.SetFreqData(name, frequencyFirst, frequencyLast, numberOfFrequencies,
                            addModalFrequencies, addSignedFractionalDeviations, addSpecifiedFrequencies, modalCase,
                            signedFractionalDeviations.Length, ref signedFractionalDeviations, 
                            specifiedFrequencies.Length, ref specifiedFrequencies);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves the constant hysteretic damping for all frequencies assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing power spectral density load case that has constant damping.</param>
        /// <param name="massProportionalDampingCoefficient">The mass proportional damping coefficient.</param>
        /// <param name="stiffnessProportionalDampingCoefficient">The stiffness proportional damping coefficient.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDampingConstant(string name,
            ref double massProportionalDampingCoefficient,
            ref double stiffnessProportionalDampingCoefficient)
        {
            _callCode = _sapModel.LoadCases.PSD.GetDampConstant(name, ref massProportionalDampingCoefficient, ref stiffnessProportionalDampingCoefficient);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets constant hysteretic damping for all frequencies for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing power spectral density load case that has constant damping.</param>
        /// <param name="massProportionalDampingCoefficient">The mass proportional damping coefficient.</param>
        /// <param name="stiffnessProportionalDampingCoefficient">The stiffness proportional damping coefficient.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDampingConstant(string name,
            double massProportionalDampingCoefficient,
            double stiffnessProportionalDampingCoefficient)
        {
            _callCode = _sapModel.LoadCases.PSD.SetDampConstant(name, massProportionalDampingCoefficient, stiffnessProportionalDampingCoefficient);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves the interpolated hysteretic damping by frequency assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing power spectral density load case that has interpolated damping.</param>
        /// <param name="frequencyUnit">The frequency unit.</param>
        /// <param name="frequencies">The frequencies, given in the units specified by <paramref name="frequencyUnit" />.</param>
        /// <param name="massProportionalDampingCoefficients">The mass proportional damping coefficients.</param>
        /// <param name="stiffnessProportionalDampingCoefficients">The stiffness proportional damping coefficients.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDampingInterpolated(string name,
            ref eFrequencyType frequencyUnit,
            ref double[] frequencies,
            ref double[] massProportionalDampingCoefficients,
            ref double[] stiffnessProportionalDampingCoefficients)
        {
            int csiFrequencyUnit = 0;

            _callCode = _sapModel.LoadCases.PSD.GetDampInterpolated(name, ref csiFrequencyUnit, ref _numberOfItems, ref frequencies, ref massProportionalDampingCoefficients, ref stiffnessProportionalDampingCoefficients);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            frequencyUnit = (eFrequencyType)csiFrequencyUnit;
        }

        /// <summary>
        /// This function sets interpolated hysteretic damping by frequency for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing power spectral density load case that has interpolated damping.</param>
        /// <param name="frequencyUnit">The frequency unit.</param>
        /// <param name="frequencies">The frequencies, given in the units specified by <paramref name="frequencyUnit" />.</param>
        /// <param name="massProportionalDampingCoefficients">The mass proportional damping coefficients.</param>
        /// <param name="stiffnessProportionalDampingCoefficients">The stiffness proportional damping coefficients.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDampingInterpolated(string name,
            eFrequencyType frequencyUnit,
            double[] frequencies,
            double[] massProportionalDampingCoefficients,
            double[] stiffnessProportionalDampingCoefficients)
        {
            arraysLengthMatch(nameof(frequencies), frequencies.Length, nameof(massProportionalDampingCoefficients), massProportionalDampingCoefficients.Length);
            arraysLengthMatch(nameof(frequencies), frequencies.Length, nameof(stiffnessProportionalDampingCoefficients), stiffnessProportionalDampingCoefficients.Length);

            _callCode = _sapModel.LoadCases.PSD.SetDampInterpolated(name, (int)frequencyUnit, frequencies.Length, ref frequencies, ref massProportionalDampingCoefficients, ref stiffnessProportionalDampingCoefficients);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves the hysteretic damping type for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing power spectral density load case.</param>
        /// <param name="dampingType">The hysteretic damping type for the load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDampingType(string name,
            ref eDampingTypeHysteretic dampingType)
        {
            int csiDampingType = 0;

            _callCode = _sapModel.LoadCases.PSD.GetDampType(name, ref csiDampingType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            dampingType = (eDampingTypeHysteretic)csiDampingType;
        }
#endregion
    }
}
#endif
