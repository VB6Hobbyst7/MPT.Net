// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IStaticNonlinearStaged.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the static nonlinear staged construction load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ISetLoadCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IInitialLoadCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IMassSource" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IGeometricNonlinearity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.INonlinearSolutionControlParameters" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IHingeUnloading" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ITargetForceParameters" />
    public interface IStaticNonlinearStaged :
        ISetLoadCase, IInitialLoadCase, IMassSource, 
        IGeometricNonlinearity, INonlinearSolutionControlParameters, IHingeUnloading, ITargetForceParameters
    {
        /// <summary>
        /// This function retrieves the material nonlinearity options for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged analysis case.</param>
        /// <param name="considerTimeDependent">True: Any specified time dependent material properties are considered in the analysis.</param>
        void GetMaterialNonlinearity(string name,
            ref bool considerTimeDependent);

        /// <summary>
        /// This function sets the material nonlinearity options for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged analysis case.</param>
        /// <param name="considerTimeDependent">True: Any specified time dependent material properties are considered in the analysis.</param>
        void SetMaterialNonlinearity(string name,
            ref bool considerTimeDependent);



        /// <summary>
        /// This function retrieves the results saved parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged analysis case.</param>
        /// <param name="stageSavedOption">The results saved option for the load case.</param>
        /// <param name="minStepsForInstantanousLoad">The minimum number of steps for application of instantaneous load.
        /// This item applies only when <paramref name="stageSavedOption" /> = <see cref="eStageSavedOption.TwoOrMoreTimesPerStage" />.</param>
        /// <param name="minStepsForTimeDependentItems">The minimum number of steps for analysis of time dependent items.
        /// This item applies only when <paramref name="stageSavedOption" /> = <see cref="eStageSavedOption.TwoOrMoreTimesPerStage" />.</param>
        void GetResultsSaved(string name,
            ref eStageSavedOption stageSavedOption,
            ref int minStepsForInstantanousLoad,
            ref int minStepsForTimeDependentItems);

        /// <summary>
        /// This function sets the results saved parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged analysis case.</param>
        /// <param name="stageSavedOption">The results saved option for the load case.</param>
        /// <param name="minStepsForInstantanousLoad">The minimum number of steps for application of instantaneous load.
        /// This item applies only when <paramref name="stageSavedOption" /> = <see cref="eStageSavedOption.TwoOrMoreTimesPerStage" />.</param>
        /// <param name="minStepsForTimeDependentItems">The minimum number of steps for analysis of time dependent items.
        /// This item applies only when <paramref name="stageSavedOption" /> = <see cref="eStageSavedOption.TwoOrMoreTimesPerStage" />.</param>
        void SetResultsSaved(string name,
            eStageSavedOption stageSavedOption,
            int minStepsForInstantanousLoad = 1,
            int minStepsForTimeDependentItems = 1);

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
        void GetStageData(string name,
            ref int stage,
            ref eStageOperations[] operations,
            ref eObjectType[] objectTypes,
            ref string[] nameObjects,
            ref int[] ages,
            ref string[] loadOrObjectTypes,
            ref string[] loadOrObjectNames,
            ref double[] scaleFactors);

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
        void SetStageData(string name,
            int stage,
            eStageOperations[] operations,
            eObjectType[] objectTypes,
            string[] nameObjects,
            int[] ages,
            string[] loadOrObjectTypes,
            string[] loadOrObjectNames,
            double[] scaleFactors);



        /// <summary>
        /// This function retrieves the stage definition data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="duration">The duration in days for each stage.</param>
        /// <param name="outputIsToBeSaved">True: The analysis output is to be saved for each stage.</param>
        /// <param name="nameOutput">A user-specified output name for each stage.</param>
        /// <param name="comment">A comment for each stage.
        /// The comment may be a blank string.</param>
        void GetStageDefinitions(string name,
            ref int[] duration,
            ref bool[] outputIsToBeSaved,
            ref string[] nameOutput,
            ref string[] comment);

        /// <summary>
        /// This function initializes the stage definition data for the specified load case.
        /// All previous stage definition data for the case is cleared when this function is called.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="duration">The duration in days for each stage.</param>
        /// <param name="outputIsToBeSaved">True: The analysis output is to be saved for each stage.</param>
        /// <param name="nameOutput">A user-specified output name for each stage.</param>
        /// <param name="comment">A comment for each stage.
        /// The comment may be a blank string.</param>
        void SetStageDefinitions(string name,
            int[] duration,
            bool[] outputIsToBeSaved,
            string[] nameOutput,
            string[] comment);
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
        void GetStageData(string name,
            ref int stage,
            ref eStageOperations[] operations,
            ref eObjectType[] objectTypes,
            ref string[] nameObjects,
            ref double[] ages,
            ref string[] loadOrObjectTypes,
            ref string[] loadOrObjectNames,
            ref double[] scaleFactors);

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
        void SetStageData(string name,
            int stage,
            eStageOperations[] operations,
            eObjectType[] objectTypes,
            string[] nameObjects,
            double[] ages,
            string[] loadOrObjectTypes,
            string[] loadOrObjectNames,
            double[] scaleFactors);



        /// <summary>
        /// This function retrieves the stage definition data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="duration">The duration in days for each stage.</param>
        /// <param name="outputIsToBeSaved">True: The analysis output is to be saved for each stage.</param>
        /// <param name="nameOutput">A user-specified output name for each stage.</param>
        /// <param name="comment">A comment for each stage.
        /// The comment may be a blank string.</param>
        void GetStageDefinitions(string name,
            ref double[] duration,
            ref bool[] outputIsToBeSaved,
            ref string[] nameOutput,
            ref string[] comment);

        /// <summary>
        /// This function initializes the stage definition data for the specified load case.
        /// All previous stage definition data for the case is cleared when this function is called.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear staged load case.</param>
        /// <param name="duration">The duration in days for each stage.</param>
        /// <param name="outputIsToBeSaved">True: The analysis output is to be saved for each stage.</param>
        /// <param name="nameOutput">A user-specified output name for each stage.</param>
        /// <param name="comment">A comment for each stage.
        /// The comment may be a blank string.</param>
        void SetStageDefinitions(string name,
            double[] duration,
            bool[] outputIsToBeSaved,
            string[] nameOutput,
            string[] comment);
#endif
    }
}
