namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can return the corresponding frame element attributes created for analysis.
    /// </summary>
    public interface IObservableElementFrame
    {

        /// <summary>
        /// This function retrieves the name of the element (analysis model) associated with a specified object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="elementNames">The name of each element created from the specified object.</param>
        /// <param name="relativeDistanceI">The relative distance along the frame object to the I-End of the element.</param>
        /// <param name="relativeDistanceJ">The relative distance along the frame object to the J-End of the element.</param>
        void GetElement(string name,
            out string[] elementNames,
            out double[] relativeDistanceI,
            out double[] relativeDistanceJ);
    }
}