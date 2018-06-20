// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-21-2017
// ***********************************************************************
// <copyright file="IOutputStations.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has output stations that can be retrieved or changed.
    /// </summary>
    public interface IOutputStations
    {
        /// <summary>
        /// This function retrieves object output station data.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="outputStationType">Indicates how the output stations are specified.</param>
        /// <param name="maxStationSpacing">The maximum segment size, that is, the maximum station spacing. [L]
        /// This item applies only when <paramref name="outputStationType" /> = <see cref="eOutputStationType.MaxSpacing" />.</param>
        /// <param name="minStationNumber">The minimum number of stations.
        /// This item applies only when <paramref name="outputStationType" /> = <see cref="eOutputStationType.MinStations" />.</param>
        /// <param name="noOutputAndDesignAtElementEnds">True: No additional output stations are added at the ends of line elements when the object is internally meshed.</param>
        /// <param name="noOutputAndDesignAtPointLoads">True: No additional output stations are added at point load locations.</param>
        void GetOutputStations(string name,
            ref eOutputStationType outputStationType,
            ref double maxStationSpacing,
            ref int minStationNumber,
            ref bool noOutputAndDesignAtElementEnds,
            ref bool noOutputAndDesignAtPointLoads);

        /// <summary>
        /// This function assigns object output station data.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="outputStationType">Indicates how the output stations are specified.</param>
        /// <param name="maxStationSpacing">The maximum segment size, that is, the maximum station spacing. [L]
        /// This item applies only when <paramref name="outputStationType" /> = <see cref="eOutputStationType.MaxSpacing" />.</param>
        /// <param name="minStationNumber">The minimum number of stations.
        /// This item applies only when <paramref name="outputStationType" /> = <see cref="eOutputStationType.MinStations" />.</param>
        /// <param name="noOutputAndDesignAtElementEnds">True: No additional output stations are added at the ends of line elements when the cable object is internally meshed.</param>
        /// <param name="noOutputAndDesignAtPointLoads">True: No additional output stations are added at point load locations.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetOutputStations(string name,
            eOutputStationType outputStationType,
            double maxStationSpacing,
            int minStationNumber,
            bool noOutputAndDesignAtElementEnds,
            bool noOutputAndDesignAtPointLoads,
            eItemType itemType = eItemType.Object);
    }
}