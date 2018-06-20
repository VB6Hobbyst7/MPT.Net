// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-03-2017
//
// Last Modified By : Mark
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IDesignDetailing.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements a design detailing interface for all concrete-based frame elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignRun" />
    public interface IDesignDetailing : IDesignRun
    {
        /// <summary>
        /// Starts the frame design.
        /// </summary>
        /// <param name="overwriteExisting">True: Any existing detailing will be overwritten.</param>
        void StartDesign(bool overwriteExisting);



        /// <summary>
        /// Retrieves longitudinal rebar data for a beam frame object .
        /// </summary>
        /// <param name="name">The name of an existing beam frame object.</param>
        /// <param name="numberOfRebarSets">Number of tie rebar sets for this beam object.</param>
        /// <param name="barSizeNames">Rebar designation.</param>
        /// <param name="barAreas">Individual rebar areas.</param>
        /// <param name="numberOfBars">The number of bars.</param>
        /// <param name="locations">Zone of this set of rebars - A, B, C, etc. (See picture for seismic/ non-seismic detailing).</param>
        /// <param name="clearCovers">Clear cover from face to ties being specified.</param>
        /// <param name="startCoordinates">Start of this set of tie rebars from start/end (depends on location) of column object.</param>
        /// <param name="barLengths">Length over which these tie bars are provided from start point.</param>
        /// <param name="bendingAnglesStart">Bend angle if any at start of rebars.</param>
        /// <param name="bendingAnglesEnd">Bend angle if any at end of rebars.</param>
        /// <param name="rebarSetGUIDs">The GUIDs of each of the rebar sets (could be duplicated for adjacent beam if rebars are continuous).</param>
        void GetBeamLongitudinalRebarData(string name,
            ref int numberOfRebarSets,
            ref string[] barSizeNames,
            ref double[] barAreas,
            ref int[] numberOfBars,
            ref string[] locations,
            ref double[] clearCovers,
            ref double[] startCoordinates,
            ref double[] barLengths,
            ref double[] bendingAnglesStart,
            ref double[] bendingAnglesEnd,
            ref string[] rebarSetGUIDs);


        /// <summary>
        /// Retrieves tie (confinement/shear) rebar data for a beam frame object.
        /// </summary>
        /// <param name="name">The name of an existing beam frame object.</param>
        /// <param name="numberOfRebarSets">Number of tie rebar sets for this beam object.</param>
        /// <param name="barSizeNames">Rebar designation.</param>
        /// <param name="barAreas">Individual rebar areas.</param>
        /// <param name="numberLegs">Number of legs for ties in this set (legs are in the local 2 direction).</param>
        /// <param name="locations">Zone of this set of rebars - A, B, C, etc. (See picture for seismic/ non-seismic detailing).</param>
        /// <param name="clearCovers">Clear cover from face to ties being specified.</param>
        /// <param name="startCoordinates">Start of this set of tie rebars from start/end (depends on location) of column object.</param>
        /// <param name="spacings">Spacing of ties.</param>
        /// <param name="lengths">Length over which these tie bars are provided from start point.</param>
        /// <param name="rebarSetGUID">The GUIDs of each of the rebar sets (could be duplicated for adjacent beam if rebars are continuous).</param>
        void GetBeamTieRebarData(string name,
            ref int numberOfRebarSets,
            ref string[] barSizeNames,
            ref double[] barAreas,
            ref double[] numberLegs,
            ref string[] locations,
            ref double[] clearCovers,
            ref double[] startCoordinates,
            ref double[] spacings,
            ref double[] lengths,
            ref string[] rebarSetGUID);



        /// <summary>
        /// Retrieves longitudinal rebar data for a column frame object.
        /// </summary>
        /// <param name="name">The name of an existing beam frame object.</param>
        /// <param name="numberOfRebarSets">Number of tie rebar sets for this beam object.</param>
        /// <param name="barSizeNames">Rebar designation.</param>
        /// <param name="barAreas">Individual rebar areas.</param>
        /// <param name="numberOfCBars">Total number of rebars in this set (for both <see cref="eRebarConfiguration"/>).</param>
        /// <param name="numberOfR3Bars">Number of rectangular pattern bars along 3 direction (per face including corner bars); 
        /// zero for <see cref="eRebarConfiguration"/> = <see cref="eRebarConfiguration.Circular"/>.</param>
        /// <param name="numberOfR2Bars">Number of rectangular pattern bars along 2 direction (per face including corner bars); 
        /// zero for <see cref="eRebarConfiguration"/> = <see cref="eRebarConfiguration.Circular"/>.</param>
        /// <param name="locations">Zone of this set of rebars - A, B, C, etc. (See picture for seismic/ non-seismic detailing).</param>
        /// <param name="clearCovers">Clear cover from face to ties being specified.</param>
        /// <param name="rebarSetGUID">The GUIDs of each of the rebar sets (could be duplicated for adjacent beam if rebars are continuous).</param>
        void GetColumnLongitudinalRebarData(string name,
            ref int numberOfRebarSets,
            ref string[] barSizeNames,
            ref double[] barAreas,
            ref int[] numberOfCBars,
            ref int[] numberOfR3Bars,
            ref int[] numberOfR2Bars,
            ref string[] locations,
            ref double[] clearCovers,
            ref string[] rebarSetGUID);

        /// <summary>
        /// Retrieves tie (confinement/shear) rebar data for a column frame object .
        /// </summary>
        /// <param name="name">The name of an existing beam frame object.</param>
        /// <param name="numberOfRebarSets">Number of tie rebar sets for this beam object.</param>
        /// <param name="barSizeNames">Rebar designation.</param>
        /// <param name="barAreas">Individual rebar areas.</param>
        /// <param name="pattern">Rebar pattern.</param>
        /// <param name="confineType">Confinement type.</param>
        /// <param name="numberOfLegs2Dir">Number of legs for ties in this set along the local 2 direction).</param>
        /// <param name="numberOfLegs3Dir">Number of legs for ties in this set along the local 3 direction).</param>
        /// <param name="locations">Zone of this set of rebars - A, B, C, etc. (See picture for seismic/ non-seismic detailing).</param>
        /// <param name="clearCovers">Clear cover from face to ties being specified.</param>
        /// <param name="startCoordinates">Start of this set of tie rebars from start/end (depends on location) of column object.</param>
        /// <param name="spacings">Spacing of ties.</param>
        /// <param name="heights">Height over which these tie bars are provided from start point.</param>
        /// <param name="rebarSetGUID">The GUIDs of each of the rebar sets (could be duplicated for adjacent beam if rebars are continuous).</param>
        void GetColumnTieRebarData(string name,
            ref int numberOfRebarSets,
            ref string[] barSizeNames,
            ref double[] barAreas,
            ref eRebarConfiguration[] pattern,
            ref eConfinementType[] confineType,
            ref int[] numberOfLegs2Dir,
            ref int[] numberOfLegs3Dir,
            ref string[] locations,
            ref double[] clearCovers,
            ref double[] startCoordinates,
            ref double[] spacings,
            ref double[] heights,
            ref string[] rebarSetGUID);



        /// <summary>
        /// Gets the detailed beam line data.
        /// </summary>
        /// <param name="beamLineID">The beam line identifier.</param>
        /// <param name="objectUniqueNames">The object unique names.</param>
        /// <param name="numberOfSpans">The number of spans.</param>
        /// <param name="spanLength">Length of the span.</param>
        /// <param name="numberOfLongitudinalBars">The number of longitudinal bars.</param>
        /// <param name="longitudinalBarDiameters">The longitudinal bar diameters.</param>
        /// <param name="longitudinalBarNotations">The longitudinal bar notations.</param>
        /// <param name="longitudinalBarStartDistances">The longitudinal bar start distances.</param>
        /// <param name="longitudinalBarStartBends">Bend angle if any at the start of longitudinal rebars.</param>
        /// <param name="longitudinalBarEndBends">Bend angle if any at the end of longitudinal rebars.</param>
        /// <param name="longitudinalBarLengths">The longitudinal bar lengths.</param>
        /// <param name="longitudinalBarNumberOfLayers">The number of layers of longitudinal bars.</param>
        /// <param name="numberOfTieBars">The number of tie bars.</param>
        /// <param name="numberOfTieVerticalLegs">The number of tie vertical legs.</param>
        /// <param name="tieBarDiameters">The tie bar diameters.</param>
        /// <param name="tieBarNotations">The tie bar notations.</param>
        /// <param name="tieBarStartDistances">The tie bar start distances.</param>
        /// <param name="tieBarSpacings">The tie bar spacings.</param>
        /// <param name="tieBarTypes">The tie bar types.</param>
        void GetDetailedBeamLineData(string beamLineID,
            ref string[] objectUniqueNames,
            ref int numberOfSpans,
            ref double[] spanLength,
            ref int[] numberOfLongitudinalBars,
            ref double[] longitudinalBarDiameters,
            ref string[] longitudinalBarNotations,
            ref double[] longitudinalBarStartDistances,
            ref int[] longitudinalBarStartBends,
            ref int[] longitudinalBarEndBends,
            ref double[] longitudinalBarLengths,
            ref int[] longitudinalBarNumberOfLayers,
            ref int[] numberOfTieBars,
            ref int[] numberOfTieVerticalLegs,
            ref double[] tieBarDiameters,
            ref string[] tieBarNotations,
            ref double[] tieBarStartDistances,
            ref double[] tieBarSpacings,
            ref int[] tieBarTypes);



        /// <summary>
        /// Gets the detailed beam lines.
        /// </summary>
        /// <param name="beamLineIDs">The beam line IDs.</param>
        void GetDetailedBeamLines(ref string[] beamLineIDs);



        /// <summary>
        /// Gets the detailed column stack data.
        /// </summary>
        /// <param name="columnStackID">The column stack identifier.</param>
        /// <param name="objectUniqueNames">The object unique names.</param>
        /// <param name="numberOfRebarSets">The number of rebar sets.</param>
        /// <param name="numberOfLongitudinalBars">The number of longitudinal bars.</param>
        /// <param name="longitudinalBarDiameters">The longitudinal bar diameters.</param>
        /// <param name="longitudinalBarNotations">The longitudinal bar notations.</param>
        /// <param name="longitudinalBarStartDistances">The longitudinal bar start distances.</param>
        /// <param name="longitudinalBarStartBends">Bend angle if any at the start of longitudinal rebars.</param>
        /// <param name="longitudinalBarEndBends">Bend angle if any at the end of longitudinal rebars.</param>
        /// <param name="longitudinalBarLengths">The longitudinal bar lengths.</param>
        /// <param name="longitudinalBarNumberOfLayers">The number of layers of longitudinal bars.</param>
        /// <param name="numberOfTieZones">The number of tie zones.</param>
        /// <param name="tieBarZones">The tie bar zones.</param>
        /// <param name="numberOfTieBars">The number of tie bars.</param>
        /// <param name="numberOfTieVerticalLegs">The number of tie vert legs.</param>
        /// <param name="tieBarDiameters">The tie bar diameters.</param>
        /// <param name="tieBarNotations">The tie bar notations.</param>
        /// <param name="tieBarStartDistances">The tie bar start distances.</param>
        /// <param name="tieBarSpacings">The tie bar spacings.</param>
        /// <param name="tieBarTypes">The tie bar types.</param>
        void GetDetailedColumnStackData(string columnStackID,
            ref string[] objectUniqueNames,
            ref int numberOfRebarSets,
            ref int[] numberOfLongitudinalBars,
            ref double[] longitudinalBarDiameters,
            ref string[] longitudinalBarNotations,
            ref double[] longitudinalBarStartDistances,
            ref int[] longitudinalBarStartBends,
            ref int[] longitudinalBarEndBends,
            ref double[] longitudinalBarLengths,
            ref int[] longitudinalBarNumberOfLayers,
            ref int numberOfTieZones,
            ref string[] tieBarZones,
            ref int[] numberOfTieBars,
            ref int[] numberOfTieVerticalLegs,
            ref double[] tieBarDiameters,
            ref string[] tieBarNotations,
            ref double[] tieBarStartDistances,
            ref double[] tieBarSpacings,
            ref int[] tieBarTypes);


        /// <summary>
        /// Gets the detailed column stacks.
        /// </summary>
        /// <param name="columnStackIDs">The column stack IDs.</param>
        void GetDetailedColumnStacks(ref string[] columnStackIDs);



        /// <summary>
        /// Gets the detailed slab bottom bar data.
        /// </summary>
        /// <param name="slabName">Name of the slab.</param>
        /// <param name="names">Rebar designations.</param>
        /// <param name="numberOfBars">The number of bars.</param>
        /// <param name="barDiameters">The bar diameters.</param>
        /// <param name="barNotations">The bar notations.</param>
        /// <param name="barMaterials">The bar materials.</param>
        /// <param name="startX">The start, x-coordinate.</param>
        /// <param name="startY">The start, y-coordinate.</param>
        /// <param name="startZ">The start, z-coordinate.</param>
        /// <param name="endX">The end, x-coordinate.</param>
        /// <param name="endY">The end, y-coordinate.</param>
        /// <param name="endZ">The end, z-coordinate.</param>
        /// <param name="widthsLeft">The strip widths, left side.</param>
        /// <param name="widthsRight">The strip widths, right side.</param>
        /// <param name="offsetsFromTop">The offsets from top.</param>
        /// <param name="offsetsFromBottom">The offsets from bottom.</param>
        /// <param name="startBarBends">Bend angle if any at start of rebars.</param>
        /// <param name="endBarBends">Bend angle if any at end of rebars.</param>
        /// <param name="GUIDs">The GUIDs of each of the rebar sets (could be duplicated for adjacent beam if rebars are continuous).</param>
        void GetDetailedSlabBottomBarData(string slabName,
            ref string[] names,
            ref int[] numberOfBars,
            ref double[] barDiameters,
            ref string[] barNotations,
            ref string[] barMaterials,
            ref double[] startX,
            ref double[] startY,
            ref double[] startZ,
            ref double[] endX,
            ref double[] endY,
            ref double[] endZ,
            ref double[] widthsLeft,
            ref double[] widthsRight,
            ref double[] offsetsFromTop,
            ref double[] offsetsFromBottom,
            ref int[] startBarBends,
            ref int[] endBarBends,
            ref string[] GUIDs);


        /// <summary>
        /// Gets the detailed slab bottom bar data.
        /// </summary>
        /// <param name="slabName">Name of the slab.</param>
        /// <param name="names">Rebar designations.</param>
        /// <param name="numberOfBars">The number of bars.</param>
        /// <param name="barDiameters">The bar diameters.</param>
        /// <param name="barNotations">The bar notations.</param>
        /// <param name="barMaterials">The bar materials.</param>
        /// <param name="startX">The start, x-coordinate.</param>
        /// <param name="startY">The start, y-coordinate.</param>
        /// <param name="startZ">The start, z-coordinate.</param>
        /// <param name="endX">The end, x-coordinate.</param>
        /// <param name="endY">The end, y-coordinate.</param>
        /// <param name="endZ">The end, z-coordinate.</param>
        /// <param name="widthsLeft">The strip widths, left side.</param>
        /// <param name="widthsRight">The strip widths, right side.</param>
        /// <param name="offsetsFromTop">The offsets from top.</param>
        /// <param name="offsetsFromBottom">The offsets from bottom.</param>
        /// <param name="startBarBends">Bend angle if any at start of rebars.</param>
        /// <param name="endBarBends">Bend angle if any at end of rebars.</param>
        /// <param name="GUIDs">The GUIDs of each of the rebar sets (could be duplicated for adjacent beam if rebars are continuous).</param>
        /// <param name="stripNames">The strip names.</param>
        /// <param name="spanNumbers">The span numbers.</param>
        void GetDetailedSlabBottomBarData(string slabName,
            ref string[] names,
            ref int[] numberOfBars,
            ref double[] barDiameters,
            ref string[] barNotations,
            ref string[] barMaterials,
            ref double[] startX,
            ref double[] startY,
            ref double[] startZ,
            ref double[] endX,
            ref double[] endY,
            ref double[] endZ,
            ref double[] widthsLeft,
            ref double[] widthsRight,
            ref double[] offsetsFromTop,
            ref double[] offsetsFromBottom,
            ref int[] startBarBends,
            ref int[] endBarBends,
            ref string[] GUIDs,
            ref string[] stripNames,
            ref int[] spanNumbers);



        /// <summary>
        /// Gets the detailed slabs.
        /// </summary>
        /// <param name="names">The names of the slabs.</param>
        /// <param name="slabElevations">The elevations of the slabs.</param>
        /// <param name="GUIDs">The GUIDs of each of the rebar sets (could be duplicated for adjacent beam if rebars are continuous).</param>
        void GetDetailedSlabs(ref string[] names,
            ref double[] slabElevations,
            ref string[] GUIDs);


        /// <summary>
        /// Gets the detailed slab top bar data.
        /// </summary>
        /// <param name="slabName">Name of the slab.</param>
        /// <param name="names">Rebar designations.</param>
        /// <param name="numberOfBars">The number of bars.</param>
        /// <param name="barDiameters">The bar diameters.</param>
        /// <param name="barNotations">The bar notations.</param>
        /// <param name="barMaterials">The bar materials.</param>
        /// <param name="startX">The start, x-coordinate.</param>
        /// <param name="startY">The start, y-coordinate.</param>
        /// <param name="startZ">The start, z-coordinate.</param>
        /// <param name="endX">The end, x-coordinate.</param>
        /// <param name="endY">The end, y-coordinate.</param>
        /// <param name="endZ">The end, z-coordinate.</param>
        /// <param name="widthsLeft">The strip widths, left side.</param>
        /// <param name="widthsRight">The strip widths, right side.</param>
        /// <param name="offsetsFromTop">The offsets from top.</param>
        /// <param name="offsetsFromBottom">The offsets from bottom.</param>
        /// <param name="startBarBends">Bend angle if any at start of rebars.</param>
        /// <param name="endBarBends">Bend angle if any at end of rebars.</param>
        /// <param name="GUIDs">The GUIDs of each of the rebar sets (could be duplicated for adjacent beam if rebars are continuous).</param>
        void GetDetailedSlabTopBarData(string slabName,
            ref string[] names,
            ref int[] numberOfBars,
            ref double[] barDiameters,
            ref string[] barNotations,
            ref string[] barMaterials,
            ref double[] startX,
            ref double[] startY,
            ref double[] startZ,
            ref double[] endX,
            ref double[] endY,
            ref double[] endZ,
            ref double[] widthsLeft,
            ref double[] widthsRight,
            ref double[] offsetsFromTop,
            ref double[] offsetsFromBottom,
            ref int[] startBarBends,
            ref int[] endBarBends,
            ref string[] GUIDs);

        /// <summary>
        /// Gets the detailed slab top bar data.
        /// </summary>
        /// <param name="slabName">Name of the slab.</param>
        /// <param name="names">Rebar designations.</param>
        /// <param name="numberOfBars">The number of bars.</param>
        /// <param name="barDiameters">The bar diameters.</param>
        /// <param name="barNotations">The bar notations.</param>
        /// <param name="barMaterials">The bar materials.</param>
        /// <param name="startX">The start, x-coordinate.</param>
        /// <param name="startY">The start, y-coordinate.</param>
        /// <param name="startZ">The start, z-coordinate.</param>
        /// <param name="endX">The end, x-coordinate.</param>
        /// <param name="endY">The end, y-coordinate.</param>
        /// <param name="endZ">The end, z-coordinate.</param>
        /// <param name="widthsLeft">The strip widths, left side.</param>
        /// <param name="widthsRight">The strip widths, right side.</param>
        /// <param name="offsetsFromTop">The offsets from top.</param>
        /// <param name="offsetsFromBottom">The offsets from bottom.</param>
        /// <param name="startBarBends">Bend angle if any at start of rebars.</param>
        /// <param name="endBarBends">Bend angle if any at end of rebars.</param>
        /// <param name="GUIDs">The GUIDs of each of the rebar sets (could be duplicated for adjacent beam if rebars are continuous).</param>
        /// <param name="stripNames">The strip names.</param>
        /// <param name="spanNumbers">The span numbers.</param>
        void GetDetailedSlabTopBarData(string slabName,
            ref string[] names,
            ref int[] numberOfBars,
            ref double[] barDiameters,
            ref string[] barNotations,
            ref string[] barMaterials,
            ref double[] startX,
            ref double[] startY,
            ref double[] startZ,
            ref double[] endX,
            ref double[] endY,
            ref double[] endZ,
            ref double[] widthsLeft,
            ref double[] widthsRight,
            ref double[] offsetsFromTop,
            ref double[] offsetsFromBottom,
            ref int[] startBarBends,
            ref int[] endBarBends,
            ref string[] GUIDs,
            ref string[] stripNames,
            ref int[] spanNumbers);



        /// <summary>
        /// Gets the similar beam lines.
        /// </summary>
        /// <param name="beamLineID">The beam line identifier.</param>
        /// <param name="numberOfSimilarBeams">The number of similar beams.</param>
        /// <param name="numberOfUniqueObjects">The number of unique objects.</param>
        /// <param name="objectUniqueNames">The object unique names of the similar beam lines.</param>
        void GetSimilarBeamLines(string beamLineID,
            ref int numberOfSimilarBeams,
            ref int[] numberOfUniqueObjects,
            ref string[] objectUniqueNames);


        /// <summary>
        /// Gets the similar column stacks.
        /// </summary>
        /// <param name="columnStackID">The column stack identifier.</param>
        /// <param name="numberOfSimilarColumns">The number of similar columns.</param>
        /// <param name="numberOfUniqueObjects">The number of unique objects.</param>
        /// <param name="objectUniqueNames">The object unique names of the similar column stacks.</param>
        void GetSimilarColumnStacks(string columnStackID,
            ref int numberOfSimilarColumns,
            ref int[] numberOfUniqueObjects,
            ref string[] objectUniqueNames);


        /// <summary>
        /// Gets the similar slabs.
        /// </summary>
        /// <param name="slabName">Name of the slab.</param>
        /// <param name="numberOfSimilarSlabs">The number of similar slabs.</param>
        /// <param name="names">The names of the similar slabs.</param>
        void GetSimilarSlabs(string slabName,
            ref int numberOfSimilarSlabs,
            ref string[] names);
    }
}
#endif