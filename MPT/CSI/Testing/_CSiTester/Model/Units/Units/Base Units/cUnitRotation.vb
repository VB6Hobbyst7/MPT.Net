Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports CSiTester.cEnumerations
Imports CSiTester.cUnitsEnum

''' <summary>
''' Base unit of the type 'rotation'.
''' Provides a list of unit names allowed and performs unit conversions.
''' </summary>
''' <remarks></remarks>
Public Class cUnitRotation
#Region "Enumerations"
    ''' <summary>
    ''' List of the unit names available for this unit type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eUnit
        <Description("")> none
        <Description("rad")> radian
        <Description("deg")> degree
        <Description("cyc")> cycle
        ''' <summary>
        ''' Used for kHZ derived unit.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("10^3 cyc")> kiloCycle
        ''' <summary>
        ''' Used for MHZ derived unit.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("10^6 cyc")> megaCycle
        ''' <summary>
        ''' Used for GHZ derived unit.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("10^9 cyc")> gigaCycle
    End Enum
#End Region

#Region "Constants"
    Private Const _DEGREE_TO_RADIAN As Double = Math.PI / 180
    Private Const _CYCLE_TO_RADIAN As Double = 2 * Math.PI

    'Within SI
    Private Const _KILO As Integer = 1000
    Private Const _MEGA As Integer = 1000000
    Private Const _GIGA As Integer = 1000000000
#End Region

#Region "Properties: Friend"
    ''' <summary>
    ''' Specified unit.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property unit As eUnit

    Private _unitDefault As eUnit = eUnit.radian
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
    '''  List of rotation units.
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
        unit = _unitDefault
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
            Case eUnit.radian 'Default value. No action needed.
            Case eUnit.degree
                p_value *= _DEGREE_TO_RADIAN
            Case eUnit.cycle
                p_value *= _CYCLE_TO_RADIAN
            Case eUnit.kiloCycle
                p_value *= (_KILO * _CYCLE_TO_RADIAN)
            Case eUnit.megaCycle
                p_value *= (_MEGA * _CYCLE_TO_RADIAN)
            Case eUnit.gigaCycle
                p_value *= (_GIGA * _CYCLE_TO_RADIAN)
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
            Case eUnit.degree
                p_value /= _DEGREE_TO_RADIAN
            Case eUnit.cycle
                p_value /= _CYCLE_TO_RADIAN
            Case eUnit.kiloCycle
                p_value /= (_KILO * _CYCLE_TO_RADIAN)
            Case eUnit.megaCycle
                p_value /= (_MEGA * _CYCLE_TO_RADIAN)
            Case eUnit.gigaCycle
                p_value /= (_GIGA * _CYCLE_TO_RADIAN)
            Case Else : Return 0
        End Select

        Return p_value
    End Function

#End Region
End Class
