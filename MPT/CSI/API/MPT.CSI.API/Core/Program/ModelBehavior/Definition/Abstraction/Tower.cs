// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="Tower.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Represents a tower object in the application..
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.ITower" />
    public class Tower : CSiApiBase, ITower
    {
#region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Tower"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Tower(CSiApiSeed seed) : base(seed)
        {

        }
#endregion

#region Methods: Interface
        /// <summary>
        /// Changes the name of a defined tower property.
        /// </summary>
        /// <param name="nameExisting">Existing name of a defined tower property.</param>
        /// <param name="nameNew">New name for the tower property.</param>
        public void ChangeName(string nameExisting,
            string nameNew)
        {
            _callCode = _sapModel.Tower.RenameTower(nameExisting, nameNew);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the names of all defined tower property.
        /// </summary>
        /// <param name="names">The tower property names retrieved by the program.</param>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.Tower.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// Deletes the specified tower property.
        /// </summary>
        /// <param name="name">The name of an existing tower property.</param>
        public void Delete(string name)
        {
            bool associate = false;
            string associateWithTower = "";
            _callCode = _sapModel.Tower.DeleteTower(name, associate, associateWithTower);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the specified tower property.
        /// </summary>
        /// <param name="name">The name of an existing tower property.</param>
        /// <param name="associate">True: The tower is associated with another tower.</param>
        /// <param name="associateWithTower">The name of the tower to associate with.</param>
        public void Delete(string name,
            bool associate,
            string associateWithTower = "")
        {
            _callCode = _sapModel.Tower.DeleteTower(name, associate, associateWithTower);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Adds a copy of the tower.
        /// </summary>
        /// <param name="towerName">Name of the tower.</param>
        /// <param name="newTowerName">New name of the tower.</param>
        public void AddCopyOfTower(string towerName,
            string newTowerName)
        {
            _callCode = _sapModel.Tower.AddCopyOfTower(towerName, newTowerName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Adds a new tower.
        /// </summary>
        /// <param name="towerName">Name of the tower.</param>
        /// <param name="numberOfStories">The number of stories.</param>
        /// <param name="typicalStoryHeight">Height of the typical story.</param>
        /// <param name="bottomStoryHeight">Height of the botttom story.</param>
        public void AddNewTower(string towerName,
            int numberOfStories,
            double typicalStoryHeight,
            double bottomStoryHeight)
        {
            _callCode = _sapModel.Tower.AddNewTower(towerName, numberOfStories, typicalStoryHeight, bottomStoryHeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets whether or not multiple towers are allowed.
        /// </summary>
        /// <param name="allowMultipleTowers">True: Multiple towers are allowed.</param>
        /// <param name="retainedTower">The retained tower.</param>
        /// <param name="combine">True: Towers are combined.</param>
        public void AllowMultipleTowers(bool allowMultipleTowers,
            string retainedTower = "",
            bool combine = true)
        {
            _callCode = _sapModel.Tower.AllowMultipleTowers(allowMultipleTowers, retainedTower, combine);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Gets the active tower.
        /// </summary>
        /// <param name="towerName">Name of the tower.</param>
        public void GetActiveTower(ref string towerName)
        {
            _callCode = _sapModel.Tower.GetActiveTower(ref towerName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the active tower.
        /// </summary>
        /// <param name="towerName">Name of the tower.</param>
        public void SetActiveTower(string towerName)
        {
            _callCode = _sapModel.Tower.SetActiveTower(towerName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif