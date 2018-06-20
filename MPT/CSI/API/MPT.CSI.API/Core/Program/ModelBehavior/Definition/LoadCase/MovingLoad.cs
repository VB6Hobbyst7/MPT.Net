// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="MovingLoad.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the moving load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IMovingLoad" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class MovingLoad : CSiApiBase, IMovingLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="MovingLoad" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public MovingLoad(CSiApiSeed seed) : base(seed) { }


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
            _callCode = _sapModel.LoadCases.Moving.SetCase(name);
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
            _callCode = _sapModel.LoadCases.Moving.GetInitialCase(name, ref initialCase);
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
            _callCode = _sapModel.LoadCases.Moving.SetInitialCase(name, initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion

        #region Methods: Public        
        /// <summary>
        /// This function retrieves the load data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing moving load case.</param>
        /// <param name="vehicleClass">The vehicle class for each load assigned to the load case.</param>
        /// <param name="scaleFactor">The scale factor for each load assigned to the load case.</param>
        /// <param name="minLanesLoaded">The minimum number of lanes loaded for each load assigned to the load case.</param>
        /// <param name="maxLanesLoaded">The maximum number of lanes loaded for each load assigned to the load case.<para />
        /// This item must be 0, or it must be greater than or equal to <paramref name="minLanesLoaded" />.<para />
        /// If this item is 0, all available lanes are loaded.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoads(string name,
            ref string[] vehicleClass,
            ref double[] scaleFactor,
            ref double[] minLanesLoaded,
            ref double[] maxLanesLoaded)
        {
            _callCode = _sapModel.LoadCases.Moving.GetLoads(name, ref _numberOfItems, ref vehicleClass, ref scaleFactor, ref minLanesLoaded, ref maxLanesLoaded);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the load data for the specified analysis case.
        /// </summary>
        /// <param name="name">The name of an existing moving load case.</param>
        /// <param name="vehicleClass">The vehicle class for each load assigned to the load case.</param>
        /// <param name="scaleFactor">The scale factor for each load assigned to the load case.</param>
        /// <param name="minLanesLoaded">The minimum number of lanes loaded for each load assigned to the load case.</param>
        /// <param name="maxLanesLoaded">The maximum number of lanes loaded for each load assigned to the load case.<para />
        /// This item must be 0, or it must be greater than or equal to <paramref name="minLanesLoaded" />.<para />
        /// If this item is 0, all available lanes are loaded.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoads(string name,
            string[] vehicleClass,
            double[] scaleFactor,
            double[] minLanesLoaded,
            double[] maxLanesLoaded)
        {
            arraysLengthMatch(nameof(vehicleClass), vehicleClass.Length, nameof(scaleFactor), scaleFactor.Length);
            arraysLengthMatch(nameof(vehicleClass), vehicleClass.Length, nameof(minLanesLoaded), minLanesLoaded.Length);
            arraysLengthMatch(nameof(vehicleClass), vehicleClass.Length, nameof(maxLanesLoaded), maxLanesLoaded.Length);

            _callCode = _sapModel.LoadCases.Moving.SetLoads(name, vehicleClass.Length, ref vehicleClass, ref scaleFactor, ref minLanesLoaded, ref maxLanesLoaded);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the lanes loaded data for a specified load assignment number in a specified load case.
        /// </summary>
        /// <param name="name">The name of an existing moving load case.</param>
        /// <param name="loadNumber">The load assignment number.</param>
        /// <param name="nameLanes">The name of each lane loaded for the specified load assignment number.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLanesLoaded(string name,
            int loadNumber,
            ref string[] nameLanes)
        {
            _callCode = _sapModel.LoadCases.Moving.GetLanesLoaded(name, loadNumber, ref _numberOfItems, ref nameLanes);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the lanes loaded data for a specified load assignment number in a specified load case.
        /// </summary>
        /// <param name="name">The name of an existing moving load case.</param>
        /// <param name="loadNumber">The load assignment number.</param>
        /// <param name="nameLanes">The name of each lane loaded for the specified load assignment number.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLanesLoaded(string name,
            int loadNumber,
            string[] nameLanes)
        {
            _callCode = _sapModel.LoadCases.Moving.SetLanesLoaded(name, loadNumber, nameLanes.Length, ref nameLanes);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// This function retrieves the directional factors for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing moving load case.</param>
        /// <param name="verticalLoad">The moving load directional factor for vertical load.</param>
        /// <param name="brakingLoad">The moving load directional factor for loads in-line with the road.</param>
        /// <param name="centrifugalLoad">The moving directional factor for centrifugal load perpendicular with the road.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDirectionalFactors(string name,
            ref double verticalLoad,
            ref double brakingLoad,
            ref double centrifugalLoad)
        {
            _callCode = _sapModel.LoadCases.Moving.GetDirectionalFactors(name, ref verticalLoad, ref brakingLoad, ref centrifugalLoad);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the directional factors for the specified load case.
        /// Calling this function is optional.
        /// By default, the directional factors are set to Vertical = 1, Braking = 0, and Centrifugal = 0 when the moving load case is defined or re-defined.
        /// If this function is called, the three directional factors must be nonnegative, and at least one must be positive.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing moving load case.</param>
        /// <param name="verticalLoad">The moving load directional factor for vertical load.</param>
        /// <param name="brakingLoad">The moving load directional factor for loads in-line with the road.</param>
        /// <param name="centrifugalLoad">The moving directional factor for centrifugal load perpendicular with the road.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDirectionalFactors(string name,
            double verticalLoad,
            double brakingLoad,
            double centrifugalLoad)
        {
            _callCode = _sapModel.LoadCases.Moving.SetDirectionalFactors(name, verticalLoad, brakingLoad, centrifugalLoad);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the multilane scale factor data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing moving load case.</param>
        /// <param name="scaleFactors">The reduction scale factor for each of the lanes loaded.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMultiLaneScaleFactor(string name,
            ref double[] scaleFactors)
        {
            _callCode = _sapModel.LoadCases.Moving.GetMultiLaneSF(name, ref _numberOfItems, ref scaleFactors);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the multilane scale factor data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing moving load case.</param>
        /// <param name="scaleFactors">The reduction scale factor for the number of lanes loaded.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMultiLaneScaleFactor(string name,
            double[] scaleFactors)
        {
            _callCode = _sapModel.LoadCases.Moving.SetMultiLaneSF(name, scaleFactors.Length, ref scaleFactors);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif
