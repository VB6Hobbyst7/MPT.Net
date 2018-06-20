Option Strict On
Option Explicit On

''' <summary>
''' Properties of the group, such as color and specified uses.
''' </summary>
''' <remarks></remarks>
Public Class GroupProperties
#Region "Properties"
    ''' <summary>
    ''' Display color for the group specified.
    ''' The integer is the integer expression of the standard hexadecimal code for a given color.
    ''' If this value is input as –1, the program automatically selects a display color for the group.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Color As Integer

    ''' <summary>
    ''' True: The group is specified to be used for selection.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForSelection As Boolean

    ''' <summary>
    ''' True: The group is specified to be used for defining section cuts.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForSectionCutDefinition As Boolean

    ''' <summary>
    ''' True: The group is specified to be used for defining steel frame design groups.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForSteelDesign As Boolean

    ''' <summary>
    ''' True: The group is specified to be used for defining concrete frame design groups.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForConcreteDesign As Boolean

    ''' <summary>
    ''' True: The group is specified to be used for defining alumnimum frame design groups.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForAluminumDesign As Boolean

    ''' <summary>
    ''' True: The group is specified to be used for defining colf formed frame design groups.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForColdFormedDesign As Boolean

    ''' <summary>
    ''' True: The group is specified to be used for defining stages for nonlinear static analysis.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForStaticNLActiveStage As Boolean

    ''' <summary>
    ''' True: The group is specified to be used for reporting bridge response output.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForBridgeResponseOutput As Boolean

    ''' <summary>
    ''' True: The group is specified to be used for reporting auto seismic loads.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForAutoSeismicOutput As Boolean

    ''' <summary>
    ''' True: The group is specified to be used for reporting auto wind loads.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForAutoWindOutput As Boolean

    ''' <summary>
    ''' True: The group is specified to be used for reporting group masses and weight.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ForMassAndWeight As Boolean
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Gets the group properties, such as display color and usages.
    ''' </summary>
    ''' <param name="nameGroup">Name of an existing group to get the properties for.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GroupProperties(ByVal nameGroup As String) As GroupProperties
        Dim newGroup As New GroupProperties

        With newGroup
            GetGroup(nameGroup,
                     .Color,
                     .ForSelection,
                     .ForSectionCutDefinition,
                     .ForSteelDesign,
                     .ForConcreteDesign,
                     .ForAluminumDesign,
                     .ForColdFormedDesign,
                     .ForStaticNLActiveStage,
                     .ForBridgeResponseOutput,
                     .ForAutoSeismicOutput,
                     .ForAutoWindOutput,
                     .ForMassAndWeight)
        End With

        Return newGroup
    End Function

    ''' <summary>
    ''' Sets group properties, such as display color and usages.
    ''' </summary>
    ''' <param name="nameGroup">Group name to assign the properties to.</param>
    ''' <param name="groupProperties">Group properties object to base the settings on.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetGroupProperties(ByVal nameGroup As String,
                                       ByVal groupProperties As GroupProperties) As Boolean
        With groupProperties
            Return SetGroup(nameGroup,
                            .Color,
                            .ForSelection,
                            .ForSectionCutDefinition,
                            .ForSteelDesign,
                            .ForConcreteDesign,
                            .ForAluminumDesign,
                            .ForColdFormedDesign,
                            .ForStaticNLActiveStage,
                            .ForBridgeResponseOutput,
                            .ForAutoSeismicOutput,
                            .ForAutoWindOutput,
                            .ForMassAndWeight)
        End With
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Gets the group properties, such as display color and usages.
    ''' </summary>
    ''' <param name="Name">Name of an existing group to get the properties for.</param>
    ''' <param name="color">Display color for the group specified.</param>
    ''' <param name="SpecifiedForSelection">True: The group is specified to be used for selection.</param>
    ''' <param name="SpecifiedForSectionCutDefinition">True: The group is specified to be used for defining section cuts.</param>
    ''' <param name="SpecifiedForSteelDesign">True: The group is specified to be used for defining steel frame design groups.</param>
    ''' <param name="SpecifiedForConcreteDesign">True: The group is specified to be used for defining concrete frame design groups.</param>
    ''' <param name="SpecifiedForAluminumDesign">True: The group is specified to be used for defining alumnimum frame design groups.</param>
    ''' <param name="SpecifiedForColdFormedDesign">True: The group is specified to be used for defining colf formed frame design groups.</param>
    ''' <param name="SpecifiedForStaticNLActiveStage">True: The group is specified to be used for defining stages for nonlinear static analysis.</param>
    ''' <param name="SpecifiedForBridgeResponseOutput">True: The group is specified to be used for reporting bridge response output.</param>
    ''' <param name="SpecifiedForAutoSeismicOutput">True: The group is specified to be used for reporting auto seismic loads.</param>
    ''' <param name="SpecifiedForAutoWindOutput">True: The group is specified to be used for reporting auto wind loads.</param>
    ''' <param name="SpecifiedForMassAndWeight">True: The group is specified to be used for reporting group masses and weight.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGroup(ByVal Name As String,
                             ByRef color As Integer,
                             ByRef SpecifiedForSelection As Boolean,
                             ByRef SpecifiedForSectionCutDefinition As Boolean,
                             ByRef SpecifiedForSteelDesign As Boolean,
                             ByRef SpecifiedForConcreteDesign As Boolean,
                             ByRef SpecifiedForAluminumDesign As Boolean,
                             ByRef SpecifiedForColdFormedDesign As Boolean,
                             ByRef SpecifiedForStaticNLActiveStage As Boolean,
                             ByRef SpecifiedForBridgeResponseOutput As Boolean,
                             ByRef SpecifiedForAutoSeismicOutput As Boolean,
                             ByRef SpecifiedForAutoWindOutput As Boolean,
                             ByRef SpecifiedForMassAndWeight As Boolean) As Boolean
        ret = SapModel.GroupDef.GetGroup(Name, color, SpecifiedForSelection, SpecifiedForSectionCutDefinition, SpecifiedForSteelDesign, SpecifiedForConcreteDesign, SpecifiedForAluminumDesign, SpecifiedForColdFormedDesign, SpecifiedForStaticNLActiveStage, SpecifiedForBridgeResponseOutput, SpecifiedForAutoSeismicOutput, SpecifiedForAutoWindOutput, SpecifiedForMassAndWeight)
        Return retCheck(ret, "SapModel.GroupDef.GetGroup")
    End Function

    ''' <summary>
    ''' Sets group properties, such as display color and usages.
    ''' </summary>
    ''' <param name="Name">Group name to assign the properties to.</param>
    ''' <param name="color">Display color for the group specified.
    ''' If this value is input as –1, the program automatically selects a display color for the group.</param>
    ''' <param name="SpecifiedForSelection">True: The group is specified to be used for selection.</param>
    ''' <param name="SpecifiedForSectionCutDefinition">True: The group is specified to be used for defining section cuts.</param>
    ''' <param name="SpecifiedForSteelDesign">True: The group is specified to be used for defining steel frame design groups.</param>
    ''' <param name="SpecifiedForConcreteDesign">True: The group is specified to be used for defining concrete frame design groups.</param>
    ''' <param name="SpecifiedForAluminumDesign">True: The group is specified to be used for defining alumnimum frame design groups.</param>
    ''' <param name="SpecifiedForColdFormedDesign">True: The group is specified to be used for defining colf formed frame design groups.</param>
    ''' <param name="SpecifiedForStaticNLActiveStage">True: The group is specified to be used for defining stages for nonlinear static analysis.</param>
    ''' <param name="SpecifiedForBridgeResponseOutput">True: The group is specified to be used for reporting bridge response output.</param>
    ''' <param name="SpecifiedForAutoSeismicOutput">True: The group is specified to be used for reporting auto seismic loads.</param>
    ''' <param name="SpecifiedForAutoWindOutput">True: The group is specified to be used for reporting auto wind loads.</param>
    ''' <param name="SpecifiedForMassAndWeight">True: The group is specified to be used for reporting group masses and weight.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetGroup(ByVal Name As String,
                             Optional ByVal color As Integer = -1,
                             Optional ByVal SpecifiedForSelection As Boolean = True,
                             Optional ByVal SpecifiedForSectionCutDefinition As Boolean = True,
                             Optional ByVal SpecifiedForSteelDesign As Boolean = True,
                             Optional ByVal SpecifiedForConcreteDesign As Boolean = True,
                             Optional ByVal SpecifiedForAluminumDesign As Boolean = True,
                             Optional ByVal SpecifiedForColdFormedDesign As Boolean = True,
                             Optional ByVal SpecifiedForStaticNLActiveStage As Boolean = True,
                             Optional ByVal SpecifiedForBridgeResponseOutput As Boolean = True,
                             Optional ByVal SpecifiedForAutoSeismicOutput As Boolean = False,
                             Optional ByVal SpecifiedForAutoWindOutput As Boolean = False,
                             Optional ByVal SpecifiedForMassAndWeight As Boolean = True) As Boolean
        ret = SapModel.GroupDef.SetGroup(Name, color, SpecifiedForSelection, SpecifiedForSectionCutDefinition, SpecifiedForSteelDesign, SpecifiedForConcreteDesign, SpecifiedForAluminumDesign, SpecifiedForColdFormedDesign, SpecifiedForStaticNLActiveStage, SpecifiedForBridgeResponseOutput, SpecifiedForAutoSeismicOutput, SpecifiedForAutoWindOutput, SpecifiedForMassAndWeight)
        Return retCheck(ret, "SapModel.GroupDef.SetGroup")
    End Function
#End Region
End Class
