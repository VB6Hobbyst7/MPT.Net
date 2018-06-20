// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="SolidProperties.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Represents the solid properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.ISolidProperties" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class SolidProperties : CSiApiBase, ISolidProperties
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidProperties" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public SolidProperties(CSiApiSeed seed) : base(seed) { }

        #endregion

        #region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing solid property.
        /// </summary>
        /// <param name="currentName">The existing name of a defined solid property.</param>
        /// <param name="newName">The new name for the solid property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, 
            string newName)
        {
            _callCode = _sapModel.PropSolid.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns the total number of defined solid properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.PropSolid.Count();
        }

        /// <summary>
        /// The function deletes a specified solid property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing solid property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.PropSolid.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined solid properties.
        /// </summary>
        /// <param name="names">Solid property names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PropSolid.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion

        #region Methods: Public

        // === Get/Set ===
        /// <summary>
        /// This function retrieves solid property definition data.
        /// </summary>
        /// <param name="name">The name of an existing solid property.</param>
        /// <param name="nameMaterial">The name of the material property assigned to the solid property.</param>
        /// <param name="materialAngle">Material angles.</param>
        /// <param name="includeIncompatibleBendingModes">True: Incompatible bending modes are included in the stiffness formulation.
        /// In general, incompatible modes significantly improve the bending behavior of the object.</param>
        /// <param name="color">The display color assigned to the property.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetProperty(string name,
            out string nameMaterial,
            out AngleLocalAxes materialAngle,
            out bool includeIncompatibleBendingModes,
            out int color,
            out string notes,
            out string GUID)
        {
            double csiAngleA = 0;
            double csiAngleB = 0;
            double csiAngleC = 0;
            nameMaterial = string.Empty;
            materialAngle = new AngleLocalAxes();
            includeIncompatibleBendingModes = false;
            color = -1;
            notes = string.Empty;
            GUID = string.Empty;

            _callCode = _sapModel.PropSolid.GetProp(name, 
                ref nameMaterial, 
                ref csiAngleA, 
                ref csiAngleB, 
                ref csiAngleC, 
                ref includeIncompatibleBendingModes, 
                ref color,
                ref notes, 
                ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            materialAngle.AngleA = csiAngleA;
            materialAngle.AngleB = csiAngleB;
            materialAngle.AngleC = csiAngleC;
        }

        /// <summary>
        /// This function defines a solid property.
        /// </summary>
        /// <param name="name">The name of an existing or new solid property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property assigned to the solid property.</param>
        /// <param name="materialAngle">Material angles.</param>
        /// <param name="includeIncompatibleBendingModes">True: Incompatible bending modes are included in the stiffness formulation.
        /// In general, incompatible modes significantly improve the bending behavior of the object.]</param>
        /// <param name="color">The display color assigned to the property.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetProperty(string name,
            string nameMaterial,
            AngleLocalAxes materialAngle,
            bool includeIncompatibleBendingModes,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropSolid.SetProp(name, nameMaterial, materialAngle.AngleA, materialAngle.AngleB, materialAngle.AngleC, includeIncompatibleBendingModes, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif