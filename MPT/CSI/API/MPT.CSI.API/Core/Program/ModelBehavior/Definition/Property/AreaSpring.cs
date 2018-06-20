// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="AreaSpring.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Represents an area spring object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.IAreaSpring" />
    public class AreaSpring : CSiApiBase, IAreaSpring
    {
#region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaSpring"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AreaSpring(CSiApiSeed seed) : base(seed)
        {

        }
#endregion

#region Methods: Interface
        /// <summary>
        /// Changes the name of a defined area spring property.
        /// </summary>
        /// <param name="nameExisting">Existing name of a defined area spring property.</param>
        /// <param name="nameNew">New name for the area spring property.</param>
        public void ChangeName(string nameExisting,
            string nameNew)
        {
            _callCode = _sapModel.PropAreaSpring.ChangeName(nameExisting, nameNew);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Deletes the specified area spring property.
        /// </summary>
        /// <param name="name">The name of an existing area spring property.</param>
        public void Delete(string name)
        {
            _callCode = _sapModel.PropAreaSpring.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the names of all defined area spring property.
        /// </summary>
        /// <param name="names">The area spring property names retrieved by the program.</param>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PropAreaSpring.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves an existing named area spring property.
        /// </summary>
        /// <param name="name">The name of the area spring property.</param>
        /// <param name="U1">The spring stiffness per unit area in the local 1 direction. [F/L^3].</param>
        /// <param name="U2">The spring stiffness per unit area in the local 2 direction. [F/L^3].</param>
        /// <param name="U3">The spring stiffness per unit area in the local 3 direction. [F/L^3].</param>
        /// <param name="nonlinearOption3">The nonlinear option for the local 3 direction.</param>
        /// <param name="springOption">This argument is for a future release, however a placeholder of type Integer is required.</param>
        /// <param name="soilProfile">This argument is for a future release, however a placeholder of type String is required.</param>
        /// <param name="endLengthRatio">This argument is for a future release, however a placeholder of type Double is required.</param>
        /// <param name="period">This argument is for a future release, however a placeholder of type Double is required.</param>
        /// <param name="color">The display color for the property specified as an integer.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        public void GetAreaSpringProperties(string name,
            ref double U1,
            ref double U2,
            ref double U3,
            ref eLinkDirection nonlinearOption3,
            ref int springOption,
            ref string soilProfile,
            ref double endLengthRatio,
            ref double period,
            ref int color,
            ref string notes,
            ref string GUID)
        {
            int csiNonlinearOption3 = 0;

            _callCode = _sapModel.PropAreaSpring.GetAreaSpringProp(name,
                            ref U1, ref U2, ref U3,
                            ref csiNonlinearOption3,
                            ref springOption, ref soilProfile, ref endLengthRatio, ref period,
                            ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            nonlinearOption3 = (eLinkDirection)csiNonlinearOption3;
        }

        /// <summary>
        /// Creates a new named area spring property, or modifies an existing named area spring property.
        /// </summary>
        /// <param name="name">The name of the area spring property.</param>
        /// <param name="U1">The spring stiffness per unit area in the local 1 direction. [F/L^3].</param>
        /// <param name="U2">The spring stiffness per unit area in the local 2 direction. [F/L^3].</param>
        /// <param name="U3">The spring stiffness per unit area in the local 3 direction. [F/L^3].</param>
        /// <param name="nonlinearOption3">The nonlinear option for the local 3 direction.</param>
        /// <param name="springOption">This argument is for a future release, however a placeholder of type Integer is required.</param>
        /// <param name="soilProfile">This argument is for a future release, however a placeholder of type String is required.</param>
        /// <param name="endLengthRatio">This argument is for a future release, however a placeholder of type Double is required.</param>
        /// <param name="period">This argument is for a future release, however a placeholder of type Double is required.</param>
        /// <param name="color">The display color for the property specified as an integer.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        public void SetAreaSpringProperties(string name,
            double U1,
            double U2,
            double U3,
            eLinkDirection nonlinearOption3,
            int springOption = 1,
            string soilProfile = "",
            double endLengthRatio = 0,
            double period = 0,
            int color = 0,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropAreaSpring.SetAreaSpringProp(name,
                            U1, U2, U3,
                            (int)nonlinearOption3,
                            springOption, soilProfile, endLengthRatio, period,
                            color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif