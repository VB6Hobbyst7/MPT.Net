Option Strict On
Option Explicit On

''' <summary>
''' Value object that contains a magnitude and associated units. 
''' This value can convert the value and units to a new set of units.
''' </summary>
''' <remarks></remarks>
Public Class cValue
#Region "Properties: Public"
    Private _magnitude As String
    ''' <summary>
    ''' Magnitude of the value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Magnitude As String
        Get
            Return _magnitude
        End Get
    End Property


    Private ReadOnly _units As New cUnitsController
    ''' <summary>
    ''' Units of the value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Units As String
        Get
            Dim unitLabel As String = _units.units.shorthandLabel
            If String.IsNullOrEmpty(unitLabel) Then unitLabel = _units.units.GetUnitsLabel

            Return unitLabel
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Create new value.
    ''' </summary>
    ''' <param name="p_magnitude">Magnitude of the value.</param>
    ''' <param name="p_units">Units of the value (e.g. kN*m/sec).</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal p_magnitude As Double,
                   ByVal p_units As String)
        setMagnitude(p_magnitude.ToString())
        If unitsCanBeParsed(p_units) Then _units.ParseStringToUnits(p_units)
    End Sub

    ''' <summary>
    ''' Units are only set if the magnitude is numeric and units are not a list.
    ''' </summary>
    ''' <param name="p_units"></param>
    ''' <returns></returns>
    Private Function unitsCanBeParsed(ByVal p_units As String) As Boolean
        Return ((String.IsNullOrEmpty(_magnitude) OrElse IsNumeric(_magnitude)) AndAlso
                (Not IsNumeric(p_units) AndAlso Not _units.isConsistent(p_units)))
    End Function



    ''' <summary>
    ''' Create new value.
    ''' </summary>
    ''' <param name="p_magnitude">Magnitude of the value. 
    ''' Must be numeric if unit features are to be used.</param>
    ''' <param name="p_units">Units of the value (e.g. kN*m/sec).</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal p_magnitude As String,
                   ByVal p_units As String)
        setMagnitude(p_magnitude)
        If unitsCanBeParsed(p_units) Then _units.ParseStringToUnits(p_units)
    End Sub


    Public Sub New(ByVal p_magnitudeOrUnits As String)
        If _units.isConsistent(p_magnitudeOrUnits) Then Return
        If unitsCanBeParsed(p_magnitudeOrUnits) Then _units.ParseStringToUnits(p_magnitudeOrUnits)

        '' TODO: Validate that units are of valid types. If not, set them blank and make value the magnitude
        If String.IsNullOrEmpty(Units) Then setMagnitude(p_magnitudeOrUnits)
    End Sub

    Public Sub New(ByVal p_value As Double)
        setMagnitude(p_value.ToString())
    End Sub

    Private Sub setMagnitude(ByVal p_magnitude As String)
        If String.IsNullOrEmpty(p_magnitude) Then
            _magnitude = "0"
        Else
            _magnitude = p_magnitude
        End If
    End Sub

#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Converts the value according to the provided set of units.
    ''' </summary>
    ''' <param name="p_units">Units to convert the value to. These can be provided in two formats: 
    ''' 1. A new set of units. Must be in the same schema as the existing units (e.g. kN*m/sec).
    ''' 2. A list of units, if the value is to be converted to a consistent set of units for a given unit type (e.g. N, mm, hr).</param>
    ''' <remarks></remarks>
    Public Sub ConvertTo(ByVal p_units As String)
        If (Not IsNumeric(_magnitude) OrElse
            String.IsNullOrEmpty(Units)) Then Exit Sub

        ' Convert the value
        Dim conversionValue As Double = _units.ConvertTo(p_units)
        Dim valueNewNumeric As Double = CDbl(_magnitude) * conversionValue
        _magnitude = CStr(valueNewNumeric)

        ' Update the units
        If _units.isConsistent(p_units) Then
            _units.MakeUnitsConsistent(p_units)
        Else
            _units.ParseStringToUnits(p_units)
        End If
    End Sub


    Public Sub ConvertFrom(ByVal p_magnitude As String,
                           ByVal p_units As String)
        If (Not IsNumeric(_magnitude) OrElse
            Not IsNumeric(p_magnitude) OrElse
            String.IsNullOrEmpty(Units)) Then Exit Sub

        ' Convert the value
        Dim conversionValue As Double = _units.ConvertFrom(p_units)
        Dim valueNewNumeric As Double = CDbl(p_magnitude) * conversionValue
        _magnitude = CStr(valueNewNumeric)
    End Sub
#End Region

End Class
