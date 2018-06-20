// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-19-2017
// ***********************************************************************
// <copyright file="Hyperstatic.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the hyperstatic load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IHyperstatic" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Hyperstatic : CSiApiBase, IHyperstatic
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Hyperstatic" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Hyperstatic(CSiApiSeed seed) : base(seed) { }


#endregion

#region Methods: Public        
        /// <summary>
        /// This function retrieves the base case for the specified hyperstatic load case.
        /// </summary>
        /// <param name="name">The name of an existing hyperstatic load case.</param>
        /// <param name="nameBaseCase">The name of an existing static linear load case that is the base case for the specified hyperstatic load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetBaseCase(string name,
            ref string nameBaseCase)
        {
            _callCode = _sapModel.LoadCases.HyperStatic.GetBaseCase(name, ref nameBaseCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the base case for the specified hyperstatic load case.
        /// </summary>
        /// <param name="name">The name of an existing hyperstatic load case.</param>
        /// <param name="nameBaseCase">The name of an existing static linear load case that is the base case for the specified hyperstatic load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetBaseCase(string name,
            string nameBaseCase)
        {
            _callCode = _sapModel.LoadCases.HyperStatic.SetBaseCase(name, nameBaseCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes a hyperstatic load case.
        /// If this function is called for an existing load case, all items for the case are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new load case.
        /// If this is an existing case, that case is modified; otherwise, a new case is added.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCase(string name)
        {
            _callCode = _sapModel.LoadCases.HyperStatic.SetCase(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#endregion
    }
}
#endif