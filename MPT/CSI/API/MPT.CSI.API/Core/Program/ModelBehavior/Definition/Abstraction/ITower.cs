// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-05-2017
// ***********************************************************************
// <copyright file="ITower.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Object has a gettable/settable tower.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    public interface ITower : IListableNames, IChangeableName
    {
        /// <summary>
        /// Adds a copy of the tower.
        /// </summary>
        /// <param name="towerName">Name of the tower.</param>
        /// <param name="newTowerName">New name of the tower.</param>
        void AddCopyOfTower(string towerName,
            string newTowerName);

        /// <summary>
        /// Adds a new tower.
        /// </summary>
        /// <param name="towerName">Name of the tower.</param>
        /// <param name="numberOfStories">The number of stories.</param>
        /// <param name="typicalStoryHeight">Height of the typical story.</param>
        /// <param name="bottomStoryHeight">Height of the botttom story.</param>
        void AddNewTower(string towerName,
            int numberOfStories,
            double typicalStoryHeight,
            double bottomStoryHeight);

        /// <summary>
        /// Sets whether or not multiple towers are allowed.
        /// </summary>
        /// <param name="allowMultipleTowers">True: Multiple towers are allowed.</param>
        /// <param name="retainedTower">The retained tower.</param>
        /// <param name="combine">True: Towers are combined.</param>
        void AllowMultipleTowers(bool allowMultipleTowers,
            string retainedTower = "",
            bool combine = true);

        /// <summary>
        /// Gets the active tower.
        /// </summary>
        /// <param name="towerName">Name of the tower.</param>
        void GetActiveTower(ref string towerName);

        /// <summary>
        /// Sets the active tower.
        /// </summary>
        /// <param name="towerName">Name of the tower.</param>
        void SetActiveTower(string towerName);

        /// <summary>
        /// Deletes the specified tower property.
        /// </summary>
        /// <param name="name">The name of an existing tower property.</param>
        /// <param name="associate">True: The tower is associated with another tower.</param>
        /// <param name="associateWithTower">The name of the tower to associate with.</param>
        void Delete(string name,
            bool associate,
            string associateWithTower = "");
    }
}
#endif