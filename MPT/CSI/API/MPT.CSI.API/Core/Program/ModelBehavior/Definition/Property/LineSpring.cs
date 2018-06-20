// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="LineSpring.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Represents a line spring object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.ILineSpring" />
    public class LineSpring : CSiApiBase, ILineSpring
    {
#region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSpring"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public LineSpring(CSiApiSeed seed) : base(seed)
        {

        }
#endregion

#region Methods: Interface
        /// <summary>
        /// Changes the name of a defined line spring property.
        /// </summary>
        /// <param name="nameExisting">Existing name of a defined line spring property.</param>
        /// <param name="nameNew">New name for the line spring property.</param>
        public void ChangeName(string nameExisting,
            string nameNew)
        {
            _callCode = _sapModel.PropLineSpring.ChangeName(nameExisting, nameNew);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Deletes the specified line spring property.
        /// </summary>
        /// <param name="name">The name of an existing line spring property.</param>
        public void Delete(string name)
        {
            _callCode = _sapModel.PropLineSpring.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the names of all defined line spring property.
        /// </summary>
        /// <param name="names">The line spring property names retrieved by the program.</param>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PropLineSpring.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves an existing named line spring property.
        /// </summary>
        /// <param name="name">The name of the area spring property.</param>
        /// <param name="U1">The spring stiffness per unit area in the local 1 direction. [F/L^3].</param>
        /// <param name="U2">The spring stiffness per unit area in the local 2 direction. [F/L^3].</param>
        /// <param name="U3">The spring stiffness per unit area in the local 3 direction. [F/L^3].</param>
        /// <param name="R1">The rotational spring stiffness about local 1. [F/rad].</param>
        /// <param name="nonlinearOption2">The nonlinear option for the local 2 direction.</param>
        /// <param name="nonlinearOption3">The nonlinear option for the local 3 direction.</param>
        /// <param name="color">The display color for the property specified as an integer.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        public void GetLineSpringProperties(string name,
            ref double U1,
            ref double U2,
            ref double U3,
            ref double R1,
            ref eLinkDirection nonlinearOption2,
            ref eLinkDirection nonlinearOption3,
            ref int color,
            ref string notes,
            ref string GUID)
        {
            int csiNonlinearOption2 = 0;
            int csiNonlinearOption3 = 0;

            _callCode = _sapModel.PropLineSpring.GetLineSpringProp(name,
                            ref U1, ref U2, ref U3, ref R1,
                            ref csiNonlinearOption2, ref csiNonlinearOption3,
                            ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            nonlinearOption2 = (eLinkDirection)csiNonlinearOption2;
            nonlinearOption3 = (eLinkDirection)csiNonlinearOption3;
        }

        /// <summary>
        /// Creates a new named line spring property, or modifies an existing named line spring property.
        /// </summary>
        /// <param name="name">The name of the line spring property.</param>
        /// <param name="U1">The spring stiffness per unit area in the local 1 direction. [F/L^3].</param>
        /// <param name="U2">The spring stiffness per unit area in the local 2 direction. [F/L^3].</param>
        /// <param name="U3">The spring stiffness per unit area in the local 3 direction. [F/L^3].</param>
        /// <param name="R1">The rotational spring stiffness about local 1. [F/rad].</param>
        /// <param name="nonlinearOption2">The nonlinear option for the local 2 direction.</param>
        /// <param name="nonlinearOption3">The nonlinear option for the local 3 direction.</param>
        /// <param name="color">The display color for the property specified as an integer.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        public void SetLineSpringProperties(string name,
            double U1,
            double U2,
            double U3,
            double R1,
            eLinkDirection nonlinearOption2,
            eLinkDirection nonlinearOption3,
            int color = 0,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropLineSpring.SetLineSpringProp(name,
                            U1, U2, U3, R1,
                            (int)nonlinearOption2, (int)nonlinearOption3,
                            color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion

    }
}
#endif