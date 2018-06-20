#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements the section cuts in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface ISectionCuts: IListableNames
    {
        #region Methods: Public

        /// <summary>
        /// This function adds a new quadrilateral to a section cut defined by quadrilaterals.
        /// </summary>
        /// <param name="nameQuad">The name of an existing quadrilateral section cut.</param>
        /// <param name="xCoordinates">This is an array of four X coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="yCoordinates">This is an array of four Y coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="zCoordinates">This is an array of four Z coordinates, one for each of the four points defining the quadrilateral.</param>
        void AddQuadrilateral(string nameQuad,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] zCoordinates);

        // === Get

        /// <summary>
        /// This function gets basic information about an existing section cut.
        /// </summary>
        /// <param name="nameSectionCut">Name of an existing section cut.</param>
        /// <param name="groupName">The name of the group associated with the section cut.</param>
        /// <param name="sectionCutType">The result type of the section cut.</param>
        /// <param name="numberQuadrilaterals">The number of quadrilateral cutting planes defined for the section cut. 
        /// If this number is zero then the section cut is defined by the associated group.</param>
        void GetCutInfo(string nameSectionCut,
            ref string groupName,
            ref eSectionResultType sectionCutType,
            ref int numberQuadrilaterals);

        /// <summary>
        /// This function returns the coordinates of a quadrilateral cutting plane in a section cut defined by quadrilaterals.
        /// </summary>
        /// <param name="nameQuad">The name of an existing quadrilateral section cut.</param>
        /// <param name="quadrilateralNumber">The number of a quadirilateral cutting plane in the section cut.</param>
        /// <param name="xCoordinates">This is an array of four X coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="yCoordinates">This is an array of four Y coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="zCoordinates">This is an array of four Z coordinates, one for each of the four points defining the quadrilateral.</param>
        void GetQuadrilateral(string nameQuad,
            int quadrilateralNumber,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] zCoordinates);

        // === Set

        /// <summary>
        /// This function adds a new section cut defined by a group to the model or reinitializes an existing section cut to be defined by a group.
        /// </summary>
        /// <param name="nameSectionCut">Name of an existing section cut.</param>
        /// <param name="groupName">The name of the group associated with the section cut.</param>
        /// <param name="sectionCutType">The result type of the section cut.</param>
        void SetByGroup(string nameSectionCut,
            string groupName,
            eSectionResultType sectionCutType);

        /// <summary>
        /// This function adds a new section cut defined by a quadrilateral to the model or reinitializes an existing section cut to be defined by a quadrilateral
        /// </summary>
        /// <param name="nameSectionCut">Name of an existing section cut.</param>
        /// <param name="groupName">The name of the group associated with the section cut.</param>
        /// <param name="sectionCutType">The result type of the section cut.</param>
        /// <param name="xCoordinates">This is an array of four X coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="yCoordinates">This is an array of four Y coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="zCoordinates">This is an array of four Z coordinates, one for each of the four points defining the quadrilateral.</param>
        void SetByQuadrilateral(string nameSectionCut,
            string groupName,
            eSectionResultType sectionCutType,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] zCoordinates);

        // === Get/Set ===

        /// <summary>
        /// This function gets the advanced local axes data for an existing section cut whose result type is Analysis.
        /// </summary>
        /// <param name="nameSectionCut">The name of an existing section cut.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="axisVectorOption">Indicates the axis reference vector option. 
        /// This item applies only when <paramref name="isActive"/> is True.</param>
        /// <param name="axisCoordinateSystem">The coordinate system used to define the axis reference vector coordinate directions and the axis user vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="axisVectorOption"/> is Coordinate Direction or User Vector.</param>
        /// <param name="axisVectorDirection">Indicates the axis reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="axisVectorOption"/> is Coordinate Direction.</param>
        /// <param name="axisPoint">Indicates the labels of two joints that define the axis reference vector. 
        /// Either of these joints may be specified as None to indicate the center of the specified object.  
        /// If both joints are specified as None, they are not used to define the plane reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="axisVectorOption"/> is Two Joints.</param>
        /// <param name="axisReferenceVector">Defines the axis reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="axisVectorOption"/> is User Vector.</param>
        /// <param name="localPlaneByReferenceVector">This is 12, 13, 21, 23, 31 or 32, indicating that the local plane determined by the plane reference vector is the 1-2, 1-3, 2-1, 2-3, 3-1, or 3-2 plane. 
        /// This item applies only when <paramref name="isActive"/> is True.</param>
        /// <param name="planeVectorOption">Indicates the plane reference vector option. 
        /// This item applies only when <paramref name="isActive"/> is True.</param>
        /// <param name="planeCoordinateSystem">The coordinate system used to define the plane reference vector coordinate directions and the plane user vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="planeVectorOption"/> is Coordinate Direction or User Vector.</param>
        /// <param name="planeVectorDirection">Indicates the plane reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="planeVectorOption"/> is Coordinate Direction.</param>
        /// <param name="planePoint">Indicates the labels of two joints that define the plane reference vector. 
        /// Either of these joints may be specified as None to indicate the center of the specified object.  
        /// If both joints are specified as None, they are not used to define the plane reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="planeVectorOption"/> is Two Joints.</param>
        /// <param name="planeReferenceVector">Defines the plane reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="planeVectorOption"/> is User Vector.</param>
        void GetLocalAxesAdvancedAnalysis(string nameSectionCut,
            ref bool isActive,
            ref eReferenceVector axisVectorOption,
            ref string axisCoordinateSystem,
            ref eReferenceVectorDirection[] axisVectorDirection,
            ref string[] axisPoint,
            ref double[] axisReferenceVector,
            ref int localPlaneByReferenceVector,
            ref eReferenceVector planeVectorOption,
            ref string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector);

        /// <summary>
        /// This function sets the advanced local axes data for an existing section cut whose result type is Analysis.
        /// </summary>
        /// <param name="nameSectionCut">The name of an existing section cut.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="axisVectorOption">Indicates the axis reference vector option. 
        /// This item applies only when <paramref name="isActive"/> is True.</param>
        /// <param name="axisCoordinateSystem">The coordinate system used to define the axis reference vector coordinate directions and the axis user vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="axisVectorOption"/> is Coordinate Direction or User Vector.</param>
        /// <param name="axisVectorDirection">Indicates the axis reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="axisVectorOption"/> is Coordinate Direction.</param>
        /// <param name="axisPoint">Indicates the labels of two joints that define the axis reference vector. 
        /// Either of these joints may be specified as None to indicate the center of the specified object.  
        /// If both joints are specified as None, they are not used to define the plane reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="axisVectorOption"/> is Two Joints.</param>
        /// <param name="axisReferenceVector">Defines the axis reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="axisVectorOption"/> is User Vector.</param>
        /// <param name="localPlaneByReferenceVector">This is 12, 13, 21, 23, 31 or 32, indicating that the local plane determined by the plane reference vector is the 1-2, 1-3, 2-1, 2-3, 3-1, or 3-2 plane. 
        /// This item applies only when <paramref name="isActive"/> is True.</param>
        /// <param name="planeVectorOption">Indicates the plane reference vector option. 
        /// This item applies only when <paramref name="isActive"/> is True.</param>
        /// <param name="planeCoordinateSystem">The coordinate system used to define the plane reference vector coordinate directions and the plane user vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="planeVectorOption"/> is Coordinate Direction or User Vector.</param>
        /// <param name="planeVectorDirection">Indicates the plane reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="planeVectorOption"/> is Coordinate Direction.</param>
        /// <param name="planePoint">Indicates the labels of two joints that define the plane reference vector. 
        /// Either of these joints may be specified as None to indicate the center of the specified object.  
        /// If both joints are specified as None, they are not used to define the plane reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="planeVectorOption"/> is Two Joints.</param>
        /// <param name="planeReferenceVector">Defines the plane reference vector. 
        /// This item applies when <paramref name="isActive"/> is True and <paramref name="planeVectorOption"/> is User Vector.</param>
        void SetLocalAxesAdvancedAnalysis(string nameSectionCut,
            bool isActive,
            eReferenceVector axisVectorOption,
            string axisCoordinateSystem,
            ref eReferenceVectorDirection[] axisVectorDirection,
            ref string[] axisPoint,
            ref double[] axisReferenceVector,
            int localPlaneByReferenceVector,
            eReferenceVector planeVectorOption,
            string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector);

        // ===

        /// <summary>
        /// This function gets the local axes angles for an existing section cut whose result type is Analysis.
        /// </summary>
        /// <param name="nameSectionCut">Name of an existing section cut.</param>
        /// <param name="zRotation">Rotation about the z axis.</param>
        /// <param name="yRotation">The rotation about the Y' axis where Y' is the orientation of the Y axis after rotation about the Z axis.</param>
        /// <param name="xRotation">The rotation about the X'' axis where X'' is the orientation of the X axis after rotation about the Z axis and about the Y' axis.</param>
        /// <param name="isAdvanced">True: Advanced local axes are specified.</param>
        void GetLocalAxesAnalysis(string nameSectionCut,
            ref double zRotation,
            ref double yRotation,
            ref double xRotation,
            ref bool isAdvanced);

        /// <summary>
        /// This function sets the local axes angles for an existing section cut whose result type is Analysis.
        /// </summary>
        /// <param name="nameSectionCut">Name of an existing section cut.</param>
        /// <param name="zRotation">Rotation about the z axis.</param>
        /// <param name="yRotation">The rotation about the Y' axis where Y' is the orientation of the Y axis after rotation about the Z axis.</param>
        /// <param name="xRotation">The rotation about the X'' axis where X'' is the orientation of the X axis after rotation about the Z axis and about the Y' axis.</param>
        void SetLocalAxesAnalysis(string nameSectionCut,
            double zRotation,
            double yRotation,
            double xRotation);

        // ===

        /// <summary>
        /// This function gets the local axes angle for section cuts whose result type is Design (Wall, Spandrel or Slab).
        /// </summary>
        /// <param name="nameSectionCut">Name of an existing section cut.</param>
        /// <param name="angle">For design local axes orientation type wall this is the angle from the global X to the local 2 axis.  
        /// For orientation types spandrel and slab it is the angle from the global X to the local 1 axis.</param>
        void GetLocalAxesAngleDesign(string nameSectionCut,
            ref double angle);

        /// <summary>
        /// This function sets the local axes angle for section cuts whose result type is Design (Wall, Spandrel or Slab).
        /// </summary>
        /// <param name="nameSectionCut">Name of an existing section cut.</param>
        /// <param name="angle">For design local axes orientation type wall this is the angle from the global X to the local 2 axis.  
        /// For orientation types spandrel and slab it is the angle from the global X to the local 1 axis.</param>
        void SetLocalAxesAngleDesign(string nameSectionCut,
            double angle);

        // ===

        /// <summary>
        /// This function gets the results location for an existing section cut.
        /// </summary>
        /// <param name="nameSectionCut">The name of an existing section cut.</param>
        /// <param name="isDefault">True: The section cut results are reported at the default location.  
        /// If so, the X, Y and Z items are ignored.</param>
        /// <param name="xCoordinate">The X coordinate of the section cut result location when it is not default.</param>
        /// <param name="yCoordinate">The Y coordinate of the section cut result location when it is not default.</param>
        /// <param name="zCoordinate">The Z coordinate of the section cut result location when it is not default.</param>
        void GetResultLocation(string nameSectionCut,
            ref bool isDefault,
            ref double xCoordinate,
            ref double yCoordinate,
            ref double zCoordinate);

        /// <summary>
        /// This function sets the results location for an existing section cut.
        /// </summary>
        /// <param name="nameSectionCut">The name of an existing section cut.</param>
        /// <param name="isDefault">True: The section cut results are reported at the default location.  
        /// If so, the X, Y and Z items are ignored.</param>
        /// <param name="xCoordinate">The X coordinate of the section cut result location when <paramref name="isDefault"/> is false.</param>
        /// <param name="yCoordinate">The Y coordinate of the section cut result location when <paramref name="isDefault"/> is false.</param>
        /// <param name="zCoordinate">The Z coordinate of the section cut result location when <paramref name="isDefault"/> is false.</param>
        void SetResultLocation(string nameSectionCut,
            bool isDefault,
            double xCoordinate = 0,
            double yCoordinate = 0,
            double zCoordinate = 0);

        // ===

        /// <summary>
        /// This function gets the side of the elements from which results are obtained.
        /// </summary>
        /// <param name="nameSectionCut">The name of an existing section cut.</param>
        /// <param name="side">This item is either 1 or 2 and indicates the side of the elements from which section cut results are obtained.
        /// Location depends on the section cut type, with 1 = Positive3, Top, Right, and 2 = Negative3, Bottom, Left for Analysis, Wall, or Spandrel/Slab.</param>
        void GetResultsSide(string nameSectionCut,
            ref int side);

        /// <summary>
        /// This function sets the side of the elements from which results are obtained.
        /// </summary>
        /// <param name="nameSectionCut">The name of an existing section cut.</param>
        /// <param name="side">This item is either 1 or 2 and indicates the side of the elements from which section cut results are obtained.
        /// Location depends on the section cut type, with 1 = Positive3, Top, Right, and 2 = Negative3, Bottom, Left for Analysis, Wall, or Spandrel/Slab.</param>
        void SetResultsSide(string nameSectionCut,
            int side);

        #endregion
    }
}
#endif