// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IRebarDataPierSpandrel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has pier and spandrel rebar data available for retrieval.
    /// </summary>
    public interface IRebarDataPierSpandrel
    {
        /// <summary>
        /// Retrieves rebar data for an area pier object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="layerIds">The rebar layer ids.</param>
        /// <param name="layerTypes">The rebar layer types.</param>
        /// <param name="clearCovers">The clear cover of each rebar layer.</param>
        /// <param name="barAreas">The bar areas for each layer.</param>
        /// <param name="barSpacings">The bar spacings for each layer.</param>
        /// <param name="numberOfBars">The number of bars in each layer.</param>
        /// <param name="isConfined">Status of whether or not the rebar layer is confined.</param>
        /// <param name="barSizeNames">The bar size names for each layer.</param>
        /// <param name="endZoneLengths">The end zone lengths for each layer.</param>
        /// <param name="endZoneThicknesses">The end zone thicknesses for each layer.</param>
        /// <param name="endZoneOffsets">The end zone offsets for each layer.</param>
        void GetRebarDataPier(string name,
            ref string[] layerIds,
            ref eWallPierRebarLayerType[] layerTypes,
            ref double[] clearCovers,
            ref double[] barAreas,
            ref double[] barSpacings,
            ref int[] numberOfBars,
            ref bool[] isConfined,
            ref string[] barSizeNames,
            ref double[] endZoneLengths,
            ref double[] endZoneThicknesses,
            ref double[] endZoneOffsets);

        /// <summary>
        /// Retrieves rebar data for an area spandrel object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="layerIds">The rebar layer ids.</param>
        /// <param name="layerTypes">The rebar layer types.</param>
        /// <param name="clearCovers">The clear cover of each rebar layer.</param>
        /// <param name="barAreas">The bar areas for each layer.</param>
        /// <param name="barSpacings">The bar spacings for each layer.</param>
        /// <param name="numberOfBars">The number of bars in each layer.</param>
        /// <param name="isConfined">Status of whether or not the rebar layer is confined.</param>
        /// <param name="barSizeIndices">The rebar size indices in each layer.</param>
        void GetRebarDataSpandrel(string name,
            ref string[] layerIds,
            ref eWallSpandrelRebarLayerType[] layerTypes,
            ref double[] clearCovers,
            ref double[] barAreas,
            ref double[] barSpacings,
            ref int[] numberOfBars,
            ref bool[] isConfined,
            ref int[] barSizeIndices);
    }
}
#endif