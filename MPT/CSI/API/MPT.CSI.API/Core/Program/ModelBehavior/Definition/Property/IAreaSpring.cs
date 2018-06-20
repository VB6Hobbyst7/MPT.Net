// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-05-2017
// ***********************************************************************
// <copyright file="IAreaSpring.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Object has a gettable/settable area spring property.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IAreaSpring: IDeletable, IChangeableName, IListableNames
    {
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
        void GetAreaSpringProperties(string name,
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
            ref string GUID);

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
        void SetAreaSpringProperties(string name,
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
            string GUID = "");
    }
}
#endif