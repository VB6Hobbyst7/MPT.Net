namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a gettable/selectable edge.
    /// </summary>
    public interface ISelectableEdge
    {
        /// <summary>
        /// This function retrieves the selected status for area object edges.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="areEdgesSelected">True: The specified area object edge is selected;
        /// Selected(0) = Selected status for edge 1;
        /// Selected(1) = Selected status for edge 2;
        /// Selected(n) = Selected status for edge(n + 1)</param>
        void GetSelectedEdge(string name,
            ref bool[] areEdgesSelected);

        /// <summary>
        /// This function sets the selected status for area object edges.
        /// </summary>
        /// <param name="name">The name of an existing area object</param>
        /// <param name="numberOfEdges">The area object edge that is to have its selected status set.</param>
        /// <param name="isEdgeSelected">True: The specified area object edge is selected.</param>
        void SetSelectedEdge(string name,
            int numberOfEdges,
            bool isEdgeSelected);
    }
}