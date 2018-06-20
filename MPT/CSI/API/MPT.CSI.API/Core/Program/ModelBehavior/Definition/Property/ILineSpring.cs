#if BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Object has a gettable/settable line spring property.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface ILineSpring : IDeletable, IChangeableName, IListableNames
    {
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
        void GetLineSpringProperties(string name,
            ref double U1,
            ref double U2,
            ref double U3,
            ref double R1,
            ref eLinkDirection nonlinearOption2,
            ref eLinkDirection nonlinearOption3,
            ref int color,
            ref string notes,
            ref string GUID);

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
        void SetLineSpringProperties(string name,
            double U1,
            double U2,
            double U3,
            double R1,
            eLinkDirection nonlinearOption2,
            eLinkDirection nonlinearOption3,
            int color = 0,
            string notes = "",
            string GUID = "");
    }
}
#endif