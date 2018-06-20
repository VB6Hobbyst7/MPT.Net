Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports CSiTester.cEnumerations
Imports CSiTester.cUnitsEnum

''' <summary>
''' A schema type that has various shorthand labels that correspond with particular unit names filled into the schema.
''' Provides a list of shorthand labels allowed and returns a units object with a schema and unit names appropriate to a chosen shorthand label.
''' </summary>
''' <remarks></remarks>
Public Class cUnitSpeedAngular
#Region "Enumerations"
    ''' <summary>
    ''' List of the unit shorthand names available for this schema type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eUnit
        <Description("")> none
        <Description("1/sec")> oneCyclePerSecond
        <Description("s^-1")> oneCyclePerSecondAlt
        <Description("rpm")> rotationsPerMinute
        <Description("Hz")> hertz
        <Description("kHz")> kiloHertz
        <Description("MHz")> megaHertz
        <Description("GHz")> gigaHertz
    End Enum
#End Region

#Region "Properties: Friend"
    ''' <summary>
    ''' Specified unit.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property unit As eUnit

    Private _unitDefault As eUnit = eUnit.none
    ''' <summary>
    ''' Default unit set for this unit type.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitDefault As eUnit
        Get
            Return _unitDefault
        End Get
    End Property

    Private _unitsList As New List(Of String)
    ''' <summary>
    '''  List of pressure units.
    ''' </summary>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitsList As List(Of String)
        Get
            Return _unitsList
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
        SetToDefault()
    End Sub

    Private Sub InitializeData()
        _unitsList = GetEnumDescriptionList(eUnit.none)
    End Sub

    ''' <summary>
    ''' Sets the shorthand label back to the default type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetToDefault()
        unit = _unitDefault
    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Returns the units object derived from the provided shorthand units name.
    ''' If the name does not match a valid shorthand unit, Nothing is returned.
    ''' </summary>
    ''' <param name="p_name">Name of the shorthand unit to use for the units object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetUnits(ByVal p_name As String) As cUnits
        SetUnitByName(p_name)

        If Not unit = eUnit.none Then
            Return GetUnits(unit)
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>
    ''' Returns the units object derived from the provided shorthand units enumeration.
    ''' </summary>
    ''' <param name="p_shorthandUnit">Shorthand unit to use in generating the units object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetUnits(Optional ByVal p_shorthandUnit As eUnit = eUnit.none) As cUnits
        Dim unitRotation As New cUnit
        unitRotation.type = eUnitType.rotation

        Dim unitTime As New cUnit
        With unitTime
            .type = eUnitType.time
            .numerator = False
        End With

        Select Case p_shorthandUnit
            Case eUnit.none
                'No action
            Case eUnit.oneCyclePerSecond, eUnit.oneCyclePerSecondAlt
                unitRotation.SetUnitName(GetEnumDescription(cUnitRotation.eUnit.cycle))
                unitTime.SetUnitName(GetEnumDescription(cUnitTime.eUnit.second))
            Case eUnit.rotationsPerMinute
                unitRotation.SetUnitName(GetEnumDescription(cUnitRotation.eUnit.cycle))
                unitTime.SetUnitName(GetEnumDescription(cUnitTime.eUnit.minute))
            Case eUnit.hertz
                unitRotation.SetUnitName(GetEnumDescription(cUnitRotation.eUnit.cycle))
                unitTime.SetUnitName(GetEnumDescription(cUnitTime.eUnit.second))
            Case eUnit.kiloHertz
                unitRotation.SetUnitName(GetEnumDescription(cUnitRotation.eUnit.kiloCycle))
                unitTime.SetUnitName(GetEnumDescription(cUnitTime.eUnit.second))
            Case eUnit.megaHertz
                unitRotation.SetUnitName(GetEnumDescription(cUnitRotation.eUnit.megaCycle))
                unitTime.SetUnitName(GetEnumDescription(cUnitTime.eUnit.second))
            Case eUnit.gigaHertz
                unitRotation.SetUnitName(GetEnumDescription(cUnitRotation.eUnit.gigaCycle))
                unitTime.SetUnitName(GetEnumDescription(cUnitTime.eUnit.second))
        End Select

        Dim unitCollection As New List(Of cUnit)
        With unitCollection
            .Add(unitRotation)
            .Add(unitTime)
        End With

        Return AssembleUnits(p_shorthandUnit, unitCollection)
    End Function

    ''' <summary>
    ''' Sets the specific shorthand unit by the provided string name.
    ''' </summary>
    ''' <param name="p_shorthandName">Name of the shorthand unit to use.</param>
    ''' <remarks></remarks>
    Friend Sub SetUnitByName(ByVal p_shorthandName As String)
        unit = ConvertStringToEnumByDescription(Of eUnit)(p_shorthandName)
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Assembles the provided list of unit objects into a single units object, and records the correspdonding shorthand label with the units object.
    ''' </summary>
    ''' <param name="p_shorthandUnit">Shorthand unit that the operation is based on.</param>
    ''' <param name="p_units">List of unit objects to assemble into one units object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AssembleUnits(ByVal p_shorthandUnit As eUnit,
                                ByVal p_units As List(Of cUnit)) As cUnits
        Dim units As New cUnits
        With units
            If Not p_shorthandUnit = eUnit.none Then .shorthandLabel = GetEnumDescription(p_shorthandUnit)

            For Each unit As cUnit In p_units
                .AddUnit(unit)
            Next
        End With

        Return units
    End Function
#End Region
End Class
