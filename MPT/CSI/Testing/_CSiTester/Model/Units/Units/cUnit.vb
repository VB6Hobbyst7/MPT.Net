Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports CSiTester.cEnumerations
Imports CSiTester.cUnitsEnum
Imports CSiTester.cLibLists

''' <summary>
''' Basic unit class that includes the type, name, power, and numerator/denominator position.
''' </summary>
''' <remarks></remarks>
Public Class cUnit
    Implements ICloneable
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Constants"
    Private Const _POWER_CHAR As String = "^"
    Private Const _MULTIPLIER As String = "*"
    Private Const _DIVISOR As String = "/"
    Private Const _OPENPARENTHESIS As String = "("
    Private Const _CLOSEPARENTHESIS As String = ")"
#End Region

#Region "Properties: Friend"
    Private _type As eUnitType
    ''' <summary>
    ''' Type of unit based on the allowed enumerations. 
    ''' This limits what 'unit' can be, as well as other class behavior.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property type As eUnitType
        Set(ByVal value As eUnitType)
            If Not _type = value Then
                _type = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("type"))
                SetUnitsList()
            End If
        End Set
        Get
            Return _type
        End Get
    End Property

    ''' <summary>
    ''' If true, then the unit is in the numerator position. If false, the unit is in the denominator position.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property numerator As Boolean

    Private _name As String
    ''' <summary>
    ''' Specified unit name, such as 'in', 'ft', or 'm' for the 'length' type.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property name As String
        Get
            Return _name
        End Get
    End Property

    ''' <summary>
    ''' The power that a unit is multiplied by, so long as it is greater than 0. e.g. 1/2, 2, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property power As String

    Private _unitsList As New ObservableCollection(Of String)
    ''' <summary>
    '''List of units available for selection based on the unit type selected.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitsList As ObservableCollection(Of String)
        Get
            Return _unitsList
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
        SetDefaults()
    End Sub

    Friend Sub New(ByVal p_type As eUnitType,
                  Optional ByVal p_power As String = "1",
                  Optional ByVal p_isNumerator As Boolean = True)
        InitializeData()
        SetDefaults()

        type = p_type
        power = p_power
        numerator = p_isNumerator
    End Sub

    Private Sub InitializeData()

    End Sub

    Private Sub SetDefaults()
        type = eUnitType.none
        SetUnitsList()

        _name = ""

        numerator = True
        power = "1"
    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cUnit

        With myClone
            .type = type
            .numerator = numerator
            ._name = name
            .power = power

            For Each unitItem As String In unitsList
                .unitsList.Add(unitItem)
            Next
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cUnit) Then Return False

        Dim comparedObject As cUnit = TryCast(p_object, cUnit)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .type = type Then Return False
            If Not .numerator = numerator Then Return False
            If Not .name = name Then Return False
            If Not .power = power Then Return False
            If ListsAreDifferent(.unitsList, unitsList) Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Returns the list of units allowed based on the specified unit type.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <param name="p_type">Unit type for which a list is returned.</param>
    Friend Function GetUnitsList(ByVal p_type As eUnitType) As List(Of String)
        Dim unitListTemp As New List(Of String)

        Select Case p_type
            Case eUnitType.none
            Case eUnitType.unitless
                SetUnitsListAll()   'All are available for selection, with the assumption being that numerator & denominator units are the same.
            Case eUnitType.length
                Dim unit As New cUnitLength
                unitListTemp = unit.unitsList
            Case eUnitType.mass
                Dim unit As New cUnitMass
                unitListTemp = unit.unitsList
            Case eUnitType.rotation
                Dim unit As New cUnitRotation
                unitListTemp = unit.unitsList
            Case eUnitType.temperature
                Dim unit As New cUnitTemperature
                unitListTemp = unit.unitsList
            Case eUnitType.time
                Dim unit As New cUnitTime
                unitListTemp = unit.unitsList
            Case eUnitType.force
                Dim unit As New cUnitForce
                unitListTemp = unit.unitsList
        End Select

        Return unitListTemp
    End Function

    ''' <summary>
    ''' Returns the unit cast as a label string (e.g. 1/length^2).
    ''' </summary>
    ''' <param name="p_parseSchema">If true, then the schema form is returned (e.g. length). Else, the name is returned (e.g. in) (default).</param>
    ''' <param name="p_withPowers">If true (default), then the unit names also have the power listed. 
    ''' Otherwise, only the list of units is returned.</param>
    ''' <param name="p_asList">If true, the label is listing out each component separately, so denominators are each written as '1/b'.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetUnitLabel(Optional ByVal p_parseSchema As Boolean = False,
                                 Optional ByVal p_withPowers As Boolean = False,
                                 Optional ByVal p_asList As Boolean = True) As String
        Dim unitString As String

        If p_parseSchema Then
            unitString = GetEnumDescription(type)
        Else
            unitString = name
            If String.IsNullOrEmpty(unitString) Then unitString = "NA"
        End If

        If p_withPowers AndAlso Not String.IsNullOrEmpty(power) Then
            Dim isFraction As Boolean = False

            For Each letter As Char In power
                If letter = _DIVISOR Then isFraction = True
            Next
            If Not power = "1" Then
                If isFraction Then
                    unitString &= (_POWER_CHAR & _OPENPARENTHESIS & power & _CLOSEPARENTHESIS)
                Else
                    unitString &= (_POWER_CHAR & power)
                End If
            End If

            'Add '1/' to the entry if it is being listed individually. 
            If (Not numerator AndAlso p_asList) Then unitString = "1" & _DIVISOR & unitString
        End If

        Return unitString
    End Function

    ''' <summary>
    ''' Sets the name of the unit.
    ''' If the name is a common variation of the name that should be used, the name is converted.
    ''' </summary>
    ''' <param name="p_name">Unit name to assign.</param>
    ''' <remarks></remarks>
    Friend Sub SetUnitName(ByVal p_name As String)
        ' Adjust unit name for in/inches
        If (String.Compare(p_name, "inch", ignoreCase:=True) = 0) Then p_name = "in"

        ' Set unit name
        _name = p_name
    End Sub

    ''' <summary>
    ''' If the unit type is not already set for the class, an attempt is made to set the unit type based on the current unit string.
    ''' The resulting value is also returned.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetUnitType() As eUnitType
        Dim unitTypeEnum As eUnitType
        Dim unitTypes As New List(Of String)
        Dim unitNames As New List(Of String)

        unitTypes = GetEnumDescriptionList(eUnitType.none)

        'For each unit type, search through the list of allowed unit names to see if any match the one currently assigned to the class
        For Each unitType As String In unitTypes
            unitTypeEnum = ConvertStringToEnumByDescription(Of eUnitType)(unitType)

            unitNames = GetUnitsList(unitTypeEnum)

            For Each unitName As String In unitNames
                If UnitMatch(unitName) Then
                    type = unitTypeEnum
                    Return type
                End If
            Next
        Next

        Return type
    End Function

    ''TODO: Test
    ''' <summary>
    ''' Returns a string of the combined powers when their bases are multiplied. 
    ''' If "/" is used for non-integers in both powers, this format is preserved if both the numerator &amp; denominator are integers. 
    ''' Otherwise, the returned value will either be an integer or decimal.
    ''' </summary>
    ''' <param name="p_existingPower">Power of the current unit. If not specified then it is taken as the class' current power.</param>
    ''' <param name="p_multipliedPower">Power of the unit to combine with the current unit through multiplication.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function MultiplyUnitPowers(ByVal p_multipliedPower As String,
                                       Optional ByVal p_existingPower As String = "") As String
        If String.IsNullOrEmpty(p_existingPower) Then p_existingPower = power

        Dim existingPower As New cSymbolicBlockPower(p_existingPower)
        Dim addedPower As New cSymbolicBlockPower(p_multipliedPower)
        Dim resultingPower As New cSymbolicBlockPower

        resultingPower = existingPower.CombinePowersBaseMultiply(addedPower)

        Return resultingPower.powerString
    End Function

    ''TODO: Test
    ''' <summary>
    ''' Returns a string of the combined powers when their bases are divided. 
    ''' If "/" is used for non-integers in both powers, this format is preserved if both the numerator &amp; denominator are integers. 
    ''' Otherwise, the returned value will either be an integer or decimal.
    ''' </summary>
    ''' <param name="p_numeratorPower">Power of the current unit. If not specified then it is taken as the class' current power.</param>
    ''' <param name="p_denominatorPower">Power of the unit to combine with the current unit through division.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function DivideUnitPowers(ByVal p_denominatorPower As String,
                                     Optional ByVal p_numeratorPower As String = "") As String
        If String.IsNullOrEmpty(p_numeratorPower) Then p_numeratorPower = power

        Dim numeratorPower As New cSymbolicBlockPower(p_numeratorPower)
        Dim denominatorPower As New cSymbolicBlockPower(p_denominatorPower)
        Dim resultingPower As New cSymbolicBlockPower

        resultingPower = numeratorPower.CombinePowersBaseDivide(denominatorPower)

        Return resultingPower.powerString
    End Function

    ''' <summary>
    ''' Returns the conversion factor of changing from one unit to another.
    ''' </summary>
    ''' <param name="p_unitToConvertFrom">Unit object to convert from.</param>
    ''' <param name="p_unitToConvertTo">Unit object to convert to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function Convert(ByVal p_unitToConvertFrom As cUnit,
                                      Optional ByVal p_unitToConvertTo As cUnit = Nothing) As Double
        If p_unitToConvertTo Is Nothing Then p_unitToConvertTo = Me
        Dim unitToConvert As String = p_unitToConvertFrom.name
        Dim unitToReference As String = p_unitToConvertTo.name
        Dim unitConversionFactor As Double = Convert(unitToConvert, unitToReference)

        Return unitConversionFactor
    End Function
    ''' <summary>
    ''' Returns the conversion factor of changing from one unit to another.
    ''' </summary>
    ''' <param name="p_unitToConvertFrom">Name of the unit to convert from. Must be present in the class list of allowed units.</param>
    ''' <param name="p_unitToConvertTo">Unit object to convert to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function Convert(ByVal p_unitToConvertFrom As String,
                                      Optional ByVal p_unitToConvertTo As cUnit = Nothing) As Double
        If p_unitToConvertTo Is Nothing Then p_unitToConvertTo = Me
        Dim unitToReference As String = p_unitToConvertTo.name

        Dim unitConversionFactor As Double = Convert(p_unitToConvertFrom, unitToReference)

        Return unitConversionFactor
    End Function
    ''' <summary>
    ''' Returns the conversion factor of changing from one unit to another.
    ''' </summary>
    ''' <param name="p_unitToConvertFrom">Name of the unit to convert from. Must be present in the class list of allowed units.</param>
    ''' <param name="p_unitToConvertTo">Name of the unit to convert to. Must be present in the class list of allowed units.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function Convert(ByVal p_unitToConvertFrom As String,
                                  Optional ByVal p_unitToConvertTo As String = "") As Double
        Dim unitConversionFactor As Double = 1

        Try
            Select Case type
                Case eUnitType.force
                    Dim unitConverter As New cUnitForce
                    Dim convertFromUnit As cUnitForce.eUnit = ConvertStringToEnumByDescription(Of cUnitForce.eUnit)(p_unitToConvertFrom)
                    Dim convertToUnit As cUnitForce.eUnit = ConvertStringToEnumByDescription(Of cUnitForce.eUnit)(p_unitToConvertTo)

                    unitConversionFactor = unitConverter.Convert(1, convertFromUnit, convertToUnit)
                Case eUnitType.length
                    Dim unitConverter As New cUnitLength
                    Dim convertFromUnit As cUnitLength.eUnit = ConvertStringToEnumByDescription(Of cUnitLength.eUnit)(p_unitToConvertFrom)
                    Dim convertToUnit As cUnitLength.eUnit = ConvertStringToEnumByDescription(Of cUnitLength.eUnit)(p_unitToConvertTo)

                    unitConversionFactor = unitConverter.Convert(1, convertFromUnit, convertToUnit)

                Case eUnitType.location
                    'TODO: Still in development
                    'Dim unitConverter As New cUnitLocation
                    'Dim convertFromUnit As cUnitLocation.eUnit = ConvertStringToEnumByDescription(p_unitToConvert, cUnitLocation.eUnit.none)
                    'Dim convertToUnit As cUnitLocation.eUnit = ConvertStringToEnumByDescription(p_unitToReference, cUnitLocation.eUnit.none)

                    'unitConversionFactor = unitConverter.Convert(1, convertFromUnit, convertToUnit)

                Case eUnitType.mass
                    Dim unitConverter As New cUnitMass
                    Dim convertFromUnit As cUnitMass.eUnit = ConvertStringToEnumByDescription(Of cUnitMass.eUnit)(p_unitToConvertFrom)
                    Dim convertToUnit As cUnitMass.eUnit = ConvertStringToEnumByDescription(Of cUnitMass.eUnit)(p_unitToConvertTo)

                    unitConversionFactor = unitConverter.Convert(1, convertFromUnit, convertToUnit)

                Case eUnitType.rotation
                    Dim unitConverter As New cUnitRotation
                    Dim convertFromUnit As cUnitRotation.eUnit = ConvertStringToEnumByDescription(Of cUnitRotation.eUnit)(p_unitToConvertFrom)
                    Dim convertToUnit As cUnitRotation.eUnit = ConvertStringToEnumByDescription(Of cUnitRotation.eUnit)(p_unitToConvertTo)

                    unitConversionFactor = unitConverter.Convert(1, convertFromUnit, convertToUnit)

                Case eUnitType.temperature
                    Dim unitConverter As New cUnitTemperature
                    Dim convertFromUnit As cUnitTemperature.eUnit = ConvertStringToEnumByDescription(Of cUnitTemperature.eUnit)(p_unitToConvertFrom)
                    Dim convertToUnit As cUnitTemperature.eUnit = ConvertStringToEnumByDescription(Of cUnitTemperature.eUnit)(p_unitToConvertTo)

                    unitConversionFactor = unitConverter.Convert(1, convertFromUnit, convertToUnit)

                Case eUnitType.time
                    Dim unitConverter As New cUnitTime
                    Dim convertFromUnit As cUnitTime.eUnit = ConvertStringToEnumByDescription(Of cUnitTime.eUnit)(p_unitToConvertFrom)
                    Dim convertToUnit As cUnitTime.eUnit = ConvertStringToEnumByDescription(Of cUnitTime.eUnit)(p_unitToConvertTo)

                    unitConversionFactor = unitConverter.Convert(1, convertFromUnit, convertToUnit)

                Case eUnitType.unitless
                    'Nothing to convert
                Case eUnitType.none
                    'Nothing to convert
            End Select
        Catch ex As Exception

        End Try

        Return unitConversionFactor
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Sets the list of available units to set for 'unit' based on the type selected.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnitsList()
        Dim unitListTemp As New List(Of String)

        'Get new unit list
        unitListTemp = GetUnitsList(type)

        'Swap lists
        _unitsList.Clear()
        For Each unit As String In unitListTemp
            _unitsList.Add(unit)
        Next

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("unitsList"))
    End Sub

    ''' <summary>
    ''' Returns a list of all allowed units for all unit types.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetUnitsListAll() As List(Of String)
        Dim unitListTemp As New List(Of String)

        Dim unitLength As New cUnitLength
        unitListTemp = AppendList(unitListTemp, unitLength.unitsList)

        Dim unitMass As New cUnitMass
        unitListTemp = AppendList(unitListTemp, unitMass.unitsList)

        Dim unitRotation As New cUnitRotation
        unitListTemp = AppendList(unitListTemp, unitRotation.unitsList)

        Dim unitTemperature As New cUnitTemperature
        unitListTemp = AppendList(unitListTemp, unitTemperature.unitsList)

        Dim unitTime As New cUnitTime
        unitListTemp = AppendList(unitListTemp, unitTime.unitsList)

        Dim unitForce As New cUnitForce
        unitListTemp = AppendList(unitListTemp, unitForce.unitsList)

        Return unitListTemp
    End Function


    ''' <summary>
    ''' Unit shorthand name matches the currently recorded name, and considering special cases.
    ''' </summary>
    ''' <param name="p_unitName">Shorthand unit name to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnitMatch(ByVal p_unitName As String) As Boolean
        Return (UnitMatchIsSIWithM(name, p_unitName) OrElse
                UnitMatchIsInch(name, p_unitName) OrElse
                UnitMatchDefault(name, p_unitName))
    End Function

    ''' <summary>
    ''' Unit shorthand name matches the currently recorded name considering the following:
    ''' Cap sensitivity is important for SI units of milli vs. mega, e.g. mN vs. MN.
    ''' Otherwise, cap-insensitive comparison is intentional, as programs are not consistent on capitalization of units.
    ''' </summary>
    ''' <param name="p_unitName">Shorthand unit name to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnitMatchIsSIWithM(ByVal p_name As String,
                                        ByVal p_unitName As String) As Boolean
        Return (p_name.Length > 1 AndAlso
                Mid(p_name, 1, 1).ToUpper = "M" AndAlso
                String.Compare(p_name, p_unitName, ignoreCase:=False) = 0)
    End Function

    ''' <summary>
    ''' Unit shorthand name matches the currently recorded name considering the following:
    ''' Since "in" is often a reserved word, this may often come in the form of "inches".
    ''' </summary>
    ''' <param name="p_unitName">Shorthand unit name to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnitMatchIsInch(ByVal p_name As String,
                                     ByVal p_unitName As String) As Boolean
        Return (String.Compare(p_name, "inch", ignoreCase:=True) = 0 AndAlso
                String.Compare(p_unitName, "in", ignoreCase:=True) = 0)
    End Function

    ''' <summary>
    ''' Unit shorthand name matches the currently recorded name.
    ''' </summary>
    ''' <param name="p_unitName">Shorthand unit name to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnitMatchDefault(ByVal p_name As String,
                                      ByVal p_unitName As String) As Boolean
        Return (String.Compare(p_name, p_unitName, ignoreCase:=True) = 0)
    End Function

#End Region
End Class
