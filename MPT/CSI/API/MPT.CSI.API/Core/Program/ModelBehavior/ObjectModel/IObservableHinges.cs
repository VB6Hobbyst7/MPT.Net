namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has gettable hinge assignments.
    /// </summary>
    public interface IObservableHinges
    {
        /// <summary>
        /// This function reports the hinge assignments for a specified frame object.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="hingeNumbers">The hinge number for each hinge on the frame object.</param>
        /// <param name="generatedPropertyNames">The name of the generated hinge property for each hinge on the frame object.</param>
        /// <param name="hingeTypes">Type of hinge for each hinge on the frame object.</param>
        /// <param name="hingeBehaviors">The behavior of the hinge for each hinge on the frame object.</param>
        /// <param name="sources">The source of the generated hinge property for each hinge on the frame object. 
        /// The source is either Auto or the name of a defined (not generated) hinge property.</param>
        /// <param name="relativeDistances">The relative distance of each hinge along the frame object.</param>
        void GetHingeAssigns(string name,
            ref int[] hingeNumbers,
            ref string[] generatedPropertyNames,
            ref eHingeType[] hingeTypes,
            ref eHingeBehavior[] hingeBehaviors,
            ref string[] sources,
            ref double[] relativeDistances);
    }
}