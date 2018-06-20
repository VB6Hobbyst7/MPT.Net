// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="IGroups.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Implements groups in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IGroups : IChangeableName, ICountable, IDeletable, IListableNames
    {
#else
    /// <summary>
    /// Implements groups in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IGroups: ICountable, IDeletable, IListableNames
    {
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Function clears (removes) all assignments from the specified group.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        void Clear(string nameGroup);
#endif

        /// <summary>
        /// Retrieves the assignments to a specified group.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group to get the assignments for.</param>
        /// <param name="objectTypes">Object type of each item in the group corresponding to an integer code.</param>
        /// <param name="objectNames">Name of each item in the group.</param>
        void GetAssignments(string nameGroup,
            ref eObjectType[] objectTypes,
            ref string[] objectNames);


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
        void GetGroup(string name,
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
            ref bool specifiedForMassAndWeight);

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
        void SetGroup(string name,
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
            bool specifiedForMassAndWeight = true);

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
        void GetGroup(string name,
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
            ref bool specifiedForConnectionDesign);

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
        void SetGroup(string name,
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
            bool specifiedForConnectionDesign = true);
#endif
    }
}