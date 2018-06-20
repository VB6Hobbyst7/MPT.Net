// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="Groups.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents groups in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.IGroups" />
    public class Groups : CSiApiBase, IGroups
    {
        #region Fields        
        /// <summary>
        /// Base default groups name.
        /// This must always be present in the application.
        /// </summary>
        public const string All = "ALL";

        #endregion


        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Groups" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Groups(CSiApiSeed seed) : base(seed) {}


        #endregion

        #region Methods: Public
        /// <summary>
        /// Return the number of defined groups.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.GroupDef.Count();
        }

        /// <summary>
        /// Retrieves the names of all defined groups.
        /// </summary>
        /// <param name="groupNames">Names of all defined groups.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] groupNames)
        {
            groupNames = new string[0];
            _callCode = _sapModel.GroupDef.GetNameList(ref _numberOfItems, ref groupNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the specified group.
        /// "ALL" is a reserved group name and cannot be deleted.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group. Cannot be "ALL".</param>
        /// <exception cref="CSiReservedNameException">Cannot delete reserved group name " + All</exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string nameGroup)
        {
            if (keywordIsReserved(nameGroup))
            {
                throw new CSiReservedNameException("Cannot delete reserved group name " + All);
            }

            _callCode = _sapModel.GroupDef.Delete(nameGroup);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Changes the name of the group.
        /// "ALL" is a reserved group name and cannot be changed.
        /// </summary>
        /// <param name="nameExisting">Existing name of a defined group.</param>
        /// <param name="nameNew">New name for the group. Cannot be "ALL".</param>
        /// <exception cref="CSiReservedNameException">Cannot change reserved group name " + Groups.All
        /// or
        /// Cannot change group name " + nameNew + " to reserved group name " + Groups.All</exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string nameExisting,
            string nameNew)
        {
            if (keywordIsReserved(nameExisting))
            {
                throw new CSiReservedNameException("Cannot change reserved group name " + Groups.All);
            }
            if (keywordIsReserved(nameNew))
            {
                throw new CSiReservedNameException("Cannot change group name " + nameNew + " to reserved group name " + Groups.All);
            }

            _callCode = _sapModel.GroupDef.ChangeName(nameExisting, nameNew);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Function clears (removes) all assignments from the specified group.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Clear(string nameGroup)
        {
            _callCode = _sapModel.GroupDef.Clear(nameGroup);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        /// <summary>
        /// Retrieves the assignments to a specified group.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group to get the assignments for.</param>
        /// <param name="objectTypes">Object type of each item in the group corresponding to an integer code.</param>
        /// <param name="objectNames">Name of each item in the group.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAssignments(string nameGroup,
            ref eObjectType[] objectTypes,
            ref string[] objectNames)
        {
            int[] csiObjectTypes = new int[0];

            _callCode = _sapModel.GroupDef.GetAssignments(nameGroup, ref _numberOfItems, ref csiObjectTypes, ref objectNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            objectTypes = csiObjectTypes.Cast<eObjectType>().ToArray();
        }


        /// <summary>
        /// Gets the group properties, such as display color and usages.
        /// </summary>
        /// <param name="name">Name of an existing group to get the properties for.</param>
        /// <param name="color">Display color for the group specified.</param>
        /// <param name="specifiedForSelection">True: The group is specified to be used for selection.</param>
        /// <param name="specifiedForSectionCutDefinition">True: The group is specified to be used for defining section cuts.</param>
        /// <param name="specifiedForSteelDesign">True: The group is specified to be used for defining steel frame design groups.</param>
        /// <param name="specifiedForConcreteDesign">True: The group is specified to be used for defining concrete frame design groups.</param>
        /// <param name="specifiedForAluminumDesign">True: The group is specified to be used for defining alumnimum frame design groups.</param>
        /// <param name="specifiedForColdFormedDesign">True: The group is specified to be used for defining colf formed frame design groups.</param>
        /// <param name="specifiedForStaticNLActiveStage">True: The group is specified to be used for defining stages for nonlinear static analysis.</param>
        /// <param name="specifiedForBridgeResponseOutput">True: The group is specified to be used for reporting bridge response output.</param>
        /// <param name="specifiedForAutoSeismicOutput">True: The group is specified to be used for reporting auto seismic loads.</param>
        /// <param name="specifiedForAutoWindOutput">True: The group is specified to be used for reporting auto wind loads.</param>
        /// <param name="specifiedForMassAndWeight">True: The group is specified to be used for reporting group masses and weight.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGroup(string name,
            ref int color,
            ref bool specifiedForSelection,
            ref bool specifiedForSectionCutDefinition,
            ref bool specifiedForSteelDesign,
            ref bool specifiedForConcreteDesign,
            ref bool specifiedForAluminumDesign,
            ref bool specifiedForColdFormedDesign,
            ref bool specifiedForStaticNLActiveStage,
            ref bool specifiedForBridgeResponseOutput,
            ref bool specifiedForAutoSeismicOutput,
            ref bool specifiedForAutoWindOutput,
            ref bool specifiedForMassAndWeight)
        {
            _callCode = _sapModel.GroupDef.GetGroup(name,
                ref color,
                ref specifiedForSelection,
                ref specifiedForSectionCutDefinition,
                ref specifiedForSteelDesign,
                ref specifiedForConcreteDesign,
                ref specifiedForAluminumDesign,
                ref specifiedForColdFormedDesign,
                ref specifiedForStaticNLActiveStage,
                ref specifiedForBridgeResponseOutput,
                ref specifiedForAutoSeismicOutput,
                ref specifiedForAutoWindOutput,
                ref specifiedForMassAndWeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets group properties, such as display color and usages.
        /// </summary>
        /// <param name="name">This is the name of a group.
        /// If this is the name of an existing group,  that group is modified, otherwise a new group is added.</param>
        /// <param name="color">Display color for the group specified.
        /// If this value is input as –1, the program automatically selects a display color for the group.</param>
        /// <param name="specifiedForSelection">True: The group is specified to be used for selection.</param>
        /// <param name="specifiedForSectionCutDefinition">True: The group is specified to be used for defining section cuts.</param>
        /// <param name="specifiedForSteelDesign">True: The group is specified to be used for defining steel frame design groups.</param>
        /// <param name="specifiedForConcreteDesign">True: The group is specified to be used for defining concrete frame design groups.</param>
        /// <param name="specifiedForAluminumDesign">True: The group is specified to be used for defining alumnimum frame design groups.</param>
        /// <param name="specifiedForColdFormedDesign">True: The group is specified to be used for defining colf formed frame design groups.</param>
        /// <param name="specifiedForStaticNLActiveStage">True: The group is specified to be used for defining stages for nonlinear static analysis.</param>
        /// <param name="specifiedForBridgeResponseOutput">True: The group is specified to be used for reporting bridge response output.</param>
        /// <param name="specifiedForAutoSeismicOutput">True: The group is specified to be used for reporting auto seismic loads.</param>
        /// <param name="specifiedForAutoWindOutput">True: The group is specified to be used for reporting auto wind loads.</param>
        /// <param name="specifiedForMassAndWeight">True: The group is specified to be used for reporting group masses and weight.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetGroup(string name,
            int color = -1,
            bool specifiedForSelection = true,
            bool specifiedForSectionCutDefinition = true,
            bool specifiedForSteelDesign = true,
            bool specifiedForConcreteDesign = true,
            bool specifiedForAluminumDesign = true,
            bool specifiedForColdFormedDesign = true,
            bool specifiedForStaticNLActiveStage = true,
            bool specifiedForBridgeResponseOutput = true,
            bool specifiedForAutoSeismicOutput = true,
            bool specifiedForAutoWindOutput = true,
            bool specifiedForMassAndWeight = true)
        {
            _callCode = _sapModel.GroupDef.SetGroup(name,
                color,
                specifiedForSelection,
                specifiedForSectionCutDefinition,
                specifiedForSteelDesign,
                specifiedForConcreteDesign,
                specifiedForAluminumDesign,
                specifiedForColdFormedDesign,
                specifiedForStaticNLActiveStage,
                specifiedForBridgeResponseOutput,
                specifiedForAutoSeismicOutput,
                specifiedForAutoWindOutput,
                specifiedForMassAndWeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets the group properties, such as display color and usages.
        /// </summary>
        /// <param name="name">Name of an existing group to get the properties for.</param>
        /// <param name="color">Display color for the group specified.</param>
        /// <param name="specifiedForSelection">True: The group is specified to be used for selection.</param>
        /// <param name="specifiedForSectionCutDefinition">True: The group is specified to be used for defining section cuts.</param>
        /// <param name="specifiedForSteelDesign">True: The group is specified to be used for defining steel frame design groups.</param>
        /// <param name="specifiedForConcreteDesign">True: The group is specified to be used for defining concrete frame design groups.</param>
        /// <param name="specifiedForStaticNLActiveStage">True: The group is specified to be used for defining stages for nonlinear static analysis.</param>
        /// <param name="specifiedForAutoSeismicOutput">True: The group is specified to be used for reporting auto seismic loads.</param>
        /// <param name="specifiedForAutoWindOutput">True: The group is specified to be used for reporting auto wind loads.</param>
        /// <param name="specifiedForMassAndWeight">True: The group is specified to be used for reporting group masses and weight.</param>
        /// <param name="specifiedForSteelJoistDesign">True: The group is specified to be used for defining steel joist design groups.</param>
        /// <param name="specifiedForWallDesign">True: The group is specified to be used for defining wall design groups.</param>
        /// <param name="specifiedForBasePlateDesign">True: The group is specified to be used for defining base plate design groups.</param>
        /// <param name="specifiedForConnectionDesign">True: The group is specified to be used for defining connection design design groups.</param>
        public void GetGroup(string name,
            ref int color,
            ref bool specifiedForSelection,
            ref bool specifiedForSectionCutDefinition,
            ref bool specifiedForSteelDesign,
            ref bool specifiedForConcreteDesign,
            ref bool specifiedForStaticNLActiveStage,
            ref bool specifiedForAutoSeismicOutput,
            ref bool specifiedForAutoWindOutput,
            ref bool specifiedForMassAndWeight,
            ref bool specifiedForSteelJoistDesign,
            ref bool specifiedForWallDesign,
            ref bool specifiedForBasePlateDesign,
            ref bool specifiedForConnectionDesign)
        {
            bool specifiedForAluminumDesign = false;
            _callCode = _sapModel.GroupDef.GetGroup_1(name,
                ref color,
                ref specifiedForSelection,
                ref specifiedForSectionCutDefinition,
                ref specifiedForSteelDesign,
                ref specifiedForConcreteDesign,
                ref specifiedForAluminumDesign,
                ref specifiedForStaticNLActiveStage,
                ref specifiedForAutoSeismicOutput,
                ref specifiedForAutoWindOutput,
                ref specifiedForMassAndWeight,
                ref specifiedForSteelJoistDesign,
                ref specifiedForWallDesign,
                ref specifiedForBasePlateDesign,
                ref specifiedForConnectionDesign);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets group properties, such as display color and usages.
        /// </summary>
        /// <param name="name">This is the name of a group.  
        /// If this is the name of an existing group,  that group is modified, otherwise a new group is added.</param>
        /// <param name="color">Display color for the group specified.
        /// If this value is input as –1, the program automatically selects a display color for the group.</param>
        /// <param name="specifiedForSelection">True: The group is specified to be used for selection.</param>
        /// <param name="specifiedForSectionCutDefinition">True: The group is specified to be used for defining section cuts.</param>
        /// <param name="specifiedForSteelDesign">True: The group is specified to be used for defining steel frame design groups.</param>
        /// <param name="specifiedForConcreteDesign">True: The group is specified to be used for defining concrete frame design groups.</param>
        /// <param name="specifiedForStaticNLActiveStage">True: The group is specified to be used for defining stages for nonlinear static analysis.</param>
        /// <param name="specifiedForAutoSeismicOutput">True: The group is specified to be used for reporting auto seismic loads.</param>
        /// <param name="specifiedForAutoWindOutput">True: The group is specified to be used for reporting auto wind loads.</param>
        /// <param name="specifiedForMassAndWeight">True: The group is specified to be used for reporting group masses and weight.</param>
        /// <param name="specifiedForSteelJoistDesign">True: The group is specified to be used for defining steel joist design groups.</param>
        /// <param name="specifiedForWallDesign">True: The group is specified to be used for defining wall design groups.</param>
        /// <param name="specifiedForBasePlateDesign">True: The group is specified to be used for defining base plate design groups.</param>
        /// <param name="specifiedForConnectionDesign">True: The group is specified to be used for defining connection design design groups.</param>
        public void SetGroup(string name,
            int color = -1,
            bool specifiedForSelection = true,
            bool specifiedForSectionCutDefinition = true,
            bool specifiedForSteelDesign = true,
            bool specifiedForConcreteDesign = true,
            bool specifiedForStaticNLActiveStage = true,
            bool specifiedForAutoSeismicOutput = true,
            bool specifiedForAutoWindOutput = true,
            bool specifiedForMassAndWeight = true,
            bool specifiedForSteelJoistDesign = true,
            bool specifiedForWallDesign = true,
            bool specifiedForBasePlateDesign = true,
            bool specifiedForConnectionDesign = true)
        {
            bool specifiedForAluminumDesign = true;
            _callCode = _sapModel.GroupDef.SetGroup_1(name,
                color,
                specifiedForSelection,
                specifiedForSectionCutDefinition,
                specifiedForSteelDesign,
                specifiedForConcreteDesign,
                specifiedForAluminumDesign,
                specifiedForStaticNLActiveStage,
                specifiedForAutoSeismicOutput,
                specifiedForAutoWindOutput,
                specifiedForMassAndWeight,
                specifiedForSteelJoistDesign,
                specifiedForWallDesign,
                specifiedForBasePlateDesign,
                specifiedForConnectionDesign);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Methods: Private

        /// <summary>
        /// Determines if the provided keyword is a reserved keyword.
        /// </summary>
        /// <param name="parameter">Keyword to check.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool keywordIsReserved(string parameter)
        {
            return parameter.CompareTo(All) == 0;
        }

        #endregion
    }
}
