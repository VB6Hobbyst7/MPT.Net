// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="ModalRitz.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;
using MPT.Enums;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the modal Ritz load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IModalRitz" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class ModalRitz : CSiApiBase, IModalRitz
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="ModalRitz" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public ModalRitz(CSiApiSeed seed) : base(seed) { }


        #endregion
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
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
            _callCode = _sapModel.LoadCases.ModalRitz.SetCase(name);
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
            _callCode = _sapModel.LoadCases.ModalRitz.GetInitialCase(name, ref initialCase);
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
            _callCode = _sapModel.LoadCases.ModalRitz.SetInitialCase(name, initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }








        /// <summary>
        /// This function retrieves the number of modes requested for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing modal load case.</param>
        /// <param name="maxNumberModes">The maximum number of modes requested.</param>
        /// <param name="minNumberModes">The minimum number of modes requested.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNumberModes(string name,
            ref int maxNumberModes,
            ref int minNumberModes)
        {
            _callCode = _sapModel.LoadCases.ModalRitz.GetNumberModes(name, ref maxNumberModes, ref minNumberModes);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the number of modes requested for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing modal load case.</param>
        /// <param name="maxNumberModes">The maximum number of modes requested.</param>
        /// <param name="minNumberModes">The minimum number of modes requested.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetNumberModes(string name,
            int maxNumberModes,
            int minNumberModes)
        {
            _callCode = _sapModel.LoadCases.ModalRitz.SetNumberModes(name, maxNumberModes, minNumberModes);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the load data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing modal Ritz load case.</param>
        /// <param name="loadTypes">The load types.</param>
        /// <param name="loadNames">This is an array that includes the name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Accel" />, this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Link" />, this item is not used.</param>
        /// <param name="maxNumberGenerationCycles">The maximum number generation cycles to be performed for the specified Ritz starting vector.
        /// A value of 0 means there is no limit on the number of cycles.</param>
        /// <param name="targetDynamicParticipationRatio">The target dynamic participation ratio.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoads(string name,
            ref eLoadTypeModal[] loadTypes,
            ref string[] loadNames,
            ref int[] maxNumberGenerationCycles,
            ref double[] targetDynamicParticipationRatio)
        {
            string[] csiLoadTypes = new string[0];

            _callCode = _sapModel.LoadCases.ModalRitz.GetLoads(name, ref _numberOfItems, ref csiLoadTypes, ref loadNames, ref maxNumberGenerationCycles, ref targetDynamicParticipationRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadTypes = new eLoadTypeModal[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                loadTypes[i] = EnumLibrary.ConvertStringToEnumByDescription<eLoadTypeModal>(csiLoadTypes[i]);
            }
        }

        /// <summary>
        /// This function sets the load data for the specified analysis case.
        /// </summary>
        /// <param name="name">The name of an existing modal Ritz load case.</param>
        /// <param name="loadTypes">The load types.</param>
        /// <param name="loadNames">This is an array that includes the name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Accel" />, this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Link" />, this item is not used.</param>
        /// <param name="maxNumberGenerationCycles">The maximum number generation cycles to be performed for the specified Ritz starting vector.
        /// A value of 0 means there is no limit on the number of cycles.</param>
        /// <param name="targetDynamicParticipationRatio">The target dynamic participation ratio.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoads(string name,
            eLoadTypeModal[] loadTypes,
            string[] loadNames,
            int[] maxNumberGenerationCycles,
            double[] targetDynamicParticipationRatio)
        {
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(loadNames), loadNames.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(maxNumberGenerationCycles), maxNumberGenerationCycles.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(targetDynamicParticipationRatio), targetDynamicParticipationRatio.Length);

            string[] csiLoadTypes = new string[loadTypes.Length - 1];
            for (int i = 0; i < loadTypes.Length; i++)
            {
                csiLoadTypes[i] = loadTypes[i].ToString();
            }

            _callCode = _sapModel.LoadCases.ModalRitz.SetLoads(name, loadTypes.Length, ref csiLoadTypes, ref loadNames, ref maxNumberGenerationCycles, ref targetDynamicParticipationRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
#endif
    }
}
