// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="TimeHistoryModalNonlinear.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;
using MPT.Enums;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the time history modal nonlinear load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ITimeHistoryModalNonlinear" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class TimeHistoryModalNonlinear : CSiApiBase, ITimeHistoryModalNonlinear
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryModalNonlinear" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public TimeHistoryModalNonlinear(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Interface
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function initializes a load case.
        /// If this function is called for an existing load case, all items for the case are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new load case.
        /// If this is an existing case, that case is modified; otherwise, a new case is added.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCase(string name)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.SetCase(name);
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
            _callCode = _sapModel.LoadCases.ModHistNonlinear.GetInitialCase(name, ref initialCase);
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
            _callCode = _sapModel.LoadCases.ModHistNonlinear.SetInitialCase(name, initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#if !BUILD_ETABS2015
        /// <summary>
        /// This function retrieves the load data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing steady state load case.</param>
        /// <param name="loadTypes">Either <see cref="eLoadType.Load" /> or <see cref="eLoadType.Accel" />, indicating the type of each load assigned to the load case.</param>
        /// <param name="loadNames">The name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />, this item is U1, U2, U3, R1, R2 or R3, indicating the direction of the load.</param>
        /// <param name="functions">The name of the steady state function associated with each load.</param>
        /// <param name="scaleFactor">The scale factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.</param>
        /// <param name="timeFactor">The time scale factor of each load assigned to the load case.</param>
        /// <param name="arrivalTime">The arrival time of each load assigned to the load case.</param>
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
            ref double[] timeFactor,
            ref double[] arrivalTime,
            ref string[] coordinateSystems,
            ref double[] angles)
        {
            string[] csiLoadTypes = new string[0];

            _callCode = _sapModel.LoadCases.ModHistNonlinear.GetLoads(name, ref _numberOfItems, ref csiLoadTypes, ref loadNames, ref functions, ref scaleFactor, ref timeFactor, ref arrivalTime, ref coordinateSystems, ref angles);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadTypes = new eLoadType[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                loadTypes[i] = EnumLibrary.ConvertStringToEnumByDescription<eLoadType>(csiLoadTypes[i]);
            }
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function sets the load data for the specified analysis case.
        /// </summary>
        /// <param name="name">The name of an existing steady state load case.</param>
        /// <param name="loadTypes">Either <see cref="eLoadType.Load" /> or <see cref="eLoadType.Accel" />, indicating the type of each load assigned to the load case.</param>
        /// <param name="loadNames">The name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />, this item is U1, U2, U3, R1, R2 or R3, indicating the direction of the load.</param>
        /// <param name="functions">The name of the steady state function associated with each load.</param>
        /// <param name="scaleFactor">The scale factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.</param>
        /// <param name="timeFactor">The time scale factor of each load assigned to the load case.</param>
        /// <param name="arrivalTime">The arrival time of each load assigned to the load case.</param>
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
            double[] timeFactor,
            double[] arrivalTime,
            string[] coordinateSystems,
            double[] angles)
        {
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(loadNames), loadNames.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(functions), functions.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(scaleFactor), scaleFactor.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(timeFactor), timeFactor.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(arrivalTime), arrivalTime.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(coordinateSystems), coordinateSystems.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(angles), angles.Length);

            string[] csiLoadTypes = new string[loadTypes.Length - 1];
            for (int i = 0; i < loadTypes.Length; i++)
            {
                csiLoadTypes[i] = loadTypes[i].ToString();
            }

            _callCode = _sapModel.LoadCases.ModHistNonlinear.SetLoads(name, loadTypes.Length, ref csiLoadTypes, ref loadNames, ref functions, ref scaleFactor, ref timeFactor, ref arrivalTime, ref coordinateSystems, ref angles);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }






        /// <summary>
        /// This function retrieves the time step data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing time history load casee.</param>
        /// <param name="numberOfOutputSteps">The number of output time steps.</param>
        /// <param name="timeStepSize">The output time step size.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTimeStep(string name,
            ref int numberOfOutputSteps,
            ref double timeStepSize)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.GetTimeStep(name, ref numberOfOutputSteps, ref timeStepSize);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function sets the time step data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing time history load casee.</param>
        /// <param name="numberOfOutputSteps">The number of output time steps.</param>
        /// <param name="timeStepSize">The output time step size.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetTimeStep(string name,
            int numberOfOutputSteps,
            double timeStepSize)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.SetTimeStep(name, numberOfOutputSteps, timeStepSize);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// This function retrieves the modal case assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="modalCase">This is either None or the name of an existing modal analysis case.
        /// It specifies the modal load case on which any mode-type load assignments to the specified load case are based.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetModalCase(string name,
            ref string modalCase)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.GetModalCase(name, ref modalCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the modal case for the specified analysis case.
        /// If the specified modal case is not actually a modal case, the program automatically replaces it with the first modal case it can find.
        /// If no modal load cases exist, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="modalCase">This is either None or the name of an existing modal analysis case.
        /// It specifies the modal load case on which any mode-type load assignments to the specified load case are based.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetModalCase(string name,
            string modalCase)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.SetModalCase(name, modalCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }






        /// <summary>
        /// This function retrieves the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="staticPeriod">The static period.</param>
        /// <param name="maxSubstepSize">The maximum substep size.</param>
        /// <param name="minSubstepSize">The minimum substep size.</param>
        /// <param name="relativeForceConvergenceTolerance">The relative force convergence tolerance.</param>
        /// <param name="relativeEnergyConvergenceTolerance">The relative energy convergence tolerance.</param>
        /// <param name="maxIterationLimit">The maximum iteration limit.</param>
        /// <param name="minIterationLimit">The minimum iteration limit.</param>
        /// <param name="convergenceFactor">The convergence factor.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSolutionControlParameters(string name,
            ref double staticPeriod,
            ref double maxSubstepSize,
            ref double minSubstepSize,
            ref double relativeForceConvergenceTolerance,
            ref double relativeEnergyConvergenceTolerance,
            ref int maxIterationLimit,
            ref int minIterationLimit,
            ref double convergenceFactor)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.GetSolControlParameters(name,
                            ref staticPeriod,
                            ref maxSubstepSize,
                            ref minSubstepSize,
                            ref relativeForceConvergenceTolerance,
                            ref relativeEnergyConvergenceTolerance,
                            ref maxIterationLimit,
                            ref minIterationLimit,
                            ref convergenceFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="staticPeriod">The static period.</param>
        /// <param name="maxSubstepSize">The maximum substep size.</param>
        /// <param name="minSubstepSize">The minimum substep size.</param>
        /// <param name="relativeForceConvergenceTolerance">The relative force convergence tolerance.</param>
        /// <param name="relativeEnergyConvergenceTolerance">The relative energy convergence tolerance.</param>
        /// <param name="maxIterationLimit">The maximum iteration limit.</param>
        /// <param name="minIterationLimit">The minimum iteration limit.</param>
        /// <param name="convergenceFactor">The convergence factor.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSolutionControlParameters(string name,
            double staticPeriod,
            double maxSubstepSize,
            double minSubstepSize,
            double relativeForceConvergenceTolerance,
            double relativeEnergyConvergenceTolerance,
            int maxIterationLimit,
            int minIterationLimit,
            double convergenceFactor)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.SetSolControlParameters(name,
                            staticPeriod,
                            maxSubstepSize,
                            minSubstepSize,
                            relativeForceConvergenceTolerance,
                            relativeEnergyConvergenceTolerance,
                            maxIterationLimit,
                            minIterationLimit,
                            convergenceFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        #region Damping

        /// <summary>
        /// This function retrieves the hysteretic damping type for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="dampingType">The modal damping type for the load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDampingType(string name,
            ref eDampingType dampingType)
        {
            int csiDampingType = 0;

            _callCode = _sapModel.LoadCases.ModHistNonlinear.GetDampType(name, ref csiDampingType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            dampingType = (eDampingType)csiDampingType;
        }




        /// <summary>
        /// This function retrieves the constant modal damping for all modes assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case that has constant damping.</param>
        /// <param name="damping">The constant damping for all modes (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDampingConstant(string name,
            ref double damping)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.GetDampConstant(name, ref damping);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets constant modal damping for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case that has constant damping.</param>
        /// <param name="damping">The constant damping for all modes (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDampingConstant(string name,
            double damping)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.SetDampConstant(name, damping);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves the interpolated modal damping data assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case that has interpolated damping.</param>
        /// <param name="dampingType">The interpolated modal damping type.</param>
        /// <param name="periodsOrFrequencies">The periods or frequencies, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByFrequency" />.</param>
        /// <param name="damping">The damping for the specified period of frequency (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDampingInterpolated(string name,
            ref eDampingTypeInterpolated dampingType,
            ref double[] periodsOrFrequencies,
            ref double[] damping)
        {
            int csiDampingType = 0;

            _callCode = _sapModel.LoadCases.ModHistNonlinear.GetDampInterpolated(name, ref csiDampingType, ref _numberOfItems, ref periodsOrFrequencies, ref damping);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            dampingType = (eDampingTypeInterpolated)csiDampingType;
        }

        /// <summary>
        /// This function retrieves the interpolated modal damping data assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case that has interpolated damping.</param>
        /// <param name="dampingType">The interpolated modal damping type.</param>
        /// <param name="periodsOrFrequencies">The periods or frequencies, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByFrequency" />.</param>
        /// <param name="damping">The damping for the specified period of frequency (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDampingInterpolated(string name,
            eDampingTypeInterpolated dampingType,
            double[] periodsOrFrequencies,
            double[] damping)
        {
            arraysLengthMatch(nameof(periodsOrFrequencies), periodsOrFrequencies.Length, nameof(damping), damping.Length);

            _callCode = _sapModel.LoadCases.ModHistNonlinear.SetDampInterpolated(name, (int)dampingType, periodsOrFrequencies.Length, ref periodsOrFrequencies, ref damping);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the proportional modal damping data assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing linear modal history analysis case that has proportional damping.</param>
        /// <param name="dampingType">The proportional modal damping type.</param>
        /// <param name="massProportionalDampingCoefficient">The mass proportional damping coefficient.</param>
        /// <param name="stiffnessProportionalDampingCoefficient">The stiffness proportional damping coefficient.</param>
        /// <param name="periodOrFrequencyPt1">The period or frequency for point 1, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="periodOrFrequencyPt2">The period or frequency for point 2, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="dampingPt1">The damping for point 1 (0 &lt;= <paramref name="dampingPt1" /> &lt; 1).
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="dampingPt2">The damping for point 2 (0 &lt;= <paramref name="dampingPt1" /> &lt; 1).
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDampingProportional(string name,
            ref eDampingTypeProportional dampingType,
            ref double massProportionalDampingCoefficient,
            ref double stiffnessProportionalDampingCoefficient,
            ref double periodOrFrequencyPt1,
            ref double periodOrFrequencyPt2,
            ref double dampingPt1,
            ref double dampingPt2)
        {
            int csiDampingType = 0;

            _callCode = _sapModel.LoadCases.ModHistNonlinear.GetDampProportional(name, ref csiDampingType, ref massProportionalDampingCoefficient, ref stiffnessProportionalDampingCoefficient,
                ref periodOrFrequencyPt1, ref periodOrFrequencyPt2, ref dampingPt1, ref dampingPt2);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            dampingType = (eDampingTypeProportional)csiDampingType;
        }

        /// <summary>
        /// This function sets proportional modal damping data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing linear modal history analysis case that has proportional damping.</param>
        /// <param name="dampingType">The proportional modal damping type.</param>
        /// <param name="massProportionalDampingCoefficient">The mass proportional damping coefficient.</param>
        /// <param name="stiffnessProportionalDampingCoefficient">The stiffness proportional damping coefficient.</param>
        /// <param name="periodOrFrequencyPt1">The period or frequency for point 1, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="periodOrFrequencyPt2">The period or frequency for point 2, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="dampingPt1">The damping for point 1 (0 &lt;= <paramref name="dampingPt1" /> &lt; 1).
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="dampingPt2">The damping for point 2 (0 &lt;= <paramref name="dampingPt1" /> &lt; 1).
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDampingProportional(string name,
            eDampingTypeProportional dampingType,
            double massProportionalDampingCoefficient,
            double stiffnessProportionalDampingCoefficient,
            double periodOrFrequencyPt1,
            double periodOrFrequencyPt2,
            double dampingPt1,
            double dampingPt2)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.SetDampProportional(name, (int)dampingType, massProportionalDampingCoefficient, stiffnessProportionalDampingCoefficient,
                periodOrFrequencyPt1, periodOrFrequencyPt2, dampingPt1, dampingPt2);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the modal damping overrides assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="modes">The modes.</param>
        /// <param name="damping">The damping for the specified mode (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDampingOverrides(string name,
            ref int[] modes,
            ref double[] damping)
        {
            _callCode = _sapModel.LoadCases.ModHistNonlinear.GetDampOverrides(name, ref _numberOfItems, ref modes, ref damping);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves the modal damping overrides assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="modes">The modes.</param>
        /// <param name="damping">The damping for the specified mode (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDampingOverrides(string name,
            int[] modes,
            double[] damping)
        {
            arraysLengthMatch(nameof(modes), modes.Length, nameof(damping), damping.Length);

            _callCode = _sapModel.LoadCases.ModHistNonlinear.SetDampOverrides(name, modes.Length, ref modes, ref damping);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion
#endif
    }
}
