// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="TendonSection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Represents the tendon properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.ITendonSection" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class TendonSection : CSiApiBase, ITendonSection
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="TendonSection" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public TendonSection(CSiApiSeed seed) : base(seed) { }

        #endregion

        #region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing tendon property.
        /// </summary>
        /// <param name="currentName">The existing name of a defined tendon property.</param>
        /// <param name="newName">The new name for the tendon property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, 
            string newName)
        {
            _callCode = _sapModel.PropTendon.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns the total number of defined tendon properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.PropTendon.Count();
        }

        /// <summary>
        /// The function deletes a specified tendon property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing tendon property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.PropTendon.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined tendon properties.
        /// </summary>
        /// <param name="names">Tendon property names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PropTendon.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion

        #region Methods: Public

        // === Get/Set ===
        /// <summary>
        /// This function retrieves tendon property definition data.
        /// </summary>
        /// <param name="name">The name of an existing tendon property.</param>
        /// <param name="nameMaterial">The name of the material property assigned to the tendon property.</param>
        /// <param name="modelingOption">Indicates the tendon modeling option.</param>
        /// <param name="area">The cross-sectional area of the tendon. [L^2]</param>
        /// <param name="color">The display color assigned to the property.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetProperty(string name,
            out string nameMaterial,
            out eTendonModelingOption modelingOption,
            out double area,
            out int color,
            out string notes,
            out string GUID)
        {
            int csiModelingOption = 0;
            nameMaterial = string.Empty;
            area = 0;
            color = -1;
            notes = string.Empty;
            GUID = string.Empty;

            _callCode = _sapModel.PropTendon.GetProp(name, 
                ref nameMaterial, 
                ref csiModelingOption, 
                ref area, 
                ref color, 
                ref notes, 
                ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            modelingOption = (eTendonModelingOption)csiModelingOption;
        }

        /// <summary>
        /// This function defines a tendon property.
        /// </summary>
        /// <param name="name">The name of an existing or new tendon property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property assigned to the tendon property.</param>
        /// <param name="modelingOption">Indicates the tendon modeling option.</param>
        /// <param name="area">The cross-sectional area of the cable. [L^2]</param>
        /// <param name="color">The display color assigned to the property.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetProperty(string name,
            string nameMaterial,
            eTendonModelingOption modelingOption,
            double area,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropTendon.SetProp(name, nameMaterial, (int)modelingOption, area, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion
    }
}
