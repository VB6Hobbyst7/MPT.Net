Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports CSiTester.cEnumerations
Imports CSiTester.cUnitsEnum

''' <summary>
''' Base unit of the type 'time'.
''' Provides a list of unit names allowed and performs unit conversions.
''' </summary>
''' <remarks></remarks>
Public Class cUnitTime
#Region "Enumerations"
    ''' <summary>
    ''' List of the unit names available for this unit type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eUnit
        <Description("")> none
        <Description("sec")> second
        <Description("min")> minute
        <Description("hr")> hour
        <Description("day")> day
        <Description("week")> week
        <Description("month")> month
        <Description("year")> year
    End Enum
#End Region

#Region "Constants"
    Private Const _MINUTE_TO_SEC As Integer = 60
    Private Const _HOUR_TO_SEC As Integer = 3600
    ''' <summary>
    ''' Day is taken as 24 hours.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const _DAY_TO_SEC As Integer = 86400
    ''' <summary>
    ''' Week is taken as 7 days of 24 hours.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const _WEEK_TO_SEC As Integer = 604800
    ''' <summary>
    ''' The month is a unit of time, used with calendars, which is approximately as long as some natural period related to the motion of the Moon (i.e. "Moonth"). 
    ''' The mean month length of the Gregorian calendar is 30.436875 days.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const _MONTH_TO_SEC As Double = 2629746
    ''' <summary>
    ''' Gregorian calendar year for one revolution of the earth around the sun.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const _YEAR_TO_SEC As Double = 31556952
#End Region

#Region "Properties: Friend"
    ''' <summary>
    ''' Specified unit.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property unit As eUnit

    Private _unitDefault As eUnit = eUnit.second
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
    '''  List of time units.
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
    ''' Sets the unit type back to the default unit type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetToDefault()
        unit = eUnit.second
    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Converts from one unit to another and returns the conversion factor.
    ''' </summary>
    ''' <param name="p_value">Original value associated with the unit. 
    ''' Use '1' if solely obtaining the conversion factor between units. 
    ''' The result is what to multiply the 'value' by to perform the conversion.</param>
    ''' <param name="p_unit">Unit type to convert from.</param>
    ''' <param name="p_unitResult">Unit type to convert to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function Convert(ByVal p_value As Double,
                           ByVal p_unit As eUnit,
                           Optional ByVal p_unitResult As eUnit = eUnit.none) As Double
        Dim value As Double = p_value

        If Not p_unitResult = eUnit.none And p_unit = p_unitResult Then Return value

        'Convert to default unit
        value = ConvertToBase(p_value, p_unit)

        'If necessary, convert to other unit
        If (Not p_unitResult = eUnit.none AndAlso Not p_unitResult = unitDefault) Then
            value = ConvertFromBase(value, p_unitResult)
        End If

        Return value
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Converts the provided unit to the default unit type and returns the value for the conversion, which is multiplied by the supplied value.
    ''' </summary>
    ''' <param name="p_value">Conversion value. Typically starts as '1'.</param>
    ''' <param name="p_unit">Unit to convert to the default unit type.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToBase(ByVal p_value As Double,
                                    ByVal p_unit As eUnit) As Double
        Select Case p_unit
            Case eUnit.second 'Default value. No action needed.
            Case eUnit.minute
                p_value *= _MINUTE_TO_SEC
            Case eUnit.hour
                p_value *= _HOUR_TO_SEC
            Case eUnit.day
                p_value *= _DAY_TO_SEC
            Case eUnit.week
                p_value *= _WEEK_TO_SEC
            Case eUnit.month
                p_value *= _MONTH_TO_SEC
            Case eUnit.year
                p_value *= _YEAR_TO_SEC
            Case Else : Return 0
        End Select

        Return p_value
    End Function

    ''' <summary>
    ''' Converts the provided unit from the default unit type to the specified unit type and returns the value for the conversion, which supplied value is divided by.
    ''' </summary>
    ''' <param name="p_value">Conversion value. Typically starts as "1" if it is not passed along from a prior conversion method.</param>
    ''' <param name="p_unitTarget">Unit object to which the conversion is to take place.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertFromBase(ByVal p_value As Double,
                                      ByVal p_unitTarget As eUnit) As Double

        Select Case p_unitTarget
            Case eUnit.minute
                p_value /= _MINUTE_TO_SEC
            Case eUnit.hour
                p_value /= _HOUR_TO_SEC
            Case eUnit.day
                p_value /= _DAY_TO_SEC
            Case eUnit.week
                p_value /= _WEEK_TO_SEC
            Case eUnit.month
                p_value /= _MONTH_TO_SEC
            Case eUnit.year
                p_value /= _YEAR_TO_SEC
            Case Else : Return 0
        End Select

        Return p_value
    End Function
#End Region
End Class
