// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="TimeHistoryDirectNonlinear.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;
using MPT.Enums;


namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the time history direct nonlinear load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ITimeHistoryDirectNonlinear" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class TimeHistoryDirectNonlinear : CSiApiBase, ITimeHistoryDirectNonlinear
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryDirectNonlinear" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public TimeHistoryDirectNonlinear(CSiApiSeed seed) : base(seed) { }


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
            _callCode = _sapModel.LoadCases.DirHistNonlinear.SetCase(name);
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
            _callCode = _sapModel.LoadCases.DirHistNonlinear.GetInitialCase(name, ref initialCase);
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
            _callCode = _sapModel.LoadCases.DirHistNonlinear.SetInitialCase(name, initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


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

            _callCode = _sapModel.LoadCases.DirHistNonlinear.GetLoads(name, ref _numberOfItems, ref csiLoadTypes, ref loadNames, ref functions, ref scaleFactor, ref timeFactor, ref arrivalTime, ref coordinateSystems, ref angles);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadTypes = new eLoadType[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                loadTypes[i] = EnumLibrary.ConvertStringToEnumByDescription<eLoadType>(csiLoadTypes[i]);
            }
        }

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

            _callCode = _sapModel.LoadCases.DirHistNonlinear.SetLoads(name, loadTypes.Length, ref csiLoadTypes, ref loadNames, ref functions, ref scaleFactor, ref timeFactor, ref arrivalTime, ref coordinateSystems, ref angles);
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

            _callCode = _sapModel.LoadCases.DirHistNonlinear.GetDampProportional(name, ref csiDampingType, ref massProportionalDampingCoefficient, ref stiffnessProportionalDampingCoefficient,
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
            _callCode = _sapModel.LoadCases.DirHistNonlinear.SetDampProportional(name, (int)dampingType, massProportionalDampingCoefficient, stiffnessProportionalDampingCoefficient,
                periodOrFrequencyPt1, periodOrFrequencyPt2, dampingPt1, dampingPt2);
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
            _callCode = _sapModel.LoadCases.DirHistNonlinear.GetTimeStep(name, ref numberOfOutputSteps, ref timeStepSize);
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
            _callCode = _sapModel.LoadCases.DirHistNonlinear.SetTimeStep(name, numberOfOutputSteps, timeStepSize);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the time integration data assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing integration time history load case.</param>
        /// <param name="integrationType">The time integration type.</param>
        /// <param name="alpha">The alphafactor (-1/3 &lt;= <paramref name="alpha" /> &lt;= 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <param name="beta">The beta factor (<paramref name="beta" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="gamma">The gamma factor (<paramref name="gamma" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="theta">The theta factor (<paramref name="theta" /> &gt; 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Wilson" /> or <see cref="eTimeIntegrationType.Collocation" />.</param>
        /// <param name="alphaM">The alpha-m factor.
        /// This item only applies when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTimeIntegration(string name,
            ref eTimeIntegrationType integrationType,
            ref double alpha,
            ref double beta,
            ref double gamma,
            ref double theta,
            ref double alphaM)
        {
            int csiIntegrationType = 0;

            _callCode = _sapModel.LoadCases.DirHistNonlinear.GetTimeIntegration(name, ref csiIntegrationType, ref alpha, ref beta, ref gamma, ref theta, ref alphaM);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            integrationType = (eTimeIntegrationType)csiIntegrationType;
        }


        /// <summary>
        /// This function sets time integration data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing integration time history load case.</param>
        /// <param name="integrationType">The time integration type.</param>
        /// <param name="alpha">The alphafactor (-1/3 &lt;= <paramref name="alpha" /> &lt;= 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <param name="beta">The beta factor (<paramref name="beta" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="gamma">The gamma factor (<paramref name="gamma" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="theta">The theta factor (<paramref name="theta" /> &gt; 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Wilson" /> or <see cref="eTimeIntegrationType.Collocation" />.</param>
        /// <param name="alphaM">The alpha-m factor.
        /// This item only applies when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetTimeIntegration(string name,
            eTimeIntegrationType integrationType,
            double alpha,
            double beta,
            double gamma,
            double theta,
            double alphaM)
        {
            _callCode = _sapModel.LoadCases.DirHistNonlinear.SetTimeIntegration(name, (int)integrationType, alpha, beta, gamma, theta, alphaM);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// This function retrieves the mass source to be used for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="sourceName">This is the name of an existing mass source or a blank string.
        /// Blank indicates to use the mass source from the previous load case or the default mass source if the load case starts from zero initial conditions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMassSource(string name,
            ref string sourceName)
        {
            _callCode = _sapModel.LoadCases.DirHistNonlinear.GetMassSource(name, ref sourceName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the mass source to be used for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="sourceName">This is the name of an existing mass source or a blank string.
        /// Blank indicates to use the mass source from the previous load case or the default mass source if the load case starts from zero initial conditions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMassSource(string name,
            string sourceName)
        {
            _callCode = _sapModel.LoadCases.DirHistNonlinear.SetMassSource(name, sourceName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the geometric nonlinearity option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="geometricNonlinearityType">The geometric nonlinearity option selected for the load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGeometricNonlinearity(string name,
            ref eGeometricNonlinearity geometricNonlinearityType)
        {
            int csiGeometricNonlinearityType = 0;

            _callCode = _sapModel.LoadCases.DirHistNonlinear.GetGeometricNonlinearity(name, ref csiGeometricNonlinearityType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            geometricNonlinearityType = (eGeometricNonlinearity) csiGeometricNonlinearityType;
        }

        /// <summary>
        /// This function sets the geometric nonlinearity option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="geometricNonlinearityType">The geometric nonlinearity option selected for the load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetGeometricNonlinearity(string name,
            eGeometricNonlinearity geometricNonlinearityType)
        {
            _callCode = _sapModel.LoadCases.DirHistNonlinear.SetGeometricNonlinearity(name, (int)geometricNonlinearityType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxSubstepSize">Maximum size of the substep.</param>
        /// <param name="minSubstepSize">Minimum size of the substep.</param>
        /// <param name="maxConstantStiffnessIterationsPerStep">The maximum constant stiffness iterations per step.</param>
        /// <param name="maxNewtonRaphsonIterationsPerStep">The maximum Newton-Raphson iterations per step.</param>
        /// <param name="relativeIterationConvergenceTolerance">The relative iteration convergence tolerance.</param>
        /// <param name="useEventStepping">True: Event-to-event stepping is used.</param>
        /// <param name="relativeEventLumpingTolerance">The relative event lumping tolerance.</param>
        /// <param name="maxNumberLineSearches">The maximum number of line-searches per iteration.</param>
        /// <param name="relativeLineSearchAcceptanceTolerance">The relative line-search acceptance tolerance.</param>
        /// <param name="lineSearchStepFactor">The line-search step factor.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSolutionControlParameters(string name,
            ref double maxSubstepSize,
            ref double minSubstepSize,
            ref int maxConstantStiffnessIterationsPerStep,
            ref int maxNewtonRaphsonIterationsPerStep,
            ref double relativeIterationConvergenceTolerance,
            ref bool useEventStepping,
            ref double relativeEventLumpingTolerance,
            ref int maxNumberLineSearches,
            ref double relativeLineSearchAcceptanceTolerance,
            ref double lineSearchStepFactor)
        {
            _callCode = _sapModel.LoadCases.DirHistNonlinear.GetSolControlParameters(name,
                            ref maxSubstepSize,
                            ref minSubstepSize,
                            ref maxConstantStiffnessIterationsPerStep,
                            ref maxNewtonRaphsonIterationsPerStep,
                            ref relativeIterationConvergenceTolerance,
                            ref useEventStepping,
                            ref relativeEventLumpingTolerance,
                            ref maxNumberLineSearches,
                            ref relativeLineSearchAcceptanceTolerance,
                            ref lineSearchStepFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxSubstepSize">Maximum size of the substep.</param>
        /// <param name="minSubstepSize">Minimum size of the substep.</param>
        /// <param name="maxConstantStiffnessIterationsPerStep">The maximum constant stiffness iterations per step.</param>
        /// <param name="maxNewtonRaphsonIterationsPerStep">The maximum Newton-Raphson iterations per step.</param>
        /// <param name="relativeIterationConvergenceTolerance">The relative iteration convergence tolerance.</param>
        /// <param name="useEventStepping">True: Event-to-event stepping is used.</param>
        /// <param name="relativeEventLumpingTolerance">The relative event lumping tolerance.</param>
        /// <param name="maxNumberLineSearches">The maximum number of line-searches per iteration.</param>
        /// <param name="relativeLineSearchAcceptanceTolerance">The relative line-search acceptance tolerance.</param>
        /// <param name="lineSearchStepFactor">The line-search step factor.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSolutionControlParameters(string name,
            double maxSubstepSize,
            double minSubstepSize,
            int maxConstantStiffnessIterationsPerStep,
            int maxNewtonRaphsonIterationsPerStep,
            double relativeIterationConvergenceTolerance,
            bool useEventStepping,
            double relativeEventLumpingTolerance,
            int maxNumberLineSearches,
            double relativeLineSearchAcceptanceTolerance,
            double lineSearchStepFactor)
        {
            _callCode = _sapModel.LoadCases.DirHistNonlinear.SetSolControlParameters(name,
                            maxSubstepSize,
                            minSubstepSize,
                            maxConstantStiffnessIterationsPerStep,
                            maxNewtonRaphsonIterationsPerStep,
                            relativeIterationConvergenceTolerance,
                            useEventStepping,
                            relativeEventLumpingTolerance,
                            maxNumberLineSearches,
                            relativeLineSearchAcceptanceTolerance,
                            lineSearchStepFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
