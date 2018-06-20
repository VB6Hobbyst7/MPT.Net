// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="StaticNonlinearStaged.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using MPT.CSI.API.Core.Support;
using MPT.Enums;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the static nonlinear staged construction load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IStaticNonlinearStaged" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class StaticNonlinearStaged : CSiApiBase, IStaticNonlinearStaged
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticNonlinearStaged" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public StaticNonlinearStaged(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Interface
        /// <summary>
        /// This function initializes a load case. <para />
        /// If this function is called for an existing load case, all items for the case are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new load case. <para />
        /// If this is an existing case, that case is modified; otherwise, a new case is added.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCase(string name)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetCase(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the initial condition assumed for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="initialCase">This is blank, None, or the name of an existing analysis case. <para />
        /// This item specifies if the load case starts from zero initial conditions, that is, an unstressed state, or if it starts using the stiffness that occurs at the end of a nonlinear static or nonlinear direct integration time history load case.<para />
        /// If the specified initial case is a nonlinear static or nonlinear direct integration time history load case, the stiffness at the end of that case is used.<para />
        /// If the initial case is anything else then zero initial conditions are assumed.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetInitialCase(string name,
            ref string initialCase)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetInitialCase(name, ref initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function sets the initial condition for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="initialCase">This is blank, None, or the name of an existing analysis case. <para />
        /// This item specifies if the load case starts from zero initial conditions, that is, an unstressed state, or if it starts using the stiffness that occurs at the end of a nonlinear static or nonlinear direct integration time history load case.<para />
        /// If the specified initial case is a nonlinear static or nonlinear direct integration time history load case, the stiffness at the end of that case is used.<para />
        /// If the initial case is anything else then zero initial conditions are assumed.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetInitialCase(string name,
            string initialCase)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetInitialCase(name, initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        
        /// <summary>
        /// This function retrieves the mass source to be used for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="sourceName">This is the name of an existing mass source or a blank string. <para />
        /// Blank indicates to use the mass source from the previous load case or the default mass source if the load case starts from zero initial conditions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMassSource(string name,
            ref string sourceName)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetMassSource(name, ref sourceName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the mass source to be used for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="sourceName">This is the name of an existing mass source or a blank string. <para />
        /// Blank indicates to use the mass source from the previous load case or the default mass source if the load case starts from zero initial conditions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMassSource(string name,
            string sourceName)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetMassSource(name, sourceName);
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

            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetGeometricNonlinearity(name, ref csiGeometricNonlinearityType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            geometricNonlinearityType = (eGeometricNonlinearity)csiGeometricNonlinearityType;
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
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetGeometricNonlinearity(name, (int)geometricNonlinearityType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxTotalSteps">The maximum total steps per stage.</param>
        /// <param name="maxNullSteps">The maximum null (zero) steps per stage.</param>
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
            ref int maxTotalSteps,
            ref int maxNullSteps,
            ref int maxConstantStiffnessIterationsPerStep,
            ref int maxNewtonRaphsonIterationsPerStep,
            ref double relativeIterationConvergenceTolerance,
            ref bool useEventStepping,
            ref double relativeEventLumpingTolerance,
            ref int maxNumberLineSearches,
            ref double relativeLineSearchAcceptanceTolerance,
            ref double lineSearchStepFactor)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetSolControlParameters(name,
                            ref maxTotalSteps,
                            ref maxNullSteps,
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
        /// <param name="maxTotalSteps">The maximum total steps per stage.</param>
        /// <param name="maxNullSteps">The maximum null (zero) steps per stage.</param>
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
            int maxTotalSteps,
            int maxNullSteps,
            int maxConstantStiffnessIterationsPerStep,
            int maxNewtonRaphsonIterationsPerStep,
            double relativeIterationConvergenceTolerance,
            bool useEventStepping,
            double relativeEventLumpingTolerance,
            int maxNumberLineSearches,
            double relativeLineSearchAcceptanceTolerance,
            double lineSearchStepFactor)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetSolControlParameters(name,
                            maxTotalSteps,
                            maxNullSteps,
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




        /// <summary>
        /// This function retrieves the material nonlinearity options for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged analysis case.</param>
        /// <param name="considerTimeDependent">True: Any specified time dependent material properties are considered in the analysis.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMaterialNonlinearity(string name,
            ref bool considerTimeDependent)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetMaterialNonlinearity(name, ref considerTimeDependent);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the material nonlinearity options for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged analysis case.</param>
        /// <param name="considerTimeDependent">True: Any specified time dependent material properties are considered in the analysis.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMaterialNonlinearity(string name,
            ref bool considerTimeDependent)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetMaterialNonlinearity(name, considerTimeDependent);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves the results saved parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged analysis case.</param>
        /// <param name="stageSavedOption">The results saved option for the load case.</param>
        /// <param name="minStepsForInstantanousLoad">The minimum number of steps for application of instantaneous load. <para />
        /// This item applies only when <paramref name="stageSavedOption" /> = <see cref="eStageSavedOption.TwoOrMoreTimesPerStage" />.</param>
        /// <param name="minStepsForTimeDependentItems">The minimum number of steps for analysis of time dependent items. <para />
        /// This item applies only when <paramref name="stageSavedOption" /> = <see cref="eStageSavedOption.TwoOrMoreTimesPerStage" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetResultsSaved(string name,
            ref eStageSavedOption stageSavedOption,
            ref int minStepsForInstantanousLoad,
            ref int minStepsForTimeDependentItems)
        {
            int csiStageSavedOption = 0;

            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetResultsSaved(name, ref csiStageSavedOption, ref minStepsForInstantanousLoad, ref minStepsForTimeDependentItems);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stageSavedOption = (eStageSavedOption) csiStageSavedOption;
        }

        /// <summary>
        /// This function sets the results saved parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged analysis case.</param>
        /// <param name="stageSavedOption">The results saved option for the load case.</param>
        /// <param name="minStepsForInstantanousLoad">The minimum number of steps for application of instantaneous load. <para />
        /// This item applies only when <paramref name="stageSavedOption" /> = <see cref="eStageSavedOption.TwoOrMoreTimesPerStage" />.</param>
        /// <param name="minStepsForTimeDependentItems">The minimum number of steps for analysis of time dependent items. <para />
        /// This item applies only when <paramref name="stageSavedOption" /> = <see cref="eStageSavedOption.TwoOrMoreTimesPerStage" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetResultsSaved(string name,
            eStageSavedOption stageSavedOption,
            int minStepsForInstantanousLoad = 1,
            int minStepsForTimeDependentItems = 1)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetResultsSaved(name, (int)stageSavedOption, minStepsForInstantanousLoad, minStepsForTimeDependentItems);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if BUILD_ETABS2015 || BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_CSiBridgev16 || BUILD_CSiBridgev17 ||BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
        /// <summary>
        /// This function retrieves stage data for the specified stage in the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="stage">The stage in the specified load case for which data is requested. <para />
        /// Stages are numbered sequentially starting from 1.</param>
        /// <param name="operations">The stage construction operation.</param>
        /// <param name="objectTypes">The object type associated with the specified operation.<para />
        /// The following list shows which object types are applicable to each operation type:<para /><paramref name="operations" /> = <see cref="eStageOperations.AddStructure" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.RemoveStructure" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.LoadObjects" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" />:  All object types except Point;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" />:  Group, Frame, Cable, Area;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" />:  Group, Frame;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />: All object types except Point;</param>
        /// <param name="nameObjects">The name of the object associated with the specified operation. <para />
        /// This is the name of a Group, Frame object, Cable object, Tendon object, Area object, Solid object, Link object or Point object, depending on the <paramref name="objectTypes" /> item.</param>
        /// <param name="ages">The age of the added structure, at the time it is added, in days. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.AddStructure" />.</param>
        /// <param name="loadOrObjectTypes">A load type or an object type, depending on what is specified for the <paramref name="operations" /> item. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />, <see cref="eStageOperations.LoadObjects" />, <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />, this is an array that includes Load or Accel, indicating the load type of an added load.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" /> or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, Cable, Tendon, Area, Solid or Link, indicating the object type for which the section property is changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" /> and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, Cable or Area, indicating the object type for which the section property modifiers are changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" /> and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, indicating the object type for which the releases are changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, and the <paramref name="objectTypes" /> item is not Group and not Point, this item is ignored and the type is picked up from the <paramref name="objectTypes" /> item.</param>
        /// <param name="loadOrObjectNames">A load assignment or an object name, depending on what is specified for the <paramref name="operations" /> item. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />, <see cref="eStageOperations.LoadObjects" />, <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />, this is an array that includes the name of the load assigned to the operation. <para />
        /// If the associated LoadType item is Load, this item is the name of a defined load pattern.If the associated LoadType item is Accel , this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" /> or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, this is the name of a Frame, Cable, Tendon, Area, Solid or Link object, depending on the object type specified.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, this is the name of a Frame, Cable or Area object, depending on the object type specified.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" />, this is the name of a Frame object.</param>
        /// <param name="scaleFactors">This is an array that includes the scale factor for the load assigned to the operation, if any. <para />
        /// [L/s^2] for Accel UX UY and UZ; otherwise unitless<para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetStageData(string name,
            ref int stage,
            ref eStageOperations[] operations,
            ref eObjectType[] objectTypes,
            ref string[] nameObjects,
            ref int[] ages,
            ref string[] loadOrObjectTypes,
            ref string[] loadOrObjectNames,
            ref double[] scaleFactors)
        {
            int[] csiOperations = new int[0];
            string[] csiObjectTypes = new string[0];
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetStageData_1(name,
                            ref stage,
                            ref _numberOfItems,
                            ref csiOperations,
                            ref csiObjectTypes,
                            ref nameObjects,
                            ref ages,
                            ref loadOrObjectTypes,
                            ref loadOrObjectNames,
                            ref scaleFactors);

            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            operations = csiOperations.Cast<eStageOperations>().ToArray();

            objectTypes = new eObjectType[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                objectTypes[i] = EnumLibrary.ConvertStringToEnumByDescription<eObjectType>(csiObjectTypes[i]);
            }
            // TODO: Handle loadOrObjectTypes & loadOrObjectNames in OOP wrapper as these are dependent upon objectTypes
        }

        /// <summary>
        /// This function sets the stage data for the specified stage in the specified load case. <para />
        /// All previous stage data for the specified stage is cleared when this function is called.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="stage">The stage in the specified load case for which data is requested. <para />
        /// Stages are numbered sequentially starting from 1.</param>
        /// <param name="operations">The stage construction operation.</param>
        /// <param name="objectTypes">The object type associated with the specified operation.<para />
        /// The following list shows which object types are applicable to each operation type:<para /><paramref name="operations" /> = <see cref="eStageOperations.AddStructure" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.RemoveStructure" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.LoadObjects" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" />:  All object types except Point;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" />:  Group, Frame, Cable, Area;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" />:  Group, Frame;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />: All object types except Point;</param>
        /// <param name="nameObjects">The name of the object associated with the specified operation. <para />
        /// This is the name of a Group, Frame object, Cable object, Tendon object, Area object, Solid object, Link object or Point object, depending on the <paramref name="objectTypes" /> item.</param>
        /// <param name="ages">The age of the added structure, at the time it is added, in days. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.AddStructure" />.</param>
        /// <param name="loadOrObjectTypes">A load type or an object type, depending on what is specified for the <paramref name="operations" /> item. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />, <see cref="eStageOperations.LoadObjects" />, <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />, this is an array that includes Load or Accel, indicating the load type of an added load.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" /> or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, Cable, Tendon, Area, Solid or Link, indicating the object type for which the section property is changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" /> and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, Cable or Area, indicating the object type for which the section property modifiers are changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" /> and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, indicating the object type for which the releases are changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, and the <paramref name="objectTypes" /> item is not Group and not Point, this item is ignored and the type is picked up from the <paramref name="objectTypes" /> item.</param>
        /// <param name="loadOrObjectNames">A load assignment or an object name, depending on what is specified for the <paramref name="operations" /> item. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />, <see cref="eStageOperations.LoadObjects" />, <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />, this is an array that includes the name of the load assigned to the operation. <para />
        /// If the associated LoadType item is Load, this item is the name of a defined load pattern.If the associated LoadType item is Accel , this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" /> or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, this is the name of a Frame, Cable, Tendon, Area, Solid or Link object, depending on the object type specified.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, this is the name of a Frame, Cable or Area object, depending on the object type specified.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" />, this is the name of a Frame object.</param>
        /// <param name="scaleFactors">This is an array that includes the scale factor for the load assigned to the operation, if any. <para />
        /// [L/s^2] for Accel UX UY and UZ; otherwise unitless<para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetStageData(string name,
            int stage,
            eStageOperations[] operations,
            eObjectType[] objectTypes,
            string[] nameObjects,
            int[] ages,
            string[] loadOrObjectTypes,
            string[] loadOrObjectNames,
            double[] scaleFactors)
        {
            arraysLengthMatch(nameof(operations), operations.Length, nameof(objectTypes), objectTypes.Length);
            arraysLengthMatch(nameof(operations), operations.Length, nameof(nameObjects), nameObjects.Length);
            arraysLengthMatch(nameof(operations), operations.Length, nameof(ages), ages.Length);
            arraysLengthMatch(nameof(operations), operations.Length, nameof(loadOrObjectTypes), loadOrObjectTypes.Length);
            arraysLengthMatch(nameof(operations), operations.Length, nameof(loadOrObjectNames), loadOrObjectNames.Length);
            arraysLengthMatch(nameof(operations), operations.Length, nameof(scaleFactors), scaleFactors.Length);

            int[] csiOperations = operations.Cast<int>().ToArray();
            string[] csiObjectTypes = new string[operations.Length - 1];
            for (int i = 0; i < operations.Length; i++)
            {
                csiObjectTypes[i] = objectTypes[i].ToString();
            }

            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetStageData_1(name, stage, operations.Length,
                ref csiOperations, ref csiObjectTypes, ref nameObjects, ref ages, ref loadOrObjectTypes, ref loadOrObjectNames, ref scaleFactors);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            // TODO: Handle loadOrObjectTypes & loadOrObjectNames in OOP wrapper as these are dependent upon objectTypes
        }

        /// <summary>
        /// This function retrieves the stage definition data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="duration">The duration in days for each stage.</param>
        /// <param name="outputIsToBeSaved">True: The analysis output is to be saved for each stage.</param>
        /// <param name="nameOutput">A user-specified output name for each stage.</param>
        /// <param name="comment">A comment for each stage. <para />
        /// The comment may be a blank string.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetStageDefinitions(string name,
            ref int[] duration,
            ref bool[] outputIsToBeSaved,
            ref string[] nameOutput,
            ref string[] comment)
        {

            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetStageDefinitions_1(name, ref _numberOfItems, ref duration, ref outputIsToBeSaved, ref nameOutput, ref comment);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes the stage definition data for the specified load case. <para />
        /// All previous stage definition data for the case is cleared when this function is called.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="duration">The duration in days for each stage.</param>
        /// <param name="outputIsToBeSaved">True: The analysis output is to be saved for each stage.</param>
        /// <param name="nameOutput">A user-specified output name for each stage.</param>
        /// <param name="comment">A comment for each stage. <para />
        /// The comment may be a blank string.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetStageDefinitions(string name,
            int[] duration,
            bool[] outputIsToBeSaved,
            string[] nameOutput,
            string[] comment)
        {
            arraysLengthMatch(nameof(duration), duration.Length, nameof(outputIsToBeSaved), outputIsToBeSaved.Length);
            arraysLengthMatch(nameof(duration), duration.Length, nameof(nameOutput), nameOutput.Length);
            arraysLengthMatch(nameof(duration), duration.Length, nameof(comment), comment.Length);

            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetStageDefinitions_1(name, duration.Length, ref duration, ref outputIsToBeSaved, ref nameOutput, ref comment);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#else
        /// <summary>
        /// This function retrieves stage data for the specified stage in the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="stage">The stage in the specified load case for which data is requested. <para />
        /// Stages are numbered sequentially starting from 1.</param>
        /// <param name="operations">The stage construction operation.</param>
        /// <param name="objectTypes">The object type associated with the specified operation.<para />
        /// The following list shows which object types are applicable to each operation type:<para /><paramref name="operations" /> = <see cref="eStageOperations.AddStructure" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.RemoveStructure" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.LoadObjects" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" />:  All object types except Point;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" />:  Group, Frame, Cable, Area;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" />:  Group, Frame;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />: All object types except Point;</param>
        /// <param name="nameObjects">The name of the object associated with the specified operation. <para />
        /// This is the name of a Group, Frame object, Cable object, Tendon object, Area object, Solid object, Link object or Point object, depending on the <paramref name="objectTypes" /> item.</param>
        /// <param name="ages">The age of the added structure, at the time it is added, in days. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.AddStructure" />.</param>
        /// <param name="loadOrObjectTypes">A load type or an object type, depending on what is specified for the <paramref name="operations" /> item. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />, <see cref="eStageOperations.LoadObjects" />, <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />, this is an array that includes Load or Accel, indicating the load type of an added load.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" /> or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, Cable, Tendon, Area, Solid or Link, indicating the object type for which the section property is changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" /> and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, Cable or Area, indicating the object type for which the section property modifiers are changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" /> and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, indicating the object type for which the releases are changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, and the <paramref name="objectTypes" /> item is not Group and not Point, this item is ignored and the type is picked up from the <paramref name="objectTypes" /> item.</param>
        /// <param name="loadOrObjectNames">A load assignment or an object name, depending on what is specified for the <paramref name="operations" /> item. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />, <see cref="eStageOperations.LoadObjects" />, <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />, this is an array that includes the name of the load assigned to the operation. <para />
        /// If the associated LoadType item is Load, this item is the name of a defined load pattern.If the associated LoadType item is Accel , this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" /> or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, this is the name of a Frame, Cable, Tendon, Area, Solid or Link object, depending on the object type specified.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, this is the name of a Frame, Cable or Area object, depending on the object type specified.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" />, this is the name of a Frame object.</param>
        /// <param name="scaleFactors">This is an array that includes the scale factor for the load assigned to the operation, if any. <para />
        /// [L/s^2] for Accel UX UY and UZ; otherwise unitless<para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetStageData(string name,
            ref int stage,
            ref eStageOperations[] operations,
            ref eObjectType[] objectTypes,
            ref string[] nameObjects,
            ref double[] ages,
            ref string[] loadOrObjectTypes,
            ref string[] loadOrObjectNames,
            ref double[] scaleFactors)
        {
            int[] csiOperations = new int[0];
            string[] csiObjectTypes = new string[0];
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetStageData_2(name, 
                            ref stage, 
                            ref _numberOfItems, 
                            ref csiOperations, 
                            ref csiObjectTypes, 
                            ref nameObjects, 
                            ref ages, 
                            ref loadOrObjectTypes, 
                            ref loadOrObjectNames, 
                            ref scaleFactors);

            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            operations = csiOperations.Cast< eStageOperations>().ToArray();

            objectTypes = new eObjectType[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                objectTypes[i] = EnumLibrary.ConvertStringToEnumByDescription<eObjectType>(csiObjectTypes[i]);
            }
            // TODO: Handle loadOrObjectTypes & loadOrObjectNames in OOP wrapper as these are dependent upon objectTypes
        }

        /// <summary>
        /// This function sets the stage data for the specified stage in the specified load case. <para />
        /// All previous stage data for the specified stage is cleared when this function is called.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="stage">The stage in the specified load case for which data is requested. <para />
        /// Stages are numbered sequentially starting from 1.</param>
        /// <param name="operations">The stage construction operation.</param>
        /// <param name="objectTypes">The object type associated with the specified operation.<para />
        /// The following list shows which object types are applicable to each operation type:<para /><paramref name="operations" /> = <see cref="eStageOperations.AddStructure" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.RemoveStructure" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.LoadObjects" />:  All object types;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" />:  All object types except Point;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" />:  Group, Frame, Cable, Area;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" />:  Group, Frame;<para /><paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />: All object types except Point;</param>
        /// <param name="nameObjects">The name of the object associated with the specified operation. <para />
        /// This is the name of a Group, Frame object, Cable object, Tendon object, Area object, Solid object, Link object or Point object, depending on the <paramref name="objectTypes" /> item.</param>
        /// <param name="ages">The age of the added structure, at the time it is added, in days. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.AddStructure" />.</param>
        /// <param name="loadOrObjectTypes">A load type or an object type, depending on what is specified for the <paramref name="operations" /> item. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />, <see cref="eStageOperations.LoadObjects" />, <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />, this is an array that includes Load or Accel, indicating the load type of an added load.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" /> or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, Cable, Tendon, Area, Solid or Link, indicating the object type for which the section property is changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" /> and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, Cable or Area, indicating the object type for which the section property modifiers are changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" /> and the <paramref name="objectTypes" /> item is Group, this is an array that includes Frame, indicating the object type for which the releases are changed.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, and the <paramref name="objectTypes" /> item is not Group and not Point, this item is ignored and the type is picked up from the <paramref name="objectTypes" /> item.</param>
        /// <param name="loadOrObjectNames">A load assignment or an object name, depending on what is specified for the <paramref name="operations" /> item. <para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" />, <see cref="eStageOperations.LoadObjects" />, <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />, this is an array that includes the name of the load assigned to the operation. <para />
        /// If the associated LoadType item is Load, this item is the name of a defined load pattern.If the associated LoadType item is Accel , this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionProperties" /> or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, this is the name of a Frame, Cable, Tendon, Area, Solid or Link object, depending on the object type specified.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, this is the name of a Frame, Cable or Area object, depending on the object type specified.<para />
        /// When <paramref name="operations" /> = <see cref="eStageOperations.ChangeReleases" />, this is the name of a Frame object.</param>
        /// <param name="scaleFactors">This is an array that includes the scale factor for the load assigned to the operation, if any. <para />
        /// [L/s^2] for Accel UX UY and UZ; otherwise unitless<para />
        /// This item applies only to operations with <paramref name="operations" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetStageData(string name,
            int stage,
            eStageOperations[] operations,
            eObjectType[] objectTypes,
            string[] nameObjects,
            double[] ages,
            string[] loadOrObjectTypes,
            string[] loadOrObjectNames,
            double[] scaleFactors)
        {
            arraysLengthMatch(nameof(operations), operations.Length, nameof(objectTypes), objectTypes.Length);
            arraysLengthMatch(nameof(operations), operations.Length, nameof(nameObjects), nameObjects.Length);
            arraysLengthMatch(nameof(operations), operations.Length, nameof(ages), ages.Length);
            arraysLengthMatch(nameof(operations), operations.Length, nameof(loadOrObjectTypes), loadOrObjectTypes.Length);
            arraysLengthMatch(nameof(operations), operations.Length, nameof(loadOrObjectNames), loadOrObjectNames.Length);
            arraysLengthMatch(nameof(operations), operations.Length, nameof(scaleFactors), scaleFactors.Length);

            int[] csiOperations = operations.Cast<int>().ToArray();
            string[] csiObjectTypes = new string[operations.Length - 1];
            for (int i = 0; i < operations.Length; i++)
            {
                csiObjectTypes[i] = objectTypes[i].ToString();
            }
            
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetStageData_2(name, stage, operations.Length,
                ref csiOperations, ref csiObjectTypes, ref nameObjects, ref ages, ref loadOrObjectTypes, ref loadOrObjectNames, ref scaleFactors);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            // TODO: Handle loadOrObjectTypes & loadOrObjectNames in OOP wrapper as these are dependent upon objectTypes
        }

        /// <summary>
        /// This function retrieves the stage definition data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="duration">The duration in days for each stage.</param>
        /// <param name="outputIsToBeSaved">True: The analysis output is to be saved for each stage.</param>
        /// <param name="nameOutput">A user-specified output name for each stage.</param>
        /// <param name="comment">A comment for each stage. <para />
        /// The comment may be a blank string.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetStageDefinitions(string name,
            ref double[] duration,
            ref bool[] outputIsToBeSaved,
            ref string[] nameOutput,
            ref string[] comment)
        {

            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetStageDefinitions_2(name, ref _numberOfItems, ref duration, ref outputIsToBeSaved, ref nameOutput, ref comment);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes the stage definition data for the specified load case. <para />
        /// All previous stage definition data for the case is cleared when this function is called.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="duration">The duration in days for each stage.</param>
        /// <param name="outputIsToBeSaved">True: The analysis output is to be saved for each stage.</param>
        /// <param name="nameOutput">A user-specified output name for each stage.</param>
        /// <param name="comment">A comment for each stage. <para />
        /// The comment may be a blank string.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetStageDefinitions(string name,
            double[] duration,
            bool[] outputIsToBeSaved,
            string[] nameOutput,
            string[] comment)
        {
            arraysLengthMatch(nameof(duration), duration.Length, nameof(outputIsToBeSaved), outputIsToBeSaved.Length);
            arraysLengthMatch(nameof(duration), duration.Length, nameof(nameOutput), nameOutput.Length);
            arraysLengthMatch(nameof(duration), duration.Length, nameof(comment), comment.Length);

            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetStageDefinitions_2(name, duration.Length, ref duration, ref outputIsToBeSaved, ref nameOutput, ref comment);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif




        /// <summary>
        /// This function retrieves the hinge unloading option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="hingeUnloadingType">Type of hinge unloading for the selected load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetHingeUnloading(string name,
            ref eHingeUnloadingType hingeUnloadingType)
        {
            int csiHingeUnloadingType = 0;

            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetHingeUnloading(name, ref csiHingeUnloadingType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            hingeUnloadingType = (eHingeUnloadingType)csiHingeUnloadingType;
        }

        /// <summary>
        /// This function sets the hinge unloading option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="hingeUnloadingType">Type of hinge unloading for the selected load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetHingeUnloading(string name,
            eHingeUnloadingType hingeUnloadingType)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetHingeUnloading(name, (int)hingeUnloadingType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the target force iteration parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="convergenceTolerance">The relative convergence tolerance for target force iteration.</param>
        /// <param name="maxIterations">The maximum iterations per stage for target force iteration.</param>
        /// <param name="accelerationFactor">The acceleration factor.</param>
        /// <param name="continueIfNoConvergence">True: Analysis is continued when there is no convergence in the target force iteration</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTargetForceParameters(string name,
            ref double convergenceTolerance,
            ref int maxIterations,
            ref double accelerationFactor,
            ref bool continueIfNoConvergence)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetTargetForceParameters(name, ref convergenceTolerance, ref maxIterations, ref accelerationFactor, ref continueIfNoConvergence);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the target force iteration parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="convergenceTolerance">The relative convergence tolerance for target force iteration.</param>
        /// <param name="maxIterations">The maximum iterations per stage for target force iteration.</param>
        /// <param name="accelerationFactor">The acceleration factor.</param>
        /// <param name="continueIfNoConvergence">True: Analysis is continued when there is no convergence in the target force iteration</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetTargetForceParameters(string name,
            double convergenceTolerance,
            int maxIterations,
            double accelerationFactor,
            bool continueIfNoConvergence)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetTargetForceParameters(name, convergenceTolerance, maxIterations, accelerationFactor, continueIfNoConvergence);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
