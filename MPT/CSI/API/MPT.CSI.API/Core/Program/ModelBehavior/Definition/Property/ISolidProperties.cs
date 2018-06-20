#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Implements the solid properties in the application.
    /// </summary>
    public interface ISolidProperties: IChangeableName, ICountable, IDeletable, IListableNames
    {
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
        void GetProperty(string name,
            out string nameMaterial,
            out AngleLocalAxes materialAngle,
            out bool includeIncompatibleBendingModes,
            out int color,
            out string notes,
            out string GUID);

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
        void SetProperty(string name,
            string nameMaterial,
            AngleLocalAxes materialAngle,
            bool includeIncompatibleBendingModes,
            int color = -1,
            string notes = "",
            string GUID = "");
    }
}
#endif