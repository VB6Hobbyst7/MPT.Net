// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="StaticLinear.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;
using MPT.Enums;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the static linear load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IStaticLinear" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class StaticLinear : CSiApiBase, IStaticLinear
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticLinear" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public StaticLinear(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Interface
        /// <summary>
        /// This function initializes a load case.
        /// If this function is called for an existing load case, all items for the case are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new load case.
        /// If this is an existing case, that case is modified; otherwise, a new case is added.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCase(string name)
        {
            _callCode = _sapModel.LoadCases.StaticLinear.SetCase(name);
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
            _callCode = _sapModel.LoadCases.StaticLinear.GetInitialCase(name, ref initialCase);
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
            _callCode = _sapModel.LoadCases.StaticLinear.SetInitialCase(name, initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the load data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing steady state load case.</param>
        /// <param name="loadTypes">Either <see cref="eLoadType.Load" /> or <see cref="eLoadType.Accel" />, indicating the type of each load assigned to the load case.</param>
        /// <param name="loadNames">The name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />, this item is U1, U2, U3, R1, R2 or R3, indicating the direction of the load.</param>
        /// <param name="scaleFactor">The scale factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoads(string name,
            ref eLoadType[] loadTypes,
            ref string[] loadNames,
            ref double[] scaleFactor)
        {
            string[] csiLoadTypes = new string[0];

            _callCode = _sapModel.LoadCases.StaticLinear.GetLoads(name, ref _numberOfItems, ref csiLoadTypes, ref loadNames, ref scaleFactor);
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
        /// <param name="name">The name of an existing steady state load case.</param>
        /// <param name="loadTypes">Either <see cref="eLoadType.Load" /> or <see cref="eLoadType.Accel" />, indicating the type of each load assigned to the load case.</param>
        /// <param name="loadNames">The name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />, this item is U1, U2, U3, R1, R2 or R3, indicating the direction of the load.</param>
        /// <param name="scaleFactor">The scale factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoads(string name,
            eLoadType[] loadTypes,
            string[] loadNames,
            double[] scaleFactor)
        {
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(loadNames), loadNames.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(scaleFactor), scaleFactor.Length);

            string[] csiLoadTypes = new string[loadTypes.Length - 1];
            for (int i = 0; i < loadTypes.Length; i++)
            {
                csiLoadTypes[i] = loadTypes[i].ToString();
            }

            _callCode = _sapModel.LoadCases.StaticLinear.SetLoads(name, loadTypes.Length, ref csiLoadTypes, ref loadNames, ref scaleFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
