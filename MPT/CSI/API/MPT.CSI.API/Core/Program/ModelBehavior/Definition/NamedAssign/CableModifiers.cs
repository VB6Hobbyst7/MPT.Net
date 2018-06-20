// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="CableModifiers.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.NamedAssign
{
    /// <summary>
    /// Represents the named set cable modifiers in the application.
    /// A named set of property modifiers can be applied to a frame or area object during staged construction to change the property modifiers that were previously assigned or applied to the object. 
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.NamedAssign.ICableModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class CableModifiers : CSiApiBase, ICableModifiers
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="CableModifiers" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public CableModifiers(CSiApiSeed seed) : base(seed) { }

        #endregion

        #region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing cable property modifier.
        /// </summary>
        /// <param name="currentName">The existing name of a defined cable property modifier.</param>
        /// <param name="newName">The new name for the cable property modifier.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, 
            string newName)
        {
            _callCode = _sapModel.NamedAssign.ModifierCable.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns the total number of defined cable property modifiers in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.NamedAssign.ModifierCable.Count();
        }

        /// <summary>
        /// The function deletes a specified cable property modifier.
        /// It returns an error if the specified property modifier can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing cable property modifier.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.NamedAssign.ModifierCable.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined cable property modifiers.
        /// </summary>
        /// <param name="names">Cable property modifier names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.NamedAssign.ModifierCable.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // === Get/Set
        /// <summary>
        /// This function retrieves the modifier assignment for cables.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing cable.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetModifiers(string name, 
            out CableModifier modifiers)
        {
            double[] csiModifiers = new double[0];

            _callCode = _sapModel.NamedAssign.ModifierCable.GetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            modifiers = new CableModifier();
            modifiers.FromArray(csiModifiers);
        }

        /// <summary>
        /// This function defines the modifier assignment for cables.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing cables.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetModifiers(string name, 
            CableModifier modifiers)
        {
            if (modifiers == null) { return; }
            double[] csiModifiers = modifiers.ToArray();

            _callCode = _sapModel.NamedAssign.ModifierCable.SetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif