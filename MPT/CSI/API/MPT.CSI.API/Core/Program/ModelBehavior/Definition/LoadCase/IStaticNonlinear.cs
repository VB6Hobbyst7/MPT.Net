// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IStaticNonlinear.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the static nonlinear load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ISetLoadCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IInitialLoadCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IMassSource" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IObservableModalCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IChangeableModalCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IGeometricNonlinearity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.INonlinearSolutionControlParameters" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IHingeUnloading" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ITargetForceParameters" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ILoadStatic" />
    public interface IStaticNonlinear:
        ISetLoadCase, IInitialLoadCase, IMassSource,
        IObservableModalCase, IChangeableModalCase, 
        IGeometricNonlinearity, INonlinearSolutionControlParameters, IHingeUnloading, ITargetForceParameters,
        ILoadStatic
    {
        /// <summary>
        /// This function retrieves the results saved parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="saveMultipleSteps">True: Multiple states are saved for the nonlinear analysis.
        /// False: Only if the final state is saved.</param>
        /// <param name="minSavedStates">The minimum number of saved states.
        /// This item applies only when <paramref name="saveMultipleSteps" /> = True.</param>
        /// <param name="maxSavedStates">The maximum number of saved states.
        /// This item applies only when <paramref name="saveMultipleSteps" /> = True.</param>
        /// <param name="savePositiveDisplacementIncrementsOnly">True: Only positive displacement increments are saved.
        /// False: All displacement increments are saved.</param>
        void GetResultsSaved(string name,
            ref bool saveMultipleSteps,
            ref int minSavedStates,
            ref int maxSavedStates,
            ref bool savePositiveDisplacementIncrementsOnly);

        /// <summary>
        /// This function sets the results saved parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="saveMultipleSteps">True: Multiple states are saved for the nonlinear analysis.
        /// False: Only if the final state is saved.</param>
        /// <param name="minSavedStates">The minimum number of saved states.
        /// This item applies only when <paramref name="saveMultipleSteps" /> = True.</param>
        /// <param name="maxSavedStates">The maximum number of saved states.
        /// This item applies only when <paramref name="saveMultipleSteps" /> = True.</param>
        /// <param name="savePositiveDisplacementIncrementsOnly">True: Only positive displacement increments are saved.
        /// False: All displacement increments are saved.</param>
        void SetResultsSaved(string name,
            bool saveMultipleSteps,
            int minSavedStates = 10,
            int maxSavedStates = 100,
            bool savePositiveDisplacementIncrementsOnly = true);




        /// <summary>
        /// This function retrieves the load application control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="loadControl">The load application control method.</param>
        /// <param name="controlDisplacementType">Type of control displacement.</param>
        /// <param name="targetDisplacement">The structure is loaded to a monitored displacement of this magnitude.
        /// [L] when <paramref name="degreeOfFreedom" /> = <see cref="eDegreeOfFreedom.U1" />, <see cref="eDegreeOfFreedom.U2" /> or <see cref="eDegreeOfFreedom.U3" /> and [rad] when <paramref name="degreeOfFreedom" /> = <see cref="eDegreeOfFreedom.R1" />, <see cref="eDegreeOfFreedom.R2" /> or <see cref="eDegreeOfFreedom.R3" />.
        /// This item applies only when displacement control is used, that is, <paramref name="loadControl" /> = <see cref="eLoadControl.DisplacementControl" />.</param>
        /// <param name="monitoredDisplacementType">Type of monitored displacement.</param>
        /// <param name="degreeOfFreedom">The degree of freedom for which the displacement at a point object is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" /></param>
        /// <param name="namePoint">The name of the point object at which the displacement is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" />.</param>
        /// <param name="nameGeneralizedDisplacement">The name of the generalized displacement for which the displacement is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.GeneralizedDisplacement" /></param>
        void GetLoadApplication(string name,
            ref eLoadControl loadControl,
            ref eLoadControlDisplacement controlDisplacementType,
            ref double targetDisplacement,
            ref eMonitoredDisplacementType monitoredDisplacementType,
            ref eDegreeOfFreedom degreeOfFreedom,
            ref string namePoint,
            ref string nameGeneralizedDisplacement);

        /// <summary>
        /// This function sets the load application control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="loadControl">The load application control method.</param>
        /// <param name="controlDisplacementType">Type of control displacement.</param>
        /// <param name="targetDisplacement">The structure is loaded to a monitored displacement of this magnitude.
        /// [L] when <paramref name="degreeOfFreedom" /> = <see cref="eDegreeOfFreedom.U1" />, <see cref="eDegreeOfFreedom.U2" /> or <see cref="eDegreeOfFreedom.U3" /> and [rad] when <paramref name="degreeOfFreedom" /> = <see cref="eDegreeOfFreedom.R1" />, <see cref="eDegreeOfFreedom.R2" /> or <see cref="eDegreeOfFreedom.R3" />.
        /// This item applies only when displacement control is used, that is, <paramref name="loadControl" /> = <see cref="eLoadControl.DisplacementControl" />.</param>
        /// <param name="monitoredDisplacementType">Type of monitored displacement.</param>
        /// <param name="degreeOfFreedom">The degree of freedom for which the displacement at a point object is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" /></param>
        /// <param name="namePoint">The name of the point object at which the displacement is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" />.</param>
        /// <param name="nameGeneralizedDisplacement">The name of the generalized displacement for which the displacement is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.GeneralizedDisplacement" /></param>
        void SetLoadApplication(string name,
            eLoadControl loadControl,
            eLoadControlDisplacement controlDisplacementType,
            double targetDisplacement,
            eMonitoredDisplacementType monitoredDisplacementType,
            eDegreeOfFreedom degreeOfFreedom,
            string namePoint,
            string nameGeneralizedDisplacement);
    }
}
