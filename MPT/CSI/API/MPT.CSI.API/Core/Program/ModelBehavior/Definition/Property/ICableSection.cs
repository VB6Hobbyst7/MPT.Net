namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Implements the cable properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableCableModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableCableModifiers" />
    public interface ICableSection : 
        IChangeableName, ICountable, IDeletable, IListableNames,
        IObservableCableModifiers, IChangeableCableModifiers
    {
        /// <summary>
        /// This function retrieves cable property definition data.
        /// </summary>
        /// <param name="name">The name of an existing cable property.</param>
        /// <param name="nameMaterial">The name of the material property assigned to the cable property.</param>
        /// <param name="area">The cross-sectional area of the cable. [L^2]</param>
        /// <param name="color">The display color assigned to the property.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetProperty(string name,
            out string nameMaterial,
            out double area,
            out int color,
            out string notes,
            out string GUID);

        /// <summary>
        /// This function defines a cable property.
        /// </summary>
        /// <param name="name">The name of an existing or new cable property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property assigned to the cable property.</param>
        /// <param name="area">The cross-sectional area of the cable. [L^2]</param>
        /// <param name="color">The display color assigned to the property.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetProperty(string name,
            string nameMaterial,
            double area,
            int color = -1,
            string notes = "",
            string GUID = "");
    }
}